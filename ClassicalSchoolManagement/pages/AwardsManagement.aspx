<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwardsManagement.aspx.cs"
    Inherits="AwardsManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Add Course
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
        .upperCaseOnly {
            text-transform: uppercase;
        }

        .mainDivLeft {
            width: 48%;
            float: left;
        }

        .mainDivRight {
            width: 48%;
            float: right;
        }

        .amountRight {
            text-align: right;
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

        function allowOnlyNumber(sender, args) {
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

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <%--    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtCourseName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblErrorcoursEmpty"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-flag"></span>&nbsp;Palmarès</h4>
        </div>
        <div class="panel-body">
            <div class="row">
               <%-- <div class="col-sm-2">
                    <asp:Label runat="server" Text="Code" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtCode" runat="server"
                        Width="100%" CssClass="upperCaseOnly" Skin="Office2007">
                    </telerik:RadTextBox>
                </div>--%>
                <div class="col-sm-3">
                    <asp:Label runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                        Width="100%" Skin="Office2007" ExpandDirection="Down">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-2">
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
                <div class="col-md-2">
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
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Année Académique"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Office2007"
                        runat="server" Width="100%">
                    </telerik:RadDropDownList>
                </div>

                <div class="row">
                    <div class="col-sm-12 text-center">
                        <br />
                        <button type="button" class="btn btn-primary" id="btnSearch" runat="server"
                            onserverclick="btnSearch_ServerClick" causesvalidation="true" style="width: 120px">
                            <span class="fa fa-search"></span>
                            <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridAwards"
                        OnItemCommand="gridAwards_ItemCommand"
                        OnItemDataBound="gridAwards_ItemDataBound"
                        AllowPaging="true" PageSize="100">
                        <ClientSettings Scrolling-AllowScroll="true" Scrolling-ScrollHeight="400"></ClientSettings>
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed">
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
