<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollTeacher.aspx.cs"
    Inherits="PayrollTeacher" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Payroll Professeur
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
        .labelDesign {
            font-size: small;
            --font-weight: bold;
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

        .fullnameDesign {
            background-color: transparent;
            border-left: 0px;
            border-top: 0px;
            border-bottom: 0px;
            border-right: 0px;
            font-size: smaller;
        }

        .amountRight {
            text-align: right;
        }

        .warningDesign {
            font-style: italic;
            font-size: smaller;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            color: purple;
            font-weight: bold;
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


        function ClientCloseStaffAmount(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007" BackColor="Transparent">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
            MaxWidth="300" MaxHeight="300" MinHeight="300" MinWidth="300"
            Skin="Office2007" DestroyOnClose="false" OnClientClose="ClientCloseStaffAmount">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" MaxWidth="1200" MinWidth="1200" MaxHeight="600" MinHeight="600"
            Skin="Office2007" DestroyOnClose="false" OnClientClose="ClientCloseStaffAmount">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>


<asp:Panel ID="pnlSearchStaff" runat="server" GroupingText="Payroll Professeur">
    <hr style="width: 100%; margin: 0px;  margin-bottom: 10px;" />
    <table border="0" style="width: 100%;" runat="server" id="tblStaffPersonel">
        <tr style="height: 40px;">
            <td colspan="1" style="width: 10%; text-align: left;">
                <asp:Label ID="lblCode" runat="server" Text="Code" Skin="Office2007"></asp:Label>
            </td>
            <td colspan="1" style="width: 23%;">
                <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
            </td>
            <td colspan="1" style="width: 10%; text-align: center;">
                <asp:Label ID="lblFirst" runat="server" Text="Nom" Skin="Office2007"></asp:Label>
            </td>
            <td colspan="1" style="width: 23%">
                <telerik:RadTextBox ID="txtFullname" runat="server" Width="95%" Skin="Bootstrap"></telerik:RadTextBox>
            </td>
            <td colspan="1" style="width: 15%">
                <asp:Label ID="RadLabel1" runat="server" Text="Ann&eacutee Academique" Skin="Office2007"></asp:Label>
            </td>
            <td colspan="1" style="width: 18%">
                <telerik:RadDropDownList ID="ddlAcademicYear" runat="server" Width="100%"
                    Skin="Bootstrap" CausesValidation="false" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>
        </tr>
    </table>

     <table align="center" border="0" style="width: 80%; margin-top: 15px;">
        <tr>
            <td colspan="1" style="text-align: center; width: 25%;">
                <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                    Text="Rechercher" Skin="Glow" Width="95%">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="text-align: center; width: 25%;">
                <telerik:RadButton runat="server" ID="btnSalaryConfig" Width="95%" Height="30px"
                    Skin="Sunset" Text="Configurer taxes des professeurs" OnClick="btnSalaryConfig_Click">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="text-align: center; width: 25%;">
                <telerik:RadButton runat="server" ID="btnValidate" Width="95%" Height="30px"
                    Skin="Web20" Text="Calculer Payroll" OnClick="btnCalculatePayroll_Click">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="text-align: center; width: 20%; text-align: right;">                
                <asp:CheckBox runat="server" ID="chkSendSMS" Text="Envoyer SMS" />
            </td>
        </tr>
    </table>
</asp:Panel>
<br />

<asp:Panel ID="pnlResult" runat="server" Visible="False" CssClass="panellDesign">
    <div style="width: 100%; text-align: right;">        
                <asp:LinkButton runat="server" Text="Exporter vers excel" Visible="true" ForeColor="Navy" Font-Bold="true"
                    ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
    </div>
    <div runat="server" id="divSearchPersonel" style="border: 0px solid red; overflow: hidden">
        <div style="overflow-x: scroll; width: 100%;">
            <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
            <asp:GridView ID="gridListTeacher" runat="server" AutoGenerateColumns="False"
                Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                BorderWidth="1px" AllowPaging="false" Width="1800px"
                OnRowCommand="gridListTeacher_RowCommand"
                BackColor="White" BorderColor="Tan"
                GridLines="Both" OnRowDataBound="gridListTeacher_RowDataBound">
                <RowStyle Height="10px" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="left" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ImageUrl="~/images/search_icon1.png"
                                CommandName="viewDetailsStaffPayroll" ToolTip="Visualiser Details Payroll"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="btnDelete"
                                OnClientClick="Confirm()" CommandName="clearAnnualPayrollStaff" ToolTip="Supprimer Payroll annuel"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="CODE">
                        <HeaderStyle Width="100px" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fullName" HeaderText="NOM">
                        <HeaderStyle Width="150px" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="left" Width="150px" />
                    </asp:BoundField>
                    <%------------------------- SEPTEMBER_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkSeptemberAll"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkSeptember"
                                Checked='<%# Convert.ToInt32(Eval("september_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SEPTEMBRE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtSeptembre" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("september_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>

                    <%------------------------- OCTOBER_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkOctoberAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkOctober"
                                Checked='<%# Convert.ToInt32(Eval("october_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OCTOBRE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtOctobre" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("october_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- NOVEMBER_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkNovemberAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNovember"
                                Checked='<%# Convert.ToInt32(Eval("november_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOVEMBRE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtNovembre" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("november_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- DECEMBER_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkDecemberAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkDecember"
                                Checked='<%# Convert.ToInt32(Eval("december_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DECEMBRE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtDecembre" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("december_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- JANUARY_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkJanuaryAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkJanuary"
                                Checked='<%# Convert.ToInt32(Eval("january_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JANVIER" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtJanvier" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("january_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- FEBUARY_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkFebuaryAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkFebuary"
                                Checked='<%# Convert.ToInt32(Eval("febuary_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FEVRIER" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtFevrier" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("febuary_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- MARCH_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkMarchAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkMarch"
                                Checked='<%# Convert.ToInt32(Eval("march_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MARS" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtMars" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("march_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- APRIL_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkAprilAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkApril"
                                Checked='<%# Convert.ToInt32(Eval("april_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AVRIL" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtAvril" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("april_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- MAY_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkMayAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkMay"
                                Checked='<%# Convert.ToInt32(Eval("may_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAI" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtMai" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("may_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- JUNE_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkJuneAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkJune"
                                Checked='<%# Convert.ToInt32(Eval("june_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUIN" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtJuin" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("june_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- JULY_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkJulyAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkJuly"
                                Checked='<%# Convert.ToInt32(Eval("july_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUILLET" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtJuillet" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("july_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <%------------------------- AUGUST_COL ------------------------%>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkAugustAll" CausesValidation="false"
                                AutoPostBack="true" OnCheckedChanged="checkAllByMonth" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAugust"
                                Checked='<%# Convert.ToInt32(Eval("august_check")) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AOUT" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox runat="server" ID="txtAout" EmptyMessage="0.00"
                                ReadOnly="true" Skin="Office2007" Width="100%"
                                ForeColor="Red" CssClass="amountRight" Value='<%# Eval("august_salary") %>'>
                                <NumberFormat GroupSizes="3" DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Tan" Height="40px" HorizontalAlign="Center" />
                <HeaderStyle Height="22px" CssClass="gridHeaderDesign" HorizontalAlign="Center" />
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

    </div>
</asp:Panel>

</asp:Content>
