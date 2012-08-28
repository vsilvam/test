using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;

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
                    GetTipoPrestacion();
                    GetEstado();
                    CargaGrilla(null, null);
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
            ddlEstado.Items.Clear();
            ddlEstado.Items.Add(new ListItem("(Todos)", ""));
            ddlEstado.DataSource = estado.GetAll();
            ddlEstado.DataBind();
        }

        private void GetTipoPrestacion()
        {
            TrxTIPO_PRESTACION prestacion = new TrxTIPO_PRESTACION();
            ddlTipoPrestacion.Items.Clear();
            ddlTipoPrestacion.Items.Add(new ListItem("(Todos)", ""));
            ddlTipoPrestacion.DataSource = prestacion.GetAll();
            ddlTipoPrestacion.DataBind();
        }

        private void CargaGrilla(int? IdEstado,int? TipoPrestacion)
        {
            TrxCARGA_PRESTACIONES_ENCABEZADO CargaPrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            gridRegistroCargaArchivo.DataSource = CargaPrestacionesEncabezado.GetResumenCargaPrestaciones(IdEstado, TipoPrestacion);
            gridRegistroCargaArchivo.DataBind();
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _link = sender as ImageButton;
            int? Id = int.Parse(_link.CommandArgument);
            if (Id.HasValue)
            {
                //Se muestra el contenido del archivo seleccionado
                Response.Redirect("EditarRegistros.aspx?Id=" + Id, false);
               
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int? estado = null;
            int? prestacion = null;
            if(!string.IsNullOrEmpty(ddlEstado.SelectedValue))
                estado = int.Parse(ddlEstado.SelectedValue);
            if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
                prestacion = int.Parse(ddlTipoPrestacion.SelectedValue);

            CargaGrilla(estado, prestacion);
        }

        protected void gridRegistroCargaArchivo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DTO_RESUMEN_CARGA_PRESTACIONES carga = (DTO_RESUMEN_CARGA_PRESTACIONES)e.Row.DataItem;

                //Evaluar si se necesita este metodo
            }
        }

        protected void btnEliminarCarga_Click(object sender, EventArgs e)
        {
            //Obteber estado de la carga
            int Id = 1;
            TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            List<DTO_RESUMEN_CARGA_PRESTACIONES> resumen = carga.GetResumenCargaPrestaciones(null,null);
            foreach (var lis in resumen)
            {
                if (lis.ID_ESTADO != 1)
                {
                    //se puede eliminar la carga
                    //TrxCARGA_PRESTACIONES_ENCABEZADO.CambiarEstado();
                }
            }
        }

        protected void btnCompletarRevision_Click(object sender, EventArgs e)
        {
            int Id = 1;
            TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            List<DTO_RESUMEN_CARGA_PRESTACIONES> resumen = carga.GetResumenCargaPrestaciones(null,null);
            foreach (var lis in resumen)
            {
                if (lis.ID_ESTADO != 1)// && lis.REGISTROS_PENDIENTES == 0)
                {
                    //es posible cambiar al estado completado
                    //TrxCARGA_PRESTACIONES_ENCABEZADO.CambiarEstado();
                }
            }
        }

        
    }
}
