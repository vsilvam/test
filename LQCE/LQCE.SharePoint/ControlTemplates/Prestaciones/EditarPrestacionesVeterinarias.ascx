﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
            <asp:LinkButton ID="lnkAgregaFicha" runat="server" onclick="lnkAgregaFicha_Click" >Agrega Fichas</asp:LinkButton>
                <asp:GridView ID="grdExamen" runat="server" AutoGenerateColumns="False" 
                    Width="100%" onrowdatabound="grdExamen_RowDataBound" 
                    EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:TextBox ID="txtId" runat="server" Text='<%# Eval("ID") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EXAMEN">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExamen" runat="server" Text='<%# Eval("NOMBRE_EXAMEN") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("VALOR_EXAMEN") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="6">
            <asp:Panel ID="pnAgregaFila" runat="server" Visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtExamen" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAgrega" runat="server" Text="Agrega Registro" 
                                onclick="btnAgrega_Click" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
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

    <asp:GridView ID="grdErrores" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="MENSAJE ERROR">
                <ItemTemplate>
                    <asp:Label ID="lblError" runat="server" Text='<%# Bind("ERRORES_VALIDACION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
    </asp:GridView>
</asp:Panel>