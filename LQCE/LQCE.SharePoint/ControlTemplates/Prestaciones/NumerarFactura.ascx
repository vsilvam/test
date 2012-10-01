<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumerarFactura.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.NumerarFactura" %>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css" type="text/css" media="all" />
<script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/_layouts/JScript/ui.datepicker-es.js")%>"></script>


<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
    <article class="module width_full">
        <header>
            <h3>
                Numerar Facturas</h3>
        </header>
       

        <asp:GridView ID="grdFacturas" CssClass="tablesorter" GridLines="Both" runat="server"
            AutoGenerateColumns="False" Width="100%" EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="fecha_facturacion" HeaderText="FECHA FACTURACION" />
                <asp:BoundField DataField="total_facturas" HeaderText="TOTAL FACTURAS" />
                <asp:BoundField DataField="TOTAL_FACTURAS_POR_NUMERAR" HeaderText="FACTURAS POR NUMERAR" />
                <asp:TemplateField HeaderText="NUMERAR">
                    <ItemTemplate>
                        <asp:LinkButton id="lnkNumerar" runat="server" CommandArgument='<%# Eval("ID_FACTURACION") %>' onclick="lnkNumerar_Click" >Numerar</asp:LinkButton>
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
                
            </div>
        </footer>
    </article>

<asp:Panel ID="pnNumerar" runat="server" Visible="false">
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblNumerar" runat="server" 
                    RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rblNumerar_SelectedIndexChanged" >
                    <asp:ListItem Value="1">Todos</asp:ListItem>
                    <asp:ListItem Value="2">Desde-Hasta</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>Desde
            </td>
            <td>
                <asp:TextBox ID="txtDesdeN" runat="server"></asp:TextBox>
            </td>
            <td>Hasta
            </td>
            <td>
                <asp:TextBox ID="txtHastaN" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Numero factura inicial</td>
            <td>
                <asp:TextBox ID="txtNroFactura" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNumerar" runat="server" Text="Numerar Facturas" 
                    onclick="btnNumerar_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>