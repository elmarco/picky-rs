use crate::{oids, x509::DirectoryString};
use picky_asn1::wrapper::ObjectIdentifierAsn1;
use serde::{de, ser};
use std::fmt;

#[derive(Debug, PartialEq, Clone)]
pub(crate) enum AttributeTypeAndValueParameters {
    CommonName(DirectoryString),
    Surname(DirectoryString),
    SerialNumber(DirectoryString),
    CountryName(DirectoryString),
    LocalityName(DirectoryString),
    StateOrProvinceName(DirectoryString),
    StreetName(DirectoryString),
    OrganisationName(DirectoryString),
    OrganisationalUnitName(DirectoryString),
}

#[derive(Debug, PartialEq, Clone)]
pub(crate) struct AttributeTypeAndValue {
    pub ty: ObjectIdentifierAsn1,
    pub value: AttributeTypeAndValueParameters,
}

impl AttributeTypeAndValue {
    pub fn new_common_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_common_name().into(),
            value: AttributeTypeAndValueParameters::CommonName(name.into()),
        }
    }

    pub fn new_surname<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_surname().into(),
            value: AttributeTypeAndValueParameters::Surname(name.into()),
        }
    }

    pub fn new_serial_number<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_serial_number().into(),
            value: AttributeTypeAndValueParameters::SerialNumber(name.into()),
        }
    }

    pub fn new_country_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_country_name().into(),
            value: AttributeTypeAndValueParameters::CountryName(name.into()),
        }
    }

    pub fn new_locality_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_locality_name().into(),
            value: AttributeTypeAndValueParameters::LocalityName(name.into()),
        }
    }

    pub fn new_state_or_province_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_state_or_province_name().into(),
            value: AttributeTypeAndValueParameters::StateOrProvinceName(name.into()),
        }
    }

    pub fn new_street_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_street_name().into(),
            value: AttributeTypeAndValueParameters::StreetName(name.into()),
        }
    }

    pub fn new_organisation_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_organisation_name().into(),
            value: AttributeTypeAndValueParameters::OrganisationName(name.into()),
        }
    }

    pub fn new_organisational_unit_name<S: Into<DirectoryString>>(name: S) -> Self {
        Self {
            ty: oids::at_organisational_unit_name().into(),
            value: AttributeTypeAndValueParameters::OrganisationalUnitName(name.into()),
        }
    }
}

impl ser::Serialize for AttributeTypeAndValue {
    fn serialize<S>(
        &self,
        serializer: S,
    ) -> Result<<S as ser::Serializer>::Ok, <S as ser::Serializer>::Error>
    where
        S: ser::Serializer,
    {
        use ser::SerializeSeq;
        let mut seq = serializer.serialize_seq(Some(2))?;
        seq.serialize_element(&self.ty)?;
        match &self.value {
            AttributeTypeAndValueParameters::CommonName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::Surname(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::SerialNumber(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::CountryName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::LocalityName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::StateOrProvinceName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::StreetName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::OrganisationName(name) => {
                seq.serialize_element(name)?;
            }
            AttributeTypeAndValueParameters::OrganisationalUnitName(name) => {
                seq.serialize_element(name)?;
            }
        }
        seq.end()
    }
}

impl<'de> de::Deserialize<'de> for AttributeTypeAndValue {
    fn deserialize<D>(deserializer: D) -> Result<Self, <D as de::Deserializer<'de>>::Error>
    where
        D: de::Deserializer<'de>,
    {
        struct Visitor;

        impl<'de> de::Visitor<'de> for Visitor {
            type Value = AttributeTypeAndValue;

            fn expecting(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
                formatter.write_str("a valid DER-encoded AttributeTypeAndValue")
            }

            fn visit_seq<A>(self, mut seq: A) -> Result<Self::Value, A::Error>
            where
                A: de::SeqAccess<'de>,
            {
                let ty: ObjectIdentifierAsn1 = seq.next_element()?.unwrap(); // cannot panic with DER deserializer

                let value = match Into::<String>::into(&ty.0).as_str() {
                    oids::AT_COMMON_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::CommonName(seq.next_element()?.unwrap())
                    }
                    oids::AT_SURNAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::Surname(seq.next_element()?.unwrap())
                    }
                    oids::AT_SERIAL_NUMBER => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::SerialNumber(seq.next_element()?.unwrap())
                    }
                    oids::AT_COUNTRY_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::CountryName(seq.next_element()?.unwrap())
                    }
                    oids::AT_LOCALITY_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::LocalityName(seq.next_element()?.unwrap())
                    }
                    oids::AT_STATE_OR_PROVINCE_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::StateOrProvinceName(
                            seq.next_element()?.unwrap(),
                        )
                    }
                    oids::AT_STREET_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::StreetName(seq.next_element()?.unwrap())
                    }
                    oids::AT_ORGANISATION_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::OrganisationName(
                            seq.next_element()?.unwrap(),
                        )
                    }
                    oids::AT_ORGANISATIONAL_UNIT_NAME => {
                        // cannot panic with DER deserializer
                        AttributeTypeAndValueParameters::OrganisationalUnitName(
                            seq.next_element()?.unwrap(),
                        )
                    }
                    oids::EMAIL_ADDRESS => {
                        return Err(de::Error::invalid_value(
                            de::Unexpected::Other(
                                "[AttributeTypeAndValue] 1.2.840.113549.1.9.1 (e-mailAddress) \
                                 attribute is deprecated and won't be supported",
                            ),
                            &"a supported type",
                        ));
                    }
                    _ => {
                        return Err(de::Error::invalid_value(
                            de::Unexpected::Other(
                                "[AttributeTypeAndValue] unsupported type (unknown oid)",
                            ),
                            &"a supported type",
                        ));
                    }
                };

                Ok(AttributeTypeAndValue { ty, value })
            }
        }

        deserializer.deserialize_seq(Visitor)
    }
}