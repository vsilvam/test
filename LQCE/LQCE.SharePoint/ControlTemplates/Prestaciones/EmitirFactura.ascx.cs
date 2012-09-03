using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EmitirFactura : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdFacturas.Visible = true;

            string desde = !string.IsNullOrEmpty(txtDesde.Text) ? txtDesde.Text : string.Empty;
            string hasta = !string.IsNullOrEmpty(txtHasta.Text) ? txtDesde.Text : string.Empty;
            string cliente = !string.IsNullOrEmpty(txtCliente.Text) ? txtCliente.Text : string.Empty;
        }
    }
}
