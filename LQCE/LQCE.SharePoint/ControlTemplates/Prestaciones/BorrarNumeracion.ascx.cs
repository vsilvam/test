using System;
using System.Web.UI;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class BorrarNumeracion : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    //getFacturas();
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

        private void getFacturas()
        {
            DateTime FechaDesde = ISConvert.ToDateTime(txtFechaEmisionDesde.Text);
            DateTime FechaHasta = ISConvert.ToDateTime(txtFechaEmisionHasta.Text);

            var facturacion = new TrxFACTURACION();
            grdFacturas.DataSource = facturacion.GetResumenFacturacion(FechaDesde, FechaHasta);
            grdFacturas.DataBind();
        }

        protected void grdFacturas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Numerar")
                {
                    pnNumerar.Visible = true;
                    txtDesdeN.Text = "";
                    txtHastaN.Text = "";
              
                    int index = int.Parse(e.CommandArgument.ToString());
                    hdnID_FACTURACION.Value = this.grdFacturas.DataKeys[index]["ID_FACTURACION"].ToString();
                    hdnID_TIPO_FACTURA.Value = this.grdFacturas.DataKeys[index]["ID_TIPO_FACTURA"].ToString();
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

        protected void btnBorrarNumeracion_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    int IdFacturacion = int.Parse(hdnID_FACTURACION.Value);
                    int IdTipoFactura = int.Parse(hdnID_TIPO_FACTURA.Value);
                     int  desde = int.Parse(txtDesdeN.Text);
                     int   hasta = int.Parse(txtHastaN.Text);

                    //llama la trx que genera la numeracion
                    TrxFACTURACION trx = new TrxFACTURACION();
                    trx.BorrarNumeracionFacturas(IdFacturacion, IdTipoFactura, desde, hasta);
                    Response.Redirect("MensajeExito.aspx?t=Numerar Facturas&m=Se han borrado las numeraciones señaladas", false);
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Page.IsValid)
                {
                    getFacturas();
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
