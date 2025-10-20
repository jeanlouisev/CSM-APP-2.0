<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterStudents.aspx.cs" Inherits="RegisterStudents"
    MasterPageFile="~/master/Master5.Master" %>

<%--<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register TagPrefix="art" TagName="bodyContainer" Src="~/design/BodyContainer/BC_RegisterStudents.ascx" %>--%>


<%--<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Inscription
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <%-- <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>


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
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">--%>



    <style type="text/css">
        #btnLoadImage {
            margin-top: 10px;
        }

        .file-upload {
            display: inline-block;
            overflow: hidden;
            position: relative;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #5C005C;
            background: #5C005C;
            color: #fff;
            border-radius: 6px;
            -moz-border-radius: 6px;
            text-shadow: #000 1px 1px 2px;
            -webkit-border-radius: 6px;
        }

        .file-upload {
            height: 1.3em;
        }

            .file-upload, .file-upload span {
                width: 3.5em;
            }

                .file-upload input {
                    position: absolute;
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    opacity: 0;
                    filter: alpha(opacity=0);
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                    cursor: pointer;
                }

                .file-upload span {
                    position: absolute;
                    top: 0;
                    left: 0;
                    display: inline-block;
                    padding-top: .15em;
                }

        #photoContainer {
            width: 100%;
            float: right;
            height: 100%;
            border: 1px solid silver;
            margin-bottom: 20px;
            -webkit-box-shadow: 0px 0px 30px silver;
            -moz-box-shadow: 0px 0px 30px silver;
            box-shadow: 0px 0px 30px silver;
        }

        .upper {
            text-transform: uppercase;
        }

        .hideUploadButton {
            visibility: hidden;
        }

        .lower {
            text-transform: lowercase;
        }

        .asterix {
            color: gray;
            font-weight: bold;
            font-size: medium;
        }
    </style>


    <script type="text/javascript">
        //function ShowDialogSearchStudent() {
        //    var oWnd = window.radopen("TimesheetDetails.aspx", "RadWindowSearchStudent");
        //    oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
        //    oWnd.SetSize(1100, 600);
        //    oWnd.center();
        //}

        function ClientCloseSearchStudent(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }


        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage.ClientID %>").click();
            }
        }


        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

        //function GridCreated(sender, args) {
        //    var scrollArea = sender.GridDataDiv;
        //    if (scrollArea != null) {
        //        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        //        if (dataHeight < 350) {
        //            scrollArea.style.height = dataHeight + "px";
        //        }
        //    }
        //}

    </script>


</asp:Content>










