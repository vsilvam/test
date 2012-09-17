﻿using System;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using System.Web.UI.WebControls;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion.DTO;
using System.Globalization;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class BuscarFactura : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getEstadoPago();
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

        private void getEstadoPago()
        {
            TrxPAGO _trx = new TrxPAGO();
            ddlEstadoPago.Items.Clear();
            ddlEstadoPago.Items.Add(new ListItem("(Todos)", ""));
            ddlEstadoPago.DataSource = _trx.GetAll();
            ddlEstadoPago.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdFacturas.PageIndex = 1;
                Facturas();
                Paginador1.SetPage(1);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void Facturas()
        {
            try
            {
                pnFacturas.Visible = true;

                IFormatProvider culture = new CultureInfo("es-CL", true);
                DTOFindFactura dto = new DTOFindFactura();
                dto.PageIndex = grdFacturas.PageIndex;
                dto.PageSize = grdFacturas.PageSize;

                if(!string.IsNullOrEmpty(txtRutCliente.Text))
                    dto.rut = txtRutCliente.Text;
                if(!string.IsNullOrEmpty(txtNombreCliente.Text))
                    dto.nombre = txtNombreCliente.Text;
                if(!string.IsNullOrEmpty(txtFechaEmision.Text))
                    dto.fecha = DateTime.Parse(txtFechaEmision.Text,culture);
                if(!string.IsNullOrEmpty(txtNroFactura.Text))
                    dto.numero = int.Parse(txtNroFactura.Text);
                if(!string.IsNullOrEmpty(ddlEstadoPago.SelectedValue))
                    dto.estado = true;


                TrxFACTURACION _trx = new TrxFACTURACION();
                int Total = _trx.GetResumenFacturasByFilterCount(dto.rut, dto.nombre, dto.fecha, dto.numero, dto.estado);
                grdFacturas.DataSource = _trx.GetResumenFacturasByFilter(dto.rut,dto.nombre,dto.fecha,dto.numero,dto.estado);
                grdFacturas.DataBind();

                Paginador1.TotalPages = Total % grdFacturas.PageSize == 0 ? Total / grdFacturas.PageSize : Total / grdFacturas.PageSize + 1;
                Paginador1.Visible = (Total > 0);
                Paginador1.Inicializar(dto);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void lnkVer_Click(object sender, EventArgs e)
        {
            LinkButton _link = sender as LinkButton;
            int? Id = int.Parse(_link.CommandArgument);
            if (Id.HasValue)
                Response.Redirect("DetalleFactura.aspx?Id=" + Id, false);
        }

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            try
            {
                grdFacturas.PageSize = (e.CurrentPageSize == 0 ? 20 : e.CurrentPageSize);
                grdFacturas.PageIndex = e.CurrentPageNumber;
                Facturas();
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
