<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarPrestacionesHumanas.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarPrestacionesHumanas" %>
<asp:Panel ID="pnEditarPrestPersonas" runat="server">
<h2>Edición de Fichas Personas</h2>
<asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td colspan="4">
                N° <span>:</span>
            </td>
            <td>
                <asp:Label ID="lblNroPrestacion" runat="server" Text="147958"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Nombre<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                RUT<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRut" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Medico<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtMedico" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Edad<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtEdad" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Telefono<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha/Hora<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtFechaHora1" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="txtFechaHora2" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                hr
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="grdExamen" runat="server" AutoGenerateColumns="false" 
                    Width="100%" onrowdatabound="grdExamen_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NOMBRE_EXAMEN" HeaderText="EXAMEN" />
                        <asp:BoundField DataField="ID" HeaderText="CODIGO" />
                        <asp:BoundField DataField="VALOR_EXAMEN" HeaderText="VALOR" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Prevision<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPrevision" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Pagado<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPagado" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Garantia<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtGarantia" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Pendiente<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPendiente" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha y Hora entrega de resultados
            </td>
            <td>
                <asp:TextBox ID="txtFechaHoraEntrega1" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="txtFechaHoraEntrega2" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Total<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ficha<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtFicha" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                -Servicio<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtServicio" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                -Diag<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtDiagostico" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Listo
            </td>
            <td>
                <asp:TextBox ID="txtListo" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Recepcion<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnValidado" runat="server" Text="Validado" 
        onclick="btnValidado_Click" />
</asp:Panel>
