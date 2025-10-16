<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxConfigurationManagement.aspx.cs"
    Inherits="TaxConfigurationManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Configuration des taxes
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

        .upperCaseOnly {
            text-transform: uppercase;
        }

        .currencyDesign {
            text-align: right;
            color: red;
            font-weight: bold;
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
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <%--   <telerik:AjaxSetting AjaxControlID="txtStaffCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtStaffFullname" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtStaffStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>






    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-cog"></span>&nbsp; Configuration des taxes</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblRoleName" runat="server" Text="Nom Groupe"></asp:Label>
                    <telerik:RadTextBox ID="txtGroupName" runat="server" Width="100%" MaxLength="50"
                        CssClass="upperCaseOnly" Skin="Office2007">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDetails" runat="server" Text="Montant Taxes Divers"></asp:Label>
                    <telerik:RadNumericTextBox runat="server" ID="txtFixedTaxAmount" EmptyMessage="0.00"
                        CssClass="currencyDesign" Width="100%" Skin="Office2007" ForeColor="Red" Font-Bold="true">
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:CheckBox runat="server" Text="ONA (6%)" ID="chkONA" Font-Size="Small" />
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:CheckBox runat="server" Text="IRI (2%)" ID="chkIRI" Font-Size="Small" />
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:CheckBox runat="server" Text="FDU (1%)" ID="chkFDU" Font-Size="Small" />
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:CheckBox runat="server" Text="CAS (2%)" ID="chkCAS" Font-Size="Small" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-primary" id="btnAddTaxe" runat="server"
                        onserverclick="btnAddTaxe_Click" causesvalidation="true" style="width: 120px; margin-top: 20px;">
                        <span class="fa fa-save"></span>&nbsp;
                        <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="radGridTaxes"
                        OnNeedDataSource="radGridTaxes_NeedDataSource"
                        OnItemCommand="radGridTaxes_ItemCommand"
                        OnItemDataBound="radGridTaxes_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed" DataKeyNames="id">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Nom Groupe" DataField="group_name">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Montant Taxes Divers" DataField="fixed_tax" DataFormatString="{0:N}">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="ONA">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" runat="server" Font-Bold="true"
                                            Checked='<%# Convert.ToInt32(Eval("ona")) == 1 ? true : false %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="IRI">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" runat="server" Font-Bold="true"
                                            Checked='<%# Convert.ToInt32(Eval("iri")) == 1 ? true : false %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="FDU">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" runat="server" Font-Bold="true"
                                            Checked='<%# Convert.ToInt32(Eval("fdu")) == 1 ? true : false %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="CAS">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" runat="server" Font-Bold="true"
                                            Checked='<%# Convert.ToInt32(Eval("cas")) == 1 ? true : false %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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


        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
