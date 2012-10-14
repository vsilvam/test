using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LQCE.Transaccion;
using App.Infrastructure.Runtime;
using LQCE.Transaccion.DTO;
using System.Globalization;
using LQCE.SharePoint.ControlTemplates.App_Code;
using System.Collections.Generic;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class AnularFactura : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
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

        private void getFacturas()
        {
            var facturacion = new TrxFACTURACION();
            gridFacturacion.DataSource = facturacion.GetResumenFacturacionPorNumerar();
            gridFacturacion.DataBind();
        }

        protected void gridFacturacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DTO_RESUMEN_FACTURACION dto = (DTO_RESUMEN_FACTURACION)e.Row.DataItem;
                    HiddenField hdnIdFacturacion = (HiddenField)e.Row.FindControl("hdnIdFacturacion");
                    HiddenField hdnIdTipoFactura = (HiddenField)e.Row.FindControl("hdnIdTipoFactura");
                    hdnIdFacturacion.Value = dto.ID_FACTURACION.ToString();
                    hdnIdTipoFactura.Value = dto.ID_TIPO_FACTURA.ToString();
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

        protected void gridFacturacion_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Seleccionar")
                {
                    grdFacturas.PageIndex = 1;
                    grdFacturas.PageSize = 10;

                    int index = int.Parse(e.CommandArgument.ToString());
                    panelAnular.Visible = true;
                    hdnID_FACTURACION.Value = this.gridFacturacion.DataKeys[index]["ID_FACTURACION"].ToString();
                    hdnID_TIPO_FACTURA.Value = this.gridFacturacion.DataKeys[index]["ID_TIPO_FACTURA"].ToString();

                    IFormatProvider culture = new CultureInfo("es-CL", true);
                    DTOFindFactura dto = new DTOFindFactura();
                    dto.PageIndex = grdFacturas.PageIndex;
                    dto.PageSize = grdFacturas.PageSize;
                    dto.ID_FACTURACION = int.Parse(hdnID_FACTURACION.Value);
                    dto.ID_TIPO_FACTURA = int.Parse(hdnID_TIPO_FACTURA.Value);

                    TrxFACTURACION _trx = new TrxFACTURACION();
                    int Total = _trx.GetResumenFacturasByFilterCount(dto.ID_FACTURACION, dto.ID_TIPO_FACTURA, "", "", null, null, null);
                    grdFacturas.DataSource = _trx.GetResumenFacturasByFilter(dto.ID_FACTURACION, dto.ID_TIPO_FACTURA, "", "", null, null, null, dto.PageIndex, dto.PageSize);
                    grdFacturas.DataBind();

                    Paginador1.TotalPages = Total % grdFacturas.PageSize == 0 ? Total / grdFacturas.PageSize : Total / grdFacturas.PageSize + 1;
                    Paginador1.Visible = (Total > 0);
                    Paginador1.Inicializar(dto);
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

        private void Facturas()
        {
            try
            {
                IFormatProvider culture = new CultureInfo("es-CL", true);
                DTOFindFactura dto = new DTOFindFactura();
                dto.PageIndex = grdFacturas.PageIndex;
                dto.PageSize = grdFacturas.PageSize;
                dto.ID_FACTURACION = int.Parse(hdnID_FACTURACION.Value);
                dto.ID_TIPO_FACTURA = int.Parse(hdnID_TIPO_FACTURA.Value);

                TrxFACTURACION _trx = new TrxFACTURACION();
                int Total = _trx.GetResumenFacturasByFilterCount(null, null, dto.RUT_CLIENTE, dto.NOMBRE_CLIENTE, dto.FECHA_FACTURACION, dto.NUMERO_FACTURA, dto.ESTADO_FACTURA);
                grdFacturas.DataSource = _trx.GetResumenFacturasByFilter(null, null, dto.RUT_CLIENTE, dto.NOMBRE_CLIENTE, dto.FECHA_FACTURACION, dto.NUMERO_FACTURA, dto.ESTADO_FACTURA, dto.PageIndex, dto.PageSize);
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

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DTO_RESUMEN_FACTURA dto = (DTO_RESUMEN_FACTURA)e.Row.DataItem;
                    CheckBox chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                    chkSeleccionar.Visible = !dto.NUMERO_FACTURA.HasValue;
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

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> ListaFacturas = new List<int>();
                foreach (GridViewRow fila in grdFacturas.Rows)
                {
                    if (fila.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSeleccionar = (CheckBox)fila.FindControl("chkSeleccionar");
                        if (chkSeleccionar.Checked)
                        {
                            int IdFactura = int.Parse(this.grdFacturas.DataKeys[fila.RowIndex]["ID_FACTURA"].ToString());
                            ListaFacturas.Add(IdFactura);
                        }
                    }
                }

                if (ListaFacturas.Count == 0)
                    throw new Exception("Debe seleccionar factura");
                else
                {
                    TrxFACTURACION _TrxFACTURACION = new TrxFACTURACION();
                    _TrxFACTURACION.AnularFacturas(ListaFacturas);
                    Response.Redirect("MensajeExito.aspx?t=Anular Factura&m=Se han anulado las facturas seleccionadas", false);
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
