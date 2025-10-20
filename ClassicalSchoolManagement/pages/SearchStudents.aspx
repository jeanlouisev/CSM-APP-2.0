<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchStudents.aspx.cs" EnableEventValidation="false"
    Inherits="SearchStudents" MasterPageFile="~/master/Master5.Master" %>

<%--<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Liste des elèves
</asp:Content>

<%--<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <%--  <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
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

        .toUpper {
            text-transform: uppercase;
        }

        .lower {
            text-transform: lowercase;
        }


        .panel {
            margin: 0px;
        }
    </style>

    <script type="text/javascript">
        function ConfirmDisable() {
            var confirm_value_disable = document.createElement("INPUT");
            confirm_value_disable.type = "hidden";
            confirm_value_disable.name = "confirm_value_disable";
            if (confirm("Voulez-vous vraiment desactiver cet eleve ?")) {
                confirm_value_disable.value = "Yes";
            } else {
                confirm_value_disable.value = "No";
            }
            document.forms[0].appendChild(confirm_value_disable);
        }

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Voulez-vous vraiment supprimer cet eleve ?")) {
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

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <%-- <telerik:AjaxSetting AjaxControlID="chkNif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
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
            <li>Liste des élèves</li>
        </ul>
    </div>
    <!-- Breadcubs Area End Here -->


    <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3><span class="fa fa-list-ol"></span>&nbsp;Liste des élèves</h3>
                </div>
            </div>


            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblFirstName" runat="server" Text="Code" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" CssClass="toUpper" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label5" runat="server" Text="Nom & Prénom	" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtFullName" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label6" runat="server" Text="Sexe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="M" Text="Masculin" />
                            <telerik:DropDownListItem Value="F" Text="Feminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label8" runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                        Width="100%" Skin="Bootstrap" ExpandDirection="Down">
                    </telerik:RadDropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label9" runat="server" Text="Vacation" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" />
                            <telerik:DropDownListItem Value="Matin" Text="Matin" Selected="true" />
                            <telerik:DropDownListItem Value="Médian" Text="Médian" />
                            <telerik:DropDownListItem Value="Soir" Text="Soir" />
                            <telerik:DropDownListItem Value="Weekend" Text="Weekend" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label11" runat="server" Text="Année Académique" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <br />
                    <button type="button" class="btn btn-lg btn-primary" id="btnSearch" runat="server"
                        onserverclick="btnSearch_Click">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    &nbsp;

                    <button type="button" class="btn btn-lg btn-secondary" id="btnExportExcel" runat="server"
                        onserverclick="btnExportExcel_Click">
                        <span class="fa fa-file-excel"></span>
                        <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>
                </div>
            </div>
        </div>


        <div class="card-body">

            <div class="row" runat="server" id="divGridStudent">
                <div class="col-md-12" style="width: 100%;">
                    <telerik:RadGrid runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                        ID="gridStudent"
                        OnItemCommand="gridStudent_ItemCommand"
                        OnItemDataBound="gridStudent_ItemDataBound">
                        <ClientSettings Scrolling-AllowScroll="true" Scrolling-ScrollHeight="500"></ClientSettings>
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id" AllowPaging="true" ShowFooter="true" PageSize="10">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("classroom_id") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-sm" title="Désactiver"
                                            id="btnDelete" onserverclick="removeStudent">
                                            <span class="fa fa-power-off"></span>
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
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code" DataField="id">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom & Prénom" DataField="fullName">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Sexe" DataField="sex">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Classe" DataField="classroom_name">
                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Année Académique" DataField="years">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
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
