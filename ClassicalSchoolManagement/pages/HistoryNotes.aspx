<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryNotes.aspx.cs" 
    Inherits="HistoryNotes" MasterPageFile="~/master/Master1.Master" %>

<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="NotesMenu" Src="~/design/Menus/NotesMenu.ascx" %>
<%--<%@ Register TagPrefix="art" TagName="HistoryNotes" Src="~/design/BodyContainer/BC_HistoryNotes.ascx" %>--%>


<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContentTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
History Notes
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="NotesMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:NotesMenu ID="NotesMenuContent" runat="server" />
</asp:Content>

<%--<asp:Content ID="HistoryNotesContainer" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    <art:HistoryNotes ID="HistoryNotesContent" runat="server" />
</asp:Content>--%>

