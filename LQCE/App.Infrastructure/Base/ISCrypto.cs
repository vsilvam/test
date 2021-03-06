using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public class ISCrypto
    {
        #region cryptoAction enum

        public enum cryptoAction
        {
            Encrypt,
            Decrypt,
        }

        #endregion

        #region cryptoProvider enum

        public enum cryptoProvider
        {
            DES,
            TripleDES,
            RC2,
            Rijndael,
        }

        #endregion

        private readonly string _IV;
        private readonly string _key;
        private readonly cryptoProvider algorithm;

        public ISCrypto()
        {
            algorithm = cryptoProvider.Rijndael;
            _key = ISConfiguration.GetConfigurationParameter("Security", "_appSecu_pkey");
            _IV = ISConfiguration.GetConfigurationParameter("Security", "_appSecu_pIV");
        }

        internal ICryptoTransform getServiceProvider(cryptoAction cAction, byte[] key, byte[] IV)
        {
            ICryptoTransform transform = null;
            switch (algorithm)
            {
                case cryptoProvider.DES:
                    var des = new DESCryptoServiceProvider();
                    switch (cAction)
                    {
                        case cryptoAction.Encrypt:
                            // si estamos cifrando, creamos el objeto cifrador. 
                            transform = des.CreateEncryptor(key, IV);
                            break;
                        case cryptoAction.Decrypt:
                            // si estamos descifrando, creamos el objeto descifrador.
                            transform = des.CreateDecryptor(key, IV);
                            break;
                    }
                    return transform;
                case cryptoProvider.TripleDES:
                    var des3 = new TripleDESCryptoServiceProvider();
                    switch (cAction)
                    {
                        case cryptoAction.Encrypt:
                            transform = des3.CreateEncryptor(key, IV);
                            break;
                        case cryptoAction.Decrypt:
                            transform = des3.CreateDecryptor(key, IV);
                            break;
                    }
                    return transform;
                case cryptoProvider.RC2:
                    var rc2 = new RC2CryptoServiceProvider();
                    switch (cAction)
                    {
                        case cryptoAction.Encrypt:
                            transform = rc2.CreateEncryptor(key, IV);
                            break;
                        case cryptoAction.Decrypt:
                            transform = rc2.CreateDecryptor(key, IV);
                            break;
                    }
                    return transform;
                case cryptoProvider.Rijndael:
                    var rijndael = new RijndaelManaged();
                    switch (cAction)
                    {
                        case cryptoAction.Encrypt:
                            transform = rijndael.CreateEncryptor(key, IV);
                            break;
                        case cryptoAction.Decrypt:
                            transform = rijndael.CreateDecryptor(key, IV);
                            break;
                    }
                    return transform;
                default:
                    throw new CryptographicException("Error al inicializar al proveedor de cifrado");
            }
        }

        private byte[] makeKeyByteArray()
        {
            string adaptedKey = _key;
            switch (algorithm)
            {
                case cryptoProvider.DES:
                case cryptoProvider.RC2:

                    if ((_key.Length < 8))
                    {
                        adaptedKey = _key.PadRight(8);
                    }
                    else if ((_key.Length > 8))
                    {
                        adaptedKey = _key.Substring(0, 8);
                    }
                    break;
                case cryptoProvider.TripleDES:
                case cryptoProvider.Rijndael:
                    if ((_key.Length < 16))
                    {
                        adaptedKey = _key.PadRight(16);
                    }
                    else if ((_key.Length > 16))
                    {
                        adaptedKey = _key.Substring(16);
                    }
                    break;
            }

            return Encoding.UTF8.GetBytes(adaptedKey);
        }

        private byte[] makeIVByteArray()
        {
            string adaptedIV = _IV;
            switch (algorithm)
            {
                case cryptoProvider.DES:
                case cryptoProvider.RC2:
                case cryptoProvider.TripleDES:

                    if ((_IV.Length < 8))
                    {
                        adaptedIV = _IV.PadRight(8);
                    }
                    else if ((_IV.Length > 8))
                    {
                        adaptedIV = _IV.Substring(8);
                    }
                    break;
                case cryptoProvider.Rijndael:
                    if ((_IV.Length < 16))
                    {
                        adaptedIV = _IV.PadRight(16);
                    }
                    else if ((_IV.Length > 16))
                    {
                        adaptedIV = _IV.Substring(16);
                    }
                    break;
            }

            return Encoding.UTF8.GetBytes(adaptedIV);
        }

        public bool crypt(string sourceString, ref string _targetString)
        {
            MemoryStream memStream = null;
            try
            {
                if (((_key != null)
                     && (_IV != null)))
                {
                    byte[] plainText = Encoding.UTF8.GetBytes(sourceString);
                    cryptBytes(plainText, ref memStream);

                    _targetString = Convert.ToBase64String(memStream.ToArray());
                    return true;
                }
                else
                {
                    _targetString = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _targetString = "";
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        public bool crypt(byte[] sourceBytes, ref byte[] _targetBytes)
        {
            // creamos el flujo tomando la memoria como respaldo
            MemoryStream memStream = null;
            try
            {
                if (((_key != null)
                     && (_IV != null)))
                {
                    cryptBytes(sourceBytes, ref memStream);

                    _targetBytes = memStream.ToArray();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        private void cryptBytes(byte[] bytes, ref MemoryStream memStream)
        {
            memStream = new MemoryStream(bytes.Length);

            byte[] key = makeKeyByteArray();
            byte[] IV = makeIVByteArray();
            ICryptoTransform transform = getServiceProvider(cryptoAction.Encrypt, key, IV);
            var cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.Close();
        }

        public bool decrypt(string sourceString, ref string _targetString, ref string _returnErrorCode)
        {
            MemoryStream memStream = null;
            try
            {
                if (((_key != null)
                     && (_IV != null)))
                {
                    byte[] plainText = Convert.FromBase64String(sourceString);
                    decryptBytes(plainText, ref memStream);
                    _targetString = Encoding.UTF8.GetString(memStream.ToArray());
                    _returnErrorCode = "";
                    return true;
                }
                else
                {
                    _targetString = "";
                    _returnErrorCode = "crytoErrorInitializingVector";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _targetString = "";
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        public bool decrypt(byte[] sourceBytes, ref byte[] _targetBytes)
        {
            MemoryStream memStream = null;
            try
            {
                if (((_key != null)
                     && (_IV != null)))
                {
                    decryptBytes(sourceBytes, ref memStream);
                    _targetBytes = memStream.ToArray();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        private void decryptBytes(byte[] bytes, ref MemoryStream memStream)
        {
            memStream = new MemoryStream((bytes.Length + 2));
            byte[] key = makeKeyByteArray();
            byte[] IV = makeIVByteArray();
            ICryptoTransform transform = getServiceProvider(cryptoAction.Decrypt, key, IV);
            var cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.Close();
        }

        public bool fileOperation(string inFileName, string outFileName, cryptoAction action,
                                  ref string _returnErrorCode)
        {
            if (!File.Exists(inFileName))
            {
                return false;
            }
            try
            {
                if (((_key != null)
                     && (_IV != null)))
                {
                    var fsIn = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                    var fsOut = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    fsOut.SetLength(0);
                    byte[] key = makeKeyByteArray();
                    byte[] IV = makeIVByteArray();
                    byte[] byteBuffer = null;
                    long fileLength = fsIn.Length;
                    long bytesProcesed = 0;
                    int bytesInBlock = 0;

                    ICryptoTransform transform = getServiceProvider(action, key, IV);
                    CryptoStream cryptoStream = null;
                    switch (action)
                    {
                        case cryptoAction.Encrypt:
                            cryptoStream = new CryptoStream(fsOut, transform, CryptoStreamMode.Write);
                            break;
                        case cryptoAction.Decrypt:
                            cryptoStream = new CryptoStream(fsOut, transform, CryptoStreamMode.Write);
                            break;
                    }
                    while ((bytesProcesed < fileLength))
                    {
                        bytesInBlock = fsIn.Read(byteBuffer, 0, 4096);
                        cryptoStream.Write(byteBuffer, 0, bytesInBlock);
                        bytesProcesed = (bytesProcesed + long.Parse(bytesInBlock.ToString()));
                    }
                    if (!(cryptoStream == null))
                    {
                        cryptoStream.Close();
                    }
                    fsIn.Close();
                    fsOut.Close();
                    _returnErrorCode = "";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }
    }
}