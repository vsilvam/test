<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargaArchivo.ascx.cs" Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.CargaArchivo" %>

<h1>
    Carga Masiva
</h1>
<asp:Panel ID="pnCargaMasiva" runat="server">
<asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
<h2>
        Adjuntar Archivo
    </h2>
    <asp:Label ID="lblMensajePaso1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    <p>
        <b>Normas generales para subir archivos</b></p>
    <ol>
        <li>Se aceptan archivos Excel (.xls; .xlsx)</li>
        <li>El tamaño del archivo no debe exceder los 4 MB</li>
    </ol>
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlTipoPrestacion" runat="server" 
                    onselectedindexchanged="ddlTipoPrestacion_SelectedIndexChanged">
                    <asp:ListItem Value="0">Seleccione Tipo Prestacion</asp:ListItem>
                    <asp:ListItem Value="1">Humano</asp:ListItem>
                    <asp:ListItem Value="2">Veterinario</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                Cargar archivo
            </td>
            <td>
                <asp:FileUpload ID="fileExcel" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fileExcel" runat="server"
                    ControlToValidate="fileExcel" ValidationGroup="paso1">Requerido</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnPaso1Template" runat="server" OnClick="btnPaso1Template_Click"
        Text="Descargar Plantilla" />
    <asp:Button ID="btnPaso1Adjuntar" runat="server" ValidationGroup="paso1" OnClick="btnPaso1Adjuntar_Click"
        Text="Adjuntar Archivo" />
<h3>
        Instrucciones para el llenado del archivo</h3>
    <ol>
        <li><strong>FICHA</strong>: Número de solicitud. Obligatorio</li>
        <li><strong>NOMBRE</strong>: Nombre del paciente. Optativo</li>
        <li><strong>RUT</strong>: RUT del paciente. Optativo</li>
        <li><strong>MEDICO</strong>: Nombre del médico. Optativo</li>
        <li><strong>EDAD</strong>: Edad del paciente. Optativo</li>
        <li><strong>TELEFONO</strong>: Telefono del paciente. Optativo</li>
        <li><strong>PROCEDENCIA: Nombre del cliente. Obligatorio</strong></li>
        <li><strong>FECHA RECEPCION: Fecha de recepcion. Formato . Obligatorio</strong></li>
        <li><strong>MUESTRA: Descripción de la muestra. Optativo</strong></li>
        <li><strong>EXAMEN: Nombre del examen. Optativo</strong></li>
        <li><strong>VALOR: Valor del examen. Optativo</strong></li>
        <li><strong>FECHA RESULTADOS</strong>Fecha de resultado de los examenes. Optativo</li>
        <li><strong>PREVISION</strong>CONVENIO, FONASA, ISAPRE, PARTICULAR</li>
        <li><strong>GARANTIA</strong></li>
        <li><strong>PAGADO</strong>Numerico. Monto pagado</li>
        <li><strong>PENDIENTE</strong>Numerico. Monto pendiente</li>
        <li><strong>TOTAL</strong>Numerico. Monto total</li>
    </ol>
</asp:Panel>