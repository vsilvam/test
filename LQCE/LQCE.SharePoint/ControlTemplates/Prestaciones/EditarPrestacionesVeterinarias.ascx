<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarPrestacionesVeterinarias.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarPrestacionesVeterinarias" %>

<asp:Panel ID="pnEditarPrestVeterinarias" runat="server">
<h2>Edicion Fichas Veterinarias</h2>
<asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td>Ficha
            </td>
            <td>
                <asp:TextBox ID="txtFicha" runat="server" Enabled="false"></asp:TextBox>
            </td>
            
            <td colspan="4">
                N° <span>:</span>
            </td>
            <td>
                <asp:Label ID="lblNroPrestacion" runat="server" Text="147958"></asp:Label>
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
                Especie<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtEspecie" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Raza<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRaza" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Edad<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtEdad" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Sexo<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtSexo" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Solicita<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtSolicita" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Telefono
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="false"></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td>
                Médico<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtMedico" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Procedencia
            </td>
            <td>
                <asp:TextBox ID="txtProcedencia" runat="server" Enabled="false"></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td>
                Recepción<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRecepcion" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td style="margin-left: 40px">
                <asp:TextBox ID="txtHoraRecepcion" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>hrs</td>
            <td>Toma Muestra</td>
            <td><asp:TextBox ID="txtMuestraFecha" runat="server" Enabled="false"></asp:TextBox></td>
            <td>--</td>
            <td><asp:TextBox ID="txtMuestraHora" runat="server" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:GridView ID="grdExamen" runat="server" AutoGenerateColumns="false" 
                    Width="100%" onrowdatabound="grdExamen_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NOMBRE_EXAMEN" HeaderText="EXAMEN" />                        
                        <asp:BoundField DataField="VALOR_EXAMEN" HeaderText="VALOR" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Pendiente<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtPendiente" runat="server" Enabled="false"></asp:TextBox>
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
                Total<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                Fecha y Hora entrega de resultados
            </td>
            <td>
                <asp:TextBox ID="txtFechaEntrega" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="txtHoraEntrega" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Recepcion<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtRecepcionEntrega" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>        
    </table>
    <!--Validar, marcar error,guarda temporal  "dejar con botones" 
    En caso de error mostrar mensaje u observacions en la ficha
    -->
    <asp:Button ID="btnValidado" runat="server" Text="Validado" 
        onclick="btnValidado_Click" />
</asp:Panel>