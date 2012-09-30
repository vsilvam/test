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
    public partial class EditarPrestacionesVeterinarias : System.Web.UI.UserControl
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
            TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE PrestacionesVeterinarias = new TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE();
            var prestaciones = PrestacionesVeterinarias.GetByIdWithReferencesFull(Id);
            if (prestaciones == null)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js_carga_prestaciones", "javascript:alert('No existe información asociada.');", true);

            //cargar ficha
            txtNumeroFicha.Text = prestaciones.FICHA;
            txtNombre.Text = prestaciones.NOMBRE;
            txtEspecie.Text = prestaciones.ESPECIE;
            txtRaza.Text = prestaciones.RAZA;
            txtEdad.Text = prestaciones.EDAD;
            txtSexo.Text = prestaciones.SEXO;
            txtSolicita.Text = prestaciones.SOLICITA;
            txtTelefono.Text = prestaciones.TELEFONO;
            txtMedico.Text = prestaciones.MEDICO;
            txtProcedencia.Text = prestaciones.PROCEDENCIA;
            txtRecepcion.Text = prestaciones.FECHA_RECEPCION;
            txtMuestraFecha.Text = prestaciones.FECHA_MUESTRA;
            txtPendiente.Text = prestaciones.PENDIENTE;
            txtPagado.Text = prestaciones.PAGADO;
            txtGarantia.Text = prestaciones.GARANTIA;
            txtTotal.Text = prestaciones.TOTAL;
            txtFechaEntrega.Text = prestaciones.FECHA_RESULTADOS;
            
            //carga grilla
            var lista = prestaciones.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Where(e => e.ACTIVO);
            List<DTOExamen> listaDTO = new List<DTOExamen>();
            foreach (var item in lista)
            {
                listaDTO.Add(new DTOExamen(item));
            }
            this.ListaExamen = listaDTO;

            //se carga grilla
            grdExamen.DataSource = listaDTO;
            grdExamen.DataBind();

            //validar
            TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
            var listaErrores = PrestacionesEncabezado.ValidarPrestacionVeterinaria(Id);
            grdErroresVeterinarios.DataSource = listaErrores;
            grdErroresVeterinarios.DataBind();
            panelErrores.Visible = listaErrores.Any();

            //Habilitar Edicion de Ficha
            //string estado = prestaciones.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.NOMBRE;
            //if (estado == ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente.ToString())
                EditarFicha();

        }

        private void EditarFicha()
        {
            txtNumeroFicha.Enabled = false;
            txtNombre.Enabled = true;
            txtEspecie.Enabled = true;
            txtRaza.Enabled = true;
            txtEdad.Enabled = true;
            txtSexo.Enabled = true;
            txtSolicita.Enabled = true;
            txtTelefono.Enabled = true;
            txtMedico.Enabled = true;
            txtProcedencia.Enabled = true;
            txtRecepcion.Enabled = true;
            txtMuestraFecha.Enabled = true;
            txtPendiente.Enabled = true;
            txtPagado.Enabled = true;
            txtGarantia.Enabled = true;
            txtTotal.Enabled = true;
            txtFechaEntrega.Enabled = true;
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
                string especie = !string.IsNullOrEmpty(txtEspecie.Text) ? txtEspecie.Text : string.Empty;
                string raza = !string.IsNullOrEmpty(txtRaza.Text) ? txtRaza.Text : string.Empty;
                string edad = !string.IsNullOrEmpty(txtEdad.Text) ? txtEdad.Text : string.Empty;
                string sexo = !string.IsNullOrEmpty(txtSexo.Text) ? txtSexo.Text : string.Empty;
                string solicita = !string.IsNullOrEmpty(txtSolicita.Text) ? txtSolicita.Text : string.Empty;
                string telefono = !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : string.Empty;
                string medico = !string.IsNullOrEmpty(txtMedico.Text) ? txtMedico.Text : string.Empty;
                string procedencia = !string.IsNullOrEmpty(txtProcedencia.Text) ? txtProcedencia.Text : string.Empty;
                string fechaRecepcion = !string.IsNullOrEmpty(txtRecepcion.Text) ? txtRecepcion.Text : string.Empty;
                string fechaMuestra = !string.IsNullOrEmpty(txtMuestraFecha.Text) ? txtMuestraFecha.Text : string.Empty;
                string pendiente = !string.IsNullOrEmpty(txtPendiente.Text) ? txtPendiente.Text : string.Empty;
                string pagado = !string.IsNullOrEmpty(txtPagado.Text) ? txtPagado.Text : string.Empty;
                string garantia = !string.IsNullOrEmpty(txtGarantia.Text) ? txtGarantia.Text : string.Empty;
                string total = !string.IsNullOrEmpty(txtTotal.Text) ? txtTotal.Text : string.Empty;
                string fechaEntrega = !string.IsNullOrEmpty(txtFechaEntrega.Text) ? txtFechaEntrega.Text : string.Empty;

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
                DTO_RESULTADO_ACTUALIZACION_FICHA resultado = PrestacionesEncabezado.ActualizarCargaPrestacionVeterinarias(Id, ficha, nombre,
                    especie, raza, edad, sexo, solicita, telefono, medico, procedencia, fechaRecepcion, fechaMuestra, fechaEntrega, pendiente, garantia,
                    pagado, total, IdCargaPrestacionesDetalleEstado, "", this.ListaExamen);

                if (!resultado.RESULTADO)
                {
                    // mostrar errores en grilla
                    var listaErrores = resultado.ERRORES_VALIDACION;
                    grdErroresVeterinarios.DataSource = listaErrores;
                    grdErroresVeterinarios.DataBind();
                    panelErrores.Visible = listaErrores.Any();
                }
                else
                {
                    //si no existio errores pasa al regsitro siguiente
                    string id = Request.QueryString["Id"].ToString();
                    Response.Redirect("EditarPrestacionesVeterinarias.aspx?Id=" + (id + 1).ToString());
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

        protected void lnkAgregaFicha_Click(object sender, EventArgs e)
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
            //guardar datos y cargar nuevamente la grilla
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"] == null)
                    throw new Exception("No se ha indicado identificador de la cuenta registrada");

                int Id = int.Parse(Request.QueryString["Id"].ToString());

                TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE _TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE();
                CARGA_PRESTACIONES_VETERINARIAS_DETALLE _CARGA_PRESTACIONES_VETERINARIAS_DETALLE = _TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferences(Id);
                int IdEncabezado = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO.ID;
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
