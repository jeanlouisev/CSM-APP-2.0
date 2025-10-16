<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeacherMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.TeacherMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknRegisterTeacher" Width="100%" runat="server" Text="Enregistrer"
                OnClick="lknRegisterTeacher_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchTeacher" Width="100%" runat="server" Text="Rechercher"
             OnClick="lknSearchTeacher_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknAffectCousrsToTeacher" Width="100%" runat="server" Text="Affecter Cours"
               OnClick="lknAffectCousrsToTeacher_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknTeacherDocumentManagement" Width="100%" runat="server" Text="Gestion de documents"
              OnClick="lknTeacherDocumentManagement_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknTransferTeacher" Width="100%" runat="server" Visible="false" Text="Transferer"
              OnClick="lknTransferTeacher_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="false"
                Width="100%" Text="Paneau de Configurations" Visible="false"></asp:LinkButton>
        </td>
    </tr>
</table>
