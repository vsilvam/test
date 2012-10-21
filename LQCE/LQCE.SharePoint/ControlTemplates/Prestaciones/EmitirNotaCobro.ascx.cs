using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirNotaCobro : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getClientes();
                    getNotaCobro();
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

        private void getClientes()
        {
            TrxCLIENTE _TrxCLIENTE = new TrxCLIENTE();
            ddlClientes.Items.Clear();
            ddlClientes.Items.Add(new ListItem("(Todos)", ""));
            ddlClientes.DataSource = _TrxCLIENTE.GetAll();
            ddlClientes.DataBind();
        }


        private void getNotaCobro()
        {
            TrxTIPO_COBRO _tipo_cobro = new TrxTIPO_COBRO();
            ddlNotaCobro.Items.Clear();
            ddlNotaCobro.Items.Add(new ListItem("(Todos)", ""));
            ddlNotaCobro.DataSource = _tipo_cobro.GetAll();
            ddlNotaCobro.DataBind();
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    DateTime desde = ISConvert.ToDateTime(txtDesde.Text);
                    DateTime hasta = ISConvert.ToDateTime(txtHasta.Text);
                    int? IdCliente = null;
                    if(!string.IsNullOrEmpty(ddlClientes.SelectedValue))
                        IdCliente = int.Parse(ddlClientes.SelectedValue);

                    int tipo = int.Parse(ddlNotaCobro.SelectedValue);

                    TrxFACTURACION _TrxFACTURACION = new TrxFACTURACION();
                    _TrxFACTURACION.EmitirNotasCobros(desde, hasta, tipo, IdCliente);

                    Response.Redirect("MensajeExito.aspx?t=Emisión de Notas de Cobros&m=Se han emitidos los documentos correspondientes", false);

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
