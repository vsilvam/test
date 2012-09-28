<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IngresarClientes.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.IngresarClientes" %>


<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:panel ID="pnIngresoClientes" runat="server">
    <h3>Ingreso Clientes</h3>
    <table>
        <tr>
            <td>Rut</td>
            <td>
                <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Direccion</td>
            <td>
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Fono</td>
            <td>
                <asp:TextBox ID="txtFono" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Giro</td>
            <td>
                <asp:TextBox ID="txtGiro" runat="server"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td>Convenio</td>
            <td>
                <asp:TextBox ID="txtConvenio" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Comuna</td>
            <td>
                <asp:TextBox ID="txtComuna" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Descuento</td>
            <td>
                <asp:TextBox ID="txtDescuento" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnIngreso" runat="server" Text="Ingresar" 
        onclick="btnIngreso_Click" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" />
</asp:panel>

