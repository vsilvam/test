<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActualizarClientes.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.ActualizarClientes" %>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<h3>
    Actualizar Clientes</h3>
<table width="100%">
    <tr>
        <td style="width: 150px">
            Rut
        </td>
        <td class="style1">
            <asp:TextBox ID="txtRut" runat="server" Columns="20" MaxLength="50"></asp:TextBox>
            12345678-9
            <asp:RequiredFieldValidator ID="Required_txtRut" runat="server" ControlToValidate="txtRut"
                ValidationGroup="cliente" Display="Dynamic" ForeColor="Red" ErrorMessage="Requerido">Requerido.</asp:RequiredFieldValidator>
        </td>
        <td>
            Telefono
        </td>
        <td class="style2">
            <asp:TextBox ID="txtFono" runat="server" Columns="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Nombre
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtNombre" runat="server" Columns="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID='Required_txtNombre' runat='server' ControlToValidate='txtNombre'
                ValidationGroup="cliente" Display='Dynamic' ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Dirección
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtDireccion" runat="server" Columns="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID='RequiredFieldValidator_txtNombre' runat='server'
                ControlToValidate='txtDireccion' ValidationGroup="cliente" Display='Dynamic'
                ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Giro
        </td>
        <td class="style1">
            <asp:TextBox ID="txtGiro" runat="server" Columns="20"></asp:TextBox>
        </td>
        <td>
            Descuento
        </td>
        <td class="style2">
            <asp:TextBox ID="txtDescuento" runat="server" Columns="10" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID='RequiredFieldValidator_txtDescuento' runat='server'
                ControlToValidate='txtDescuento' ValidationGroup="cliente" Display='Dynamic'
                ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="compareDescuento" runat="server" ControlToValidate="txtDescuento"
                Type="Integer" Operator="DataTypeCheck" ValidationGroup="cliente" Display="Dynamic"
                ForeColor="Red" ErrorMessage="Formato no válido">Formato no válido</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            Region
        </td>
        <td class="style1">
            <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            Comuna
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlComuna" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                AppendDataBoundItems="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID='RequiredFieldValidator_Comuna' runat='server' ControlToValidate='ddlComuna'
                ValidationGroup="cliente" Display='Dynamic' ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Tipo de Prestación
        </td>
        <td class="style1">
            <asp:DropDownList ID="ddlTipoPrestacion" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPrestacion_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID='Required_TipoPrestacion' runat='server' ControlToValidate='ddlTipoPrestacion'
                ValidationGroup="cliente" Display='Dynamic' ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
        <td>
            Convenio
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlConvenio" runat="server" DataTextField="NOMBRE" DataValueField="ID"
                AppendDataBoundItems="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID='RequiredFieldValidator_Convenio' runat='server' ControlToValidate='ddlConvenio'
                ValidationGroup="cliente" Display='Dynamic' ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Tipo de Factura
        </td>
        <td class="style1">
            <asp:DropDownList ID="ddlTipoFactura" runat="server" DataTextField="NOMBRE_FACTURA"
                DataValueField="ID" AppendDataBoundItems="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID='RequiredFieldValidator_TipoFactura' runat='server'
                ControlToValidate='ddlTipoFactura' ValidationGroup="cliente" Display='Dynamic'
                ForeColor='red' ErrorMessage='Requerido'>Requerido.</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<h3>
    Sinónimo</h3>
<table>
    <tr>
        <td>
            Ingrese sinonimo para el cliente
        </td>
        <td>
            <asp:TextBox ID="txtSinonimo" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="requiredSinonimo" runat="server" ControlToValidate="txtSinonimo"
                ValidationGroup="sinonimo" ErrorMessage="Requerido" Text="Requerido" ForeColor="Red">Requerido</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btnAgrega" runat="server" Text="Agregar Sinonimo" ValidationGroup="sinonimo"
                OnClick="btnAgrega_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grdSinonimoCliente" runat="server" AutoGenerateColumns="false"
                DataKeyNames="ID" Width="100%" OnRowCommand="grdSinonimoCliente_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Id" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Sinonimo" />
                    <asp:ButtonField CommandName="Eliminar" Text="Eliminar" />
                </Columns>
                <EmptyDataTemplate>
                    El cliente no tiene sinónimos registrados.
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click"
    ValidationGroup="cliente" />
<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false"
    PostBackUrl="~/_layouts/Prestaciones/RegistroClientes.aspx" />
