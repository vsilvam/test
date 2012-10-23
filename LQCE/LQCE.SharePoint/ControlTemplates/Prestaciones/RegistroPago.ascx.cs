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
                        int? IdFactura = int.Parse(Request.QueryString["Id"].ToString());
                        if (IdFactura.HasValue)
                        {
                            pnRegistroPagos.Visible = true;
                            getDatos(IdFactura.Value);

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

        private void getDatos(int factura)
        {
            try 
            {                
                var facturacion = new TrxFACTURACION();
                grdPrestacionesPendientes.DataSource = facturacion.FacturaForPagos(factura);
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
                            IFormatProvider culture = new CultureInfo("es-CL", true);
                            DateTime? fechaPago = null;
                            int IdFactura = int.Parse(this.grdPrestacionesPendientes.DataKeys[fila.RowIndex]["PRESTACION.ID"].ToString());
                            if (!string.IsNullOrEmpty(txtFechaPago.Text))
                                fechaPago = DateTime.Parse(txtFechaPago.Text, culture);
                            string medioPago = !string.IsNullOrEmpty(txtMedioPago.Text) ? txtMedioPago.Text: string.Empty;
                            string Observaciones = !string.IsNullOrEmpty(txtObservaciones.Text) ? txtObservaciones.Text: string.Empty;

                            //guardar pago
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
