<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccademicYear.aspx.cs"
    Inherits="AccademicYear" MasterPageFile="~/master/Master3.Master" %>


<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Année Académique
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
    </style>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Avertissement : En acceptant de supprimer vous aller perdre toutes les transactions (elpaiement, scolarité etc...)" +
                " et configurations pour l\'année académique en question.\nVoulez-vous vraiment supprimer ?")) {
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
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radFromDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radFromDate" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radTodate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radTodate" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-flag"></span>&nbsp;Configurer Année académique</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblstartDate" runat="server" Text="Début Année Académique"></asp:Label>
                    <telerik:RadDatePicker ID="radFromDate" runat="server" Width="100%" Skin="Bootstrap"
                        OnSelectedDateChanged="radFromDate_SelectedDateChanged">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy" CausesValidation="false"
                            AutoPostBack="true">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEndDate" runat="server" Text="Fin Année Académique"></asp:Label>
                    <telerik:RadDatePicker ID="radTodate" runat="server" Width="100%" Skin="Bootstrap"
                        OnSelectedDateChanged="radTodate_SelectedDateChanged">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy" CausesValidation="false" AutoPostBack="true"></DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-md-3">
                    <br />
                    <button type="button" class="btn btn-primary" id="btnAdd" runat="server"
                        onserverclick="btnAdd_Click" style="width: 120px;">
                        <span class="fa fa-plus"></span>
                        <asp:Literal runat="server" Text="Ajouter"></asp:Literal></button>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridAcademicYear"
                        OnNeedDataSource="gridAcademicYear_NeedDataSource"
                        OnItemCommand="gridAcademicYear_ItemCommand"
                        OnItemDataBound="gridAcademicYear_ItemDataBound">
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
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                            id="btnDelete" onserverclick="removeAcademicYear">
                                            <span class="fa fa-remove"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Année Académique" DataField="years">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Début Année Académique" DataField="start_date">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fin Année Académique" DataField="end_date">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
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
