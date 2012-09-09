using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.Transaccion.Enum;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarPrestacionesHumanas : System.Web.UI.UserControl
    {
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
            var prestaciones = PrestacionesHumanas.GetByIdWithReferences2(Id);
            if (prestaciones == null)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js_carga_prestaciones", "javascript:alert('No existe información asociada.');", true);
                
                            

            //cargar ficha
            lblNroPrestacion.Text = prestaciones.ID.ToString();
            txtNombre.Text = prestaciones.NOMBRE;
            txtRut.Text = prestaciones.RUT;
            txtMedico.Text = prestaciones.MEDICO;
            txtEdad.Text = prestaciones.EDAD;
            txtTelefono.Text = prestaciones.TELEFONO;
            txtFechaHora1.Text = prestaciones.FECHA_RECEPCION;
            txtFechaHora2.Text = prestaciones.FECHA_RECEPCION;
            txtPrevision.Text = prestaciones.PREVISION;
            txtPagado.Text = prestaciones.PAGADO;
            txtGarantia.Text = prestaciones.GARANTIA;
            txtPendiente.Text = prestaciones.PENDIENTE;
            txtFechaHoraEntrega1.Text = prestaciones.FECHA_RESULTADOS;
            txtFechaHoraEntrega2.Text = prestaciones.FECHA_RESULTADOS;
            txtTotal.Text = prestaciones.TOTAL;
            txtFicha.Text = prestaciones.FICHA;
            txtServicio.Text = prestaciones.MUESTRA;
            //txtDiagostico.Text = prestaciones;
            //txtListo.Text = prestaciones;
            //txtRecepcion.Text = prestaciones;

            //se carga grilla
            grdExamen.DataSource = prestaciones.CARGA_PRESTACIONES_HUMANAS_EXAMEN;
            grdExamen.DataBind();

            //Habilitar Edicion de Ficha
            if (prestaciones.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID == (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                EditarFicha();

        }

        private void EditarFicha()
        {
            //Se habilita la ficha para edicion            
            txtNombre.Enabled = true;
            txtRut.Enabled = true;
            txtMedico.Enabled = true;
            txtEdad.Enabled = true;
            txtTelefono.Enabled = true;
            txtFechaHora1.Enabled = true;
            txtFechaHora2.Enabled = true;
            txtPrevision.Enabled = true;
            txtPagado.Enabled = true;
            txtGarantia.Enabled = true;
            txtPendiente.Enabled = true;
            txtFechaHoraEntrega1.Enabled = true;
            txtFechaHoraEntrega2.Enabled = true;
            txtTotal.Enabled = true;
            txtFicha.Enabled = true;
            txtServicio.Enabled = true;
            txtDiagostico.Enabled = true;
            txtListo.Enabled = true;
            txtRecepcion.Enabled = true;

        }

        protected void btnValidado_Click(object sender, EventArgs e)
        {
            try
            {
                //se obtienen los datos desde el formuario
                int IdTipoPrestacion = 1;
                int IdCargaPrestacionesDetalleEstado = 2;
                string numero = !string.IsNullOrEmpty(lblNroPrestacion.Text) ? lblNroPrestacion.Text : string.Empty;
                string nombre = !string.IsNullOrEmpty(txtNombre.Text) ? txtNombre.Text : string.Empty;
                string rut = !string.IsNullOrEmpty(txtRut.Text) ? txtRut.Text : string.Empty;
                string medico = !string.IsNullOrEmpty(txtMedico.Text) ? txtMedico.Text : string.Empty;
                string edad = !string.IsNullOrEmpty(txtEdad.Text) ? txtEdad.Text : string.Empty;
                string telefono = !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : string.Empty;
                string procedencia = string.Empty;
                string fechaDesde = !string.IsNullOrEmpty(txtFechaHora1.Text) ? txtFechaHora1.Text : string.Empty;
                string fechaHasta = !string.IsNullOrEmpty(txtFechaHora2.Text) ? txtFechaHora2.Text : string.Empty;
                string fechaHora = !string.IsNullOrEmpty(txtHora.Text) ? txtHora.Text : string.Empty;
                string prevision = !string.IsNullOrEmpty(txtPrevision.Text) ? txtPrevision.Text : string.Empty;
                string pagado = !string.IsNullOrEmpty(txtPagado.Text) ? txtPagado.Text : string.Empty;
                string garantia = !string.IsNullOrEmpty(txtGarantia.Text) ? txtGarantia.Text : string.Empty;
                string pendiente = !string.IsNullOrEmpty(txtPendiente.Text) ? txtPendiente.Text : string.Empty;
                string entregaDesde = !string.IsNullOrEmpty(txtFechaHoraEntrega1.Text) ? txtFechaHoraEntrega1.Text : string.Empty;
                string entregaHasta = !string.IsNullOrEmpty(txtFechaHoraEntrega2.Text) ? txtFechaHoraEntrega2.Text : string.Empty;
                string total = !string.IsNullOrEmpty(txtTotal.Text) ? txtTotal.Text : string.Empty;
                string ficha = !string.IsNullOrEmpty(txtFicha.Text) ? txtFicha.Text : string.Empty;
                string servicio = !string.IsNullOrEmpty(txtServicio.Text) ? txtServicio.Text : string.Empty;
                string diagnostico = !string.IsNullOrEmpty(txtDiagostico.Text) ? txtDiagostico.Text : string.Empty;
                string listo = !string.IsNullOrEmpty(txtListo.Text) ? txtListo.Text : string.Empty;
                string recepcion = !string.IsNullOrEmpty(txtRecepcion.Text) ? txtRecepcion.Text : string.Empty;

                //se recorren los examenes para guardar
                List<DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN> lista = new List<DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN>();
                DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN _examen;
                foreach (GridViewRow grilla in grdExamen.Rows)
                {
                    TextBox txtExamen = (TextBox)grilla.FindControl("lblExamen");
                    TextBox txtCodigo = (TextBox)grilla.FindControl("lblCodigo");
                    TextBox txtValor = (TextBox)grilla.FindControl("lblValor");

                    _examen = new DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN();
                    _examen.ID = int.Parse(txtExamen.Text);
                    _examen.NOMBRE_EXAMEN = txtCodigo.Text;
                    _examen.VALOR_EXAMEN = txtValor.Text;
                    lista.Add(_examen);
                }

                //se guardan las modificaciones de la grilla.




                //Se retornan errores en caso de existir
                TrxCARGA_PRESTACIONES_HUMANAS_DETALLE PrestacionesHumanas = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
                var prestaciones = PrestacionesHumanas.GetByIdWithReferences(1);
                string error = prestaciones.MENSAJE_ERROR;

                if (string.IsNullOrEmpty(prestaciones.MENSAJE_ERROR))
                {
                    lblMensaje.Text = prestaciones.MENSAJE_ERROR;
                }
                else
                {
                    TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                    DTO_RESULTADO_ACTUALIZACION_FICHA resutado = PrestacionesEncabezado.ActualizarCargaPrestacionHumana(IdTipoPrestacion, ficha, nombre, rut,
                        medico, edad, telefono, procedencia, fechaDesde, "", entregaDesde, prevision, garantia, pagado, pendiente,
                        IdCargaPrestacionesDetalleEstado, error, lista);

                    if (!resutado.RESULTADO)
                    {
                        // mostrar errores en grilla
                        grdErroresHumanos.DataSource = resutado;
                        grdErroresHumanos.DataBind();
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

        protected void grdExamen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
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
                string examen = txtExamen.Text;
                string codigo = txtCodigo.Text;
                string valor = txtValor.Text;

                //guardar datos y cargar nuevamente la grilla
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
    }
}
