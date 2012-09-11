using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class MensajeExito : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                if (Request.QueryString["Id"] == null)
                    throw new Exception("No se ha indicado identificador de la cuenta registrada");
                string msg = Request.QueryString["Id"].ToString();
            }
        }
    }
}
