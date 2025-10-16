<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultHeader.ascx.cs" Inherits="DefaultHeader" %>


<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
<!-- Font Awesome -->
<link rel="stylesheet" href="../bootstrap/css/font-awesome.min.css">
<!-- Ionicons -->
<link rel="stylesheet" href="../bootstrap/css/ionicons.min.css">
<!-- jvectormap -->
<link rel="stylesheet" href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css">
<!-- Theme style -->
<link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
<!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
<link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css">
<link rel="stylesheet" href="../plugins/flag-icon-css-master/css/flag-icon.css">

<style>
    .sidebar-toggle {
        background-color: red;
        color: black;
    }
</style>


<a href="../Pages/homepage.aspx" class="logo" style="color: white; background-color: #337ab7">
    <!-- mini logo for sidebar mini 50x50 pixels -->
    <span class="logo-mini"><b>CSM</b></span>
    <!-- logo for regular state and mobile devices -->
    <span class="logo-lg"><b>CSM</b></span>
</a>

<!-- Header Navbar: style can be found in header.less      #29088A       -->
<nav class="navbar navbar-static-top" role="navigation" style="color: white; background-color: #337ab7">

    <!-- Sidebar toggle button-->
    <a href="#" id="toggleLink" class="sidebar-toggle" style="background-color: #337ab7;" data-toggle="offcanvas" role="button" data-target="divMenu">
        <span class="sr-only">Toggle navigation</span>
    </a>
    <!-- Navbar Right Menu -->
    <div id="divMenu" class="navbar-custom-menu">
        <ul class="nav navbar-nav">
            <!-- User Account: style can be found in dropdown.less -->
            <li class="dropdown user user-menu">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    <%--<img src="../dist/img/tsm_logo.jpg" class="user-image" alt="User Image">--%>
                    <span class="hidden-xs">
                        <asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label>
                    </span>
                </a>
                <ul class="dropdown-menu">
                    <!-- User image -->
                    <li class="user-header" style="background-color: #337ab7;">
                        <div class="row">
                            <div class="col-xs-6">
                                <img src="../images/image_data/Default.png" style="width: 100%; height: 60%"
                                    class="img-responsive" runat="server" id="imgLoginUser" />
                            </div>
                            <div class="col-xs-6">
                                <p>
                                    <asp:Label ID="lblFullName" runat="server" Text="Guest"></asp:Label>
                                    <small>
                                        <asp:Label ID="lblAddress" runat="server" /></small>
                                </p>
                            </div>
                        </div>
                    </li>
                    <!-- Menu Body -->

                    <!-- Menu Footer-->
                    <li class="user-footer">
                        <div class="pull-left">
                            <a href="../Pages/Profile.aspx" class="btn btn-info btn-flat">
                                <asp:Literal runat="server" Text="Mon Profil"></asp:Literal></a>
                        </div>
                        <div class="pull-right">
                            <a href="#" class="btn btn-danger btn-flat" runat="server"
                                onserverclick="lblLogout_Click">
                                <asp:Literal runat="server" Text="Se déconnecter"></asp:Literal></a>
                        </div>
                    </li>
                </ul>
            </li>
            <!-- Control Sidebar Toggle Button -->
            <%--<li>
                <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
            </li>--%>

            <li>
                <%--<div class="input-group-btn">--%>
                <%--<a href="#">
                    <i class="fa fa-gears"></i>
                </a>--%>
               <%-- <a id="btnLang" runat="server" value="en"
                    onserverclick="ddlanguage_SelectedIndexChanged">
                    <i class="flag-icon flag-icon-gb"></i>
                </a>--%>

                <%-- <ul class="dropdown-menu"> 
                        <li><asp:LinkButton runat="server"></asp:LinkButton></li> 
                      </ul>
                    </div>--%>

                <%--<asp:DropDownList runat="server"  OnSelectedIndexChanged="ddlanguage_SelectedIndexChanged">
                    <asp:ListItem Text="EN <span class='flag-icon flag-icon-gb'></span>">                        
                    </asp:ListItem>
                    <asp:ListItem Text="FR <span class='flag-icon flag-icon-fr'></span>">                        
                    </asp:ListItem>
                </asp:DropDownList>--%>
                <%-- <select runat="server" onserverchange="ddlanguage_SelectedIndexChanged">
                    <option value="en" selected>EN </option>
                    <option value="fr">FR</option>
                </select>--%>
            </li>
        </ul>
    </div>
</nav>
