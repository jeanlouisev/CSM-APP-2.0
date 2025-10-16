<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" 
    Inherits="Personal" MasterPageFile="~/master/Master1.Master" %>

<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="PersonalMenu" Src="~/design/Menus/PersonalMenu.ascx" %>


<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContentTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
  Personal
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="PersonalMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:PersonalMenu ID="PersonalMenuContent" runat="server" />
</asp:Content>