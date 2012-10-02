using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class ActualizarClientes : UserControl
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

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trxCliente = new TrxCLIENTE();
                    var objCliente = trxCliente.GetById(Id);

                    txtRut.Text = objCliente.RUT;
                    txtNombre.Text = objCliente.NOMBRE;
                    txtDireccion.Text = objCliente.DIRECCION;
                    txtFono.Text = objCliente.FONO;
                    txtGiro.Text = objCliente.GIRO;
                    ddlConvenio.SelectedIndex = int.Parse(objCliente.CONVENIO.ToString());
                    ddlComuna.SelectedIndex = int.Parse(objCliente.COMUNA.ToString());
                    txtDescuento.Text = objCliente.DESCUENTO.ToString();
                    //txtContacto.Text = objCliente;
                    txtConvenioPrecios.Text = objCliente.CONVENIO.ToString();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {                   

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trxCliente = new TrxCLIENTE();
                    string rut = txtRut.Text;
                    string nombre =txtNombre.Text;
                    string direccion = txtDireccion.Text;
                    string fono = txtFono.Text;
                    string giro = txtGiro.Text;
                    int? convenio = int.Parse(ddlConvenio.SelectedValue);
                    int? comuna = int.Parse(ddlConvenio.SelectedValue);
                    int descuento = int.Parse(txtDescuento.Text);
                    string contacto = txtContacto.Text;
                    string convenioPrecios = txtConvenioPrecios.Text;
                    int tipoPrestacion = (int)ENUM_TIPO_PRESTACION.Humanas;
                    int tipoFactura = (int)ENUM_TIPO_FACTURA.Monari_con_IVA;
                    trxCliente.Update(Id, comuna.Value, convenio.Value, tipoPrestacion, tipoFactura, rut, nombre, descuento, direccion, fono, giro);
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
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    txtRut.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtDireccion.Text = string.Empty;
                    txtFono.Text = string.Empty;
                    txtGiro.Text = string.Empty;
                    ddlConvenio.ClearSelection();
                    ddlComuna.ClearSelection();
                    txtDescuento.Text = string.Empty;
                    //txtContacto.Text = string.Empty;
                    txtConvenioPrecios.Text = string.Empty;
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
    }
}
