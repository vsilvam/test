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
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                RUT<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
            <td>
                Edad<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td>
                Telefono<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha/Hora<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
            <td>
                hr
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EXAMEN" HeaderText="EXAMEN" />
                        <asp:BoundField DataField="CODIGO" HeaderText="CODIGO" />
                        <asp:BoundField DataField="VALOR" HeaderText="VALOR" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Prevision<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
            <td>
                Pagado<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Garantia<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
            </td>
            <td>
                Pendiente<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Fecha y Hora entrega de resultados
            </td>
            <td>
                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
            </td>
            <td>
                Total<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ficha<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
            </td>
            <td>
                -Servicio<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
            </td>
            <td>
                -Diag<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
            </td>
            <td>
                Listo
            </td>
            <td>
                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
            </td>
            <td>
                Recepcion<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Panel>
