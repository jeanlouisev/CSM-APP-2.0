<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Erreur</title>
    <link rel="icon" href="../images/saphir_logo.jpg">
    <style>
        .wrapping {
            margin: auto;
            text-align: center;
            width: 500px;
            height: 300px;
            -- border: 0.5px solid silver;
            margin-top: 100px;
        }

        .sorryMsgDesign {
            font-size: large;
            color: darkmagenta;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="wrapping">
            <table style="width: 100%" border="0">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" Width="80px" Height="60px" ImageUrl="~/images/oops_1.jpeg" /></td>
                    <td class="sorryMsgDesign">Votre session a expirée !!!</td>
                    <td>
                        <telerik:RadButton runat="server" Text="Retour à la page de connexion"
                            Skin="Web20" OnClick="btnBackToLogin_Click" ID="btnBackToLogin">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
