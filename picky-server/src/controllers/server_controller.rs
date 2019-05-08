use saphir::*;
use serde_json;
use serde_json::Value;
use base64::URL_SAFE_NO_PAD;

use crate::configuration::ServerConfig;
use picky_core::controllers::core_controller::CoreController;
use crate::db::backend::BackendStorage;
use crate::utils::*;

const CERT_PREFIX: &str = "-----BEGIN CERTIFICATE-----\n";
const CERT_SUFFIX: &str = "\n-----END CERTIFICATE-----\n";
const KEY_PREFIX: &str = "-----BEGIN RSA PRIVATE KEY-----\n";
const KEY_SUFFIX: &str = "\n-----END RSA PRIVATE KEY-----";
const SUBJECT_KEY_IDENTIFIER: &[u64] = &[2, 5, 29, 14];
const AUTHORITY_KEY_IDENTIFIER_OID: &[u64] = &[2, 5, 29, 35];

pub enum CertFormat{
    Der = 0,
    Pem = 1
}

pub struct ControllerData{
    pub repos: Box<BackendStorage>,
    pub config: ServerConfig
}

pub struct ServerController{
    dispatch: ControllerDispatch<ControllerData>
}

impl ServerController {
    pub fn new(repos: Box<BackendStorage>, config: ServerConfig) -> Self{
        let controller_data = ControllerData{
            repos,
            config
        };

        let dispatch = ControllerDispatch::new(controller_data);
        dispatch.add(Method::GET, "/chain/<api-key>", chains);
        dispatch.add(Method::POST, "/signcert/", sign_cert);
        dispatch.add(Method::POST, "/name/", request_name);
        dispatch.add(Method::GET, "/health/", health);
        dispatch.add(Method::GET, "/cert/<format>/<multihash>", cert);

        ServerController {
            dispatch
        }
    }
}

impl Controller for ServerController{
    fn handle(&self, req: &mut SyncRequest, res: &mut SyncResponse){
        self.dispatch.dispatch(req, res);
    }

    fn base_path(&self) -> &str{
        "/"
    }
}

impl From<String> for CertFormat{
    fn from(format: String) -> Self{
        if format.to_lowercase().eq("der"){
            return CertFormat::Der;
        } else {
            return CertFormat::Pem;
        }
    }
}

pub fn health(_controller_data: &ControllerData, _req: &SyncRequest, res: &mut SyncResponse){
    res.status(StatusCode::OK).body("Everything should be alright!");
}

