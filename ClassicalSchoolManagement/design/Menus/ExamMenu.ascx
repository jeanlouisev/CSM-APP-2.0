<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.ExamMenu" %>
<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknAddExams" Width="100%" runat="server" Text="Ajouter" 
               OnClick="lknAddExams_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchExams" Width="100%" runat="server" Text="Rechercher" 
                OnClick="lknSearchExams_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <%-- 
    <tr>
        <td>
            <asp:LinkButton ID="lknModifyExams" Font-Underline="false" Width="100%" runat="server" Text="Modifier" PostBackUrl="~/pages/ModifyExams.aspx"></asp:LinkButton>
        </td>
    </tr>--%>
</table>
