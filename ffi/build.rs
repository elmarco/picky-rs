#[cfg(not(target_os = "windows"))]
use other::main_stub;
#[cfg(target_os = "windows")]
use win::main_stub;

fn main() {
    main_stub();
}

#[cfg(target_os = "windows")]
mod win {
    extern crate embed_resource;

    use std::env;
    use std::fs::File;
    use std::io::Write;

    fn generate_version_rc() -> String {
        let output_name = "DevolutionsPicky";
        let filename = format!("{}.dll", output_name);
        let company_name = "Devolutions Inc.";
        let legal_copyright = format!("Copyright 2019-2022 {}", company_name);

        let version_number = env::var("CARGO_PKG_VERSION").unwrap() + ".0";
        let version_commas = version_number.replace(".", ",");
        let file_description = output_name;
        let file_version = version_number.clone();
        let internal_name = filename.clone();
        let original_filename = filename;
        let product_name = output_name;
        let product_version = version_number;
        let vs_file_version = version_commas.clone();
        let vs_product_version = version_commas;

        let version_rc = format!(
            r#"#include <winresrc.h>
VS_VERSION_INFO VERSIONINFO
    FILEVERSION {vs_file_version}
    PRODUCTVERSION {vs_product_version}
    FILEFLAGSMASK 0x3fL
#ifdef _DEBUG
    FILEFLAGS 0x1L
#else
    FILEFLAGS 0x0L
#endif
    FILEOS 0x40004L
    FILETYPE 0x2L
    FILESUBTYPE 0x0L
BEGIN
    BLOCK "StringFileInfo"
    BEGIN
        BLOCK "040904b0"
        BEGIN
            VALUE "CompanyName", "{company_name}"
            VALUE "FileDescription", "{file_description}"
            VALUE "FileVersion", "{file_version}"
            VALUE "InternalName", "{internal_name}"
            VALUE "LegalCopyright", "{legal_copyright}"
            VALUE "OriginalFilename", "{original_filename}"
            VALUE "ProductName", "{product_name}"
            VALUE "ProductVersion", "{product_version}"
        END
    END
    BLOCK "VarFileInfo"
    BEGIN
        VALUE "Translation", 0x409, 1200
    END
END
"#,
            vs_file_version = vs_file_version,
            vs_product_version = vs_product_version,
            company_name = company_name,
            file_description = file_description,
            file_version = file_version,
            internal_name = internal_name,
            legal_copyright = legal_copyright,
            original_filename = original_filename,
            product_name = product_name,
            product_version = product_version
        );

        version_rc
    }

    pub fn main_stub() {
        let out_dir = env::var("OUT_DIR").unwrap();
        let version_rc_file = format!("{}/version.rc", out_dir);
        let version_rc_data = generate_version_rc();
        let mut file = File::create(&version_rc_file).expect("cannot create version.rc file");
        file.write_all(version_rc_data.as_bytes()).unwrap();
        embed_resource::compile(&version_rc_file);
    }
}

#[cfg(not(target_os = "windows"))]
mod other {
    pub fn main_stub() {}
}
