﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaMasivaPrestacionesHumanas.aspx.cs"
    Inherits="LQCE.SharePoint.Layouts.Prestaciones.CargaMasivaPrestacionesHumanas"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Src="~/_controltemplates/Prestaciones/UCCargaMasivaPrestacionesHumanas.ascx"
    TagName="CargaMasivaPrestacionesHumanas" TagPrefix="uc1" %>
<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
</asp:content>
<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <uc1:CargaMasivaPrestacionesHumanas ID="UCCargaMasivaPrestacionesHumanas1" runat="server" />
</asp:content>
<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    Carga Masiva Prestaciones Humanas
</asp:content>
<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Carga Masiva Prestaciones Humanas
</asp:content>
<asp:content id="PageTitleInTitleArea2" contentplaceholderid="PlaceHolderPageTitleInTitleArea2"
    runat="server">
    Carga Masiva Prestaciones Humanas
</asp:content>