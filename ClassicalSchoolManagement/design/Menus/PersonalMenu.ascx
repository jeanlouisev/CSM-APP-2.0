<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalMenu.ascx.cs"
    Inherits="ClassicalSchoolManagement.design.Menus.PersonalMenu" %>

<table border="0" style="width: 100%" class="xxDesign">
    <tr>
        <td>
            <asp:LinkButton ID="lknAddPersonal" Width="100%" runat="server" Text="Ajouter"
                OnClick="lknAddPersonal_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknSearchPersonal" Width="100%" runat="server" Text="Rechercher"
                OnClick="lknSearchPersonal_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lknPersonalDocumentManagement" Font-Underline="false"
                Width="100%" runat="server" Text="Gestion de documents"
                OnClick="lknPersonalDocumentManagement_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknModifyPersonal" Font-Underline="false" Width="100%" runat="server"
                Text="Modifier" OnClick="lknModifyPersonal_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknTransferPersonal" Width="100%" runat="server"
                Text="Transferer" OnClick="lknTransferPersonal_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton Visible="false" ID="lknHistoryPersonal" Width="100%" runat="server"
                Text="Historiques" OnClick="lknHistoryPersonal_Click" Font-Underline="false"></asp:LinkButton>
        </td>
    </tr>
</table>
