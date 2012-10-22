<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroPago.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.RegistroPago" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css"
    type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtFechaPago.ClientID %>').datepicker({});
    });
</script>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="pnBuscarPagos" runat="server" Visible="false">
    <table width="100%">
        <tr>
            <td>
                Buscar Facturas Pendientes
            </td>
            <td>
                <asp:DropDownList ID="ddlFacturas" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlFacturas_SelectedIndexChanged" DataTextField="NOMBRE_CLIENTE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnRegistroPagos" runat="server" Visible="false">
    <table width="100%">
        <tr>
            <td>
                Prestaciones a Pagar
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grdPrestacionesPendientes" runat="server" GridLines="None" AutoGenerateColumns='False'
                    Width='100%' CssClass="tablesorter" EnableModelValidation="True" DataKeyNames="PRESTACION.ID">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSeleccionar" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPrestacion" runat="server" Text='<%# Bind("PRESTACION.ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRESTACION">
                            <ItemTemplate>
                                <asp:Label ID="lblPrestacion" runat="server" Text='<%# Bind("PRESTACION.FECHA_RECEPCION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron prestaciones pendientes de pago.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Fecha de Pago
            </td>
            <td>
                <asp:TextBox ID="txtFechaPago" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Medio de Pago
            </td>
            <td>
                <asp:TextBox ID="txtMedioPago" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Observaciones
            </td>
            <td>
                <asp:TextBox ID="txtObservaciones" runat="server" Width='500px' Height='80px' TextMode='MultiLine'></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    <input type="button" value="Volver" onclick="javascript:history.back(1)" />
</asp:Panel>
