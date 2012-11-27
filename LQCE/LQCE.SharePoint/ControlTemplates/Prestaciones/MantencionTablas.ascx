<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MantencionTablas.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.MantencionTablas" %>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    </h4>
</asp:Panel>
<asp:Panel ID="pnTablas" runat="server">
    <table>
        <tr>
            <td>
                Seleccione Tabla
            </td>
            <td>
                <asp:DropDownList ID="ddlTablas" runat="server" OnSelectedIndexChanged="ddlTablas_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="">Seleccione</asp:ListItem>
                    <asp:ListItem Value="1">Comuna</asp:ListItem>
                    <asp:ListItem Value="2">Examen</asp:ListItem>
                    <asp:ListItem Value="3">Especie</asp:ListItem>
                    <asp:ListItem Value="7">Garantia</asp:ListItem>
                    <asp:ListItem Value="8">Prevision</asp:ListItem>
                    <asp:ListItem Value="9">Raza</asp:ListItem>
                    <asp:ListItem Value="10">Region</asp:ListItem>
                    <asp:ListItem Value="11">Tipo Cobro</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
            </td>
            <td>
                <input type="button" value="Volver" onclick="javascript:history.back(1)" />
            </td>
            <td>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel Id="panelExamen" runat="server" Visible="false">
    <asp:Panel ID="panelExamenListar" runat="server" Visible="true">
        <h3>Examen</h3>
        <asp:GridView ID="gridExamen" runat="server"  Width="100%" AutoGenerateColumns="false" GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE_EXAMEN" />
                <asp:BoundField HeaderText="CODIGO" DataField="CODIGO" />
                <asp:BoundField HeaderText="TIPOPRESTACION" DataField="TIPO_PRESTACION" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminarExamen" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" onclick="imgEliminarExamen_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizarExamen" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" onclick="imgActualizarExamen_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelExamenAgregar" runat="server" Visible="false">
        <h3>AgregarExamen</h3>
        <table>
            <tr>
                <td>Nombre</td>
                <td>
                    <asp:TextBox ID="txtExamenAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExamenAgregarNombre"
                        ValidationGroup="ExamenAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Codigo</td>
                <td>
                    <asp:TextBox ID="txtExamenAgregarCodigo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtExamenAgregarCodigo"
                        ValidationGroup="ExamenAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Tipo Prestacion</td>
                <td>
                    <asp:DropDownList ID="selExamenAgregaTipoPrestacion" runat="server" 
                        AppendDataBoundItems="True" DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="selExamenAgregaTipoPrestacion"
                        ValidationGroup="ExamenAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Sinonimo</td>
                <td>
                    <table>
                        <tr>
                            <td>Ingresa Sinonimo</td>
                            <td>
                                <asp:TextBox ID="txtExamenAgregarIngresaSinonimo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnExamenAgregarIngresaSinonimo" runat="server" Text="Ingresar" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gridAgregarSinonimoExamen" runat="server" AutoGenerateColumns="false"
                        GridLines="Both" Width="100%" DataKeyNames="ID">
                        <Columns>
                            <asp:BoundField DataField="NOMBRE" HeaderText="Sinonimo" />
                        </Columns>
                        <EmptyDataTemplate>
                            El Examne no tiene sinónimos registrados.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnExamenAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="ExamenAgregar" />
        <asp:Button ID="btnExamenAgregaCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
    </asp:Panel>
    <asp:Panel ID="panelExamenActualizar" runat="server" Visible="false">
        <h3>Actualizar Examen</h3>
        <asp:HiddenField ID="hdnExamenActuaizarId" runat="server" />
        <table>
            <tr>
                <td>Nombre</td>
                <td>
                    <asp:TextBox ID="txtExamenActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtExamenActualizarNombre" ValidationGroup="ExamenActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Codigo</td>
                <td>
                    <asp:TextBox ID="txtExamenActualizarCodigo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtExamenActualizarCodigo" ValidationGroup="ExamenActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Tipo Prestacion</td>
                <td>
                    <asp:DropDownList ID="selExamenActualizarTipoPrestacion" runat="server" 
                        AppendDataBoundItems="True" DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="selExamenActualizarTipoPrestacion"
                        ValidationGroup="ExamenActualizar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Sinonimo</td>
                <td>
                    <table>
                        <tr>
                            <td>Ingresa Sinonimo</td>
                            <td>
                                <asp:TextBox ID="txtExamenActualizarIngresaSinonimo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnExamenActualizarIngresaSinonimo" runat="server" Text="Ingresar" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gridActualizarSinonimoExamen" runat="server" AutoGenerateColumns="false"
                        GridLines="Both" Width="100%" DataKeyNames="ID" 
                        onrowcommand="gridActualizarSinonimoExamen_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="NOMBRE" HeaderText="Sinonimo" />
                            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" />
                        </Columns>
                        <EmptyDataTemplate>
                            El Examne no tiene sinónimos registrados.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnExamenActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="ExamenActualizar" />
        <asp:Button ID="btnExamenActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelComuna" runat="server" Visible="false">
    <asp:Panel ID="panelComunaListar" runat="server" Visible="true">
        <h3>
            Comunas</h3>
        <asp:GridView ID="gridComunas" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="REGIÓN">
                    <ItemTemplate>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("REGION.NOMBRE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarComuna_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarComuna_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelComunaAgregar" runat="server" Visible="false">
        <h3>
            Agregar Comuna</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtComunaAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredtxtComunaAgregarNombre" runat="server" ControlToValidate="txtComunaAgregarNombre"
                        ValidationGroup="ComunaAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Region
                </td>
                <td>
                    <asp:DropDownList ID="selComunaAgregarRegion" runat="server" AppendDataBoundItems="True"
                        DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="requiredselComunaAgregarRegion" runat="server" ControlToValidate="selComunaAgregarRegion"
                        ValidationGroup="ComunaAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnComunaAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="ComunaAgregar"
            OnClick="btnComunaAgregarGuardar_Click" />
        <asp:Button ID="btnComunaAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnComunaAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelComunaActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Comuna</h3>
        <asp:HiddenField ID="hdnComunaActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtComunaActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtComunaActualizarNombre" runat="server"
                        ControlToValidate="txtComunaActualizarNombre" ValidationGroup="ComunaActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Region
                </td>
                <td>
                    <asp:DropDownList ID="selComunaActualizarRegion" runat="server" AppendDataBoundItems="True"
                        DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredselComunaActualizarRegion" runat="server"
                        ControlToValidate="selComunaActualizarRegion" ValidationGroup="ComunaActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnComunaActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="ComunaActualizar"
            OnClick="btnComunaActualizarGuardar_Click" />
        <asp:Button ID="btnComunaActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnComunaActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelEspecie" runat="server" Visible="false">
    <asp:Panel ID="panelEspecieListar" runat="server" Visible="true">
        <h3>
            Especies</h3>
        <asp:GridView ID="gridEspecies" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarEspecie_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarEspecie_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelEspecieAgregar" runat="server" Visible="false">
        <h3>
            Agregar Especie</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtEspecieAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtEspecieAgregarNombre" runat="server" ControlToValidate="txtEspecieAgregarNombre"
                        ValidationGroup="EspecieAgregar" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnEspecieAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="EspecieAgregar"
            OnClick="btnEspecieAgregarGuardar_Click" />
        <asp:Button ID="btnEspecieAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnEspecieAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelEspecieActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Especie</h3>
        <asp:HiddenField ID="hdnEspecieActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtEspecieActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEspecieActualizarNombre"
                        ValidationGroup="EspecieActualizar" ForeColor="Red" ErrorMessage="Requerido"
                        Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnEspecieActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="EspecieActualizar"
            OnClick="btnEspecieActualizarGuardar_Click" />
        <asp:Button ID="btnEspecieActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnEspecieActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelGarantia" runat="server" Visible="false">
    <asp:Panel ID="panelGarantiaListar" runat="server" Visible="true">
        <h3>
            Garantia</h3>
        <asp:GridView ID="gridGarantia" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarGarantia_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarGarantia_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelGarantiaAgregar" runat="server" Visible="false">
        <h3>
            Agregar Garantia</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtGarantiaAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtGarantiaAgregarNombre" runat="server"
                        ControlToValidate="txtGarantiaAgregarNombre" ValidationGroup="GarantiaAgregar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnGarantiaAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="GarantiaAgregar"
            OnClick="btnGarantiaAgregarGuardar_Click" />
        <asp:Button ID="btnGarantiaAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnGarantiaAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelGarantiaActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Garantia</h3>
        <asp:HiddenField ID="hdnGarantiaActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtGarantiaActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtGarantiaActualizarNombre"
                        runat="server" ControlToValidate="txtGarantiaActualizarNombre" ValidationGroup="GarantiaActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnGarantiaActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="GarantiaActualizar"
            OnClick="btnGarantiaActualizarGuardar_Click" />
        <asp:Button ID="btnGarantiaActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnGarantiaActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelPrevision" runat="server" Visible="false">
    <asp:Panel ID="panelPrevisionListar" runat="server" Visible="true">
        <h3>
            Previsión</h3>
        <asp:GridView ID="gridPrevision" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarPrevision_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarPrevision_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelPrevisionAgregar" runat="server" Visible="false">
        <h3>
            Agregar Prevision</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtPrevisionAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPrevisionAgregarNombre"
                        runat="server" ControlToValidate="txtPrevisionAgregarNombre" ValidationGroup="PrevisionAgregar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnPrevisionAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="PrevisionAgregar"
            OnClick="btnPrevisionAgregarGuardar_Click" />
        <asp:Button ID="btnPrevisionAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnPrevisionAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelPrevisionActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Prevision</h3>
        <asp:HiddenField ID="hdnPrevisionActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtPrevisionActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtPrevisionActualizarNombre" runat="server"
                        ControlToValidate="txtPrevisionActualizarNombre" ValidationGroup="PrevisionActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnPrevisionActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="PrevisionActualizar"
            OnClick="btnPrevisionActualizarGuardar_Click" />
        <asp:Button ID="btnPrevisionActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnPrevisionActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelRaza" runat="server" Visible="false">
    <asp:Panel ID="panelRazaListar" runat="server" Visible="true">
        <h3>
            Razas</h3>
        <asp:GridView ID="gridRazas" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="ESPECIE">
                    <ItemTemplate>
                        <asp:Label ID="lblEspecie" runat="server" Text='<%# Bind("ESPECIE.NOMBRE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarRaza_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarRaza_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelRazaAgregar" runat="server" Visible="false">
        <h3>
            Agregar Raza</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtRazaAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRazaAgregarNombre" runat="server"
                        ControlToValidate="txtRazaAgregarNombre" ValidationGroup="RazaAgregar" ForeColor="Red"
                        ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Especie
                </td>
                <td>
                    <asp:DropDownList ID="selRazaAgregarEspecie" runat="server" AppendDataBoundItems="True"
                        DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_selRazaAgregarEspecie" runat="server"
                        ControlToValidate="selRazaAgregarEspecie" ValidationGroup="RazaAgregar" ForeColor="Red"
                        ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnRazaAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="RazaAgregar"
            OnClick="btnRazaAgregarGuardar_Click" />
        <asp:Button ID="btnRazaAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnRazaAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelRazaActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Raza</h3>
        <asp:HiddenField ID="hdnRazaActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtRazaActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRazaActualizarNombre" runat="server"
                        ControlToValidate="txtRazaActualizarNombre" ValidationGroup="RazaActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Especie
                </td>
                <td>
                    <asp:DropDownList ID="selRazaActualizarEspecie" runat="server" AppendDataBoundItems="True"
                        DataTextField="NOMBRE" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorselRazaActualizarEspecie" runat="server"
                        ControlToValidate="selRazaActualizarEspecie" ValidationGroup="RazaActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnRazaActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="RazaActualizar"
            OnClick="btnRazaActualizarGuardar_Click" />
        <asp:Button ID="btnRazaActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnRazaActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelRegion" runat="server" Visible="false">
    <asp:Panel ID="panelRegionListar" runat="server" Visible="true">
        <h3>
            Regiones</h3>
        <asp:GridView ID="gridRegiones" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarRegion_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarRegion_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelRegionAgregar" runat="server" Visible="false">
        <h3>
            Agregar Region</h3>
        <table>
            <tr>
                <td>
                    ID
                </td>
                <td>
                    <asp:TextBox ID="txtRegionAgregarId" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredtxtRegionAgregarId" runat="server" ControlToValidate="txtRegionAgregarId"
                        ValidationGroup="RegionAgregar"  Display="Dynamic" ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareRegionAgregarId" runat="server" ControlToValidate="txtRegionAgregarId"
                        ValidationGroup="RegionAgregar" ForeColor="Red" ErrorMessage="Formato no válido"
                        Text="Formato no válido" Display="Dynamic" Type="Integer" Operator="DataTypeCheck">Formato no válido</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtRegionAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRegionAgregarNombre" runat="server"
                        ControlToValidate="txtRegionAgregarNombre" ValidationGroup="RegionAgregar" ForeColor="Red"
                        ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnRegionAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="RegionAgregar"
            OnClick="btnRegionAgregarGuardar_Click" />
        <asp:Button ID="btnRegionAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnRegionAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelRegionActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Region</h3>
        <table>
            <tr>
                <td>
                    Id
                </td>
                <td>
                    <asp:Label ID="lblRegionActualizarId" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtRegionActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRegionActualizarNombre"
                        runat="server" ControlToValidate="txtRegionActualizarNombre" ValidationGroup="RegionActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnRegionActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="RegionActualizar"
            OnClick="btnRegionActualizarGuardar_Click" />
        <asp:Button ID="btnRegionActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnRegionActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="panelTipoCobro" runat="server" Visible="false">
    <asp:Panel ID="panelTipoCobroListar" runat="server" Visible="true">
        <h3>
            Tipos de Cobro</h3>
        <asp:GridView ID="gridTiposCobro" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="Both" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="NOMBRE" DataField="NOMBRE" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/eliminar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Eliminar" OnClick="imgEliminarTipoCobro_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgActualizar" runat="server" ImageUrl="../../_layouts/Style/Imagenes/editar.jpg"
                            CommandArgument='<%# Eval("ID") %>' Height="15px" ToolTip="Editar" OnClick="imgActualizarTipoCobro_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelTipoCobroAgregar" runat="server" Visible="false">
        <h3>
            Agregar Tipo de Cobro</h3>
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtTipoCobroAgregarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTipoCobroAgregarNombre"
                        runat="server" ControlToValidate="txtTipoCobroAgregarNombre" ValidationGroup="TipoCobroAgregar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnTipoCobroAgregarGuardar" runat="server" Text="Guardar" ValidationGroup="TipoCobroAgregar"
            OnClick="btnTipoCobroAgregarGuardar_Click" />
        <asp:Button ID="btnTipoCobroAgregarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnTipoCobroAgregarCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="panelTipoCobroActualizar" runat="server" Visible="false">
        <h3>
            Actualizar Tipo de Cobro</h3>
        <asp:HiddenField ID="hdnTipoCobroActualizarId" runat="server" />
        <table>
            <tr>
                <td>
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtTipoCobroActualizarNombre" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTipoCobroActualizarNombre"
                        runat="server" ControlToValidate="txtTipoCobroActualizarNombre" ValidationGroup="TipoCobroActualizar"
                        ForeColor="Red" ErrorMessage="Requerido" Text="Requerido">Requerido</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnTipoCobroActualizarGuardar" runat="server" Text="Guardar" ValidationGroup="TipoCobroActualizar"
            OnClick="btnTipoCobroActualizarGuardar_Click" />
        <asp:Button ID="btnTipoCobroActualizarCancelar" runat="server" Text="Cancelar" CausesValidation="false"
            OnClick="btnTipoCobroActualizarCancelar_Click" />
    </asp:Panel>
</asp:Panel>
