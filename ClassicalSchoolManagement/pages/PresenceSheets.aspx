<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PresenceSheets.aspx.cs" EnableEventValidation="false"
    Inherits="PresenceSheet" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Presence Sheets
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

        .trStyle {
            height: 30px;
        }
    </style>

    <script type="text/javascript">

        //toggle button click maximize page
        $(document).ready(function () {
            $('#toggleLink').click();
        });



        function ShowDialogSearchStudent() {
            var oWnd = window.radopen("TimesheetDetails.aspx", "RadWindowSearchStudent");
            oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
            oWnd.SetSize(1024, 600);
            oWnd.center();
        }

        function ClientCloseSearchStudent(oWnd, args) {
        // $find("<%-- = RadAjaxManager1.ClientID --%>").ajaxRequest();
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
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGrildListReferenceStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReportType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCodeReportSt" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationReportSt" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlClassroomReportSt" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindowSearchStudent" runat="server" Modal="True" OnClientClose="ClientCloseSearchStudent"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>




    <div class="row">
        <div class="col-md-6">
            <telerik:RadTabStrip CausesValidation="false" RenderMode="Lightweight"
                runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
                <Tabs>
                    <telerik:RadTab Text="FP | Eleve"></telerik:RadTab>
                    <telerik:RadTab Text="RP | Eleve"></telerik:RadTab>
                    <telerik:RadTab Text="FP | Professeur"></telerik:RadTab>
                    <telerik:RadTab Text="RP | Professeur"></telerik:RadTab>
                    <telerik:RadTab Text="FP | Personnel"></telerik:RadTab>
                    <telerik:RadTab Text="RP | Personnel"></telerik:RadTab>
                    <telerik:RadTab Text="Liste des motifs d'absence"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">

                <%--page 1 [ STUDENT TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblClassRoom" runat="server" Text="Classe"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlClassroom" runat="server" Width="100%" Skin="Office2007" DropDownHeight="200px">
                                        <%-- <Items>
                                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                        </Items>--%>
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="RadLabel17" runat="server" Text="Vacation"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Office2007">
                                        <Items>
                                            <telerik:DropDownListItem Value="AM" Text="Matin" />
                                            <telerik:DropDownListItem Value="PM" Text="Median" />
                                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                    <telerik:RadDatePicker runat="server" ID="radSheetDateStudent" Width="100%" Skin="Web20">
                                        <DateInput DateFormat="dd/MM/yyyy" runat="server" ReadOnly="true"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <button type="button" class="btn btn-sm btn-primary" id="btnSearchStudent" runat="server"
                                        onserverclick="btnSearchStudent_ServerClick" style="width: 120px;">
                                        <span class="fa fa-search"></span>
                                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                                    &nbsp;
                                    <button type="button" class="btn btn-sm btn-success" id="btnValidateStudent" runat="server"
                                        onserverclick="btnValidateStudent_ServerClick" style="width: 120px;">
                                        <span class="fa fa-check"></span>
                                        <asp:Literal runat="server" Text="Valider"></asp:Literal></button>

                                    <%--                                    <button type="button" class="btn btn-default" id="btnExportExcel" runat="server"
                                        onserverclick="btnExportExcel_Click">
                                        <span class="fa fa-file-excel-o"></span>
                                        <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>--%>
                                </div>
                            </div>

                            <br />

                            <%-- STUDENT GRID --%>

                            <br />
                            <div class="row">
                                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                        ID="radGridStudentTimesheets"
                                        OnItemCommand="radGridStudentTimesheets_ItemCommand"
                                        OnItemDataBound="radGridStudentTimesheets_ItemDataBound"
                                        Font-Size="Small">
                                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                                            DataKeyNames="student_code">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("class_id").ToString().Trim() %>' />
                                                        <asp:HiddenField runat="server" ID="hiddenVacation" Value='<%# Eval("vacation").ToString().Trim() %>' />
                                                        <asp:HiddenField runat="server" ID="hiddenAbsenceReasonId" Value='<%# Eval("absence_reason_id").ToString().Trim() %>' />
                                                        <asp:HiddenField runat="server" ID="hiddenPresenceStatus" Value='<%# Eval("presence_status").ToString().Trim() %>' />
                                                        <asp:HiddenField runat="server" ID="hiddenSheeDateStudent" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation_name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Code" DataField="student_code">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nom & Prénom" DataField="fullname">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Date">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSheetDate"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Valide">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Image runat="server" ID="validationImage" ToolTip="Voir feuille de presence professeur"
                                                            ImageUrl='<%# Eval("validation_status").ToString().Trim() == "1" ? "~/images/green_status.png"  : "~/images/red_status.png"  %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Motifs Absence">
                                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox runat="server" ID="cboReasonTypeStudent" Skin="Office2007"
                                                            Width="100%" MaxHeight="200px" Font-Size="Small" DropDownWidth="300"
                                                            CausesValidation="false" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cboReasonTypeStudent_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Status présence">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox runat="server" ID="cboPresenceStatusStudent" Skin="Office2007" Width="100%"
                                                            CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="cboPresenceStatusStudent_SelectedIndexChanged">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="1" Text="Present" />
                                                                <telerik:RadComboBoxItem Value="-1" Text="Absent" Font-Bold="true" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                                            id="btnDelete" onserverclick="deleteTImeSheetStudent">
                                                            <span class="fa fa-remove"></span>
                                                        </button>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>

                <%--page 2 [ STUDENT REPORT TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView2">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="Label3" runat="server" Text="Type de rapport"></asp:Label>
                                    <telerik:RadDropDownList ID="cboReportType" runat="server" Width="100%" Skin="Office2007"
                                        CausesValidation="false" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboReportType_SelectedIndexChanged">
                                        <Items>
                                            <telerik:DropDownListItem Value="INDIVIDUAL" Text="Individuel" />
                                            <telerik:DropDownListItem Value="GROUP" Text="Groupe" Selected="true" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label4" runat="server" Text="Code"></asp:Label>
                                    <telerik:RadTextBox ID="txtCodeReportSt" runat="server" Width="100%" CssClass="toUpper" Enabled="false">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label2" runat="server" Text="Classe"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlClassroomReportSt" runat="server"
                                        Width="100%" Skin="Office2007" ExpandDirection="Down">
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" runat="server" Text="Vacation"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlVacationReportSt" runat="server" Width="100%" Skin="Office2007">
                                        <Items>
                                            <telerik:DropDownListItem Value="AM" Text="Matin" Selected="true" />
                                            <telerik:DropDownListItem Value="PM" Text="Median" />
                                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="Label6" runat="server" Text="Date de debut"></asp:Label>
                                    <telerik:RadDatePicker runat="server" ID="radFromDateReportSt" Width="100%" Skin="Web20">
                                        <DateInput DateFormat="dd/MM/yyyy" runat="server" ReadOnly="true"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label7" runat="server" Text="Date de fin"></asp:Label>
                                    <telerik:RadDatePicker runat="server" ID="radToDateReportSt" Width="100%" Skin="Web20">
                                        <DateInput DateFormat="dd/MM/yyyy" runat="server" ReadOnly="true"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>


                            <br />
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <button type="button" class="btn btn-sm btn-primary" id="btnSearhStudentTimesheetsReport" runat="server"
                                        onserverclick="btnSearhStudentTimesheetsReport_ServerClick" style="width: 120px;">
                                        <span class="fa fa-search"></span>
                                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                                    &nbsp;
                                    <button type="button" class="btn btn-sm btn-default" id="btnExpExcelReportSt" runat="server"
                                        onserverclick="btnExpExcelReportSt_ServerClick">
                                        <span class="fa fa-file-excel-o"></span>
                                        <asp:Literal runat="server" Text="Exporter en excel"></asp:Literal></button>
                                    <%--   &nbsp;
                                    <button type="button" class="btn btn-sm btn-warning" id="btnExpPdfReportSt" runat="server"
                                        onserverclick="btnExpPdfReportSt_ServerClick">
                                        <span class="fa fa-file-pdf-o"></span>
                                        <asp:Literal runat="server" Text="Exporter en PDF"></asp:Literal></button>--%>
                                </div>
                            </div>

                            <br />

                            <%-- STUDENT REPORT GRID --%>

                            <br />
                            <div class="row">
                                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                        ID="radGridStudentTimesheetsReport"
                                        OnItemCommand="radGridStudentTimesheetsReport_ItemCommand"
                                        OnItemDataBound="radGridStudentTimesheetsReport_ItemDataBound"
                                        Font-Size="Small">
                                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                                            DataKeyNames="student_code">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Code" DataField="student_code">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nom & Prénom" DataField="fullname">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation_name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Motifs Absence" DataField="absence_reason">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Status Présence" DataField="presence_status_def">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Date" DataField="sheet_date" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>

                <%--page 3 [ TEACHER TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView3">
                    xxxxxxxxxxxxxxxxxxxxxxx
                </telerik:RadPageView>

                <%--page 4 [ TEACHER REPORT TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView4">
                    xxxxxxxxxxxxxxxxxxxxxxx
                </telerik:RadPageView>

                <%--page 5 [ STAFF TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView5">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <telerik:RadTextBox ID="txtStaffCode" runat="server" Skin="Web20" EmptyMessage="Code employé"></telerik:RadTextBox>
                                    &nbsp;&nbsp;
                                    <telerik:RadDatePicker ID="radWeeks" runat="server" Skin="Web20"></telerik:RadDatePicker>
                                    &nbsp;&nbsp;                                    
                                    <button type="button" id="btnSearchStaff" runat="server" class="btn btn-sm btn-primary"
                                        onserverclick="btnSearchStaff_ServerClick" style="width: 120px;">
                                        <i class="fa fa-search"></i>
                                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal>
                                    </button>
                                    &nbsp;
                                    <button type="button" id="btnValidateStaff" runat="server" class="btn btn-sm btn-success"
                                        onserverclick="btnValidateStaff_ServerClick" style="width: 120px;">
                                        <i class="fa fa-check"></i>
                                        <asp:Literal runat="server" Text="Valider"></asp:Literal>
                                    </button>
                                </div>
                            </div>

                            <br />
                            <div style="width: 100%; height: auto; overflow: scroll;">
                                <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                    ID="radGridTimesheetsStaff"
                                    OnItemCommand="radGridTimesheetsStaff_ItemCommand"
                                    OnItemDataBound="radGridTimesheetsStaff_ItemDataBound"
                                    Width="2400px">
                                    <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                                        DataKeyNames="id">
                                        <ColumnGroups>
                                            <telerik:GridColumnGroup Name="group_monday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_tuesday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_wednesday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_thursday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_friday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_saturday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                            <telerik:GridColumnGroup Name="group_sunday" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            </telerik:GridColumnGroup>
                                        </ColumnGroups>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="No">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="labelNo"></asp:Label>

                                                    <%--*************** hidden weeks dates ****************--%>
                                                    <asp:HiddenField runat="server" ID="hiddenMonDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenTueDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenWedDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenThuDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenFriDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenSatDate" />
                                                    <asp:HiddenField runat="server" ID="hiddenSunDate" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderText="Code employé" DataField="id">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Nom et Prénom" DataField="fullname">
                                                <HeaderStyle Width="80px" />
                                            </telerik:GridBoundColumn>


                                            <%--************************ group monday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_monday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourMon" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_monday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInMon" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_monday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourMon" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_monday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutMon" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group tuesday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_tuesday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourTue" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_tuesday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInTue" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_tuesday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourTue" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_tuesday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutTue" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group wednesday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_wednesday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourWed" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_wednesday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInWed" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_wednesday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourWed" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_wednesday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutWed" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group thursday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_thursday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourThu" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_thursday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInThu" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_thursday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourThu" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_thursday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutThu" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group friday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_friday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourFri" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_friday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInFri" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_friday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourFri" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_friday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutFri" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group saturday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_saturday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourSat" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_saturday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInSat" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_saturday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourSat" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_saturday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutSat" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <%--************************ group sunday ************************--%>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_sunday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Entrée"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radEntryHourSun" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_sunday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInSun" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_sunday">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Heure Sortie"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <telerik:RadTimePicker ID="radExitHourSun" runat="server" Width="100%"></telerik:RadTimePicker>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ColumnGroupName="group_sunday">
                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Sign"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOutSun" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>

                <%--page 6 [ STAFF REPORT TIMESHEETS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView6">
                    xxxxxxxxxxxxxxxxxxxxxxx
                </telerik:RadPageView>

                <%--page 7 [ LISTE DES MOTIFS ] --%>

                <telerik:RadPageView runat="server" ID="RadPageView7">
                    xxxxxxxxxxxxxxxxxxxxxxx
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>

</asp:Content>
