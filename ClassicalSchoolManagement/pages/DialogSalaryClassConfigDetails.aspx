<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogSalaryClassConfigDetails.aspx.cs" Inherits="DialogSalaryClassConfigDetails" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration Paiement/Classe</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <link rel="icon" href="../images/graduation_cap_icon.png">
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

    <style>
        .mainDiv {
            margin: auto;
            width: 100%;
            border: 0px solid red;
        }

        .alignRight {
            text-align: right;
        }

        .divPrimaryDesign {
            width: 250px;
            margin: auto;
        }

        .divSecondaryDesign {
            width: 700px;
            margin: auto;
        }

        .divGridHeader {
            background-color: navy;
            color: white;
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            font-size: small;
            font-weight: bold;
        }

        .panellDesign {
            border: 0px solid silver;
        }

        .amountDesign {
            text-align: right;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">

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



        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cboVacation">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tr_error_msg"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="tr_salary_actual" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="divSecondary" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlVacation">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gridListSecondary"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gridListSecondary"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>


        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                    Skin="Office2007" DestroyOnClose="false" Behaviors="Close,Minimize">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>


        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="mainDiv">
            <asp:HiddenField runat="server" ID="hiddenClassId" />

            <%--*********** PRIMARY CLASS *************--%>

            <div runat="server" id="divPrimary">
                <div class="container">
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label runat="server" Text="Classe"></asp:Label>
                            <telerik:RadTextBox runat="server" ID="txtClassNamePrimary" Font-Bold="true"
                                Skin="Bootstrap" ReadOnly="true" Width="100%">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label runat="server" Text="Montant Salaire (HTG)"></asp:Label>
                            <telerik:RadNumericTextBox runat="server" ID="txtAmount" Skin="Bootstrap"
                                Width="100%" CssClass="amountDesign">
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <button runat="server" class="btn btn-sm btn-primary" title="Definir Montant"
                                id="btnSavePrimaryClassInfo" onserverclick="btnSavePrimaryClassInfo_ServerClick">
                                <span class="fa fa-save"></span>&nbsp;Sauvegarder
                            </button>
                        </div>
                    </div>
                </div>
            </div>


            <%--*********** SECONDARY CLASS *************--%>

            <div runat="server" id="divSecondary">
                <div class="container">
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Text="Classe"></asp:Label>
                            <telerik:RadTextBox runat="server" ID="txtClassNameSecondary" Font-Bold="true"
                                Skin="Bootstrap" ReadOnly="true" Width="100%">
                            </telerik:RadTextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                ID="radGridCourseSecondary" ClientSettings-DataBinding-ShowEmptyRowsOnLoad="true"
                                OnNeedDataSource="radGridCourseSecondary_NeedDataSource"
                                OnItemCommand="radGridCourseSecondary_ItemCommand"
                                OnItemDataBound="radGridCourseSecondary_ItemDataBound">
                                <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="No">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hiddenCourseId" Value='<%# Eval("cours_id").ToString() %>' />
                                                <asp:HiddenField runat="server" ID="hiddenAmount" Value='<%# Eval("amount").ToString() %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Cours" DataField="cours_name">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Prix cours / heure">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <telerik:RadComboBox runat="server" ID="cboPrices" Skin="Office2007" MaxHeight="200"></telerik:RadComboBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <button runat="server" class="btn btn-sm btn-primary" title="Definir Montant" style="width: 120px"
                                id="btnSaveSecondaryClassInfo" onserverclick="btnSaveSecondaryClassInfo_ServerClick">
                                <span class="fa fa-save"></span>&nbsp;Sauvegarder
                            </button>
                            &nbsp;
                               <button runat="server" class="btn btn-sm btn-danger" title="Fermer"
                                   id="btnClose" onclick="CloseDialog()" style="width: 120px">
                                   <span class="fa fa-remove"></span>&nbsp;Fermer
                               </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
