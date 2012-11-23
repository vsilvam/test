<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroClientes.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.RegistroClientes" %>
<%@ Register TagPrefix="uc1" TagName="Paginador" Src="Paginador1.ascx" %>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="pnIngresoClientes" runat="server">
    <h3>
        Busqueda de Clientes</h3>
    <table>
        <tr>
            <td>
                Rut Cliente
            </td>
            <td>
                <asp:TextBox ID="txtRutCliente" runat="server"></asp:TextBox>
                <asp:Label ID="lblRut" runat="server" Text="12345678-9"></asp:Label>
            </td>
            <td>
                Nombre Cliente
            </td>
            <td>
                <asp:TextBox ID="txtNombreCliente" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Región
            </td>
            <td>
                <asp:DropDownList ID="ddlRegion" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                    DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Comuna
            </td>
            <td>
                <asp:DropDownList ID="ddlComuna" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Tipo de Prestación
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" AppendDataBoundItems="True"
                    DataTextField="NOMBRE" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPrestacion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Convenio
            </td>
            <td>
                <asp:DropDownList ID="ddlConvenio" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    <asp:Button ID="btnAgregaCliente" runat="server" Text="Agregar Cliente" PostBackUrl="~/_layouts/Prestaciones/IngresarClientes.aspx" />
</asp:Panel>
<asp:Panel ID="pnClientes" runat="server" Visible="false">
    <asp:GridView ID="grdClientes" runat="server" GridLines="Both" AutoGenerateColumns='False'
        Width='100%' CssClass="tablesorter" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Id" />
            <asp:BoundField DataField="RUT" HeaderText="Rut" />
            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
            <asp:TemplateField HeaderText="Region">
                <ItemTemplate>
                    <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("COMUNA.REGION.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comuna">
                <ItemTemplate>
                    <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("COMUNA.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo de Prestación">
                <ItemTemplate>
                    <asp:Label ID="lblTipoPrestacion" runat="server" Text='<%# Bind("TIPO_PRESTACION.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Convenio">
                <ItemTemplate>
                    <asp:Label ID="lblConvenio" runat="server" Text='<%# Bind("CONVENIO.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo de Factura">
                <ItemTemplate>
                    <asp:Label ID="lblTipoFactura" runat="server" Text='<%# Bind("TIPO_FACTURA.NOMBRE_FACTURA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actualizar">
                <ItemTemplate>
                    <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                        CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Actualizar" OnClick="imgActualizar_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                        CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminar_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron clientes.
        </EmptyDataTemplate>
    </asp:GridView>
    <uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged" />
</asp:Panel>
