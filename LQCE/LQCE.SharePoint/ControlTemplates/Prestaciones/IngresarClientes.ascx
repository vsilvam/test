<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IngresarClientes.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.IngresarClientes" %>
<style type="text/css">
    .style1
    {
        width: 230px;
    }
    .style2
    {
        width: 509px;
    }
</style>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<asp:Panel ID="pnIngresoClientes" runat="server">
    <h3>
        Ingreso Clientes</h3>
    <table width="80%">
        <tr>
            <td style="width: 150px">
                Rut
            </td>
            <td class="style1">
                <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
                12345678-9
                <asp:RequiredFieldValidator ID='RequiredFieldValidator' runat='server' ControlToValidate='txtRut' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
            <td>
                Telefono
            </td>
            <td class="style2">
                <asp:TextBox ID="txtFono" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Nombre Comercial
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNombre" runat="server" Width="80%"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='txtNombre' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Razón Social
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRazonSocial" runat="server" Width="80%"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator2' runat='server' ControlToValidate='txtRazonSocial' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Direccion Comercial
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDireccion" runat="server" Width="80%"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator3' runat='server' ControlToValidate='txtDireccion' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Direccion Entrega
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDireccionEntrega" runat="server" Width="80%"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator4' runat='server' ControlToValidate='txtDireccionEntrega' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Region
            </td>
            <td class="style1">
                <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                    AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator5' runat='server' ControlToValidate='ddlRegion' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
            <td>
                Comuna
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlComuna" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                    AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator6' runat='server' ControlToValidate='ddlComuna' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Ciudad
            </td>
            <td class="style1">
                <asp:TextBox ID="txtCiudad" runat="server" Width="80%"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator7' runat='server' ControlToValidate='txtCiudad' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Giro
            </td>
            <td class="style1">
                <asp:TextBox ID="txtGiro" runat="server" Width="80%"></asp:TextBox>
            </td>
            <td>
                Tipo Facturación
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlTipoFacturacion" runat="server" DataTextField="NOMBRE_FACTURA"
                    DataValueField="ID" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator8' runat='server' ControlToValidate='ddlTipoFacturacion' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Convenio
            </td>
            <td class="style1">
                <asp:DropDownList ID="ddlConvenio" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                    AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator9' runat='server' ControlToValidate='ddlConvenio' ValidationGroup="Ingreso"
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
            <td>
                Descuento
            </td>
            <td class="style2">
                <asp:TextBox ID="txtDescuento" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID='RequiredFieldValidator10' runat='server' ControlToValidate='txtDescuento' ValidationGroup="Ingreso" 
                    Display='Dynamic' Font-Size='7pt' ForeColor='red' ErrorMessage='RequiredFieldValidator'>Requerido.</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
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
                <asp:Button ID="btnAgrega" runat="server" Text="Agregar Sinonimo" 
                    onclick="btnAgrega_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdSinonimoCliente" runat="server" 
                    AutoGenerateColumns="False" EnableModelValidation="True">
                    <Columns>                        
                        <asp:TemplateField HeaderText="Sinonimo">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreSinonimo" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron clientes.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>
    <asp:Button ID="btnIngreso" runat="server" Text="Ingresar" OnClick="btnIngreso_Click"
        ValidationGroup="Ingreso" />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
