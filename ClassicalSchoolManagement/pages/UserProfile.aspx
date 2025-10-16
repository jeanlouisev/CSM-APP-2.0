<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs"
    Inherits="UserProfile"  MasterPageFile="~/master/Master4.Master" %> 

<%--<%@ Register TagPrefix="art" TagName="AccademicYear" src="~/design/BodyContainer/BC_Profile.ascx"%>--%>


<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
     User Profile
</asp:Content>



<%--<asp:Content ID="bannerContent" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <art:banner ID="bannerContainer" runat="server" />
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="ParameterMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:ParameterMenu ID="ParameterMenuContent" runat="server" />
</asp:Content>--%>

<asp:Content ID="AccademicYearContainer" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
</asp:Content>
