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
            try
            {
                TrxCARGA_PRESTACIONES_HUMANAS_DETALLE PrestacionesHumanas = new TrxCARGA_PRESTACIONES_HUMANAS_DETALLE();
                var prestaciones = PrestacionesHumanas.GetByIdWithReferencesFull(Id);
                if (prestaciones == null)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js_carga_prestaciones", "javascript:alert('No existe información asociada.');", true);

                //cargar ficha
                txtNombre.Text = prestaciones.NOMBRE;
                txtNumeroFicha.Text = prestaciones.FICHA;
                txtFechaRecepcion.Text = prestaciones.FECHA_RECEPCION;
                txtTelefono.Text = prestaciones.TELEFONO;
                txtMedico.Text = prestaciones.MEDICO;
                txtProcedencia.Text = prestaciones.PROCEDENCIA;
                txtPrevision.Text = prestaciones.PREVISION;
                txtGarantia.Text = prestaciones.GARANTIA;
                txtPendiente.Text = prestaciones.PENDIENTE;
                txtPagado.Text = prestaciones.PAGADO;
                txtMontoTotal.Text = prestaciones.TOTAL;
                txtRecepcion.Text = prestaciones.RECEPCION;
                txtEdad.Text = prestaciones.EDAD;
                txtRut.Text = prestaciones.RUT;

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
                if (prestaciones.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID == (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente) //o con errores 
                    EditarFicha();

                CalculoMontoPrestaciones();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void CalculoMontoPrestaciones()
        {
            try
            {
                int montoTotal = 0;
                foreach (GridViewRow grilla in grdExamen.Rows)
                {
                    TextBox Valor = (TextBox)grilla.FindControl("txtValor");
                    if (!string.IsNullOrEmpty(Valor.Text))
                        montoTotal = int.Parse(Valor.Text) + montoTotal;
                }

                txtMontoTotal.Text = montoTotal.ToString();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void EditarFicha()
        {
            //Se habilita la ficha para edicion   
            txtNumeroFicha.Enabled = false;
            txtNombre.Enabled = true;
            txtFechaRecepcion.Enabled = true;
            txtTelefono.Enabled = true;
            txtMedico.Enabled = true;
            txtProcedencia.Enabled = true;
            txtPrevision.Enabled = true;
            txtGarantia.Enabled = true;
            txtPendiente.Enabled = true;
            txtPagado.Enabled = true;
            txtMontoTotal.Enabled = true;
            txtRecepcion.Enabled = true;
            txtEdad.Enabled = true;
            txtRut.Enabled = true;
            foreach (GridViewRow grilla in grdExamen.Rows)
            {
                if (grilla.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtExamen = (TextBox)grilla.FindControl("txtExamen");
                    TextBox txtValor = (TextBox)grilla.FindControl("txtValor");
                    txtExamen.Enabled = true;
                    txtValor.Enabled = true;
                }
            }

        }

        protected void btnValidado_Click(object sender, EventArgs e)
        {
            try
            {
                CalculoMontoPrestaciones();

                if (Request.QueryString["Id"] == null)
                        throw new Exception("No se ha indicado identificador de la cuenta registrada");

                    int Id = int.Parse(Request.QueryString["Id"].ToString());

                    if (!string.IsNullOrEmpty(txtRut.Text))
                        if (!ValidaRut(txtRut.Text))
                            throw new Exception("Rut no es valido");

                //se obtienen los datos desde el formuario
                IFormatProvider culture = new CultureInfo("es-CL", true);
                int IdCargaPrestacionesDetalleEstado = (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Validado;
                string ficha = !string.IsNullOrEmpty(txtNumeroFicha.Text) ? txtNumeroFicha.Text : string.Empty;
                string nombre = !string.IsNullOrEmpty(txtNombre.Text) ? txtNombre.Text : string.Empty;
                string fechaRecepcion = !string.IsNullOrEmpty(txtFechaRecepcion.Text) ? txtFechaRecepcion.Text : string.Empty;
                string telefono = !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : string.Empty;
                string medico = !string.IsNullOrEmpty(txtMedico.Text) ? txtMedico.Text : string.Empty;
                string procedencia = txtProcedencia.Text.Trim();
                string prevision = !string.IsNullOrEmpty(txtPrevision.Text) ? txtPrevision.Text : string.Empty;
                string garantia = !string.IsNullOrEmpty(txtGarantia.Text) ? txtGarantia.Text : string.Empty;
                string pendiente = !string.IsNullOrEmpty(txtPendiente.Text) ? txtPendiente.Text : string.Empty;
                string pagado = !string.IsNullOrEmpty(txtPagado.Text) ? txtPagado.Text : string.Empty;
                string total = !string.IsNullOrEmpty(txtMontoTotal.Text) ? txtMontoTotal.Text : string.Empty;
                string recepcion = txtRecepcion.Text.Trim();
                string edad = !string.IsNullOrEmpty(txtEdad.Text) ? txtEdad.Text : string.Empty;
                string rut = !string.IsNullOrEmpty(txtRut.Text) ? txtRut.Text : string.Empty;
                
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
                DTO_RESULTADO_ACTUALIZACION_FICHA resultado = PrestacionesEncabezado.ActualizarCargaPrestacionHumana(Id,
                    ficha, nombre, fechaRecepcion, telefono, medico, procedencia, prevision, garantia,
                    pendiente, pagado, total, recepcion, edad, rut,
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
                if (string.IsNullOrEmpty(txtExamen.Text))
                    throw new Exception("Debe ingrsesar un nombre para el examen");

                if (string.IsNullOrEmpty(txtValor.Text))
                    throw new Exception("Debe ingresar el valor del examen");

                DTOExamen dto = new DTOExamen();
                dto.NOMBRE_EXAMEN = txtExamen.Text;
                dto.ID = 0;
                dto.VALOR_EXAMEN = txtValor.Text;
                var lista = this.ListaExamen;
                lista.Add(dto);
                this.ListaExamen = lista;

                grdExamen.DataSource = lista;
                grdExamen.DataBind();

                txtExamen.Text = string.Empty;
                txtValor.Text = string.Empty;
                pnAgregaFila.Visible = false;

                CalculoMontoPrestaciones();
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
                txtExamen.Enabled = true;
                txtValor.Enabled = true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                List<DTOExamen> listaDTO = this.ListaExamen;
                int numeroFila = 0;
                foreach (GridViewRow grilla in grdExamen.Rows)
                {
                    CheckBox ChkSeleccionar = (CheckBox)grilla.FindControl("ChkSeleccionar");
                    if (ChkSeleccionar.Checked)
                    {
                        listaDTO[numeroFila].ID = -1;
                    }
                    numeroFila++;
                }
                listaDTO.RemoveAll(d => d.ID == -1);
                this.ListaExamen = listaDTO;
                grdExamen.DataSource = listaDTO;
                grdExamen.DataBind();

                CalculoMontoPrestaciones();
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

        public static bool ValidaRut(string rut)
        {
            try
            {
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                if (rut == "1-9")
                {
                    return false;
                }
                rut = rut.Trim();
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                string digit = rut.Substring(rut.Length - 1, 1);
                digit = digit.ToUpper();
                string digitComparer = "";
                rut = rut.Substring(0, rut.Length - 1);
                int wiMultiplicador = 9;
                int wiSumatoria = 0;
                int wiSubTotal = 0;
                int wiLargo = rut.Length;
                for (int i = wiLargo; i > 0; i--)
                {
                    wiSubTotal = Convert.ToInt32(rut.Substring(i - 1, 1));
                    wiSumatoria = wiSumatoria + (wiSubTotal * wiMultiplicador);
                    if (wiMultiplicador == 4)
                    {
                        wiMultiplicador = 10;
                    }
                    wiMultiplicador = wiMultiplicador - 1;
                }
                wiSumatoria = wiSumatoria % 11;
                if (wiSumatoria == 10)
                {
                    digitComparer = "K";
                }
                else
                {
                    digitComparer = wiSumatoria.ToString();
                }

                if (digit.Equals(digitComparer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
    }
}
