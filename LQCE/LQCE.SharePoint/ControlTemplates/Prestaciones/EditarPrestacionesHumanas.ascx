﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarPrestacionesHumanas.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarPrestacionesHumanas" %>

<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/smoothness/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="<%= ResolveUrl("~/_layouts/Style/css/lqce.css")%>" type="text/css" media="all" />

<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.0.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/jquery-ui-timepicker-addon.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/jquery-ui-sliderAccess.js")%>"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaRecepcion.ClientID %>').datetimepicker();
        $('#<%=txtHoraRecepcion.ClientID %>').datetimepicker();
    });
</script>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="panelErrores" runat="server" Visible="false">
    <article class="module width_full">
   <header><h3>Errores Prestación</h3></header>
   <asp:BulletedList ID="grdErroresHumanos" runat="server"></asp:BulletedList>
</article>
</asp:Panel>
<article class="module width_full">

    <header><h3>Edición Prestación</h3></header>

    <asp:HiddenField ID="hdnListaExamen" runat="server" />

    <div class="module_content">
        
    <table>
        <tr>
            <td style="width:150px">
                Numero de Ficha
            </td>
            <td>
                <asp:TextBox ID="txtNumeroFicha" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator4' runat='server' ControlToValidate='txtNumeroFicha' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Nombre
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='txtNombre' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
            <td>
                RUT
            </td>
            <td>
                <asp:TextBox ID="txtRut" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Edad
            </td>
            <td>
                <asp:TextBox ID="txtEdad" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Teléfono
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Médico
            </td>
            <td>
                <asp:TextBox ID="txtMedico" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>   
             <td>Procedencia</td>
        <td>
        <asp:TextBox ID="txtProcedencia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
        <asp:RequiredFieldValidator ID='RequiredFieldValidator7' runat='server' ControlToValidate='txtProcedencia' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
        </td>         
        </tr>
        <tr>       
            <td>
                Fecha Recepcion<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtFechaRecepcion" runat="server" Enabled="false" Columns="30" ></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator3' runat='server' ControlToValidate='txtFechaRecepcion' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
            <td>
                Hora Recepcion
            </td>
            <td>
                <asp:TextBox ID="txtHoraRecepcion" runat="server" Enabled="false" Columns="30" CssClass="hasDatePicker"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator6' runat='server' ControlToValidate='txtHoraRecepcion' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Prevision
            </td>
            <td>
                <asp:TextBox ID="txtPrevision" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Pagado
            </td>
            <td>
                <asp:TextBox ID="txtPagado" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Garantia
            </td>
            <td>
                <asp:TextBox ID="txtGarantia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
            <td>
                Pendiente
            </td>
            <td>
                <asp:TextBox ID="txtPendiente" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Recepcion
            </td>
            <td>
                <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false" Columns="30" CssClass="hasDatePicker"></asp:TextBox>
            </td>
            <td>
                Total
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>Muestra</td>
        <td colspan="5">
        <asp:TextBox ID="txtMuestra" runat="server" Enabled="false" Columns="30"></asp:TextBox>
        </td>
        </tr>
    </table>
     
     
     
      </div>
      

   </article>
<article class="module width_full">

  <header><h3>Agregar Nuevo Examen</h3></header>

   <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Agrega Examen</asp:LinkButton>
   <asp:Panel ID="pnAgregaFila" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                Examen
                            </td>
                            <td>
                                Valor
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtExamen" runat="server" Columns="30" Enabled="false" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator18' runat='server' ControlToValidate='txtExamen'
                                Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='Ingrese Examen'></asp:RequiredFieldValidator>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtValor" runat="server" Columns="30" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator2' runat='server' ControlToValidate='txtValor'
                                Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='Ingrese Valor'></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtValor" ErrorMessage="solo se admiten numeros." ValidationGroup='btnAgrega'
                                ToolTip="debe ingresar un valor numerico." ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnAgrega" runat="server" Text="Agrega Registro" OnClick="btnAgrega_Click" ValidationGroup='btnAgrega' />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
   </article>
<article class="module width_full">
  <header><h3>Exámenes</h3></header> 
     <asp:GridView ID="grdExamen"  CssClass="tablesorter" GridLines="None" runat="server" AutoGenerateColumns="False" Width="50%" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Examen">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExamen" runat="server" Text='<%# Eval("NOMBRE_EXAMEN") %>' Columns="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidatortxtExamen' runat='server' ControlToValidate='txtExamen' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("VALOR_EXAMEN") %>' Columns="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidatortxtValor' runat='server' ControlToValidate='txtValor' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="head" />
                </asp:GridView>
                Monto total prestacioones
                <asp:TextBox ID="txtMontoTotal" runat="server" Columns="30" ReadOnly="true"></asp:TextBox>
               <asp:RequiredFieldValidator ID='RequiredFieldValidatortxtMontoTotal' runat='server' ControlToValidate='txtMontoTotal' ValidationGroup="validado"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
    </article>
<footer>
      <div class="submit_link">
      <asp:Button ID="btnValidado" runat="server" Text="Validado" CausesValidation="false" OnClick="btnValidado_Click" ValidationGroup="validado"/>
      <asp:Button ID="btnCancelar" runat="server" Text="Cerrar Revisión" CausesValidation="false"
              onclick="btnCancelar_Click" />
      </div>
      </footer>
