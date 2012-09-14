using System;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using System.Web.UI.WebControls;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class BuscarFactura : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getEstadoPago();
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

        private void getEstadoPago()
        {
            TrxPAGO _trx = new TrxPAGO();
            ddlEstadoPago.Items.Clear();
            ddlEstadoPago.Items.Add(new ListItem("(Todos)", ""));
            ddlEstadoPago.DataSource = _trx.GetAll();
            ddlEstadoPago.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                pnFacturas.Visible = true;

                string rut = !string.IsNullOrEmpty(txtRutCliente.Text) ? txtRutCliente.Text : string.Empty;
                string nombre = !string.IsNullOrEmpty(txtNombreCliente.Text) ? txtNombreCliente.Text : string.Empty;
                DateTime fecha = Convert.ToDateTime(!string.IsNullOrEmpty(txtFechaEmision.Text) ? txtFechaEmision.Text : string.Empty);
                int? numero = int.Parse(!string.IsNullOrEmpty(txtNroFactura.Text) ? txtNroFactura.Text : null);
                int? estado = int.Parse(ddlEstadoPago.SelectedValue);

                TrxFACTURACION _trx = new TrxFACTURACION();
                grdFacturas.DataSource = _trx.GetByFilter();
                grdFacturas.DataBind();


            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void lnkVer_Click(object sender, EventArgs e)
        {
            LinkButton _link = sender as LinkButton;
            int? Id = int.Parse(_link.CommandArgument);
            if (Id.HasValue)
                Response.Redirect("DetalleFactura.aspx?Id=" + Id, false);
        }
    }
}
