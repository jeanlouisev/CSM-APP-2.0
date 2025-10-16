<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogsManagement.aspx.cs" EnableEventValidation="false"
    Inherits="LogsManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
   Gestion Logs
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
        .col {
            word-wrap: normal;
            //text-transform:lowercase;
        }

        .tdTitle {
            width: 10%;
        }

        .tdContent {
            width: 20%;
        }
    </style>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-files-o"></span>&nbsp;Gestion Logs</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblFirstName" runat="server" Text="Code employé" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox runat="server" Skin="Web20" ID="txtStaffCode" Width="100%"></telerik:RadTextBox>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label1" runat="server" Text="Date Heure Début" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDatePicker runat="server" Skin="Web20" Width="100%" ID="radFromDate"></telerik:RadDatePicker>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label2" runat="server" Text="Date Heure Fin" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDatePicker runat="server" Skin="Web20" Width="100%" ID="radToDate"></telerik:RadDatePicker>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-primary" id="btnSearch" runat="server"
                        onserverclick="btnSearch_Click" style="width: 120px;">
                        <span class="fa fa-search"></span>&nbsp;
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    &nbsp;
                    <button type="button" class="btn btn-default" id="btnExportExcel" runat="server"
                        onserverclick="btnExportExcel_Click">
                        <span class="fa fa-file-excel-o"></span>&nbsp;
                        <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridLogs"
                        OnNeedDataSource="gridLogs_NeedDataSource"
                        OnItemCommand="gridLogs_ItemCommand"
                        OnItemDataBound="gridLogs_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code employé" DataField="staff_code">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom et prénom" DataField="fullName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Date et heure de connexion" DataField="log_time">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Actions" DataField="log_status">
                                    <HeaderStyle HorizontalAlign="Center" />
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
