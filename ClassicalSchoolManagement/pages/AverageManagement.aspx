<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AverageManagement.aspx.cs"
    Inherits="AverageManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Définir la moyenne générale
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

        .alignNumberRight {
            text-align: right;
        }

        #tblEditClass tr > td {
            padding-bottom: 10px;
        }
    </style>

    <script>
        function checkAverage(event) {
            var classAverage = getElementById("txtAverage");
            if (classAverage == null) {
                alert('element is empty');
            } else {
                alert('element contains value');
            }
        }


        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListClassroom");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListClassroom");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtAverage">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListClassroom" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divLabelError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalAverage" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="gridListClassroom" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divLabelError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnValidate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListClassroom" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divLabelError"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListClassroom" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divLabelError"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTotalAverage">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListClassroom" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divLabelError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalAverage" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>


    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-file-text-o"></span>&nbsp;Définir la moyenne générale</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCode" runat="server" Text="Classe" Skin="Default" CssClass="labelDesign"></asp:Label>
                    <telerik:RadDropDownList runat="server" ID="ddlClassroom" Width="100%" Skin="Office2007">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label3" runat="server" Text="Année Académique"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Office2007" runat="server" Width="100%">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Moyenne de passage (%)	"></asp:Label>
                    <telerik:RadDropDownList ID="ddlSuccessPercent" Skin="Office2007" runat="server" Width="100%" DropDownHeight="200">
                    </telerik:RadDropDownList>
                </div>
                <%--   </div>
            <br />
            <div class="row">--%>
                <div class="col-md-3" style="padding-top: 12px;">
                    <button type="button" class="btn btn-sm btn-default" id="btnSearch" runat="server"
                        onserverclick="btnSearch_ServerClick" width="120px">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    &nbsp;
                    <button type="button" class="btn btn-sm btn-primary" id="btnValidate1" runat="server"
                        onserverclick="btnValidate_Click" width="120px">
                        <span class="fa fa-save"></span>
                        <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                </div>
            </div>
            <br />


            <div class="row">
                <div class="col-md-12" style="width: 100%;  overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridAverage" Width="100%"
                        OnNeedDataSource="gridAverage_NeedDataSource"
                        OnItemCommand="gridAverage_ItemCommand"
                        OnItemDataBound="gridAverage_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("id").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenAcademicYearId" Value='<%# Eval("academic_year_id").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenSuccessPercent" Value='<%# Eval("success_percent").ToString() %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--  <telerik:GridTemplateColumn>
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
                            </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Moyenne de passage (%)" DataField="success_percent">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Annee academique" DataField="years">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
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
