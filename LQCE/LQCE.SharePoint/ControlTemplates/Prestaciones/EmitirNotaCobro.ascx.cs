using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Modelo;
using System.Collections.Generic;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirNotaCobro : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getClientes();
                    getNotaCobro();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void getNotaCobro()
        {
            TrxTIPO_COBRO _tipo_cobro = new TrxTIPO_COBRO();
            ddlNotaCobro.Items.Clear();
            ddlNotaCobro.Items.Add(new ListItem("(Todos)", ""));
            ddlNotaCobro.DataSource = _tipo_cobro.GetAll();
            ddlNotaCobro.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnNotas.Visible = true;

            string desde = !string.IsNullOrEmpty(txtDesde.Text) ? txtDesde.Text : string.Empty;
            string hasta = !string.IsNullOrEmpty(txtHasta.Text) ? txtDesde.Text : string.Empty;
            string cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? ddlClientes.SelectedValue : string.Empty;
            string tipo = !string.IsNullOrEmpty(ddlNotaCobro.SelectedValue) ? ddlNotaCobro.SelectedValue : string.Empty ;

            var notaCobro = new TrxNOTA_COBRO();
            grdNotaCobro.DataSource = notaCobro.GetAllWithReferences();
            grdNotaCobro.DataBind();

            //se generan los archivos
        }

        private void getClientes()
        {
            TrxCLIENTE estado = new TrxCLIENTE();
            ddlClientes.Items.Clear();
            ddlClientes.Items.Add(new ListItem("(Todos)", ""));
            ddlClientes.DataSource = estado.GetAll();
            ddlClientes.DataBind();
        }

        protected void grdNotaCobro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<NOTA_COBRO> _nota = (List<NOTA_COBRO>)e.Row.DataItem;
                if (_nota != null)
                {
                    foreach (var lis in _nota)
                    {
                        Label lblRut = (Label)e.Row.FindControl("lblRut");
                        Label lblNombre = (Label)e.Row.FindControl("lblNombre");
                        Label lblCantidad = (Label)e.Row.FindControl("lblCantidad");
                        Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                        TextBox txtDescuento = (TextBox)e.Row.FindControl("txtDescuento");

                        //lblRut.Text = lis.Rut;
                        //lblNombre.Text = lis;
                        //lblCantidad.Text = lis;
                        //lblTotal.Text = lis;
                        //txtDescuento.Text = lis;
                    }
                }
            }
        }
    }
}
