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
<asp:Panel ID="pnEmitirFactura" runat="server">
    <table>
        <tr>
            <td>
                BUSCAR FACTURAS
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Periodo
            </td>
        </tr>
        <tr>
            <td>
                Desde
            </td>
            <td>
                <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
            </td>
            <td>
                Hasta
            </td>
            <td>
                <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Clientes
            </td>
            <td>
                <asp:DropDownList ID="ddlClientes" runat="server" DataTextField="NOMBRE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Calcuar facturas" 
                    onclick="btnBuscar_Click" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnFacturas" runat="server" Visible="false">
    <asp:GridView ID="grdFacturas" runat="server" AutoGenerateColumns="false" 
                    Width="100%" onrowdatabound="grdFacturas_RowDataBound">
    <Columns>
            <asp:TemplateField HeaderText="RUT">
                <ItemTemplate>
                    <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NOMBRE">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD">
                <ItemTemplate>
                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOTAL A COBRAR">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="% DESCUENTO">
                <ItemTemplate>
                    <asp:TextBox ID="txtDescuento" runat="server" Text='<%# Eval("Descuento") %>'></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron prestaciones coincidentes.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Button ID="btnEmitir" runat="server" Text="Emitir Facturas" 
        onclick="btnEmitir_Click" />
</asp:Panel>
