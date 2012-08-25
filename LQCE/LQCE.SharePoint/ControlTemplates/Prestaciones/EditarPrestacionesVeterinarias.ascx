<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarPrestacionesVeterinarias.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarPrestacionesVeterinarias" %>

<asp:Panel ID="pnEditarPrestVeterinarias" runat="server">
    <table>
        <tr>
            <td>Ficha
            </td>
            <td>
                <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
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
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                Especie<span>:</span>
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
                Raza<span>:</span>
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
                Sexo<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Solicita<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
            <td>
                Telefono
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td>
                Médico<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
            </td>
            <td>
                Procedencia
            </td>
            <td>
                <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td>
                Recepción<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
            </td>
            <td>
                --
            </td>
            <td>
                <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
            </td>
            <td>hrs</td>
            <td>Toma Muestra</td>
            <td><asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>
            <td>--</td>
            <td><asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EXAMEN" HeaderText="EXAMEN" />                        
                        <asp:BoundField DataField="VALOR" HeaderText="VALOR" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Pendiente<span>:</span>
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
                Total<span>:</span>
            </td>
            <td>
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
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
    </table>
    <!--Validar, marcar error,guarda temporal  "dejar con botones" 
    En caso de error mostrar mensaje u observacions en la ficha
    -->
</asp:Panel>