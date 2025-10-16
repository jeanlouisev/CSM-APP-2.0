<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogEditUser.aspx.cs" Inherits="DialogEditUser" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<head runat="server">
    <title>Modifier les utilisateurs
    </title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!--[if IE]>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
        <![endif]-->
    <!-- BOOTSTRAP STYLE SHEET -->
    <%-- <link href="../css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../css/bootstrap-min.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />--%>
    <!-- FONT AWESOME ICONS STYLE SHEET -->
    <%--<link href="../css/font-awesome.css" rel="stylesheet" />--%>
    <!-- CUSTOM STYLES -->
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
    <link rel="stylesheet" href="../plugins/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">
    <!-- Select -->
    <link rel="stylesheet" href="../plugins/select2/select2.css">

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

    <script src="../plugins/input-mask/jquery.inputmask.js"></script>
    <script src="../plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="../plugins/input-mask/jquery.inputmask.extensions.js"></script>

    <script type="text/javascript" src="../plugins/daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="../plugins/daterangepicker/daterangepicker.js"></script>

    <script src="../plugins/datepicker/js/bootstrap-datepicker.js"></script>

    <!-- Select2 -->
    <script src="../plugins/select2/select2.full.min.js"></script>
    <!-- InputMask -->
    <!-- bootstrap color picker -->
    <script src="../plugins/colorpicker/bootstrap-colorpicker.min.js"></script>
    <!-- bootstrap time picker -->
    <script src="../plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <!-- SlimScroll 1.3.0 -->
    <script src="../plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- iCheck 1.0.1 -->
    <script src="../plugins/iCheck/icheck.min.js"></script>
    <!-- FastClick -->
    <script src="../plugins/fastclick/fastclick.min.js"></script>
    <!-- Data table + Export -->
    <script src="../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../plugins/datatables/dataTables.bootstrap.min.js"></script>
    <script src="../plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="../plugins/datatables/buttons.bootstrap.min.js"></script>
    <script src="../plugins/datatables/jszip.min.js"></script>
    <script src="../plugins/datatables/pdfmake.min.js"></script>
    <script src="../plugins/datatables/vfs_fonts.js"></script>
    <script src="../plugins/datatables/buttons.html5.min.js"></script>
    <script src="../plugins/datatables/buttons.print.min.js"></script>
    <script src="../plugins/datatables/buttons.colVis.min.js"></script>

    <style type="text/css">
        .wrapper1 {
            overflow-x: hidden;
            border: 0px solid red;
        }

        .wrapper2 {
            overflow-x: hidden;
            border: 1px solid purple;
        }

        .upperCaseOnly {
            text-transform: uppercase;
        }
    </style>
