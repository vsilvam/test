﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
            Emitir Nota de Crédito</h3>
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
    <asp:GridView ID="gridFacturacion" CssClass="tablesorter" runat="server" AutoGenerateColumns="False"
        Width="100%" EnableModelValidation="True" DataKeyNames="ID_FACTURACION, ID_TIPO_FACTURA"
        OnRowCommand="gridFacturacion_RowCommand">
        <Columns>
            <asp:BoundField DataField="FECHA_FACTURACION" HeaderText="FECHA FACTURACION" />
            <asp:BoundField DataField="TOTAL_FACTURAS" HeaderText="TOTAL FACTURAS" />
            <asp:BoundField DataField="TOTAL_FACTURAS_POR_NUMERAR" HeaderText="FACTURAS POR NUMERAR" />
            <asp:BoundField DataField="NOMBRE_TIPO_FACTURA" HeaderText="TIPO FACTURA" />
            <asp:ButtonField CommandName="Seleccionar" Text="Ver Facturas" />
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
        DataKeyNames="ID_FACTURA" EnableModelValidation="True" 
        OnRowCommand="grdFacturas_RowCommand" onrowdatabound="grdFacturas_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Número Factura" DataField="NUMERO_FACTURA" />
            <asp:BoundField HeaderText="RUT" DataField="RUT_CLIENTE" />
            <asp:BoundField HeaderText="Cliente" DataField="NOMBRE_CLIENTE" />
            <asp:BoundField HeaderText="Fecha de Emisión" DataField="FECHA_EMISION" />
            <asp:BoundField HeaderText="Total" DataField="VALOR_TOTAL" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="linkSeleccionar" runat="server" CausesValidation="false" 
                        CommandName="Seleccionar" Text="Seleccionar"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No hay facturas numeradas.
        </EmptyDataTemplate>
    </asp:GridView>
    <uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged" />
</asp:Panel>
<asp:Panel ID="panelNota" runat="server" Visible="false">
    <p>
        <b>Emisión de Nota de Crédito</b></p>
    <table>
        <tr>
            <td>
                Número de Factura
            </td>
            <td>
                <asp:HiddenField ID="hdnIdFactura" runat="server" />
                <asp:Label ID="lblNumeroFactura" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Número de Nota de Crédito
            </td>
            <td>
                <asp:TextBox ID="txtNumeroNotaCredito" runat="server" Text="10" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredtxtNumeroNotaCredito" runat="server" ControlToValidate="txtNumeroNotaCredito"
                    Text="Requerido" ValidationGroup="nota" Display="Dynamic">Requerido</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comparetxtNumeroNotaCredito" runat="server" ControlToValidate="txtNumeroNotaCredito"
                    ValidationGroup="nota" Display="Dynamic" Text="Formato no válido" Type="Integer"
                    Operator="DataTypeCheck">Formato no válido</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                Corrección Total o Parcial
            </td>
            <td>
                <asp:RadioButtonList ID="radioCorreccionTotal" runat="server">
                    <asp:ListItem Text="Total" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Parcial" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnEmitir" runat="server" Text="Emitir Nota de Crédito" ValidationGroup="nota"
        OnClick="btnEmitir_Click" />
</asp:Panel>
