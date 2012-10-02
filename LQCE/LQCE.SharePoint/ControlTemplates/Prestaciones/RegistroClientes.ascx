<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroClientes.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.RegistroClientes" %>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:panel ID="pnIngresoClientes" runat="server">
    <h3>Busqueda de Clientes</h3>
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
            <td>Comuna</td>
            <td>
                <asp:DropDownList ID="ddlComuna" runat="server" AppendDataBoundItems="True" 
                    DataTextField="NOMBRE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Convenio Asignado</td>
            <td>
                <asp:DropDownList ID="ddlConvenio" runat="server" AppendDataBoundItems="True" 
                    DataTextField="NOMBRE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
        onclick="btnBuscar_Click" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
        onclick="btnLimpiar_Click" />
        <asp:Button ID="btnAgregaCliente" runat="server" Text="Agregar Cliente" PostBackUrl="~/_layouts/Prestaciones/IngresarClientes.aspx"/>

</asp:panel>
<asp:Panel ID="pnClientes" runat="server" Visible="false">
    <asp:GridView ID="grdClientes" runat="server" GridLines="None" AutoGenerateColumns='false'
        Width='100%' CssClass="tablesorter">
        <Columns>
            <asp:BoundField DataField="" HeaderText="RUT" />
            <asp:BoundField DataField="" HeaderText="NOMBRE" />
            <asp:BoundField DataField="" HeaderText="COMUNA" />
            <asp:BoundField DataField="" HeaderText="CONVENIO" />
            <asp:TemplateField HeaderText="ACTUALIZAR">
                <ItemTemplate>
                    <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                        CommandArgument='<%# Eval("Id") %>' Height="10px" ToolTip="Actualizar" 
                        onclick="imgActualizar_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron clientes.
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>