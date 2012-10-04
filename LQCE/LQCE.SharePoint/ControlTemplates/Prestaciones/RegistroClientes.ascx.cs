using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroClientes : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getComuna();
                    getConvenio();
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

        private void getComuna()
        {
            TrxCOMUNA _trx = new TrxCOMUNA();
            ddlComuna.Items.Clear();
            ddlComuna.Items.Add(new ListItem("(Todos)", ""));
            ddlComuna.DataSource = _trx.GetAll();
            ddlComuna.DataBind();
        }

        private void getConvenio()
        {
            TrxCONVENIO _trx = new TrxCONVENIO();
            ddlConvenio.Items.Clear();
            ddlConvenio.Items.Add(new ListItem("(Todos)", ""));
            ddlConvenio.DataSource = _trx.GetAll();
            ddlConvenio.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    string Rut = txtRutCliente.Text;
                    string Nombre = txtNombreCliente.Text;
                    int? Comuna = int.Parse(ddlComuna.SelectedValue);
                    int? Convenio = int.Parse(ddlConvenio.SelectedValue);

                    pnClientes.Visible = true;
                    var trx = new TrxCLIENTE();
                    grdClientes.DataSource = trx.GetByFilterWithReferences(Comuna.Value,Convenio.Value,null,null,Rut,Nombre,null,"","","");
                    grdClientes.DataBind();
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

        protected void imgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    LinkButton _link = sender as LinkButton;
                    int? Id = int.Parse(_link.CommandArgument);
                    if (Id.HasValue)
                        Response.Redirect("ActualizarClientes.aspx?Id=" + Id, false);
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                txtRutCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
                ddlComuna.ClearSelection();
                ddlConvenio.ClearSelection();
                
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
