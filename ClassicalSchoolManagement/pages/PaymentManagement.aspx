<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentManagement.aspx.cs"
    Inherits="PaymentManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Payment
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <!-- FastClick -->
    <script src="../plugins/fastclick/fastclick.min.js"></script>
    <!-- AdminLTE App -->
    <script src="../dist/js/app.min.js"></script>
    <!-- Sparkline -->
    <script src="../plugins/sparkline/jquery.sparkline.min.js"></script>
    <!-- jvectormap -->
    <script src="../plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="../plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
    <!-- SlimScroll 1.3.0 -->
    <script src="../plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- ChartJS 1.0.1 -->
    <script src="../plugins/chartjs/Chart.min.js"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="../dist/js/demo.js"></script>
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../bootstrap/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../bootstrap/css/ionicons.min.css">
    <!-- jvectormap -->
    <link rel="stylesheet" href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="../plugins/datatables/dataTables.bootstrap.css">
    <!-- daterange picker -->
    <link rel="stylesheet" href="../plugins/daterangepicker/daterangepicker-bs3.css">
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">


    <style type="text/css">
        .upper {
            text-transform: uppercase;
        }

        .col {
            word-wrap: break-word;
        }
    </style>

    <script type="text/javascript">
        function ShowDialogSearchStudent() {
            var oWnd = window.radopen("SearchStudentDetails.aspx", "RadWindowSearchStudent");
            oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
            oWnd.SetSize(1100, 600);
            oWnd.center();
        }


        function ClientCloseSearchStudent(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }


        function ShowDialogDialogDefinePaymentType() {
            var oWnd = window.radopen("DialogDefinePaymentType.aspx", "RadWindowSearchStudent");
            oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
            oWnd.SetSize(1100, 600);
            oWnd.center();
        }


        function ClientCloseDialogDefinePaymentType(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListPaymentOthers");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListPaymentOthers");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>
    <script type="text/javascript">
        function ShowDialogExecuteScolarite() {
            var oWnd = window.radopen("Execute_Scolarite.aspx", "RadWindowSearchStudent");
            oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
            oWnd.SetSize(1100, 600);
            oWnd.center();
        }

    <%--  function ClientCloseSearchStudent(oWnd, args) {
        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
    }--%>


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

    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblPayment" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtStudentCode">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblPayment" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlMonth">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblPayment" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblPayment" LoadingPanelID="RaFdAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtPaidAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtBalance" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtPaidAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridListPaymentOthers" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>



    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false" Modal="true"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindowSearchStudent" runat="server" Modal="True" OnClientClose="ClientCloseSearchStudent"
            Skin="Bootstrap">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<asp:Panel ID="pnlPayment" runat="server" GroupingText="Effectuer Paiement" CssClass="panellDesign">
    <hr style="width: 80%; text-align: left;" />
    <table border="0" width="80%" runat="server" id="tblPayment">
        <tr class="trDesign">
            <td colspan="1">
                <asp:Label ID="Label4" runat="server" Text="Code Eleve : "></asp:Label>
            </td>
            <td colspan="1" style="width: 20%;">
                <telerik:RadTextBox ID="txtStudentCode" runat="server" Width="100%" MaxLength="50" CssClass="upper"
                    CausesValidation="false" AutoPostBack="true" OnTextChanged="txtStudentCode_TextChanged"
                    Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1" style="text-align: center;">
                <asp:ImageButton runat="server" ID="btnSearchStudent"
                    OnClientClick="ShowDialogSearchStudent(); return false;"
                    ImageUrl="~/images/Staff_search.png" Width="15px"
                    Height="15px" />
            </td>
            <td colspan="1">
                <asp:Label ID="Label3" runat="server" Text="Nom Eleve : "></asp:Label>
            </td>
            <td colspan="1"></td>
            <td colspan="1" style="width: 20%;">
                <telerik:RadTextBox ID="txtStudentFullname" Font-Bold="true"
                    runat="server" Width="100%" MaxLength="50" Enabled="false" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1"></td>
            <td>
                <asp:Label ID="lblDate_" runat="server" Text="Classe : " Enabled="false"></asp:Label></td>

            <td colspan="1">
                <telerik:RadTextBox ID="txtClassroom" Font-Bold="true" runat="server"
                    Width="100%" MaxLength="50" Enabled="false" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1">
                <asp:Label ID="lblstatus" runat="server" Text="Statut: "></asp:Label>
            </td>
            <td colspan="1" style="width: 20%;">
                <telerik:RadTextBox ID="txtStatus" runat="server" Enabled="false" Width="100%" MaxLength="50" CssClass="upper"
                    CausesValidation="false" AutoPostBack="true"
                    Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1" style="text-align: center;"></td>
            <td colspan="1">
                <asp:Label ID="Label6" runat="server" Text="Sexe : "></asp:Label>
            </td>
            <td colspan="1"></td>
            <td colspan="1" style="width: 20%;">
                <telerik:RadTextBox ID="txtSexe" Font-Bold="true"
                    runat="server" Width="100%" MaxLength="50" Enabled="false" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1"></td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Vacation : " Enabled="false"></asp:Label></td>

            <td colspan="1">
                <telerik:RadTextBox ID="txtVacation" Font-Bold="true" runat="server"
                    Width="100%" MaxLength="50" Enabled="false" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1">
                <asp:Label ID="Label8" runat="server" Text="Type Paiement : "></asp:Label>
            </td>
            <td colspan="1" style="width: 20%;">
                <telerik:RadDropDownList runat="server" ID="ddlType" Width="100%" CausesValidation="false"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                    DropDownHeight="100px" DropDownWidth="259px" Enabled="false" Skin="Bootstrap">
                </telerik:RadDropDownList>
            </td>
            <td colspan="1"></td>


            <td>
                <asp:Label ID="lblPrivilege" runat="server" Text="Privillege : " Enabled="false"></asp:Label>

            </td>
            <td colspan="1"></td>

            <td colspan="1" style="width: 10%;">
                <telerik:RadTextBox ID="txtPrivillege" Font-Bold="true"
                    runat="server" Width="100%" MaxLength="50" Enabled="false" Skin="Bootstrap">
                </telerik:RadTextBox>

            </td>
            <td colspan="1"></td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Annee :" Enabled="false"></asp:Label></td>

            <td>
                <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="100%"
                    CausesValidation="false" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>
        </tr>

        <tr class="trDesign">
            <td colspan="1">
                <asp:Label ID="Label" runat="server" Text="Montant Defini : "></asp:Label>
            </td>

            <td colspan="1">
                <telerik:RadNumericTextBox ID="txtDefinedAmount" runat="server" Enabled="false"
                    Font-Bold="true" Skin="Bootstrap"
                    Width="100%" EmptyMessage="0.00" ForeColor="black" MaxLength="50"
                    EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
            <td colspan="1" style="text-align: center;">
                <%--  <asp:ImageButton runat="server" ID="ImageButton2"
                    OnClientClick="ShowDialogSearchStudent(); return false;"
                    ImageUrl="~/images/Edit.jpg" Width="20px"
                    Height="15px" />--%>
            </td>
            <td colspan="1">
                <asp:Label ID="Label1" runat="server" Text="Montant Paye : "></asp:Label>
            </td>
            <td colspan="1"></td>
            <td colspan="1">
                <telerik:RadNumericTextBox ID="txtPaidAmount" runat="server" Skin="Bootstrap"
                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                    EnabledStyle-HorizontalAlign="Right" Font-Bold="true" Enabled="false">
                </telerik:RadNumericTextBox>
            </td>
            <td colspan="1"></td>
            <td colspan="1">
                <asp:Label ID="Label7" runat="server" Text="Balance : "></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadNumericTextBox ID="txtBalance" runat="server" Skin="Bootstrap"
                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                    EnabledStyle-HorizontalAlign="Right" Enabled="false" Font-Bold="true">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>

    <table border="0" align="right" width="15%" runat="server" id="Table1">
        <tr class="trDesign">
            <td colspan="1" style="width: 20%;">
                <telerik:RadButton runat="server" ID="btnDefinePaymentType" Width="98%" Height="35px"
                    Skin="Web20" Text="Configurer Type Paiement" OnClick="btnDefinePaymentType_Click">
                </telerik:RadButton>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1" style="width: 20%;">
                <telerik:RadButton runat="server" ID="btnScolariteAmountConfig" Width="98%" Height="35px"
                    Skin="Web20" Text="Configurer frais  scolarite " OnClick="btnScolariteAmountConfige_Click">
                </telerik:RadButton>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1" style="width: 20%;">
                <telerik:RadButton runat="server" ID="btnSMSactive" Width="98%" Height="35px"
                    Skin="Web20" Text="OPTION SMS ACTIVER">
                </telerik:RadButton>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1" style="width: 20%;"></td>
        </tr>
    </table>