<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblCounter" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="gridListParent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardId" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardId" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCardId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardId" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClearParents">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imageKeeperReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentFirstName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentLastName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlParentSex" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlParentMaritalStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentPhone" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentAdress" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="gridListParent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
            <telerik:AjaxSetting AjaxControlID="radBirthDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radBirthDate" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtEmail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEmail" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblEmailError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Bootstrap" OnClientClose="ClientCloseSearchStudent">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <!-- Breadcubs Area Start Here -->
    <div class="breadcrumbs-area">
        <%-- <h3>Eleve</h3>--%>
        <ul>
            <li>
                <a href="#">Élèves</a>
            </li>
            <li>Inscription</li>
        </ul>
    </div>
    <!-- Breadcubs Area End Here -->


    <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3>Ajouter Nouveau Élève</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10" style="border: 0px solid red;">
                    <div class="row">
                        <%--   <div class="col-md-4">
                            <asp:Label runat="server" Text="Code" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtCode" ReadOnly="true" Font-Bold="true" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>--%>
                        <div class="col-md-4">
                            <asp:Label ID="lblFirstName" runat="server" Text="Nom" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLastName" runat="server" Text="Prénom" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblSex" runat="server" Text="Sexe" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlSex" runat="server" Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                    <telerik:DropDownListItem Value="M" Text="Masculin" />
                                    <telerik:DropDownListItem Value="F" Text="Feminin" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label3" runat="server" Text="CIN/NIF" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtCardId" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblBirthPlace" runat="server" Text="Lieu de naissance" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtBirthPlace" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblBirthDate" runat="server" Text="Date de naissance" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDatePicker ID="radBirthDate" runat="server"
                                Skin="Bootstrap" MinDate="1800-01-01" Width="100%"
                                OnSelectedDateChanged="radBirthDate_SelectedDateChanged">
                                <DateInput CausesValidation="false" AutoPostBack="true" runat="server"
                                    DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblPhone1" runat="server" Text="Téléphone" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <telerik:RadNumericTextBox ID="txtPhone1" runat="server"
                                Font-Size="Small" MaxLength="8" Width="100%" Skin="Bootstrap"
                                ForeColor="Black" Type="Number"
                                EmptyMessageStyle-Font-Italic="true">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblAddress" runat="server" Text="Adresse" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="RadLabel10" runat="server" Text="Email" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="100%"
                                Skin="Bootstrap" EmptyMessageStyle-Font-Italic="true" CssClass="lower"
                                CausesValidation="false" AutoPostBack="true"
                                OnTextChanged="txtEmail_TextChanged">
                            </telerik:RadTextBox>
                            <asp:Label runat="server" ID="lblEmailError" Text="Erreur : Adresse email est invalide !"
                                Visible="false" ForeColor="Red" Font-Italic="true" Font-Size="Smaller"></asp:Label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="RadLabel12" runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlClassroom" runat="server" Width="100%"
                                DropDownHeight="250px" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblVacation" runat="server" Text="Vacation" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlVacation" runat="server"
                                Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="AM" Text="Matin" />
                                    <telerik:DropDownListItem Value="PM" Text="Median" />
                                    <telerik:DropDownListItem Value="NG" Text="Soir" />
                                    <telerik:DropDownListItem Value="WK" Text="Weekend" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" Text="Année Académique" Skin="Bootstrap" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtAcademicYear" ReadOnly="true" Font-Bold="true" runat="server" Width="100%" Skin="Bootstrap">
                            </telerik:RadTextBox>
                            <asp:HiddenField runat="server" ID="hiddenAcademicYearId" />
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <img runat="server" id="imgStudent" src="../images/image_data/Default.png"
                                class="img-bordered-sm" style="width: 120px; height: 120px" />

                            <div style="margin: auto; width: 120px; text-align: center">
                                <asp:FileUpload runat="server" ID="imageUploader" Width="90" />

                                <asp:ImageButton ID="btnUploadImage" OnClick="btnUploadImage_Click"
                                    runat="server" ImageUrl="~/images/uploadButton1.png"
                                    Width="0px" Height="0px" CssClass="hideUploadButton" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    
    
    <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3>Documents Administratifs</h3>
                </div>
            </div>
          
            <div class="row">
                <div class="col-md-5">
                    <asp:Label ID="Label7" runat="server" Text="Description" CssClass="app-label-design">
                    </asp:Label>
                    <telerik:RadDropDownList ID="ddlDocumentCategory" Width="100%" runat="server" Skin="Bootstrap">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label6" runat="server" Text="Pièces Jointes" CssClass="app-label-design">
                    </asp:Label>
                    <asp:FileUpload ID="documentsAttachFile" runat="server" Width="100%" />
                </div>
                <div class="col-md-3">
                    <span runat="server">
                        <br />
                        <button type="button" class="btn btn-primary btn-lg" id="btnAttachDocuments" runat="server"
                            onserverclick="btnAttachDocuments_ServerClick">
                            <span class="fa fa-plus"></span>
                            <asp:Literal runat="server" Text="Ajouter"></asp:Literal></button>
                    </span>
                </div>
            </div>




            <br />
            <div class="row">
                <div class="col-md-12" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Bootstrap" RenderMode="Lightweight"
                        ID="gridAttachDocuments"
                        OnNeedDataSource="gridAttachDocuments_NeedDataSource"
                        OnItemCommand="gridAttachDocuments_ItemCommand"
                        OnItemDataBound="gridAttachDocuments_ItemDataBound">
                        <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="No">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labelNo"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                             
                                <telerik:GridBoundColumn HeaderText="Description" DataField="document_name">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Pièces Jointes" DataField="document_path">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Date d'ajout" DataField="upload_time">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>


    <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3>Personne à contacter en cas d'urgence</h3>
                </div>
            </div>

            <%--     <div class="panel panel-info">
                <div class="panel-heading">
                    <h4><span class="fa fa-users"></span>&nbsp;Personne à contacter en cas d'urgence</h4>
                </div>
                <div class="panel-body">--%>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="RadLabel1" runat="server" Text="Nom" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentFirstName" runat="server" Width="100%" Skin="Bootstrap" MaxLength="30">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel3" runat="server" Text="Prénom" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentLastName" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel5" runat="server" Text="Sexe" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadDropDownList ID="ddlParentSex" Width="100%" runat="server" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                            <telerik:DropDownListItem Value="M" Text="Masculin" />
                            <telerik:DropDownListItem Value="F" Text="Feminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Lieu de naissance" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentBirthPlace" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label4" runat="server" Text="Date de naissance" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadDatePicker ID="radParentBirthDate" runat="server"
                        Skin="Bootstrap" MinDate="1800-01-01" Width="100%"
                        OnSelectedDateChanged="radBirthDate_SelectedDateChanged">
                        <DateInput CausesValidation="false" AutoPostBack="true" runat="server"
                            DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label5" runat="server" Text="Profession" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentOccupation" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label8" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlParentMaritalStatus" runat="server" Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                    <telerik:DropDownListItem Value="C" Text="Célibataire" />
                                    <telerik:DropDownListItem Value="M" Text="Marié(e)" />
                                    <telerik:DropDownListItem Value="D" Text="Divorcé(e)" />
                                    <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                                    <telerik:DropDownListItem Value="UL" Text="Union libre" />
                                </Items>
                            </telerik:RadDropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="Label9" runat="server" Text="NIF" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentIdCard" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>

            </div>
            <div class="row">

                <div class="col-md-3">
                    <asp:Label ID="Label11" runat="server" Text="Poste Occupe" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentJobTitle" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel4" runat="server" Text="Téléphone" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadNumericTextBox ID="txtParentPhone" runat="server"
                        Font-Size="Small" MaxLength="8" Width="100%" Skin="Bootstrap" ForeColor="Black" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                    </telerik:RadNumericTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label10" runat="server" Text="E-mail" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentEmail" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="RadLabel6" runat="server" Text="Addresse" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentAddress" runat="server" MaxLength="200" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" Text="Lien de parenté" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadDropDownList ID="ddlParentRelationship" Width="100%" runat="server" Skin="Bootstrap" DropDownHeight="100px">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                            <telerik:DropDownListItem Value="FATHER" Text="Père" />
                            <telerik:DropDownListItem Value="MOTHER" Text="Mère" />
                            <telerik:DropDownListItem Value="UNCLE" Text="Oncle" />
                            <telerik:DropDownListItem Value="AUNTIE" Text="Tante" />
                            <telerik:DropDownListItem Value="BROTHER" Text="Frere" />
                            <telerik:DropDownListItem Value="SISTER" Text="Soeur" />
                            <telerik:DropDownListItem Value="COUSIN" Text="Cousin(e)" />
                            <telerik:DropDownListItem Value="GOD_FATHER" Text="Parrain" />
                            <telerik:DropDownListItem Value="GOD_MOTHER" Text="Marraine" />
                            <telerik:DropDownListItem Value="HUSBAND" Text="Epoux" />
                            <telerik:DropDownListItem Value="WIFE" Text="Epouse" />
                            <telerik:DropDownListItem Value="BOYFRIEND" Text="Copain" />
                            <telerik:DropDownListItem Value="GIRLFRIEND" Text="Copine" />
                            <telerik:DropDownListItem Value="NEIGHBOR" Text="Voisin(e)" />
                            <telerik:DropDownListItem Value="OTHER" Text="Autre" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>


                <br />
                <br />
                <br />

                <div class="col-12 form-group mg-t-8">
                    <button type="button" class="btn-fill-lg btn-gradient-yellow btn-hover-bluedark" id="btnSave" runat="server"
                        onserverclick="btnSave_Click">
                        Sauvegarder
                    </button>

                    <button type="button" class="btn-fill-lg bg-blue-dark btn-hover-yellow" id="btnBack" runat="server"
                        onserverclick="btnBack_Click">
                        Annuler</button>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
