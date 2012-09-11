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
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">

<header><h3>Buscar Facturas</h3></header>

 <div class="module_content">
    
    <h4>Periodo</h4>

    <table>
       
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
        
    </table>
    
    </div>
    <footer>

    <div class="submit_link">    
    <asp:Button ID="btnBuscar" runat="server" Text="Calcular facturas" OnClick="btnBuscar_Click" />
    </div>

    </footer>
</article>
<asp:Panel ID="pnFacturas" runat="server" Visible="false">
    <article class="module width_full">

<header><h3>Facturas</h3></header>

    <asp:GridView ID="grdFacturas"  CssClass="tablesorter" GridLines="None" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdFacturas_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="RUT">
                <ItemTemplate>
                    <asp:Label ID="lblRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NOMBRE">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NOMBRE_CLIENTE") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CANTIDAD">
                <ItemTemplate>
                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("CANTIDAD_PRESTACIONES") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOTAL A COBRAR">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("TOTAL_PRESTACIONES") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="% DESCUENTO">
                <ItemTemplate>
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
