<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotesMenu.ascx.cs"
    Inherits="ClassicalSchoolManagement.design.Menus.NotesMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknInsertNotes" Width="100%"
                runat="server" Text="Inserer" Font-Underline="false"
                OnClick="lknInsertNotes_Click"></asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td>
            <asp:LinkButton ID="lknSearchNotes" Width="100%"
                runat="server" Text="Rechercher Notes" Font-Underline="false"
                OnClick="lknSearchNotes_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknDownloadNotes" Width="100%"
                runat="server" Text="Télécharger palmarès" Font-Underline="false"
                OnClick="lknDownloadNotes_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknUploaderNotes" Width="100%"
                runat="server" Text="Upload palmarès" Font-Underline="false"
                OnClick="lknUploaderNotes_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknBulletinManagement" Width="100%"
                runat="server" Text="Gestion Bulletin" Font-Underline="false"
                OnClick="lknBulletinManagement_Click"></asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td>
            <asp:LinkButton ID="lknAverageManagement" Width="100%"
                runat="server" Text="Gestion Moyenne Generale" Font-Underline="false"
                OnClick="lknAverageManagement_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknChangingClassManagement" Width="100%"
                runat="server" Text="Changement de Classe" Font-Underline="false"
                OnClick="lknChangingClassManagement_Click"></asp:LinkButton>
        </td>
    </tr>
    <%-- 
    <tr>
        <td>
            <asp:LinkButton ID="lknHistoryNotes" Width="100%" 
            runat="server" Text="Historiques"  Font-Underline="false"
            PostBackUrl="~/pages/HistoryNotes.aspx"></asp:LinkButton>
        </td>
    </tr>
    --%>
</table>
