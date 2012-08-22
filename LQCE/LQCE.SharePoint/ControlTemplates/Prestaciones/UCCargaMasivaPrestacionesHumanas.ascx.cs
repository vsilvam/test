using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using System.IO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class UCCargaMasivaPrestacionesHumanas : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    MostrarPaso(1);
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnPaso1Template_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=PrestacionesHumanas.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                string nombreArchivo = MapPath("~/_layouts/Prestaciones/PrestacionesHumanas.xls");
                Response.WriteFile(nombreArchivo);
                this.Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = "Cambio de Prueba";// ex.Message;
                return;
            }
        }

        protected void btnPaso1Adjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (!fileExcel.HasFile) return;
                    btnPaso1Adjuntar.Visible = false;

                    //Leer archivo Excel
                    string fileExt = Path.GetExtension(fileExcel.FileName).ToLower();
                    if ((fileExt == ".xls") || (fileExt == ".xlsx"))
                    {
                        int fileSize = fileExcel.PostedFile.ContentLength;

                        // Limite: 4MB
                        if (fileSize < 4194304)
                        {
                            string filename = DateTime.UtcNow.ToString("yyyyMMddhhmmssffff") + "_" + fileExcel.FileName;
                            string archivo = Variables.DirectorioCargaMasiva + @"\" + filename;

                            if (!Directory.Exists(Variables.DirectorioCargaMasiva))
                            {
                                Directory.CreateDirectory(Variables.DirectorioCargaMasiva);
                            }

                            fileExcel.SaveAs(archivo);

                            List<DTOCargaTribunal> lista = new List<DTOCargaTribunal>();
                            if ((fileExt == ".txt") || (fileExt == ".csv"))
                            {
                                lista = ProcesarArchivoTexto(archivo);
                            }
                            else if ((fileExt == ".xls") || (fileExt == ".xlsx"))
                            {
                                varPathFile = archivo;

                                Guid TaskID = Guid.NewGuid();
                                this.hdnTaskId.Value = TaskID.ToString();

                                Thread newThread = new Thread(ProcesarArchivoExcel);
                                newThread.Start();

                                Timer1.Enabled = true;
                            }
                        }
                        else
                        {
                            lblErrorArchivo.Text = "El archivo supera el tamaño máximo permitido: 4MB. ";
                        }
                    }
                    else
                    {
                        lblErrorArchivo.Text = "Formato de archivo no permitido. ";
                    }

                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
            finally
            {
                btnPaso1Adjuntar.Visible = true;
            }
        }



        private void MostrarPaso(int NumeroPaso)
        {
            panelPaso1.Visible = (NumeroPaso == 1);
            panelPaso2.Visible = (NumeroPaso == 2);
            panelPaso3.Visible = (NumeroPaso == 3);
        }
    }
}
