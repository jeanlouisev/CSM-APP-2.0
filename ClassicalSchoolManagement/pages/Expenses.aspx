<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expenses.aspx.cs"
    Inherits="Expenses" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Expenses
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
        .labelDesign1 {
            font-size: smaller;
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">

    function ShowDialogSearchStaff() {
        var oWnd = window.radopen("SearchStaffDetails.aspx", "RadWindow1");
        oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
        oWnd.SetSize(1100, 600);
        oWnd.center();
    }

    function ShowDialogExpenseType() {
        var oWnd = window.radopen("ExpensesType.aspx", "RadWindow1");
        oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
        oWnd.SetSize(600, 600);
        oWnd.center();
    }

    function ClientCloseSearchStaff(oWnd, args) {
        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
    }

    function closeRadWin1(oWnd, args) {
        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
    }

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

    function onMouseOver(rowIndex) {
        var gv = document.getElementById("gridListExpenses");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#c8e4b6";
    }

    function onMouseOut(rowIndex) {
        var gv = document.getElementById("gridListExpenses");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#fff";
    }

    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <%--     <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtStaffCodeTest"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>--%>
        <telerik:AjaxSetting AjaxControlID="txtStaffRequestCode">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtNomOrdonnateur"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtPosition"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnSearchExpense">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridListExpenses" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblCounter"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblFound"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap" OnClientClose="closeRadWin1">
        </telerik:RadWindow>
        <%--  <telerik:RadWindow ID="RadWindowSearchStaff" runat="server" Modal="True" OnClientClose="ClientCloseSearchStaff" Skin="Office2007">
        </telerik:RadWindow>--%>
    </Windows>
</telerik:RadWindowManager>

<asp:Panel ID="pnlDecaisssement" runat="server" GroupingText="Effectuer Décaissement">
    <table align="center" width="100%" runat="server" id="tblExpenses">
        <tr class="trDesign">
            <td colspan="1" style="width: 10%" class="labelDesign1">
                <asp:Label ID="Label2" runat="server" Text="Type "></asp:Label>
            </td>
            <td colspan="1" style="width: 20%; vertical-align: bottom">
                <span style="width: 80%;">
                    <telerik:RadDropDownList runat="server" ID="ddlType" Width="85%" Skin="Bootstrap" DropDownHeight="200" DropDownWidth="350">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="-2" Text="Autre" />
                        </Items>
                    </telerik:RadDropDownList>
                </span>
                <span>
                    <asp:ImageButton runat="server" ID="ImageButton2" ToolTip="Ajouter Types de depense"
                        OnClientClick="ShowDialogExpenseType(); return false;"
                        ImageUrl="~/images/AddNewitem.jpg" Width="20px" Height="20px" />
                </span>
            </td>
            <td colspan="1" style="width: 1%"></td>
            <td colspan="1" style="width: 10%" class="labelDesign1">
                <asp:Label ID="Label4" runat="server" Text="Code Ordonnateur "></asp:Label>
            </td>
            <td colspan="1" style="width: 20%">
                <span style="width: 80%;">
                    <telerik:RadTextBox ID="txtStaffRequestCode" runat="server" Width="85%" MaxLength="50" Skin="Bootstrap"
                        CausesValidation="false" AutoPostBack="true" OnTextChanged="txtStaffRequestCode_TextChanged">
                    </telerik:RadTextBox>
                </span>
                <span style="width: 10%">
                    <asp:ImageButton runat="server" ID="btnSearchStaff" ToolTip="Rechercher Ordonnateur"
                        OnClientClick="ShowDialogSearchStaff(); return false;"
                        ImageUrl="~/images/search_icon1.png" Width="20px" Height="20px" /></span>
            </td>
            <td colspan="1" style="width: 1%"></td>
            <td colspan="1" style="width: 10%" class="labelDesign1">
                <asp:Label ID="Label5" runat="server" Text="Ordonnateur " Enabled="false"></asp:Label>
            </td>
            <td colspan="1" style="width: 20%">
                <telerik:RadTextBox ID="txtNomOrdonnateur" Font-Bold="true" Skin="Bootstrap"
                    runat="server" Width="100%" MaxLength="50" Enabled="false" ForeColor="Maroon">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr class="trDesign">
            <td colspan="1" class="labelDesign1">
                <asp:Label ID="Label7" runat="server" Text="Du " Enabled="false"></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadDatePicker runat="server" ID="radFromDate" Skin="Bootstrap" Width="100%"></telerik:RadDatePicker>
            </td>
            <td colspan="1"></td>
            <td colspan="1" class="labelDesign1">
                <asp:Label ID="Label8" runat="server" Text="Au "></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadDatePicker runat="server" ID="radToDate" Skin="Bootstrap" Width="100%"></telerik:RadDatePicker>
            </td>
            <td colspan="1"></td>
            <td colspan="1" class="labelDesign1">
                <asp:Label ID="Label6" runat="server" Text="Au "></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadTextBox ID="txtPosition" Font-Bold="true" Skin="Bootstrap"
                    runat="server" Width="100%" MaxLength="50" Enabled="false" ForeColor="Maroon">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr class="trDesign">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Description "></asp:Label>
            </td>
            <td colspan="7">
                <telerik:RadTextBox ID="txtDetails" runat="server" Width="100%"
                    EmptyMessage="Tapez votre description ici ..." MaxLength="200" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr class="trDesign">
            <td>
                <asp:Label ID="Label3" runat="server" Text="Montant (HTG) "></asp:Label>
            </td>
            <td colspan="2">
                <telerik:RadNumericTextBox ID="txtAmount" runat="server" Skin="Bootstrap"
                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                    EnabledStyle-HorizontalAlign="Right" Font-Bold="true">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>

    <table style="width: 100%; margin-bottom: 10px">
        <tr style="height: 50px">
            <td colspan="1" style="text-align: center; width: 40%"></td>
            <td colspan="1" style="text-align: center; width: 10%">
                <telerik:RadButton ID="btnSearchExpense" runat="server" Text="Rechercher" Width="100%"
                    Skin="Glow" OnClick="btnSearchExpense_Click">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="width: 2%"></td>
            <td colspan="1" style="text-align: center; width: 10%">
                <telerik:RadButton ID="btnAddPayment" runat="server" Text="Effectuer" Width="100%"
                    Skin="Glow" OnClick="btnAddExpenses_Click">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="text-align: right; width: 40%">
                
                <asp:LinkButton runat="server" Text="Exporter vers excel" Visible="false"
                    ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnlResult" runat="server" CssClass="panellDesign">
    <div>
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListExpenses" runat="server" AutoGenerateColumns="False"
            Style="overflow: auto;" CellPadding="2" ShowFooter="true"
            ForeColor="Black"
            AllowPaging="false" Width="100%" DataKeyNames="id"
            OnRowCommand="gridListExpenses_RowCommand"
            BackColor="White" BorderColor="Tan"
            GridLines="Both"
            OnRowDataBound="gridListExpenses_RowDataBound">
            <RowStyle Height="10px" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                </asp:TemplateField>
                <asp:BoundField DataField="description" HeaderText="TYPE">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="details" HeaderText="COMMENTAIRES">
                    <HeaderStyle Width="230px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="230px" />
                </asp:BoundField>
                <%--      <asp:BoundField DataField="amount" HeaderText="MONTANT" DataFormatString="{0:###,###,###.00}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>--%>

                <asp:TemplateField HeaderText="MONTANT" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAmount" Text='<%# Double.Parse(Eval("amount").ToString()) %>' Font-Bold="true" ForeColor="Red"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblTotalAmount"></asp:Label>
                    </FooterTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    <FooterStyle HorizontalAlign="Right" Font-Size="Small" />
                </asp:TemplateField>

                <asp:BoundField DataField="staff_request_code" HeaderText="CODE ORDONNATEUR">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="staff_request_name" HeaderText="ORDONNATEUR">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="staff_request_position" HeaderText="POSITION">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="staff_received_name" HeaderText="EXECUTER PAR">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="transaction_date" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="ImageButton1"
                            OnClientClick="Confirm()" OnClick="removeExpense" ToolTip="Cliquer pour supprimer"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#3678A3" BorderStyle="None" Height="30px" HorizontalAlign="Center" ForeColor="White" Font-Bold="true" />
            <HeaderStyle Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" HorizontalAlign="Center" />
            <RowStyle Height="5px" Font-Size="Smaller"/>
            <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver" BorderStyle="None" />
            <SortedAscendingCellStyle BackColor="SkyBlue" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
    </div>
    <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
</asp:Panel>

</asp:Content>
