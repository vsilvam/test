using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirNotaCredito : UserControl
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

        private void getFacturas()
        {
            DateTime FechaDesde = ISConvert.ToDateTime(txtFechaEmisionDesde.Text);
            DateTime FechaHasta = ISConvert.ToDateTime(txtFechaEmisionHasta.Text);

            var facturacion = new TrxFACTURACION();
            gridFacturacion.DataSource = facturacion.GetResumenFacturacion(FechaDesde, FechaHasta);
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
                    panelEmitir.Visible = true;
                    panelNota.Visible = false;
                    hdnID_FACTURACION.Value = this.gridFacturacion.DataKeys[index]["ID_FACTURACION"].ToString();
                    hdnID_TIPO_FACTURA.Value = this.gridFacturacion.DataKeys[index]["ID_TIPO_FACTURA"].ToString();
                    Facturas();
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
                int Total = _trx.GetResumenFacturasByFilterCount(dto.ID_FACTURACION, dto.ID_TIPO_FACTURA, "", "", null, null, null);
                grdFacturas.DataSource = _trx.GetResumenFacturasByFilter(dto.ID_FACTURACION, dto.ID_TIPO_FACTURA, "", "", null, null, null, dto.PageIndex, dto.PageSize);
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



        protected void grdFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Seleccionar")
                {
                    int IdFactura = int.Parse(e.CommandArgument.ToString());

                    TrxFACTURA _TrxFACTURA = new TrxFACTURA();
                    FACTURA _FACTURA = _TrxFACTURA.GetById(IdFactura);
                    if (_FACTURA == null)
                        throw new Exception("No se encuentra informacion de la factura");

                    if (!_FACTURA.NUMERO_FACTURA.HasValue)
                        throw new Exception("La factura no ha sido numerada");

                    if (_FACTURA.PAGADA.HasValue && _FACTURA.PAGADA.Value == true)
                        throw new Exception("La factura ya ha sido pagada");

                    panelNota.Visible = true;
                    hdnIdFactura.Value = IdFactura.ToString();
                    lblNumeroFactura.Text = _FACTURA.NUMERO_FACTURA.ToString();
                    txtNumeroNotaCredito.Text = "";
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
                if (Page.IsValid)
                {
                    int IdFactura = int.Parse(hdnIdFactura.Value);
                    int NumeroNotaCredito = int.Parse(txtNumeroNotaCredito.Text);
                    bool CorreccionTotal = (radioCorreccionTotal.SelectedValue == "1");

                    TrxFACTURACION _TrxFACTURACION = new TrxFACTURACION();
                    _TrxFACTURACION.EmitirNotaCredito(IdFactura, NumeroNotaCredito, CorreccionTotal);

                    Response.Redirect("MensajeExito.aspx?t=Emitir Nota de Crédito&m=Se ha emitido nota de crédito", false);

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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DTO_RESUMEN_FACTURA dto = (DTO_RESUMEN_FACTURA)e.Row.DataItem;
                    LinkButton linkSeleccionar = (LinkButton)e.Row.FindControl("linkSeleccionar");
                    linkSeleccionar.Visible = (dto.NUMERO_FACTURA.HasValue && dto.PAGADA == false);
                    linkSeleccionar.CommandArgument = dto.ID_FACTURA.ToString();
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