using System;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using System.Web.UI.WebControls;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion.DTO;
using System.Globalization;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class BuscarFactura : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getEstadoPago();
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

        private void getEstadoPago()
        {
            ddlEstadoPago.Items.Clear();
            ddlEstadoPago.Items.Add(new ListItem("(Todos)", ""));
            ddlEstadoPago.Items.Add(new ListItem("Pagada", "1"));
            ddlEstadoPago.Items.Add(new ListItem("Pendiente", "0"));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdFacturas.PageIndex = 1;
                Facturas();
                Paginador1.SetPage(1);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void Facturas()
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (!string.IsNullOrEmpty(txtRutCliente.Text))
                    if (!ValidaRut(txtRutCliente.Text))
                        throw new Exception("Rut no es valido");

                pnFacturas.Visible = true;

                IFormatProvider culture = new CultureInfo("es-CL", true);
                DTOFindFactura dto = new DTOFindFactura();
                dto.PageIndex = grdFacturas.PageIndex;
                dto.PageSize = grdFacturas.PageSize;

                if(!string.IsNullOrEmpty(txtRutCliente.Text))
                    dto.RUT_CLIENTE = txtRutCliente.Text;
                if(!string.IsNullOrEmpty(txtNombreCliente.Text))
                    dto.NOMBRE_CLIENTE = txtNombreCliente.Text;
                if(!string.IsNullOrEmpty(txtFechaEmision.Text))
                    dto.FECHA_FACTURACION = DateTime.Parse(txtFechaEmision.Text,culture);
                if(!string.IsNullOrEmpty(txtNroFactura.Text))
                    dto.NUMERO_FACTURA = int.Parse(txtNroFactura.Text);
                if (!string.IsNullOrEmpty(ddlEstadoPago.SelectedValue))
                    dto.ESTADO_FACTURA = ddlEstadoPago.SelectedValue == "1" ? true : false;

                TrxFACTURACION _trx = new TrxFACTURACION();
                int Total = _trx.GetResumenFacturasByFilterCount(null, null, dto.RUT_CLIENTE, dto.NOMBRE_CLIENTE, dto.FECHA_FACTURACION, dto.NUMERO_FACTURA, dto.ESTADO_FACTURA);
                grdFacturas.DataSource = _trx.GetResumenFacturasByFilter(null, null, dto.RUT_CLIENTE,dto.NOMBRE_CLIENTE,dto.FECHA_FACTURACION,dto.NUMERO_FACTURA,dto.ESTADO_FACTURA,dto.PageIndex, dto.PageSize);
                grdFacturas.DataBind();

                Paginador1.TotalPages = Total % grdFacturas.PageSize == 0 ? Total / grdFacturas.PageSize : Total / grdFacturas.PageSize + 1;
                Paginador1.Visible = (Total > 0);
                Paginador1.Inicializar(dto);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private bool ValidaRut(string rut)
        {
            try
            {
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                if (rut == "1-9")
                {
                    return false;
                }
                rut = rut.Trim();
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                string digit = rut.Substring(rut.Length - 1, 1);
                digit = digit.ToUpper();
                string digitComparer = "";
                rut = rut.Substring(0, rut.Length - 1);
                int wiMultiplicador = 9;
                int wiSumatoria = 0;
                int wiSubTotal = 0;
                int wiLargo = rut.Length;
                for (int i = wiLargo; i > 0; i--)
                {
                    wiSubTotal = Convert.ToInt32(rut.Substring(i - 1, 1));
                    wiSumatoria = wiSumatoria + (wiSubTotal * wiMultiplicador);
                    if (wiMultiplicador == 4)
                    {
                        wiMultiplicador = 10;
                    }
                    wiMultiplicador = wiMultiplicador - 1;
                }
                wiSumatoria = wiSumatoria % 11;
                if (wiSumatoria == 10)
                {
                    digitComparer = "K";
                }
                else
                {
                    digitComparer = wiSumatoria.ToString();
                }

                if (digit.Equals(digitComparer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        protected void grdFacturas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Detalles")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    string val = this.grdFacturas.DataKeys[index]["ID_FACTURA"].ToString();
                    Response.Redirect("DetalleFactura.aspx?Id=" + val, false);
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

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            try
            {
                grdFacturas.PageSize = (e.CurrentPageSize == 0 ? 20 : e.CurrentPageSize);
                grdFacturas.PageIndex = e.CurrentPageNumber;
                Facturas();
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
