using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class MantencionTablas : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {

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

        protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                pnGrilla.Visible = true;
                switch (ddlTablas.SelectedValue)
                {
                    case "1": Limpiar();
                        var comuna = new TrxCOMUNA();
                        grdTablas.DataSource = comuna.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "2": Limpiar();
                        var clienteSinonimo = new TrxCLIENTE_SINONIMO();
                        grdTablas.DataSource = clienteSinonimo.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "3": Limpiar(); 
                        var especie = new TrxESPECIE();
                        grdTablas.DataSource = especie.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "4": Limpiar();
                        var examen = new TrxEXAMEN();
                        grdTablas.DataSource = examen.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "5": Limpiar();
                        var examenDetalle = new TrxEXAMEN_DETALLE();
                        grdTablas.DataSource = examenDetalle.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "6": Limpiar(); 
                        var examenSinonimo = new TrxEXAMEN_SINONIMO();
                        grdTablas.DataSource = examenSinonimo.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "7": Limpiar(); 
                        var garantia = new TrxGARANTIA();
                        grdTablas.DataSource = garantia.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "8": Limpiar(); 
                        var prevision = new TrxPREVISION();
                        grdTablas.DataSource = prevision.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "9": Limpiar(); 
                        var raza = new TrxRAZA();
                        grdTablas.DataSource = raza.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "10": Limpiar(); 
                        var region = new TrxREGION();
                        grdTablas.DataSource = region.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "11": Limpiar(); 
                        var tipoCobro = new TrxTIPO_COBRO();
                        grdTablas.DataSource = tipoCobro.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "12": Limpiar(); 
                        var tipoFactura = new TrxTIPO_FACTURA();
                        grdTablas.DataSource = tipoFactura.GetAll();
                        grdTablas.DataBind();
                        break;
                    case "13": Limpiar(); 
                        var tipoPrestacion = new TrxTIPO_PRESTACION();
                        grdTablas.DataSource = tipoPrestacion.GetAll();
                        grdTablas.DataBind();
                        break;
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

        private void Limpiar()
        {            
            pnComuna.Visible = false;
        }

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegionComuna.Items.Clear();
            ddlRegionComuna.Items.Add(new ListItem("(Todos)", ""));
            ddlRegionComuna.DataSource = _trx.GetAll();
            ddlRegionComuna.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                Limpiar();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void imgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int? Id = int.Parse(_link.CommandArgument);

                switch (int.Parse(ddlTablas.SelectedValue))
                {
                    case 1: Limpiar();
                        pnComuna.Visible = true;
                        getRegion();
                        var comuna = new TrxCOMUNA();
                        var objComuna = comuna.GetByIdWithReferences(Id.Value);
                        txtIdComuna.Text = objComuna.ID.ToString();
                        txtNombreComuna.Text = objComuna.NOMBRE;
                        ddlRegionComuna.SelectedValue = objComuna.REGION.ID.ToString();
                        rblEstadoComuna.SelectedValue = objComuna.ACTIVO == true ? "1" : "2" ;
                        btnModificarComuna.Text = "Modificar";
                        break;
                    case 2: Limpiar();
                        pnClienteSinonimo.Visible = true;
                        break;
                    case 3: Limpiar();
                        pnEspecie.Visible = true;
                        break;
                    case 4: Limpiar();
                        pnExamen.Visible = true;
                        break;
                    case 5: Limpiar();
                        pnExamenDetalle.Visible = true;
                        break;
                    case 6: Limpiar();
                        break;
                    case 7: Limpiar();
                        break;
                    case 8: Limpiar();
                        break;
                    case 9: Limpiar();
                        break;
                    case 10: Limpiar();
                        break;
                    case 11: Limpiar();
                        break;
                    case 12: Limpiar();
                        break;
                    case 13: Limpiar();
                        break;
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

        protected void btnModificarComuna_Click(object sender, EventArgs e)
        {
            try
            {
                int? Id = null;
                int? Region = null;
                if(!string.IsNullOrEmpty(txtIdComuna.Text))
                    Id = int.Parse(txtIdComuna.Text);
                string Nombre = txtNombreComuna.Text;
                if(!string.IsNullOrEmpty(ddlRegionComuna.SelectedValue))
                    Region = int.Parse(ddlRegionComuna.SelectedValue);
                bool Activo = rblEstadoComuna.SelectedValue == "1" ? true : false;
                var Comuna = new TrxCOMUNA();

                if (Id != null)
                {
                    Comuna.Update(Id.Value,Region.Value,Nombre);
                }
                else
                {
                    Comuna.Add(Region.Value,Nombre);
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

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (int.Parse(ddlTablas.SelectedValue))
                {
                    case 1: Limpiar();
                        pnComuna.Visible = true;
                        getRegion();
                        btnModificarComuna.Text = "Agregar";
                        break;
                    case 2: Limpiar();
                        pnClienteSinonimo.Visible = true;
                        break;
                    case 3: Limpiar();
                        pnEspecie.Visible = true;
                        break;
                    case 4: Limpiar();
                        pnExamen.Visible = true;
                        break;
                    case 5: Limpiar();
                        pnExamenDetalle.Visible = true;
                        break;
                    case 6: Limpiar();
                        break;
                    case 7: Limpiar();
                        break;
                    case 8: Limpiar();
                        break;
                    case 9: Limpiar();
                        break;
                    case 10: Limpiar();
                        break;
                    case 11: Limpiar();
                        break;
                    case 12: Limpiar();
                        break;
                    case 13: Limpiar();
                        break;
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
    }
}
