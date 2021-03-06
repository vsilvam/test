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
<%--<%@ Register TagPrefix="uc1" TagName="Paginador" Src="~/ControlTemplates/UserControl/Paginador1.ascx" %>--%>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Buscador</h3>
    </header>
    <table>
        <tr>
            <td>
                Tipo de Prestacion
            </td>
            <td>
                <span>:</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" AppendDataBoundItems="true"
                    DataTextField="NOMBRE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Estado
            </td>
            <td>
                <span>:</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" DataTextField="NOMBRE"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <footer>
        <div class="submit_link">
            <asp:Button ID="Button3" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>
    </footer>
</article>
<article class="module width_full">
    <header>
        <h3>
            Listado de Cargas Masivas</h3>
    </header>
    <div>
        <asp:GridView CssClass="tablesorter" ID="gridRegistroCargaArchivo" GridLines="Both"
            runat="server" AutoGenerateColumns="False" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="Modificar Estado">
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkEditar" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ARCHIVO" HeaderText="Archivo" />
                <asp:BoundField DataField="FECHA_CARGA" HeaderText="Fecha de Carga" />
                <asp:BoundField DataField="TOTAL_REGISTROS" HeaderText="Total Fichas" />
                <asp:BoundField DataField="REGISTROS_VALIDADOS" HeaderText="Fichas Validadas" />
                <asp:BoundField DataField="REGISTROS_CON_ERRORES" HeaderText="Fichas con errores" />
                <asp:BoundField DataField="NOMBRE_TIPO_PRESTACION" HeaderText="Tipo Prestacion" />
                <asp:BoundField DataField="NOMBRE_ESTADO" HeaderText="Estado" />
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ID") %>' />
                        <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="10px" ToolTip="Editar" OnClick="imgEditar_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No se encontraron prestaciones coincidentes.
            </EmptyDataTemplate>
            <HeaderStyle CssClass="head" />
            <RowStyle CssClass="row" />
        </asp:GridView>
        <%--<uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged"/>--%>
    </div>
    <footer>
        <div class="submit_link">
            <asp:Button ID="Button1" runat="server" Text="Eliminar Carga" OnClick="btnEliminarCarga_Click" />
            <asp:Button ID="Button2" runat="server" Text="Completar Revision" OnClick="btnCompletarRevision_Click" />
        </div>
    </footer>
</article>
