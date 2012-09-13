<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmitirFactura.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EmitirFactura" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtDesde.ClientID %>').datepicker({});
        $('#<%=txtHasta.ClientID %>').datepicker({});
    });
</script>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Buscar Facturas</h3>
    </header>
    <div class="module_content">
        <h4>
            Periodo</h4>
        <table>
            <tr>
                <td>
                    Desde
                </td>
                <td>
                    <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredDesde" runat="server" ValidationGroup="buscar" ControlToValidate="txtDesde" Text="Requerido" ErrorMessage="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
                <td>
                    Hasta
                </td>
                <td>
                    <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtHasta" runat="server" ValidationGroup="buscar" ControlToValidate="txtHasta" Text="Requerido" ErrorMessage="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Clientes
                </td>
                <td>
                    <asp:DropDownList ID="ddlClientes" runat="server" DataTextField="NOMBRE" DataValueField="ID" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <footer>
        <div class="submit_link">
            <asp:Button ID="btnBuscar" runat="server" Text="Calcular facturas" OnClick="btnBuscar_Click" ValidationGroup="buscar" />
        </div>
    </footer>
</article>
<asp:Panel ID="pnFacturas" runat="server" Visible="false">
    <article class="module width_full">
        <header>
            <h3>
                Facturas</h3>
        </header>
        <asp:HiddenField ID="hdnFechaDesde" runat="server" />
        <asp:HiddenField ID="hdnFechaHasta" runat="server" />

        <asp:GridView ID="grdFacturas" CssClass="tablesorter" GridLines="None" runat="server"
            AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdFacturas_RowDataBound"
            EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT" />
                <asp:BoundField DataField="NOMBRE_CLIENTE" HeaderText="NOMBRE" />
                <asp:BoundField DataField="CANTIDAD_PRESTACIONES" HeaderText="CANTIDAD" />
                <asp:BoundField DataField="TOTAL_PRESTACIONES" HeaderText="TOTAL A COBRAR" />
                <asp:TemplateField HeaderText="% DESCUENTO">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ID_CLIENTE") %>'></asp:HiddenField>
                        <asp:TextBox ID="txtDescuento" runat="server" Text='<%# Eval("DESCUENTO") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No se encontraron prestaciones coincidentes.
            </EmptyDataTemplate>
        </asp:GridView>
        <footer>
            <div class="submit_link">
                <asp:Button ID="btnEmitir" runat="server" Text="Emitir Facturas" OnClick="btnEmitir_Click" />                
            </div>
        </footer>
    </article>
</asp:Panel>
