<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendBirthdaySMS.aspx.cs" Inherits="sendBirthdaySMS" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Birthday SMS Management</title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        .divWrapper {
            width: 400px;
            height: auto;
            border: 0px solid red;
            margin: auto;
        }

        .txtMessageDesign {
            width: 100%;
            height: 100px;
            font-family: sans-serif;
            font-size: small;
        }

        .printchatboxDesign {
            text-align: right;
            font-size: smaller;
            font-weight: bold;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnSend">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="txtMessageContent"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="btnSend"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>

        <div runat="server" id="tbl_multiple_sms" visible="false"
            style="border: 0px solid blue; text-align: center; width: 100%; height: 150px; float: left; margin-bottom: 100px;">
            <asp:Image runat="server" ImageUrl="~/images/birthday_animation1.gif" Width="100" Height="50" />
            <textarea runat="server" id="txtAllStaff" style="height: 100%; width: 100%; background-color: white;" disabled="disabled"></textarea>
        </div>
        <div style="width: 100%; border: 0px solid red; height: 200px; float: left;">
            <table border="0" style="width: 100%" runat="server" id="tbl_single_sms">
                <tr style="height: 150px">
                    <td colspan="2" style="text-align: center;">
                        <asp:Image runat="server" ImageUrl="~/images/birthday_animation1.gif" Width="200" Height="100" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 35%; font-weight: bold;">Code : 
                    </td>
                    <td style="width: 65%">
                        <asp:Label runat="server" ID="lblCode"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Nom & Prenom : 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFullname"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Position : 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblPosition"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Telephone : 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblPhone"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;"></td>
                </tr>
            </table>
            <table border="0" style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div id="printchatbox" class="printchatboxDesign"></div>
                        <textarea runat="server" rows="6" id='txtMessageContent' placeholder="Tapez votre text ici ..."
                            class="txtMessageDesign" autofocus="autofocus" maxlength="120"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;"></td>
                </tr>
                <tr style="height: 50px;">
                    <td colspan="2" style="text-align: center;">
                        <telerik:RadButton Skin="Glow" runat="server" ID="btnSend"
                            Text="Envoyer SMS" Width="40%" OnClick="btnSend_Click">
                        </telerik:RadButton>
                        <br />
                        <asp:Label runat="server" ID="lblError" Font-Bold="true" Font-Italic="true" ForeColor="Red"></asp:Label>
                    </td>

                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
        var inputBox = document.getElementById('txtMessageContent');
        inputBox.onkeyup = function () {
            document.getElementById('printchatbox').innerHTML = inputBox.maxLength - inputBox.value.length + " Charactère(s)";
            document.getElementById('lblError').style.visibility = false;
        }
    </script>
</body>
</html>
