<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimesheetManagement.aspx.cs" 
    Inherits="TimesheetManagement" MasterPageFile="~/master/Master1.Master" %>

<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="TimesheetMenu" Src="~/design/Menus/TimesheetMenu.ascx" %>

<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContentTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
  Timesheet
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="TimesheetMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:TimesheetMenu ID="TimesheetMenuContent" runat="server" />
</asp:Content>
