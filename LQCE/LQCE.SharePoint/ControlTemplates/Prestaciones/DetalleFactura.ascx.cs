using System;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Modelo;

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

                TrxFACTURA trxFactura = new TrxFACTURA();
                var detalle = trxFactura.GetById(IdFactura);

                lblNombreCliente.Text = detalle.NOMBRE_CLIENTE;
                lblRutCliente.Text = detalle.RUT_CLIENTE;
                //lblFechaEmision.Text = detalle.fec;
                lblNroFactura.Text = detalle.NUMERO_FACTURA.ToString();
                //lblEstadoPago.Text = detalle.esta;


                //carga grilla detalle factura


                //carga grilla Pagos


                //carga grilla Notas de Cobro

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
