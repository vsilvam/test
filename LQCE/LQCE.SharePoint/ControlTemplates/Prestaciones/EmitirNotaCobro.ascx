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
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css"
    type="text/css" media="all" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/ui-lightness/jquery-ui.css"
    type="text/css" media="all" />
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
    <header>
        <h3>
            Buscar Notas de Cobro</h3>
    </header>
    <div class="module_content">
        <h4>
            Periodo</h4>
        <table>
            <tr>
                <td>
                    Desde
                </td>
                <td>
                    <asp:TextBox ID="txtDesde" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtDesde" runat="server" ControlToValidate="txtDesde"
                        ValidationGroup="emitir" Text="Requerido" Display="Dynamic">Requerido</asp:RequiredFieldValidator>
                </td>
                <td>
                    Hasta
                </td>
                <td>
                    <asp:TextBox ID="txtHasta" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtHasta" runat="server" ControlToValidate="txtHasta"
                        ValidationGroup="emitir" Text="Requerido" Display="Dynamic">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Clientes
                </td>
                <td>
                    <asp:DropDownList ID="ddlClientes" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                        DataValueField="ID">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_ddlNotaCobro" runat="server" ControlToValidate="ddlNotaCobro"
                        ValidationGroup="emitir" Text="Requerido" Display="Dynamic">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <footer>
        <div class="submit_link">
            <asp:Button ID="btnEmitir" runat="server" Text="Emitir notas de cobro" ValidationGroup="emitir"  OnClick="btnEmitir_Click" />
        </div>
    </footer>
</article>
