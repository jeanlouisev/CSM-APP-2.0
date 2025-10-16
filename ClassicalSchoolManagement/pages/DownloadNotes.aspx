<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadNotes.aspx.cs"
    Inherits="DownloadNotes" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Download Palmares
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


        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListNotes");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListNotes");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>

    <style type="text/css">
        .style80 {
            width: 80px;
        }


        .auto-style2 {
            width: 194px;
        }

        tr.marginUnder > td {
            padding-bottom: 3px;
        }
    </style>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYearStart">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlClassroom">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlCourse">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlTeacher">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlVacation">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlPeriod">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblMoreInformation"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblError"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
            Skin="Office2007">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>


<asp:Panel ID="pnlAddExams" runat="server" GroupingText="Download Palmarès" CssClass="panellDesign">
    <div style="text-align: center; width: 100%;">
        <asp:Label runat="server" ID="lblError" Visible="false" ForeColor="Red"></asp:Label>
    </div>
    <div>
        <table runat="server" border="0" align="left" width="100%" id="tblMoreInformation">
            <tr class="trDesignMedium">
                <td colspan="1" style="width: 15%">
                    <asp:Label ID="lblClassroom" runat="server" Text="Classe"></asp:Label>
                </td>
                <td style="width: 35%">
                    <telerik:RadDropDownList ID="ddlClassroom" Width="90%" Skin="Office2007"
                        OnSelectedIndexChanged="ddlClassroom_OnSelectedIndexChanged"
                        CausesValidation="false" DropDownHeight="250px" DropDownWidth="300px    "
                        AutoPostBack="true" runat="server">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td style="width: 15%">
                    <asp:Label ID="lblVacation" align="center" runat="server" Text="Vacation"></asp:Label>
                </td>
                <td colspan="1" style="width: 35%">
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%"
                        CausesValidation="false" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="ddlVacation_SelectedIndexChanged">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
            </tr>
            <tr class="trDesignMedium">
                <td colspan="1">
                    <asp:Label ID="lblCourse" runat="server" Text="Matiere"></asp:Label>
                </td>
                <td colspan="1">
                    <telerik:RadDropDownList ID="ddlCourse" runat="server" DropDownHeight="300px"
                        OnSelectedIndexChanged="ddlCourse_OnSelectedIndexChanged" Skin="Office2007"
                        CausesValidation="false" AutoPostBack="true" Width="90%">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td colspan="1">
                    <asp:Label ID="lblTeacher" runat="server" Text="Professeur"></asp:Label>
                </td>
                <td colspan="1">
                    <telerik:RadDropDownList ID="ddlTeacher" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--"
                                Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
            </tr>
            <tr class="trDesignMedium">
                <td colspan="1">
                    <asp:Label ID="Label1" align="center" runat="server" Text="Année Académique"></asp:Label>
                </td>
                <td colspan="1">
                    <telerik:RadDropDownList ID="ddlAcademicYear" Width="90%" Skin="Office2007" runat="server"
                        OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" CausesValidation="false"
                        AutoPostBack="true">
                    </telerik:RadDropDownList>
                </td>
                <td colspan="1">
                    <asp:Label ID="Label2" align="center" runat="server" Text="Controle"></asp:Label>

                </td>
                <td colspan="1">
                    <telerik:RadDropDownList ID="ddlPeriod" Width="100%" runat="server" Skin="Office2007"
                        OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged1" CausesValidation="false"
                        AutoPostBack="true">
                        <Items>
                            <telerik:DropDownListItem Value="1" Text="1ere" Selected="true" />
                            <telerik:DropDownListItem Value="2" Text="2ieme" />
                            <telerik:DropDownListItem Value="3" Text="3ieme" />
                            <telerik:DropDownListItem Value="4" Text="4ieme" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>

<table border="0" style="width: 100%; margin-bottom: 20px;">
    <tr class="trDesign">
        <td style="width: 45%"></td>
        <td style="width: 10%">
            <telerik:RadButton ID="btnSearch" runat="server" Text="Rechercher"
                Width="100%" Skin="Glow" OnClick="btnSearch_Click">
            </telerik:RadButton>
        </td>
        <td style="width: 45%; text-align: right;">
            <asp:LinkButton runat="server" Font-Size="Smaller" ID="lblExport" Font-Bold="true"
                Text="Exporter Fichier Excel" Visible="false" ForeColor="Blue" OnClick="lnkExport_Click">
            </asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Panel runat="server" ID="pnlResult" CssClass="panellDesign">
    <div style="width: 100%; overflow: scroll;">
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListNotes" runat="server"
            AutoGenerateColumns="False" DataKeyNames="id"
            Style="overflow: auto;" CellPadding="2"
            ForeColor="Black" BorderWidth="1px"
            AllowPaging="false" Width="2000px"
            OnRowCommand="gridListNotes_RowCommand"
            BackColor="White" BorderColor="Tan"
            GridLines="Both" OnRowDataBound="gridListNotes_RowDataBound">
            <RowStyle Height="10px" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </asp:TemplateField>
                <asp:BoundField DataField="classroom_id" HeaderText="CODE CLASSE">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="left" Width="80px" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="classroom_fullname" HeaderText="CLASSE">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="left" Width="160px" />
                </asp:BoundField>
                <asp:BoundField DataField="vacation" HeaderText="VACATION">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="id_teacher" HeaderText="CODE PROFESSEUR">
                    <HeaderStyle Width="130px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="130px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="teacher_fullname" HeaderText="PROFESSEUR">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="student_fullname" HeaderText="NOM ELEVE">
                    <HeaderStyle Width="180px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="cours_fullname" HeaderText="MATIERE">
                    <HeaderStyle Width="70px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="70px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="exam_points" HeaderText="COEFFICIENT">
                    <HeaderStyle Width="60px" HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="student_points" HeaderText="NOTE ELEVE">
                    <HeaderStyle Width="60px" HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="control" HeaderText="CONTROL">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="academic_year" HeaderText="ANNEE ACADEMIQUE">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="exam_code" HeaderText="CODE EXAMEN">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="70px" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="student_code" HeaderText="CODE ELEVE">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" Wrap="true" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
            <HeaderStyle Height="22px" Width="940px" CssClass="gridHeaderDesign" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                HorizontalAlign="Center" />
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


