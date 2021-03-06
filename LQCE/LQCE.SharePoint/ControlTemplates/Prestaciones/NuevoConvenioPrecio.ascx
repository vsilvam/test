﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NuevoConvenioPrecio.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.NuevoConvenioPrecio" %>


<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="pnConvenios" runat="server">
    <asp:GridView ID="grdConvenios" runat="server" AutoGenerateColumns='False'
        Width='100%' EnableModelValidation="True">
        <Columns>
        <asp:TemplateField HeaderText="Seleccionar">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("ID") %>'
                        onclick="lnkSeleccionar_Click">Seleccionar</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblIdTipoPrestacion" runat="server" Text='<%# Bind("TIPO_PRESTACION.ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo Prestacion">
                <ItemTemplate>
                    <asp:Label ID="lblTipoPrestacion" runat="server" Text='<%# Bind("TIPO_PRESTACION.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se han encontrado convenios.
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="pnNuevoConvenio" runat="server" visible="false">
    <table>
        <tr>
            <td>Tipo Prestacion
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" DataTextField="NOMBRE" 
                    DataValueField="ID" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Nombre
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
        onclick="btnAceptar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
</asp:Panel>