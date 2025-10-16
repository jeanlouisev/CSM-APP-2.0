<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExams.aspx.cs"
     Inherits="AddExams" MasterPageFile="~/master/Master3.Master"%>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
   Add Exam
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

    
<script type="text/javascript">
    function keyPress(sender, args) {
        var text = sender.get_value() + args.get_keyCharacter();
        if (!text.match('^[0-9]+$'))
            args.set_cancel(true);
    }

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


    function onMouseOver(rowIndex) {
        var gv = document.getElementById("gridListExam");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#c8e4b6";
    }

    function onMouseOut(rowIndex) {
        var gv = document.getElementById("gridListExam");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#fff";
    }
</script>

<style>
    .tdDesign1 {
        width: 80px;
    }

    .tdDesign2 {
        width: 80px;
    }

    .tdDesign3 {
        width: 1px;
    }

    .alignRight {
        text-align: right;
    }
</style>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ddlClassroom">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlVacation" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlVacation">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYearStart">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlAcademicYearEnd" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYearEnd">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlAcademicYearStart" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlCourse">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlTeacher">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="radStartHour">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="radEndHour" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtDuration" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="radEndHour">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="radStartHour" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtDuration" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<asp:Panel ID="pnlAddExams" runat="server" GroupingText="Ajouter Examens" CssClass="panellDesign">
    <table id="tlbMainForm" border="0" style="width: 100%">
        <tr>
            <td colspan="1" style="width: 10%">
                <asp:Label ID="lblClassroom" runat="server" Text="Classe" ></asp:Label>
            </td>
            <td colspan="1" style="width: 23%">
                <telerik:RadDropDownList ID="ddlClassroom" Skin="Bootstrap"
                    OnSelectedIndexChanged="ddlClassroom_OnSelectedIndexChanged"
                    CausesValidation="false" AutoPostBack="true" runat="server"
                    Width="100%" DropDownWidth="300px" DropDownHeight="200px">
                </telerik:RadDropDownList>
            </td>
            <td colspan="1" style="width: 2%"></td>
            <td colspan="1" style="width: 10%">
                <asp:Label ID="lblVacation" runat="server" Text="Vacation" ></asp:Label>
            </td>
            <td colspan="1" style="width: 23%">
                <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Bootstrap"
                    CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ddlVacation_SelectedIndexChanged">
                    <Items>
                        <telerik:DropDownListItem Value="-1" Text="" />
                        <telerik:DropDownListItem Value="AM" Text="Matin" />
                        <telerik:DropDownListItem Value="PM" Text="Median" />
                        <telerik:DropDownListItem Value="NG" Text="Soir" />
                        <telerik:DropDownListItem Value="WK" Text="Weekend" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
            <td colspan="1" style="width: 2%"></td>
            <td colspan="1" style="width: 10%">
                <asp:Label ID="Label2" runat="server" Text="Control" ></asp:Label>
            </td>
            <td colspan="1" style="width: 23%">
                <telerik:RadDropDownList ID="ddlControl" runat="server" Width="100%" Skin="Bootstrap">
                    <Items>
                        <telerik:DropDownListItem Value="1" Text="1er" />
                        <telerik:DropDownListItem Value="2" Text="2ieme" />
                        <telerik:DropDownListItem Value="3" Text="3ieme" />
                        <telerik:DropDownListItem Value="4" Text="4ieme" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCourse" runat="server" Text="Matière" ></asp:Label>
            </td>
            <td>
                <telerik:RadDropDownList ID="ddlCourse" runat="server" Width="100%"
                    OnSelectedIndexChanged="ddlCourse_OnSelectedIndexChanged" CausesValidation="false"
                    AutoPostBack="true" Skin="Bootstrap">
                    <Items>
                        <telerik:DropDownListItem Value="-1" Text="" Selected="true" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
            <td></td>
            <td colspan="1">
                <asp:Label ID="lblTeacher" runat="server" Text="Professeur" ></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadDropDownList ID="ddlTeacher" runat="server" Width="100%" Skin="Bootstrap" DropDownWidth="300px">
                </telerik:RadDropDownList>


                <%--OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged" CausesValidation="false"
                    AutoPostBack="true"--%>
            </td>
            <td></td>
            <td colspan="1">
                <asp:Label ID="Label3" runat="server" Text="Coefficient" ></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadNumericTextBox runat="server" ID="txtPoints" Width="100%" Skin="Bootstrap"
                    EmptyMessage="0" MaxValue="1000" CssClass="alignRight" MaxLength="3">
                    <NumberFormat DecimalDigits="0" DecimalSeparator=" " />
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="1">
                <asp:Label ID="lblExamDate" runat="server" Text="Date" ></asp:Label>
            </td>
            <td colspan="1">
                <telerik:RadDatePicker runat="server" ID="radExamDate" Width="100%" Skin="Bootstrap">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                </telerik:RadDatePicker>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblStartHour" runat="server" Text="Heure debut" ></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker runat="server" ID="radStartHour" Width="100%" AutoPostBack="true"
                    DateInput-ReadOnly="false" DateInput-CausesValidation="false" Skin="Bootstrap"
                    OnSelectedDateChanged="radStartHour_SelectedDateChanged">
                </telerik:RadTimePicker>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblEndHour" runat="server" Text="Heure fin" ></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker runat="server" ID="radEndHour"
                    Width="100%" DateInput-ReadOnly="false" Skin="Bootstrap"
                    DateInput-CausesValidation="false" AutoPostBack="true"
                    OnSelectedDateChanged="radEndHour_SelectedDateChanged">
                </telerik:RadTimePicker>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblprof" runat="server" Text="Duree" ></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="txtDuration" runat="server" Width="100%" ForeColor="Maroon"
                    ReadOnly="true" Font-Italic="true" Font-Size="Small" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td></td>
            <td>
                <asp:Label ID="lblDescription" runat="server" Text="Description" ></asp:Label></td>
            <td colspan="4">
                <telerik:RadTextBox ID="txtDescription"
                    runat="server" Width="100%" Skin="Bootstrap"
                    EmptyMessage="Type your description ..."
                    MaxLength="200" TextMode="MultiLine" Height="40px">
                </telerik:RadTextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Année Académique" ></asp:Label>
            </td>
            <td>
                <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="100%"
                    CausesValidation="false" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>
            <td></td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Attacher Examen" ></asp:Label>
            </td>
            <td>
                <asp:FileUpload runat="server" ID="examUploader" Width="200px" />
            </td>
        </tr>
    </table>
