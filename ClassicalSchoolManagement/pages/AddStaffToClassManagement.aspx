<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStaffToClassManagement.aspx.cs" 
    Inherits="AddStaffToClassManagement" MasterPageFile="~/master/Master1.Master"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="ClassroomMenu" Src="~/design/Menus/ClassroomMenu.ascx" %>
<%--<%@ Register TagPrefix="art" TagName="AddStaffToClassroomManagement" Src="~/design/BodyContainer/BC_AddStaffToClassroomManagement.ascx" %>--%>


<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="ClassroomMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:ClassroomMenu ID="ClassroomMenuContent" runat="server" />
</asp:Content>

<%--<asp:Content ID="RegisterStudentContainer" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    <art:AddStaffToClassroomManagement ID="ClassroomManagementContent" runat="server" />
</asp:Content>--%>
