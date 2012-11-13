<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActualizarClientes.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.ActualizarClientes" %>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<p>
    ñ</p>
<asp:Panel ID="pnSinonimoCliente" runat="server">
    <table>
        <tr>
            <td>
                Ingrese sinonimo para el cliente
            </td>
            <td>
                <asp:TextBox ID="txtSinonimo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Agregar Sinonimo" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdSinonimoCliente" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Sinonimo" />
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron clientes.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Button ID="btnActualizar" runat="server" Text="Actualizar" onclick="btnActualizar_Click" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
        onclick="btnLimpiar_Click" />
