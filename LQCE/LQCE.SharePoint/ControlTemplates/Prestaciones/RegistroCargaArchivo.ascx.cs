﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.Transaccion.Enum;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroCargaArchivo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
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
                panelMensaje.CssClass = "MostrarMensaje";
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
            try
            {
                ImageButton _link = sender as ImageButton;
                int? Id = int.Parse(_link.CommandArgument);
                if (Id.HasValue)
                {
                    //Se muestra el contenido del archivo seleccionado
                    Response.Redirect("EditarRegistros.aspx?Id=" + Id, false);

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? estado = null;
                int? prestacion = null;
                if (!string.IsNullOrEmpty(ddlEstado.SelectedValue))
                    estado = int.Parse(ddlEstado.SelectedValue);
                if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
                    prestacion = int.Parse(ddlTipoPrestacion.SelectedValue);

                CargaGrilla(estado, prestacion);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }        

        protected void btnEliminarCarga_Click(object sender, EventArgs e)
        {
            try
            {
                string Mensaje = "";
                //verificar los registros selecionados
                foreach (GridViewRow grilla in gridRegistroCargaArchivo.Rows)
                {
                    CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                    if (ChkEditar.Checked)
                    {
                        var hdnId = grilla.FindControl("hdnId") as HiddenField;
                        int IdCargaPrestacionesEncabezado = int.Parse(hdnId.Value);
                        
                        TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                        try
                        {
                            carga.CambiarEstadoCarga(IdCargaPrestacionesEncabezado, (int)ENUM_CARGA_PRESTACIONES_ESTADO.Eliminado);
                        }
                        catch (Exception ex2)
                        {
                            Mensaje = Mensaje + ex2.Message + ". ";
                        }
                    }
                }
                btnBuscar_Click(null, null);
                if (!string.IsNullOrEmpty(Mensaje))
                {
                    throw new Exception(Mensaje);
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

        protected void btnCompletarRevision_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar los registros selecionados
                foreach (GridViewRow grilla in gridRegistroCargaArchivo.Rows)
                {
                     CheckBox ChkEditar = (CheckBox)grilla.FindControl("ChkEditar");
                    if (ChkEditar.Checked)
                    {
                        var hdnId = grilla.FindControl("hdnId") as HiddenField;
                        int IdCargaPrestacionesEncabezado = int.Parse(hdnId.Value);

                        TrxCARGA_PRESTACIONES_ENCABEZADO carga = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                        carga.CambiarEstadoCarga(IdCargaPrestacionesEncabezado, (int)ENUM_CARGA_PRESTACIONES_ESTADO.Completado);
                    }
                }
                btnBuscar_Click(null, null);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            try
            {
                gridRegistroCargaArchivo.PageSize = (e.CurrentPageSize == 0 ? 10 : e.CurrentPageSize);
                gridRegistroCargaArchivo.PageIndex = e.CurrentPageNumber;
                CargaGrilla(null, null);
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
