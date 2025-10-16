<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParameterMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.ParameterMenu" %>
<table border="0" style="width: 100%" class="xxDesign">
   <tr>
        <td>
            <asp:LinkButton ID="lknManageGroupe" Width="100%" runat="server"  Font-Underline="false"
                Text="Gestion Groupes" OnClick="lknManageGroupe_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknManageUsers" Width="100%" runat="server" Text="Gestion Utilisateurs" 
                OnClick="lknManageUsers_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
   <%-- <tr>
        <td>
            <asp:LinkButton ID="lknEmailBroadcasts" Font-Underline="false" Width="100%" runat="server" Text="Email" PostBackUrl="~/pages/EmailBroadcasts.aspx"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSmsBroadcasts" Font-Underline="false" Width="100%" runat="server" Text="SMS" PostBackUrl="~/pages/SMSBroadcasts.aspx"></asp:LinkButton>
        </td>
    </tr>--%>
 <tr>
        <td>
            <asp:LinkButton ID="lknAccademicYear" Width="100%" runat="server"  Font-Underline="false"
                Text="Année académique" OnClick="lknAccademicYear_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
