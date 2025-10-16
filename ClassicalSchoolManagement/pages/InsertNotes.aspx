<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertNotes.aspx.cs"
    Inherits="InsertNotes" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Insérer Notes
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
        .textBoxDesign {
            border-top: 0px;
            border-left: 0px;
            border-right: 0px;
            border-bottom: 1px solid black;
        }

        .divResult {
            overflow: scroll;
            border: 0px solid red;
            max-height: 250px;
            overflow-x: hidden;
            height: auto;
            max-width: 960px;
        }

        .txtStPointsDesign {
            text-align: right;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            -moz-appearance: none;
            margin: 0;
            border: 0px solid blue;
        }

        #tblSearch > tr > td {
            padding-bottom: 7px;
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


        .currencyDesign {
            text-align: right;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        // toggle button click maximize page
        //$(document).ready(function () {
        //    $('#toggleLink').click();
        //});


        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
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
            <telerik:AjaxSetting AjaxControlID="txtStudentCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtStudentFullname"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>



    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-book"></span>&nbsp;Inserer Notes</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Code"></asp:Label>
                    <telerik:RadTextBox ID="txtStudentCode" runat="server" Width="100%" Skin="Web20"
                        Font-Bold="true" ForeColor="Maroon" OnTextChanged="txtStudentCode_TextChanged"
                        CausesValidation="false" AutoPostBack="true">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Classe"></asp:Label>
                    <telerik:RadDropDownList ID="ddlClassroom" Skin="Office2007" runat="server"
                        Width="100%" DropDownHeight="200px" CausesValidation="false"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblFirstName" runat="server" Text="Vacation : "></asp:Label>
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="AM" Text="Matin" />
                            <telerik:DropDownListItem Value="PM" Text="Median" />
                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                            <telerik:DropDownListItem Value="WK" Text="Weekend" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblLastName" runat="server" Text="Control : "></asp:Label>
                    <telerik:RadDropDownList ID="ddlControl" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="1" Text="1er" />
                            <telerik:DropDownListItem Value="2" Text="2ieme" />
                            <telerik:DropDownListItem Value="3" Text="3ieme" />
                            <telerik:DropDownListItem Value="4" Text="4ieme" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Année Académique"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Office2007"
                        runat="server" Width="100%">
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Nom complet"></asp:Label>
                    <telerik:RadTextBox ID="txtStudentFullname" runat="server" Width="100%" Skin="Web20"
                        Font-Bold="true" ForeColor="Maroon" Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" Text="Cours"></asp:Label>
                    <telerik:RadDropDownList ID="ddlCourse" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Sélectionner--" Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <button type="button" class="btn btn-primary" id="btnSearch1" runat="server"
                    onserverclick="btnSearch_Click" causesvalidation="true" style="width: 120px">
                    <span class="fa fa-search"></span>
                    <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                &nbsp;
                <button type="button" class="btn btn-success" id="btnAddNotes" runat="server"
                    onserverclick="btnAddNotes_Click" causesvalidation="true" style="width: 120px">
                    <span class="fa fa-save"></span>
                    <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                &nbsp;
                <button type="button" class="btn btn-default" id="btnExportPdf" runat="server"
                    onserverclick="btnExportPdf_ServerClick" causesvalidation="true">
                    <span class="fa fa-file-pdf-o"></span>
                    <asp:Literal runat="server" Text="Exporter en pdf"></asp:Literal></button>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                    ID="gridNotes"
                    OnItemCommand="gridNotes_ItemCommand"
                    OnItemDataBound="gridNotes_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="No">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hiddenVacation" Value='<%# Eval("vacation_code") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenClassId" Value='<%# Eval("class_id") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenStudentId" Value='<%# Eval("student_id") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenAcademicYearId" Value='<%# Eval("academic_year_id") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenCoursId" Value='<%# Eval("cours_id") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenCoefficient" Value='<%# Eval("coefficient") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenControl" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                        id="btnDelete" onserverclick="removeTeacher">
                                        <span class="fa fa-remove"></span>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-primary btn-sm" title="Modifier"
                                        id="btnEdit" onserverclick="btnEdit_ServerClick">
                                        <span class="fa fa-edit"></span>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-dark btn-sm" title="Visualiser les details"
                                        id="btnViewDetails" onserverclick="btnViewDetails_ServerClick">
                                        <span class="fa fa-eye"></span>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn HeaderText="Code" DataField="student_id">
                                <HeaderStyle Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Nom complet" DataField="student_fullName">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Control">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Cours" DataField="cours_name">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Coefficient" DataField="coefficient">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Note">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtNoteObtained" EmptyMessage="0"
                                        CssClass="currencyDesign" Width="100%" Skin="Web20"
                                        ForeColor="Red" Font-Bold="true"
                                        Value='<%# Eval("note_obtained") %>'>
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>

    </div>
</asp:Content>
