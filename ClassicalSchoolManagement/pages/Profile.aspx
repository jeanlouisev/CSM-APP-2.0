<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" EnableEventValidation="false"
    Inherits="Profile" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Profile
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
        .col {
            word-wrap: normal;
            //text-transform:lowercase;
        }

        .divPhoto {
            height: 250px;
            width: 250px;
            float: left;
        }

        .divInfo {
            height: 250px;
            width: 600px;
            float: left;
            margin-left: 10px;
        }

        .divWrapper {
            width: 900px;
            height: 400px;
        }

        .Style1 {
            text-align: left;
            text-transform: uppercase;
            background-color: #3678A3;
            color: white;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: small;
            padding-left: 10px;
        }

        .Style2 {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            text-transform: uppercase;
            background-color: white;
            padding-left: 10px;
            font-size: small;
        }

        .StyleEmail {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            text-transform: lowercase;
            background-color: white;
            padding-left: 10px;
            font-size: small;
        }
    </style>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <asp:Panel ID="pnluser" runat="server" GroupingText="Profil de l'utilisateur" CssClass="panellDesign">
        <div class="divWrapper">
            <div class="divPhoto">
                <asp:Image runat="server" ID="imgUser" Width="100%" Height="100%" />
            </div>

            <div class="divInfo">
                <table style="width: 100%; margin: auto;">
                    <tr class="trDesign">
                        <td colspan="1" style="width: 30%;" class="Style1">Nom & Prénom : 
                        </td>
                        <td colspan="1" style="width: 70%;" class="Style2">
                            <asp:Label runat="server" ID="lblFullname"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Position : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblPosition"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Sexe : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblSex"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Date de naissance : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                        </td>
                    </tr>
                    <%--  <tr class="trDesign">
                    <td colspan="1" class="Style1">Age : 
                    </td>
                    <td colspan="1" class="Style2">
                        <asp:Label runat="server" ID="lblAge"></asp:Label>
                    </td>
                </tr>--%>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Lieu de naissance : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblBirthPlace"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Téléphone : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblPHone"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Email : 
                        </td>
                        <td colspan="1" class="StyleEmail">
                            <asp:Label runat="server" ID="lblEmail"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1" class="Style1">Adresse : 
                        </td>
                        <td colspan="1" class="Style2">
                            <asp:Label runat="server" ID="lblAddress"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
