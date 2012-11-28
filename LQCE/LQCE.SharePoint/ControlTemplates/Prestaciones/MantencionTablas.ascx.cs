using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.Modelo;
using System.Collections.Generic;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class MantencionTablas : UserControl
    {
        public static List<EXAMEN> _list = new List<EXAMEN>();

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

        private void MostrarRegiones()
        {
            TrxREGION trx = new TrxREGION();
            var lista = trx.GetAll();

            selComunaActualizarRegion.Items.Clear();
            selComunaActualizarRegion.Items.Add(new ListItem("(Seleccionar)", ""));
            selComunaActualizarRegion.DataSource = lista;
            selComunaActualizarRegion.DataBind();

            selComunaAgregarRegion.Items.Clear();
            selComunaAgregarRegion.Items.Add(new ListItem("(Seleccionar)", ""));
            selComunaAgregarRegion.DataSource = lista;
            selComunaAgregarRegion.DataBind();
        }

        private void MostrarEspecies()
        {
            TrxESPECIE trx = new TrxESPECIE();
            var lista = trx.GetAll();

            selRazaActualizarEspecie.Items.Clear();
            selRazaActualizarEspecie.Items.Add(new ListItem("(Seleccionar)", ""));
            selRazaActualizarEspecie.DataSource = lista;
            selRazaActualizarEspecie.DataBind();

            selRazaAgregarEspecie.Items.Clear();
            selRazaAgregarEspecie.Items.Add(new ListItem("(Seleccionar)", ""));
            selRazaAgregarEspecie.DataSource = lista;
            selRazaAgregarEspecie.DataBind();
        }

        private void MostrarTipoPrestaciones()
        {
            TrxTIPO_PRESTACION trx = new TrxTIPO_PRESTACION();
            var lista = trx.GetAll();

            selExamenActualizarTipoPrestacion.Items.Clear();
            selExamenActualizarTipoPrestacion.Items.Add(new ListItem("(Seleccionar)",""));
            selExamenActualizarTipoPrestacion.DataSource = lista;
            selExamenActualizarTipoPrestacion.DataBind();

            selExamenAgregaTipoPrestacion.Items.Clear();
            selExamenAgregaTipoPrestacion.Items.Add(new ListItem("(Seleccionar)",""));
            selExamenAgregaTipoPrestacion.DataSource = lista;
            selExamenAgregaTipoPrestacion.DataBind();
        }

        protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTablas.SelectedValue == "")
                {
                    lblMensaje.Text = "Debe seleccionar una tabla";
                    return;
                }
                Limpiar();
                switch (int.Parse(ddlTablas.SelectedValue))
                {
                    case 1:
                        panelComuna.Visible = true;
                        panelComunaAgregar.Visible = true;
                        panelComunaListar.Visible = false;
                        panelComunaActualizar.Visible = false;

                        txtComunaAgregarNombre.Text = "";
                        selComunaAgregarRegion.SelectedIndex = 0;
                        break;
                    case 3:
                        panelEspecie.Visible = true;
                        panelEspecieAgregar.Visible = true;
                        panelEspecieListar.Visible = false;
                        panelEspecieActualizar.Visible = false;

                        txtEspecieAgregarNombre.Text = "";
                        break;
                    case 7:
                        panelGarantia.Visible = true;
                        panelGarantiaAgregar.Visible = true;
                        panelGarantiaListar.Visible = false;
                        panelGarantiaActualizar.Visible = false;

                        txtGarantiaAgregarNombre.Text = "";
                        break;
                    case 8:
                        panelPrevision.Visible = true;
                        panelPrevisionAgregar.Visible = true;
                        panelPrevisionListar.Visible = false;
                        panelPrevisionActualizar.Visible = false;

                        txtPrevisionAgregarNombre.Text = "";
                        break;
                    case 9:
                        panelRaza.Visible = true;
                        panelRazaAgregar.Visible = true;
                        panelRazaListar.Visible = false;
                        panelRazaActualizar.Visible = false;

                        txtRazaAgregarNombre.Text = "";
                        selRazaAgregarEspecie.SelectedIndex = 0;
                        break;
                    case 10:
                        panelRegion.Visible = true;
                        panelRegionAgregar.Visible = true;
                        panelRegionListar.Visible = false;
                        panelRegionActualizar.Visible = false;

                        txtRegionAgregarId.Text = "";
                        txtRegionAgregarNombre.Text = "";
                        break;
                    case 11:
                        panelTipoCobro.Visible = true;
                        panelTipoCobroAgregar.Visible = true;
                        panelTipoCobroListar.Visible = false;
                        panelTipoCobroActualizar.Visible = false;

                        txtTipoCobroAgregarNombre.Text = "";
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
                Limpiar();
                MostrarRegiones();
                MostrarEspecies();
                MostrarTipoPrestaciones();
                switch (ddlTablas.SelectedValue)
                {
                    case "1":
                        panelComuna.Visible = true;
                        panelComunaListar.Visible = true;
                        panelComunaAgregar.Visible = false;
                        panelComunaActualizar.Visible = false;

                        var comuna = new TrxCOMUNA();
                        gridComunas.DataSource = comuna.GetAllWithReferences();
                        gridComunas.DataBind();
                        break;
                    case "2":
                        panelExamen.Visible = true;
                        panelExamenListar.Visible = true;
                        panelExamenAgregar.Visible = false;
                        panelExamenActualizar.Visible = false;

                        var examen = new TrxEXAMEN();
                        List<EXAMEN> _listaExamen = examen.GetAllWithReferences();
                        List<DTOExamen> lista = new List<DTOExamen>();
                        //gridExamen.DataSource = _listaExamen;
                        //gridExamen.DataBind();
                        foreach (var lis in _listaExamen)
                        {
                            DTOExamen _dtoExamen = new DTOExamen();
                            _dtoExamen.ID = lis.ID;
                            _dtoExamen.NOMBRE_EXAMEN = lis.NOMBRE;
                            _dtoExamen.CODIGO = lis.CODIGO;
                            _dtoExamen.TIPO_PRESTACION = lis.TIPO_PRESTACION.NOMBRE;
                            lista.Add(_dtoExamen);
 
                        }
                        gridExamen.DataSource = lista;
                        gridExamen.DataBind();



                        break;
                    case "3":
                        panelEspecie.Visible = true;
                        panelEspecieListar.Visible = true;
                        panelEspecieAgregar.Visible = false;
                        panelEspecieActualizar.Visible = false;

                        var Especie = new TrxESPECIE();
                        gridEspecies.DataSource = Especie.GetAllWithReferences();
                        gridEspecies.DataBind();
                        break;
                    case "7":
                        panelGarantia.Visible = true;
                        panelGarantiaListar.Visible = true;
                        panelGarantiaAgregar.Visible = false;
                        panelGarantiaActualizar.Visible = false;

                        var Garantia = new TrxGARANTIA();
                        gridGarantia.DataSource = Garantia.GetAllWithReferences();
                        gridGarantia.DataBind();
                        break;
                    case "8":
                        panelPrevision.Visible = true;
                        panelPrevisionListar.Visible = true;
                        panelPrevisionAgregar.Visible = false;
                        panelPrevisionActualizar.Visible = false;

                        var Prevision = new TrxPREVISION();
                        gridPrevision.DataSource = Prevision.GetAllWithReferences();
                        gridPrevision.DataBind();
                        break;
                    case "9":
                        panelRaza.Visible = true;
                        panelRazaListar.Visible = true;
                        panelRazaAgregar.Visible = false;
                        panelRazaActualizar.Visible = false;

                        var Raza = new TrxRAZA();
                        gridRazas.DataSource = Raza.GetAllWithReferences();
                        gridRazas.DataBind();
                        break;
                    case "10":
                        panelRegion.Visible = true;
                        panelRegionListar.Visible = true;
                        panelRegionAgregar.Visible = false;
                        panelRegionActualizar.Visible = false;

                        var Region = new TrxREGION();
                        gridRegiones.DataSource = Region.GetAll();
                        gridRegiones.DataBind();
                        break;
                    case "11":
                        panelTipoCobro.Visible = true;
                        panelTipoCobroListar.Visible = true;
                        panelTipoCobroAgregar.Visible = false;
                        panelTipoCobroActualizar.Visible = false;

                        var TipoCobro = new TrxTIPO_COBRO();
                        gridTiposCobro.DataSource = TipoCobro.GetAll();
                        gridTiposCobro.DataBind();
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
            panelComuna.Visible = false;
            panelEspecie.Visible = false;
            panelGarantia.Visible = false;
            panelPrevision.Visible = false;
            panelRaza.Visible = false;
            panelRegion.Visible = false;
            panelTipoCobro.Visible = false;
        }

        /* COMUNA */
        protected void imgEliminarComuna_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var comuna = new TrxCOMUNA();
                comuna.Delete(Id);
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

        protected void imgActualizarComuna_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelComunaListar.Visible = false;
                panelComunaActualizar.Visible = true;
                panelComunaAgregar.Visible = false;

                var comuna = new TrxCOMUNA();
                var objComuna = comuna.GetByIdWithReferences(Id);
                hdnComunaActualizarId.Value = objComuna.ID.ToString();
                txtComunaActualizarNombre.Text = objComuna.NOMBRE;
                selComunaActualizarRegion.SelectedValue = objComuna.REGION.ID.ToString();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnComunaAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtComunaAgregarNombre.Text;
                    int RegionId = int.Parse(selComunaAgregarRegion.SelectedValue);

                    var comuna = new TrxCOMUNA();
                    comuna.Add(RegionId, Nombre);
                    ViewGrilla();
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

        protected void btnComunaAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnComunaActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnComunaActualizarId.Value);
                    string Nombre = txtComunaActualizarNombre.Text;
                    int RegionId = int.Parse(selComunaActualizarRegion.SelectedValue);

                    var comuna = new TrxCOMUNA();
                    comuna.Update(Id, RegionId, Nombre);
                    ViewGrilla();
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

        protected void btnComunaActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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


        /* ESPECIE */
        protected void imgEliminarEspecie_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var especie = new TrxESPECIE();
                especie.Delete(Id);
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

        protected void imgActualizarEspecie_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelEspecieListar.Visible = false;
                panelEspecieActualizar.Visible = true;
                panelEspecieAgregar.Visible = false;

                var Especie = new TrxESPECIE();
                var objEspecie = Especie.GetByIdWithReferences(Id);
                hdnEspecieActualizarId.Value = objEspecie.ID.ToString();
                txtEspecieActualizarNombre.Text = objEspecie.NOMBRE;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnEspecieAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtEspecieAgregarNombre.Text;

                    var Especie = new TrxESPECIE();
                    Especie.Add(Nombre);
                    ViewGrilla();
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

        protected void btnEspecieAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnEspecieActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnEspecieActualizarId.Value);
                    string Nombre = txtEspecieActualizarNombre.Text;

                    var Especie = new TrxESPECIE();
                    Especie.Update(Id, Nombre);
                    ViewGrilla();
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

        protected void btnEspecieActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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


        /* GARANTIA */
        protected void imgEliminarGarantia_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var Garantia = new TrxGARANTIA();
                Garantia.Delete(Id);
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

        protected void imgActualizarGarantia_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelGarantiaListar.Visible = false;
                panelGarantiaActualizar.Visible = true;
                panelGarantiaAgregar.Visible = false;

                var Garantia = new TrxGARANTIA();
                var objGarantia = Garantia.GetByIdWithReferences(Id);
                hdnGarantiaActualizarId.Value = objGarantia.ID.ToString();
                txtGarantiaActualizarNombre.Text = objGarantia.NOMBRE;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnGarantiaAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtGarantiaAgregarNombre.Text;

                    var Garantia = new TrxGARANTIA();
                    Garantia.Add(Nombre);
                    ViewGrilla();
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

        protected void btnGarantiaAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnGarantiaActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnGarantiaActualizarId.Value);
                    string Nombre = txtGarantiaActualizarNombre.Text;

                    var Garantia = new TrxGARANTIA();
                    Garantia.Update(Id, Nombre);
                    ViewGrilla();
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

        protected void btnGarantiaActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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


        /* PREVISION */
        protected void imgEliminarPrevision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var Prevision = new TrxPREVISION();
                Prevision.Delete(Id);
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

        protected void imgActualizarPrevision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelPrevisionListar.Visible = false;
                panelPrevisionActualizar.Visible = true;
                panelPrevisionAgregar.Visible = false;

                var Prevision = new TrxPREVISION();
                var objPrevision = Prevision.GetByIdWithReferences(Id);
                hdnPrevisionActualizarId.Value = objPrevision.ID.ToString();
                txtPrevisionActualizarNombre.Text = objPrevision.NOMBRE;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnPrevisionAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtPrevisionAgregarNombre.Text;

                    var Prevision = new TrxPREVISION();
                    Prevision.Add(Nombre);
                    ViewGrilla();
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

        protected void btnPrevisionAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnPrevisionActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnPrevisionActualizarId.Value);
                    string Nombre = txtPrevisionActualizarNombre.Text;

                    var Prevision = new TrxPREVISION();
                    Prevision.Update(Id, Nombre);
                    ViewGrilla();
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

        protected void btnPrevisionActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        /* RAZA */
        protected void imgEliminarRaza_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var Raza = new TrxRAZA();
                Raza.Delete(Id);
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

        protected void imgActualizarRaza_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelRazaListar.Visible = false;
                panelRazaActualizar.Visible = true;
                panelRazaAgregar.Visible = false;

                var Raza = new TrxRAZA();
                var objRaza = Raza.GetByIdWithReferences(Id);
                hdnRazaActualizarId.Value = objRaza.ID.ToString();
                txtRazaActualizarNombre.Text = objRaza.NOMBRE;
                selRazaActualizarEspecie.SelectedValue = objRaza.ESPECIE.ID.ToString();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnRazaAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtRazaAgregarNombre.Text;
                    int EspecieId = int.Parse(selRazaAgregarEspecie.SelectedValue);

                    var Raza = new TrxRAZA();
                    Raza.Add(EspecieId, Nombre);
                    ViewGrilla();
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

        protected void btnRazaAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnRazaActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnRazaActualizarId.Value);
                    string Nombre = txtRazaActualizarNombre.Text;
                    int EspecieId = int.Parse(selRazaActualizarEspecie.SelectedValue);

                    var Raza = new TrxRAZA();
                    Raza.Update(Id, EspecieId, Nombre);
                    ViewGrilla();
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

        protected void btnRazaActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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


        /* REGION */
        protected void imgEliminarRegion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var Region = new TrxREGION();
                Region.Delete(Id);
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

        protected void imgActualizarRegion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelRegionListar.Visible = false;
                panelRegionActualizar.Visible = true;
                panelRegionAgregar.Visible = false;

                var Region = new TrxREGION();
                var objRegion = Region.GetByIdWithReferences(Id);
                lblRegionActualizarId.Text = objRegion.ID.ToString();
                txtRegionActualizarNombre.Text = objRegion.NOMBRE;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnRegionAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(txtRegionAgregarId.Text);
                    string Nombre = txtRegionAgregarNombre.Text;

                    var Region = new TrxREGION();
                    Region.Add(Id, Nombre);
                    ViewGrilla();
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

        protected void btnRegionAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnRegionActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(lblRegionActualizarId.Text);
                    string Nombre = txtRegionActualizarNombre.Text;

                    var Region = new TrxREGION();
                    Region.Update(Id, Nombre);
                    ViewGrilla();
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

        protected void btnRegionActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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


        /* TIPOS DE COBRO */
        protected void imgEliminarTipoCobro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);

                var TipoCobro = new TrxTIPO_COBRO();
                TipoCobro.Delete(Id);
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

        protected void imgActualizarTipoCobro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelTipoCobroListar.Visible = false;
                panelTipoCobroActualizar.Visible = true;
                panelTipoCobroAgregar.Visible = false;

                var TipoCobro = new TrxTIPO_COBRO();
                var objTipoCobro = TipoCobro.GetByIdWithReferences(Id);
                hdnTipoCobroActualizarId.Value = objTipoCobro.ID.ToString();
                txtTipoCobroActualizarNombre.Text = objTipoCobro.NOMBRE;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnTipoCobroAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    string Nombre = txtTipoCobroAgregarNombre.Text;

                    var TipoCobro = new TrxTIPO_COBRO();
                    TipoCobro.Add(Nombre);
                    ViewGrilla();
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

        protected void btnTipoCobroAgregarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        protected void btnTipoCobroActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                if (Page.IsValid)
                {
                    int Id = int.Parse(hdnTipoCobroActualizarId.Value);
                    string Nombre = txtTipoCobroActualizarNombre.Text;

                    var TipoCobro = new TrxTIPO_COBRO();
                    TipoCobro.Update(Id, Nombre);
                    ViewGrilla();
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

        protected void btnTipoCobroActualizarCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

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

        /*  EXAMEN  */

        protected void gridActualizarSinonimoExamen_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    _list.RemoveAt(index);

                    gridActualizarSinonimoExamen.DataSource = _list;
                    gridActualizarSinonimoExamen.DataBind();                    
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

        protected void gridAgregarSinonimoExamen_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    _list.RemoveAt(index);

                    gridAgregarSinonimoExamen.DataSource = _list;
                    gridAgregarSinonimoExamen.DataBind();
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

        protected void imgEliminarExamen_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);


                var examen_sinonimo = new TrxEXAMEN_SINONIMO();
                var objExamen_sinonimo = examen_sinonimo.GetByFilter(Id,"");
                foreach (var lis in objExamen_sinonimo)
                {
                    examen_sinonimo.Delete(lis.ID);
                }

                var examen = new TrxEXAMEN();
                examen.Delete(Id);
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

        protected void imgActualizarExamen_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";

                ImageButton _link = sender as ImageButton;
                int Id = int.Parse(_link.CommandArgument);
                panelExamenListar.Visible = false;
                panelExamenAgregar.Visible = false;
                panelExamenActualizar.Visible = true;

                MostrarTipoPrestaciones();
                var examen = new TrxEXAMEN();
                var objExamen = examen.GetByIdWithReferences(Id);
                hdnExamenActuaizarId.Value = objExamen.ID.ToString();
                txtExamenActualizarNombre.Text = objExamen.NOMBRE;
                txtExamenActualizarCodigo.Text = objExamen.CODIGO;
                selExamenActualizarTipoPrestacion.SelectedValue = objExamen.TIPO_PRESTACION.ID.ToString();

                var examen_sinonimo = new TrxEXAMEN_SINONIMO();
                var objexamen_sinonimo = examen_sinonimo.GetByFilter(Id,"");
                gridActualizarSinonimoExamen.DataSource = objexamen_sinonimo;
                gridActualizarSinonimoExamen.DataBind();

            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnExamenAgregarIngresaSinonimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtExamenAgregarIngresaSinonimo.Text))
                {
                    List<EXAMEN> _list = new List<EXAMEN>();
                    EXAMEN objExamen = new EXAMEN();
                    objExamen.NOMBRE = txtExamenAgregarIngresaSinonimo.Text;
                    _list.Add(objExamen);

                    gridAgregarSinonimoExamen.DataSource = _list;
                    gridAgregarSinonimoExamen.DataBind();

                    txtExamenAgregarIngresaSinonimo.Text = string.Empty;
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

        protected void btnExamenAgregarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TrxEXAMEN _examen = new TrxEXAMEN();
                TrxEXAMEN_SINONIMO _sinonimoExamen = new TrxEXAMEN_SINONIMO();

                string NombreExamen = txtExamenAgregarNombre.Text;
                string CodigoExamen = txtExamenAgregarCodigo.Text;
                int TipoPrestacionExamen = int.Parse(selExamenAgregaTipoPrestacion.SelectedValue);

                /*Agrega examen*/
                var ingresoExamen = _examen.Add(TipoPrestacionExamen, CodigoExamen, NombreExamen);

                /*Guardar los sinonimos del examen desde la grilla*/
                foreach (GridViewRow grilla in gridAgregarSinonimoExamen.Rows)
                {
                    if (grilla.RowType == DataControlRowType.DataRow)
                    {
                        string IdSinonimo = gridAgregarSinonimoExamen.DataKeyNames[int.Parse("ID")];
                        TextBox txtNombreAgregaSinonimo = (TextBox)grilla.FindControl("txtNombreAgregaSinonimo");

                        _sinonimoExamen.Add(ingresoExamen, txtNombreAgregaSinonimo.Text);
                    }
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

        protected void btnExamenActualizarIngresaSinonimo_Click(object sender, EventArgs e)
        {
            try
            {                
                EXAMEN objExamen = new EXAMEN();
                objExamen.NOMBRE = txtExamenActualizarIngresaSinonimo.Text;
                _list.Add(objExamen);

                gridActualizarSinonimoExamen.DataSource = _list;
                gridActualizarSinonimoExamen.DataBind();

                txtExamenActualizarIngresaSinonimo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnExamenActualizarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TrxEXAMEN _examen = new TrxEXAMEN();
                TrxEXAMEN_SINONIMO _sinonimoExamen = new TrxEXAMEN_SINONIMO();

                int IdExamen = int.Parse(hdnExamenActuaizarId.Value);
                string NombreExamen = txtExamenActualizarNombre.Text;
                string CodigoExamen = txtExamenActualizarCodigo.Text;
                int TipoPrestacionExamen = int.Parse(selExamenActualizarTipoPrestacion.SelectedValue);

                /*Guardar los sinonimos del examen desde la grilla*/
                foreach (GridViewRow grilla in gridActualizarSinonimoExamen.Rows)
                {
                    if (grilla.RowType == DataControlRowType.DataRow)
                    {
                        string IdSinonimo = gridActualizarSinonimoExamen.DataKeyNames[int.Parse("ID")];
                        TextBox txtNombre = (TextBox)grilla.FindControl("txtNombre");

                        _sinonimoExamen.Update(int.Parse(IdSinonimo), IdExamen, txtNombre.Text);
                    }
                }
                     
                /*Actualiza examen*/                
                _examen.Update(IdExamen,TipoPrestacionExamen,CodigoExamen,NombreExamen);
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
