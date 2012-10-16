<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmitirNotaCredito.aspx.cs"
    Inherits="LQCE.SharePoint.Layouts.Prestaciones.EmitirNotaCredito" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Src="~/_controltemplates/Prestaciones/EmitirNotaCredito.ascx" TagName="EmitirNotaCredito"
    TagPrefix="uc1" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <uc1:EmitirNotaCredito ID="EmitirNotaCredito1" runat="server" />
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Emitir Nota de Crédito
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
    Emitir Nota de Crédito
</asp:Content>
<asp:Content ID="PageTitleInTitleArea2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea2">
    Emitir Nota de Crédito
</asp:Content>
