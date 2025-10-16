<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <%--<link rel="icon" href="../images/graduation_cap_icon.png">--%>
    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="../akkhor/img/favicon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


    <meta charset="UTF-8">
    <title>CSM Login</title>
    <%-- <link rel="stylesheet" href="../css/reset.css">
    <link rel='stylesheet prefetch' href='../css/css.css'>
    <link rel='stylesheet prefetch' href='../css/css1.css'>
    <link rel='stylesheet prefetch' href="../css/font-awesome.min.css">
    <link rel="stylesheet" href="../css/style.css">--%>

    <style type="text/css">
        body {
            width: 100%;
            height: 100%;
            background: url(../images/school_bg5.jpg) repeat fixed;
            font-family: 'Open Sans', sans-serif;
            font-weight: 200;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .form {
            margin-top: 30% !important;
            position: relative;
        }

        .imgCenter {
            position: relative;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -10%);
        }
    </style>

</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4 col-xs-6 col-xs-offset-3">
                    <div class="form">
                        <asp:ScriptManager runat="server"></asp:ScriptManager>
                        <div class="col-xs-12">
                            <%--<img src="../images/logos/csm_logo.png" class="img-responsive img-circle imgCenter" />--%>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon bg-info">
                                    <i class="glyphicon glyphicon-user"></i>
                                </span>
                                <input runat="server" id="txtUserName" type="text" class="form-control" placeholder="Username" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon bg-info">
                                    <i class="glyphicon glyphicon-lock"></i>
                                </span>
                                <input runat="server" id="txtPassword" type="password" class="form-control" placeholder="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <button id="btnSubmit" runat="server"
                                onserverclick="btnLogin_Click"
                                class="btn btn-primary col-xs-12">
                                <span class="glyphicon glyphicon-log-in"></span>&nbsp;
                                Login
                            </button>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div visible="false" runat="server" id="divErrorAlert" class="alert alert-danger">
                        <a id="linkClose" class="close" data-dismiss="divErrorAlert">&times;</a>
                        <asp:Label runat="server" ID="lblError" Font-Size="Small" Font-Bold="true"></asp:Label>
                    </div>

                </div>
            </div>
        </div>
    </form>



    <script type="text/javascript">
        $('#linkClose').click(function () {
            $('#divErrorAlert').hide('fade');
        })
    </script>
</body>

<script src="../js/jquery.min.js"></script>
<script src="../js/index.js"></script>
</html>
