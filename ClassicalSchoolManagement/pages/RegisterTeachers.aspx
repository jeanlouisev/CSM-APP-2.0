<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterTeachers.aspx.cs"
    Inherits="RegisterTeacher" MasterPageFile="~/master/Master3.Master" %>


<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Nouveau Professeur
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
            color: red;
            font-weight: bold;
            font-size: medium;
        }
    </style>

    <script type="text/javascript">
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


        function ShowDialogSearchStudent() {
            var oWnd = window.radopen("TimesheetDetails.aspx", "RadWindowSearchStudent");
            oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Fade);
            oWnd.SetSize(1100, 600);
            oWnd.center();
        }

        function ClientCloseSearchStudent(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }
    </script>


</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
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
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacation" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCardId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardId" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtParentIdCard">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imageKeeperReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentFirstName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentLastName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlParentSex" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlParentMaritalStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentPhone" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtParentAdress" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-info-circle"></span>&nbsp;Information du professeur
            <span class="pull-right">
                <asp:Label runat="server" ID="lblTeacherCode" Font-Bold="true" ForeColor="Red"></asp:Label></span></h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <img runat="server" id="imgTeacher" src="../images/image_data/Default.png"
                                class="img-bordered-sm" style="width: 120px; height: 120px" />

                            <div style="margin: auto; width: 120px; text-align: center">
                                <asp:FileUpload runat="server" ID="imageUploader" Width="90%" />

                                <asp:ImageButton ID="btnUploadImage" OnClick="btnUploadImage_Click" CssClass="hideUploadButton"
                                    runat="server" ImageUrl="~/images/uploadButton1.png" Width="50px" Height="50px" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-10" style="border: 0px solid red;">
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblFirstName" runat="server" Text="Nom"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblLastName" runat="server" Text="Prénom"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblSex" runat="server" Text="Sexe"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlSex" runat="server" Skin="Office2007" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="M" Text="Masculin" />
                                    <telerik:DropDownListItem Value="F" Text="Feminin" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblBirthPlace" runat="server" Text="Lieu de naissance"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtBirthPlace" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblBirthDate" runat="server" Text="Date de naissance"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDatePicker ID="radBirthDate" runat="server"
                                Skin="Web20" MinDate="1800-01-01" Width="100%">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblPhone1" runat="server" Text="Téléphone"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadNumericTextBox ID="txtPhone1" runat="server"
                                Font-Size="Small" Width="100%" Skin="Web20"
                                ForeColor="Black" Type="Number"
                                EmptyMessageStyle-Font-Italic="true">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblAddress" runat="server" Text="Adresse"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtAddress" MaxLength="150" runat="server" Width="100%" Skin="Web20"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="CIN/NIF"></asp:Label>
                            <telerik:RadTextBox ID="txtCardId" runat="server" Width="100%" Skin="Web20">
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="Rdlemail" runat="server" Text="E-mail"></asp:Label>
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="100%"
                                Skin="Web20" EmptyMessageStyle-Font-Italic="true" CssClass="lower"
                                CausesValidation="false" AutoPostBack="true"
                                OnTextChanged="txtEmail_TextChanged">
                            </telerik:RadTextBox>
                            <asp:Label runat="server" ID="lblEmailError" Text="Erreur : Adresse email est invalide !"
                                Visible="false" ForeColor="Red" Font-Italic="false" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblMaritalStatus" runat="server" Text="État Civil"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlMaritalStatus" Width="100%" runat="server" Skin="Office2007">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="C" Text="Célibataire" />
                                    <telerik:DropDownListItem Value="M" Text="Marié(e)" />
                                    <telerik:DropDownListItem Value="D" Text="Divorcé(e)" />
                                    <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                                    <telerik:DropDownListItem Value="UL" Text="Union libre" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="RadLabel3" runat="server" Text="Niveau d’étude"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlStudyLevel" Width="100%" runat="server" Skin="Office2007">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="Normalien" Text="Normalien" />
                                    <telerik:DropDownListItem Value="Professionnel" Text="Professionnel" />
                                    <telerik:DropDownListItem Value="Universitaire" Text="Universitaire" />
                                    <telerik:DropDownListItem Value="Licencier" Text="Licencier" />
                                    <telerik:DropDownListItem Value="Maitrise" Text="Maitrise" />
                                    <telerik:DropDownListItem Value="Doctorat" Text="Doctorat" />
                                    <telerik:DropDownListItem Value="Professorat" Text="Professorat" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="RadLabelxx" runat="server" Text="Type de taxe"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlTax" Width="100%" runat="server" Skin="Office2007">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-users"></span>&nbsp;Personne de contact en cas d'urgence</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-3">
                    <asp:Label ID="Label2" runat="server" Text="Nom">
                    </asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentFirstName" runat="server" Width="100%" Skin="Web20" MaxLength="30">
                    </telerik:RadTextBox>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Label1" runat="server" Text="Prénom"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentLastName" MaxLength="30" runat="server" Width="100%" Skin="Web20">
                    </telerik:RadTextBox>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="RadLabel5" runat="server" Text="Sexe"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadDropDownList ID="ddlParentSex" runat="server" Skin="Office2007" Width="100%">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                            <telerik:DropDownListItem Value="M" Text="Masculin" />
                            <telerik:DropDownListItem Value="F" Text="Feminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="RadLabel4" runat="server" Text="Téléphone"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadNumericTextBox ID="txtParentPhone" runat="server"
                        Font-Size="Small" MaxLength="8" Width="100%" Skin="Web20" ForeColor="Black" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                    </telerik:RadNumericTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-3">
                    <asp:Label ID="RadLabel6" runat="server" Text="Addresse"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentAdress" runat="server" MaxLength="200" Width="100%" Skin="Web20">
                    </telerik:RadTextBox>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Label4" runat="server" Text="Lien de parenté"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadDropDownList ID="ddlParentRelationship" runat="server"
                        Skin="Office2007" Width="100%" DropDownHeight="200px">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                            <telerik:DropDownListItem Value="FATHER" Text="Père" />
                            <telerik:DropDownListItem Value="MOTHER" Text="Mère" />
                            <telerik:DropDownListItem Value="UNCLE" Text="Oncle" />
                            <telerik:DropDownListItem Value="AUNTIE" Text="Tante" />
                            <telerik:DropDownListItem Value="BROTHER" Text="Frere" />
                            <telerik:DropDownListItem Value="SISTER" Text="Soeur" />
                            <telerik:DropDownListItem Value="COUSIN" Text="Cousin (e)" />
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
            </div>
        </div>
    </div>



    <div class="panel panel-info" runat="server" id="pnlDocuments" visible="false">
        <div class="panel-heading">
            <h4><span class="fa fa-file-archive-o"></span>&nbsp;Documents Administratifs</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-3">
                    <asp:Label ID="Label7" runat="server" Text="Description">
                    </asp:Label>
                    <telerik:RadDropDownList ID="ddlDocumentCategory" Width="100%" runat="server" Skin="Office2007">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Label6" runat="server" Text="Pièces Jointes">
                    </asp:Label>
                    <asp:FileUpload ID="documentsAttachFile" runat="server" Width="100%" />
                </div>
                <div class="col-sm-3">
                    <span runat="server" style="position: absolute; margin-top: 10px;">
                        <button type="button" class="btn btn-sm btn-primary" id="btnAttachDocuments" runat="server"
                            onserverclick="btnAttachDocuments_ServerClick" width="120px">
                            <span class="fa fa-plus"></span>
                            <asp:Literal runat="server" Text="Ajouter"></asp:Literal></button>
                    </span>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-8" style="width: 100%; overflow-x: scroll;">
                    <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
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
                             <%--   <telerik:GridTemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button runat="server" class="btn btn-danger btn-sm" title="Cliquer ici pour supprimer"
                                            id="btnRemoveDocuments" onserverclick="btnRemoveDocuments_ServerClick">
                                            <span class="fa fa-remove"></span>
                                        </button>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn HeaderText="Description" DataField="document_name">
                                    <HeaderStyle Width="100px" />
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





    <div class="row">
        <div class="col-xs-12 text-center">
            <button type="button" class="btn btn-primary btn-sm" id="Button1" runat="server"
                onserverclick="btnSave_Click" style="width: 120px;">
                <span class="fa fa-save"></span>
                <asp:Literal runat="server" Text="Sauvegarder"></asp:Literal></button>
            &nbsp;&nbsp;
            <button type="button" class="btn btn-danger btn-sm" id="btnBack" runat="server"
                style="width: 120px;" onserverclick="btnBack_ServerClick">
                <span class="fa fa-remove"></span>
                <asp:Literal runat="server" Text="Annuler"></asp:Literal></button>
        </div>
    </div>
    <br />
    <br />
    <br />

</asp:Content>
