using System;
using App.Infrastructure.Runtime;
using Microsoft.SharePoint;

namespace LQCE.SharePoint
{
    public class Libreria
    {
        public Guid AddFile(string Site, string List, string Filename, string Extension, byte[] Content)
        {
            try
            {
                using (SPSite spsite = new SPSite(Site))
                {
                    using (SPWeb spweb = spsite.OpenWeb())
                    {
                        spweb.AllowUnsafeUpdates = true;
                        SPFolder spfolder = spweb.Folders[Site + "/" + List];
                        string nombreArchivo = Filename + Extension;
                        int contador = 0;
                        while (spfolder.Files[nombreArchivo].Exists)
                        {
                            contador++;
                            nombreArchivo = Filename + "_" + contador.ToString() + Extension;
                        }

                        SPFile spfile = spfolder.Files.Add(nombreArchivo, Content, true);
                        spfolder.Update();
                        spweb.Update();
                        return spfile.UniqueId;
                    }
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                throw ex;
            }
        }
    }
}
