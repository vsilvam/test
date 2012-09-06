<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paginador1.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.UserControl.Paginador1" %>


<asp:Panel ID="panelPaginador" runat="server">
    <table width="100%">
        <tr>
            <td>
                Mostrar Página
                <asp:DropDownList ID="ddlPageNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageNumber_SelectedIndexChanged">
                </asp:DropDownList>
                de
                <asp:Label ID="lblShowRecords" runat="server"></asp:Label>
                Páginas
            </td>
            <td>
                <asp:LinkButton ID="linkAnterior" runat="server" onclick="linkAnterior_Click">Anterior</asp:LinkButton>
                &nbsp;
                &nbsp;
                <asp:LinkButton ID="linkSiguiente" runat="server" onclick="linkSiguiente_Click">Siguiente</asp:LinkButton>
            </td>
            <td>
                Mostrar
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                </asp:DropDownList>
                Registros por Página
            </td>
        </tr>
    </table>
</asp:Panel>