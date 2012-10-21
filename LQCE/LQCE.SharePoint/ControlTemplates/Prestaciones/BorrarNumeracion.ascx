<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BorrarNumeracion.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.BorrarNumeracion" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css"
    type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaEmisionDesde.ClientID %>').datepicker({});
        $('#<%=txtFechaEmisionHasta.ClientID %>').datepicker({});
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Borrar Numeración
        </h3>
    </header>
    <b>Procesos de Facturación</b>
    <table>
        <tr>
            <td>
                Fecha de Emisión
            </td>
            <td>
                Desde
                <asp:TextBox ID="txtFechaEmisionDesde" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtFechaEmisionDesde" runat="server"
                    ControlToValidate="txtFechaEmisionDesde" ErrorMessage="Requerido" Display="Dynamic"
                    ValidationGroup="buscar">Requerido</asp:RequiredFieldValidator>
                <br />
                Hasta
                <asp:TextBox ID="txtFechaEmisionHasta" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtFechaEmisionHasta" runat="server"
                    ControlToValidate="txtFechaEmisionHasta" Display="Dynamic" ErrorMessage="Requerido"
                    ValidationGroup="buscar">Requerido</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"
                    ValidationGroup="buscar" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="grdFacturas" CssClass="tablesorter" runat="server" AutoGenerateColumns="False"
        Width="100%" EnableModelValidation="True" DataKeyNames="ID_FACTURACION, ID_TIPO_FACTURA"
        OnRowCommand="grdFacturas_RowCommand">
        <Columns>
            <asp:BoundField DataField="FECHA_FACTURACION" HeaderText="FECHA FACTURACION" />
            <asp:BoundField DataField="TOTAL_FACTURAS" HeaderText="TOTAL FACTURAS" />
            <asp:BoundField DataField="TOTAL_FACTURAS_POR_NUMERAR" HeaderText="FACTURAS POR NUMERAR" />
            <asp:BoundField DataField="NOMBRE_TIPO_FACTURA" HeaderText="TIPO FACTURA" />
            <asp:ButtonField CommandName="Numerar" Text="Seleccionar" />
        </Columns>
        <EmptyDataTemplate>
            No hay procesos de facturación registrados.
        </EmptyDataTemplate>
    </asp:GridView>
</article>
<asp:Panel ID="pnNumerar" runat="server" Visible="false">
    <asp:HiddenField ID="hdnID_FACTURACION" runat="server" />
    <asp:HiddenField ID="hdnID_TIPO_FACTURA" runat="server" />
    <table>
        <tr>
            <td>
                Correlativos
            </td>
            <td>
                Desde
                <asp:TextBox ID="txtDesdeN" runat="server" Columns="10" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDesdeN" runat="server"
                    ControlToValidate="txtDesdeN" ErrorMessage="Requerido" Display="Dynamic" ValidationGroup="numerar">Requerido</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator_txtDesdeN" runat="server" Type="Integer"
                    Operator="DataTypeCheck" ControlToValidate="txtDesdeN" ErrorMessage="Formato no válido"
                    Display="Dynamic" ValidationGroup="numerar">Formato no válido</asp:CompareValidator>
                <br />
                Hasta
                <asp:TextBox ID="txtHastaN" runat="server" Columns="10" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtHastaN" runat="server"
                    ControlToValidate="txtHastaN" Display="Dynamic" ErrorMessage="Requerido" ValidationGroup="numerar">Requerido</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator_" runat="server" Type="Integer" Operator="DataTypeCheck"
                    ControlToValidate="txtHastaN" ErrorMessage="Formato no válido" Display="Dynamic"
                    ValidationGroup="numerar">Formato no válido</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBorrarNumeracion" runat="server" Text="Borrar Numeración" ValidationGroup="numerar"
                    OnClick="btnBorrarNumeracion_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
