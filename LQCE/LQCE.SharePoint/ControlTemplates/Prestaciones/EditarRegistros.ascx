﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditarRegistros.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EditarRegistros" %>
<%@ Register TagPrefix="uc1" TagName="Paginador" Src="Paginador1.ascx" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<article class="module width_full">

 <header><h3>Buscador de Prestaciones</h3></header>

    <div class="module_content">
            
      <table width="100%">
        <tr>
            <td width="20%">
                Numero de Ficha
                </td><td><span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtNroFicha" runat="server"></asp:TextBox>
            </td>
             <td width="20%">
                Nombre</td><td><span>:</span>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
           <td width="20%">
                Estado de Prestacion</td><td><span>:</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoPrestacion" runat="server" Height="16px" DataValueField="ID"
                    DataTextField="NOMBRE" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
       
            <td width="20%">
                Procedencia</td><td><span>:</span>
            </td>
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
              
    </table>

    </div>
   
   <footer>
   <div class="submit_link">

    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />

   </div>
   </footer>

 </article>

 <article class="module width_full">
 <header><h3>Prestaciones</h3></header>
  
  <div> 
  <asp:GridView ID="grdPrestaciones" CssClass="tablesorter" runat="server" GridLines="Both" AutoGenerateColumns="False" Visible="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ID_TIPO_PRESTACION" HeaderText="Tipo Prestacion" Visible="false" />
                        <asp:BoundField DataField="NUMERO_FICHA" HeaderText="N° Ficha" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="NOMBRE_ESTADO_DETALLE" HeaderText="Estado" />
                        <asp:BoundField DataField="PROCEDENCIA" HeaderText="Procedencia" />
                        <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="Fecha Recepcion" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                                    CommandArgument='<%# Eval("ID") %>' Height="20px" ToolTip="Editar" OnClick="imgEditar_Click"
                                    CommandName='<%# Eval("ID_TIPO_PRESTACION") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron Solicitudes coincidentes.
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="head" />
                </asp:GridView>
 </div>
 
 <footer>
  <div>
    <uc1:Paginador ID="Paginador1" runat="server" OnPageChanged="Paginador1_PageChanged" />
</div>
 </footer>
 </article>
