<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.menus.StudentMenu" %>


<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknRegisterStudents" Width="100%"
                runat="server" Text="Inscrire" Font-Underline="false"
                OnClick="lknRegisterStudents_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchStudents" Width="100%"
                runat="server" Text="Rechercher" Font-Underline="false"
                OnClick="lknSearchStudents_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknStudentDocumentManagement" Width="100%"
                runat="server" Text="Gestion de documents" Font-Underline="false"
                OnClick="lknStudentDocumentManagement_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lnkParentManagement" Width="100%"
                runat="server" Text="Gestion Parent" Font-Underline="false"
                OnClick="lnkParentManagement_Click"></asp:LinkButton>
        </td>
    </tr>
    <%--  <tr>
        <td>
            <asp:LinkButton ID="lknStudentReferenceManagement" Width="100%"
                runat="server" Text="Gestion des Parents" Font-Underline="false"
                OnClick="lknStudentReferenceManagement_Click"></asp:LinkButton>
        </td>
    </tr>--%>
    <%-- <tr>
        <td>
            <asp:LinkButton ID="lknSearchReference" Width="100%"
                runat="server" Text="Rechercher Reference" Font-Underline="false"
                OnClick="lknSearchReference_Click" Visible="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknTransferStudent" Width="100%"
                runat="server" Text="Transferer" Font-Underline="false"
                OnClick="lknTransferStudent_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknHistoryStudent" Width="100%"
                runat="server" Text="Historiques" Font-Underline="false"
                OnClick="lknHistoryStudent_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknStudentConfigurationPanel"
                Width="100%" runat="server" Font-Underline="false"
                Text="Paneau de Configurations"></asp:LinkButton>
        </td>
    </tr>--%>
</table>

