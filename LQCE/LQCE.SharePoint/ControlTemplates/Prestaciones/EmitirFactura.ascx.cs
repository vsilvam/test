using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Modelo;
using System.Collections.Generic;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirFactura : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getClientes();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
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
            pnFacturas.Visible = true;

            string desde = !string.IsNullOrEmpty(txtDesde.Text) ? txtDesde.Text : string.Empty;
            string hasta = !string.IsNullOrEmpty(txtHasta.Text) ? txtDesde.Text : string.Empty;
            string cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? ddlClientes.SelectedValue : string.Empty;

            var facturacion = new TrxFACTURACION();
            grdFacturas.DataSource = facturacion.GetByFilterWithReferences();
            grdFacturas.DataBind();
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdFacturas.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow) continue;

                var lblRut = row.FindControl("lblRut") as Label;
                var lblNombre = row.FindControl("lblNombre") as Label;
                var lblCantidad = row.FindControl("lblCantidad") as Label;
                var lblTotal = row.FindControl("lblTotal") as Label;
                var txtDescuento = row.FindControl("txtDescuento") as TextBox;

                //se guardan las modificaciones cuando existen

                //se generan los reportes

            }
        }

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<FACTURACION> _facturacion = (List<FACTURACION>)e.Row.DataItem;
                if (_facturacion != null)
                {
                    foreach (var lis in _facturacion)
                    {
                        Label lblRut = (Label)e.Row.FindControl("lblRut");
                        Label lblNombre = (Label)e.Row.FindControl("lblNombre");
                        Label lblCantidad = (Label)e.Row.FindControl("lblCantidad");
                        Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                        TextBox txtDescuento = (TextBox)e.Row.FindControl("txtDescuento");

                        //lblRut.Text = lis.Rut;
                        //lblNombre.Text = lis;
                        //lblCantidad.Text = lis;
                        //lblTotal.Text = lis;
                        //txtDescuento.Text = lis;
                    }
                }
            }
        }
    }
}
