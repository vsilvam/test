<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetalleFactura.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.DetalleFactura" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.accordion.js")%>"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#accordion").accordion();
	    });
	</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<asp:panel ID='pnDetalleFacturas' runat='server'>
<h3>Ficha de Factura</h3>
    <table>
        <tr>
            <td>Nombre Cliente</td>
            <td>
                <asp:Label ID="lblNombreCliente" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Rut Cliente</td>
            <td>
                <asp:Label ID="lblRutCliente" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Fecha Emision</td>
            <td>
                <asp:Label ID="lblFechaEmision" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Numero Factura</td>
            <td>
                <asp:Label ID="lblNroFactura" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Estado de Pago</td>
            <td>
                <asp:Label ID="lblEstadoPago" runat="server"></asp:Label>
            </td>
        </tr>
    </table>    
</asp:panel>

<div id="accordion">
	<h3><a href="#">Detalle Factura</a></h3>
	<div>
		<asp:GridView ID="grdDetalleFactura" runat="server" AutoGenerateColumns='false' Width="100%">
    </asp:GridView>
	</div>
	<h3><a href="#">Pagos</a></h3>
	<div>
		<asp:GridView ID="grdPagos" runat="server" AutoGenerateColumns='false' Width="100%">
    </asp:GridView>
	</div>
	<h3><a href="#">Notas de Cobro</a></h3>
	<div>
		<asp:GridView ID="grdNotasCobro" runat="server" AutoGenerateColumns='false' Width="100%">
    </asp:GridView>
	</div>	
</div>

