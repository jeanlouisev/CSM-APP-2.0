<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogSalaryTeacherConfiguration.aspx.cs"
    Inherits="DialogSalaryTeacherConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration taxes des professeurs</title>
    <!--connec to main css file-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />

    <style type="text/css">
        .labelDesign {
            font-size: small;
            font-family: sans-serif;
        }

        #photoContainer {
            width: 100%;
            float: right;
            height: 70%;
            border: 1px solid silver;
            margin-bottom: 20px;
            -webkit-box-shadow: 0px 0px 30px silver;
            -moz-box-shadow: 0px 0px 30px silver;
            box-shadow: 0px 0px 30px silver;
        }

        .hideUploadButton {
            visibility: hidden;
        }

        .mainDiv {
            margin: auto;
            width: 900px;
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

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

    </script>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007" DestroyOnClose="false">
                </telerik:RadWindow>
                <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" MaxWidth="500" MaxHeight="500" MinHeight="500" MinWidth="500"
                    Skin="Office2007" DestroyOnClose="false">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <div class="mainDiv">
            <asp:Panel ID="pnlSearchStaff" runat="server" Width="100%" GroupingText="Configurer taxes des professeurs" CssClass="panellDesign">
                <table align="center" border="0" style="width: 80%;" runat="server" id="tblStaffPersonel">
                    <tr>
                        <td colspan="1" style="width: 10%;">
                            <asp:Label ID="lblCode" runat="server" Text="Code" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 40%;">
                            <telerik:RadTextBox ID="txtCode" runat="server" Width="250px" Skin="Bootstrap"></telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="width: 10%; text-align: center;">
                            <asp:Label ID="lblFullname" runat="server" Text="Nom" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 40%">
                            <telerik:RadTextBox ID="txtFullname" runat="server" Width="250px" Skin="Bootstrap"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%"></td>
                        <td style="text-align: center; width: 60%;">
                            <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                                Text="Rechercher" Width="100px" Skin="Glow">
                            </telerik:RadButton>
                        </td>
                        <td colspan="1" style="width: 20%; text-align: right;">
                            <asp:LinkButton runat="server" Font-Bold="true" Text="Exporter vers excel" Visible="false"
                                ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlResult" runat="server" Width="100%" GroupingText="" Visible="False" CssClass="panellDesign">
                <div runat="server" id="divSearchPersonel">
                    <div>
                        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                        <asp:GridView ID="gridListTeacher" runat="server" AutoGenerateColumns="False"
                            Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px"
                            AllowPaging="false" Width="100%"
                            OnRowCommand="gridListTeacher_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                            GridLines="Both" OnRowDataBound="gridListTeacher_RowDataBound">
                            <RowStyle Height="10px" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="left" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="CODE">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Width="100px" Font-Size="Medium"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="fullName" HeaderText="NOM">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Width="200px" Font-Size="Medium" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="TAXE GROUPE" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hiddenTaxGroupName" Value='<%# Eval("group_name_id").ToString() %>' />
                                        <telerik:RadComboBox Skin="Bootstrap" runat="server" Width="95%" ID="ddlTaxGroup">
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" Width="100px" Text="Valider" Height="30"
                                            CommandName="validateAmount" ToolTip="Valider Montant" Skin="Web20"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Tan" Height="40px" />
                            <HeaderStyle Height="22px" CssClass="gridHeaderDesign" Width="940px" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" />
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
                </div>
            </asp:Panel>


        </div>
    </form>
</body>
</html>
