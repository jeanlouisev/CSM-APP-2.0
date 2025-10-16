<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixCoursePrice.aspx.cs"
    Inherits="FixCoursePrice" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Fixer Prix Cours
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


    <%--<style type="text/css">
    .FixedHeader {
        position: absolute;
        font-weight: bold;
        overflow: hidden;
        z-index: 10;
        top: expression(<%= gridListCourse.HeaderRow %>.offsetParent.scrollTop-2);
    }
</style>--%>

    <script type="text/javascript">
        function allowOnlyNumber(sender, args) {
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
            var gv = document.getElementById("gridListCourse");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListCourse");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
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









      
    </div>



  <%--  <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des prix" Visible="false" CssClass="panellDesign">
        <div style="width: 100%">
            <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
            <asp:GridView ID="gridListCourse" runat="server" AutoGenerateColumns="False"
                Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                BorderWidth="1px" AllowPaging="false" Width="100%"
                OnRowCommand="gridListCourse_RowCommand"
                BackColor="White" BorderColor="Tan"
                GridLines="Both" OnRowDataBound="gridListCourse_RowDataBound"
                OnRowDeleting="gridListCourse_RowDeleting" DataKeyNames="id"
                OnPageIndexChanging="gridListCourse_PageIndexChanging1">
                <RowStyle Height="10px" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="price" HeaderText="PRIX / HEURE (HTG)">
                        <HeaderStyle Width="350px" HorizontalAlign="Right" />
                        <ItemStyle Width="350px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="update_time" HeaderText="DATE ET HEURE">
                        <HeaderStyle Width="400px" HorizontalAlign="Center" />
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%--   <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="btnDelete"
                                OnClientClick="Confirm()" OnClick="removePrice" ToolTip="Supprimer"
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />--
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Left" Width="20px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                <HeaderStyle Height="22px" Width="960px" CssClass="gridHeaderDesign" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                    HorizontalAlign="Center" />
                <RowStyle Height="5px" Font-Size="Smaller" />
                <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                    BorderStyle="None" />
                <SortedAscendingCellStyle BackColor="SkyBlue" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
        </div>
        <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
    </asp:Panel>--%>

</asp:Content>

