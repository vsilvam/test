using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;
using LQCE.Transaccion.DTO;
using System.Collections.Generic;
using LQCE.Modelo;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarPrestacionesVeterinarias : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void CargaFicha(int Id)
        {
            TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE veterinarias = new TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE();
            //var prestaciones = veterinarias.GetByIdWithReferences(Id);
            var prestaciones = veterinarias.GetByIdWithReferences(1);

            //cargar ficha
            txtFicha.Text = prestaciones.FICHA;
            lblNroPrestacion.Text = prestaciones.ID.ToString();
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
            txtHoraRecepcion.Text = prestaciones.FECHA_RECEPCION;
            txtMuestraFecha.Text = prestaciones.FECHA_MUESTRA;
            txtMuestraHora.Text = prestaciones.FECHA_MUESTRA;
            txtPendiente.Text = prestaciones.PENDIENTE;
            txtPagado.Text = prestaciones.PAGADO;
            txtGarantia.Text = prestaciones.GARANTIA;
            txtTotal.Text = prestaciones.TOTAL;
            txtFechaEntrega.Text = prestaciones.FECHA_RESULTADOS;
            txtHoraEntrega.Text = prestaciones.FECHA_RESULTADOS;
            txtRecepcionEntrega.Text = prestaciones.FECHA_RECEPCION;
            
            //carga grilla
            grdExamen.DataSource = prestaciones.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN;
            grdExamen.DataBind();

            //Habilitar Edicion de Ficha
            string estado = prestaciones.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.NOMBRE;
            if (estado == ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente.ToString())
                EditarFicha();

        }

        private void EditarFicha()
        {
            txtFicha.Enabled = true;
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
            txtHoraRecepcion.Enabled = true;
            txtMuestraFecha.Enabled = true;
            txtMuestraHora.Enabled = true;
            txtPendiente.Enabled = true;
            txtPagado.Enabled = true;
            txtGarantia.Enabled = true;
            txtTotal.Enabled = true;
            txtFechaEntrega.Enabled = true;
            txtHoraEntrega.Enabled = true;
            txtRecepcionEntrega.Enabled = true;
        }

        protected void btnValidado_Click(object sender, EventArgs e)
        {
            //se obtienen los datos desde el formuario
            int IdTipoPrestacion = 1;
            int IdCargaPrestacionesDetalleEstado = 2;
            string ficha = !string.IsNullOrEmpty(lblNroPrestacion.Text) ? lblNroPrestacion.Text : string.Empty;
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
            string horaRecepcion = !string.IsNullOrEmpty(txtHoraRecepcion.Text) ? txtHoraRecepcion.Text : string.Empty;
            string fechaMuestra = !string.IsNullOrEmpty(txtMuestraFecha.Text) ? txtMuestraFecha.Text : string.Empty;
            string horaMuestra = !string.IsNullOrEmpty(txtMuestraHora.Text) ? txtMuestraHora.Text : string.Empty;
            string pendiente = !string.IsNullOrEmpty(txtPendiente.Text) ? txtPendiente.Text : string.Empty;
            string pagado = !string.IsNullOrEmpty(txtPagado.Text) ? txtPagado.Text : string.Empty;
            string garantia = !string.IsNullOrEmpty(txtGarantia.Text) ? txtGarantia.Text : string.Empty;
            string total = !string.IsNullOrEmpty(txtTotal.Text) ? txtTotal.Text : string.Empty;
            string entregaDesde = !string.IsNullOrEmpty(txtFechaEntrega.Text) ? txtFechaEntrega.Text : string.Empty;
            string entregaHasta = !string.IsNullOrEmpty(txtFechaEntrega.Text) ? txtFechaEntrega.Text : string.Empty;
            string recepcion = !string.IsNullOrEmpty(txtRecepcionEntrega.Text) ? txtRecepcionEntrega.Text : string.Empty;

            //se recorren los examenes para guardar
            List<DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> lista = new List<DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN>();
            DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN _examen;
            foreach(GridViewRow grilla in grdExamen.Rows)
            {
                Label lblId = (Label)grilla.FindControl("lblId");
                Label lblExamen = (Label)grilla.FindControl("lblExamen");                
                Label lblValor = (Label)grilla.FindControl("lblValor");

                _examen = new DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN();
                _examen.ID = int.Parse(lblId.Text);
                _examen.NOMBRE_EXAMEN = lblExamen.Text;
                _examen.VALOR_EXAMEN = lblValor.Text;
                lista.Add(_examen);
            }

            //Se retornan errores en caso de existir
            TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE PrestacionesVeterinarias = new TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE();
            var prestaciones = PrestacionesVeterinarias.GetByIdWithReferences(1);
            string error = prestaciones.MENSAJE_ERROR;

            if (!string.IsNullOrEmpty(prestaciones.MENSAJE_ERROR))
            {
                lblMensaje.Text = prestaciones.MENSAJE_ERROR;
            }
            else
            {
                TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                DTO_RESULTADO_ACTUALIZACION_FICHA resutado = PrestacionesEncabezado.ActualizarCargaPrestacionVeterinarias(IdTipoPrestacion,ficha,nombre,
                    especie, raza, edad, sexo, solicita, telefono, medico, procedencia, fechaRecepcion, fechaMuestra,entregaDesde,pendiente,garantia,
                    pagado, total, IdCargaPrestacionesDetalleEstado, error, lista);

                if (!resutado.RESULTADO)
                {
                    // mostrar errores en grilla
                    grdErrores.DataSource = resutado;
                    grdErrores.DataBind();
                }
            }
        }

        protected void grdExamen_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
