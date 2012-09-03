using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirNotaCobro : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdNotaCobro.Visible = true;
        }
    }
}
