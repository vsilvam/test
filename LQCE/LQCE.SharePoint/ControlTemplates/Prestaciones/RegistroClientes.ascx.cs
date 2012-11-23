using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Infrastructure.Runtime;
using LQCE.Transaccion;
using LQCE.Transaccion.DTO;
using LQCE.SharePoint.ControlTemplates.App_Code;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class RegistroClientes : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getRegion();
                    getComuna();
                    getTipoPrestacion();
                    getConvenio();
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

        private void getRegion()
        {
            TrxREGION _trx = new TrxREGION();
            ddlRegion.Items.Clear();
            ddlRegion.Items.Add(new ListItem("(Todos)", ""));
            ddlRegion.DataSource = _trx.GetAll();
            ddlRegion.DataBind();
        }

        private void getComuna()
        {
            TrxCOMUNA _trx = new TrxCOMUNA();
            ddlComuna.Items.Clear();
            ddlComuna.Items.Add(new ListItem("(Todos)", ""));
            if (!string.IsNullOrEmpty(ddlRegion.SelectedValue))
            {
                ddlComuna.DataSource = _trx.GetByFilter(int.Parse(ddlRegion.SelectedValue), "");
                ddlComuna.DataBind();
            }
        }

        private void getTipoPrestacion()
        {
            TrxTIPO_PRESTACION _trx = new TrxTIPO_PRESTACION();
            ddlTipoPrestacion.Items.Clear();
            ddlTipoPrestacion.Items.Add(new ListItem("(Todos)", ""));
            ddlTipoPrestacion.DataSource = _trx.GetAll();
            ddlTipoPrestacion.DataBind();
        }

        private void getConvenio()
        {
            TrxCONVENIO _trx = new TrxCONVENIO();
            ddlConvenio.Items.Clear();
            ddlConvenio.Items.Add(new ListItem("(Todos)", ""));
            if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
            {
                ddlConvenio.DataSource = _trx.GetByFilter(int.Parse(ddlTipoPrestacion.SelectedValue), "");
                ddlConvenio.DataBind();
            }
            else
            {
                ddlConvenio.DataSource = _trx.GetAll();
                ddlConvenio.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdClientes.PageIndex = 1;
                BuscarCliente();
                Paginador1.SetPage(1);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private void BuscarCliente()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRutCliente.Text))
                    if (!ValidaRut(txtRutCliente.Text))
                        throw new Exception("Rut no es valido");
                
                panelMensaje.CssClass = "OcultarMensaje";

                DTOFindCliente dto = new DTOFindCliente();
                dto.PageIndex = grdClientes.PageIndex;
                dto.PageSize = grdClientes.PageSize;

                dto.RUT = txtRutCliente.Text;
                dto.NOMBRE = txtNombreCliente.Text;
                if (!string.IsNullOrEmpty(ddlRegion.SelectedValue))
                    dto.ID_REGION = int.Parse(ddlRegion.SelectedValue);
                if (!string.IsNullOrEmpty(ddlComuna.SelectedValue))
                    dto.ID_COMUNA = int.Parse(ddlComuna.SelectedValue);
                if (!string.IsNullOrEmpty(ddlTipoPrestacion.SelectedValue))
                    dto.ID_TIPO_PRESTACION = int.Parse(ddlTipoPrestacion.SelectedValue);
                if (!string.IsNullOrEmpty(ddlConvenio.SelectedValue))
                    dto.ID_CONVENIO = int.Parse(ddlConvenio.SelectedValue);

                pnClientes.Visible = true;
                var trx = new TrxCLIENTE();
                //grdClientes.DataSource = trx.GetByFilterWithReferences(Comuna, Convenio, null, null, Rut, Nombre, null, "", "", "");
                //grdClientes.DataBind();
                int Total = trx.GetByFilterWithFullReferencesCount(dto.RUT, dto.NOMBRE, dto.ID_REGION, dto.ID_COMUNA, dto.ID_TIPO_PRESTACION, dto.ID_CONVENIO);
                grdClientes.DataSource = trx.GetByFilterWithFullReferences(dto.RUT, dto.NOMBRE, dto.ID_REGION, dto.ID_COMUNA, dto.ID_TIPO_PRESTACION, dto.ID_CONVENIO, dto.PageIndex, dto.PageSize);
                grdClientes.DataBind();

                Paginador1.TotalPages = Total % grdClientes.PageSize == 0 ? Total / grdClientes.PageSize : Total / grdClientes.PageSize + 1;
                Paginador1.Visible = (Total > 0);
                Paginador1.Inicializar(dto);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void Paginador1_PageChanged(object sender, CustomPageChangeArgs e)
        {
            try
            {
                grdClientes.PageSize = (e.CurrentPageSize == 0 ? 20 : e.CurrentPageSize);
                grdClientes.PageIndex = e.CurrentPageNumber;
                BuscarCliente();
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
                if (Id.HasValue)
                    Response.Redirect("ActualizarClientes.aspx?Id=" + Id, false);
                
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
                txtRutCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
                ddlComuna.ClearSelection();
                ddlConvenio.ClearSelection();
                grdClientes.Visible = false;
                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        private bool ValidaRut(string rut)
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

        protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                ImageButton _link = sender as ImageButton;
                int? Id = int.Parse(_link.CommandArgument);
                if (Id.HasValue)
                {
                    TrxCLIENTE trxCliente = new TrxCLIENTE();
                    trxCliente.Delete(Id.Value);

                    BuscarCliente();
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

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getComuna();
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void ddlTipoPrestacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getConvenio();
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
