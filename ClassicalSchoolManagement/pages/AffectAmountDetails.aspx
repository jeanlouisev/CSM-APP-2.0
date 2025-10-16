<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AffectAmountDetails.aspx.cs" Inherits="AffectAmountDetails" %>

<%--@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affecter Montant</title>

    <style>
        .wrapper {
            margin: auto;
            width: 100%;
            border: 0px solid red;
        }

        .alignRight {
            text-align: right;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="wrapper">
            <asp:HiddenField runat="server" ID="hiddenCode" />
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFullname"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblPosition"></asp:Label></td>
                </tr>
                <tr style="height: 30px;">
                    <td align="center">
                        <asp:Label runat="server" ID="lblErrorMsg" ForeColor="Red" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblSuccess" ForeColor="Green" Visible="false"></asp:Label></td>
                </tr>
                <tr style="height: 70px;">
                    <td>
                        <asp:Panel runat="server" GroupingText="Salaire Actuel" BorderColor="Silver">
                            <telerik:RadNumericTextBox Width="100%" runat="server" ID="txtAmount"
                                EmptyMessage="0.00" CssClass="alignRight" ForeColor="Red" Skin="Bootstrap">
                                <NumberFormat GroupSizes="3" />
                            </telerik:RadNumericTextBox>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <telerik:RadButton Skin="Glow" Text="Valider" ID="btnValidate" OnClick="btnValidate_Click"
                            Width="50%" runat="server">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
