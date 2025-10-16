<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentsManagement.aspx.cs"
    Inherits="DocumentsManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Documents Management
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

        .col {
            word-wrap: break-word;
        }

        .upper {
            text-transform: uppercase;
        }

        .wrapWordDesign{
            overflow-wrap: break-word;
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
            var gv = document.getElementById("gridListDocuments");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListDocuments");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>
</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUploadDocuments" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlCategory" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUploadDocuments">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUploadDocuments" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Bootstrap" NavigateUrl="StudentDetailsInformation.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <div class="panel panel-info" runat="server" id="pnlCreateGroup">
        <div class="panel-heading">
            <h4><span class="fa fa-flag"></span>&nbsp;Gestion des Documents</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-9">
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Label ID="Label1" runat="server" Text="Code" CssClass="app-label-design"></asp:Label>
                            <div class="input-group">
                                <telerik:RadTextBox ID="txtCode" runat="server" CssClass="upper"
                                    Width="100%" Skin="Web20">
                                </telerik:RadTextBox>
                                <div class="input-group-btn">
                                    <button class="btn btn-primary btn-sm" runat="server" id="btnSearchByCode"
                                        onserverclick="btnSearchByCode_ServerClick">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="Label2" runat="server" Text="Nom & Prénom" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtFullname" ReadOnly="true" runat="server" CssClass="upper"
                                Width="100%" Skin="Web20">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Label ID="RadLabel1" runat="server" Text="Description" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="100%" Skin="Web20">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="Label3" runat="server" Text="Attacher documents" CssClass="app-label-design"></asp:Label>
                            <asp:FileUpload runat="server" ID="documentUploader"
                                EnableViewState="true" Width="100%" AllowMultiple="true" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <img runat="server" id="imgStaffs" src="../images/image_data/Default.png"
                        class="img-bordered-sm" style="width: 120px; height: 120px" />
                </div>
            </div>


            <div class="row">
                <div class="col-sm-12 text-center">
                    <button type="button" class="btn btn-primary" id="btnUploadDocument" runat="server"
                        onserverclick="btnUploadDocuments_Click" style="width: 120px">
                        <span class="fa fa-upload"></span>
                        <asp:Literal runat="server" Text="Upload"></asp:Literal></button>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridDocuments"
                        OnNeedDataSource="gridDocuments_NeedDataSource"
                        OnItemCommand="gridDocuments_ItemCommand"
                        OnItemDataBound="gridDocuments_ItemDataBound">
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
                                        <button runat="server" class="btn btn-default btn-sm" title="Télécharger documents"
                                             onserverclick="downloadDocuments" disabled="disabled">
                                            <span class="fa fa-download"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code " DataField="staff_code">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Description" DataField="document_name">
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom du fichier" DataField="document_path">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle CssClass="wrapWordDesign" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Date d'ajout" DataField="upload_time">
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </div>
        </div>
    </div>




</asp:Content>
