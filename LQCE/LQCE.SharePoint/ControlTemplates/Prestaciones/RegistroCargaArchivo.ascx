﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
        <td>Tipo de Prestacion</td>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" DataTextField="NOMBRE" 
                    DataValueField="ID">
                    <asp:ListItem Value="">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Pendiente</asp:ListItem>
                    <asp:ListItem Value="2">Aprobada</asp:ListItem>
                    <asp:ListItem Value="3">Eliminada</asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td>Estado</td>
                <td><asp:DropDownList ID="ddlEstado" runat="server" DataTextField="NOMBRE" 
                        DataValueField="ID">
                    <asp:ListItem Value="">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Pendiente</asp:ListItem>
                    <asp:ListItem Value="2">Completado</asp:ListItem>
                    <asp:ListItem Value="3">Eliminado</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr>
            <td colspan="2">
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
    <asp:GridView ID="gridRegistroCargaArchivo" runat="server" 
        AutoGenerateColumns="False" 
        onrowdatabound="gridRegistroCargaArchivo_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Modificar Estado">
                <ItemTemplate>
                    <asp:CheckBox ID="ChkEditar" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="Id" />
            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre Archivo" />
            <asp:BoundField DataField="FECHA_CARGA" HeaderText="Fecha Carga" />
            <asp:BoundField DataField="TOTAL_REGISTROS" HeaderText="N° Total Fichas" />            
            <asp:BoundField DataField="REGISTROS_VALIDADOS" HeaderText="Fichas Validadas" />
            <asp:BoundField DataField="REGISTROS_CON_ERRORES" HeaderText="Fichas con errores" />
            <asp:BoundField DataField="NOMBRE_TIPO_PRESTACION" HeaderText="Tipo Prestacion" />
            <asp:BoundField DataField="NOMBRE_ESTADO" HeaderText="Estado" />
            
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                        CommandArgument='<%# Eval("Id") %>' Height="10px" ToolTip="Editar" OnClick="imgEditar_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnEliminarCarga" runat="server" Text="Eliminar Carga" 
        onclick="btnEliminarCarga_Click" />
    <asp:Button ID="btnCompletarRevision" runat="server" Text="Completar Revision" 
        onclick="btnCompletarRevision_Click"  />
</asp:Panel>
