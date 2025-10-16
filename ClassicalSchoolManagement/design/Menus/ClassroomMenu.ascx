<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassroomMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.ClassroomMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
   <%-- <tr>
        <td>
            <asp:LinkButton ID="lknClassroomManagement" Font-Underline="false" Width="100%" runat="server" Text="Rechercher" PostBackUrl="~/pages/ClassroomManagement.aspx"></asp:LinkButton>
        </td>
    </tr>--%> 
   <tr>
        <td>
            <asp:LinkButton ID="lknCoursToClassroomManagement" Width="100%" 
                runat="server" Text="Gérer Classes" Font-Underline="false"
                OnClick="lknCoursToClassroomManagement_Click"></asp:LinkButton>
        </td>
    </tr> 
</table>
