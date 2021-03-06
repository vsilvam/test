﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirFactura : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getClientes();
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
            TrxCLIENTE estado = new TrxCLIENTE();
            ddlClientes.Items.Clear();
            ddlClientes.Items.Add(new ListItem("(Todos)", ""));
            ddlClientes.DataSource = estado.GetAll();
            ddlClientes.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    IFormatProvider culture = new CultureInfo("es-CL", true);

                    DateTime desde = DateTime.Parse(txtDesde.Text, culture);
                    DateTime hasta = DateTime.Parse(txtHasta.Text, culture);
                    int? cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? int.Parse(ddlClientes.SelectedValue) : (int?)null;

                    var facturacion = new TrxFACTURACION();
                    hdnFechaDesde.Value = desde.ToString();
                    hdnFechaHasta.Value = hasta.ToString();
                    var lista = facturacion.GetClientesAFacturar(desde, hasta, cliente);
                    grdFacturas.DataSource = lista;
                    grdFacturas.DataBind();
                    pnFacturas.Visible = true;
                    btnEmitir.Visible = lista.Any();
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

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnFechaDesde.Value) || string.IsNullOrEmpty(hdnFechaHasta.Value))
                    throw new Exception("Debe realizar busqueda primero");

                List<DTO_EMISION_FACTURA> lista = new List<DTO_EMISION_FACTURA>();
                foreach (GridViewRow row in grdFacturas.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        var hdnId = row.FindControl("hdnId") as HiddenField;
                        var txtDescuento = row.FindControl("txtDescuento") as TextBox;

                        //se guardan las modificaciones cuando existen
                        DTO_EMISION_FACTURA dto = new DTO_EMISION_FACTURA();
                        dto.ID_CLIENTE = int.Parse(hdnId.Value);
                        dto.DESCUENTO = int.Parse(txtDescuento.Text);
                        lista.Add(dto);
                    }
                }

                if (!lista.Any())
                    throw new Exception("No hay prestaciones por facturar");

                DateTime FechaDesde = DateTime.Parse(hdnFechaDesde.Value);
                DateTime FechaHasta = DateTime.Parse(hdnFechaHasta.Value);

                TrxFACTURACION _TrxFACTURACION = new TrxFACTURACION();
                _TrxFACTURACION.EmitirFacturas(lista, FechaDesde, FechaHasta);

                Response.Redirect("MensajeExito.aspx?t=Emisión de Facturas&m=Se han emitidos los documentos correspondientes", false);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        List<FACTURACION> _facturacion = (List<FACTURACION>)e.Row.DataItem;
            //        if (_facturacion != null)
            //        {
            //            foreach (var lis in _facturacion)
            //            {
            //                Label lblRut = (Label)e.Row.FindControl("lblRut");
            //                Label lblNombre = (Label)e.Row.FindControl("lblNombre");
            //                Label lblCantidad = (Label)e.Row.FindControl("lblCantidad");
            //                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
            //                TextBox txtDescuento = (TextBox)e.Row.FindControl("txtDescuento");

            //                //lblRut.Text = lis.Rut;
            //                //lblNombre.Text = lis;
            //                //lblCantidad.Text = lis;
            //                //lblTotal.Text = lis;
            //                //txtDescuento.Text = lis;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ISException.RegisterExcepcion(ex);
            //    panelMensaje.CssClass = "MostrarMensaje";
            //    lblMensaje.Text = ex.Message;
            //    return;
            //}

         
        }
    }
}
