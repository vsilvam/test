using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public class ISXml
    {
        private const string TEXT_INICIO = "[";
        private const string TEXT_FIN = "]";

        private const string XSL_INICIO = @"<xsl:value-of select=""";
        private const string XSL_FIN = @"""></xsl:value-of>";

        private const string TEXT_INICIO_A = "&lt;[";
        private const string TEXT_FIN_A = "]&gt;";

        private const string XSL_INICIO_A = @"<xsl:attribute name=""href"">";
        private const string XSL_FIN_A = "</xsl:attribute>";

        public readonly List<InvalidXmlWord> InvalidXmlWords;

        public ISXml()
        {
            InvalidXmlWords = new List<InvalidXmlWord>();
            InvalidXmlWords.Add(new InvalidXmlWord("<BR>", "<br/>"));
            InvalidXmlWords.Add(new InvalidXmlWord("<br>", "<br/>"));
            InvalidXmlWords.Add(new InvalidXmlWord("&nbsp;", " "));
            InvalidXmlWords.Add(new InvalidXmlWord("<P></P>", "<br/>"));
            InvalidXmlWords.Add(new InvalidXmlWord("<P> </P>", "<br/>"));
            InvalidXmlWords.Add(new InvalidXmlWord("align=left", "align='left'"));
            InvalidXmlWords.Add(new InvalidXmlWord("align=center", "align='center'"));
            InvalidXmlWords.Add(new InvalidXmlWord("align=right", "align='right'"));
            InvalidXmlWords.Add(new InvalidXmlWord("align=justify", "align='justify'"));
            InvalidXmlWords.Add(new InvalidXmlWord("dir=ltr", "dir='ltr'"));

            InvalidXmlWords.Add(new InvalidXmlWord("&lt;a&gt;", "<a>"));
            InvalidXmlWords.Add(new InvalidXmlWord("&lt;/a&gt;", "</a>"));

            //InvalidXmlWords.Add(new InvalidXmlWord(@"><xsl:attribute name=""href"">",@" href="""));

            //InvalidXmlWords.Add(new InvalidXmlWord("</xsl:attribute>", @""">"));
        }

        public static string GetSafeXmlData(string strXmlData)
        {
            if (string.IsNullOrEmpty(strXmlData))
            {
                return String.Empty;
            }
            return ("<![CDATA[" + (strXmlData.Trim().Replace("]>", "") + "]]>"));
        }

        public string Transform(string inputXml, string inputXslt)
        {
            try
            {
                //SE CREA Y CARGA EL DOCUMENTO XML INPUT
                var objXmlIntputDocument = new XmlDocument();
                objXmlIntputDocument.LoadXml(inputXml);

                //SE CREA Y CARGA EL XLST INTPUT
                var objXslTemplate = new XmlDocument();
                objXslTemplate.LoadXml(inputXslt);

                //SE CREA EL TRANSFORM Y SE CARGA EL XSLT
                var objXslCompiledTransform = new XslCompiledTransform();
                objXslCompiledTransform.Load(objXslTemplate);

                //SE CREA LOS ESCRITORES DE SALIDA
                var sbOutput = new StringBuilder();
                using (XmlWriter objXmlWriter = XmlWriter.Create(sbOutput, objXslCompiledTransform.OutputSettings))
                {
                    //SE TRANSFORMA EL XML CON EL XSLT
                    objXslCompiledTransform.Transform(objXmlIntputDocument, objXmlWriter);

                    objXmlWriter.Flush();
                    objXmlWriter.Close();
                }

                return sbOutput.ToString();
            }
            catch (XmlException exXml)
            {
                ISException.RegisterExcepcion(exXml);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
            }

            return string.Empty;
        }

        public bool GetStringFromXslt(string inputValue, out string _outputValue, out string _strResultMessage)
        {
            _strResultMessage = string.Empty;
            _outputValue = string.Empty;

            var sbText = new StringBuilder(inputValue);

            //SE TRANSFORMAN LOS SALTOS DE HTML A TEXTO
            sbText.Replace("<br/>", "\r\n");

            try
            {
                //SE BUSCA CADA UNA LAS VARIABLES XSL
                while (sbText.ToString().IndexOf(XSL_INICIO) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(XSL_INICIO) + XSL_INICIO.Length;
                    int fin = sbText.ToString().IndexOf(XSL_FIN);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE XSL POR TEXTO
                    sbText.Replace(string.Format("{0}{1}{2}", XSL_INICIO, selectXsl, XSL_FIN),
                                   string.Format("{0}{1}{2}", TEXT_INICIO, selectXsl, TEXT_FIN));
                }

                var objXslDocument = new XmlDocument();
                objXslDocument.LoadXml(sbText.ToString());

                _outputValue = objXslDocument.ChildNodes[1].ChildNodes[1].InnerXml.Trim();
                //_outputValue = objXslDocument.InnerText.Trim();

                return true;
            }
            catch (ArgumentOutOfRangeException exOut)
            {
                ISException.RegisterExcepcion(exOut);
                _strResultMessage = exOut.Message;

                return false;
            }
            catch (XmlException exXml)
            {
                ISException.RegisterExcepcion(exXml);
                _strResultMessage = exXml.Message;

                return false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                _strResultMessage = ex.Message;

                return false;
            }
        }

        public bool GetXsltFromString(string inputValue, out string _outputValue, out string _strResultMessage)
        {
            _strResultMessage = string.Empty;
            _outputValue = string.Empty;

            var sbText = new StringBuilder(inputValue);

            //SE CAMBIAN LOS SALTOS DE TEXTO A HTML
            sbText.Replace("\r\n", "<br/>");

            try
            {
                //SE BUSCA CADA UNA DE LAS VARIABLES
                while (sbText.ToString().IndexOf(TEXT_INICIO) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(TEXT_INICIO) + TEXT_INICIO.Length;
                    int fin = sbText.ToString().IndexOf(TEXT_FIN);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE TEXTO POR XSL
                    sbText.Replace(string.Format("{0}{1}{2}", TEXT_INICIO, selectXsl, TEXT_FIN),
                                   string.Format("{0}{1}{2}", XSL_INICIO, selectXsl, XSL_FIN));
                }

                var sbXslt = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
                sbXslt.AppendLine(@"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">");
                sbXslt.AppendLine(@"<xsl:output method=""html"" encoding=""utf-16"" />");
                sbXslt.AppendLine(@"<xsl:template match=""MENSAJE"" xml:space=""preserve"">");

                sbXslt.AppendLine(sbText.ToString());

                sbXslt.AppendLine("</xsl:template>");
                sbXslt.AppendLine("</xsl:stylesheet>");

                _outputValue = sbXslt.ToString().Trim();

                return true;
            }
            catch (ArgumentOutOfRangeException exOut)
            {
                ISException.RegisterExcepcion(exOut);
                _strResultMessage = exOut.Message;

                return false;
            }
            catch (XmlException exXml)
            {
                ISException.RegisterExcepcion(exXml);
                _strResultMessage = exXml.Message;

                return false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                _strResultMessage = ex.Message;

                return false;
            }
        }

        public bool GetXsltFromHtmlBody(string inputValue, out string _outputValue, out string _strResultMessage)
        {
            _strResultMessage = string.Empty;
            _outputValue = string.Empty;

            var sbText = new StringBuilder(inputValue);

            //SE CAMBIAN LOS SALTOS DE TEXTO A HTML
            for (int i = 0; i < InvalidXmlWords.Count; i++)
            {
                sbText.Replace(InvalidXmlWords[i].InvalidText, InvalidXmlWords[i].ReplaceText);
            }

            try
            {
                //SE BUSCA CADA UNA DE LAS VARIABLES
                while (sbText.ToString().IndexOf("<IMG") > -1)
                {
                    int indice = sbText.ToString().IndexOf("<IMG");
                    sbText.Replace("<IMG", "<img", indice, 4);

                    for (int i = indice; i < sbText.Length; i++)
                    {
                        if (sbText[i].Equals('>'))
                        {
                            sbText.Replace(">", "/>", i, 1);
                            break;
                        }
                    }
                }

                //SE BUSCA CADA UNA DE LAS VARIABLES DE TIPO ATRIBUTO
                while (sbText.ToString().IndexOf(TEXT_INICIO_A) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(TEXT_INICIO_A) + TEXT_INICIO_A.Length;
                    int fin = sbText.ToString().IndexOf(TEXT_FIN_A);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE TEXTO POR XSL
                    sbText.Replace(string.Format("{0}{1}{2}", TEXT_INICIO_A, selectXsl, TEXT_FIN_A),
                                   string.Format("{0}{1}{2}", XSL_INICIO_A, selectXsl, XSL_FIN_A));
                }

                //SE BUSCA CADA UNA DE LAS VARIABLES
                while (sbText.ToString().IndexOf(TEXT_INICIO) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(TEXT_INICIO) + TEXT_INICIO.Length;
                    int fin = sbText.ToString().IndexOf(TEXT_FIN);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE TEXTO POR XSL
                    sbText.Replace(string.Format("{0}{1}{2}", TEXT_INICIO, selectXsl, TEXT_FIN),
                                   string.Format("{0}{1}{2}", XSL_INICIO, selectXsl, XSL_FIN));
                }

                var sbXslt = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
                sbXslt.AppendLine(@"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">");
                sbXslt.Append(@"<xsl:output method=""html"" indent=""yes"" encoding=""utf-16"" ");
                sbXslt.Append(@" doctype-system=""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""");
                sbXslt.Append(@" doctype-public=""-//W3C//DTD XHTML 1.0 Transitional//EN""");
                sbXslt.AppendLine("/>");

                sbXslt.AppendLine(@"<xsl:template match=""MENSAJE"" xml:space=""preserve"">");

                sbXslt.AppendLine(sbText.ToString());

                sbXslt.AppendLine("</xsl:template>");
                sbXslt.AppendLine("</xsl:stylesheet>");


                var objXslDocument = new XmlDocument();
                objXslDocument.LoadXml(sbXslt.ToString());

                _outputValue = objXslDocument.InnerXml;

                return true;
            }
            catch (ArgumentOutOfRangeException exOut)
            {
                ISException.RegisterExcepcion(exOut);
                _strResultMessage = exOut.Message;

                return false;
            }
            catch (XmlException exXml)
            {
                ISException.RegisterExcepcion(exXml);
                _strResultMessage = exXml.Message;

                return false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                _strResultMessage = ex.Message;

                return false;
            }
        }

        public bool GetHtmlBodyFromXslt(string inputValue, out string _outputValue, out string _strResultMessage)
        {
            _strResultMessage = string.Empty;
            _outputValue = string.Empty;

            StringBuilder sbText = new StringBuilder(inputValue);

            //SE TRANSFORMAN LOS SALTOS DE HTML A TEXTO
            //sbText.Replace("<br/>", "\r\n");
            //sbText.Replace("&nbsp;", " ");

            try
            {

                //SE BUSCA CADA UNA LAS VARIABLES XSL
                while (sbText.ToString().IndexOf(XSL_INICIO) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(XSL_INICIO) + XSL_INICIO.Length;
                    int fin = sbText.ToString().IndexOf(XSL_FIN);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE XSL POR TEXTO
                    sbText.Replace(string.Format("{0}{1}{2}", XSL_INICIO, selectXsl, XSL_FIN), string.Format("{0}{1}{2}", TEXT_INICIO, selectXsl, TEXT_FIN));
                }

                //SE BUSCA CADA UNA LAS VARIABLES XSL DE TIPO ATRIBUTO
                while (sbText.ToString().IndexOf(XSL_INICIO_A) > -1)
                {
                    int inicio = sbText.ToString().IndexOf(XSL_INICIO_A) + XSL_INICIO_A.Length;
                    int fin = sbText.ToString().IndexOf(XSL_FIN_A);

                    //SE OBTIENE EL NOMBRE DE LA VARIABLE
                    string selectXsl = sbText.ToString().Substring(inicio, fin - inicio);

                    //SE REMPLAZA LA VARIABLE XSL POR TEXTO
                    sbText.Replace(string.Format("{0}{1}{2}", XSL_INICIO_A, selectXsl, XSL_FIN_A), string.Format("{0}{1}{2}", TEXT_INICIO_A, selectXsl, TEXT_FIN_A));
                }

                XmlDocument objXslDocument = new XmlDocument();
                objXslDocument.LoadXml(sbText.ToString());

                _outputValue = objXslDocument.ChildNodes[1].ChildNodes[1].InnerXml.Trim();// Replace(@" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""", string.Empty).Trim();

                //sbText = new StringBuilder(_outputValue); 

                ////SE BUSCA CADA UNA DE LAS VARIABLES
                //while (sbText.ToString().IndexOf("<img") > -1)
                //{
                //    int indice = sbText.ToString().IndexOf("<img");
                //    sbText.Replace("<img", "<IMG", indice, 4);

                //    for (int i = indice; i < sbText.Length; i++)
                //    {
                //        if (sbText[i].Equals('>'))
                //        {
                //            sbText.Replace("/>", ">", i - 1, 2);
                //            break;
                //        }
                //    }
                //}
                //_outputValue = sbText.ToString(); 

                return true;
            }
            catch (ArgumentOutOfRangeException exOut)
            {
                ISException.RegisterExcepcion(exOut);
                _strResultMessage = exOut.Message;

                return false;
            }
            catch (XmlException exXml)
            {
                ISException.RegisterExcepcion(exXml);
                _strResultMessage = exXml.Message;

                return false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                _strResultMessage = ex.Message;

                return false;
            }
        }


        #region Validación al XML por medio de un XSD

        /// <summary>
        /// Mensaje con excepciones encontradas por la validación, si se retorna vacio; no se encontraron errores.
        /// </summary>
        private string mensaje = string.Empty;

        /// <summary>
        /// Verifica el formato y estructura de un XML a partir de un esquema (XSD)
        /// </summary>
        /// <param name="xmlDoc">Fichero XML a validar estructura y formato</param>
        /// <param name="schemaPath">Ruta en la cual se encuantra el esquema</param>
        /// <returns>retorna mensaje con los errores encontrados, si este esta vacio, no tiene errores</returns>
        public string CheckFormatXmlLogic(XmlDocument xmlDoc, string schemaPath)
        {
            try
            {
                var settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, XmlReader.Create(schemaPath));
                settings.ValidationEventHandler += settings_ValidationEventHandler;
                var nodeReader = new XmlNodeReader(xmlDoc);
                using (XmlReader lector = XmlReader.Create(nodeReader, settings))
                {
                    while (lector.Read())
                    {
                    }
                }
                return mensaje; //mensaje con errores encontrados en el XML
            }
            catch (Exception ex)
            {
                //capturar error
                return mensaje = ex.Message;
            }
        }

        /// <summary>
        /// Manejador de eventos del validador XSD
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            //capturar error e ir agregando al mensaje
            mensaje += e.Message + ", ";
        }

        #endregion

        #region Nested type: InvalidXmlWord

        public sealed class InvalidXmlWord
        {
            public InvalidXmlWord(string invalidText, string replaceText)
            {
                InvalidText = invalidText;
                ReplaceText = replaceText;
            }

            public string InvalidText { get; set; }

            public string ReplaceText { get; set; }
        }

        #endregion
    }
}