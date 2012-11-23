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
                    getTipoPrestacion();
                    getConvenio();
                    getTipoFactura();

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trxCliente = new TrxCLIENTE();
                    var objCliente = trxCliente.GetByIdWithFullReferences(Id);

                    txtRut.Text = objCliente.RUT;
                    txtFono.Text = objCliente.FONO;
                    txtNombre.Text = objCliente.NOMBRE;
                    txtDireccion.Text = objCliente.DIRECCION;
                    txtGiro.Text = objCliente.GIRO;
                    if (objCliente.DESCUENTO.HasValue)
                    {
                        txtDescuento.Text = objCliente.DESCUENTO.ToString();
                    }
                    if (objCliente.COMUNA != null)
                    {
                        if (objCliente.COMUNA.REGION != null)
                        {
                            ddlRegion.SelectedValue = objCliente.COMUNA.REGION.ID.ToString();
                            getComuna();
                        }
                        ddlComuna.SelectedValue = objCliente.COMUNA.ID.ToString();
                    }
                    if (objCliente.TIPO_PRESTACION != null)
                    {
                        ddlTipoPrestacion.SelectedValue = objCliente.TIPO_PRESTACION.ID.ToString();
                        getConvenio();
                    }
                    if (objCliente.CONVENIO != null)
                    {
                        ddlConvenio.SelectedValue = objCliente.CONVENIO.ID.ToString();
                    }
                    if (objCliente.TIPO_FACTURA != null)
                    {
                        ddlTipoFactura.SelectedValue = objCliente.TIPO_FACTURA.ID.ToString();
                    }
                    //txtRazonSocial.Text = ;
                    //txtDireccionEntrega.Text = ;
                    //txtCiudad.Text = ;

                    var _trxClienteSinonimo = new TrxCLIENTE_SINONIMO();
                    grdSinonimoCliente.DataSource = _trxClienteSinonimo.GetByFilterWithReferences(Id, "");
                    grdSinonimoCliente.DataBind();

                    txtSinonimo.Text = string.Empty;
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
                if (Page.IsValid)
                {
                    panelMensaje.CssClass = "OcultarMensaje";

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trxCliente = new TrxCLIENTE();

                    string rut = !string.IsNullOrEmpty(txtRut.Text) ? txtRut.Text : string.Empty;
                    string fono = !string.IsNullOrEmpty(txtFono.Text) ? txtFono.Text : string.Empty;
                    string nombre = !string.IsNullOrEmpty(txtNombre.Text) ? txtNombre.Text : string.Empty;
                    string direccion = !string.IsNullOrEmpty(txtDireccion.Text) ? txtDireccion.Text : string.Empty;
                    string giro = !string.IsNullOrEmpty(txtGiro.Text) ? txtGiro.Text : string.Empty;
                    int? descuento = null;
                    if (!string.IsNullOrEmpty(txtDescuento.Text))
                    {
                        descuento = int.Parse(txtDescuento.Text);
                    }
                    int comuna = int.Parse(ddlComuna.SelectedValue);
                    int tipoPrestacion = int.Parse(ddlTipoPrestacion.SelectedValue);
                    int convenio = int.Parse(ddlConvenio.SelectedValue);
                    int tipoFactura = int.Parse(ddlTipoFactura.SelectedValue); 
                    //string razonSocial = !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : string.Empty;
                    //string direccinEntrega = !string.IsNullOrEmpty(txtDireccionEntrega.Text) ? txtDireccionEntrega.Text : string.Empty;
                    //string ciudad = !string.IsNullOrEmpty(txtCiudad.Text) ? txtCiudad.Text : string.Empty;
                    
                    trxCliente.Update(Id, comuna, convenio, tipoPrestacion, tipoFactura, rut, nombre, descuento, direccion, fono, giro);
                    Response.Redirect("MensajeExito.aspx?t=Actualizar Clientes&m=Se ha modificado la informacion del cliente " + nombre, false);
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

        //protected void btnLimpiar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        panelMensaje.CssClass = "OcultarMensaje";
        //        //txtRut.Text = string.Empty;
                
        //        txtNombre.Text = string.Empty;
        //        txtRazonSocial.Text = string.Empty;
        //        txtDireccion.Text = string.Empty;
        //        txtDireccionEntrega.Text = string.Empty;
        //        txtCiudad.Text = string.Empty;
        //        txtFono.Text = string.Empty;
        //        txtGiro.Text = string.Empty;
        //        txtDescuento.Text = string.Empty;
        //        ddlRegion.ClearSelection();
        //        ddlConvenio.ClearSelection();
        //        ddlComuna.ClearSelection();
        //        ddlTipoFacturacion.ClearSelection();
                 
        //    }
        //    catch (Exception ex)
        //    {
        //        ISException.RegisterExcepcion(ex);
        //        panelMensaje.CssClass = "MostrarMensaje";
        //        lblMensaje.Text = ex.Message;
        //        return;
        //    }
        //}

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("(Seleccionar)", ""));
            ddlRegion.DataSource = _trx.GetAll();
            ddlRegion.DataBind();
        }

        private void getComuna()
        {
            TrxCOMUNA _trx = new TrxCOMUNA();
            ddlComuna.Items.Clear();
            ddlComuna.Items.Add(new ListItem("(Seleccionar)", ""));
            if (!string.IsNullOrEmpty(ddlRegion.SelectedValue))
            {
                ddlComuna.DataSource = _trx.GetByFilter(int.Parse(ddlRegion.SelectedValue), "");
            }
            else
            {
                ddlComuna.DataSource = _trx.GetAll();
            }
            ddlComuna.DataBind();
        }

        private void getTipoPrestacion()
        {
            TrxTIPO_PRESTACION _trx = new TrxTIPO_PRESTACION();
            ddlTipoPrestacion.Items.Clear();
            ddlTipoPrestacion.Items.Add(new ListItem("(Seleccionar)", ""));
            ddlTipoPrestacion.DataSource = _trx.GetAll();
            ddlTipoPrestacion.DataBind();
        }

        private void getConvenio()
        {
            TrxCONVENIO _trx = new TrxCONVENIO();
            ddlConvenio.Items.Clear();
            ddlConvenio.Items.Add(new ListItem("(Seleccionar)", ""));
            if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
            {
                ddlConvenio.DataSource = _trx.GetByFilter(int.Parse(ddlTipoPrestacion.SelectedValue), "");
            }
            else
            {
                ddlConvenio.DataSource = _trx.GetAll();
            }
            ddlConvenio.DataBind();
        }

        
        private void getTipoFactura()
        {
            TrxTIPO_FACTURA _trx = new TrxTIPO_FACTURA();
            ddlTipoFactura.Items.Clear();
            ddlTipoFactura.Items.Add(new ListItem("(Seleccionar)", ""));
            ddlTipoFactura.DataSource = _trx.GetAll();
            ddlTipoFactura.DataBind();
        }

        protected void btnAgrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (!string.IsNullOrEmpty(txtSinonimo.Text))
                    {
                        if (Request.QueryString["Id"] == null)
                            throw new Exception("No se ha indicado identificador de la cuenta registrada");

                        int Id = int.Parse(Request.QueryString["Id"]);
                        string sinonimo = !string.IsNullOrEmpty(txtSinonimo.Text) ? txtSinonimo.Text : string.Empty;

                        var _trxClienteSinonimo = new TrxCLIENTE_SINONIMO();
                        _trxClienteSinonimo.Add(Id, sinonimo);

                        grdSinonimoCliente.DataSource = _trxClienteSinonimo.GetByFilterWithReferences(Id, "");
                        grdSinonimoCliente.DataBind();

                        txtSinonimo.Text = string.Empty;
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

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getComuna();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void ddlTipoPrestacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getConvenio();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void grdSinonimoCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int Id = int.Parse(Request.QueryString["Id"]);

                    int index = int.Parse(e.CommandArgument.ToString());
                    int ID_SINONIMO = int.Parse(this.grdSinonimoCliente.DataKeys[index]["ID"].ToString());
                    var _trxClienteSinonimo = new TrxCLIENTE_SINONIMO();
                    _trxClienteSinonimo.Delete(ID_SINONIMO);

                    grdSinonimoCliente.DataSource = _trxClienteSinonimo.GetByFilterWithReferences(Id, "");
                    grdSinonimoCliente.DataBind();

                    txtSinonimo.Text = string.Empty;
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
