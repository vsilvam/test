using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroCargaArchivo : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    CargaGrilla();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void CargaGrilla()
        {
            TrxCARGA_PRESTACIONES_ENCABEZADO trx = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            gridPrevia.DataSource = trx.GetAll();
            gridPrevia.DataBind();
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _link = sender as ImageButton;
            int? Id = int.Parse(_link.CommandArgument);
            if (Id.HasValue)
            {
                //Se muestra el contenido del archivo seleccionado
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
