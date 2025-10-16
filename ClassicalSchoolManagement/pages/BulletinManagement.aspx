<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulletinManagement.aspx.cs"
    Inherits="BulletinManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Gestion Bulletin
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


        .lower {
            text-transform: lowercase;
        }


        .style1 {
            width: 10px;
        }

        .style2 {
            width: 80px;
        }

        .style3 {
            width: 100px;
        }

        .upperCaseDesign {
            text-transform: uppercase;
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

        function ClientClose(oWnd, args) {

        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListStudent");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListStudent");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkNif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNifReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCinReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacation" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroomStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacationStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCardIdStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtParentIdCard">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlPositionReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlClassRoomReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblCode" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassRoomReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radBirthDateStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radBirthDateStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" CssClass="borderLessDialog"
                runat="server" Modal="true" OnClientClose="ClientClose"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>




    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-file-certificate"></span>&nbsp;Gestion Bulletin</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Code"></asp:Label>
                    <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" 
                        Skin="Office2007" CssClass="upperCaseDesign"></telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Classe"></asp:Label>
                    <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                        Width="100%" Skin="Office2007" ExpandDirection="Down">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Vacation"></asp:Label>
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="AM" Text="Matin" />
                            <telerik:DropDownListItem Value="PM" Text="Median" />
                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                            <telerik:DropDownListItem Value="WK" Text="Weekend" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Année Académique"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Office2007" runat="server" Width="100%">
                    </telerik:RadDropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Nom et prénom"></asp:Label>
                    <telerik:RadTextBox ID="txtFullName" runat="server" Width="100%" Skin="Office2007"></telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Control"></asp:Label>
                    <telerik:RadDropDownList ID="ddlControl" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="1" Text="1er" />
                            <telerik:DropDownListItem Value="2" Text="2ieme" />
                            <telerik:DropDownListItem Value="3" Text="3ieme" />
                            <telerik:DropDownListItem Value="4" Text="4ieme" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center" style="padding-top: 12px;">
                    <button type="button" class="btn btn-sm btn-primary" id="Button1" runat="server"
                        onserverclick="btnSearch_ServerClick" width="120px">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    <%--  &nbsp;
                    <button type="button" class="btn btn-sm btn-primary" id="btnValidate1" runat="server"
                        onserverclick="btnValidate_Click" width="120px">
                        <span class="fa fa-save"></span>
                        <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>--%>
                </div>
            </div>
            <br />


            <div class="row">
                <div class="col-md-12" style="width: 100%">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridBulletin" Width="100%"
                        OnItemCommand="gridBulletin_ItemCommand"
                        OnItemDataBound="gridBulletin_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenClassId" Value='<%# Eval("class_id").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenVacationCode" Value='<%# Eval("vacation_code").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenStudentId" Value='<%# Eval("id").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenControl" Value='<%# Eval("control").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenAcademicYearId" Value='<%# Eval("academic_year_id").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenYears" Value='<%# Eval("years").ToString() %>' />

                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-primary btn-sm" title="Imprimer bulletin"
                                            id="btnPrintPdf" onserverclick="btnPrintPdf_ServerClick">
                                            <span class="fa fa-print"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code" DataField="id">
                                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom et prénom" DataField="fullName">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Année Académique" DataField="years">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Control">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
