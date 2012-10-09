<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MantencionTablas.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.MantencionTablas" %>


<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>

<asp:Panel ID="pnTablas" runat="server">
    <table>
        <tr>
            <td>Seleccione Tabla</td>
            <td>
                <asp:DropDownList ID="ddlTablas" runat="server" 
                    onselectedindexchanged="ddlTablas_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="">Seleccione</asp:ListItem>
                    <asp:ListItem Value="1">Comuna</asp:ListItem>
                    <asp:ListItem Value="2">Cliente Sinonimo</asp:ListItem>
                    <asp:ListItem Value="3">Especie</asp:ListItem>
                    <asp:ListItem Value="4">Examen</asp:ListItem>
                    <asp:ListItem Value="5">Examen Detalle</asp:ListItem>
                    <asp:ListItem Value="6">Examen Sinonimo</asp:ListItem>
                    <asp:ListItem Value="7">Garantia</asp:ListItem>
                    <asp:ListItem Value="8">Prevision</asp:ListItem>
                    <asp:ListItem Value="9">Raza</asp:ListItem>
                    <asp:ListItem Value="10">Region</asp:ListItem>
                    <asp:ListItem Value="11">Tipo Cobro</asp:ListItem>
                    <asp:ListItem Value="12">Tipo Factura</asp:ListItem>
                    <asp:ListItem Value="13">Tipo Prestacion</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                    onclick="btnLimpiar_Click" />
            </td>
            <td>
                <input type="button" value="Volver" onclick="javascript:history.back(1)" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnGrilla" runat="server" Visible= "false">
    <asp:GridView ID="grdTablas" runat="server" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="EDITAR">
                <ItemTemplate>
                    <asp:ImageButton ID="imgAgregar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/agregar.jpg"
                        CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" 
                        onclick="imgAgregar_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EDITAR">
                <ItemTemplate>
                    <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                        CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" 
                        onclick="imgActualizar_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>

<asp:Panel ID="pnComuna" runat="server" Visible="false">
    <table>
        <tr id="IdComuna">
            <td>Id</td>
            <td>
                <asp:TextBox ID="txtIdComuna" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreComuna" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Region</td>
            <td>
                <asp:DropDownList ID="ddlRegionComuna" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE" DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblEstadoComuna" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnModificarComuna" runat="server" Text="Modificar" 
        onclick="btnModificarComuna_Click" />
</asp:Panel>

<asp:Panel ID="pnClienteSinonimo" runat="server" Visible="false">
    <table>
        <tr>
            <td>Id</td>
            <td>
                <asp:TextBox ID="txtIdClienteSinonimo" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre - Sinonimo</td>
            <td>
                <asp:TextBox ID="txtNombreSinonimo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Cliente</td>
            <td>
                <asp:TextBox ID="txtClienteSinonimo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblEstadoClienteSinonimo" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnEspecie" runat="server" Visible="false">
    <table>
        <tr>
            <td>Id</td>
            <td>
                <asp:TextBox ID="txtIdEspecie" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreEspecie" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblEstadoEspecie" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnExamen" runat="server" Visible="false">
    <table>
        <tr>
            <td>Id</td>
            <td>
                <asp:TextBox ID="txtIdExamen" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Tipo Prestacion</td>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacionExamen" runat="server" 
                    AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Codigo</td>
            <td>
                <asp:TextBox ID="txtCodigoExamen" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreExamen" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblEstadoExamen" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnExamenDetalle" runat="server" Visible="false">
    <table>
        <tr>
            <td>Id</td>
            <td>
                <asp:TextBox ID="txtIdExamenDetalle" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Examen
            </td>
            <td>
                <asp:TextBox ID="txtExameExamenDetalle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Sub Examen</td>
            <td>
                <asp:TextBox ID="txtSubExamenExamenDetalle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoExamenDetalle" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnExamenSinonimo" runat="server" Visible="false">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdES" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Examen
            </td>
            <td>
                <asp:DropDownList ID="ddlExamenES" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Nombre Sinonimo
            </td>
            <td>
                <asp:TextBox ID="txtSinonimoES" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Activo
            </td>
            <td>
                <asp:RadioButtonList ID="rblActivoES" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnGarantia" runat="server" Visible="false">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdG" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreG" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoG" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pnPrevision" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdP" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreP" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoP" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pnRaza" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdR" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreR" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Especie</td>
            <td>
                <asp:DropDownList ID="ddlEspecieR" runat="server" AppendDataBoundItems="True" DataTextField="NOMBRE"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rdbActivoR" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pnRegion" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdRegion" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreRegion" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoRegion" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pnTipoCobro" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdTC" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreTC" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Reporte</td>
            <td>
                <asp:TextBox ID="txtReporteTC" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoTC" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pntipoFactura" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdTF" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreTF" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Reporte</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel id="pnTipoPrestacion" Visible="false" runat="server">
    <table>
        <tr>
            <td>
                Id
            </td>
            <td>
                <asp:TextBox ID="txtIdTP" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="txtNombreTP" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Activo</td>
            <td>
                <asp:RadioButtonList ID="rblActivoTP" runat="server">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>
