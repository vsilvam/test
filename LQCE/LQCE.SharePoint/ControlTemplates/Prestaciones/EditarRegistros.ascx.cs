using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarRegistros : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    GetEstado();
                    //if (Request.QueryString["Id"] == null)
                    //    throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    //string Id = Request.QueryString["Id"].ToString();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void GetEstado()
        {
            TrxCARGA_PRESTACIONES_ESTADO estado = new TrxCARGA_PRESTACIONES_ESTADO();
            ddlEstadoPrestacion.DataSource = estado.GetAll();
            ddlEstadoPrestacion.DataBind();
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _link = sender as ImageButton;
            int? Id = int.Parse(_link.CommandArgument);
            if (Id.HasValue)
            {
                //Se muestra el contenido del archivo seleccionado
                //if(tipo prestacion)
                //{
                    Response.Redirect("EditarPrestacionesHumanas.aspx?Id=" + Id, false);
                //}
                //else
                //{
                //    Response.Redirect("_layouts/Prestaciones/EditarPrestacionesVeterinarias.aspx?Id=" + Id);
                //}
                
            }


            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] == null)
                throw new Exception("No se ha indicado identificador de la cuenta registrada");

            string Id = Request.QueryString["Id"].ToString();

            //Tomar valores de busqueda
            string  numero = string.Empty;
            string Nombre = string.Empty;
            int? Estado = null;
            string  Procedencia = string.Empty;
            if (!string.IsNullOrEmpty(txtNroFicha.Text))
                numero = txtNroFicha.Text;
            if (!string.IsNullOrEmpty(txtNombre.Text))
                Nombre = txtNombre.Text;
            if (!string.IsNullOrEmpty(ddlEstadoPrestacion.SelectedValue))
                Estado = int.Parse(ddlEstadoPrestacion.SelectedValue);
            if (!string.IsNullOrEmpty(txtProcedencia.Text))
                Procedencia = txtProcedencia.Text;

            grdPrestaciones.Visible = true;

            TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            //grdPrestaciones.DataSource = carga.GetDetalleCargaPrestaciones(int.Parse(Id), numero, Nombre, Estado.Value, Procedencia, 10, 10);
            grdPrestaciones.DataSource = carga.GetDetalleCargaPrestaciones(int.Parse(Id), numero, Nombre, Estado, Procedencia, 1, 10);
            grdPrestaciones.DataBind();
        }

        protected void grdPrestaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}
