﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarPrestacionesVeterinarias.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarPrestacionesVeterinarias" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/smoothness/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="<%= ResolveUrl("~/_layouts/Style/css/lqce.css")%>" type="text/css"
    media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.0.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/jquery-ui-timepicker-addon.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/jquery-ui-sliderAccess.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtRecepcion.ClientID %>').datetimepicker();
        $('#<%=txtMuestraFecha.ClientID %>').datetimepicker();
        $('#<%=txtFechaEntrega.ClientID %>').datetimepicker();
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="panelErrores" runat="server" Visible="false">
    <article class="module width_full">
   <header><h3>Errores Prestación</h3></header>
   <asp:BulletedList ID="grdErroresVeterinarios" runat="server"></asp:BulletedList>
</article>
</asp:Panel>
<article class="module width_full">
    
    <header><h3>Edición Prestación Veterinarias</h3></header>
    
     <asp:HiddenField ID="hdnListaExamen" runat="server" />

    <div class="module_content">

    <table>
        <tr>
            <td colspan="4"></td>
            <td>
                Numero de Ficha
            </td>
            <td>
                <asp:TextBox ID="txtNumeroFicha" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Nombre
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Especie<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtEspecie" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                Raza
            </td>
            <td>
                <asp:TextBox ID="txtRaza" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Edad
            </td>
            <td>
                <asp:TextBox ID="txtEdad" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Sexo
            </td>
            <td>
                <asp:TextBox ID="txtSexo" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Solicita
            </td>
            <td>
                <asp:TextBox ID="txtSolicita" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Telefono
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>
                Médico
            </td>
            <td>
                <asp:TextBox ID="txtMedico" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Procedencia
            </td>
            <td>
                <asp:TextBox ID="txtProcedencia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>
               Fecha  Recepción
            </td>
            <td>
                <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Fecha Toma de Muestra
            </td>
            <td>
                <asp:TextBox ID="txtMuestraFecha" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Fecha Entrega de Resultados
            </td>
            <td>
                <asp:TextBox ID="txtFechaEntrega" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Pendiente
            </td>
            <td>
                <asp:TextBox ID="txtPendiente" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Pagado
            </td>
            <td>
                <asp:TextBox ID="txtPagado" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td>
                Garantia
            </td>
            <td>
                <asp:TextBox ID="txtGarantia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Total
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
    </table>
    </div>

    </article>
<article class="module width_full">
     <asp:LinkButton ID="lnkAgregaFicha" runat="server" OnClick="lnkAgregaFicha_Click">Agrega Examen</asp:LinkButton>
                <asp:GridView ID="grdExamen" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None"
                   EnableModelValidation="True">
                    <Columns>
                       <asp:TemplateField HeaderText="EXAMEN">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExamen" runat="server" Text='<%# Eval("NOMBRE_EXAMEN") %>' Columns="30"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("VALOR_EXAMEN") %>' Columns="30"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="head" />
               </asp:GridView>
    </article>
<article class="module width_full">
    <asp:Panel ID="pnAgregaFila" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                EXAMEN
                            </td>
                            <td>
                                VALOR
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtExamen" runat="server" Columns="30"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtValor" runat="server" Columns="30"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtValor" ErrorMessage="solo se admiten numeros." ValidationGroup='btnAgrega'
                                ToolTip="debe ingresar un valor numerico." ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnAgrega" runat="server" Text="Agrega Registro" OnClick="btnAgrega_Click" />
                            </td>
                        </tr>
                    </table>
    </asp:Panel>
    </article>
<!--Validar, marcar error,guarda temporal  "dejar con botones" 
    En caso de error mostrar mensaje u observacions en la ficha
    -->
<footer>
      <div class="submit_link">
      <asp:Button ID="btnValidado" runat="server" Text="Validado" OnClick="btnValidado_Click" />
       <asp:Button ID="btnCancelar" runat="server" Text="Cerrar Revisión" CausesValidation="false"
              onclick="btnCancelar_Click" />
      </div>
    </footer>
