<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassroomManagement.aspx.cs"
    Inherits="ClassroomManagement" MasterPageFile="~/master/Master3.Master"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
   Liste des Classes
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
            /*--font-weight: bold;*/
            /*font-family: sans-serif;*/
        }

        #tblEditClass tr > td {
            padding-bottom: 10px;
        }

        .labelStyle1 {
            text-align: left;
            font-weight: bold;
        }

        .currencyDesign {
            text-align: right;
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlVacationConfig">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtClassMaxQuantityConfig" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtClassActualQuantityConfig" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassCurrentStatusConfig">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblClassCurrentStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtFixedCapacityConfig" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnAddVacationConfig" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="gridVacationConfig" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- <telerik:AjaxSetting AjaxControlID="ddlClassroomPrice">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCoursePricePerHour" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="radGridAffectedCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <asp:Panel runat="server" ID="pnlClassroomList">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4><span class="fa fa-list-ol"></span>&nbsp;Liste des classes</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblCode" runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                        <telerik:RadDropDownList runat="server" ID="ddlClassroom" Width="100%"
                            OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged" Skin="Office2007"
                            CausesValidation="false" AutoPostBack="true">
                            <Items>
                                <telerik:DropDownListItem Text="--Tout Sélectionner--" Selected="true" Value="-1" />
                                <telerik:DropDownListItem Text="1ere Annee Kindergarden " Value="1" />
                                <telerik:DropDownListItem Text="2ieme Annee Kindergarden" Value="2" />
                                <telerik:DropDownListItem Text="3ieme Annee Kindergarden " Value="3" />
                                <telerik:DropDownListItem Text="1ere Annee Fondamentale" Value="10" />
                                <telerik:DropDownListItem Text="2ieme Annee Fondamentale" Value="20" />
                                <telerik:DropDownListItem Text="3ieme Annee Fondamentale" Value="30" />
                                <telerik:DropDownListItem Text="4ieme Annee Fondamentale" Value="40" />
                                <telerik:DropDownListItem Text="5ieme Annee Fondamentale" Value="50" />
                                <telerik:DropDownListItem Text="6ieme Annee Fondamentale" Value="60" />
                                <telerik:DropDownListItem Text="7ieme Annee Fondamentale" Value="70" />
                                <telerik:DropDownListItem Text="8ieme Annee Fondamentale" Value="80" />
                                <telerik:DropDownListItem Text="9ieme Annee Fondamentale" Value="90" />
                                <telerik:DropDownListItem Text="3ieme Secondaire (NS1)" Value="100" />
                                <telerik:DropDownListItem Text="Seconde (NS2)" Value="110" />
                                <telerik:DropDownListItem Text="Retho (NS3)" Value="120" />
                                <telerik:DropDownListItem Text="Philo (NS4)" Value="130" />
                            </Items>
                        </telerik:RadDropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Année Académique" CssClass="app-label-design"></asp:Label>
                        <telerik:RadDropDownList runat="server" ID="ddlAcademicYear" Width="100%"
                            OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" Skin="Office2007"
                            CausesValidation="false" AutoPostBack="true" Font-Bold="true">
                        </telerik:RadDropDownList>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="xxxxxx" runat="server" Text="Statut" CssClass="app-label-design"></asp:Label>
                        <telerik:RadComboBox runat="server" Skin="Office2007"
                            Width="100%" ID="ddlClassroomCurrentStatus" CausesValidation="false"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlClassroomCurrentStatus_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="--Tout Sélectionner--" Value="-1" />
                                <telerik:RadComboBoxItem Value="1" Text="Activé" Selected="true" />
                                <telerik:RadComboBoxItem Value="0" Text="Désactivé" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                    ID="radGridClassroom"
                    OnNeedDataSource="radGridClassroom_NeedDataSource"
                    OnItemCommand="radGridClassroom_ItemCommand"
                    OnItemDataBound="radGridClassroom_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                        DataKeyNames="id" AllowPaging="false" ShowFooter="true">
                        <ColumnGroups>
                            <telerik:GridColumnGroup Name="vacation_capacity"
                                HeaderText="Effectif des classes par vacation">
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridColumnGroup>
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="No">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hiddenClassroomName" Value='<%# Eval("name") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenStatus" Value='<%# Eval("status") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                         <%--   <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-primary btn-sm" title="Configurer une classe"
                                        id="btnConfigClass" onserverclick="btnConfigClass_ServerClick"
                                        style="width: 100%;">
                                        <span class="fa fa-cogs"></span>&nbsp; Configurations
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-primary btn-sm" title="Affecter cours aux classe"
                                        id="btnAddCoursePrice" onserverclick="btnAddCoursePrice_ServerClick">
                                        <span class="fa fa-plus-circle"></span>&nbsp; Affecter cours
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Nom des classes" DataField="name">
                                <HeaderStyle Width="250px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Matin" DataField="am_cc" ColumnGroupName="vacation_capacity">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Median" DataField="pm_cc" ColumnGroupName="vacation_capacity">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Soir" DataField="ng_cc" ColumnGroupName="vacation_capacity">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="weekend" DataField="wk_cc" ColumnGroupName="vacation_capacity">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Année académique" DataField="academic_year_concat">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="Navy" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Statut">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus"
                                        Font-Bold="true"
                                        Text='<%# int.Parse(Eval("status").ToString()) == 1 ? "Activé" : "Désactivé" %>'
                                        ForeColor='<%# int.Parse(Eval("status").ToString()) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>

                                    <asp:HiddenField runat="server" ID="hiddenClassStatus" Value='<%# int.Parse(Eval("status").ToString()) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <button runat="server" class="btn btn-warning btn-sm" title="Changer Statut"
                                        id="btnActivateDesactivateClass" onserverclick="btnActivateDesactivateClass_ServerClick">
                                        <span class="fa fa-power-off"></span>
                                    </button>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </asp:Panel>



    <%--ATTACH COURSE TO CLASSROOM--%>
    <asp:Panel ID="pnlAddCoursToClassroom" runat="server" Visible="false" CssClass="panellDesign">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4><span class="fa fa-book"></span>&nbsp;Affecter cours aux classes</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4 col-md-offset-2">
                        <asp:Label runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                        <telerik:RadComboBox ID="ddlClassroomPrice" runat="server" Width="100%" Skin="Office2007"
                            CausesValidation="false" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlClassroomPrice_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="Cours" CssClass="app-label-design"></asp:Label>
                        <telerik:RadComboBox ID="ddlCourseNamePrice" runat="server" Width="100%" Skin="Office2007"
                            CheckBoxes="true" EnableCheckAllItemsCheckBox="true" MaxHeight="200">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                        <button type="button" class="btn btn-primary" id="btnAffectCoursePrice" runat="server"
                            onserverclick="btnAffectCoursePrice_ServerClick">
                            <span class="fa fa-save"></span>
                            <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                        &nbsp;&nbsp;
                        <button type="button" class="btn btn-default" id="btnReturn1" runat="server"
                            onserverclick="btnViewListClass_ServerClick">
                            <span class="fa fa-arrow-circle-o-left"></span>
                            <asp:Literal runat="server" Text="Retour à la liste des classes"></asp:Literal></button>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                        <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                            ID="radGridAffectedCourse" ClientSettings-DataBinding-ShowEmptyRowsOnLoad="true"
                            OnNeedDataSource="radGridAffectedCourse_NeedDataSource"
                            OnItemCommand="radGridAffectedCourse_ItemCommand"
                            OnItemDataBound="radGridAffectedCourse_ItemDataBound">
                            <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labelNo"></asp:Label>

                                            <asp:HiddenField runat="server" ID="hiddenClassId" Value='<%# Eval("class_id").ToString() %>' />
                                            <asp:HiddenField runat="server" ID="hiddenCourseId" Value='<%# Eval("cours_id").ToString() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <button runat="server" class="btn btn-danger btn-sm" title="Supprimer cours & prix"
                                                id="btnRemoveAffectedCourse"
                                                onserverclick="btnRemoveAffectedCourse_ServerClick">
                                                <span class="fa fa-remove"></span>
                                            </button>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cours" DataField="cours_name">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Coefficient">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtCoefficient" EmptyMessage="0"
                                                CssClass="currencyDesign" Width="100%" Skin="Web20" ForeColor="Red" Font-Bold="true"
                                                 Value='<%# double.Parse(Eval("coefficient").ToString()) %>'>
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                          <%--          <telerik:GridTemplateColumn HeaderText="Prix cours / heure">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtPricePerHour" EmptyMessage="0.00"
                                                CssClass="currencyDesign" Width="100%" Skin="Web20" ForeColor="Red" Font-Bold="true"
                                                 Value='<%# double.Parse(Eval("course_price").ToString()) %>'>
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>

                                    <%--  <telerik:GridBoundColumn HeaderText="Prix cours / heure" DataField="course_price" DataFormatString="{0:F2}">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" ForeColor="Red" Font-Bold="true" />
                                    </telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
