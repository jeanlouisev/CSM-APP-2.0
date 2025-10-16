<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenu.ascx.cs" Inherits="SideMenu" %>


<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
<!-- Font Awesome -->
<link rel="stylesheet" href="../bootstrap/css/font-awesome.min.css">
<!-- Ionicons -->
<link rel="stylesheet" href="../bootstrap/css/ionicons.min.css">
<!-- jvectormap -->
<link rel="stylesheet" href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css">
<!-- Theme style -->
9
<link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
<!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
<link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css">


<script>
    $(document).ready(function () {
        var url = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
        $('.treeview-menu li').removeClass('active');
        $('[href$="' + url + '"]').parent().addClass("active");
        $('.treeview').removeClass('menu-open active');
        $('[href$="' + url + '"]').closest('li.treeview').addClass("menu-open active");
    });
</script>
<style type="text/css">
    .treeview-menu .active {
        background-color: gray;
    }

    .treeview-menu {
        display: none;
    }
</style>





<%-- +++++++++++++++++++++ GENERAL MENU ++++++++++++++++ --%>

<!-- sidebar: style can be found in sidebar.less -->
<section class="sidebar" runat="server" visible="true">
    <!-- Sidebar user panel -->
    <ul class="sidebar-menu">
        <li>
            <div class="row">
                <div class="col-xs-5">
                    <img src="../images/image_data/Default.png" style="width: 100%; height: 60%"
                        class="img-responsive" runat="server" id="imgLoginUser" />
                </div>
                <div class="col-xs-7">
                    <asp:Label ID="lblFullname" runat="server" Text="Guest"
                        CssClass="text text-lime text-bold" Style="width: 230px;"></asp:Label>
                </div>
            </div>
        </li>
        <li class="header">
            <asp:Literal runat="server" Text="menu"></asp:Literal>
        </li>

        <li class="treeview">
            <a href="../Pages/Home.aspx">
                <i class="fa fa-home"></i><span>
                    <asp:Literal runat="server" Text="Acceuil">
                    </asp:Literal></span>
            </a>
        </li>

        <%-- **************** ELEVE | STUDENT  menu-1 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-graduation-cap"></i><span>
                    <asp:Literal runat="server" Text="Gestion des elèves"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">
                <li><a href="../Pages/RegisterStudents.aspx"><i class="fa fa-file"></i>
                    <asp:Literal runat="server" Text="Inscription"></asp:Literal></a></li>
                <li><a href="../Pages/SearchStudents.aspx"><i class="fa fa-list-ol"></i>
                    <asp:Literal runat="server" Text="Liste des elèves"></asp:Literal></a></li>
                   <li><a href="../Pages/Parents.aspx"><i class="fa fa-list-ol"></i>
                    <asp:Literal runat="server" Text="Liste des Parent"></asp:Literal></a></li>
                <li><a href="../Pages/ChangingClassManagement.aspx"><i class="fa fa-refresh"></i>
                    <asp:Literal runat="server" Text="Changement de Classe"></asp:Literal></a></li>
            </ul>
        </li>

        <%-- **************** CLASSE | CLASSROOM  menu-2 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-building-o"></i><span>
                    <asp:Literal runat="server" Text="Gestion des Classes"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">
                <li><a href="../Pages/ClassroomManagement.aspx"><i class="fa fa-list-ol"></i>
                    <asp:Literal runat="server" Text="Liste des Classes"></asp:Literal></a></li>
            </ul>
        </li>


        <%-- **************** SECRETARIAT | SECRETARIAT  menu-3 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-desktop"></i><span>
                    <asp:Literal runat="server" Text="Secrétariat"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">
                <li><a href="../Pages/AddCours.aspx"><i class="fa fa-book"></i>
                    <asp:Literal runat="server" Text="Liste des cours"></asp:Literal></a></li>
                <li><a href="../Pages/InsertNotes.aspx"><i class="fa fa-book"></i>
                    <asp:Literal runat="server" Text="Insérer Notes"></asp:Literal></a></li>
              <%--  <li><a href="../Pages/DownloadNotes.aspx"><i class="fa fa-download"></i>
                    <asp:Literal runat="server" Text="Download Palmarès"></asp:Literal></a></li>
                <li><a href="../Pages/UploaderNotes.aspx"><i class="fa fa-upload"></i>
                    <asp:Literal runat="server" Text="Upload Palmarès"></asp:Literal></a></li>--%>
                <li><a href="../Pages/BulletinManagement.aspx"><i class="fa fa-certificate"></i>
                    <asp:Literal runat="server" Text="Gestion Bulletin"></asp:Literal></a></li>
                <li><a href="../Pages/AverageManagement.aspx"><i class="fa fa-file-text-o"></i>
                    <asp:Literal runat="server" Text="Définir la moyenne générale"></asp:Literal></a></li>
                <li><a href="../Pages/DefineShedule.aspx"><i class="fa fa-calendar"></i>
                    <asp:Literal runat="server" Text="Horaire de Travail"></asp:Literal></a></li>
                <li><a href="../Pages/PresenceSheets.aspx"><i class="fa fa-clock-o  "></i>
                    <asp:Literal runat="server" Text="Feuille de présence"></asp:Literal></a></li>
                <li><a href="../Pages/ReasonSheets.aspx"><i class="fa fa-files-o"></i>
                    <asp:Literal runat="server" Text="Definir Motifs Absence"></asp:Literal></a></li>
                <li><a href="../Pages/AddExams.aspx"><i class="fa fa-book"></i>
                    <asp:Literal runat="server" Text="Gestion des Examens"></asp:Literal></a></li>
            </ul>
        </li>

        <%-- **************** RESSOURCES HUMAINES  | HR  menu-4 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-users"></i><span>
                    <asp:Literal runat="server" Text="Ressources Humaines"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">

                <li><a href="../Pages/AddPersonal.aspx"><i class="fa fa-plus"></i>
                    <asp:Literal runat="server" Text="Nouveau Personnel"></asp:Literal></a></li>
                <li><a href="../Pages/SearchPersonal.aspx"><i class="fa fa-list-ol"></i>
                    <asp:Literal runat="server" Text="Liste des personnels"></asp:Literal></a></li>

                <li><a href="../Pages/RegisterTeachers.aspx"><i class="fa fa-plus"></i>
                    <asp:Literal runat="server" Text="Nouveau Professeur"></asp:Literal></a></li>
                <li><a href="../Pages/SearchTeachers.aspx"><i class="fa fa-list-ol"></i>
                    <asp:Literal runat="server" Text="Liste des professeurs"></asp:Literal></a></li>

                <li><a href="../Pages/AffectCoursToTeacher.aspx"><i class="fa fa-book"></i>
                    <asp:Literal runat="server" Text="Affecter Cours aux Professeurs"></asp:Literal></a></li>
                <%-- <li><a href="../Pages/ParentManagement.aspx"><i class="fa fa-search"></i>
                    <asp:Literal runat="server" Text="Personne a Contacter"></asp:Literal></a></li>--%>


                <%--   <li><a href="../Pages/ParentManagement.aspx"><i class="fa fa-search"></i>
                    <asp:Literal runat="server" Text="Personne a Contacter"></asp:Literal></a></li>--%>
                <%--  <li><a href="../Pages/DocumentsManagement.aspx"><i class="fa fa-file-archive-o"></i>
                    <asp:Literal runat="server" Text="Gestion des documents"></asp:Literal></a></li>--%>
            </ul>
        </li>

        <%-- **************** ECONOMAT | ECONOMAT  menu-5 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-money"></i><span>
                    <asp:Literal runat="server" Text="Economat"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">
            <%--    <li><a href="../Pages/FixCoursePrice.aspx"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Liste des prix par cours"></asp:Literal></a></li>--%>
                <li><a href="../Pages/SalaryClassConfiguration.aspx"><i class="fa fa-cog"></i>
                    <asp:Literal runat="server" Text="Configuration Paiement/Classe"></asp:Literal></a></li>
                <li><a href="../Pages/TaxConfigurationManagement.aspx"><i class="fa fa-cog"></i>
                    <asp:Literal runat="server" Text="Configuration des taxes"></asp:Literal></a></li>
                <li><a href="../Pages/PayrollPersonal.aspx"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Payroll des personnels"></asp:Literal></a></li>
                <li><a href="../Pages/PayrollTeacher.aspx"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Payroll des professeurs"></asp:Literal></a></li>
                <li>
                   <%-- <a href="#"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Paiement frais de scolarité"></asp:Literal></a></li>--%>
                    
                    <a href="../Pages/PaymentManagement.aspx"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Paiement frais de scolarité"></asp:Literal></a></li>
                <li><a href="../Pages/Expenses.aspx"><i class="fa fa-money"></i>
                    <asp:Literal runat="server" Text="Dépenses Administratives"></asp:Literal></a></li>
            </ul>
        </li>

        <%-- **************** PARAMETRES | SETTINGS  menu-12 **************** --%>
        <li class="treeview">
            <a href="#">
                <i class="fa fa-cogs"></i><span>
                    <asp:Literal runat="server" Text="Paramètres"></asp:Literal>
                </span><i class="fa fa-angle-left pull-right"></i>
            </a>
            <ul class="treeview-menu">
                <%--<li><a href="../Pages/SMSBroadcasts.aspx"><i class="fa fa-envelope"></i>
                    <asp:Literal runat="server" Text="Broadcast SMS & Whatsapp"></asp:Literal></a></li>--%>
                <li><a href="../Pages/ManageGroupe.aspx"><i class="fa fa-flag"></i>
                    <asp:Literal runat="server" Text="Gestion des roles"></asp:Literal></a></li>
                <li><a href="../Pages/UserManagement.aspx"><i class="fa fa-users"></i>
                    <asp:Literal runat="server" Text="Gestion des utilisateurs"></asp:Literal></a></li>
                <li><a href="../Pages/AccademicYear.aspx"><i class="fa fa-calendar"></i>
                    <asp:Literal runat="server" Text="Année académique"></asp:Literal></a></li>
                <li><a href="../Pages/LogsManagement.aspx"><i class="fa fa-files-o"></i>
                    <asp:Literal runat="server" Text="Gestion Logs"></asp:Literal></a></li>
            </ul>
        </li>

        <li class="header">
            <asp:Literal runat="server" Text="session"></asp:Literal></li>
        <li><a href="../Pages/Profile.aspx"><i class="fa fa-user-md text-aqua"></i><span>
            <asp:Literal runat="server" Text="Mon Profil"></asp:Literal></span></a></li>
        <li><a href="../Pages/ChangePassword.aspx"><i class="fa fa-key text-yellow"></i><span>
            <asp:Literal runat="server" Text="Changer mot de passe"></asp:Literal></span></a></li>
        <li><a href="#" runat="server" onserverclick="lblLogout_Click"><i class="fa fa-sign-out text-red"></i><span>
            <asp:Literal runat="server" Text="Se déconnecter"></asp:Literal></span></a></li>
    </ul>
</section>
