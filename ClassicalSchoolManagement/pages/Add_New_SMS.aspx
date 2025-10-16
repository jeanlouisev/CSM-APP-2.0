<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_New_SMS.aspx.cs" Inherits="Add_New_SMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
            Skin="Bootstrap">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            border: 0px solid red;
            width: 960px;
        }
    </style>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseDialog() {
            var oWindow = GetRadWindow();
            if (oWindow != null) {
                oWindow.close();
            }
        }

    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Voulez-vous vraiment supprimer ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

        //function removeErrorLabel() {
        //    alert('this test is ok');

        //    var labelError = document.getElementById("lblErrorSmsmEmpty");
        //    if (labelError.visible) {
        //        labelError.style.visibility = "hidden";
        //    }
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadAjaxManager ID="MessageAlertSMS" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="txtMessage">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblErrorSmsmEmpty"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <asp:Panel ID="pnlSearcStaff" runat="server" GroupingText="Ajouter Model Message" CssClass="panellDesign">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            </telerik:RadAjaxManager>

            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
            </telerik:RadAjaxLoadingPanel>

            <div id="MasterWrapper">
                <p style="width: 100%; text-align: center;">
                    <asp:Label Visible="false" Width="250px" Font-Bold="true" runat="server" ID="lblErrorSmsmEmpty" ForeColor="red"></asp:Label>
                </p>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 10%; text-align: left;">Contenue :</td>
                        <td style="width: 80%">
                            <telerik:RadTextBox ID="txtMessage" runat="server" Width="100%" TextMode="MultiLine" Skin="Bootstrap"
                                EmptyMessage="Taper votre message.160 caractères maximum  !" Height="50" MaxLength="160">
                            </telerik:RadTextBox>

                             <%--onkeyup="removeErrorLabel()"--%>
                        </td>
                        <td style="width: 10%; text-align: center;">
                            <telerik:RadButton ID="btnAddNewSmsContent" OnClick="btnAddNewSmsContent_Click" runat="server" CausesValidation="true"
                                Text="Ajouter" Skin="Glow">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des Contenus" Visible="false" CssClass="panellDesign">
            <div style="width: 100%">
                <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red">
            PAS DE DONNEES
                </asp:Label>
                <asp:GridView ID="gridListContent" runat="server" AutoGenerateColumns="False"
                    Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                    BorderWidth="1px" AllowPaging="false" Width="100%"
                    OnRowCommand="gridListContent_RowCommand" DataKeyNames="id"
                    BackColor="White" BorderColor="Tan"
                    GridLines="Both"
                    OnRowDataBound="gridListContent_RowDataBound"
                    OnRowDeleting="gridListContent_RowDeleting">
                    <RowStyle Height="20px" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="No" Visible="true">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" HorizontalAlign="Left" BorderColor="White" BorderWidth="0" />
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="message_content" HeaderText="Contenu Message">
                            <HeaderStyle Width="300px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="btnDelete"
                                    OnClientClick="Confirm()" OnClick="removeContent" ToolTip="Supprimer"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                    <HeaderStyle Height="22px" Width="960px" CssClass="gridHeaderDesign" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" HorizontalAlign="Center" />
                    <RowStyle Height="5px" Font-Size="Smaller" />
                    <SelectedRowStyle BackColor="Aquamarine" ForeColor="GhostWhite" BorderColor="White" BorderStyle="None" />
                    <SortedAscendingCellStyle BackColor="SkyBlue" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
            </div>
            <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
        </asp:Panel>
    </form>




</body>
</html>
