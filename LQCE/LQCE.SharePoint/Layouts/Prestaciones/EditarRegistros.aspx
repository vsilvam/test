<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="C#" 
    DynamicMasterPageFile="~masterurl/default.master" 
    AutoEventWireup="true" 
    Inherits="LQCE.SharePoint.Layouts.Prestaciones.EditarRegistros" 
    CodeBehind="EditarRegistros.aspx.cs" %>

<%@ Register Src="~/_controltemplates/Prestaciones/EditarRegistros.ascx" TagName="EditarRegistros" TagPrefix="uc1" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <uc1:EditarRegistros ID="EditarRegistros1" runat="server" />

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Formulario de Editar Registros
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
    Formulario de Editar Registros
</asp:Content>
<asp:Content ID="PageTitleInTitleArea2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea2">
    Formulario de Editar Registros
</asp:Content>