</head>
<body onunload="window.opener.reload();">
    <form id="form1" runat="server">
        <script type="text/javascript">
            function closeAndPassData() {
                var data = "Test"; //this can be a complex object obtained according to the project logic
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(data);
                    top.location.href = top.location.href;
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function CloseDialog() {
                var oWindow = GetRadWindow();
                if (oWindow != null) {
                    oWindow.close();
                }
            }

        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <%--        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>

        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="false" runat="server" EnableShadow="true" Skin="Telerik" DestroyOnClose="false">
        </telerik:RadWindowManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <%--      <AjaxSettings>
                 <telerik:AjaxSetting AjaxControlID="chkContactPerson">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFirstNameParent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>--%>
        </telerik:RadAjaxManager>
        <div class="modal-content">
            <%--   <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="CloseDialog();">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title" id="myModalLabel">
                    <asp:Literal runat="server" Text="<%$Resources:Resource, user_management %>"></asp:Literal></h4>
            </div>--%>
            <div class="modal-body">
                <div class="containter">
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label runat="server" Text="Code"></asp:Label>
                            <telerik:RadTextBox runat="server" ID="txtCode" Skin="Office2007"
                                Font-Bold="true" ForeColor="Maroon" Width="100%" Enabled="false">
                            </telerik:RadTextBox>
                        </div>

                        <div class="col-sm-4">
                            <asp:Label runat="server" Text="Nom et prénom"></asp:Label>
                            <telerik:RadTextBox runat="server" ID="txtFullname" Skin="Office2007"
                                Font-Bold="true" ForeColor="Maroon" Width="100%" Enabled="false">
                            </telerik:RadTextBox>
                        </div>

                        <div class="col-sm-4">
                            <asp:Label runat="server" Text="Date d’éxpiration"></asp:Label>
                            <telerik:RadDatePicker runat="server" ID="radExpiryDate" Width="100%"
                                Skin="Office2007" Font-Bold="true">
                            </telerik:RadDatePicker>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label runat="server" Text="Statut"></asp:Label>
                            <telerik:RadComboBox runat="server" ID="ddlLockedStatus" Skin="Office2007" Width="100%" Font-Bold="true">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Dé-Vérouillé" />
                                    <telerik:RadComboBoxItem Value="1" Text="Vérouillé" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label runat="server" Text="Roles"></asp:Label>
                            <telerik:RadComboBox runat="server" ID="ddlRoles" Skin="Office2007" Width="100%" Font-Bold="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" CausesValidation="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Vérouillé" />
                                    <telerik:RadComboBoxItem Value="1" Text="Dé-Vérouillé" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>

                    <br />
                 <%--   <div class="well well-sm" style="background-color: #767676; color: whitesmoke;">
                        <i class="fa fa-shield"></i>&nbsp&nbsp
                        <asp:Label runat="server" Text="Droits d’accès"></asp:Label>
                    </div>--%>
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h4><span class="fa fa-shield"></span>&nbsp;Droits d’accès</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12">
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
                                            <asp:CheckBox Enabled="false" ID="ChkStudentView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkStudentEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkStudentDelete" runat="server" /></td>
                                    </tr>

                                    <%-- **************** CLASSE  menu-2 **************** --%>
                                    <tr>
                                        <td>Gestion des Classes</td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkClasseView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkClasseEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkClasseDelete" runat="server" /></td>
                                    </tr>

                                    <%-- **************** SECRETARIAT  menu-3 **************** --%>
                                    <tr>
                                        <td>Secrétariat</td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkSecretaryView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkSecretaryEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkSecretaryDelete" runat="server" /></td>
                                    </tr>

                                    <%-- **************** RESSOURCES HUMAINES  menu-4 **************** --%>
                                    <tr>
                                        <td>Ressources Humaines</td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkHrView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkHrEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkHrDelete" runat="server" /></td>
                                    </tr>

                                    <%-- **************** ECONOMAT  menu-5 **************** --%>
                                    <tr>
                                        <td>Economat</td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkEconomatView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkEconomatEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkEconomatDelete" runat="server" /></td>
                                    </tr>


                                    <%-- **************** PARAMETRES  menu-6 **************** --%>
                                    <tr>
                                        <td>Paramètres</td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkParameterView" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkParameterEdit" runat="server" /></td>
                                        <td class="text-center">
                                            <asp:CheckBox Enabled="false" ID="ChkParameterDelete" runat="server" /></td>
                                    </tr>

                                    <%--   <tr class="bg-primary">
                                    <td colspan="3" style="text-align: right; padding-right: 10px;">
                                        <asp:Label runat="server" Text="Full Droit" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td colspan="1" class="text-center">
                                        <asp:CheckBox Enabled="false" ID="chkFullAccessGrant" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkFullAccessGrant_CheckedChanged" />
                                    </td>
                                </tr>--%>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div>
                            <button class="btn btn-primary" id="btnAdd" runat="server"
                                onserverclick="btnAdd_Click">
                                <i class="fa fa-save"></i>&nbsp;
                            <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="CloseDialog();" id="btnCloseModal">
                                <i class="fa fa-remove"></i>&nbsp;
                            <asp:Literal runat="server" Text="Fermer"></asp:Literal></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>

