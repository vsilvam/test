using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class ModificarConvenioPrecio : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getTipoPrestacion();

                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"]);
                    var trx = new TrxCONVENIO();
                    var objConvenio = trx.GetByIdWithReferences(Id);

                    txtId.Text = objConvenio.ID.ToString();
                    ddlTipoPrestacion.SelectedIndex = int.Parse(objConvenio.TIPO_PRESTACION.ID.ToString());
                    txtNombre.Text = objConvenio.NOMBRE;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                var Trx = new TrxCONVENIO();

                int Id = int.Parse(txtId.Text);
                int TipoPrestacion = int.Parse(ddlTipoPrestacion.SelectedValue);
                string Nombre = txtNombre.Text;
                Trx.Update(Id,TipoPrestacion,Nombre);
                Response.Redirect("MensajeExito.aspx?t=Modificar Convenio de Precios&m=Se ha modificado la informacion del convenio " + Nombre, false);
                
                //Response.Redirect("RegistroConvenioPrecios.ascx", false);
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
