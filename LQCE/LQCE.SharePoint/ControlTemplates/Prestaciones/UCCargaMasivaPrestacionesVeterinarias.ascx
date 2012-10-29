<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCargaMasivaPrestacionesVeterinarias.ascx.cs"
    Inherits="LQCE.SharePoint.ControlTemplates.Prestaciones.UCCargaMasivaPrestacionesVeterinarias" %>
<asp:Panel ID="panelMensaje" runat="server">
    <h4 class="alert_warning">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    </h4>
</asp:Panel>
<article class="module width_full">
    <header>
        <h3>
            Carga Masiva Prestaciones Veterinarias</h3>
    </header>
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
</article>
<article class="module width_full">
    <header>
        <h3>
            Instrucciones para el llenado del archivo</h3>
    </header>
    <div class="module_content">
        <ol>
            <li><strong>INGRESO</strong>: Número de ingreso. Obligatorio</li>
            <li><strong>NOMBRE</strong>: Nombre del paciente. Obligatorio</li>
            <li><strong>ESPECIE</strong>: Especie. Obligatorio</li>
            <li><strong>RAZA</strong>: Raza. Optativo</li>
            <li><strong>SEXO</strong>: Sexo. Optativo</li>
            <li><strong>EDAD</strong>: Edad. Optativo</li>
            <li><strong>TELEFONO</strong>: Telefono del paciente. Optativo</li>
            <li><strong>PROCEDENCIA</strong>: Procedencia. Optativo</li>
            <li><strong>GARANTIA</strong>: Garantía. Optativo</li>
            <li><strong>PENDIENTE</strong>Numerico. Monto pendiente. Optativo</li>
            <li><strong>EXAMEN 1</strong>Nombre del examen 1. Obligatorio</li>
            <li><strong>EXAMEN 2</strong>Nombre del examen 2. Optativo</li>
            <li><strong>EXAMEN 3</strong>Nombre del examen 3. Optativo</li>
            <li><strong>EXAMEN 4</strong>Nombre del examen 4. Optativo</li>
            <li><strong>EXAMEN 5</strong>Nombre del examen 5. Optativo</li>
            <li><strong>TOTAL</strong>Numerico. Monto total. Obligatorio</li>
            <li><strong>VALOR 1</strong>: Valor del examen 1. Obligatorio</li>
            <li><strong>VALOR 2</strong>: Valor del examen 2. Optativo</li>
            <li><strong>VALOR 3</strong>: Valor del examen 3. Optativo</li>
            <li><strong>VALOR 4</strong>: Valor del examen 4. Optativo</li>
            <li><strong>VALOR 5</strong>: Valor del examen 5. Optativo</li>
            <li><strong>RECEPCION</strong>: Recepción. Optativo</li>
            <li><strong>MEDICO</strong>: Nombre del médico. Optativo</li>
            <li><strong>SOLICITANTE</strong>: Nombre del cliente. Obligatorio</li>
            <li><strong>FECHA RECEPCION: Fecha de recepcion. Obligatorio</strong></li>
            <li><strong>HORA RECEPCION: Hora de recepcion. Obligatorio</strong></li>
            <li><strong>FICHA</strong>Numerico. Número de ficha clínica. Optativo</li>
        </ol>
        <div class="clear">
        </div>
    </div>
</article>