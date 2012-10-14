using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class NuevoConvenioPrecio : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getTipoPrestacion();
                    MostrarConvenios();
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

        private void MostrarConvenios()
        {
            try 
            {
                TrxCONVENIO convenio = new TrxCONVENIO();
                grdConvenios.DataSource = convenio.GetAllWithReferences();
                grdConvenios.DataBind();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void getTipoPrestacion()
        {
            try
            {
                TrxTIPO_PRESTACION _trx = new TrxTIPO_PRESTACION();
                ddlTipoPrestacion.Items.Clear();
                ddlTipoPrestacion.Items.Add(new ListItem("(Todos)", ""));
                ddlTipoPrestacion.DataSource = _trx.GetAll();
                ddlTipoPrestacion.DataBind();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                var Trx = new TrxCONVENIO();
                int? TipoPrestacion = null;
                string Nombre = string.Empty;
                if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
                {
                    lblMensaje.Text = "debe seleccionar un tipo de prestacion para la creacion del convenio";
                    return;
                }
                else
                    TipoPrestacion = int.Parse(ddlTipoPrestacion.SelectedValue);

                if (!string.IsNullOrEmpty(txtNombre.Text))
                {
                    lblMensaje.Text = "debe indicar un nombre al convenio";
                    return;
                }
                else
                    Nombre = txtNombre.Text;

                Trx.Add(TipoPrestacion.Value,Nombre);
                Response.Redirect("MensajeExito.aspx?t=Nuevo Convenio de Precios&m=Se ha registrado un nuevo convenio", false);

            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        //protected void ChkEditar_CheckedChanged(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        foreach (GridViewRow grilla in grdConvenios.Rows)
        //        {
        //            CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
        //            if (ChkEditar.Checked)
        //            {
        //                var lblId = grilla.FindControl("lblId") as Label;
        //                var lblTipoPrestacion = grilla.FindControl("lblTipoPrestacion") as Label;
        //                var lblNombre = grilla.FindControl("lblNombre") as Label;

        //                ddlTipoPrestacion.SelectedValue = lblTipoPrestacion.Text;
        //                txtNombre.Text = lblNombre.Text;

        //                pnNuevoConvenio.Visible = true;
        //                pnConvenios.Visible = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ISException.RegisterExcepcion(ex);
        //        panelMensaje.CssClass = "MostrarMensaje";
        //        lblMensaje.Text = ex.Message;
        //        return;
        //    }
        //}

        protected void lnkSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grilla in grdConvenios.Rows)
                {
                    LinkButton link = sender as LinkButton;
                    int? Id = int.Parse(link.CommandArgument);
                    var lblId = grilla.FindControl("lblId") as Label;
                    if (Id.Value == int.Parse(lblId.Text))
                    {
                        var lblIdTipoPrestacion = grilla.FindControl("lblIdTipoPrestacion") as Label;
                        var lblTipoPrestacion = grilla.FindControl("lblTipoPrestacion") as Label;
                        var lblNombre = grilla.FindControl("lblNombre") as Label;

                        ddlTipoPrestacion.SelectedValue = lblIdTipoPrestacion.Text;
                        txtNombre.Text = lblNombre.Text;

                        pnNuevoConvenio.Visible = true;
                        pnConvenios.Visible = false;
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
    }
}
