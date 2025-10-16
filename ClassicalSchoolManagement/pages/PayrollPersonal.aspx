<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollPersonal.aspx.cs" EnableEventValidation="false"
    Inherits="PayrollPersonal" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Payroll des personnels
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
        / .labelDesign {
            font-size: small;
            --font-weight: bold;
            font-family: sans-serif;
        }

        #photoContainer {
            width: 100%;
            float: right;
            height: 70%;
            border: 1px solid silver;
            margin-bottom: 20px;
            -webkit-box-shadow: 0px 0px 30px silver;
            -moz-box-shadow: 0px 0px 30px silver;
            box-shadow: 0px 0px 30px silver;
        }

        .hideUploadButton {
            visibility: hidden;
        }

        .fullnameDesign {
            background-color: transparent;
            border-left: 0px;
            border-top: 0px;
            border-bottom: 0px;
            border-right: 0px;
            font-size: smaller;
        }

        .amountRight {
            text-align: right;
        }

        .warningDesign {
            font-style: italic;
            font-size: smaller;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            color: purple;
            font-weight: bold;
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

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }


        function ClientCloseStaffAmount(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007" BackColor="Transparent">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" Modal="true" IconUrl="~/images/graduation_cap_icon.png"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" IconUrl="~/images/saphir_logo_ico.jpg"
                Skin="Office2007" DestroyOnClose="false" OnClientClose="ClientCloseStaffAmount">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" IconUrl="~/images/saphir_logo_ico.jpg"
                Skin="Office2007" DestroyOnClose="false" OnClientClose="ClientCloseStaffAmount">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <div class="row">
        <div class="col-md-6">
            <telerik:RadTabStrip CausesValidation="false" RenderMode="Lightweight"
                runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
                <Tabs>
                    <telerik:RadTab Text="Payroll des personnels"></telerik:RadTab>
                    <telerik:RadTab Text="Historiques Payroll"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage">
                <%--page 1--%>
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="RadLabel5" runat="server" Text="Année Académique"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlAcademicYear" runat="server" Width="100%"
                                        Skin="Bootstrap" Font-Bold="true">
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label1" runat="server" Text="Mois"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlMonths" runat="server" Width="100%" Skin="Bootstrap">
                                        <Items>
                                            <telerik:DropDownListItem Value="SEP" Text="Septembre" />
                                            <telerik:DropDownListItem Value="OCT" Text="Octobre" />
                                            <telerik:DropDownListItem Value="NOV" Text="Novembre" />
                                            <telerik:DropDownListItem Value="DEC" Text="Décembre" />
                                            <telerik:DropDownListItem Value="JAN" Text="Janvier" />
                                            <telerik:DropDownListItem Value="FEB" Text="Février" />
                                            <telerik:DropDownListItem Value="MAR" Text="Mars" />
                                            <telerik:DropDownListItem Value="APR" Text="Avril" />
                                            <telerik:DropDownListItem Value="MAY" Text="Mai" />
                                            <telerik:DropDownListItem Value="JUN" Text="Juin" />
                                            <telerik:DropDownListItem Value="JUL" Text="Juillet" />
                                            <telerik:DropDownListItem Value="AUG" Text="Août" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-5">
                                    <button type="button" class="btn btn-primary" id="btnSearch" runat="server"
                                        onserverclick="btnSearch_Click" causesvalidation="true" style="margin-top: 20px;">
                                        <span class="fa fa-search"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                                    &nbsp;
                            
                            <button type="button" class="btn btn-success" id="btnCalculatePayroll" runat="server"
                                onserverclick="btnCalculatePayroll_Click" causesvalidation="true" style="margin-top: 20px;">
                                <span class="fa fa-dollar"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Calculer Payroll"></asp:Literal></button>
                                    &nbsp;
                            
                            <button type="button" class="btn btn-default" id="btnExportExcel" runat="server"
                                onserverclick="btnExportExcelStaffPayroll_ServerClick" causesvalidation="true" style="margin-top: 20px;">
                                <span class="fa fa-file-excel-o"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Exporter en excel"></asp:Literal></button>
                                </div>
                            </div>
                            <br />
                            <br />


                            <div class="row">
                                <div class="col-md-8" style="width: 100%;">
                                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                        ID="radGridPayrollPersonal" Font-Size="Small"
                                        OnItemCommand="radGridPayrollPersonal_ItemCommand"
                                        OnItemDataBound="radGridPayrollPersonal_ItemDataBound">
                                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Code" DataField="staff_code">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nom & Prenom" DataField="fullName">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Année Académique" DataField="years">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Mois" DataField="salary_month_fr">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Salaire Brut" DataField="contract_salary" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="ONA (6%)" DataField="ona_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="IRI (2%)" DataField="iri_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="FDU (1%)" DataField="fdu_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="CAS (2%)" DataField="cas_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Montant Taxes Divers" DataField="fixed_tax" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Déduction" DataField="deduction" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Salaire Net" DataField="net_salary" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                                <%--      <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <button runat="server" class="btn btn-sm btn-danger" title="Supprimer"
                                                            id="btnDelete" onserverclick="btnDelete_ServerClick">
                                                            <span class="fa fa-remove"></span>
                                                        </button>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
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
                                <div class="col-md-2">
                                    <asp:Label ID="Labelxx" runat="server" Text="Code "></asp:Label>
                                   <telerik:RadTextBox runat="server" ID="txtCodeHis" Skin="Bootstrap" Width="100%">
                                   </telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label2" runat="server" Text="Année Académique"></asp:Label>
                                    <telerik:RadDropDownList ID="ddlAcademicYearHistoric" runat="server" Width="100%"
                                        Skin="Bootstrap" Font-Bold="true">
                                    </telerik:RadDropDownList>
                                </div>
                                <div class="col-md-5">
                                    <button type="button" class="btn btn-primary" id="btnSearchHistoric" runat="server"
                                        onserverclick="btnSearchHistoric_ServerClick" causesvalidation="true" style="margin-top: 20px;">
                                        <span class="fa fa-search"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                                    &nbsp;
                            
                            
                            <button type="button" class="btn btn-default" id="btnExportExcelHis" runat="server"
                                onserverclick="btnExportExcelHis_ServerClick" causesvalidation="true" style="margin-top: 20px;">
                                <span class="fa fa-file-excel-o"></span>&nbsp;
                                        <asp:Literal runat="server" Text="Exporter en excel"></asp:Literal></button>
                                </div>
                            </div>
                            <br />
                            <br />


                            <div class="row">
                                <div class="col-md-8" style="width: 100%;">
                                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                                        ID="radGridPayrollHis" Font-Size="Small"
                                        OnItemCommand="radGridPayrollHis_ItemCommand"
                                        OnItemDataBound="radGridPayrollHis_ItemDataBound">
                                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Code" DataField="staff_code">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nom & Prenom" DataField="fullName">
                                                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Année Académique" DataField="years">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Mois" DataField="salary_month_fr">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Salaire Brut" DataField="contract_salary" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="ONA (6%)" DataField="ona_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="IRI (2%)" DataField="iri_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="FDU (1%)" DataField="fdu_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="CAS (2%)" DataField="cas_tax_amount" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Montant Taxes Divers" DataField="fixed_tax" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Déduction" DataField="deduction" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Salaire Net" DataField="net_salary" DataFormatString="{0:N}">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                                <%--      <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <button runat="server" class="btn btn-sm btn-danger" title="Supprimer"
                                                            id="btnDelete" onserverclick="btnDelete_ServerClick">
                                                            <span class="fa fa-remove"></span>
                                                        </button>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
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
