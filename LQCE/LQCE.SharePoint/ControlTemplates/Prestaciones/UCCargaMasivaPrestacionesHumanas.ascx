<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCargaMasivaPrestacionesHumanas.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.UCCargaMasivaPrestacionesHumanas" %>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></h4>
</asp:Panel>
<article class="module width_full">

<header><h3>Carga Masiva Prestaciones Humanas</h3></header>

<div class="module_content">

<p>
    <b>Normas generales para subir archivos</b></p>
<ol>
    <li>Se aceptan archivos Excel (.xls; .xlsx)</li>
    <li>El tamaño del archivo no debe exceder los 4 MB</li>
</ol>
<table>
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
</div>

<footer>
<div class="submit_link">
<asp:Button ID="btnPaso1Template" runat="server" OnClick="btnPaso1Template_Click"
    Text="Descargar Plantilla" />
<asp:Button ID="btnPaso1Adjuntar" runat="server" ValidationGroup="paso1" OnClick="btnPaso1Adjuntar_Click"
    Text="Adjuntar Archivo" />
</div>
</footer>
<div class="clear"></div>
</article>

<article class="module width_full">

<header><h3>Instrucciones para el llenado del archivo</h3></header>

<div class="module_content">

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

<div class="clear"></div>
</div>
</article>
