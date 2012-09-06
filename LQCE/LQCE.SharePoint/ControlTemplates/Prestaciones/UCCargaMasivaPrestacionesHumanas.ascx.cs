using System;
using System.IO;
using System.Web.UI;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class UCCargaMasivaPrestacionesHumanas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    
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
                lblMensaje.Text = ex.Message;
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
                            TrxCARGA_PRESTACIONES_ENCABEZADO _TrxCARGA_PRESTACIONES_ENCABEZADO = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                            int IdCargaPrestacionesEncabezado = _TrxCARGA_PRESTACIONES_ENCABEZADO.UploadArchivoPrestaciones((int)ENUM_TIPO_PRESTACION.Humanas, fileExcel.FileName, fileExcel.FileBytes);
                            Response.Redirect("RegistroCargaArchivo.aspx", false);
                        }
                        else
                        {
                            lblMensaje.Text = "El archivo supera el tamaño máximo permitido: 4MB. ";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Formato de archivo no permitido. ";
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
    }
}
