<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SudentContributionPayments.aspx.cs"
    Inherits="SudentContributionPayments" MasterPageFile="~/master/Master5.Master" %>

<%--<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Paiement de contribution
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

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlContributionType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtContributionAmount"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>





    <!-- Breadcubs Area Start Here -->
    <div class="breadcrumbs-area">
        <%-- <h3>Eleve</h3>--%>
        <ul>
            <li>
                <a href="#">Élèves</a>
            </li>
            <li>Paiement de contribution</li>
        </ul>
    </div>
    <!-- Breadcubs Area End Here -->




    <div class="row">

        <%--------------------- STUDENT INFORMATION ------------------------%>

        <div class="col-4-xxxl col-12">
            <div class="card height-auto">
                <div class="card-body">
                    <div class="heading-layout1">
                        <div class="item-title">
                            <h3><span class="fa fa-info-circle"></span>&nbsp;Information de l'élève</h3>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-10">
                            <telerik:RadTextBox ID="txtStudentId" runat="server" EmptyMessage="Tapez code élève ici..."
                                Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-2">
                            <button type="button" class="btn btn-warning btn-lg" id="btnSearch" runat="server"
                                causesvalidation="true" onserverclick="btnSearch_ServerClick" title="Rechercher">
                                <span class="fa fa-search"></span>
                            </button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" Text="Nom et Prénom" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtFullname" runat="server" Font-Bold="true"
                                Width="100%" Skin="Bootstrap" ReadOnly="true">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtClassroomName" runat="server" Font-Bold="true"
                                Width="100%" Skin="Bootstrap" ReadOnly="true">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" Text="Vacation" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtVacation" runat="server" Font-Bold="true"
                                Width="100%" Skin="Bootstrap" ReadOnly="true">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" Text="Année Académique" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtAcademicYear" runat="server" Font-Bold="true"
                                Width="100%" Skin="Bootstrap" ReadOnly="true">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12 form-group mg-t-8">
                            <button type="button" class="btn-fill-lg bg-blue-dark btn-hover-yellow" id="btnCancel" runat="server"
                                onserverclick="btnCancel_ServerClick">
                                Annuler</button>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hiddenStudentId" />
                    <asp:HiddenField runat="server" ID="hiddenClassroomId" />
                    <asp:HiddenField runat="server" ID="hiddentAccYearId" />
                </div>
            </div>
        </div>


        <%--------------------- CONTRIBUTION LIST TO BE PAID ------------------------%>

        <div class="col-8-xxxl col-12">
            <div class="card height-auto">
                <div class="card-body">
                    <div class="heading-layout1">
                        <div class="item-title">
                            <h3><span class="fa fa-list"></span>&nbsp;Liste des contributions a payer</h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label runat="server" Text="Type de contritubion" CssClass="app-label-design"></asp:Label>
                            <telerik:RadDropDownList ID="ddlContributionType" runat="server" Skin="Bootstrap" Width="100%"
                                CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ddlContributionType_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" Text="Montant" CssClass="app-label-design"></asp:Label>
                            <telerik:RadNumericTextBox runat="server" ID="txtContributionAmount" EmptyMessage="0.00" ReadOnly="true"
                                CssClass="currencyDesign" Width="100%" Skin="Bootstrap" ForeColor="Red" Font-Bold="true">
                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button runat="server" class="btn btn-success btn-lg" disabled
                                id="btnPay" onserverclick="btnPay_ServerClick" style="width: 120px">
                                <span class="fa fa-money-bill"></span>&nbsp; Payer
                            </button>
                        </div>
                    </div>


                    <br />
                    <div class="row">


                        <div class="col-12" style="overflow-y: scroll; min-height: 600px;">
                            <telerik:RadGrid runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                                ID="gridContributionPayment"
                                OnNeedDataSource="gridContributionPayment_NeedDataSource"
                                OnItemCommand="gridContributionPayment_ItemCommand"
                                OnItemDataBound="gridContributionPayment_ItemDataBound"
                                AllowPaging="true" PageSize="20">
                                <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                                    DataKeyNames="id">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="No">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="labelNo"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Description" DataField="description">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Montant" DataField="paid_amount" DataFormatString="{0:N2}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Date" DataField="date_register" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Actions">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <button runat="server" class="btn btn-danger btn-lg" title="Supprimer"
                                                    id="btnDelete" onserverclick="removePayment" visible='<%# Eval("contribution_type_id").ToString() == "-2" ? false : true %>'>
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

        </div>

    </div>
</asp:Content>

