using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Transaccion;

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
                pnFacturas.Visible = true;

                DateTime? desde = !string.IsNullOrEmpty(txtDesde.Text) ? DateTime.Parse(txtDesde.Text) : (DateTime?)null;
                DateTime? hasta = !string.IsNullOrEmpty(txtHasta.Text) ? DateTime.Parse(txtHasta.Text) : (DateTime?)null;
                int? cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? int.Parse(ddlClientes.SelectedValue) : (int?)null;

                var facturacion = new TrxFACTURACION();
                grdFacturas.DataSource = facturacion.GetClientesAFacturar(desde, hasta, cliente);
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

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in grdFacturas.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        var lblRut = row.FindControl("lblRut") as Label;
                        var lblNombre = row.FindControl("lblNombre") as Label;
                        var lblCantidad = row.FindControl("lblCantidad") as Label;
                        var lblTotal = row.FindControl("lblTotal") as Label;
                        var txtDescuento = row.FindControl("txtDescuento") as TextBox;

                        //se guardan las modificaciones cuando existen
                        // PENDIENTE: Recorrer grillas, recuperar descuentos y generar facturas
                        //se generan los reportes
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
