using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class NuevoConvenioPrecio : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getTipoPrestacion();
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

        private void getTipoPrestacion()
        {
            try
            {
                TrxTIPO_PRESTACION _trx = new TrxTIPO_PRESTACION();
                ddlTipoPrestacion.Items.Clear();
                ddlTipoPrestacion.Items.Add(new ListItem("(Todos)", ""));
                ddlTipoPrestacion.DataSource = _trx.GetAll();
                ddlTipoPrestacion.DataBind();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                var Trx = new TrxCONVENIO();
                int TipoPrestacion = int.Parse(ddlTipoPrestacion.SelectedValue);
                string Nombre = txtNombre.Text;
                Trx.Add(TipoPrestacion,Nombre);
                Response.Redirect("MensajeExito.aspx?t=Nuevo Convenio de Precios&m=Se ha registrado un nuevo convenio", false);

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
