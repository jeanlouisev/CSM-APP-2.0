<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogDefinePaymentType.aspx.cs" Inherits="DialogDefinePaymentType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration Type Paiement</title>
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
            width: 1100px;

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

    <script type="text/javascript">
    function ShowDialogSearchSpecialStudent() {
        var oWnd = window.radopen("SearchSpecialStudentDetails.aspx", "RadWindowSearchStudent");
        oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
        oWnd.SetSize(1030, 528);


        oWnd.center();
    }


</script>


</head>
<body>
    <form id="form1" runat="server">
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


        <table border="0" style="width: 100%;">
    <tr>
        <td>
            <div id="divUploadDocuments" style="border: 0px solid red; width: 35%; float: left;">
                <asp:Panel ID="pnlAddPaiement" runat="server" GroupingText="Frais">
                    <table border="0" style="width: 100%" align="center">
                        <tr class="trDesign">
                            <td colspan="1" style="width:20%">
                                <asp:Label ID="lblDescription" runat="server" Text="Description :"
                                    Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="3" style="width: 100px">
                                <telerik:RadTextBox ID="txtDescription" runat="server" Width="100%" MaxLength="50"
                                    CssClass="upperCaseOnly" Skin="Bootstrap">
                                </telerik:RadTextBox>
                            </td>

                        </tr>
                        <tr class="trDesign">
                            <td colspan="1" style="width:25%">
                                <asp:Label ID="Label6" runat="server" Text="Montant :"
                                    Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 54px">
                                <telerik:RadNumericTextBox ID="txtAmount" ForeColor="Red" Font-Bold="true"
                                    runat="server" Width="100%" EnabledStyle-HorizontalAlign="Right"
                                    DisabledStyle-Font-Italic="true" Skin="Bootstrap">
                                    <NumberFormat GroupSizes="3" />
                                </telerik:RadNumericTextBox>
                            </td>

                                <td colspan="1" style="width: 49px"><asp:Label ID="Label5" runat="server" Text="Annee :" Enabled="false"></asp:Label></td>


                        <td colspan="1" style="width: 25px">
                                     <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="100%"
                              CausesValidation="false" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>

                            



                        </tr>
                    </table>
                    <table border="0" align="right" style="margin-top: 10px; width: 75%">
                        <tr>
                            <td colspan="1" style="width: 50%" align="right">
                                <telerik:RadButton ID="btnSearchPayment" runat="server" Text="Rechercher" Width="100%" Skin="Glow"
                                    OnClick="btnsearchPayment_Click">
                                </telerik:RadButton>
                                     
                            </td>
                             <td colspan="1" style="width: 50%" align="left">
                                <telerik:RadButton ID="btnAddPayment" runat="server" Text="Ajouter" Width="100%" Skin="Glow"
                                    OnClick="btnAddPayment_Click">
                                </telerik:RadButton>
                                     
                            </td>

                        </tr>
                    </table>
                </asp:Panel>
            </div>

            <div style="border-top: 0px; border-bottom: 0px; border-right: 0px; border-left: 0px solid blue; width: 63%; float: right; padding-left: 5px">
                <asp:Panel ID="pnlSpecialPayment" runat="server" GroupingText="Privilèges">
                    <table border="0" style="width: 100%">
                        <tr class="trDesign">
                            <td colspan="1" style="width: 100px">
                                <asp:Label ID="lbltype" runat="server" Text="Type"
                                    Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 100px">
                                <telerik:RadDropDownList runat="server" ID="ddlTypeSpecial" Width="100%" CausesValidation="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTypeSpecial_OnSelectedIndexChanged" Skin="Bootstrap">
                                    <Items>
                                        <telerik:DropDownListItem Value="-2" Text="BOURSE" Selected="true" />
                                        <telerik:DropDownListItem Value="-3" Text="DEMI_BOURSE" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                            <td colspan="1" style="width: 10px"></td>
                            <td colspan="1" style="width: 100px">
                                <asp:Label ID="RadLabel1" runat="server" Text="Inscription % :"
                                    Skin="Default" CssClass="labelDesign" ReadOnly="true"></asp:Label>
                            </td>
                            <td colspan="1">
                                <telerik:RadNumericTextBox ID="Txtpercentage_Inscription" ForeColor="Red" Font-Bold="true"
                                    runat="server" Width="90%" EnabledStyle-HorizontalAlign="left"
                                    DisabledStyle-Font-Italic="true" Skin="Bootstrap" ReadOnly="true">
                                <NumberFormat GroupSizes="3" /> 
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr class="trDesign">
                            <td colspan="1">
                                <asp:Label ID="lblFrais_entree" runat="server" Text="Frais Entree % :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1">
                                <telerik:RadNumericTextBox ID="Txtpercentage_CommingFee" ForeColor="Red" Font-Bold="true"
                                    runat="server" EnabledStyle-HorizontalAlign="left"
                                    DisabledStyle-Font-Italic="true" Skin="Bootstrap" ReadOnly="true">
                                    <NumberFormat GroupSizes="3" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1">
                                <asp:Label ID="lblToDate" runat="server" Text="Vers / Mens %:" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1">
                                <telerik:RadNumericTextBox ID="Txtpercentage_Monthly" ForeColor="Red" Font-Bold="true"
                                    runat="server" EnabledStyle-HorizontalAlign="left" Width="90%"
                                    DisabledStyle-Font-Italic="true" Skin="Bootstrap" ReadOnly="true">
                                    <NumberFormat GroupSizes="3" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" align="center" style="margin-top: 10px;" width="100%">
                        <tr>
                             <td colspan="1" align="center">
                                <asp:Button runat="server" Text="Attacher a élève" OnClientClick="ShowDialogSearchSpecialStudent(); return false;" />
                            </td>



                        </tr>
                    </table>
                </asp:Panel>
            </div>

        </td>
    </tr>

</table>

            <br />
        <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des frais" Visible="false"> 
    <div style="width: 100%">
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListPaymentType" runat="server" AutoGenerateColumns="False"
            Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
            OnRowCommand="gridListPaymentType_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
            GridLines="Both" OnRowDataBound="gridListPaymentType_RowDataBound"
            OnRowDeleting="gridListPaymentType_RowDeleting">
            <RowStyle Height="10px" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="No" Visible="true">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="description" HeaderText="Description" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="500px" HorizontalAlign="Left" />
                    <ItemStyle Width="500px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="Montant" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="390px" HorizontalAlign="Right" />
                    <ItemStyle Width="390px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentTypeId" Visible="false" runat="server" Text='<%# Eval("id").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" BorderStyle="None" />
                    <ItemStyle BorderStyle="None" Width="0px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg"
                            OnClientClick="Confirm()" OnClick="removePaymentType"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemStyle HorizontalAlign="Left" Width="30px" BorderStyle="None" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Navy" Font-Bold="True" Height="22px"
                ForeColor="WhiteSmoke" VerticalAlign="Middle" Font-Size="Small" />
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
    <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small"
        Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
    <span style="float: right; border: 0px solid red; text-align: left; margin-right: 20px;">
        <asp:Label runat="server" ID="lblTotalAmount" Visible="false" Font-Size="Small"
            Font-Bold="true" Font-Names="sans-serif" ForeColor="Black" Text="Montant Total :"></asp:Label>
    </span>
</asp:Panel>

 

     
    </form>
</body>
</html>
