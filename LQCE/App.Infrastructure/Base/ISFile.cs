using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public class ISFile : MarshalByRefObject
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool ReadFile(string strPath, string strFileName, out string strText)
        {
            strText = string.Empty;

            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if ((targetFolder.Exists == false))
            {
                return false;
            }
            strFileName = strPath + strFileName;

            var objStreamReader = new StreamReader(strFileName, Encoding.GetEncoding(1252));

            strText = objStreamReader.ReadToEnd();

            objStreamReader.Close();
            objStreamReader = null;

            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool DeleteFile(string strPath, string strFileName)
        {
            if (ExistisFile(strPath, strFileName))
            {
                File.Delete(strFileName);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool ExistisFile(string strPath, string strFileName)
        {
            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if (targetFolder.Exists == false)
            {
                return false;
            }
            strFileName = strPath + strFileName;

            var fileInfo = new FileInfo(strFileName);
            if (fileInfo.Exists)
            {
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool WriteFile(string strPath, string strFileName, bool bolOverWrite, string strText)
        {
            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if ((targetFolder.Exists == false))
            {
                Directory.CreateDirectory(strPath);
            }
            strFileName = strPath + strFileName;

            try
            {
                var objStreamWriter = new StreamWriter(strFileName, !bolOverWrite, Encoding.GetEncoding(1252));
                objStreamWriter.WriteLine(strText);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objStreamWriter = null;

                return true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        /// <summary>
        /// Método de escritura de un archivo en Servidor de Aplicaciones en caso de configurar .Net Remoting
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strFileName"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool ReadRemoteFile(string strPath, string strFileName, out string strText)
        {
            strText = string.Empty;

            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if ((targetFolder.Exists == false))
            {
                return false;
            }
            strFileName = strPath + strFileName;

            var objStreamReader = new StreamReader(strFileName, Encoding.GetEncoding(1252));

            strText = objStreamReader.ReadToEnd();

            objStreamReader.Close();
            objStreamReader = null;

            return true;
        }

        /// <summary>
        /// Método de lectura de un archivo en Servidor de Aplicaciones en caso de configurar .Net Remoting
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool DeleteRemoteFile(string strPath, string strFileName)
        {
            if (ExistisRemoteFile(strPath, strFileName))
            {
                File.Delete(strFileName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método de eliminación de un archivo en Servidor de Aplicaciones en caso de configurar .Net Remoting
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool ExistisRemoteFile(string strPath, string strFileName)
        {
            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if (targetFolder.Exists == false)
            {
                return false;
            }
            strFileName = strPath + strFileName;

            var fileInfo = new FileInfo(strFileName);
            if (fileInfo.Exists)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método de validar la existencia de un archivo en Servidor de Aplicaciones en caso de configurar .Net Remoting
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strFileName"></param>
        /// <param name="bolOverWrite"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool WriteRemoteFile(string strPath, string strFileName, bool bolOverWrite, string strText)
        {
            if (!strPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPath += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPath);
            if ((targetFolder.Exists == false))
            {
                Directory.CreateDirectory(strPath);
            }
            strFileName = strPath + strFileName;

            try
            {
                var objStreamWriter = new StreamWriter(strFileName, !bolOverWrite, Encoding.GetEncoding(1252));
                objStreamWriter.WriteLine(strText);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objStreamWriter = null;

                return true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        public static DirectoryEntity ReadDirectory(string strPathBase)
        {
            if (!strPathBase.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPathBase += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPathBase);
            if ((targetFolder.Exists == false))
            {
                return null;
            }

            var objDirectoryEntity = new DirectoryEntity();
            objDirectoryEntity.Name = targetFolder.Name;
            objDirectoryEntity.FullPath = strPathBase;

            return objDirectoryEntity;
        }

        public static FileEntity[] GetDirectoryFiles(string strPathBase)
        {
            if (!strPathBase.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPathBase += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPathBase);
            if ((targetFolder.Exists == false))
            {
                return new FileEntity[0];
            }

            var objArrayFileEntity = new FileEntity[targetFolder.GetFiles().Length];
            FileEntity objFileEntity;

            for (int i = 0; i < targetFolder.GetFiles().Length; i++)
            {
                objFileEntity = new FileEntity();
                objFileEntity.Name = targetFolder.GetFiles()[i].Name;
                objFileEntity.IsReadOnly = targetFolder.GetFiles()[i].IsReadOnly;
                objFileEntity.DirectoryName = strPathBase;
                objFileEntity.Extension = targetFolder.GetFiles()[i].Extension;

                objArrayFileEntity[i] = objFileEntity;
            }

            return objArrayFileEntity;
        }

        public static bool OpenFile(string fullFileName)
        {
            try
            {
                Process.Start(fullFileName);
                return true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        #region Nested type: DirectoryEntity

        public class DirectoryEntity
        {
            private string[] _fileSearchExtension;
            private FileEntity[] _files;
            private DirectoryEntity[] _subDirectories;

            public DirectoryEntity()
            {
                _subDirectories = null;
                _files = null;
            }

            public string Name { get; set; }

            public string FullPath { get; set; }

            public DirectoryEntity[] SubDirectories
            {
                get
                {
                    if (_subDirectories == null)
                    {
                        GetSubDirectories();
                    }
                    return _subDirectories;
                }
            }

            public FileEntity[] Files
            {
                get
                {
                    if (_files == null)
                    {
                        GetDirectoryFiles();
                    }
                    return _files;
                }
            }

            public bool IsSubDirectoryContainer
            {
                get
                {
                    if (_subDirectories != null && _subDirectories.Length > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public bool IsFileContainer
            {
                get
                {
                    if (_files != null && _files.Length > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public string[] FileSearchExtension
            {
                get { return _fileSearchExtension; }
                set { _fileSearchExtension = value; }
            }

            protected void GetSubDirectories()
            {
                var targetFolder = new DirectoryInfo(FullPath);
                DirectoryInfo[] strArrayDirectories = targetFolder.GetDirectories();

                _subDirectories = new DirectoryEntity[strArrayDirectories.Length];
                DirectoryEntity objDirectoryEntity;

                for (int i = 0; i < strArrayDirectories.Length; i++)
                {
                    objDirectoryEntity = new DirectoryEntity();
                    objDirectoryEntity.Name = strArrayDirectories[i].Name;
                    objDirectoryEntity.FileSearchExtension = FileSearchExtension;
                    objDirectoryEntity.FullPath = FullPath + objDirectoryEntity.Name + Path.DirectorySeparatorChar;

                    _subDirectories[i] = objDirectoryEntity;
                }
            }

            protected void GetDirectoryFiles()
            {
                var targetFolder = new DirectoryInfo(FullPath);
                FileInfo[] strArrayFile = targetFolder.GetFiles();

                _files = new FileEntity[strArrayFile.Length];
                FileEntity objFileEntity;

                //SE CARGA SOLO LA EXTENSION DE BUSQUEDA
                if (_fileSearchExtension != null && _fileSearchExtension.Length > 0)
                {
                    int indice = 0;
                    for (int i = 0; i < strArrayFile.Length; i++)
                    {
                        if (IsValidExtension(strArrayFile[i].Extension))
                        {
                            objFileEntity = new FileEntity();
                            objFileEntity.Name = strArrayFile[i].Name;
                            objFileEntity.IsReadOnly = strArrayFile[i].IsReadOnly;
                            objFileEntity.DirectoryName = FullPath;
                            objFileEntity.Extension = strArrayFile[i].Extension;

                            _files[indice] = objFileEntity;
                            indice++;
                        }
                    }

                    Array.Resize(ref _files, indice);

                    return;
                }

                //SE CARGAN TODOS LOS TIPOS DE EXTENSIONES
                for (int i = 0; i < strArrayFile.Length; i++)
                {
                    objFileEntity = new FileEntity();
                    objFileEntity.Name = strArrayFile[i].Name;
                    objFileEntity.IsReadOnly = strArrayFile[i].IsReadOnly;
                    objFileEntity.DirectoryName = FullPath;
                    objFileEntity.Extension = strArrayFile[i].Extension;

                    _files[i] = objFileEntity;
                }
            }

            public void ReloadSubDirectories()
            {
                GetSubDirectories();
            }

            public void ReloadDirectoryFiles()
            {
                GetDirectoryFiles();
            }

            protected bool IsValidExtension(string extension)
            {
                extension = extension.Remove(0, 1);
                for (int i = 0; i < _fileSearchExtension.Length; i++)
                {
                    if (extension.Equals(_fileSearchExtension[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion

        #region Nested type: FileEntity

        public class FileEntity
        {
            private string _directoryName;
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string DirectoryName
            {
                get { return _directoryName; }
                set { _directoryName = value; }
            }

            public string FullPath
            {
                get { return _directoryName + _name; }
            }

            public bool IsReadOnly { get; set; }

            public string Extension { get; set; }
        }

        #endregion
    }
}