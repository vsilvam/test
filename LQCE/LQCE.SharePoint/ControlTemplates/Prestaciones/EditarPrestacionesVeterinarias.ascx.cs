using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.Enum;

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
            //Se retornan errores en caso de existir
            TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE PrestacionesVeterinarias = new TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE();
            var prestaciones = PrestacionesVeterinarias.GetByIdWithReferences(1);

            if (!string.IsNullOrEmpty(prestaciones.MENSAJE_ERROR))
            {
                lblMensaje.Text = prestaciones.MENSAJE_ERROR;
            }
        }

        protected void grdExamen_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
