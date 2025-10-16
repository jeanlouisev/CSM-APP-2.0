<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPersonal.aspx.cs"
    Inherits="SearchPersonal" MasterPageFile="~/master/Master3.Master"
    EnableEventValidation="false" %>


<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Liste des personels
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

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListStaff");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListStaff");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }


    </script>

    <script type="text/javascript">

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <%-- <telerik:AjaxSetting AjaxControlID="imageUploader">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imageKeeper" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
         --%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Office2007" NavigateUrl="StudentDetailsInformation.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>




    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-list-ol"></span>&nbsp;Liste des personnels</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblFirstName" runat="server" Text="Code" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" CssClass="toUpper">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label5" runat="server" Text="Nom & Prénom" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtFullName" runat="server" Width="100%" Skin="Web20">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label6" runat="server" Text="Sexe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="M" Text="Masculin" />
                            <telerik:DropDownListItem Value="F" Text="Feminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblMaritalStatus" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlMaritalStatus" runat="server" Skin="Office2007" Width="100%">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="C" Text="Célibataire" />
                            <telerik:DropDownListItem Value="M" Text="Marier" />
                            <telerik:DropDownListItem Value="D" Text="Divorcer" />
                            <telerik:DropDownListItem Value="UL" Text="Union Libre" />
                            <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label11" runat="server" Text="Poste Occupé" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlPosition" runat="server" Width="100%" Skin="Office2007">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlStatus" runat="server" Skin="Office2007" Width="100%">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" />
                            <telerik:DropDownListItem Value="1" Text="Activé" Selected="true" />
                            <telerik:DropDownListItem Value="0" Text="Désactivé" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="row">
            </div>


            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-sm btn-primary" id="btnSearch" runat="server"
                        onserverclick="btnSearch_Click" width="120px">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    &nbsp;
                    <button type="button" class="btn btn-sm btn-default" id="btnExportExcel" runat="server"
                        onserverclick="btnExportExcel_ServerClick" width="120px">
                        <span class="fa fa-file-excel-o"></span>
                        <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>
                </div>
            </div>
            <br />


            <div class="row" runat="server" id="divgridListStaff">
                <div class="col-md-12" style="width: 100%;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridStaff"
                        OnItemCommand="gridStaff_ItemCommand"
                        OnItemDataBound="gridStaff_ItemDataBound">
                        <ClientSettings Scrolling-AllowScroll="true" Scrolling-ScrollHeight="500"></ClientSettings>
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id" AllowPaging="true" ShowFooter="true" PageSize="10">
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
                                        <button runat="server" class="btn btn-danger btn-sm" title="Désactiver"
                                            id="btnDelete" onserverclick="removeStaff">
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
                                <telerik:GridBoundColumn HeaderText="id" DataField="id">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom & Prénom" DataField="fullName">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Sexe" DataField="sex">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="État civil" DataField="marital_status_definition">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <%--       <telerik:GridBoundColumn HeaderText="Poste Occupé" DataField="position_name">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>--%>

                                <telerik:GridTemplateColumn HeaderText="Poste Occupé">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPositionName"
                                            Text='<%# Eval("position_name").ToString().Replace(",","<br/>") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Téléphone" DataField="phone1">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status" DataField="status_definition">
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