</asp:Panel>

<table style="width: 100%; margin-bottom: 20px; margin-top: 10px;">
    <tr class="trDesign">
        <td style="width: 35%"></td>
        <td style="text-align: center; width: 15%;">
            <telerik:RadButton ID="btnsearchpayment" runat="server" Text="Rechercher" Width="80%"
                Skin="Glow" OnClick="btnSearchPayment_Click">
            </telerik:RadButton>
        </td>
        <td style="text-align: center; width: 15%;">
            <telerik:RadButton ID="btnAddPayment" runat="server" Text="Valider" Width="80%"
                Skin="Glow" OnClick="btnValidateAddPayment_Click">
            </telerik:RadButton>
        </td>
        <td style="width: 35%"></td>
    </tr>
</table>


<asp:Panel ID="pnlResultOthers" runat="server" GroupingText="" Visible="false">
    <div style="width: 100%">
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListPaymentOthers" runat="server" AutoGenerateColumns="False"
            Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
            OnRowCommand="gridListPaymentOthers_RowCommand" BackColor="White" BorderColor="Tan"
            GridLines="Both" OnRowDataBound="gridListPaymentOthers_RowDataBound"
            OnRowDeleting="gridListPaymentOthers_RowDeleting">
            <RowStyle Height="10px" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="No" Visible="true">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="staff_code" HeaderText="Eleve" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="500px" HorizontalAlign="Left" />
                    <ItemStyle Width="500px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="description" HeaderText="Description" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="500px" HorizontalAlign="Left" />
                    <ItemStyle Width="500px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="transaction_date" HeaderText="Date" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="390px" HorizontalAlign="Left" />
                    <ItemStyle Width="390px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="login_user" HeaderText="Utilisateur" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="300px" HorizontalAlign="Left" />
                    <ItemStyle Width="300px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="years" HeaderText="Annee" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="amount_paid" HeaderText="Montant" ControlStyle-Font-Bold="true">
                    <HeaderStyle Width="390px" HorizontalAlign="Left" />
                    <ItemStyle Width="390px" HorizontalAlign="Center" />
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
                            OnClientClick="Confirm()" OnClick="removepaymentOthers"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemStyle HorizontalAlign="Left" Width="30px" BorderStyle="None" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
            <HeaderStyle Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                HorizontalAlign="Center" />
            <RowStyle Height="5px" Font-Size="Smaller" />
            <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                BorderStyle="None" />
            <SortedAscendingCellStyle BackColor="SkyBlue" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
    </div>
    <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small"
        Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
    <span style="float: right; border: 0px solid red; text-align: Center; margin-right: 20px;">
        <asp:Label runat="server" ID="lblTotalAmount" Visible="false" Font-Size="Small"
            Font-Bold="true" Font-Names="sans-serif" ForeColor="Black" Text="Montant Total :"></asp:Label>
    </span>
</asp:Panel>

</asp:Content>
