<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmitirNotaCredito.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EmitirNotaCredito" %>
<%@ Register TagPrefix="uc1" TagName="Paginador" Src="Paginador1.ascx" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css"
    type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Emitir Nota de Crédito</h3>
    </header>
    <b>Procesos de Facturación</b>
    <asp:GridView ID="gridFacturacion" CssClass="tablesorter" runat="server" AutoGenerateColumns="False"
        Width="100%" EnableModelValidation="True" DataKeyNames="ID_FACTURACION, ID_TIPO_FACTURA"
        OnRowCommand="gridFacturacion_RowCommand">
        <Columns>
            <asp:BoundField DataField="FECHA_FACTURACION" HeaderText="FECHA FACTURACION" />
            <asp:BoundField DataField="TOTAL_FACTURAS" HeaderText="TOTAL FACTURAS" />
            <asp:BoundField DataField="TOTAL_FACTURAS_POR_NUMERAR" HeaderText="FACTURAS POR NUMERAR" />
            <asp:BoundField DataField="NOMBRE_TIPO_FACTURA" HeaderText="TIPO FACTURA" />
            <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
        </Columns>
        <EmptyDataTemplate>
            No hay procesos de facturación pendientes.
        </EmptyDataTemplate>
    </asp:GridView>
</article>
<asp:Panel ID="panelEmitir" runat="server" Visible="false">
    <p>
        <b>Facturas Numeradas</b></p>
    <asp:HiddenField ID="hdnID_FACTURACION" runat="server" />
    <asp:HiddenField ID="hdnID_TIPO_FACTURA" runat="server" />
    <asp:GridView ID="grdFacturas" runat="server" AutoGenerateColumns='False' Width='100%'
        DataKeyNames="ID_FACTURA" EnableModelValidation="True" OnRowDataBound="grdFacturas_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Número Factura" DataField="NUMERO_FACTURA" />
            <asp:BoundField HeaderText="RUT" DataField="RUT_CLIENTE" />
            <asp:BoundField HeaderText="Cliente" DataField="NOMBRE_CLIENTE" />
            <asp:BoundField HeaderText="Fecha de Emisión" DataField="FECHA_EMISION" />
            <asp:BoundField HeaderText="Total" DataField="VALOR_TOTAL" />
        </Columns>
        <EmptyDataTemplate>
            No hay facturas numeradas.
        </EmptyDataTemplate>
    </asp:GridView>
    <uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged" />
    <asp:Button ID="btnEmitir" runat="server" Text="Emitir Nota de Crédito" OnClick="btnEmitir_Click" />
</asp:Panel>
