using System;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Modelo;
using System.Web.UI.WebControls;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class DetalleFactura : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Request.QueryString["Id"] == null)
                    throw new Exception("No se ha indicado identificador de la factura");

                int IdFactura = int.Parse(Request.QueryString["Id"].ToString());

                TrxFACTURACION trxFacturacion = new TrxFACTURACION();
                var detalle = trxFacturacion.GetDetalleFacturaById(IdFactura);

                lblNombreCliente.Text = detalle.NOMBRE_CLIENTE;
                lblRutCliente.Text = detalle.RUT_CLIENTE;
                lblFechaEmision.Text = detalle.FECHA_EMISION.ToString();
                lblNroFactura.Text = detalle.ID_FACTURA.ToString();
                lblEstadoPago.Text = detalle.PAGADA ? "PAGADA" : "PENDIENTE";
                
                //carga grilla detalle factura
                grdDetalleFactura.DataSource = detalle.LISTA_PRESTACIONES;
                grdDetalleFactura.DataBind();

                //carga grilla Pagos
                grdPagos.DataSource = detalle.LISTA_PAGOS;
                grdPagos.DataBind();

                //carga grilla Notas de Cobro
                grdNotasCobro.DataSource = detalle.LISTA_COBROS;
                grdNotasCobro.DataBind();

            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void imgPagar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try 
            {
                ImageButton image = sender as ImageButton;
                int? Id_pago = int.Parse(image.CommandArgument);
                if (Id_pago.HasValue)
                {
                    Response.Redirect("RegistroPago.aspx?Id=" + Id_pago, false);
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
