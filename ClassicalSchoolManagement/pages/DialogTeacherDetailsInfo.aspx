<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogTeacherDetailsInfo.aspx.cs" Inherits="DialogTeacherDetailsInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information</title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
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
    <link rel="stylesheet" href="../plugins/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">
    <!-- Select -->
    <link rel="stylesheet" href="../plugins/select2/select2.css">


    <style type="text/css">
        body {
            background-color: whitesmoke;
        }

        .divPhotoContainer {
            float: left;
            width: 150px;
            height: 150px;
        }

        .divFullName {
            float: left;
            margin-left: 20px;
            margin-bottom: 0px;
            width: 670px;
            height: 30px;
            margin-top: 0px;
            border: 0px solid red;
        }

        .divInformation {
            float: left;
            margin-left: 20px;
            width: 790px;
            height: auto;
            border: 0px solid blue;
        }

        .labelDesign {
            color: maroon;
            font-size: 15px;
            font-weight: bold;
        }

        .infoDesign {
            font-size: 15px;
            color: black;
        }

        .fullNameDesign {
            font-size: 20px;
            font-weight: bold;
        }

        .masterPanelDesign {
            border: 0px solid red;
        }

        .Style1 {
            text-align: left;
            /*text-transform: s;*/
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

        /*#imageKeeper{
            border-radius: 45%;
        }

        #imageKeeper2{
            border-radius: 45%;
        }*/

        #tblInfoStaff {
            border-collapse: collapse;
        }

        #tblReference {
            border-collapse: collapse;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <div style="margin: auto; width: 100%; border: 0px solid red;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4><span class="fa fa-info-circle"></span>&nbsp;Information Personnelle</h4>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="divPhotoContainer">
                                <asp:Image runat="server" ID="imageKeeper" Width="100%" Height="100%" />
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <table border="0" style="width: 100%;" runat="server" id="tblInfoStaff">
                                <tr class="trDesignMedium">
                                    <td style="width: 17%;" colspan="1" class="Style1">Nom :
                                    </td>
                                    <td style="width: 33%;" class="Style2">
                                        <asp:Label runat="server" ID="lblFirstName">
                                        </asp:Label>
                                    </td>
                                    <td style="width: 1%;"></td>
                                    <td style="width: 19%;" class="Style1">Prénom :
                                    </td>
                                    <td style="width: 30%;" class="Style2">
                                        <asp:Label runat="server" ID="lblLastName">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Code :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblCode" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">État Civil :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblMaritalStatus">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Sexe :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblSex">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">Statut :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblStatus">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">E-mail  :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblEmail">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">Téléphone :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblPhone">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Adresse :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblAdresse">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">Date de naissance :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblBirthDate">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">NIF / NIU :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblIdCard">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">Lieu de naissance :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblBirthPlace">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Niveau :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblLevel">
                                        </asp:Label>
                                    </td>
                                    <td></td>
                                    <td class="Style1">
                                    </td>
                                    <td class="Style2">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4><span class="fa fa-phone-square"></span>&nbsp;Personne à contacter en cas d'urgence</h4>
                </div>
                <div class="panel-body" runat="server">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="divPhotoContainer">
                                <asp:Image runat="server" ID="imageKeeperCont" ImageUrl="~/images/image_data/Default.png" Width="100%" Height="100%" />
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <table border="0" style="width: 100%;" runat="server" id="Table1">
                                <tr class="trDesignMedium">
                                    <td style="width: 15%;" colspan="1" class="Style1">Nom :
                                    </td>
                                    <td style="width: 35%;" class="Style2">
                                        <asp:Label runat="server" ID="lblFirstNameCont">
                                        </asp:Label>
                                    </td> 
                                    <td style="width: 1%;"></td>
                                    <td style="width: 19%;">
                                    </td>
                                    <td style="width: 30%;">                                      
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Prénom :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblLastNameCont">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Sexe :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblSexCont">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Téléphone  :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblPhoneCont" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Addresse :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblAdressCont">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr class="trDesignMedium">
                                    <td class="Style1">Lien de parenté  :
                                    </td>
                                    <td class="Style2">
                                        <asp:Label runat="server" ID="lblRelationshipCont">
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

