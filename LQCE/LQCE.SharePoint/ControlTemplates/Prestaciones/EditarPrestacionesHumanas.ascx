<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaHora1.ClientID %>').datepicker({});
        $('#<%=txtFechaHora2.ClientID %>').datepicker({});

        $('#<%=txtFechaHoraEntrega1.ClientID %>').datepicker({});
        $('#<%=txtFechaHoraEntrega2.ClientID %>').datepicker({});
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<article class="module width_full">
   <header><h3>Errores Prestación</h3></header>
   <asp:BulletedList ID="grdErroresHumanos" runat="server"></asp:BulletedList>
</article>

<article class="module width_full">

    <header><h3>Edición Prestación</h3></header>

    <div class="module_content">
        
    <table>
        <tr>
            <td colspan="4">
                N° <span>:</span>
            </td>
            <td>
                <asp:Label ID="lblNroPrestacion" runat="server" Text="147958"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>Nombre<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                RUT<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRut" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Médico<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtMedico" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Edad<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtEdad" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Teléfono<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha/Hora<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtFechaHora1" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="txtFechaHora2" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                hr
            </td>
            <td>
                <asp:TextBox ID="txtHora" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
              
        <tr>
            <td>
                Prevision<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPrevision" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Pagado<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPagado" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Garantia<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtGarantia" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Pendiente<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPendiente" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha y Hora entrega de resultados
            </td>
            <td>
                <asp:TextBox ID="txtFechaHoraEntrega1" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="txtFechaHoraEntrega2" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Total<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ficha<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtFicha" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                -Servicio<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtServicio" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                -Diag<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtDiagostico" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Listo
            </td>
            <td>
                <asp:TextBox ID="txtListo" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Recepcion<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
      </div>
      <footer>
      <div class="submit_link">
      <asp:Button ID="btnValidado" runat="server" Text="Validado" OnClick="btnValidado_Click" />
      </div>
      </footer>

   </article>

<article class="module width_full">

  <header><h3>Agregar Nuevo Examen</h3></header>

   <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Agrega Examen</asp:LinkButton>
   <asp:Panel ID="pnAgregaFila" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                EXAMEN
                            </td>
                            <td>
                                CODIGO
                            </td>
                            <td>
                                VALOR
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtExamen" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator18' runat='server' ControlToValidate='txtExamen'
                                Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='Ingrese Examen'></asp:RequiredFieldValidator>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='txtCodigo'
                                Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='Ingrese Codigo'></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCorreo" runat="server"
                            ControlToValidate="txtCodigo" ErrorMessage="solo se admiten numeros."
                            ToolTip="debe ingresar un valor numerico." ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
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
     <asp:GridView ID="grdExamen"  CssClass="tablesorter" GridLines="None" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdExamen_RowDataBound" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="EXAMEN">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExamen" runat="server" Text='<%# Eval("NOMBRE_EXAMEN") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CODIGO">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("ID") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("VALOR_EXAMEN") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="head" />
                </asp:GridView>
    </article>
