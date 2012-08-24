<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroCargaArchivo.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.RegistroCargaArchivo" %>
<asp:Panel ID="pnRegistroCargaArchivo" runat="server">
    <h2>
        Despliegue de Archivos Ingresados
    </h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlEstado" runat="server">
                    <asp:ListItem Value="">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Pendiente</asp:ListItem>
                    <asp:ListItem Value="2">Aprobada</asp:ListItem>
                    <asp:ListItem Value="3">Eliminada</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            </td>
        </tr>
    </table>
    <%--<p>
        El archivo subido con el nombre
        <asp:Label ID="lblNombreArchivo" runat="server"></asp:Label>
        tiene
        <asp:Label ID="lblCantidadRegistros" runat="server"></asp:Label>
        registros.</p>--%>
    <asp:GridView ID="gridPrevia" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre Archivo" />
            <asp:BoundField DataField="FECHA_CARGA" HeaderText="Fecha Carga" />
            <asp:BoundField DataField=" " HeaderText="N° Total Filas" />
            <asp:BoundField DataField=" " HeaderText="N° filas con Observaciones" />
            <asp:BoundField DataField=" " HeaderText="Filas Validadas" />
            <asp:BoundField DataField=" " HeaderText="Filas con errores" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="1">Pendiente</asp:ListItem>
                        <asp:ListItem Value="2">Aprobada</asp:ListItem>
                        <asp:ListItem Value="3">Eliminada</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                        CommandArgument='<%# Eval("Id") %>' Height="10px" ToolTip="Editar" OnClick="imgEditar_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>
