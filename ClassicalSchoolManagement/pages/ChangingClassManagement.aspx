<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangingClassManagement.aspx.cs"
    Inherits="ChangingClassManagement" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Changement de Classe
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
        function ConfirmDisable() {
            var confirm_value_disable = document.createElement("INPUT");
            confirm_value_disable.type = "hidden";
            confirm_value_disable.name = "confirm_value_disable";
            if (confirm("Voulez-vous vraiment desactiver cet eleve ?")) {
                confirm_value_disable.value = "Yes";
            } else {
                confirm_value_disable.value = "No";
            }
            document.forms[0].appendChild(confirm_value_disable);
        }

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Voulez-vous vraiment supprimer cet eleve ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListStudent");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListStudent");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlClassroom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlVacation" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>





    <div class="panel panel-info">
        <div class="panel-heading">
            <h4><span class="fa fa-list-ol"></span>&nbsp;Liste des élèves</h4>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCode" runat="server" Text="Classe" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                        Width="100%" Skin="Office2007" ExpandDirection="Down"
                        CausesValidation="false" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged">
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Vacation" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" />
                            <telerik:DropDownListItem Value="AM" Text="Matin" Selected="true" />
                            <telerik:DropDownListItem Value="PM" Text="Median" />
                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                            <telerik:DropDownListItem Value="WK" Text="Weekend" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" Text="Année Académique" CssClass="app-label-design"></asp:Label>
                    <telerik:RadDropDownList ID="ddlAcademicYear" runat="server" Width="100%" Skin="Office2007">
                    </telerik:RadDropDownList>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-sm btn-primary" id="Button1" runat="server"
                        onserverclick="btnSearch_Click" width="120px">
                        <span class="fa fa-search"></span>
                        <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                    &nbsp;

                    <button type="button" class="btn btn-sm btn-success" id="btnChange" runat="server"
                        onserverclick="btnChange_Click" width="120px">
                        <span class="fa fa-refresh"></span>
                        <asp:Literal runat="server" Text="Affecter nouvelle classe"></asp:Literal></button>
                    &nbsp;

                      <button type="button" class="btn btn-sm btn-default" id="btnExportExcel" runat="server"
                          onserverclick="btnExportExcel_ServerClick" width="120px">
                          <span class="fa fa-file-excel-o"></span>
                          <asp:Literal runat="server" Text="Exporter vers excel"></asp:Literal></button>
                </div>
            </div>
        </div>
    </div>



    <%-- <asp:Panel ID="pnlSearchStudent" runat="server" GroupingText="Changer de Classe" CssClass="panellDesign">
        <table border="0" style="width: 100%;">
            <tr class="trDesign">
                </td>
                <td colspan="1" style="width: 15%; text-align: center;"></td>
                <td colspan="1" style="width: 15%"></td>
            </tr>
        </table>
        <table border="0" style="margin-top: 10px; width: 100%;">
            <tr>
                <td colspan="1" style="width: 39%"></td>
                <td colspan="1" style="width: 10%">
                    <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                        Text="Rechercher" Width="100%" Skin="Web20">
                    </telerik:RadButton>
                </td>
                <td colspan="1" style="width: 2%"></td>
                <td colspan="1" style="width: 10%">
                    <telerik:RadButton ID="" OnClick="" runat="server" CausesValidation="true"
                        Text="Changer" Width="100%" Skin="Web20">
                    </telerik:RadButton>
                </td>
                <td style="text-align: right; width: 39%;">
                    <asp:LinkButton runat="server" Text="Exporter vers excel" Visible="false"
                        ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>--%>





        <div style="overflow: scroll; overflow-x: hidden; height: auto; width: 100%; border: 0px solid red;">
            <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
            <asp:GridView ID="gridListStudent" runat="server" AutoGenerateColumns="False"
                Style="overflow: auto;" CellPadding="2"
                ForeColor="Black" BorderWidth="1px"
                AllowPaging="true" Width="100%"
                OnRowCommand="gridListStudent_RowCommand"
                BackColor="White" BorderColor="Tan"
                GridLines="Both"
                OnRowDataBound="gridListStudent_RowDataBound">
                <RowStyle Height="10px" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                    </asp:TemplateField>
                    <%--  <asp:BoundField DataField="classroom" HeaderText="CLASSE">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="CLASSE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblClassroomName" Font-Bold="true"
                                Text='<%# Eval("classroom").ToString()%>'></asp:Label>

                            <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("classroom_id").ToString()%>' />
                        </ItemTemplate>
                        <HeaderStyle Width="180px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                    </asp:TemplateField>
                    <%--       <asp:BoundField DataField="vacation" HeaderText="VACATION">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="VACATION">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblVacationName" Font-Bold="true"
                                Text='<%# Eval("vacation").ToString()%>'></asp:Label>

                            <asp:HiddenField runat="server" ID="hiddenVacationCode" Value='<%# Eval("vacation_id").ToString()%>' />
                        </ItemTemplate>
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="CODE">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fullName" HeaderText="NOM">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="total_exam_point" HeaderText="NOTES TOTAL EXAMEN">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="total_student_point" HeaderText="NOTES TOTAL EVELE">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="student_average_percent" HeaderText="MOYENNE ELEVE">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="success_percent" HeaderText="MOYENNE DE PASSAGE">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="RESULTAT FINAL" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActualStatus" Font-Bold="true"
                                Text='<%# Convert.ToInt32(Eval("final_result_status").ToString()) == 1 ? "Succès" : "Echec" %>'
                                ForeColor='<%# Convert.ToInt32(Eval("final_result_status").ToString()) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>

                            <asp:HiddenField runat="server" ID="hiddenResultStatus" Value='<%# Eval("final_result_status").ToString()%>' />
                            <asp:HiddenField runat="server" ID="hiddenAcademicYear" Value='<%# Eval("academic_year").ToString()%>' />
                        </ItemTemplate>
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkStudent" Checked='<%# Convert.ToInt32(Eval("final_result_status").ToString()) == 1 ? true : false %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="25px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                <HeaderStyle Font-Bold="True" Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign"
                    ForeColor="WhiteSmoke" VerticalAlign="Middle" Font-Size="Smaller" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                    HorizontalAlign="Center" />
                <RowStyle Height="5px" Font-Size="Smaller" BorderWidth="0.3" />
                <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                    BorderStyle="None" />
                <SortedAscendingCellStyle BackColor="SkyBlue" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
        </div>


</asp:Content>
