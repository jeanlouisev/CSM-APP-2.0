<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPersonal.aspx.cs"
    Inherits="AddPersonal" MasterPageFile="~/master/Master5.Master" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>







<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
<%--    <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
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

        .asterix {
            color: gray;
            font-weight: bold;
            font-size: medium;
        }

        .amountDesign {
            text-align: right;
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
            <telerik:AjaxSetting AjaxControlID="txtCardId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCardId" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRefIdCard">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRefIdCard" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtPhone1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPhone1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindowSearchStudent" runat="server" Modal="True" OnClientClose="ClientCloseSearchStudent" Skin="Bootstrap">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

        <!-- Breadcubs Area Start Here -->
    <div class="breadcrumbs-area">
   
        <ul>
            <li>
                <a href="#">Personnel</a>
            </li>
            <li>Nouveau Personnel</li>
        </ul>
    </div>
    <!-- Breadcubs Area End Here -->






  <%--  <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-info-circle"></span>&nbsp;Information du personnel
                 <span class="pull-right">
                     <asp:Label runat="server" ID="lblStaffCode" Font-Bold="true" ForeColor="Red"></asp:Label></span>
            </h4>
        </div>
        <div class="panel-body">--%>

    <div class="card height-auto">
    <div class="card-body">
        <div class="heading-layout1">
            <div class="item-title">
                <h3><span class="fa fa-info-circle"></span>&nbsp;Ajouter Nouveau Personnel</h3>
            </div>
        </div>


            <div class="row">

                <div class="col-md-10">
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblFirstName" runat="server" Text="Nom" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblLastName" runat="server" Text="Prénom" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
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
                        <div class="col-sm-4">
                            <asp:Label ID="lblBirthPlace" runat="server" Text="Lieu de naissance" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtBirthPlace" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblBirthDate" runat="server" Text="Date de naissance" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDatePicker ID="radBirthDate" runat="server" Width="100%"
                                EnableTyping="true" Skin="Bootstrap" MinDate="1800-01-01"
                                OnSelectedDateChanged="radBirthDate_SelectedDateChanged">
                                <DateInput CausesValidation="false" AutoPostBack="true" runat="server"
                                    DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblPhone1" runat="server" Text="Téléphone" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadNumericTextBox ID="txtPhone1" runat="server"
                                Font-Size="Small" MaxLength="8" Width="100%" Skin="Bootstrap"
                                ForeColor="Black" Type="Number"
                                EmptyMessageStyle-Font-Italic="true">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblAddress" runat="server" Text="Adresse" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="CIN/NIF" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtCardId" runat="server" Width="100%" Skin="Bootstrap"
                                MaxLength="10"
                                EmptyMessageStyle-Font-Italic="true">
                                <ClientEvents OnKeyPress="keyPress" />
                            </telerik:RadTextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblemail" runat="server" Text="E-mail" CssClass="app-label-design"></asp:Label>
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="100%"
                                Skin="Bootstrap" EmptyMessageStyle-Font-Italic="true" CssClass="lower">
                            </telerik:RadTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Label ID="lblMaritalStatus" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlMaritalStatus" runat="server" Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                    <telerik:DropDownListItem Value="Célibataire" Text="Célibataire" />
                                    <telerik:DropDownListItem Value="Marié(e)" Text="Marié(e)" />
                                    <telerik:DropDownListItem Value="Divorcé(e)" Text="Divorcé(e)" />
                                    <telerik:DropDownListItem Value="Veuf(ve)" Text="Veuf(ve)" />
                                    <telerik:DropDownListItem Value="Union libre" Text="Union libre" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="RadLabel3" runat="server" Text="Niveau d’étude" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlStudyLevel" Width="100%" runat="server" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
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
                            <asp:Label ID="Label5" runat="server" Text="Poste occupé" CssClass="app-label-design"></asp:Label>
                            <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlPosition" runat="server" Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </div>
                    </div>
                </div>
          
                
                <div class="col-md-2">z
                    <div class="col-md-12 text-center">
                        <img runat="server" id="imgStaff" src="../images/image_data/Default.png"
                            class="img-bordered-sm" style="width: 120px; height: 120px" />

                        <div style="margin: auto; width: 120px; text-align: center">
                            <asp:FileUpload runat="server" ID="imageUploader" Width="90" />

                            <asp:ImageButton ID=" " OnClick="btnUploadImage_Click"
                                runat="server" ImageUrl="~/images/uploadButton1.png"
                                Width="0px" Height="0px" CssClass="hideUploadButton" />
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
                <div class="col-sm-5">
                    <asp:Label ID="Label7" runat="server" Text="Description" CssClass="app-label-design">
                    </asp:Label>
                    <telerik:RadDropDownList ID="ddlDocumentType" Width="100%" runat="server" Skin="Bootstrap">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Label6" runat="server" Text="Pièces Jointes" CssClass="app-label-design">
                    </asp:Label>
                    <asp:FileUpload ID="documentsAttachFile" runat="server" Width="100%" />
                </div>
                <div class="col-sm-3">

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



      <div class="card height-auto">
        <div class="card-body">
            <div class="heading-layout1">
                <div class="item-title">
                    <h3><span class="fa fa-users"></span>&nbsp;Personne à contacter en cas d'urgence</h3>
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
                    <asp:Label ID="Label2" runat="server" Text="Prénom" CssClass="app-label-design"></asp:Label>
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
                            <telerik:DropDownListItem Value="Masculin" Text="Masculin" />
                            <telerik:DropDownListItem Value="Féminin" Text="Féminin" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label3" runat="server" Text="Lieu de naissance" CssClass="app-label-design"></asp:Label>
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
                    <asp:Label ID="Label10" runat="server" Text="Profession" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentOccupation" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label11" runat="server" Text="État Civil" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                            <telerik:RadDropDownList ID="ddlParentMaritalStatus" runat="server" Skin="Bootstrap" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="-- Sélectionner --" Selected="true" />
                                    <telerik:DropDownListItem Value="Célibataire" Text="Célibataire" />
                                    <telerik:DropDownListItem Value="Marié(e)" Text="Marié(e)" />
                                    <telerik:DropDownListItem Value="Divorcé(e)" Text="Divorcé(e)" />
                                    <telerik:DropDownListItem Value="Veuf(ve)" Text="Veuf(ve)" />
                                    <telerik:DropDownListItem Value="Union libre" Text="Union libre" />
                                </Items>
                            </telerik:RadDropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="Label12" runat="server" Text="NIF" CssClass="app-label-design"></asp:Label>
                    <span class="asterix">*</span>
                    <telerik:RadTextBox ID="txtParentIdCard" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </div>

            </div>
            <div class="row">

                <div class="col-md-3">
                    <asp:Label ID="Label13" runat="server" Text="Poste Occupe" CssClass="app-label-design"></asp:Label>
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
                    <asp:Label ID="Label14" runat="server" Text="E-mail" CssClass="app-label-design"></asp:Label>
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
                    <asp:Label ID="Label15" runat="server" Text="Lien de parenté" CssClass="app-label-design"></asp:Label>
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




            </div>
        </div>
    </div>





  <div class="row">
        <div class="col-12 form-group mg-t-8">
    <button type="button" class="btn-fill-lg btn-gradient-yellow btn-hover-bluedark" id="btnSave" runat="server"
        onserverclick="btnSave_Click">
        Sauvegarder
    </button>

    <button type="button" class="btn-fill-lg bg-blue-dark btn-hover-yellow" id="btnBack" runat="server"
        onserverclick="btnBack_ServerClick">
        Annuler</button>
</div>
    
    </div>
</asp:Content>
