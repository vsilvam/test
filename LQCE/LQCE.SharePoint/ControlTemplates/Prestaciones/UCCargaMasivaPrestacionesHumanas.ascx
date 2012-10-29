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
    <header>
        <h3>
            Carga Masiva Prestaciones Humanas</h3>
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
    <div class="clear">
    </div>
</article>
<article class="module width_full">
    <header>
        <h3>
            Instrucciones para el llenado del archivo</h3>
    </header>
    <div class="module_content">
        <ol>
            <li><strong>NOMBRE</strong>: Nombre del paciente. Optativo</li>
            <li><strong>FICHA</strong>: Número de solicitud. Obligatorio</li>
            <li><strong>FECHA RECEPCION</strong>: Fecha de recepcion. Obligatorio</li>
            <li><strong>HORA RECEPCION</strong>: Hora de recepcion. Obligatorio</li>
            <li><strong>TELEFONO</strong>: Telefono del paciente. Optativo</li>
            <li><strong>MEDICO</strong>: Nombre del médico. Optativo</li>
            <li><strong>PROCEDENCIA: Nombre del cliente. Obligatorio</strong></li>
            <li><strong>PREVISION</strong>: CONVENIO, FONASA, ISAPRE, PARTICULAR. Optativo</li>
            <li><strong>GARANTIA</strong>: Garantía. Optativo</li>
            <li><strong>PENDIENTE</strong>Numerico. Monto pendiente. Optativo</li>
            <li><strong>PAGADO</strong>Numerico. Monto pagado. Optativo</li>
            <li><strong>EXAMEN 1: Nombre del examen 1. Obligatorio</strong></li>
            <li><strong>EXAMEN 2: Nombre del examen 2. Optativo</strong></li>
            <li><strong>EXAMEN 3: Nombre del examen 3. Optativo</strong></li>
            <li><strong>EXAMEN 4: Nombre del examen 4. Optativo</strong></li>
            <li><strong>EXAMEN 5: Nombre del examen 5. Optativo</strong></li>
            <li><strong>EXAMEN 6: Nombre del examen 6. Optativo</strong></li>
            <li><strong>EXAMEN 7: Nombre del examen 7. Optativo</strong></li>
            <li><strong>EXAMEN 8: Nombre del examen 8. Optativo</strong></li>
            <li><strong>EXAMEN 9: Nombre del examen 9. Optativo</strong></li>
            <li><strong>TOTAL</strong>Numerico. Monto total. Obligatorio</li>
            <li><strong>VALOR 1: Valor del examen 1. Obligatorio</strong></li>
            <li><strong>VALOR 2: Valor del examen 2. Optativo</strong></li>
            <li><strong>VALOR 3: Valor del examen 3. Optativo</strong></li>
            <li><strong>VALOR 4: Valor del examen 4. Optativo</strong></li>
            <li><strong>VALOR 5: Valor del examen 5. Optativo</strong></li>
            <li><strong>VALOR 6: Valor del examen 6. Optativo</strong></li>
            <li><strong>VALOR 7: Valor del examen 7. Optativo</strong></li>
            <li><strong>VALOR 8: Valor del examen 8. Optativo</strong></li>
            <li><strong>VALOR 9: Valor del examen 9. Optativo</strong></li>
            <li><strong>RECEPCION</strong>: Recepción. Optativo</li>
            <li><strong>EDAD</strong>: Edad del paciente. Optativo</li>
            <li><strong>RUT</strong>: RUT del paciente. Optativo</li>
        </ol>
        </asp:Panel>
        <div class="clear">
        </div>
    </div>
</article>
