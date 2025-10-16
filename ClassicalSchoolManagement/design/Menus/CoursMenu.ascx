<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoursMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.CoursMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknAddCours" Width="100%" runat="server"  Font-Underline="false"
                Text="Ajouter" OnClick="lknAddCours_Click"></asp:LinkButton>
        </td>
    </tr>
    <%--
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchCours" Width="100%" Font-Underline="false" runat="server" Text="Rechercher" PostBackUrl="~/pages/SearchCours.aspx"></asp:LinkButton>
        </td>
    </tr>  --%>   
    <tr>
        <td>
            <asp:LinkButton ID="lknFixPrice" Width="100%" runat="server"  Font-Underline="false"
                Text="Fixer prix" OnClick="lknFixPrice_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
