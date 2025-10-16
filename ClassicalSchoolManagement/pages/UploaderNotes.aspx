<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploaderNotes.aspx.cs"
    Inherits="UploaderNotes" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Upload Notes
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
        function validate() {
            var InputFile = document.getElementById('<%= NoteUploader.ClientID %>');
            var UploadPath = InputFile.value;
            if (UploadPath == '') {
                alert("Please choose file to import !");
                InputFile.focus();
                InputFile.style.background = "Yellow";
                return false;
            }
            var Extension = UploadPath.substring(UploadPath.lastIndexOf('.') + 1).toLowerCase();
            if (Extension != "xls" && Extension != "xlsx") {
                alert("Only Accept File Excell !");
                return false;
            }
        }
    </script>
</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<asp:Panel runat="server" ID="pnlMaster2">
    <div style="width: 100%; border: 0px solid red; height: auto; text-align: center;">
        <asp:Label runat="server" ID="lblResult" Visible="false"></asp:Label>
    </div>
    <asp:Panel runat="server" ID="pnlPart2" GroupingText="Upload Palmarès" BorderColor="WhiteSmoke" CssClass="panellDesign">
        <hr style="width: 100%;" />
        <div class="divPart2">
            <table border="0" align="center" width="40%">
                <tr>
                    <td colspan="1" width="30%" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Attacher Fichier :"></asp:Label>
                    </td>
                    <td colspan="1" width="30%">
                        <asp:FileUpload runat="server" ID="NoteUploader" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="30%" align="center">
                        <br />
                        <asp:LinkButton Visible="false" ID="lnkDownLoad" runat="server"
                            Text="Telecharger model de fichier note" OnClick="lnkDownLoad_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <table border="0" align="center" style="margin-top: 10px;">
        <tr>
            <td>
                <telerik:RadButton ID="btnUpload" runat="server" Text="Charger"
                    OnClick="btnUploadExam_Click" Width="120px" Skin="Glow"
                    OnClientClicking="return validate()" Visible="false">
                </telerik:RadButton>
                <asp:Button ID="btnUploadExam" runat="server" Text="Charger" OnClick="btnUploadExam_Click"
                    OnClientClick="return validate()" Width="120px" />
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
