<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchTeachers.aspx.cs" EnableEventValidation="false"
    Inherits="SearchTeacher" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Search Teacher
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


        .lower {
            text-transform: lowercase;
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

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkNif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNifReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCinReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacation" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassroomStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacationStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCardIdTeacher">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardIdTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtParentIdCard">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtParentIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlPositionReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlClassRoomReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblCode" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlClassRoomReference">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacationReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVacation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCapacity" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radBirthDateStudent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radBirthDateStudent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtEmailTeacher">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEmailTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>





    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-list-ol"></span>&nbsp;Liste des professeurs</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="RadLabel5" runat="server" Text="Code" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtStaffCode" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel6" runat="server" Text="Nom complet" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtFullName" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>

                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblPhone" runat="server" Text="Téléphone" CssClass="app-label-design"></asp:Label>
                    <telerik:RadTextBox ID="txtPhone" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>

                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel7" runat="server" Text="Sexe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="M" Text="Masculin" />
                            <telerik:DropDownListItem Value="F" Text="Feminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblSex" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlMaritalStatus" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="C" Text="Célibataire" />
                            <telerik:DropDownListItem Value="M" Text="Marié(e)" />
                            <telerik:DropDownListItem Value="D" Text="Divorcé(e)" />
                            <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel4" runat="server" Text="Niveau" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlLevel" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="Normalien" Text="Normalien" />
                            <telerik:DropDownListItem Value="Professionnel" Text="Professionnel" />
                            <telerik:DropDownListItem Value="Universitaire" Text="Universitaire" />
                            <telerik:DropDownListItem Value="Licencier" Text="Licencier" />
                            <telerik:DropDownListItem Value="Maitrise" Text="Maitrise" />
                            <telerik:DropDownListItem Value="Doctorat" Text="Doctorat" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-primary" id="Button1" runat="server"
                        onserverclick="btnSearch_Click" width="120px">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>

                    <button type="button" class="btn btn-default" id="btnExportExcel" runat="server"
                        onserverclick="btnExportExcel_ServerClick" width="120px">
                        <span class="fa fa-file-excel-o"></span>
                        <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                        ID="gridTeacher"
                        OnNeedDataSource="gridTeacher_NeedDataSource"
                        OnItemCommand="gridTeacher_ItemCommand"
                        OnItemDataBound="gridTeacher_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                            DataKeyNames="id" AllowPaging="true" ShowFooter="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("classroom_id") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                            id="btnDelete" onserverclick="removeTeacher">
                                            <span class="fa fa-remove"></span>
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
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Code" DataField="id">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nom complet" DataField="fullName">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Sexe">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                       <asp:Literal runat="server" Text='<%# Eval("sex").ToString() == "M" ? "Masculin" : "Féminin"  %>'></asp:Literal>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="État Civil" DataField="marital_status_definition">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Niveau d’étude" DataField="study_level">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Téléphone" DataField="phone1">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <HeaderStyle Width="100px" />
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

