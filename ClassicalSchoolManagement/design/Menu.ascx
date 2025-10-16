<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menu1" %>

<style type="text/css">
    .UsernameDesign {
        white-space: normal;
        margin: 0px;
        padding: 0px;
    }
</style>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
<telerik:RadMenu ID="menu" runat="server" Width="1000px" Skin="Glow" Style="z-index: 2900">
    <Items>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Acceuil"
            NavigateUrl="~/Home.aspx?menu=Acceuil" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide"  Text="Eleve" 
            NavigateUrl="~/pages/RegisterStudents.aspx" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>

         <%--   <telerik:RadMenuItem runat="server" ExpandMode="ClientSide"  Text="Eleve" 
            NavigateUrl="~/pages/RegisterStudents.aspx?menu=Eleve" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>--%>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Professeur" 
            NavigateUrl="~/pages/RegisterTeachers.aspx?menu=Professeur" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Personnel" 
            NavigateUrl="~/pages/AddPersonal.aspx?menu=Personnel" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Cours" 
            NavigateUrl="~/pages/AddCours.aspx?menu=Cours" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide"
             Text="Classe" 
            NavigateUrl="~/pages/ClassroomManagement.aspx?menu=Classe" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Examen" 
            NavigateUrl="~/pages/AddExams.aspx?menu=Examen" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Notes" 
            NavigateUrl="~/pages/InsertNotes.aspx?menu=Notes" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Feuille de Controle" 
            NavigateUrl="~/pages/PresenceSheets.aspx?menu=Timesheet" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Horaire" 
            NavigateUrl="~/pages/DefineShedule.aspx?menu=Horaire" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Economat" 
            NavigateUrl="~/pages/Payment.aspx?menu=Economat" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
    <Items>
        <telerik:RadMenuItem IsSeparator="true"></telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" 
            Text="Parametres" 
            NavigateUrl="~/pages/ManageGroupe.aspx?menu=Parametres" Font-Size="11px" Font-Bold="true">
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>
<span style="height: 39px; width: 198px; border: 0px solid black; background-color: white; float: right;">
    <telerik:RadMenu ID="menuUser" runat="server" Skin="Glow" Width="200px" Style="z-index: 2900">
        <Items>
            <telerik:RadMenuItem Text="Username"  ForeColor="White" id="txtUserLoginFullName"
                Height="24px" Width="80%" Font-Size="11px" CssClass="UsernameDesign">
            </telerik:RadMenuItem>
        </Items>
        <Items>
            <telerik:RadMenuItem ToolTip="Logout" NavigateUrl="~/Pages/Login.aspx?action=logout" Width="15%"
                ImageUrl="~/images/logout.png" Height="24px">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadMenu>
</span>
