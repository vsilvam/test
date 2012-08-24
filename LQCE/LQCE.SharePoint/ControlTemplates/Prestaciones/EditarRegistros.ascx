<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarRegistros.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarRegistros" %>

<asp:Panel ID="pnEditarRegistros" runat="server">
    <h2>
        Registros Agregados</h2>
        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField=" " HeaderText="Nombre" />
            <asp:BoundField DataField=" " HeaderText="Tipo Prestacion" />
            <asp:BoundField DataField=" " HeaderText="Examen" />
            <asp:BoundField DataField=" " HeaderText="Valor" />
            <asp:TemplateField HeaderText="Validar">
                        <ItemTemplate>
                            <asp:RadioButton ID="rbValidaSi" runat="server" Text="Si" />
                            <asp:RadioButton ID="rbValidaNo" runat="server" Text="No" />
                        </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg" CommandArgument='<%# Eval("Id") %>'
                                Height="10px" ToolTip="Editar" OnClick="imgEditar_Click" />
                        </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>