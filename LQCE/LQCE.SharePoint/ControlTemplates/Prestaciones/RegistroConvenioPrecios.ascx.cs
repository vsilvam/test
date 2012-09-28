using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroConvenioPrecios : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow grilla in grdRegistroConvenioPrecio.Rows)
                {
                    CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                    if (ChkEditar.Checked)
                    {
                        var lblId = grilla.FindControl("lblId") as Label;
                        Response.Redirect("ModificarConvenioPrecio.ascx?Id=" + lblId.Text, false);
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
