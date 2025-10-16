<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchSheetsDetails.aspx.cs" Inherits="SearchSheetsDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Timesheet Details</title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            border: 0px solid red;
            width: 960px;
        }

        .headerInfo {
            border: 0px solid purple;
            width: 950px;
            margin: auto;
        }
    </style>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function ClientCloseSearchStudent() {
            var oWindow = GetRadWindow();
            if (oWindow != null) {
                oWindow.close();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="chkPresenceState">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtReasonAbsence" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnClose">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="MasterWrapper" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radDateTimesheet">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="MasterWrapper" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnInsertAll">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="MasterWrapper" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap" Height="100%">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="true"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                    Skin="Office2007">
                </telerik:RadWindow>
                <telerik:RadWindow ID="RadWindowSearchStudent" runat="server" Modal="True" OnClientClose="ClientCloseSearchStudent"
                    Skin="Vista">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel runat="server" ID="masterPanel" CssClass="panellDesign">
            <div class="headerInfo">
                <table border="0" style="width: 70%; margin: auto;" align="center">
                    <tr>
                        <td style="width: 50%; text-align: left;">Code :
                            <asp:Label runat="server" ID="lblTeacherId" ForeColor="Purple" Font-Bold="true" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="width: 50%; text-align: left;">Professeur :
                            <asp:Label runat="server" ID="lblFullname" ForeColor="Purple" Font-Bold="true" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Du : 
                            <telerik:RadDatePicker runat="server" Width="200px" ID="radFromDate" Culture="French (France)"
                                OnSelectedDateChanged="radFromDate_SelectedDateChanged">
                                <DateInput runat="server" CausesValidation="false" DateFormat="dddd dd MMMM yyyy"
                                    AutoPostBack="true" Width="100%" Culture="fr-LU" ReadOnly="true">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td>Au : 
                            <telerik:RadDatePicker runat="server" Width="200px" ID="radToDate" Culture="French (France)"
                                OnSelectedDateChanged="radToDate_SelectedDateChanged">
                                <DateInput runat="server" CausesValidation="false" DateFormat="dddd dd MMMM yyyy"
                                    AutoPostBack="true" Width="100%" Culture="fr-LU" ReadOnly="true">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="MasterWrapper">
                <asp:Panel runat="server" ID="pnlTeacherInfo" Visible="false" CssClass="panellDesign" GroupingText="Feuille de presence">
                    <div runat="server" id="divGridHeaderTeacher" class="divGridHeader">
                        <table border="1" visible="false" runat="server" id="tblGridHeaderTeacher" class="tblGridHeader" style="text-align: left;">
                            <tr>
                                <th style="width: 45px; font-weight: bold; text-align: left; border-left: 0px;">No</th>
                                <th style="width: 135px; font-weight: bold; text-align: center;">MATIERE</th>
                                <th style="width: 205px; font-weight: bold; text-align: center;">CLASSE</th>
                                <th style="width: 83px; font-weight: bold; text-align: center;">DATE</th>
                                <th style="width: 70px; font-weight: bold; text-align: center;">H. DEBUT</th>
                                <th style="width: 63px; font-weight: bold; text-align: center;">H. FIN</th>
                                <th style="width: 210px; font-weight: bold; text-align: center;">MOTIFS</th>
                                <th style="width: 50px; font-weight: bold; text-align: left;">STATUT</th>
                            </tr>
                        </table>
                    </div>
                    <div style="overflow: scroll; overflow-x: hidden; max-height: 420px; height: auto;" runat="server" id="divGridTeacherTimesheet">
                        <asp:Label ID="lblFoundTeacher" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                        <asp:GridView ID="gridListTeacher" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                            Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                            OnRowCommand="gridListTeacher_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                            GridLines="Both" HeaderStyle-CssClass="FixedHeader" OnRowDataBound="gridListTeacher_RowDataBound">
                            <RowStyle Height="10px" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cours_name" HeaderText="cours_name" Visible="true">
                                    <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="class_name" HeaderText="class_name" Visible="true">
                                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="register_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="days" Visible="true">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="start_hour" HeaderText="start_hour" Visible="true">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="end_hour" HeaderText="end_hour" Visible="true">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="absence_reason_definition" HeaderText="ABSENCE REASON"
                                    Visible="true">
                                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="true" Font-Size="Smaller" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="presence_status_definition" HeaderText="PRESENCE"
                                    Visible="true">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" Font-Bold="true" Wrap="true" Font-Size="Small" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="Navy" Font-Bold="True" Height="20px" HorizontalAlign="Left"
                                ForeColor="WhiteSmoke" BorderColor="Navy" VerticalAlign="Top" BorderWidth="2px" Width="940px" Font-Size="Small" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                                HorizontalAlign="Center" />
                            <RowStyle Height="5px" Font-Size="Smaller" />
                            <SelectedRowStyle BackColor="Aquamarine" ForeColor="GhostWhite" BorderColor="Silver"
                                BorderStyle="None" />
                            <SortedAscendingCellStyle BackColor="SkyBlue" />
                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                        </asp:GridView>
                    </div>
                    <asp:Label runat="server" ID="lblCounterTeacher" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                </asp:Panel>

            </div>
            <table border="0" align="center">
                <tr>
                    <td style="text-align: center;">
                        <telerik:RadButton runat="server" Text="Inserer" Skin="Glow" Width="90px"
                            ID="btnInsertAll" OnClick="btnInsertAll_Click">
                        </telerik:RadButton>
                    </td>
                    <td colspan="1" style="width: 5px;"></td>
                    <td style="text-align: center;">
                        <telerik:RadButton runat="server" Text="Terminer" Skin="Glow" Width="90px"
                            ID="btnClose" OnClick="btnClose_Click" Visible="false">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>

        </asp:Panel>
    </form>
</body>
</html>
