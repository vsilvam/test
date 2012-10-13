<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarFactura.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.BuscarFactura" %>
<%@ Register TagPrefix="uc1" TagName="Paginador" Src="Paginador1.ascx" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css"
    type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaEmision.ClientID %>').datepicker({});
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID='pnBusquedaFactura' runat='server'>
    <h3>
        Busqueda de Facturas</h3>
    <table>
        <tr>
            <td>
                Rut Cliente
            </td>
            <td>
                <asp:TextBox ID="txtRutCliente" runat="server"></asp:TextBox>
                <asp:Label ID="lblRut" runat="server" Text="12345678-9"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Nombre Cliente
            </td>
            <td>
                <asp:TextBox ID="txtNombreCliente" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha Emision
            </td>
            <td>
                <asp:TextBox ID="txtFechaEmision" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Numero Factura
            </td>
            <td>
                <asp:TextBox ID="txtNroFactura" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCorreo" runat="server"
                    ControlToValidate="txtNroFactura" ErrorMessage="solo se admiten numeros." ToolTip="debe ingresar un valor numerico."
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Estado de Pago
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoPago" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
</asp:Panel>
<asp:Panel ID='pnFacturas' runat='server' Visible='false'>
    <asp:GridView ID="grdFacturas" runat="server" GridLines="Both" AutoGenerateColumns='false'
        Width='100%' DataKeyNames="ID_FACTURA" OnRowCommand="grdFacturas_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="Número Factura" DataField="NUMERO_FACTURA" />
            <asp:BoundField HeaderText="RUT" DataField="RUT_CLIENTE" />
            <asp:BoundField HeaderText="Cliente" DataField="NOMBRE_CLIENTE" />
            <asp:BoundField HeaderText="Fecha de Emisión" DataField="FECHA_EMISION" />
            <asp:BoundField HeaderText="Total" DataField="VALOR_TOTAL" />
            <asp:BoundField HeaderText="Pagado" DataField="VALOR_PAGADO" />
            <asp:BoundField HeaderText="Total Pagos" DataField="PAGOS_REGISTRADOS" />
            <asp:BoundField HeaderText="Saldo Deudor" DataField="SALDO_DEUDOR" />
            <asp:ButtonField CommandName="Detalles" Text="Ver Detalles" />
        </Columns>
        <EmptyDataTemplate>
            No hay facturas coincidientes.
        </EmptyDataTemplate>
    </asp:GridView>
    <uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged" />
</asp:Panel>
