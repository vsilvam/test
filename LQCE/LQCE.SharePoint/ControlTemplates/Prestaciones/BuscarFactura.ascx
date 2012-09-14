<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarFactura.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.BuscarFactura" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/Rut.js")%>"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaEmision.ClientID %>').datepicker({});
        $('#<%=txtFechaEmision.ClientID %>').jQuery.fn.Ru({});
    });
</script>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<asp:Panel ID='pnBusquedaFactura' runat='server'>
<h3>Busqueda de Facturas</h3>
    <table>
        <tr>
            <td>Rut Cliente</td>
            <td>
                <asp:TextBox ID="txtRutCliente" runat="server"></asp:TextBox>
                <asp:Label ID="lblRut" runat="server" Text="12345678-9"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Nombre Cliente</td>
            <td>
                <asp:TextBox ID="txtNombreCliente" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Fecha Emision</td>
            <td>
                <asp:TextBox ID="txtFechaEmision" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Numero Factura</td>
            <td>
                <asp:TextBox ID="txtNroFactura" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCorreo" runat="server"
                ControlToValidate="txtNroFactura" ErrorMessage="solo se admiten numeros."
                ToolTip="debe ingresar un valor numerico." ValidationExpression="\d+">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Estado de Pago</td>
            <td>
                <asp:DropDownList ID="ddlEstadoPago" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
        onclick="btnBuscar_Click" />
</asp:Panel>
<asp:Panel ID='pnFacturas' runat='server' Visible='false'>
    <asp:GridView ID="grdFacturas" runat="server" GridLines="None" AutoGenerateColumns='false' Width='100%'>
        <Columns>
            <asp:TemplateField HeaderText="RUT">
                <ItemTemplate>
                    <asp:Label ID="lblRut" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CLIENTE">
                <ItemTemplate>
                    <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FECHA EMISION">
                <ItemTemplate>
                    <asp:Label ID="lblFechaEmision" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VALOR TOTAL">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VALOR PAGADO">
                <ItemTemplate>
                    <asp:Label ID="lblPagado" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PAGOS REGISTRADOS">
                <ItemTemplate>
                    <asp:Label ID="lblPagsoRegistrados" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOTAL PAGOS">
                <ItemTemplate>
                    <asp:Label ID="lblTotalPagos" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SALDO DEUDOR">
                <ItemTemplate>
                    <asp:Label ID="lblSaldoDeudor" runat="server" Text='<%# Eval("") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkVer" runat="server" CommandArgument='<%# Eval("") %>' onclick="lnkVer_Click">Ver Detalles</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>