pub fn sign_cert(controller_data: &ControllerData, req: &SyncRequest, res: &mut SyncResponse){
    res.status(StatusCode::BAD_REQUEST);
    let repos = &mut controller_data.repos.clone();

    if let Ok(body) = String::from_utf8(req.body().clone()) {
        if let Ok(json) = serde_json::from_str::<Value>(body.as_ref()) {
            let mut ca = json["ca"].to_string();
            ca = ca.trim_matches('"').to_string();
            let mut csr = json["csr"].to_string().trim_matches('"').replace("\\n", "\n").to_string();
            csr = csr.trim_matches('"').to_string();

            if let Ok(ca) = repos.find(ca.trim_matches('"')) {
                if ca.len() > 0{
                    if let Ok(ca_cert) = repos.get_cert(&ca[0].value){
                        if let Ok(ca_key) = repos.get_key(&ca[0].value){
                            if let Some(cert) = CoreController::generate_certificate_from_csr(&pem_to_der(&ca_cert).unwrap(), &pem_to_der(&ca_key).unwrap(), controller_data.config.key_config.hash_type, &csr){
                                if let Ok(ski) = CoreController::get_key_identifier(&cert.certificate_der, SUBJECT_KEY_IDENTIFIER){
                                    let pem = format!("{}{}{}", CERT_PREFIX, &der_to_pem(&cert.certificate_der), CERT_SUFFIX);
                                    if let Err(e) = repos.store(&cert.common_name.clone(), &pem , &der_to_pem(&cert.keys.key_der), &ski.clone()){
                                        return info!("{}",&format!("Insertion error for leaf {}: {}", &cert.common_name.clone(), e));
                                    }
                                    res.body(fix_pem(&pem));
                                    res.status(StatusCode::OK);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

pub fn cert(controller_data: &ControllerData, req: &SyncRequest, res: &mut SyncResponse){
    res.status(StatusCode::BAD_REQUEST);
    let repos = &controller_data.repos;

    if let Some(multihash) = req.captures().get("multihash"){
        if let Some(format) = req.captures().get("format"){
            match repos.get_cert(multihash) {
                Ok(ca_cert) => {
                    if (CertFormat::from(format.to_string()) as u8) == 0{
                        res.body(pem_to_der(&ca_cert).unwrap());
                    } else {
                        res.body(ca_cert);
                    }
                    res.status(StatusCode::OK);
                },
                Err(e) => {
                    if let Ok(multihash) = sha256_to_multihash(multihash) {
                        if let Ok(ca_cert) = repos.get_cert(&multihash){
                            if (CertFormat::from(format.to_string()) as u8) == 0{
                                res.body(pem_to_der(&ca_cert).unwrap());
                            } else {
                                res.body(fix_pem(&ca_cert));
                            }
                            res.status(StatusCode::OK);
                        }
                    } else {
                        error!("{}", e);
                    }
                }
            }
        }
    }
}

pub fn chains(controller_data: &ControllerData, req: &SyncRequest, res: &mut SyncResponse){
    res.status(StatusCode::BAD_REQUEST);
    let repos = &controller_data.repos;

    if let Some(common_name) = req.captures().get("api-key").and_then(|c| base64::decode_config(c, URL_SAFE_NO_PAD).ok()){
        let decoded = String::from_utf8_lossy(&common_name);

        if let Ok(intermediate) = repos.find(decoded.clone().trim_matches('"').trim_matches('\0')) {
            if intermediate.len() > 0{
                if let Ok(cert) = repos.get_cert(&intermediate[0].value){
                    let mut chain = fix_pem(&cert.clone());

                    let mut key_identifier = String::default();
                    loop {
                        if let Ok(aki) = CoreController::get_key_identifier(&pem_to_der(&cert).unwrap(), AUTHORITY_KEY_IDENTIFIER_OID){
                            if key_identifier == aki{
                                break;
                            }

                            key_identifier = aki.clone();

                            if let Ok(hash) = repos.get_hash_from_key_identifier(&aki){
                                if let Ok(cert) = repos.get_cert(&hash){
                                    chain.push_str(&fix_pem(&cert.clone()));
                                } else {
                                    break;
                                }
                            } else {
                                break;
                            }
                        } else {
                            break;
                        }
                    }
                    res.body(chain.to_string());
                    res.status(StatusCode::OK);
                }
            }
        }
    }
}

pub fn request_name(_controller_data: &ControllerData, req: &SyncRequest, res: &mut SyncResponse){
    res.status(StatusCode::BAD_REQUEST);

    if let Ok(body) = String::from_utf8(req.body().clone()) {
        if let Ok(json) = serde_json::from_str::<Value>(body.as_ref()){
            let csr = json["csr"].to_string().trim_matches('"').replace("\\n", "\n");
            if let Ok(common_name) = CoreController::request_name(&csr){
                res.body(common_name);
                res.status(StatusCode::OK);
            }
        }
    }
}

pub fn generate_root_ca(config: &ServerConfig, repos: &mut Box<BackendStorage>) -> Result<bool, String>{
    if let Ok(certs) = repos.find(&format!("{} Root CA", config.realm)) {
        if certs.len() > 0 {
            return Ok(false);
        }
    }

    if let Some(root) = CoreController::generate_root_ca(&config.realm, config.key_config.hash_type, config.key_config.key_type){
        let ski = CoreController::get_key_identifier(&root.certificate_der, SUBJECT_KEY_IDENTIFIER)?;
        if let Err(e) = repos.store(&root.common_name.clone(), &format!("{}{}{}", CERT_PREFIX, &der_to_pem(&root.certificate_der.clone()), CERT_SUFFIX), &der_to_pem(&root.keys.key_der.clone()) , &ski.clone()){
            return Err(format!("Insertion error: {:?}", e));
        }
    }

    Ok(true)
}

pub fn generate_intermediate(config: &ServerConfig, repos: &mut Box<BackendStorage>) -> Result<bool, String>{
    if let Ok(certs) = repos.find(&format!("{} Authority", config.realm)) {
        if certs.len() > 0 {
            return Ok(false);
        }
    }

    let root = match repos.find(&format!("{} Root CA", config.realm)){
        Ok(r) => r,
        Err(e) => {
            return Err(format!("Could not find root: {}", e));
        }
    };

    if let Ok(root_cert) = repos.get_cert(&root[0].value){
        if let Ok(root_key) = repos.get_key(&root[0].value){
            if let Some(intermediate) = CoreController::generate_intermediate_ca(&pem_to_der(&root_cert).unwrap(), &pem_to_der(&root_key).unwrap(), &config.realm, config.key_config.hash_type, config.key_config.key_type){
                if let Ok(ski) = CoreController::get_key_identifier(&intermediate.certificate_der, SUBJECT_KEY_IDENTIFIER){
                    if let Err(e) = repos.store(&intermediate.common_name.clone(), &format!("{}{}{}", CERT_PREFIX, &der_to_pem(&intermediate.certificate_der), CERT_SUFFIX), &der_to_pem(&intermediate.keys.key_der) , &ski.clone()){
                        return Err(format!("Insertion error: {:?}", e));
                    }
                    return Ok(true)
                }
            }
        }
    }

    Err("Error while creating intermediate".to_string())
}

pub fn check_certs_in_env(config: &ServerConfig, repos: &mut Box<BackendStorage>) -> Result<(), String> {
    if !config.root_cert.is_empty() && !config.root_key.is_empty() {
        if let Err(e) = get_and_store_env_cert_info(&config.root_cert, &config.root_key, repos) {
            return Err(e);
        }
    }

    if !config.intermediate_cert.is_empty() && !config.intermediate_key.is_empty() {
        if let Err(e) = get_and_store_env_cert_info(&config.intermediate_cert, &config.intermediate_key, repos) {
            return Err(e);
        }
    }

    Ok(())
}

fn get_and_store_env_cert_info(cert: &str, key: &str, repos: &mut Box<BackendStorage>) -> Result<(), String>{
    let der = pem_to_der(&cert)?;
    match CoreController::get_key_identifier(&der, SUBJECT_KEY_IDENTIFIER) {
        Ok(ski) => {
            match CoreController::get_subject_name(&der){
                Ok(name) => {
                    let name = name.trim_start_matches("CN=");
                    let key = key.replace(KEY_PREFIX, "").replace(KEY_SUFFIX, "");
                    if let Err(e) = repos.store(name, cert, &key, &ski){
                        return Err(e);
                    }
                    return Ok(());
                },
                Err(e) => return Err(e)
            }
        },
        Err(e) => return Err(e)
    }
}