</asp:Panel>

<table border="0" style="margin-top: 10px; width: 100%;">
    <tr>
        <td colspan="1" style="width: 30%"></td>
        <td colspan="1" style="width: 15%; text-align: center;">
            <telerik:RadButton ID="btnSearch" runat="server"
                Text="Rechercher" Width="90%"
                Skin="Glow" OnClick="btnSearch_Click">
            </telerik:RadButton>
        </td>
        <td colspan="1" style="width: 15%; text-align: center;">
            <telerik:RadButton ID="btnAddExams" runat="server"
                Text="Ajouter" Width="90%"
                Skin="Glow" OnClick="btnAddExams_Click">
            </telerik:RadButton>
        </td>
     <%--   <td colspan="1" style="width: 15%; text-align: center;">
            <telerik:RadButton ID="btnClear" runat="server"
                Text="Annuler" Width="90%" Skin="Glow"
                OnClick="btnClear_Click">
            </telerik:RadButton>
        </td>--%>
        <td colspan="1" style="width: 40%; text-align: right;">
            <asp:LinkButton runat="server" Text="Exporter vers excel" Visible="false"
                ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
        </td>
    </tr>
</table>

<asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des Examens" Visible="false" CssClass="panellDesign">
    <div style="width: 100%; overflow:scroll">
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListExam" runat="server" AutoGenerateColumns="False"
            Style="overflow: auto;" CellPadding="2" ForeColor="Black" DataKeyNames="id"
            BorderWidth="1px" AllowPaging="false" Width="2500px"
            OnRowCommand="gridListExam_RowCommand"
            BackColor="White"
            BorderColor="Tan" GridLines="Both"
            OnRowDataBound="gridListExam_RowDataBound">
            <RowStyle Height="10px" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/search_icon1.png" ID="btnDownloadFile"
                            CommandName="viewPdf" ToolTip="Visualiser fichier examen"
                            Visible='<%# (Eval("file_path").ToString().Length <= 0 ? false : true) %>'
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="btnDelete"
                            OnClientClick="Confirm()" OnClick="removeExam" ToolTip="Supprimer"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="class_name" HeaderText="CLASSE">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="vacation" HeaderText="VACATION">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="control_name" HeaderText="CONTROL">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="cours_name" HeaderText="MATIERE">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="teacher_name" HeaderText="PROFESSEUR">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="points" HeaderText="COEFFICIENT">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="exam_date" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="start_hour" HeaderText="HEURE D.">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="end_hour" HeaderText="HEURE F.">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="description" HeaderText="DESCRIPTION">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="academic_year_description" HeaderText="ANNEE ACADEMIQUE">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="file_name" HeaderText="NOM FICHIER">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" Wrap="true" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
            <HeaderStyle Height="22px" Width="960px" CssClass="gridHeaderDesign" HorizontalAlign="Center" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" HorizontalAlign="Center" />
            <RowStyle Height="5px" Font-Size="Smaller" />
            <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver" BorderStyle="None" />
            <SortedAscendingCellStyle BackColor="SkyBlue" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
    </div>
    <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small"
        Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
</asp:Panel>

</asp:Content>
