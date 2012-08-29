using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Modelo;
using LQCE.Transaccion.Enum;
using LQCE.Transaccion.DTO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class EditarPrestacionesHumanas : UserControl
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
            TrxCARGA_PRESTACIONES_HUMANAS_DETALLE PrestacionesHumanas = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
            var prestaciones = PrestacionesHumanas.GetByIdWithReferences2(Id);
            //var prestaciones = PrestacionesHumanas.GetByIdWithReferences(1);

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
            //Se retornan errores en caso de existir
            TrxCARGA_PRESTACIONES_HUMANAS_DETALLE PrestacionesHumanas = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
            var prestaciones = PrestacionesHumanas.GetByIdWithReferences(1);

            if (string.IsNullOrEmpty(prestaciones.MENSAJE_ERROR))
            {
                lblMensaje.Text = prestaciones.MENSAJE_ERROR;
            }
            else
            {
                TrxCARGA_PRESTACIONES_ENCABEZADO PrestacionesEncabezado = new TrxCARGA_PRESTACIONES_ENCABEZADO();
                //DTO_RESULTADO_ACTUALIZACION_FICHA resutado = PrestacionesEncabezado.ActualizarCargaPrestacionHumana();
                
            }
        }

        protected void grdExamen_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
