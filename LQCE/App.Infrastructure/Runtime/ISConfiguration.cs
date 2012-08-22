using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace App.Infrastructure.Runtime
{
    public sealed class ISConfiguration : MarshalByRefObject
    {
        private const string CONFIGURATION_FILE = "Configuration.xml";
        private const string CONFIGURATION_CACHE = "CONFIGURATION_CACHE";
        private const string CONFIG_APP = "CONFIG_APP";

        public static string GetConfig(string key)
        {
            return GetConfigurationParameter("", key);
        }

        public static string GetDbConfig(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string GetConfigurationParameter(string xmlTag, string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
            //return GetConfigurationParameterOld(xmlTag, key);
        }

        public static string GetConfigurationParameterOld(string XmlTag, string Key)
        {
            string _strValue = string.Empty;
            var objXmlConfiguration = new XmlDocument();
            string strAssemblyPath = GetAssemblyPath();
            _strValue = string.Empty;

            // Si el archivo XML de configuración no está en Cache, lo carga
            if (HttpRuntime.Cache[CONFIGURATION_CACHE] == null)
            {
                string strFile = strAssemblyPath + CONFIGURATION_FILE;
                try
                {
                    objXmlConfiguration.Load(strFile);
                }
                catch (FileNotFoundException)
                {
                    //ISException.RegisterExcepcion(ex);
                    return string.Empty;
                }
                HttpRuntime.Cache.Insert(CONFIGURATION_CACHE, objXmlConfiguration.InnerXml, new CacheDependency(strFile),
                                         DateTime.MaxValue, TimeSpan.Zero);
            }
            else
            {
                objXmlConfiguration.LoadXml(HttpRuntime.Cache[CONFIGURATION_CACHE].ToString());
            }

            XmlNodeList objXmlNode = null;

            try
            {
                objXmlNode = objXmlConfiguration.SelectNodes("//Configuration/" + XmlTag);
                objXmlConfiguration = null;

                if (objXmlNode.Count > 0)
                {
                    XmlElement childno;
                    for (int i = 0; i < objXmlNode[0].ChildNodes.Count; i++)
                    {
                        if (objXmlNode[0].ChildNodes[i].NodeType == XmlNodeType.Element)
                        {
                            childno = (XmlElement) objXmlNode[0].ChildNodes[i];
                            if (childno.Name == "add" && string.Compare(Key, childno.Attributes["key"].Value, true) == 0)
                            {
                                _strValue = childno.Attributes["value"].Value;
                                return _strValue;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Entrada No Encontrada");
                }
            }
            catch (Exception ex)
            {
                _strValue = string.Empty;
                ISException.RegisterExcepcion(ex);
                return string.Empty;
            }

            return string.Empty;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool SetConfigurationParameter(string XmlTag, string Key, string Value)
        {
            var objXmlConfiguration = new XmlDocument();
            string strAssemblyPath = GetAssemblyPath();

            string strFile = strAssemblyPath + CONFIGURATION_FILE;
            try
            {
                objXmlConfiguration.Load(strFile);
            }
            catch (FileNotFoundException)
            {
                //ISException.RegisterExcepcion(ex);
                return false;
            }

            XmlNodeList objXmlNode;

            try
            {
                objXmlNode = objXmlConfiguration.SelectNodes("//Configuration/" + XmlTag);

                if (objXmlNode.Count > 0)
                {
                    XmlElement childno;
                    for (int i = 0; i < objXmlNode[0].ChildNodes.Count; i++)
                    {
                        if (objXmlNode[0].ChildNodes[i].NodeType == XmlNodeType.Element)
                        {
                            childno = (XmlElement) objXmlNode[0].ChildNodes[i];
                            if (childno.Name == "add" && string.Compare(Key, childno.Attributes["key"].Value, true) == 0)
                            {
                                childno.Attributes["value"].Value = Value;

                                objXmlConfiguration.Save(strFile);

                                // Si el archivo XML de configuración no está en Cache, lo descarga
                                if (HttpRuntime.Cache[CONFIGURATION_CACHE] != null)
                                {
                                    HttpRuntime.Cache[CONFIGURATION_CACHE] = null;
                                }
                                HttpRuntime.Cache.Insert(CONFIGURATION_CACHE, objXmlConfiguration.InnerXml,
                                                         new CacheDependency(strFile), DateTime.MaxValue, TimeSpan.Zero);

                                return true;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Entrada No Encontrada");
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
            objXmlConfiguration = null;

            return false;
        }

        public static string GetAssemblyPath()
        {
            string strAssemblyPath = string.Empty;
            try
            {
                if (Assembly.GetExecutingAssembly().GlobalAssemblyCache)
                {
                    return Environment.GetEnvironmentVariable(CONFIG_APP, EnvironmentVariableTarget.Machine);
                }

                strAssemblyPath = Assembly.GetExecutingAssembly().GetName().CodeBase;
                strAssemblyPath = Path.GetDirectoryName(strAssemblyPath);

                Directory.GetCurrentDirectory();
                if (!strAssemblyPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    strAssemblyPath += Path.DirectorySeparatorChar;
                }
                if (strAssemblyPath.StartsWith(@"file:\") || strAssemblyPath.StartsWith("file:/"))
                {
                    strAssemblyPath = strAssemblyPath.Remove(0, 6);
                }
                return strAssemblyPath;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Método de acceso a la configuración del Servidor de Aplicaciones en caso de configurar .Net Remoting
        /// </summary>
        /// <param name="XmlTag"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetRemoteConfigurationParameter(string XmlTag, string Key)
        {
            string _strValue = string.Empty;
            var objXmlConfiguration = new XmlDocument();
            string strAssemblyPath = GetAssemblyPath();
            _strValue = string.Empty;

            // Si el archivo XML de configuración no está en Cache, lo carga
            if (HttpRuntime.Cache[CONFIGURATION_CACHE] == null)
            {
                string strFile = strAssemblyPath + CONFIGURATION_FILE;
                try
                {
                    objXmlConfiguration.Load(strFile);
                }
                catch (FileNotFoundException)
                {
                    //ISException.RegisterExcepcion(ex);
                    return string.Empty;
                }
                HttpRuntime.Cache.Insert(CONFIGURATION_CACHE, objXmlConfiguration.InnerXml, new CacheDependency(strFile),
                                         DateTime.MaxValue, TimeSpan.Zero);
            }
            else
            {
                objXmlConfiguration.LoadXml(HttpRuntime.Cache[CONFIGURATION_CACHE].ToString());
            }

            XmlNodeList objXmlNode = null;

            try
            {
                objXmlNode = objXmlConfiguration.SelectNodes("//Configuration/" + XmlTag);
                objXmlConfiguration = null;

                if (objXmlNode.Count > 0)
                {
                    XmlElement childno;
                    for (int i = 0; i < objXmlNode[0].ChildNodes.Count; i++)
                    {
                        if (objXmlNode[0].ChildNodes[i].NodeType == XmlNodeType.Element)
                        {
                            childno = (XmlElement) objXmlNode[0].ChildNodes[i];
                            if (childno.Name == "add" && string.Compare(Key, childno.Attributes["key"].Value, true) == 0)
                            {
                                _strValue = childno.Attributes["value"].Value;
                                return _strValue;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Entrada No Encontrada");
                }
            }
            catch (Exception ex)
            {
                _strValue = string.Empty;
                ISException.RegisterExcepcion(ex);
                return string.Empty;
            }
            objXmlConfiguration = null;
            _strValue = string.Empty;

            return string.Empty;
        }
    }
}