using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public class ISExcel
    {
        /// <summary>
        /// Lee un archivo excel desdel el directorio especificado.
        /// </summary>
        /// <param name="fileName">Nombre completo del archivo.</param>
        /// <param name="headers">Indica si el Excel contiene las cabeceras.</param>
        /// <returns></returns>
        public static DataTable ReadExcelFile(string fileName, bool headers)
        {
            //SE VALIDA NOMBRE DEL PATH + ARCHIVO
            if (string.IsNullOrEmpty(fileName))
                return null;

            //SE OBTIENE EL NOMBRE DEL ARCHIVO
            var paths = fileName.Split('\\');
            if (paths.Length < 1)
                return null;

            var archivo = paths[paths.Length - 1];

            //SE VALIDA QUE EXISTA EL ARCHIVO
            if (!ISFile.ExistisFile(fileName.Replace(archivo, string.Empty), archivo))
                return null;

            //SE CREA LA CADENA SEGUN LA VERSION DE EXCEL
            string connectionString;
            if (archivo.IndexOf("xlsx") > 0)
                //READ A 2007 FILE  
                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" +
                    "Extended Properties=\"Excel 12.0 Xml;HDR={1}; IMEX=1\"", fileName, headers ? "YES" : "NO");
            else if (archivo.IndexOf("xls") > 0)
                //READ A 97-2003 FILE  
                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};" +
                     "Extended Properties=\"Excel 8.0;HDR={1}; IMEX=1\"", fileName, headers ? "YES" : "NO");
            else
                return null;

            //OPEN THE EXCEL FILE USING OLEDB  
            using (var con = new OleDbConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                        return null;

                    DataSet excelDataSet;
                    using (var cmd = new OleDbDataAdapter("select * from [" + dt.Rows[0][2] + "]", con))
                    {
                        excelDataSet = new DataSet();
                        cmd.Fill(excelDataSet);
                    }
                    con.Close();
                    return excelDataSet.Tables[0];
                }
                catch (Exception ex)
                {
                    ISException.RegisterExcepcion(ex);
                    throw ex;
                }

            }

        }

        /// <summary>
        /// Exporta la información a Excel.
        /// </summary>
        /// <param name="response">HttpResponse actual.</param>
        /// <param name="data">Datos a exportar.</param>
        /// <param name="nombreArchivo">Nombre del archivo Excel</param>
        public static void ExportToExcelFile(HttpResponse response, DataView data, string nombreArchivo)
        {
            var dg = new DataGrid { DataSource = data };
            dg.DataBind();

            response.Clear();
            response.Buffer = true;

            //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            response.AddHeader("Content-Disposition", "filename=" + nombreArchivo);
            response.ContentType = "application/vnd.ms-excel"; 
            response.Charset = "UTF-8";
            response.ContentEncoding = System.Text.Encoding.Default;

            var stringWriter = new StringWriter();
            var htmlWriter = new HtmlTextWriter(stringWriter);
            dg.RenderControl(htmlWriter);

            response.Write(stringWriter.ToString());
            //resp.Flush();
            try
            {
                response.End();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                throw ex;
            }
        }
    }
}
