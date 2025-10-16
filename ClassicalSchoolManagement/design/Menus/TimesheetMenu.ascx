<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimesheetMenu.ascx.cs"
    Inherits="ClassicalSchoolManagement.design.Menus.TimesheetMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknPresenceSheet" Width="100%" runat="server" Font-Underline="false"
                Text="Feuille de presence" OnClick="lknPresenceSheet_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchSheets" Width="100%" runat="server"  Font-Underline="false"
                Text="Rechercher"  OnClick="lknSearchSheets_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknReasonSheets" Width="100%" runat="server"  Font-Underline="false"
                Text="Motifs" OnClick="lknReasonSheets_Click"></asp:LinkButton>
        </td>
    </tr>
 <%--   <tr>
        <td>
            <asp:LinkButton Font-Underline="false" ID="lknHistorySheets" Width="100%" runat="server" Text="Historiques" PostBackUrl="~/pages/HistorySheets.aspx"></asp:LinkButton>
        </td>
    </tr>--%>
</table>

