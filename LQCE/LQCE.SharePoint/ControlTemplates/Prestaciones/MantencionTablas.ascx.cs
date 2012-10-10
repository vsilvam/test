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
                ViewGrilla();                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
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
                        pnGrilla.Visible = false;
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
                        pnGrilla.Visible = false;
                        pnClienteSinonimo.Visible = true;
                        var clienteSinonimo = new TrxCLIENTE_SINONIMO();
                        var objClienteSinonimo = clienteSinonimo.GetByIdWithReferences(Id.Value);
                        txtIdClienteSinonimo.Text  = objClienteSinonimo.ID.ToString();
                        txtNombreSinonimo.Text = objClienteSinonimo.NOMBRE;
                        ddlClienteSinonimo.SelectedValue = objClienteSinonimo.CLIENTE.ID.ToString();
                        rblEstadoClienteSinonimo.SelectedValue = objClienteSinonimo.ACTIVO == true ? "1" : "2";
                        btnClienteSinonimo.Text = "Modificar";
                        break;
                    case 3: Limpiar();
                        pnGrilla.Visible = false;
                        pnEspecie.Visible = true;
                        var especie = new TrxESPECIE();
                        var objEspecie = especie.GetByIdWithReferences(Id.Value);
                        txtIdEspecie.Text = objEspecie.ID.ToString();
                        txtNombreEspecie.Text = objEspecie.NOMBRE;
                        rblEstadoEspecie.SelectedValue = objEspecie.ACTIVO == true ? "1" : "2";
                        btnEspecie.Text = "Modificar";
                        break;
                    case 4: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamen.Visible = true;
                        var examen = new TrxEXAMEN();
                        var objExamen = examen.GetByIdWithReferences(Id.Value);
                        txtIdExamen.Text = objExamen.ID.ToString();
                        ddlTipoPrestacionExamen.SelectedValue = objExamen.TIPO_PRESTACION.ID.ToString();
                        txtCodigoExamen.Text = objExamen.CODIGO;
                        txtNombreExamen.Text = objExamen.NOMBRE;
                        rblEstadoExamen.SelectedValue = objExamen.ACTIVO == true ? "1" : "2" ;
                        btnExamen.Text = "Modificar";
                        break;
                    case 5: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamenDetalle.Visible = true;
                        var examenDetalle = new TrxEXAMEN_DETALLE();
                        var objExmenDetalle = examenDetalle.GetByIdWithReferences(Id.Value);
                        txtIdExamenDetalle.Text = objExmenDetalle.ID.ToString();
                        txtExameExamenDetalle.Text = objExmenDetalle.EXAMEN.NOMBRE;
                        txtSubExamenExamenDetalle.Text = objExmenDetalle.EXAMEN1.NOMBRE;
                        rblActivoExamenDetalle.SelectedValue = objExmenDetalle.ACTIVO == true ? "1" : "2" ;
                        btnExamenDetalle.Text = "Modificar";
                        break;
                    case 6: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamenSinonimo.Visible = true;
                        getExamen();
                        var examenSinonimo = new TrxEXAMEN_SINONIMO();
                        var objExamenSinonimo = examenSinonimo.GetByIdWithReferences(Id.Value);
                        txtIdES.Text = objExamenSinonimo.ID.ToString();
                        ddlExamenES.SelectedValue = objExamenSinonimo.EXAMEN.ID.ToString();
                        txtSinonimoES.Text = objExamenSinonimo.NOMBRE;
                        rblActivoES.SelectedValue = objExamenSinonimo.ACTIVO == true ? "1": "2";
                        btnExamenSinonimo.Text = "Modificar";
                        break;
                    case 7: Limpiar();
                        pnGrilla.Visible = false;
                        pnGarantia.Visible = true;
                        var garantia = new TrxGARANTIA();
                        var objGarantia = garantia.GetByIdWithReferences(Id.Value);
                        txtIdG.Text = objGarantia.ID.ToString();
                        txtNombreG.Text = objGarantia.NOMBRE;
                        rblActivoG.Text = objGarantia.ACTIVO == true ? "1" : "2";
                        btnGarantia.Text = "Modificar";
                        break;
                    case 8: Limpiar();
                        pnGrilla.Visible = false;
                        pnPrevision.Visible = true;
                        var prevision = new TrxPREVISION();
                        var objPrevision = prevision.GetByIdWithReferences(Id.Value);
                        txtIdP.Text = objPrevision.ID.ToString();
                        txtNombreP.Text = objPrevision.NOMBRE;
                        rblActivoP.SelectedValue = objPrevision.ACTIVO == true ? "1" : "2";
                        btnPrevision.Text = "Modificar";
                        break;
                    case 9: Limpiar();
                        pnGrilla.Visible = false;
                        pnRaza.Visible = true;
                        getEspecie();
                        var raza = new TrxRAZA();
                        var objRaza = raza.GetByIdWithReferences(Id.Value);
                        txtIdR.Text = objRaza.ID.ToString();
                        txtNombreR.Text = objRaza.NOMBRE;
                        ddlEspecieR.SelectedValue = objRaza.ESPECIE.ID.ToString();
                        rdbActivoR.SelectedValue = objRaza.ACTIVO == true ? "1" : "2";
                        btnRaza.Text = "Modificar";
                        break;
                    case 10: Limpiar();
                        pnGrilla.Visible = false;
                        pnRegion.Visible = true;
                        var region = new TrxREGION();
                        var objRegion = region.GetByIdWithReferences(Id.Value);
                        txtIdRegion.Text = objRegion.ID.ToString();
                        txtNombreRegion.Text  = objRegion.NOMBRE;
                        rblActivoRegion.SelectedValue = objRegion.ACTIVO == true ? "1" : "2";
                        btnRegion.Text = "Modificar";
                        break;
                    case 11: Limpiar();
                        pnGrilla.Visible = false;
                        pnTipoCobro.Visible = true;
                        var tipoCobro = new TrxTIPO_COBRO();
                        var objTipoCobro = tipoCobro.GetByIdWithReferences(Id.Value);
                        txtIdTC.Text = objTipoCobro.ID.ToString();
                        txtNombreTC.Text = objTipoCobro.NOMBRE;
                        txtReporteTC.Text = objTipoCobro.REPORTE;
                        rblActivoTC.SelectedValue = objTipoCobro.ACTIVO == true ? "1" : "2";
                        btnTipoCobro.Text = "Modificar";
                        break;
                    case 12: Limpiar();
                        pnGrilla.Visible = false;
                        pntipoFactura.Visible = true;
                        break;
                    case 13: Limpiar();
                        pnGrilla.Visible = false;
                        pnTipoPrestacion.Visible = true;
                        var tipoPrestacion = new TrxTIPO_PRESTACION();
                        var objTipoPrestacion = tipoPrestacion.GetByIdWithReferences(Id.Value);
                        txtIdTP.Text = objTipoPrestacion.ID.ToString();
                        txtNombreTP.Text = objTipoPrestacion.NOMBRE;
                        rblActivoTP.SelectedValue = objTipoPrestacion.ACTIVO == true ? "1" : "2";
                        btnTipoPrestacion.Text = "Modificar";
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

        protected void btnCommand_Click(object sender, EventArgs e)
        {
            try
            {
                var btnCommand = (Button)sender;
                var accionPagina = Convert.ToInt16(btnCommand.CommandName);
                int? Id = null;
                bool Activo;

                switch (accionPagina)
                {
                    case 1: 
                        int? Region = null;
                        if (!string.IsNullOrEmpty(txtIdComuna.Text))
                            Id = int.Parse(txtIdComuna.Text);
                        string Nombre = txtNombreComuna.Text;
                        if (!string.IsNullOrEmpty(ddlRegionComuna.SelectedValue))
                            Region = int.Parse(ddlRegionComuna.SelectedValue);
                        Activo = rblEstadoComuna.SelectedValue == "1" ? true : false;
                        var Comuna = new TrxCOMUNA();
                        if (Id != null)
                            Comuna.Update(Id.Value, Region.Value, Nombre);
                        else
                            Comuna.Add(Region.Value, Nombre);
                        ViewGrilla();
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtIdClienteSinonimo.Text))
                            Id = int.Parse(txtIdClienteSinonimo.Text);
                        string sinonimo = txtNombreSinonimo.Text;
                        int cliente = int.Parse(ddlClienteSinonimo.SelectedValue);
                        Activo = rblEstadoClienteSinonimo.SelectedValue == "1" ? true: false;
                        var clienteSinonimo = new TrxCLIENTE_SINONIMO();
                        if (Id != null)
                            clienteSinonimo.Update(Id.Value,cliente,sinonimo);
                        else
                            clienteSinonimo.Add(cliente,sinonimo);
                        ViewGrilla();
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(txtIdEspecie.Text))
                            Id = int.Parse(txtIdEspecie.Text);
                        string nombre = txtNombreEspecie.Text;
                        Activo = rblEstadoEspecie.SelectedValue == "1" ? true : false;
                        var especie = new TrxESPECIE();
                        if(Id != null)
                            especie.Update(Id.Value,nombre);
                        else
                            especie.Add(nombre);
                        ViewGrilla();                        
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(txtIdExamen.Text))
                            Id = int.Parse(txtIdExamen.Text);
                        int tipoPrestacionEX = int.Parse(ddlTipoPrestacionExamen.SelectedValue);
                        string codigo = txtCodigoExamen.Text;
                        string nombreExamen = txtNombreExamen.Text;
                        Activo = rblEstadoExamen.SelectedValue == "1" ? true : false;
                        var examen = new TrxEXAMEN();
                        if (Id != null)
                            examen.Update(Id.Value,tipoPrestacionEX,codigo,nombreExamen);
                        else
                            examen.Add(tipoPrestacionEX, codigo, nombreExamen);
                        ViewGrilla();                        
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(txtIdExamenDetalle.Text))
                            Id = int.Parse(txtIdExamenDetalle.Text);
                        string nomExamen = txtExameExamenDetalle.Text;
                        string SubExamen = txtSubExamenExamenDetalle.Text;
                        Activo = rblActivoExamenDetalle.SelectedValue == "1" ? true : false;
                        var examenDetalle = new TrxEXAMEN_DETALLE();
                        //if (Id != null)
                        //    examenDetalle.Update(Id.Value, nomExamen, SubExamen);
                        //else
                        //    examenDetalle.Add(nomExamen, SubExamen);
                        ViewGrilla();
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(txtIdES.Text))
                            Id = int.Parse(txtIdES.Text);
                        int nomExamenSin = int.Parse(ddlExamenES.SelectedValue);
                        string ExSinonimo = txtSinonimoES.Text;
                        Activo = rblActivoES.SelectedValue == "1" ? true : false;
                        var examenSinonimo = new TrxEXAMEN_SINONIMO();
                        if (Id != null)
                            examenSinonimo.Update(Id.Value,nomExamenSin,ExSinonimo);
                        else
                            examenSinonimo.Add(nomExamenSin, ExSinonimo);
                        ViewGrilla();
                        break;
                    case 7:
                        if (!string.IsNullOrEmpty(txtIdG.Text))
                            Id = int.Parse(txtIdG.Text);
                        string nomGarantia = txtNombreG.Text;
                        Activo = rblActivoG.SelectedValue == "1" ? true : false;
                        var garantia = new TrxGARANTIA();
                        if (Id != null)
                            garantia.Update(Id.Value,nomGarantia);
                        else
                            garantia.Add(nomGarantia);
                        ViewGrilla();
                        break;
                    case 8:
                        if (!string.IsNullOrEmpty(txtIdP.Text))
                            Id = int.Parse(txtIdP.Text);
                        string nomPrevision = txtNombreP.Text;
                        Activo = rblActivoP.SelectedValue == "1" ? true : false;
                        var prevision = new TrxPREVISION();
                        if (Id != null)
                            prevision.Update(Id.Value,nomPrevision);
                        else
                            prevision.Add(nomPrevision);
                        ViewGrilla();
                        break;
                    case 9:
                        if (!string.IsNullOrEmpty(txtIdR.Text))
                            Id = int.Parse(txtIdR.Text);
                        string nomRaza = txtNombreR.Text;
                        int especieRaza = int.Parse(ddlEspecieR.SelectedValue);
                        Activo = rdbActivoR.SelectedValue == "1" ? true : false;
                        var raza = new TrxRAZA();
                        if (Id != null)
                            raza.Update(Id.Value,especieRaza,nomRaza);
                        else
                            raza.Add(especieRaza, nomRaza);
                        ViewGrilla();
                        break;
                    case 10:
                        if (!string.IsNullOrEmpty(txtIdRegion.Text))
                            Id = int.Parse(txtIdRegion.Text);
                        string nomRegion = txtNombreRegion.Text;
                        Activo = rblActivoRegion.SelectedValue == "1" ? true : false;
                        var region = new TrxREGION();
                        if (Id != null)
                            region.Update(Id.Value,nomRegion);
                        else
                            region.Add(nomRegion);
                        ViewGrilla();
                        break;
                    case 11:
                        if (!string.IsNullOrEmpty(txtIdTC.Text))
                            Id = int.Parse(txtIdTC.Text);
                        string nomTipoCobro = txtNombreTC.Text;
                        string reporte = txtReporteTC.Text;
                        Activo = rblActivoTC.SelectedValue == "1" ? true : false;
                        var tipoCobro = new TrxTIPO_COBRO();
                        if (Id != null)
                            tipoCobro.Update(Id.Value,nomTipoCobro,reporte);
                        else
                            tipoCobro.Add(nomTipoCobro, reporte);
                        break;
                    case 12://pendiente
                        //if (!string.IsNullOrEmpty(txtIdTF.Text))
                        //    Id = int.Parse(txtIdTF.Text);
                        ViewGrilla();
                        break;
                    case 13:
                        if (!string.IsNullOrEmpty(txtIdTP.Text))
                            Id = int.Parse(txtIdTP.Text);
                        string nomTipoPrestacion = txtNombreTP.Text;
                        Activo = rblActivoTP.SelectedValue == "1" ? true : false;
                        var tipoPrestacion = new TrxTIPO_PRESTACION();
                        if (Id != null)
                            tipoPrestacion.Update(Id.Value,nomTipoPrestacion);
                        else
                            tipoPrestacion.Add(nomTipoPrestacion);
                        ViewGrilla();
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTablas.SelectedValue == "")
                {
                    lblMensaje.Text = "Debe seleccionar una tabla";
                    return;
                }

                switch (int.Parse(ddlTablas.SelectedValue))
                {
                    case 1: Limpiar();
                        pnGrilla.Visible = false;
                        pnComuna.Visible = true;
                        getRegion();
                        btnModificarComuna.Text = "Agregar";
                        break;
                    case 2: Limpiar();
                        pnGrilla.Visible = false;
                        pnClienteSinonimo.Visible = true;
                        btnClienteSinonimo.Text = "Agregar";
                        break;
                    case 3: Limpiar();
                        pnGrilla.Visible = false;
                        pnEspecie.Visible = true;
                        btnEspecie.Text = "Agregar";
                        break;
                    case 4: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamen.Visible = true;
                        btnExamen.Text = "Agregar";
                        break;
                    case 5: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamenDetalle.Visible = true;
                        btnExamenDetalle.Text = "Agregar";
                        break;
                    case 6: Limpiar();
                        pnGrilla.Visible = false;
                        pnExamenSinonimo.Visible = true;
                        btnExamenSinonimo.Text = "Agregar";
                        break;
                    case 7: Limpiar();
                        pnGrilla.Visible = false;
                        pnGarantia.Visible = true;
                        btnGarantia.Text = "Agregar";
                        break;
                    case 8: Limpiar();
                        pnGrilla.Visible = false;
                        pnPrevision.Visible = true;
                        btnPrevision.Text = "Agregar";
                        break;
                    case 9: Limpiar();
                        pnGrilla.Visible = false;
                        pnRaza.Visible = true;
                        btnRaza.Text = "Agregar";
                        break;
                    case 10: Limpiar();
                        pnGrilla.Visible = false;
                        pnRegion.Visible = true;
                        btnRegion.Text = "Agregar";
                        break;
                    case 11: Limpiar();
                        pnGrilla.Visible = false;
                        pnTipoCobro.Visible = true;
                        btnTipoCobro.Text = "Agregar";
                        break;
                    case 12: Limpiar();
                        pnGrilla.Visible = false;
                        pntipoFactura.Visible = true;
                        btntipoFactura.Text = "Agregar";
                        break;
                    case 13: Limpiar();
                        pnGrilla.Visible = false;
                        pnTipoPrestacion.Visible = true;
                        btnTipoPrestacion.Text = "Agregar";
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

        private void ViewGrilla()
        {
            try
            {
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
            pnClienteSinonimo.Visible = false;
            pnEspecie.Visible = false;
            pnExamen.Visible = false;
            pnExamenDetalle.Visible = false;
            pnExamenSinonimo.Visible = false;
            pnGarantia.Visible = false;
            pnPrevision.Visible = false;
            pnRaza.Visible = false;
            pnRegion.Visible = false;
            pnTipoCobro.Visible = false;
            pntipoFactura.Visible = false;
            pnTipoPrestacion.Visible = false;
        }

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegionComuna.Items.Clear();
            ddlRegionComuna.Items.Add(new ListItem("(Todos)", ""));
            ddlRegionComuna.DataSource = _trx.GetAll();
            ddlRegionComuna.DataBind();
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
    }
}
