<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="C#" DynamicMasterPageFile="~masterurl/default.master" AutoEventWireup="true"
    Inherits="LQCE.SharePoint.Layouts.Prestaciones.EditarPrestacionesHumanas" CodeBehind="EditarPrestacionesHumanas.aspx.cs" %>

<%@ Register Src="~/_controltemplates/Prestaciones/EditarPrestacionesHumanas.ascx"
    TagName="EditarPrestacionesHumanas" TagPrefix="uc1" %>
<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <uc1:EditarPrestacionesHumanas ID="EditarPrestacionesHumanas1" runat="server" />
</asp:content>
<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    Formulario de Edicion Pretaciones a Personas
</asp:content>
<asp:content id="PageTitleInTitleArea" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">
    Formulario de Edicion Pretaciones a Personas
</asp:content>
<asp:content id="PageTitleInTitleArea2" runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea2">
    Formulario de Edicion Pretaciones a Personas
</asp:content>
