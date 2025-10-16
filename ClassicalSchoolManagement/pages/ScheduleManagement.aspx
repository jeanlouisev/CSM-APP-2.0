<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleManagement.aspx.cs" 
    Inherits="ScheduleManagement" MasterPageFile="~/master/Master1.Master" %>

<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="ScheduleMenu" Src="~/design/Menus/ScheduleMenu.ascx" %>


<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContentTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
 Schedule
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="ScheduleMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:ScheduleMenu ID="ScheduleMenuContent" runat="server" />
</asp:Content>
