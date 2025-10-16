<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogAffectCourseToTeacher.aspx.cs" Inherits="DialogAffectCourseToTeacher" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<head runat="server">
    <title>Affecter cours au professeur</title>
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
                            <asp:Label runat="server" ID="Label2" Text="Code" Font-Size="Medium" ForeColor="Navy"></asp:Label>
                            : 
                              <asp:Label runat="server" ID="lblTeacherCode" Font-Bold="true" ForeColor="Purple" Font-Size="Medium"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label runat="server" ID="Label1" Text="Nom" Font-Size="Medium" ForeColor="Navy"></asp:Label>
                            :                             
                        <asp:Label runat="server" ID="lblTeacherFullName" Font-Bold="true" ForeColor="Purple" Font-Size="Medium"></asp:Label>
                        </div>
                        <div class="col-sm-4 text-right">
                            <button type="button" class="btn btn-sm btn-primary" id="btnAffect" runat="server"
                                onserverclick="btnAffect_ServerClick" causesvalidation="true" style="width: 120px; margin-top: 10px;">
                                <span class="fa fa-plus"></span>
                                <asp:Literal runat="server" Text="Affecter"></asp:Literal></button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-8" style="width: 100%;">
                            <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                ID="radGridCours"
                                OnItemCommand="radGridCours_ItemCommand"
                                OnItemDataBound="radGridCours_ItemDataBound">
                                <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="No">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hiddenCourseId" Value='<%# Eval("id").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" ID="btnCheckAll"
                                                    OnCheckedChanged="btnCheckAll_CheckedChanged" AutoPostBack="true"
                                                    CausesValidation="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Cours" DataField="name">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>

                </div>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-primary" id="Button1" runat="server"
                    onserverclick="btnAffect_ServerClick" causesvalidation="true" style="width: 120px; margin-top: 10px;">
                    <span class="fa fa-plus"></span>
                    <asp:Literal runat="server" Text="Affecter"></asp:Literal></button>
            </div>
        </div>
    </form>
</body>
