<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Define_payment_Type.aspx.cs" Inherits="Define_payment_Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search staff Details</title>
    <link rel="icon" href="../images/saphir_logo.jpg">
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <!-- Main CSS-->
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <!-- Font-icon css-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <link rel="stylesheet" type="text/css" href="../bootstrap/css/font-awesome.min.css" />

    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            border: 0px solid red;
            width: 960px;
            margin-top: 20px;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            overflow: hidden;
            z-index: 10;
            top: expression(<%# gridListPaymentType.HeaderRow %>.offsetParent.scrollTop-2);
        }

        body {
            background-color: whitesmoke;
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

   <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
            Skin="Bootstrap">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="MasterWrapper">
            <asp:Panel ID="pnlSearchStaff" runat="server" GroupingText="Definir Paiement et privilège " CssClass="panellDesign">
                <hr style="width: 100%" />

               <table border="0" style="width: 100%;">
    <tr>
        <td>
            <div id="divUploadDocuments" style="border: 0px solid red; width: 40%; float: left;">
                <asp:Panel ID="pnlAddPaiement" runat="server" GroupingText="Paiement">
                    <table border="0" style="width: 100%" align="center">
                        <tr class="trDesign">
                            <td colspan="1" style="width: 100px">
                                <asp:Label ID="lblDescription" runat="server" Text="Description :"
                                    Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 100px">
                                <telerik:RadTextBox ID="txtDescription" runat="server" Width="250px" MaxLength="50"
                                    CssClass="upperCaseOnly" Skin="Bootstrap">
                                </telerik:RadTextBox>
                            </td>

                        </tr>
                        <tr class="trDesign">
                            <td colspan="1" style="width: 100px">
                                <asp:Label ID="Label6" runat="server" Text="Montant :"
                                    Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 100px">
                                <telerik:RadNumericTextBox ID="txtAmount" ForeColor="Red" Font-Bold="true"
                                    runat="server" Width="250px" EnabledStyle-HorizontalAlign="Right"
                                    DisabledStyle-Font-Italic="true" Skin="Bootstrap">
                                    <NumberFormat GroupSizes="3" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" align="center" style="margin-top: 10px; width: 100%">
                        <tr>
                            <td colspan="1" align="center">
                                <telerik:RadButton ID="btnAddPayment" runat="server" Text="Ajouter" Width="100px" Skin="Glow"
                                    OnClick="btnAddPayment_Click">
                                </telerik:RadButton>
                            </td>

                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="border-top: 0px; border-bottom: 0px; border-right: 0px; border-left: 1px solid blue; width: 60%; float: left; padding-left: 20px">
                <asp:Panel ID="pnlSearchStudent" runat="server" GroupingText="privilège">
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
                                <asp:Label ID="RadLabel1" runat="server" Text="Inscription  :"
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
                                <asp:Label ID="lblFrais_entree" runat="server" Text="Frais Entree :" Skin="Default" CssClass="labelDesign"></asp:Label>
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
                                <asp:Label ID="lblToDate" runat="server" Text="Vers / Mens :" Skin="Default" CssClass="labelDesign"></asp:Label>
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
                               <%-- <telerik:RadButton ID="btnSearchStudent" runat="server" Text="Attacher" Width="100px"
                                    Skin="Glow" OnClick="btnAddPaymentSpeciale_Click" Visible="false">
                                </telerik:RadButton>--%>
                                <asp:Button runat="server" Text="Attacher" OnClientClick="ShowDialogSearchSpecialStudent(); return false;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

        </td>
    </tr>

</table>

            </asp:Panel>
               <hr style="width: 100%" />


<asp:Panel ID="pnlResult" runat="server" GroupingText="Liste  Paiement" Visible="false">
  <%--  <div runat="server" id="divGridHeader" class="divGridHeader">
        <table border="0" runat="server" id="tblGridHeader" class="tblGridHeader" style="text-align: left;">
            <tr>
                <td style="width: 90px; font-weight: bold;">No</td>
                <td style="width: 460px; font-weight: bold;">Description</td>
                <td style="width: 370px; font-weight: bold;">Montant</td>
            </tr>
        </table>
    </div>--%>
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
            
        </div>
    </form>
</body>
</html>

