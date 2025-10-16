<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensesType.aspx.cs"
    Inherits="ExpensesType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<head runat="server">
    <title>Expense Type</title>
    <link rel="icon" href="../images/saphir_logo.jpg">
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <!-- Main CSS-->
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <!-- Font-icon css-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <link rel="stylesheet" type="text/css" href="../bootstrap/css/font-awesome.min.css" />
    <style type="text/css">
        .upper {
            text-transform: uppercase;
        }

        .masterDiv {
            width: 550px;
            margin: auto;
        }
    </style>

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
                top.location.href = top.location.href;
            }
        }

    </script>
</head>


<body>
    <form id="form1" runat="server">
        <div class="masterDiv">
            <asp:ScriptManager runat="server"></asp:ScriptManager>

            <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
                ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
                <Windows>
                    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                        Skin="Office2007">
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>

            <asp:Panel ID="pnlAddExpendesType" runat="server" GroupingText="Type Decaissement">
                <table border="0" align="center" width="100%">
                    <tr>
                        <td colspan="1">
                            <asp:Label Width="100px" ID="lbldescription" runat="server" Text="Description :"></asp:Label>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadTextBox ID="txtdescription" runat="server"
                                Width="100%" CssClass="upper" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnAdddecaissementType" runat="server"
                                Text="Ajouter" Width="100px" Skin="Glow" OnClick="btnAddExpensesType_Click">
                            </telerik:RadButton>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnClose" runat="server"
                                Text="Terminer" Width="100px" Skin="Glow" OnClick="btnClose_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Label Font-Size="Small" Visible="true" Font-Italic="true"
                                runat="server" ID="lblErrordescription" ForeColor="red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste" Visible="false">
                <div style="width: 100%; max-height: 350px; overflow: scroll;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListExpende" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListExpende_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" OnRowDataBound="gridListExpende_RowDataBound"
                        OnRowDeleting="gridListExpende_RowDeleting">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" Visible="true">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" HorizontalAlign="Left" BorderColor="White" BorderWidth="0" />
                                <ItemStyle HorizontalAlign="Left" Width="35px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="description" HeaderText="Type Decaissement">
                                <HeaderStyle Width="720px" HorizontalAlign="Left" BorderColor="White" BorderWidth="0" />
                                <ItemStyle Width="700px" HorizontalAlign="Left" BorderWidth="0px" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseTypeId" Visible="false" runat="server" Text='<%# Eval("id").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle BorderWidth="0" Width="0px" BorderStyle="None" />
                                <ItemStyle BorderWidth="0" Width="0px" BorderStyle="None" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg"
                                        OnClientClick="Confirm()" OnClick="removeExpendeType"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                            HorizontalAlign="Center" />
                        <RowStyle Height="5px" Font-Size="Smaller" />
                        <SelectedRowStyle BackColor="Aquamarine" ForeColor="GhostWhite" BorderColor="Silver"
                            BorderStyle="None" />
                        <SortedAscendingCellStyle BackColor="SkyBlue" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </div>
                <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>

            </asp:Panel>
        </div>
    </form>
</body>



