using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public sealed class ISSerializer
    {
        public static bool XmlSerialize(object objectInput, out string _strXmlOutPut, out string _strResultMessage)
        {
            _strXmlOutPut = string.Empty;
            if (objectInput != null)
            {
                var objStringWriter = new StringWriter();
                var objXmlSerializer = new XmlSerializer(objectInput.GetType());
                try
                {
                    objXmlSerializer.Serialize(objStringWriter, objectInput);

                    _strXmlOutPut = objStringWriter.ToString();
                    _strResultMessage = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    _strResultMessage = ex.Message;
                    ISException.RegisterExcepcion(ex);
                    return false;
                }
            }
            _strResultMessage = ISException.NullReferenceMessage;
            return false;
        }

        public static bool XmlDeserialize(string strXmlInput, ref object _objectOutput, out string _strResultMessage)
        {
            if (_objectOutput != null && !string.IsNullOrEmpty(strXmlInput))
            {
                var objStringReader = new StringReader(strXmlInput);
                var objXmlSerializer = new XmlSerializer(_objectOutput.GetType());
                try
                {
                    _objectOutput = objXmlSerializer.Deserialize(objStringReader);
                    _strResultMessage = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    _strResultMessage = ex.Message;
                    ISException.RegisterExcepcion(ex);
                    return false;
                }
            }
            _strResultMessage = ISException.NullReferenceMessage;
            return false;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static string UTF8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(string pXmlString)
        {
            var encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            try
            {
                string xmlString = null;
                var memoryStream = new MemoryStream();
                var xs = new XmlSerializer(typeof(T));
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            var memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
            //var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return (T)xs.Deserialize(memoryStream);
        }
    }
}