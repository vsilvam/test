using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LQCE.Transaccion;
using App.Infrastructure.Runtime;
using System.Globalization;

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

        //private void getClientes()
        //{
        //    TrxCLIENTE estado = new TrxCLIENTE();
        //    ddlClientes.Items.Clear();
        //    ddlClientes.Items.Add(new ListItem("(Todos)", ""));
        //    ddlClientes.DataSource = estado.GetAll();
        //    ddlClientes.DataBind();
        //}

        private void getFacturas()
        {
            var facturacion = new TrxFACTURACION();
            grdFacturas.DataSource = facturacion.GetResumenFacturacionPorNumerar();
            grdFacturas.DataBind();
        }

        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
            //try
            //{
            //    if (Page.IsValid)
            //    {
            //        pnFacturas.Visible = true;
            //        IFormatProvider culture = new CultureInfo("es-CL", true);
            //        DateTime desde = DateTime.Parse(txtDesde.Text,culture);
            //        DateTime hasta = DateTime.Parse(txtHasta.Text,culture);
            //        int? cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? int.Parse(ddlClientes.SelectedValue) : (int?)null;

            //        var facturacion = new TrxFACTURACION();
            //        hdnFechaDesde.Value = desde.ToString();
            //        hdnFechaHasta.Value = hasta.ToString();
            //        cambiar metodo que buscas las facturas faltantes de numerar
            //        grdFacturas.DataSource = facturacion.GetResumenFacturacionPorNumerar();
            //        grdFacturas.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ISException.RegisterExcepcion(ex);
            //    panelMensaje.CssClass = "MostrarMensaje";
            //    lblMensaje.Text = ex.Message;
            //    return;
            //}
        //}

        protected void btnNumerar_Click(object sender, EventArgs e)
        {
            try 
            {
                //if (string.IsNullOrEmpty(hdnFechaDesde.Value) || string.IsNullOrEmpty(hdnFechaHasta.Value))
                //    throw new Exception("Debe realizar busqueda primero");

                //ID fcaturacion , bool numerar todos, si es falso debe pedir desde hasta, nuemro fact inicial.

                //tomar valores
                IFormatProvider culture = new CultureInfo("es-CL", true);
                LinkButton _link = sender as LinkButton;
                int? IdFacturacion = int.Parse(_link.CommandArgument);
                DateTime desde = DateTime.Parse(txtDesdeN.Text,culture);
                DateTime hasta = DateTime.Parse(txtHastaN.Text,culture);
                string nroFactura = txtNroFactura.Text;
                bool todos = Convert.ToBoolean(rblNumerar.SelectedValue);

                //llama la trx que genera la numeracion
                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void lnkNumerar_Click(object sender, EventArgs e)
        {
            pnNumerar.Visible = true;
        }

        protected void rblNumerar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblNumerar.SelectedValue == "1")
            {
                txtDesdeN.Enabled = false;
                txtHastaN.Enabled = false;
            }
        }     
    }
}
