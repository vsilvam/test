<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmitirNotaCobro.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.EmitirNotaCobro" %>

<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=txtDesde.ClientID %>').datepicker({});
        $('#<%=txtHasta.ClientID %>').datepicker({});
    });
</script>

<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<article class="module width_full">

<header><h3>Buscar Notas de Cobro</h3></header>

 <div class="module_content">

    <h4>Periodo</h4>

    <table>
      
        <tr>
            <td>
                Desde
            </td>
            <td>
                <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
            </td>
            <td>
                Hasta
            </td>
            <td>
                <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Clientes
            </td>
            <td>
                <asp:DropDownList ID="ddlClientes" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Tipo Nota de Cobro
            </td>
            <td>
                <asp:DropDownList ID="ddlNotaCobro" runat="server" DataTextField='NOMBRE' DataValueField='ID'>
                </asp:DropDownList>
            </td>
        </tr>
       
    </table>

    </div>

    <footer>
    
     <div class="submit_link">    
       <asp:Button ID="btnBuscar" runat="server" Text="Emitir notas de cobro" OnClick="btnBuscar_Click" />
     </div>

    </footer>
  </article>

<asp:Panel ID='pnNotas' runat="server" Visible="false">

<article class="module width_full">

<header><h3>Notas de Cobro</h3></header>

    <asp:GridView ID="grdNotaCobro" runat="server" CssClass="tablesorter" GridLines="None" AutoGenerateColumns="false" Width="100%"
        OnRowDataBound="grdNotaCobro_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="FECHA EMISION">
                <ItemTemplate>
                    <asp:Label ID="lblFechaEmision" runat="server" Text='<%# Bind("FechaEmision") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FECHA FACTURA">
                <ItemTemplate>
                    <asp:Label ID="lblFechaFactura" runat="server" Text='<%# Bind("FechaFactura") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="RUT CLIENTE">
                <ItemTemplate>
                    <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NOMBRE CLIENTE">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TIPO NOTA">
                <ItemTemplate>
                    <asp:Label ID="lblTipoNota" runat="server" Text='<%# Bind("TipoNota") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton id="lnkVer" runat="server" CommandArgument='<%# Eval("ID") %>' >Ver</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron prestaciones coincidentes.
        </EmptyDataTemplate>
    </asp:GridView>

    <footer>

    <div class="submit_link">  
    <!--Falta Boton ?? -->  
    </div>
    
    </footer>

</article>

</asp:Panel>