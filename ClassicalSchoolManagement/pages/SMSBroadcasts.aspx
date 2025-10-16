<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMSBroadcasts.aspx.cs"
    Inherits="SMSBroadcasts" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    BroadcastSms
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
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

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListContact");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListContact");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

        function ClientCloseSmsTemplate(oWnd, args) {
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        }
    </script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">

    <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" OnClientClose="ClientCloseSmsTemplate"
                Skin="Office2007">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlcontentSms" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlcontentSms">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtMessage" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlNetwork">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlNetwork" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlcontentSms">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlcontentSms" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlCourse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlCourse" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlPosition">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlPosition" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <asp:Panel ID="pnlSearcStaff" runat="server" GroupingText="Boadcast Sms & Whatsapp " CssClass="panellDesign">
        <table border="0" style="width: 100%;">
            <tr class="trDesign">
                <td colspan="1" style="width: 8%; text-align: left;">Code : 
                </td>
                <td colspan="1" style="width: 200px">
                    <telerik:RadTextBox ID="txtCode" runat="server"
                        Width="100%" Skin="Bootstrap">
                    </telerik:RadTextBox>
                </td>
                <td colspan="1" style="width: 5px; text-align: center;">Téléphone : 
                </td>
                <td colspan="1" style="width: 250px">
                    <telerik:RadNumericTextBox ID="txtTelephone" runat="server"
                        Font-Size="Small" MaxLength="8" Width="100%" Skin="Bootstrap"
                        ForeColor="Black" Type="Number" EmptyMessage=""
                        EmptyMessageStyle-Font-Italic="true">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                    </telerik:RadNumericTextBox>
                </td>
                <td colspan="1" style="width: 5px; text-align: center;">Nom : 
                </td>
                <td colspan="1" style="width: 200px">
                    <telerik:RadTextBox ID="txtFullName" runat="server"
                        Width="95%"
                        Skin="Bootstrap">
                    </telerik:RadTextBox>
                </td>
                <td colspan="1" style="width: 5px; text-align: left;">Position : 
                </td>
                <td colspan="1" style="width: 200px">
                    <telerik:RadDropDownList ID="ddlPosition" runat="server" Width="100%" CausesValidation="false"
                        AutoPostBack="true" Skin="Bootstrap" OnSelectedIndexChanged="ddlposition_SelectedIndexChanged">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="PERSONNEL" Text="PERSONNEL" />
                            <telerik:DropDownListItem Value="PROFESSEUR" Text="PROFESSEUR" />
                            <telerik:DropDownListItem Value="ELEVE" Text="ELEVE" />
                            <telerik:DropDownListItem Value="PARENT" Text="PARENT" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
            </tr>
            <tr class="trDesign">
                <td colspan="1">Réseau(x) :
                </td>
                <td>
                    <telerik:RadDropDownList ID="ddlNetwork" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged" Width="100%" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="1" Text="NATCOM & DIGICEL" Selected="true" />
                            <telerik:DropDownListItem Value="2" Text="NATCOM" />
                            <telerik:DropDownListItem Value="3" Text="DIGICEL" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td colspan="1">Classe : 
                </td>
                <td>
                    <telerik:RadDropDownList ID="ddlClassroom" runat="server" Width="100%" Skin="Bootstrap"
                        DropDownWidth="300px" DropDownHeight="220px">
                    </telerik:RadDropDownList>
                </td>
                <td colspan="1">Vaction : 
                </td>
                <td colspan="1">
                    <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="95%" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:DropDownListItem Value="AM" Text="Matin" />
                            <telerik:DropDownListItem Value="PM" Text="Median" />
                            <telerik:DropDownListItem Value="NG" Text="Soir" />
                            <telerik:DropDownListItem Value="WK" Text="Weekend" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td colspan="1">Cours : </td>
                <td colspan="1">
                    <asp:UpdatePanel runat="server" ID="udpDdlCourse" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <telerik:RadDropDownList ID="ddlCourse" runat="server" Width="100%" Skin="Bootstrap"
                                CausesValidation="false" DropDownWidth="200px" AutoPostBack="true" Enabled="false">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="trDesign">
                <td colspan="6" rowspan="4">
                    <telerik:RadTextBox ID="txtMessage" runat="server" Width="99%" TextMode="MultiLine" Skin="Bootstrap"
                        EmptyMessage="Taper votre message.160 caractères maximum  !" Height="75" MaxLength="160">
                    </telerik:RadTextBox>
                    <asp:Label runat="server" ID="lblContentSMS" Text="Erreur : Le contenu du message est vide . Veuiller ajouter un message !"
                        Visible="false" ForeColor="Red" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                </td>
                <td colspan="1">Modèle</td>
                <td colspan="1">
                    <telerik:RadComboBox runat="server" Skin="Bootstrap" ID="ddlcontentSms" Width="100%" MaxHeight="150px"
                        OnSelectedIndexChanged="ddlcontentSms_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                        <Items>
                            <telerik:RadComboBoxItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                            <telerik:RadComboBoxItem Value="-2" Text="Ajouter Nouveau" Font-Bold="true" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr class="trDesign">
                <td colspan="1">Type</td>
                <td>
                    <telerik:RadDropDownList ID="RadDropDownList2" runat="server" Width="100%" Skin="Bootstrap">
                        <Items>
                            <telerik:DropDownListItem Value="-1" Text="SMS" Selected="true" />
                            <telerik:DropDownListItem Value="-2" Text="WHATSAPP" />
                            <telerik:DropDownListItem Value="-3" Text="SMS & WHATSAPP" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
            </tr>
        </table>

        <table align="center" border="0" style="margin-top: 10px; width: 100%;">
            <tr>
                <td colspan="1" style="width: 35%"></td>
                <td colspan="1" style="width: 15%; text-align: center;">
                    <telerik:RadButton ID="btnSearchContact" OnClick="btnSearchContact_Click" runat="server" CausesValidation="true"
                        Text="Rechercher" Width="80%" Skin="Glow">
                    </telerik:RadButton>
                </td>
                <td colspan="1" style="width: 15%; text-align: center;">
                    <telerik:RadButton ID="btnSendMessage" OnClick="btnSendMessag_Click" runat="server" CausesValidation="true"
                        Text="Envoyer" Width="80%" Skin="Glow">
                    </telerik:RadButton>
                </td>
                <td colspan="1" style="width: 35%"></td>
            </tr>
        </table>
    </asp:Panel>


    <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des Contacts" Visible="false" CssClass="panellDesign">
        <span style="float: right;">
            <asp:Label runat="server" ID="lblGridNotCheck" Test-align="right" Text="Erreur : Aucun contact n'a été selectioné !"
                Visible="false" ForeColor="Red" Font-Italic="true" Font-Size="Smaller"></asp:Label>
        </span>
        <div style="overflow: scroll; overflow-x: hidden; height: auto; width: 100%; border: 0px solid red;">
            <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
            <asp:GridView ID="gridListContact" runat="server" AutoGenerateColumns="False"
                Style="overflow: auto;" CellPadding="2"
                ForeColor="Black" BorderWidth="1px"
                AllowPaging="false" Width="100%"
                OnRowCommand="gridListContact_RowCommand"
                BackColor="White" BorderColor="Tan"
                GridLines="Both"
                OnRowDataBound="gridListContact_RowDataBound">
                <RowStyle Height="10px" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="35px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="staff_code" HeaderText="CODE">
                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fullName" HeaderText="NOM">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="phone" HeaderText="PHONE">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="level" HeaderText="POSITION">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="classroom" HeaderText="CLASSE">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="vacation" HeaderText="VACATION">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkBroascast" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkAll" runat="server"
                                OnCheckedChanged="checkAll_CheckedChanged" AutoPostBack="True" CausesValidation="false" />
                            <itemstyle horizontalalign="left" width="45px" verticalalign="Middle" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="45px" />
                        <ItemStyle HorizontalAlign="Left" Width="45px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                <HeaderStyle Height="22px" CssClass="gridHeaderDesign" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" HorizontalAlign="Center" />
                <RowStyle Height="5px" Font-Size="Smaller" BorderWidth="0.3" />
                <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver" BorderStyle="None" />
                <SortedAscendingCellStyle BackColor="SkyBlue" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
        </div>
        <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
    </asp:Panel>

</asp:Content>
