using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion.DTO;

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
            int? IdTipoPrestacion = int.Parse(_link.CommandName);
            if (Id.HasValue)
            {
                //Se muestra el contenido del archivo seleccionado
                if (IdTipoPrestacion == (int)ENUM_TIPO_PRESTACION.Humanas)
                {
                    Response.Redirect("EditarPrestacionesHumanas.aspx?Id=" + Id, false);
                }
                else
                {
                    Response.Redirect("_layouts/Prestaciones/EditarPrestacionesVeterinarias.aspx?Id=" + Id);
                }
                
            }


            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdPrestaciones.PageIndex = 1;
            BuscarPrestaciones();
            Paginador1.SetPage(1);
        }

        private void BuscarPrestaciones()
        {
            grdPrestaciones.Visible = true;
            if (Request.QueryString["Id"] == null)
                throw new Exception("No se ha indicado identificador de la cuenta registrada");
            string Id = Request.QueryString["Id"].ToString();

            DTOFindPrestaciones dto = new DTOFindPrestaciones();
            dto.PageIndex = grdPrestaciones.PageIndex;
            dto.PageSize = grdPrestaciones.PageSize;

            if (!string.IsNullOrEmpty(txtNroFicha.Text))
                dto.numero = txtNroFicha.Text;
            if (!string.IsNullOrEmpty(txtNombre.Text))
                dto.nombre = txtNombre.Text;
            if (!string.IsNullOrEmpty(ddlEstadoPrestacion.SelectedValue))
                dto.estado = int.Parse(ddlEstadoPrestacion.SelectedValue);
            if (!string.IsNullOrEmpty(txtProcedencia.Text))
                dto.prodedencia = txtProcedencia.Text;
            dto.id = int.Parse(Id);


            //Tomar valores de busqueda
            //string numero = string.Empty;
            //string Nombre = string.Empty;
            //int? Estado = null;
            //string Procedencia = string.Empty;

            //if (!string.IsNullOrEmpty(txtNroFicha.Text))
            //    numero = txtNroFicha.Text;
            //if (!string.IsNullOrEmpty(txtNombre.Text))
            //    Nombre = txtNombre.Text;
            //if (!string.IsNullOrEmpty(ddlEstadoPrestacion.SelectedValue))
            //    Estado = int.Parse(ddlEstadoPrestacion.SelectedValue);
            //if (!string.IsNullOrEmpty(txtProcedencia.Text))
            //    Procedencia = txtProcedencia.Text;

            TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            int Total = carga.GetDetalleCargaPrestacionesCount(dto);
            grdPrestaciones.DataSource = carga.GetDetalleCargaPrestaciones(dto);
            grdPrestaciones.DataBind();

            Paginador1.TotalPages = Total % grdPrestaciones.PageSize == 0 ? Total / grdPrestaciones.PageSize : Total / grdPrestaciones.PageSize + 1;
            Paginador1.Visible = (Total > 0);
        }

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            grdPrestaciones.PageSize = (e.CurrentPageSize == 0 ? 10 : e.CurrentPageSize);
            grdPrestaciones.PageIndex = e.CurrentPageNumber;
            //CargaGrilla(null, null);
        }
    }
}
