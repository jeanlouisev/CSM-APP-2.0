<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentContributionType.aspx.cs"
    Inherits="StudentContributionType" MasterPageFile="~/master/Master5.Master" %>

<%--<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Types de contribution
</asp:Content>

<%--<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <%-- <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
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
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">--%>


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


        .currencyDesign {
            text-align: right;
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
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Bootstrap">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

 <%--   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridContributionType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDescription"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>





    <!-- Breadcubs Area Start Here -->
    <div class="breadcrumbs-area">
        <%-- <h3>Eleve</h3>--%>
        <ul>
            <li>
                <a href="#">Élèves</a>
            </li>
            <li>Types de contribution</li>
        </ul>
    </div>
    <!-- Breadcubs Area End Here -->


    <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3><span class="fa fa-money-bill-wave"></span>&nbsp;Types de contributions</h3>
                </div>
            </div>
            <div class="row">
                <asp:HiddenField runat="server" ID="hiddenId" />
                <div class="col-sm-4">
                    <asp:Label runat="server" Text="Description" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtDescription" runat="server"
                        Width="100%" CssClass="upperCaseOnly" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-sm-4">
                    <asp:Label runat="server" Text="Prix" CssClass="app-label-design"></asp:Label>
                    <telerik:RadNumericTextBox runat="server" ID="txtPrice" EmptyMessage="0"
                        CssClass="currencyDesign" Width="100%" Skin="Bootstrap" ForeColor="Red" Font-Bold="true">
                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-sm-3">
                    <br />
                    <button type="button" class="btn btn-primary btn-lg" id="btnAdd" runat="server"
                        onserverclick="btnSave_Click" causesvalidation="true">
                        <span class="fa fa-save"></span>
                        <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12" style="width: 100%; overflow-x: scroll; min-height: 400px;">
                    <telerik:RadGrid runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                        ID="gridContributionType"
                        OnNeedDataSource="gridContributionType_NeedDataSource"
                        OnItemCommand="gridContributionType_ItemCommand"
                        OnItemDataBound="gridContributionType_ItemDataBound"
                        AllowPaging="true" PageSize="20">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenContributionTypeId" Value='<%# Eval("id").ToString() %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Description" DataField="description">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Prix" DataField="price" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-lg" title="Modifier"
                                            id="btnEdit" onserverclick="editContributionType">
                                            <span class="fa fa-edit"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-lg" title="Supprimer"
                                            id="btnDelete" onserverclick="removeContributionType" visible='<%# Eval("id").ToString() == "-2" ? false : true %>'>
                                            <span class="fa fa-times"></span>
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

</asp:Content>
