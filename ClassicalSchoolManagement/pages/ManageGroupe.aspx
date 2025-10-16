<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageGroupe.aspx.cs"
    Inherits="ManageGroupe" MasterPageFile="~/master/Master3.Master" %>

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
            <h4><span class="fa fa-flag"></span>&nbsp;Gestion des roles</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblRoleName" runat="server" Text="Nom du role"></asp:Label>
                    <telerik:RadTextBox ID="txtRoleName" runat="server" Width="100%" MaxLength="50"
                        CssClass="upperCaseOnly" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-2">
                    <br />
                    <button type="button" class="btn btn-primary" id="btnAddRole" runat="server"
                        onserverclick="btnAddRole_Click" style="width: 120px">
                        <span class="fa fa-plus"></span>
                        <asp:Literal runat="server" Text="Ajouter"></asp:Literal></button>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridRoles"
                        OnNeedDataSource="gridRoles_NeedDataSource"
                        OnItemCommand="gridRoles_ItemCommand"
                        OnItemDataBound="gridRoles_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                            id="btnDelete" onserverclick="btnDelete_ServerClick">
                                            <span class="fa fa-remove"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-primary btn-sm" title="Droits d'acces"
                                            id="btnAffectRightsToRole" onserverclick="btnAffectRightsToRole_ServerClick">
                                            <span class="fa fa-shield"></span>&nbsp;Droits d'accès
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Nom du role" DataField="name">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>


            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>


    <div class="panel panel-info" runat="server" id="pnlRoleAccess" visible="false">
        <div class="panel-heading">
            <h4><span class="fa fa-flag"></span>&nbsp;
                <asp:Label runat="server" ID="lblAccessRightHeading"></asp:Label>
                </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3">
                    <table class="table table-bordered table-responsive">
                        <tr class="bg-primary">
                            <th>Menu</th>
                            <th class="text-center">
                                <i class="glyphicon glyphicon-eye-open"></i>
                            </th>
                            <th class="text-center">
                                <i class="glyphicon glyphicon-edit"></i>
                            </th>
                            <th class="text-center">
                                <i class="glyphicon glyphicon-remove-sign"></i>
                            </th>
                        </tr>

                        <%-- **************** ELEVE  menu-1 **************** --%>
                        <tr>
                            <td>Gestion des elèves</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkStudentView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkStudentEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkStudentDelete" runat="server" /></td>
                        </tr>

                        <%-- **************** CLASSE  menu-2 **************** --%>
                        <tr>
                            <td>Gestion des Classes</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkClasseView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkClasseEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkClasseDelete" runat="server" /></td>
                        </tr>

                        <%-- **************** SECRETARIAT  menu-3 **************** --%>
                        <tr>
                            <td>Secrétariat</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkSecretaryView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkSecretaryEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkSecretaryDelete" runat="server" /></td>
                        </tr>

                        <%-- **************** RESSOURCES HUMAINES  menu-4 **************** --%>
                        <tr>
                            <td>Ressources Humaines</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkHrView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkHrEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkHrDelete" runat="server" /></td>
                        </tr>

                        <%-- **************** ECONOMAT  menu-5 **************** --%>
                        <tr>
                            <td>Economat</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkEconomatView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkEconomatEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkEconomatDelete" runat="server" /></td>
                        </tr>


                        <%-- **************** PARAMETRES  menu-6 **************** --%>
                        <tr>
                            <td>Paramètres</td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkParameterView" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkParameterEdit" runat="server" /></td>
                            <td class="text-center">
                                <asp:CheckBox ID="ChkParameterDelete" runat="server" /></td>
                        </tr>

                        <tr class="bg-primary">
                            <td colspan="3" style="text-align: right; padding-right: 10px;">
                                <asp:Label runat="server" Text="Full Droit" Font-Size="Medium"></asp:Label>
                            </td>
                            <td colspan="1" class="text-center">
                                <asp:CheckBox ID="chkFullAccessGrant" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkFullAccessGrant_CheckedChanged" />
                            </td>
                        </tr>
                    </table>


                    <div class="row">
                        <div class="col-sm-12 text-center">
                            <button class="btn btn-primary" id="btnAddAccessRights" runat="server"
                                onserverclick="btnAddAccessRights_Click" width="120px">
                                <span class="fa fa-save"></span>
                                <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>

                            <button class="btn btn-default" id="btnViewRoleList" runat="server"
                                onserverclick="btnViewRoleList_ServerClick" width="120px">
                                <span class="fa fa-arrow-circle-o-left"></span>
                                <asp:Literal runat="server" Text="Retour à la liste des roles"></asp:Literal></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>






</asp:Content>
