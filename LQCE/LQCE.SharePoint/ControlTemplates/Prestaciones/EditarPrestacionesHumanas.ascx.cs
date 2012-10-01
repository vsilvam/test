using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.Transaccion.Enum;
using LQCE.Modelo;
using System.Linq;
using System.Globalization;
using App.Infrastructure.Base;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarPrestacionesHumanas : System.Web.UI.UserControl
    {
        private List<DTOExamen> ListaExamen
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnListaExamen.Value))
                {
                    return ISSerializer.DeserializeObject<List<DTOExamen>>(hdnListaExamen.Value);
                }
                else
                {
                    return new List<DTOExamen>();
                }
            }
            set
            {
                hdnListaExamen.Value = ISSerializer.SerializeObject(value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    //Si toma Id desde url
                    if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    string Id = Request.QueryString["Id"].ToString();
                    CargaFicha(int.Parse(Id));
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

        private void CargaFicha(int Id)
        {
            TrxCARGA_PRESTACIONES_HUMANAS_DETALLE PrestacionesHumanas = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
            var prestaciones = PrestacionesHumanas.GetByIdWithReferencesFull(Id);
            if (prestaciones == null)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js_carga_prestaciones", "javascript:alert('No existe información asociada.');", true);

            //cargar ficha
            txtNumeroFicha.Text = prestaciones.FICHA;
            txtNombre.Text = prestaciones.NOMBRE;
            txtRut.Text = prestaciones.RUT;
            txtMedico.Text = prestaciones.MEDICO;
            txtEdad.Text = prestaciones.EDAD;
            txtTelefono.Text = prestaciones.TELEFONO;
            txtProcedencia.Text = prestaciones.PROCEDENCIA;
            txtFechaHora1.Text = prestaciones.FECHA_RECEPCION.Replace("/","-");
            txtPrevision.Text = prestaciones.PREVISION;
            txtPagado.Text = prestaciones.PAGADO;
            txtGarantia.Text = prestaciones.GARANTIA;
            txtPendiente.Text = prestaciones.PENDIENTE;
            txtFechaHoraEntrega1.Text = prestaciones.FECHA_RESULTADOS.Replace("/", "-"); 
            txtTotal.Text = prestaciones.TOTAL;
            txtMuestra.Text = prestaciones.MUESTRA;

            var lista = prestaciones.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Where(e => e.ACTIVO);
            List<DTOExamen> listaDTO = new List<DTOExamen>();
            foreach (var item in lista)
            {
                listaDTO.Add(new DTOExamen(item));
            }
            this.ListaExamen = listaDTO;

            //se carga grilla
            grdExamen.DataSource = listaDTO;
            grdExamen.DataBind();
            
            // validar
            TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            var listaErrores = PrestacionesEncabezado.ValidarPrestacionHumana(Id);
            grdErroresHumanos.DataSource = listaErrores;
            grdErroresHumanos.DataBind();
            panelErrores.Visible = listaErrores.Any();

            //Habilitar Edicion de Ficha
            //if (prestaciones.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID == (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente) //o con errores 
            EditarFicha();
        }

        private void EditarFicha()
        {
            //Se habilita la ficha para edicion   
            txtNumeroFicha.Enabled = false;
            txtNombre.Enabled = true;
            txtRut.Enabled = true;
            txtMedico.Enabled = true;
            txtEdad.Enabled = true;
            txtTelefono.Enabled = true;
            txtProcedencia.Enabled = true;
            txtFechaHora1.Enabled = true;
            txtPrevision.Enabled = true;
            txtPagado.Enabled = true;
            txtGarantia.Enabled = true;
            txtPendiente.Enabled = true;
            txtFechaHoraEntrega1.Enabled = true;
            txtTotal.Enabled = true;
            txtMuestra.Enabled = true;

        }

        protected void btnValidado_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"].ToString());

                //se obtienen los datos desde el formuario
                IFormatProvider culture = new CultureInfo("es-CL", true);
                int IdCargaPrestacionesDetalleEstado = (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Validado;
                string ficha = !string.IsNullOrEmpty(txtNumeroFicha.Text) ? txtNumeroFicha.Text : string.Empty;
                string nombre = !string.IsNullOrEmpty(txtNombre.Text) ? txtNombre.Text : string.Empty;
                string rut = !string.IsNullOrEmpty(txtRut.Text) ? txtRut.Text : string.Empty;
                string medico = !string.IsNullOrEmpty(txtMedico.Text) ? txtMedico.Text : string.Empty;
                string edad = !string.IsNullOrEmpty(txtEdad.Text) ? txtEdad.Text : string.Empty;
                string telefono = !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : string.Empty;
                string procedencia = txtProcedencia.Text.Trim();
                string fechaRecepcion = !string.IsNullOrEmpty(txtFechaHora1.Text) ? txtFechaHora1.Text : string.Empty;
                string prevision = !string.IsNullOrEmpty(txtPrevision.Text) ? txtPrevision.Text : string.Empty;
                string pagado = !string.IsNullOrEmpty(txtPagado.Text) ? txtPagado.Text : string.Empty;
                string garantia = !string.IsNullOrEmpty(txtGarantia.Text) ? txtGarantia.Text : string.Empty;
                string pendiente = !string.IsNullOrEmpty(txtPendiente.Text) ? txtPendiente.Text : string.Empty;
                string fechaResultado = !string.IsNullOrEmpty(txtFechaHoraEntrega1.Text) ? txtFechaHoraEntrega1.Text : string.Empty;
                string total = !string.IsNullOrEmpty(txtTotal.Text) ? txtTotal.Text : string.Empty;
                string muestra = txtMuestra.Text.Trim();

                //se recorren los examenes para guardar
                List<DTOExamen> listaDTO = this.ListaExamen;
                int numeroFila = 0;
                foreach (GridViewRow grilla in grdExamen.Rows)
                {
                    TextBox txtExamen = (TextBox)grilla.FindControl("txtExamen");
                    TextBox txtValor = (TextBox)grilla.FindControl("txtValor");

                    listaDTO[numeroFila].NOMBRE_EXAMEN = txtExamen.Text;
                    listaDTO[numeroFila].VALOR_EXAMEN = txtValor.Text;

                    numeroFila++;
                }
                this.ListaExamen = listaDTO;

                TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                DTO_RESULTADO_ACTUALIZACION_FICHA resultado = PrestacionesEncabezado.ActualizarCargaPrestacionHumana(Id, ficha, nombre, rut,
                    medico, edad, telefono, procedencia, fechaRecepcion, muestra,
                    fechaResultado, prevision, garantia, pagado, pendiente,
                    IdCargaPrestacionesDetalleEstado, "", this.ListaExamen);

                if (!resultado.RESULTADO)
                {
                    // mostrar errores en grilla
                    var listaErrores = resultado.ERRORES_VALIDACION;
                    grdErroresHumanos.DataSource = listaErrores;
                    grdErroresHumanos.DataBind();
                    panelErrores.Visible = listaErrores.Any();
                }
                else
                {
                    //si no existio errores pasa al regsitro siguiente
                    string id = Request.QueryString["Id"].ToString();
                    int? IdSiguiente = PrestacionesEncabezado.GetIdSiguienteFichaHumana(int.Parse(id));
                    if (IdSiguiente.HasValue)
                        Response.Redirect("EditarPrestacionesHumanas.aspx?Id=" + (IdSiguiente.Value).ToString(), false);
                    else
                        btnCancelar_Click(null, null);
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

        protected void btnAgrega_Click(object sender, EventArgs e)
        {
            try
            {
                DTOExamen dto = new DTOExamen();
                dto.NOMBRE_EXAMEN = txtExamen.Text;
                dto.ID = 0;
                dto.VALOR_EXAMEN = txtValor.Text;
                this.ListaExamen.Add(dto);

                grdExamen.DataSource = this.ListaExamen;
                grdExamen.DataBind();

                txtExamen.Text = string.Empty;
                txtValor.Text = string.Empty;
                pnAgregaFila.Visible = false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                pnAgregaFila.Visible = true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"] == null)
                    throw new Exception("No se ha indicado identificador de la cuenta registrada");

                int Id = int.Parse(Request.QueryString["Id"].ToString());

                TrxCARGA_PRESTACIONES_HUMANAS_DETALLE _TrxCARGA_PRESTACIONES_HUMANAS_DETALLE = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
                CARGA_PRESTACIONES_HUMANAS_DETALLE _CARGA_PRESTACIONES_HUMANAS_DETALLE = _TrxCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferences(Id);
                int IdEncabezado = _CARGA_PRESTACIONES_HUMANAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO.ID;
                Response.Redirect("EditarRegistros.aspx?Id=" + IdEncabezado, false);
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
