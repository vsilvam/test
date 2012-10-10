using System;
using System.Web.UI;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using System.Web.UI.WebControls;
using LQCE.Transaccion.DTO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class NumerarFactura : UserControl
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
            grdFacturas.DataSource = facturacion.GetResumenFacturacionPorNumerar();
            grdFacturas.DataBind();
        }

        protected void btnNumerar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (Page.IsValid)
                {
                    int IdFacturacion = int.Parse(hdnID_FACTURACION.Value);
                    int IdTipoFactura = int.Parse(hdnID_TIPO_FACTURA.Value); 
                    bool todos = (rblNumerar.SelectedValue == "1");
                    int nroFactura = int.Parse(txtNroFactura.Text);
                    int? desde = null;
                    int? hasta = null;
                    if (!todos)
                    {
                        desde = int.Parse(txtDesdeN.Text);
                        hasta = int.Parse(txtHastaN.Text);
                    }
                    
                    //llama la trx que genera la numeracion
                    TrxFACTURACION trx = new TrxFACTURACION();
                    trx.NumerarFacturas(IdFacturacion, IdTipoFactura, todos,desde,hasta,nroFactura);
                    Response.Redirect("MensajeExito.aspx?t=Numerar Facturas&m=Se han numerado las facturas", false);
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

        protected void rblNumerar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool EsRango = (rblNumerar.SelectedValue != "1");
                txtDesdeN.Enabled = EsRango;
                txtHastaN.Enabled = EsRango;
                txtDesdeN.Text = "";
                txtHastaN.Text = "";
                RequiredFieldValidator_txtDesdeN.Enabled = EsRango;
                RequiredFieldValidator_txtHastaN.Enabled = EsRango;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
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
                    txtNroFactura.Text = "";
                    rblNumerar.SelectedValue = "1";

                    int index = int.Parse(e.CommandArgument.ToString());
                    HiddenField hdnIdFacturacion = (HiddenField)grdFacturas.Rows[index].FindControl("hdnIdFacturacion");
                    HiddenField hdnIdTipoFactura = (HiddenField)grdFacturas.Rows[index].FindControl("hdnIdTipoFactura");
                    hdnID_FACTURACION.Value = hdnIdFacturacion.Value;
                    hdnID_TIPO_FACTURA.Value = hdnIdTipoFactura.Value;
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
    }
}
