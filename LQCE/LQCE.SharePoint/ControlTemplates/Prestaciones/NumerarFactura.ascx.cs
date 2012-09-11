﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LQCE.Transaccion;
using App.Infrastructure.Runtime;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public partial class NumerarFactura : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                panelMensaje.CssClass = "OcultarMensaje";
                if (!Page.IsPostBack && !Page.IsCallback)
                {
                    getClientes();
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

        private void getClientes()
        {
            TrxCLIENTE estado = new TrxCLIENTE();
            ddlClientes.Items.Clear();
            ddlClientes.Items.Add(new ListItem("(Todos)", ""));
            ddlClientes.DataSource = estado.GetAll();
            ddlClientes.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    pnFacturas.Visible = true;

                    DateTime desde = DateTime.Parse(txtDesde.Text);
                    DateTime hasta = DateTime.Parse(txtHasta.Text);
                    int? cliente = !string.IsNullOrEmpty(ddlClientes.SelectedValue) ? int.Parse(ddlClientes.SelectedValue) : (int?)null;

                    var facturacion = new TrxFACTURACION();
                    hdnFechaDesde.Value = desde.ToString();
                    hdnFechaHasta.Value = hasta.ToString();
                    //cambiar metodo que buscas las facturas faltantes de numerar
                    grdFacturas.DataSource = facturacion.GetClientesAFacturar(desde, hasta, cliente);
                    grdFacturas.DataBind();
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

        protected void btnNumerar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (string.IsNullOrEmpty(hdnFechaDesde.Value) || string.IsNullOrEmpty(hdnFechaHasta.Value))
                    throw new Exception("Debe realizar busqueda primero");

                //grilla  fecha facturacion,total facturas periodo,total fact x numerar

                //ID fcaturacion , bool numerar todos, si es falso debe pedir desde hasta, nuemro fact inicial.

                //tomar valores
                string desde = txtDesdeN.Text;
                string hasta = txtHastaN.Text;
                string nroFactura = txtNroFactura.Text;
                bool todos = Convert.ToBoolean(rblNumerar.SelectedValue);
                int IdFacturacion = 1;

                
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                panelMensaje.CssClass = "MostrarMensaje";
                lblMensaje.Text = ex.Message;
                return;
            }
        }

        protected void lnkNumerar_Click(object sender, EventArgs e)
        {
            pnNumerar.Visible = true;
        }

        protected void rblNumerar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblNumerar.SelectedValue == "1")
            {
                txtDesdeN.Enabled = false;
                txtHastaN.Enabled = false;
            }
        }        
    }
}
