using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarRegistros : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {   
                    GetEstado();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void GetEstado()
        {
            TrxCARGA_PRESTACIONES_ESTADO estado = new TrxCARGA_PRESTACIONES_ESTADO();
            ddlEstadoPrestacion.DataSource = estado.GetAll();
            ddlEstadoPrestacion.DataBind();
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdPrestaciones.Visible = true;

            TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            //grdPrestaciones.DataSource = carga.GetDTODetalleCargaPrestaciones();
            //grdPrestaciones.DataBind();
        }

        protected void grdPrestaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}
