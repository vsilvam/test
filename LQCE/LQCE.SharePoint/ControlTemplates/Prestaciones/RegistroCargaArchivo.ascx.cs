using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion.Enum;
using LQCE.Modelo;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroCargaArchivo : System.Web.UI.UserControl
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

        protected void btnEliminarCarga_Click(object sender, EventArgs e)
        {
            //verificar los registros selecionados
            foreach (GridViewRow grilla in gridRegistroCargaArchivo.Rows)
            {
                CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                if (ChkEditar.Checked)
                {
                    var lblId = grilla.FindControl("lblId") as Label;
                    //Obteber estado de la carga
                    TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                    //List<DTO_RESUMEN_CARGA_PRESTACIONES> resumen = carga.GetResumenCargaPrestaciones(null, null);
                    //foreach (var lis in resumen)
                    //{
                    //    if (lis.ID_ESTADO == (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Pendiente)
                    //    {
                    //        //se puede eliminar la carga
                    //        int IdCargaPrestacionesEncabezado = lis.ID;
                    //        int IdCargaPrestacionesEstado = lis.ID_ESTADO;
                    //        carga.CambiarEstadoCarga(IdCargaPrestacionesEncabezado, IdCargaPrestacionesEstado);
                    //    }
                    //}
                    CARGA_PRESTACIONES_ENCABEZADO nose = carga.GetByIdWithReferences(int.Parse(lblId.Text));
                    int IdCargaPrestacionesEncabezado = nose.ID;
                    int IdCargaPrestacionesEstado = int.Parse(nose.CARGA_PRESTACIONES_ESTADO.ID.ToString());
                    carga.CambiarEstadoCarga(IdCargaPrestacionesEncabezado, IdCargaPrestacionesEstado);
                }
            }
            
        }

        protected void btnCompletarRevision_Click(object sender, EventArgs e)
        {
            //verificar los registros selecionados
            foreach (GridViewRow grilla in gridRegistroCargaArchivo.Rows)
            {
                CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                if (ChkEditar.Checked)
                {                    
                    TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                    List<DTO_RESUMEN_CARGA_PRESTACIONES> resumen = carga.GetResumenCargaPrestaciones(null, null);
                    foreach (var lis in resumen)
                    {                       
                        if (lis.ID_ESTADO == (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Pendiente)
                        {
                            //es posible cambiar al estado completado
                            int IdCargaPrestacionesEncabezado = lis.ID;
                            int IdCargaPrestacionesEstado = lis.ID_ESTADO;
                            carga.CambiarEstadoCarga(IdCargaPrestacionesEncabezado, IdCargaPrestacionesEstado);
                        }                        
                    }
                }
            }
        }

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            gridRegistroCargaArchivo.PageSize = (e.CurrentPageSize == 0 ? 10 : e.CurrentPageSize);
            gridRegistroCargaArchivo.PageIndex = e.CurrentPageNumber;
            CargaGrilla(null, null);
        }
    }
}
