using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace App.Infrastructure.Base
{
    public class ISHash
    {
        #region HashAlgorithmType enum

        public enum HashAlgorithmType
        {
            MD5,

            SHA1,

            SHA256,

            SHA384,

            SHA512,
        }

        #endregion

        private const string SALTHash = "622b2870-cd26-48cc-aa95-b374ca8c5dd0";

        public static string ComputeHash(string plainText)
        {
            return ComputeHash(plainText, HashAlgorithmType.SHA1, true);
        }

        public static string ComputeHash(string plainText, HashAlgorithmType hashAlgorithm)
        {
            return ComputeHash(plainText, hashAlgorithm, true);
        }

        public static string ComputeHash(string plainText, HashAlgorithmType hashAlgorithm, bool outputHexFormat)
        {
            HashAlgorithm algorithm;
            plainText = (SALTHash + plainText);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            switch (hashAlgorithm)
            {
                case HashAlgorithmType.SHA1:
                    algorithm = new SHA1Managed();
                    break;
                case HashAlgorithmType.SHA256:
                    algorithm = new SHA256Managed();
                    break;
                case HashAlgorithmType.SHA384:
                    algorithm = new SHA384Managed();
                    break;
                case HashAlgorithmType.SHA512:
                    algorithm = new SHA512Managed();
                    break;
                default:
                    algorithm = new MD5CryptoServiceProvider();
                    break;
            }
            byte[] inArray = algorithm.ComputeHash(bytes);
            if (outputHexFormat)
            {
                var builder = new StringBuilder();
                int num2 = (inArray.Length - 1);
                int i = 0;
                while ((i <= num2))
                {
                    if ((Conversion.Hex(inArray[i]).Length == 1))
                    {
                        builder.Append(("0" + Conversion.Hex(inArray[i])));
                    }
                    else
                    {
                        builder.Append(Conversion.Hex(inArray[i]));
                    }
                    if ((((i + 1)%2) == 0))
                    {
                        builder.Append(" ");
                    }
                    i++;
                }
                return builder.ToString().Trim().Replace(" ", "-");
            }
            return Convert.ToBase64String(inArray);
        }

        public static bool IsValidHash(string plainText, string hashValue)
        {
            return IsValidHash(plainText, HashAlgorithmType.SHA1, true, hashValue);
        }

        public static bool IsValidHash(string plainText, HashAlgorithmType hashAlgorithm, string hashValue)
        {
            return IsValidHash(plainText, hashAlgorithm, true, hashValue);
        }

        public static bool IsValidHash(string plainText, HashAlgorithmType hashAlgorithm, bool outputHexFormat,
                                       string hashValue)
        {
            return string.Equals(ComputeHash(plainText, hashAlgorithm, outputHexFormat), hashValue);
        }
    }
}