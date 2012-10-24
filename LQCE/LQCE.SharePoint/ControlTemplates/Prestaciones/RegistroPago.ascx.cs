using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using System.Globalization;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroPago : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    if (Request.QueryString["Id"] != null)
                    {
                        //throw new Exception("No se ha indicado identificador de la factura");
                        int? IdDetalleFactura = int.Parse(Request.QueryString["Id"].ToString());
                        if (IdDetalleFactura.HasValue)
                        {
                            pnRegistroPagos.Visible = true;
                            getDatos(IdDetalleFactura.Value);

                        }
                    }
                    else
                    {
                        pnBuscarPagos.Visible = true;
                        getFacturas();
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

        private void getDatos(int IdDtetalleFactura)
        {
            try
            {
                var facturacion = new TrxFACTURACION();
                grdPrestacionesPendientes.DataSource = facturacion.FacturaForPagos(IdDtetalleFactura);
                grdPrestacionesPendientes.DataBind();
                pnRegistroPagos.Visible = true;
                pnBuscarPagos.Visible = false;
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
            try
            {                
                var factura = new TrxFACTURA();
                ddlFacturas.Items.Clear();
                ddlFacturas.Items.Add(new ListItem("(Todos)", ""));
                ddlFacturas.DataSource = factura.GetAllWithReferences();//GetByFilter(ACTIVO = true, estado = pagado);
                ddlFacturas.DataBind();

                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow fila in grdPrestacionesPendientes.Rows)
                {
                    if (fila.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSeleccionar = (CheckBox)fila.FindControl("chkSeleccionar");
                        if (chkSeleccionar.Checked)
                        {
                            int? IdCliente = int.Parse(Request.QueryString["cliente"].ToString());
                            if (!IdCliente.HasValue)
                                throw new Exception("No se ha señalado el Id del cliente");

                            Label lblValorPrestacion = (Label)fila.FindControl("lblValorPrestacion");
                            IFormatProvider culture = new CultureInfo("es-CL", true);
                            DateTime? fechaPago = null;
                            int IdDetalleFactura = int.Parse(this.grdPrestacionesPendientes.DataKeys[fila.RowIndex]["ID_FACTURA_DETALLE"].ToString());
                            if (!string.IsNullOrEmpty(txtFechaPago.Text))
                                fechaPago = DateTime.Parse(txtFechaPago.Text, culture);
                            string medioPago = !string.IsNullOrEmpty(txtMedioPago.Text) ? txtMedioPago.Text: string.Empty;
                            string Observaciones = !string.IsNullOrEmpty(txtObservaciones.Text) ? txtObservaciones.Text: string.Empty;

                            //guardar pago
                            TrxPAGO _pago = new TrxPAGO();
                            //int pago = _pago.Add(IdCliente.Value, Convert.ToInt32(fechaPago.ToString()), int.Parse(lblValorPrestacion.Text));
                            int pago = _pago.Add(IdCliente.Value, 10, int.Parse(lblValorPrestacion.Text));
                            if (pago > 0)
                            {
                                TrxPAGO_DETALLE _pagoDetalle = new TrxPAGO_DETALLE();
                                var detalle = _pagoDetalle.Add(IdDetalleFactura, pago, int.Parse(lblValorPrestacion.Text));

                                lblMensaje.Text = "El pago ha sido exitoso";
                            }
                        }
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

        protected void ddlFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                int Id = int.Parse(ddlFacturas.SelectedValue);
                //carga grilla con las prestaciones asociadas a la factura seleccionada
                getDatos(Id);


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
