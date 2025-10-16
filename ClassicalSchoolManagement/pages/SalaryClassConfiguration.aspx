<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryClassConfiguration.aspx.cs"
    Inherits="SalaryClassConfiguration" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Configuration Salaire/Classe
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


        #tblEditClass tr > td {
            padding-bottom: 10px;
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

           function ClientClose(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radGridClassroomList" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
        ReloadOnShow="true" BackColor="White" runat="server" 
        EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" OnClientClose="ClientClose"
                Skin="Office2007" DestroyOnClose="false" Behaviors="Close,Minimize">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <%--  <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-cog"></span>&nbsp;Configuration Salaire/Classe</h4>
        </div>
        <div class="panel-body">--%>

    <div class="row">
        <div class="col-md-6">
            <telerik:RadTabStrip CausesValidation="false" RenderMode="Lightweight"
                runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
                <Tabs>
                  <telerik:RadTab Text="Configuration Paiement/Classe"></telerik:RadTab>
                    <telerik:RadTab Text="Liste des prix par cours"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                <%--page 1--%>
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8" style="width: 100%;">
                                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                        ID="radGridClassroomList"
                                        OnNeedDataSource="radGridClassroomList_NeedDataSource"
                                        OnItemCommand="radGridClassroomList_ItemCommand"
                                        OnItemDataBound="radGridClassroomList_ItemDataBound">
                                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Classe" DataField="name">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Statut">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatusClass" Text='<%# Eval("status").ToString() == "1" ? "Actif" : "Inactif" %>'
                                                            runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Montant Salaire (HTG)">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" Text='<%# double.Parse(Eval("amount").ToString()) > 0 ? Eval("amount").ToString() : "" %>'
                                                            runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <button runat="server" class="btn btn-sm btn-primary" title="Definir Montant"
                                                            id="btnDefineAmount" onserverclick="btnDefineAmount_ServerClick">
                                                            <span class="fa fa-dollar"></span>&nbsp;Definir Montant
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
                </telerik:RadPageView>


                <%--page 2--%>
                <telerik:RadPageView runat="server" ID="RadPageView2">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3 col-md-offset-1"></div>
                                <div class="col-md-3">
                                    <asp:Label ID="RadLabel5" runat="server" Text="Prix / Heure"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txtCoursePrice" runat="server" Width="100%" Skin="Bootstrap">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" class="btn btn-primary" id="btnAddPrice" runat="server"
                                        onserverclick="btnAddPrice_Click" causesvalidation="true" style="width: 120px; margin-top: 20px;">
                                        <span class="fa fa-save"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                                </div>
                            </div>
                            <br /><br />

                        <div class="row">
                            <div class="col-md-8" style="width: 100%;">
                                <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                    ID="radGridPrices"
                                    OnItemCommand="radGridPrices_ItemCommand"
                                    OnItemDataBound="radGridPrices_ItemDataBound">
                                    <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="No">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderText="Prix/Heure (HTG)" DataField="amount" DataFormatString="{0:N}">
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <button runat="server" class="btn btn-sm btn-danger" title="Supprimer"
                                                        id="btnDelete" onserverclick="btnDelete_ServerClick">
                                                        <span class="fa fa-remove"></span>
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

                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>


</asp:Content>
