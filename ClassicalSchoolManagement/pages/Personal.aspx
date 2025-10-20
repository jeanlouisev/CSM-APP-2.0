<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" 
    Inherits="Personal" MasterPageFile="~/master/Master5.Master" %>

<!--<%@ Register TagPrefix="art" TagName="banner" Src="~/design/Banner.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/design/Menu.ascx" %>
<%@ Register TagPrefix="art" TagName="PersonalMenu" Src="~/design/Menus/PersonalMenu.ascx" %>-->
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="bannerContent" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <!--<art:banner ID="bannerContainer" runat="server" />-->
    
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

<!--<asp:Content ID="MenuContentTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
  Personal
</asp:Content>

<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="PersonalMenuContainer" ContentPlaceHolderID="NavigationPaneContentPlaceHolder" runat="Server">
    <art:PersonalMenu ID="PersonalMenuContent" runat="server" />
</asp:Content>-->