using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace App.Infrastructure.Base
{
    public class ISSign
    {
        /// <summary>
        /// Obtiene el certificado digital desde el almacén de certificados instalados en el equipo cliente
        /// </summary>
        /// <param name="nameCertificate">Subject del certificado instalado en el equipo que se quiere recuperar</param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(string nameCertificate)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = store.Certificates;
            X509Certificate2 x509 = null;
            foreach (X509Certificate2 cert in certCollection)
            {
                if ((cert.Subject == nameCertificate))
                {
                    x509 = cert;
                    break;
                }
            }
            if ((x509 == null))
            {
                throw new CryptographicException("El certificado X.509 no fue encontrado.");
            }
            store.Close();

            return x509;
        }

        /// <summary>
        /// Obtiene el certificado digital desde un archivo en el disco local
        /// </summary>
        /// <param name="pathCertificate">Ruta especifica de la ubicación del certificado</param>
        /// <param name="password">Contraseña del certificado</param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(string pathCertificate, string password)
        {
            FileStream fs = File.Open(pathCertificate, FileMode.Open, FileAccess.Read);
            var bufferStr = new byte[0];

            fs.Close();
            var x509 = new X509Certificate2(bufferStr, password);
            if ((x509 == null))
            {
                throw new CryptographicException("El certificado X.509 no fue encontrado.");
            }
            return x509;
        }

        private static XmlDsigXPathTransform CreateXPathTransform(string xPathString)
        {
            var doc = new XmlDocument();
            doc.LoadXml("<XPath xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></XPath>");
            var xPathElem = (XmlElement) doc.SelectSingleNode("XPath");
            xPathElem.InnerText = xPathString;
            var xForm = new XmlDsigXPathTransform();
            xForm.LoadInnerXml(xPathElem.SelectNodes("."));
            return xForm;
        }

        /// <summary>
        /// Permite desencriptar con una llave privada un documento XML encriptado con una clave pública
        /// </summary>
        /// <param name="xmlIn"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static XmlDocument DecryptXml(XmlDocument xmlIn, RSA privateKey)
        {
            const string keyName = "Llave";
            if ((xmlIn == null))
            {
                throw new ArgumentNullException("xmlIn");
            }
            if ((privateKey == null))
            {
                throw new ArgumentNullException("privateKey");
            }

            var exml = new EncryptedXml(xmlIn);
            exml.AddKeyNameMapping(keyName, privateKey);
            exml.DecryptDocument();
            return xmlIn;
        }

        /// <summary>
        /// Firma un documento XML con un certificado digital, ademas incluye el certificado en el documento
        /// </summary>
        /// <param name="xmlIn"></param>
        /// <param name="x509Certificate"></param>
        /// <returns></returns>
        public static XmlDocument SignXml(XmlDocument xmlIn, X509Certificate2 x509Certificate)
        {
            //var s = Environment.UserName; 

            var xmlReturn = new XmlDocument();
            xmlReturn.LoadXml(xmlIn.OuterXml);

            var nsmgr = new XmlNamespaceManager(xmlReturn.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");

            xmlReturn.PreserveWhitespace = true;
            var reference = new Reference("");
            var signedXml =
                new SignedXml(
                    (XmlElement)
                    xmlReturn.SelectSingleNode("soapenv:Envelope", nsmgr).SelectSingleNode("soapenv:Body", nsmgr));
            var keyInfo = new KeyInfo();

            // ASIGNACION DE METODOS Y UBICACIUN DE ELEMENTOS A FIRMAR 
            XmlDsigXPathTransform xPathTransform = CreateXPathTransform("ancestor-or-self::soap:Body");
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            var canMethod = ((XmlDsigExcC14NTransform) (signedXml.SignedInfo.CanonicalizationMethodObject));

            reference.AddTransform(xPathTransform);
            reference.AddTransform(new XmlDsigC14NTransform());
            signedXml.AddReference(reference);
            keyInfo.AddClause(new KeyInfoX509Data(x509Certificate));
            signedXml.KeyInfo = keyInfo;
            signedXml.SigningKey = x509Certificate.PrivateKey;

            // FIRMA
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            // INSERTAR FIRMA EN CABECERA
            xmlReturn.SelectSingleNode("soapenv:Envelope", nsmgr).SelectSingleNode("soapenv:Header", nsmgr).AppendChild(
                xmlReturn.ImportNode(xmlDigitalSignature, true));

            return xmlReturn;
        }
    }
}