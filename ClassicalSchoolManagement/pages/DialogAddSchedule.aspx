<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogAddSchedule.aspx.cs" Inherits="DialogAddSchedule" %>


<%--@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--  <title>Definir Horaire Classe</title>--%>
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
        .divLeftDesign {
            width: 100%;
            float: left;
        }

        .divRightDesign {
            width: 100%;
            float: left;
        }
    </style>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseDialog() {
            GetRadWindow().close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="radStartHour">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radEndHour">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblError" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlCourse">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddlTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>


        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>





        <div class="panel panel-info">
            <%-- <div class="panel-heading">
                <h4><span class="fa fa-calendar"></span>&nbsp; <asp:Label runat="server" ID="lblClassName"></asp:Label></h4>
            </div>--%>

            <div class="panel-body">
                <asp:HiddenField runat="server" ID="hiddenClassId" />
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lbldays" runat="server" Text="Jours"></asp:Label>
                        <telerik:RadComboBox ID="ddlDays" runat="server" Skin="Office2007" Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem Value="MO" Text="Lundi" Selected="true" />
                                <telerik:RadComboBoxItem Value="TU" Text="Mardi" />
                                <telerik:RadComboBoxItem Value="WE" Text="Mercredi" />
                                <telerik:RadComboBoxItem Value="TH" Text="Jeudi" />
                                <telerik:RadComboBoxItem Value="FR" Text="Vendredi" />
                                <%--<telerik:RadComboBoxItem Value="SA" Text="Samedi" />--%>
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="Vacation"></asp:Label>
                        <telerik:RadDropDownList runat="server" ID="ddlVacation" Skin="Office2007"
                            CausesValidation="false" AutoPostBack="true" Width="100%" Enabled="false" Font-Bold="true"
                            OnSelectedIndexChanged="ddlVacation_SelectedIndexChanged">
                            <Items>
                                <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                <telerik:DropDownListItem Value="AM" Text="Matin" />
                                <telerik:DropDownListItem Value="PM" Text="Median" />
                                <telerik:DropDownListItem Value="NG" Text="Soir" />
                                <%--<telerik:DropDownListItem Value="WK" Text="Weekend" />--%>
                            </Items>
                        </telerik:RadDropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblHeure_Debut" runat="server" Text="Heure de début"></asp:Label>
                        <telerik:RadTimePicker runat="server" ID="radStartHour" AutoPostBack="true"
                            DateInput-ReadOnly="false" DateInput-CausesValidation="false" Skin="Office2007"
                            OnSelectedDateChanged="radStartHour_SelectedDateChanged" Width="100%">
                        </telerik:RadTimePicker>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblHeure_Fin" runat="server" Text="Heure de fin"></asp:Label>
                        <telerik:RadTimePicker runat="server" ID="radEndHour" Skin="Office2007"
                            DateInput-ReadOnly="false" DateInput-CausesValidation="false" AutoPostBack="true"
                            OnSelectedDateChanged="radEndHour_SelectedDateChanged" Width="100%">
                        </telerik:RadTimePicker>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblToal_hrs" runat="server" Text="Total Heures"></asp:Label>
                        <telerik:RadTextBox ID="txtDuration" runat="server" Width="100%"
                            Enabled="false" Font-Italic="true" Skin="Web20" Font-Size="Small">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblCours" runat="server" Text="Cours"></asp:Label>
                        <telerik:RadComboBox ID="ddlCourse" runat="server" Width="100%" Skin="Office2007" MaxHeight="200"
                            OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" CausesValidation="false"
                            AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblteacher" runat="server" Text="Professeur"></asp:Label>
                        <telerik:RadComboBox ID="ddlTeacher" runat="server" Width="100%" Skin="Office2007" MaxHeight="200">
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    
                    <div class="col-md-5">
                     <br />
                            <span runat="server" id="divCoursAttached" visible="false" class="text-red text-bold fa-italic text-sm text-info">
                               La couleur rouge indique que le professeur n'enseigne plus ce cours
                            </span>

                        </div>
                </div>


                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                     <%--   <button type="button" class="btn btn-sm btn-default" id="btnSearch" runat="server"
                            onserverclick="btnSearch_Click" style="width: 120px;">
                            <span class="fa fa-search"></span>
                            <asp:Literal runat="server" Text="Rechercher"></asp:Literal></button>
                        &nbsp;--%>
                        <button type="button" class="btn btn-sm btn-primary" id="btnAdd" runat="server"
                            onserverclick="btnAdd_Click" style="width: 120px;">
                            <span class="fa fa-plus"></span>
                            <asp:Literal runat="server" Text="Ajouter"></asp:Literal></button>
                        &nbsp;
                        <button type="button" class="btn btn-sm btn-danger" id="Button1" runat="server"
                            onserverclick="btnBack_Click" style="width: 120px;">
                            <span class="fa fa-remove"></span>
                            <asp:Literal runat="server" Text="Fermer"></asp:Literal></button>
                    </div>
                </div>


                <%--*************  RAD GRID VIEW ********************--%>

                <br />
                <div class="row" runat="server">
                    <div class="col-md-12" style="width: 100%;">
                        <telerik:RadGrid runat="server" Skin="Office2007" RenderMode="Lightweight"
                            ID="radGridScheduleDetails"
                            OnNeedDataSource="radGridScheduleDetails_NeedDataSource"
                            OnItemCommand="radGridScheduleDetails_ItemCommand"
                            OnItemDataBound="radGridScheduleDetails_ItemDataBound">
                            <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed"
                                DataKeyNames="id" AllowPaging="true" ShowFooter="true" PageSize="10">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labelNo"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hiddenCoursAttachStatus" Value='<%# Eval("teacher_cours_attach_status") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Jours" DataField="days">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Vacation" DataField="vacation">
                                        <HeaderStyle HorizontalAlign="Center"  Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Heure de début" DataField="start_hour">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Heure de fin" DataField="end_hour">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cours" DataField="cours_name">
                                        <HeaderStyle HorizontalAlign="Center"  Width="250px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nom du professeur" DataField="fullName">
                                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <button runat="server" class="btn btn-danger btn-sm" title="Supprimer"
                                                id="btnRemoveSchedule" onserverclick="removeSchedule">
                                                <span class="fa fa-remove"></span>
                                            </button>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
