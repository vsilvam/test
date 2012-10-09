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
                        var clienteSinonimo = new TrxCLIENTE_SINONIMO();
                        var objClienteSinonimo = clienteSinonimo.GetByIdWithReferences(Id.Value);
                        txtIdClienteSinonimo.Text  = objClienteSinonimo.ID.ToString();
                        txtNombreSinonimo.Text = objClienteSinonimo.NOMBRE;
                        txtClienteSinonimo.Text = objClienteSinonimo.CLIENTE.NOMBRE;
                        rblEstadoClienteSinonimo.SelectedValue = objClienteSinonimo.ACTIVO == true ? "1" : "2";
                        break;
                    case 3: Limpiar();
                        pnEspecie.Visible = true;
                        var especie = new TrxESPECIE();
                        var objEspecie = especie.GetByIdWithReferences(Id.Value);
                        txtIdEspecie.Text = objEspecie.ID.ToString();
                        txtNombreEspecie.Text = objEspecie.NOMBRE;
                        rblEstadoEspecie.SelectedValue = objEspecie.ACTIVO == true ? "1" : "2";
                        break;
                    case 4: Limpiar();
                        pnExamen.Visible = true;
                        var examen = new TrxEXAMEN();
                        var objExamen = examen.GetByIdWithReferences(Id.Value);
                        txtIdExamen.Text = objExamen.ID.ToString();
                        ddlTipoPrestacionExamen.SelectedValue = objExamen.TIPO_PRESTACION.ID.ToString();
                        txtCodigoExamen.Text = objExamen.CODIGO;
                        txtNombreExamen.Text = objExamen.NOMBRE;
                        rblEstadoExamen.SelectedValue = objExamen.ACTIVO == true ? "1" : "2" ;
                        break;
                    case 5: Limpiar();
                        pnExamenDetalle.Visible = true;
                        var examenDetalle = new TrxEXAMEN_DETALLE();
                        var objExmenDetalle = examenDetalle.GetByIdWithReferences(Id.Value);
                        txtIdExamenDetalle.Text = objExmenDetalle.ID.ToString();
                        txtExameExamenDetalle.Text = objExmenDetalle.EXAMEN.NOMBRE;
                        txtSubExamenExamenDetalle.Text = objExmenDetalle.EXAMEN1.NOMBRE;
                        rblActivoExamenDetalle.SelectedValue = objExmenDetalle.ACTIVO == true ? "1" : "2" ;
                        break;
                    case 6: Limpiar();
                        pnExamenSinonimo.Visible = true;
                        getExamen();
                        var examenSinonimo = new TrxEXAMEN_SINONIMO();
                        var objExamenSinonimo = examenSinonimo.GetByIdWithReferences(Id.Value);
                        txtIdES.Text = objExamenSinonimo.ID.ToString();
                        ddlExamenES.SelectedValue = objExamenSinonimo.EXAMEN.ID.ToString();
                        txtSinonimoES.Text = objExamenSinonimo.NOMBRE;
                        rblActivoES.SelectedValue = objExamenSinonimo.ACTIVO == true ? "1": "2";
                        break;
                    case 7: Limpiar();
                        pnGarantia.Visible = true;
                        var garantia = new TrxGARANTIA();
                        var objGarantia = garantia.GetByIdWithReferences(Id.Value);
                        txtIdG.Text = objGarantia.ID.ToString();
                        txtNombreG.Text = objGarantia.NOMBRE;
                        rblActivoG.Text = objGarantia.ACTIVO == true ? "1" : "2";
                        break;
                    case 8: Limpiar();
                        pnPrevision.Visible = true;
                        var prevision = new TrxPREVISION();
                        var objPrevision = prevision.GetByIdWithReferences(Id.Value);
                        txtIdP.Text = objPrevision.ID.ToString();
                        txtNombreP.Text = objPrevision.NOMBRE;
                        rblActivoP.SelectedValue = objPrevision.ACTIVO = true ? "1" : "2";
                        break;
                    case 9: Limpiar();
                        pnRaza.Visible = true;
                        getEspecie();
                        var raza = new TrxRAZA();
                        var objRaza = raza.GetByIdWithReferences(Id.Value);
                        txtIdR.Text = objRaza.ID.ToString();
                        txtNombreR.Text = objRaza.NOMBRE;
                        ddlEspecieR.SelectedValue = objRaza.ESPECIE.ID.ToString();
                        rdbActivoR.SelectedValue = objRaza.ACTIVO == true ? "1" : "2";
                        break;
                    case 10: Limpiar();
                        pnRegion.Visible = true;
                        var region = new TrxREGION();
                        var objRegion = region.GetByIdWithReferences(Id.Value);
                        txtIdRegion.Text = objRegion.ID.ToString();
                        txtNombreRegion.Text  = objRegion.NOMBRE;
                        rblActivoRegion.SelectedValue = objRegion.ACTIVO == true ? "1" : "2";
                        break;
                    case 11: Limpiar();
                        pnTipoCobro.Visible = true;
                        var tipoCobro = new TrxTIPO_COBRO();
                        var objTipoCobro = tipoCobro.GetByIdWithReferences(Id.Value);
                        txtIdTC.Text = objTipoCobro.ID.ToString();
                        txtNombreTC.Text = objTipoCobro.NOMBRE;
                        txtReporteTC.Text = objTipoCobro.REPORTE;
                        rblActivoTC.SelectedValue = objTipoCobro.ACTIVO == true ? "1" : "2";
                        break;
                    case 12: Limpiar();
                        pntipoFactura.Visible = true;
                        break;
                    case 13: Limpiar();
                        pnTipoPrestacion.Visible = true;
                        var tipoPrestacion = new TrxTIPO_PRESTACION();
                        var objTipoPrestacion = tipoCobro.GetByIdWithReferences(Id.Value);
                        txtIdTP.Text = objTipoPrestacion.ID.ToString();
                        txtNombreTP.Text = objTipoPrestacion.NOMBRE;
                        rblActivoTP.SelectedValue = objTipoPrestacion.ACTIVO == true ? "1" : "2";
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

        private void getEspecie()
        {
            TrxESPECIE _trx = new TrxESPECIE();
            ddlEspecieR.Items.Clear();
            ddlEspecieR.Items.Add(new ListItem("(Todos)", ""));
            ddlEspecieR.DataSource = _trx.GetAll();
            ddlEspecieR.DataBind();
        }

        private void getExamen()
        {
            TrxEXAMEN _trx = new TrxEXAMEN();
            ddlExamenES.Items.Clear();
            ddlExamenES.Items.Add(new ListItem("(Todos)", ""));
            ddlExamenES.DataSource = _trx.GetAll();
            ddlExamenES.DataBind();
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
