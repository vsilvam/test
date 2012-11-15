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
    public partial class ActualizarClientes : UserControl
    {       
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

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trxCliente = new TrxCLIENTE();
                    var objCliente = trxCliente.GetByIdWithReferences(Id);
                    
                    txtRut.Text = objCliente.RUT;
                    txtNombre.Text = objCliente.NOMBRE;
                    //txtRazonSocial.Text = ;
                    txtDireccion.Text = objCliente.DIRECCION;
                    //txtDireccionEntrega.Text = ;
                    //txtCiudad.Text = ;
                    txtFono.Text = objCliente.FONO;
                    txtGiro.Text = objCliente.GIRO;
                    txtDescuento.Text = objCliente.DESCUENTO.ToString();
                    ddlConvenio.SelectedIndex = int.Parse(objCliente.CONVENIO.ID.ToString());
                    //ddlRegion.SelectedIndex = int.Parse(objCliente.REGION.ID.ToString());
                    ddlComuna.SelectedIndex = int.Parse(objCliente.COMUNA.ID.ToString());
                    ddlTipoFacturacion.SelectedIndex = int.Parse(objCliente.TIPO_FACTURA.ID.ToString());
               
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

                if (Request.QueryString["Id"] == null)
                    throw new Exception("No se ha indicado identificador de la cuenta registrada");

                int Id = int.Parse(Request.QueryString["Id"]);
                var trxCliente = new TrxCLIENTE();
                
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
                int tipoPrestacion = (int)ENUM_TIPO_PRESTACION.Humanas;

                trxCliente.Update(Id, comuna, convenio, tipoPrestacion, tipoFactura, rut, nombre, descuento, direccion, fono, giro);
                Response.Redirect("MensajeExito.aspx?t=Actualizar Clientes&m=Se ha modificado la informacion del cliente " + nombre, false);
                
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
                //txtRut.Text = string.Empty;
                
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

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("(Todos)", ""));
            ddlRegion.DataSource = _trx.GetAll();
            ddlRegion.DataBind();
        }

        private void getTipoFactura()
        {
            TrxTIPO_FACTURA _trx = new TrxTIPO_FACTURA();
            ddlTipoFacturacion.Items.Clear();
            ddlTipoFacturacion.Items.Add(new ListItem("(Todos)", ""));
            ddlTipoFacturacion.DataSource = _trx.GetAll();
            ddlTipoFacturacion.DataBind();
        }

        protected void btnAgrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                int Id = int.Parse(Request.QueryString["Id"]);
                string sinonimo = !string.IsNullOrEmpty(txtSinonimo.Text) ? txtSinonimo.Text : string.Empty;

                var _trxClienteSinonimo = new TrxCLIENTE_SINONIMO();
                _trxClienteSinonimo.Add(Id, sinonimo);

                grdSinonimoCliente.DataSource = _trxClienteSinonimo.GetByFilterWithReferences(Id,"");
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
