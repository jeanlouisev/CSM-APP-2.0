<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs"
    Inherits="UserManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Gestion des roles
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
        .inlineBlock {
            display: inline-block;
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
            var gv = document.getElementById("gridListRole");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListRole");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtStaffCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtStaffFullname" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtStaffStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="panel panel-info" runat="server" id="pnlCreateRole">
        <div class="panel-heading">
            <h4><span class="fa fa-users"></span>&nbsp;Gestion des utilisateurs</h4>
        </div>
        <div class="panel-body">
            <span style="float: left;">
                <telerik:RadTextBox runat="server" Skin="Bootstrap" CssClass="upperCaseOnly"
                    ID="txtCodeSearch" EmptyMessage="Code" Width="200px">
                </telerik:RadTextBox>

                <button runat="server" class="btn btn-primary" id="btnSearch" onserverclick="btnSearch_Click">
                    <i class="fa fa-search"></i>&nbsp;
                                <asp:Literal runat="server" Text="Rechercher"></asp:Literal>
                </button>
            </span>
            <br />
            <br />
            <div class="form-group" id="tr_grid" runat="server">
                <div  style="width: 100%;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridUsers"
                        OnNeedDataSource="gridUsers_NeedDataSource"
                        OnItemCommand="gridUsers_ItemCommand"
                        OnItemDataBound="gridUsers_ItemDataBound"
                        Width="100%">
                        <MasterTableView DataKeyNames="username" AutoGenerateColumns="false" TableLayout="Fixed">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code" DataField="username">
                                    <HeaderStyle Width="150" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom et prenom" DataField="fullname">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Statut" UniqueName="lock_status">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Literal ID="lblLockedStatus" runat="server"
                                            Text='<%# int.Parse(Eval("locked").ToString()) == 0 ? "Dé-vérouillé" : "Vérouillé" %>'></asp:Literal>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Heure de verouillage" DataField="locked_time" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Date d'expiration"
                                    DataFormatString="{0:dd/MM/yyyy}" DataField="expiry_date">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-primary" title="Modifier"
                                            id="btnEdit" onserverclick="btnEdit_ServerClick">
                                            <span class="fa fa-pencil"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-warning" title="Reinitialiser mot de passe"
                                            id="btnResetAccount" onserverclick="btnResetAccount_ServerClick">
                                            <span class="fa fa-refresh"></span>
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
