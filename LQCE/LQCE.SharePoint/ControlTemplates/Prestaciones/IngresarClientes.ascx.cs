using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;
using LQCE.Modelo;
using System.Collections.Generic;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class IngresarClientes : UserControl
    {
        public static List<CLIENTE_SINONIMO> _listaClienteSinonimo = new List<CLIENTE_SINONIMO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getRegion();
                    getComuna();
                    getConvenio();
                    getTipoFactura();
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

        private void getTipoFactura()
        {
            TrxTIPO_FACTURA _trx = new TrxTIPO_FACTURA();
            ddlTipoFacturacion.Items.Clear();
            ddlTipoFacturacion.Items.Add(new ListItem("(Todos)", ""));
            ddlTipoFacturacion.DataSource = _trx.GetAll();
            ddlTipoFacturacion.DataBind();
        }

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("(Todos)", ""));
            ddlRegion.DataSource = _trx.GetAll();
            ddlRegion.DataBind();
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

        protected void btnIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (!string.IsNullOrEmpty(txtRut.Text))
                    if (!ValidaRut(txtRut.Text))
                        throw new Exception("Rut no es valido");

                string rut = !string.IsNullOrEmpty(txtRut.Text) ? txtRut.Text : string.Empty;
                string nombre = !string.IsNullOrEmpty(txtNombre.Text) ? txtNombre.Text : string.Empty;
                string razonSocial = !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : string.Empty;
                string direccion = !string.IsNullOrEmpty(txtDireccion.Text) ? txtDireccion.Text : string.Empty;
                string direccinEntrega = !string.IsNullOrEmpty(txtDireccionEntrega.Text) ? txtDireccionEntrega.Text : string.Empty;
                string fono = !string.IsNullOrEmpty(txtFono.Text) ? txtFono.Text : string.Empty;
                string giro = !string.IsNullOrEmpty(txtGiro.Text) ? txtGiro.Text : string.Empty;
                string ciudad = !string.IsNullOrEmpty(txtCiudad.Text) ? txtCiudad.Text : string.Empty;
                int convenio = int.Parse(ddlConvenio.SelectedValue);
                int region = int.Parse(ddlRegion.SelectedValue);
                int comuna = int.Parse(ddlComuna.SelectedValue);
                int descuento = int.Parse(txtDescuento.Text);                
                int tipoFactura = int.Parse(ddlTipoFacturacion.SelectedValue); //(int)ENUM_TIPO_FACTURA.Monari_con_IVA;
                int tipoPrestacio = (int)ENUM_TIPO_PRESTACION.Humanas;

                var cliente = new TrxCLIENTE();
                var ingreso = cliente.Add(comuna, convenio, tipoPrestacio, tipoFactura, rut, nombre, descuento, direccion, fono, giro);
                var ingreso_sinonimo = 0;

                /*guardar datos del cliente sinonimo desde la grilla*/
                foreach (GridViewRow grilla in grdSinonimoCliente.Rows)
                {
                    if (grilla.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtNombreSinonimo = (TextBox)grilla.FindControl("txtNombreSinonimo");
                        var trxClienteSinonimo = new TrxCLIENTE_SINONIMO();
                        ingreso_sinonimo = trxClienteSinonimo.Add(ingreso, txtNombreSinonimo.Text);
                    }
                }

                //se despliega mensaje de exito
                if (ingreso > 0 && ingreso_sinonimo > 0 )
                    Response.Redirect("MensajeExito.aspx?t=Ingreso Clientes&m=Se ha registrado un nuevo cliente", false);
                else if (ingreso == 0)
                    Response.Redirect("MensajeError.aspx?t=Ingreso Clientes&m=Ha ocurrido un error al ingresar el cliente", false);
                else if (ingreso_sinonimo == 0)
                    Response.Redirect("MensajeError.aspx?t=Ingreso Cliente_Sinonimo&m=Ha ocurrido un error al ingresar el Sinonimo del cliente", false);
                
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
                txtRut.Text = string.Empty;                
                txtNombre.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                txtDireccion.Text = string.Empty;
                txtDireccionEntrega.Text = string.Empty;
                txtCiudad.Text = string.Empty;                
                txtFono.Text = string.Empty;
                txtGiro.Text = string.Empty;
                txtDescuento.Text = string.Empty;
                ddlRegion.ClearSelection();
                ddlConvenio.ClearSelection();
                ddlComuna.ClearSelection();
                ddlTipoFacturacion.ClearSelection();
                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private bool ValidaRut(string rut)
        {
            try
            {
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                if (rut == "1-9")
                {
                    return false;
                }
                rut = rut.Trim();
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                string digit = rut.Substring(rut.Length - 1, 1);
                digit = digit.ToUpper();
                string digitComparer = "";
                rut = rut.Substring(0, rut.Length - 1);
                int wiMultiplicador = 9;
                int wiSumatoria = 0;
                int wiSubTotal = 0;
                int wiLargo = rut.Length;
                for (int i = wiLargo; i > 0; i--)
                {
                    wiSubTotal = Convert.ToInt32(rut.Substring(i - 1, 1));
                    wiSumatoria = wiSumatoria + (wiSubTotal * wiMultiplicador);
                    if (wiMultiplicador == 4)
                    {
                        wiMultiplicador = 10;
                    }
                    wiMultiplicador = wiMultiplicador - 1;
                }
                wiSumatoria = wiSumatoria % 11;
                if (wiSumatoria == 10)
                {
                    digitComparer = "K";
                }
                else
                {
                    digitComparer = wiSumatoria.ToString();
                }

                if (digit.Equals(digitComparer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        protected void btnAgrega_Click(object sender, EventArgs e)
        {
            try
            {
                var objClienteSinonimo = new CLIENTE_SINONIMO();
                objClienteSinonimo.NOMBRE = !string.IsNullOrEmpty(txtSinonimo.Text) ? txtSinonimo.Text : string.Empty;
                _listaClienteSinonimo.Add(objClienteSinonimo);

                grdSinonimoCliente.DataSource = _listaClienteSinonimo;
                grdSinonimoCliente.DataBind();

                txtSinonimo.Text = string.Empty;
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
