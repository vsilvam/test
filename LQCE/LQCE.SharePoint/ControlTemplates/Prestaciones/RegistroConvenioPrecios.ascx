﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroConvenioPrecios.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.RegistroConvenioPrecios" %>


<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="pnRegistroConvenioPrecio" runat="server">
    <asp:GridView ID="grdRegistroConvenioPrecio" runat="server" GridLines="None" AutoGenerateColumns='False'
        Width='100%' CssClass="tablesorter" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField HeaderText="Actualizar">
                <ItemTemplate>
                    <asp:CheckBox ID="ChkEditar" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />            
            <asp:TemplateField HeaderText="PRESTACION">
                <ItemTemplate>
                    <asp:Label ID="lblPrestacion" runat="server" 
                        Text='<%# Bind("TIPO_PRESTACION.NOMBRE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron clientes.
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <asp:Button ID="btnAgrega" runat="server" Text="Agrega" 
        PostBackUrl="~/_layouts/Prestaciones/NuevoConvenioPrecio.aspx" />
    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" 
        onclick="btnActualizar_Click" />
</asp:Panel>
