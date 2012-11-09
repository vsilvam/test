<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
        $('#<%=txtFechaRecepción.ClientID %>').datetimepicker();
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="panelErrores" runat="server" Visible="false">
    <article class="module width_full">
        <header>
            <h3>
                Errores Prestación</h3>
        </header>
        <asp:BulletedList ID="grdErroresVeterinarios" runat="server">
        </asp:BulletedList>
    </article>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Edición Prestación Veterinarias</h3>
    </header>
    <asp:HiddenField ID="hdnListaExamen" runat="server" />
    <div class="module_content">
        <table width="80%">
            <tr>
                <td>
                    INGRESO
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroFicha" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    NOMBRE
                </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID='RequiredFieldValidator' runat='server' ControlToValidate='txtNombre'
                        ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                        ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                </td>
                <td style="width: 150px">
                    ESPECIE
                </td>
                <td>
                    <asp:TextBox ID="txtEspecie" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='txtEspecie'
                        ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                        ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    RAZA
                </td>
                <td>
                    <asp:TextBox ID="txtRaza" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
                <td>
                    SEXO
                </td>
                <td>
                    <asp:TextBox ID="txtSexo" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    EDAD
                </td>
                <td>
                    <asp:TextBox ID="txtEdad" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
                <td>
                    TELEFONO
                </td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    PROCEDENCIA
                </td>
                <td>
                    <asp:TextBox ID="txtProcedencia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID='RequiredFieldValidator_txtProcedencia' runat='server'
                        ControlToValidate='txtProcedencia' ValidationGroup="validado" Display='Dynamic'
                        Font-Size='7pt' ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                </td>
                <td>
                    GARANTIA
                </td>
                <td>
                    <asp:TextBox ID="txtGarantia" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    PENDIENTE
                </td>
                <td>
                    <asp:TextBox ID="txtPendiente" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
                <td>
                    RECEPCION
                </td>
                <td>
                    <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    MEDICO
                </td>
                <td>
                    <asp:TextBox ID="txtMedico" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                </td>
                <td>
                    SOLICITANTE
                </td>
                <td>
                    <asp:TextBox ID="txtSolicitante" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID='RequiredFieldValidator2' runat='server' ControlToValidate='txtSolicitante'
                        ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                        ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    FICHA
                </td>
                <td>
                    <asp:TextBox ID="txtFichaClinica" runat="server" Enabled="false" Columns="30" MaxLength="9"></asp:TextBox>
                </td>
                <td>
                    FECHA/HORA RECEPCION
                </td>
                <td>
                    <asp:TextBox ID="txtFechaRecepción" runat="server" Enabled="false" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID='RequiredFieldValidator3' runat='server' ControlToValidate='txtFechaRecepción'
                        ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                        ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
</article>
<article class="module width_full">
     <asp:LinkButton ID="lnkAgregaFicha" runat="server" OnClick="lnkAgregaFicha_Click">Agrega Examen</asp:LinkButton>&nbsp&nbsp
     <asp:LinkButton ID="lnkEliminaFicha" runat="server" 
    onclick="lnkEliminaFicha_Click" >Elimina Examen</asp:LinkButton>
                <asp:GridView ID="grdExamen" runat="server" AutoGenerateColumns="False" Width="70%"
                    GridLines="None" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSeleccionar" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Examen" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExamen" runat="server" Text='<%# Eval("NOMBRE_EXAMEN") %>' Columns="30"
                                    Enabled="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator_txtExamen' runat='server' ControlToValidate='txtExamen'
                                    ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                                    ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValorNuevoExamen" runat="server" Text='<%# Eval("VALOR_EXAMEN") %>'
                                    Columns="30" Enabled="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator_txtValorNuevoExamen' runat='server' ControlToValidate='txtValorNuevoExamen'
                                    ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                                    ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="head" />
                </asp:GridView>
               TOTAL
               <asp:TextBox ID="txtMontoTotal" runat="server" Columns="30" ReadOnly="true"></asp:TextBox>
               <asp:RequiredFieldValidator ID='RequiredFieldValidator5' runat='server' ControlToValidate='txtMontoTotal'
                   ValidationGroup="validado" Display='Dynamic' Font-Size='7pt' ForeColor='red'
                   ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
    </article>
<article class="module width_full">
    <asp:Panel ID="pnAgregaFila" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    <center>
                                Examen</center>
                </td>
                <td>
                    <center>
                                Valor</center>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtExamen" runat="server" Columns="30"></asp:TextBox>
                     <asp:RequiredFieldValidator ID='RequiredFieldValidator18' runat='server' ValidationGroup='btnAgrega' ControlToValidate='txtExamen'
                        Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='Ingrese Examen'>Requerido</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtValor" runat="server" Columns="30"></asp:TextBox>
                     <asp:RequiredFieldValidator ID='RequiredFieldValidator4' runat='server' ControlToValidate='txtValor'
                        Display='Dynamic' Font-Size='7pt'  ValidationGroup='btnAgrega' ForeColor='red' ErrorMessage='Ingrese Valor'>
                        Requerido</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtValor"
                        ErrorMessage="solo se admiten numeros." ValidationGroup='btnAgrega' ToolTip="debe ingresar un valor numerico."
                        ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnAgrega" runat="server" Text="Agrega Registro" OnClick="btnAgrega_Click" ValidationGroup='btnAgrega' />
                </td>
            </tr>
        </table>
    </asp:Panel>
</article>
<footer>
    <div class="submit_link">
        <asp:Button ID="btnValidado" runat="server" Text="Validado" OnClick="btnValidado_Click"
            ValidationGroup="validado" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cerrar Revisión" CausesValidation="false"
            OnClick="btnCancelar_Click" />
    </div>
</footer>
