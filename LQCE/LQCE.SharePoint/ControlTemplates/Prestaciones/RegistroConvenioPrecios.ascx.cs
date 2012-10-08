using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroConvenioPrecios : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getConvenios();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void getConvenios()
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                var trx = new TrxCONVENIO();
                grdRegistroConvenioPrecio.DataSource = trx.GetAllWithReferences();
                grdRegistroConvenioPrecio.DataBind();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grilla in grdRegistroConvenioPrecio.Rows)
                {
                    CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                    if (ChkEditar.Checked)
                    {
                        var lblId = grilla.FindControl("lblId") as Label;
                        Response.Redirect("ModificarConvenioPrecio.aspx?Id=" + lblId.Text, false);
                    }
                }                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }
    }
}
