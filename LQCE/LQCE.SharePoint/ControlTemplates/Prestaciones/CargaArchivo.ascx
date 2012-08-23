<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargaArchivo.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.CargaArchivo" %>

<h1>
    Carga Masiva
</h1>
<asp:Panel ID="pnCargaMasiva" runat="server">
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" 
                    onselectedindexchanged="ddlTipoPrestacion_SelectedIndexChanged">
                    <asp:ListItem Value="0">Seleccione Tipo Prestacion</asp:ListItem>
                    <asp:ListItem Value="1">Humano</asp:ListItem>
                    <asp:ListItem Value="2">Veterinario</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel>