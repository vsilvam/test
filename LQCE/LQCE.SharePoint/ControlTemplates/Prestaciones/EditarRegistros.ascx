<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarRegistros.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarRegistros" %>

<asp:Panel ID="pnEditarRegistros" runat="server">
    <h2>
        Registros Agregados</h2>
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
        <legend>Buscador de Prestaciones</legend>
        <table>
            <tr>
                <td>Numero de Ficha</td>
                <td>
                    <asp:TextBox ID="txtNroFicha" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Nombre </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Estado de Prestacion</td>
                <td>
                    <asp:DropDownList ID="ddlEstadoPrestacion" runat="server" Height="16px" DataValueField="ID" DataTextField="NOMBRE">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Procedencia</td>
                <td>
                    <asp:TextBox ID="txtProcedencia" runat="server"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td colspan="4">Fecha Recepcion</td>
            </tr>
            <tr>
                <td>Desde</td>
                <td>
                    <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
                </td>
                <td>Hasta</td>
                <td>
                    <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdPrestaciones" runat="server" 
        AutoGenerateColumns="False" Visible="false" 
        onrowdatabound="grdPrestaciones_RowDataBound">
        <Columns>
            <asp:BoundField DataField="NUMERO_FICHA" HeaderText="N° Ficha" />
            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
            <asp:BoundField DataField="NOMBRE_ESTADO_DETALLE" HeaderText="Estado" />
            <asp:BoundField DataField="PROCEDENCIA" HeaderText="Procedencia" />
            <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="Fecha Recepcion" />
            <%--<asp:TemplateField HeaderText="Validar">
                        <ItemTemplate>
                            <asp:RadioButton ID="rbValidaSi" runat="server" Text="Si" />
                            <asp:RadioButton ID="rbValidaNo" runat="server" Text="No" />
                        </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg" CommandArgument='<%# Eval("Id") %>'
                                Height="10px" ToolTip="Editar" OnClick="imgEditar_Click" />
                        </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron Solicitudes coincidentes.
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>