<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleMenu.ascx.cs"
    Inherits="ClassicalSchoolManagement.design.Menus.ScheduleMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknDefineShedule" Width="100%" runat="server" Font-Underline="false"
                Text="Definir Horaire" OnClick="lknDefineShedule_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknListSchedule" Width="100%" runat="server" Font-Underline="false"
                Text="Lister Horaires Professeur" OnClick="lknListSchedule_Click"></asp:LinkButton>
        </td>
    </tr>  
    <%-- 
    <tr>
        <td>
            <asp:LinkButton ID="lknModifySchedule" Width="100%" runat="server" Font-Underline="false"
                Text="Modifier Horaire" OnClick="lknModifySchedule_Click"></asp:LinkButton>
        </td>
    </tr>--%>
</table>

