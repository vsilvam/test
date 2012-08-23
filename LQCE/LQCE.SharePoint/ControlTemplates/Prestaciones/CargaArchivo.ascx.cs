using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class CargaArchivo : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ddlTipoPrestacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPrestacion.SelectedValue == "1")
            {
                Response.Redirect("/ControlTemplates/Prestaciones/UCCargaMasivaPrestacionesHumanas.ascx",false);
            }
            if (ddlTipoPrestacion.SelectedValue == "2")
            {
                Response.Redirect("/ControlTemplates/Prestaciones/UCCargaMasivaPrestacionesVeterinarias.ascx", false);
            }
        }
    }
}