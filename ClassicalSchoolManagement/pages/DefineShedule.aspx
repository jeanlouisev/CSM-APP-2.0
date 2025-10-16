<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefineShedule.aspx.cs"
    Inherits="DefineShedule" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Define Schedule
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

        .EditClassroomTableCells {
            width: 243px;
        }

        .labelHeaderDesign {
            font-size: small;
            text-align: center;
            font-weight: bold;
        }

        .labelContentDesign {
            font-size: small;
            color: purple;
        }
    </style>

    <script type="text/javascript">
        //toggle button click maximize page
        $(document).ready(function () {
            $('#toggleLink').click();
        });


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
                    <telerik:AjaxUpdatedControl ControlID="radGridSchedule" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radGridSchedule" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>


    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>


            <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" Skin="Office2007" Behaviors="Close" OnClientClose="ClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>






    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-calendar"></span>&nbsp;Horaire de Travail</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCode" runat="server" Text="Classe"></asp:Label>
                    <telerik:RadComboBox runat="server" ID="ddlClassroom" Width="100%" Skin="Office2007" MaxHeight="200">
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Vacation"></asp:Label>
                    <telerik:RadComboBox runat="server" ID="ddlVacation" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:RadComboBoxItem Value="-1" Text="--Tout Sélectionner--"/>
                            <telerik:RadComboBoxItem Value="AM" Text="Matin" Selected="true"  />
                            <telerik:RadComboBoxItem Value="PM" Text="Median" />
                            <telerik:RadComboBoxItem Value="NG" Text="Soir" />
                            <telerik:RadComboBoxItem Value="WK" Text="Weekend" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Année Académique"></asp:Label>
                    <telerik:RadComboBox runat="server" ID="ddlAcademicYear"
                        Width="100%" Skin="Office2007" MaxHeight="200">
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-3">
                    <button type="button" class="btn btn-sm btn-primary" id="btnSearch" runat="server"
                        onserverclick="btnSearch_Click" style="width: 120px; margin-top: 10px;">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                </div>
            </div>



            <%--*************  RAD GRID VIEW ********************--%>

            <br />
            <div class="row" runat="server">
                <div class="col-md-12" style="width: 100%;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="radGridSchedule"
                        OnNeedDataSource="radGridSchedule_NeedDataSource"
                        OnItemCommand="radGridSchedule_ItemCommand"
                        OnItemDataBound="radGridSchedule_ItemDataBound"
                        Font-Size="Small">
                        <ClientSettings Scrolling-AllowScroll="true" Scrolling-ScrollHeight="500"></ClientSettings>
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id" AllowPaging="true" ShowFooter="true" PageSize="10">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenVacation" Value='<%# Eval("vacation").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hiddenClassName" Value='<%# Eval("class_name").ToString() %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Classe" DataField="class_name">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation_definition">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Lundi" DataField="monday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Mardi" DataField="tuesday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Mercredi" DataField="wednesday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Jeudi" DataField="thursday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Vendredi" DataField="friday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <%--       <telerik:GridBoundColumn HeaderText="Samedi" DataField="saturday">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-success btn-sm" title="Definir l'horaire"
                                            id="btnAddSchedule" onserverclick="btnAddSchedule_ServerClick">
                                            <span class="fa fa-plus"></span>
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
