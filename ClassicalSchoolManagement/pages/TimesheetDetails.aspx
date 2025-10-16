<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimesheetDetails.aspx.cs"
    Inherits="TimesheetDetails" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Détails des feuilles de temps</title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            width: 1124px;
            min-width: 1024px;
        }

        .headerInfo {
            border: 0px solid purple;
            width: 100%;
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
                <telerik:AjaxSetting AjaxControlID="ddlAcademicYear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radFromDate"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="radToDate"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblFoundTeacher"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="gridListTeacher"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblCounterTeacher"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblFoundTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="gridListTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblCounterTeacher" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Office2007" Height="100%">
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

        <div id="MasterWrapper">
            <asp:Panel runat="server" ID="masterPanel" BorderColor="Silver" BorderWidth="1" CssClass="panellDesign">
                <div class="headerInfo">
                    <table style="width: 100%; margin-top: 0px;">
                        <tr class="trDesign">
                            <td colspan="1" style="width: 10%;">Code :</td>
                            <td colspan="1" style="width: 23%;">
                                <asp:Label runat="server" ID="lblTeacherId" ForeColor="Purple" Font-Bold="true" Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%;">Professeur :</td>
                            <td colspan="1" style="width: 23%;">
                                <asp:Label runat="server" ID="lblFullname" ForeColor="Purple" Font-Bold="true" Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 15%;">Année Académique :</td>
                            <td colspan="1" style="width: 20%;">
                                <telerik:RadComboBox runat="server" ID="ddlAcademicYear" Width="100%" Skin="Default"
                                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" CausesValidation="false"
                                    AutoPostBack="true">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr class="trDesign">
                            <td colspan="1">Date Debut : </td>
                            <td colspan="1">
                                <telerik:RadDatePicker runat="server" Width="200px" ID="radFromDate" Culture="French (France)"
                                    OnSelectedDateChanged="radFromDate_SelectedDateChanged">
                                    <DateInput runat="server" CausesValidation="false" DateFormat="dddd dd MMMM yyyy"
                                        AutoPostBack="true" Width="100%" Culture="fr-LU" ReadOnly="true">
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>Date Fin : </td>
                            <td colspan="1">
                                <telerik:RadDatePicker runat="server" Width="200px" ID="radToDate" Culture="French (France)"
                                    OnSelectedDateChanged="radToDate_SelectedDateChanged">
                                    <DateInput runat="server" CausesValidation="false" DateFormat="dddd dd MMMM yyyy"
                                        AutoPostBack="true" Width="100%" Culture="fr-LU" ReadOnly="true">
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>Cours : </td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ddlCourse" Width="100%" Skin="Default">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Classe : </td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ddlClassroom" Width="100%" Skin="Default">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                    <table border="0" style="width: 100%;">
                        <tr>
                            <td colspan="1" style="width: 45%"></td>
                            <td colspan="1" style="width: 10%; text-align: center;">
                                <telerik:RadButton runat="server" Text="Rechercher" Skin="Web20"
                                    ID="btnSearch" OnClick="btnSearch_Click" Width="90px" >
                                </telerik:RadButton>
                            </td>
                            <td colspan="1" style="width: 10%; text-align: center;">
                                <telerik:RadButton runat="server" Text="Inserer" Skin="Web20"
                                    ID="btnInsertAll" OnClick="btnInsertAll_Click" Width="90px">
                                </telerik:RadButton>
                            </td>
                            <td colspan="1" style="width: 35%; text-align: right;">
                                <asp:LinkButton runat="server" Text="Exporter vers excel" Visible="false"
                                    ID="lnkExportExcel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlTeacherInfo" GroupingText="Feuille de presence">
                <div style="overflow: scroll; overflow-x: hidden; max-height: 620px; height: auto;" runat="server" id="divGridTeacherTimesheet">
                    <asp:Label ID="lblFoundTeacher" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListTeacher" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                        BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListTeacher_RowCommand" BackColor="White" BorderColor="Tan"
                        GridLines="Both" OnRowDataBound="gridListTeacher_RowDataBound">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="days" HeaderText="Jours" Visible="true">
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sheet_date" HeaderText="Date" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cours_name" HeaderText="Matiere" Visible="true">
                                <ItemStyle HorizontalAlign="Left" Width="220px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="class_name" HeaderText="Classe" Visible="true">
                                <ItemStyle HorizontalAlign="Left" Width="300px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="start_hour" HeaderText="Heure Debut" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="60px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="end_hour" HeaderText="Heure Fin" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="60px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_hour" HeaderText="Durée" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="80px" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Valide" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="validationImage"
                                        ImageUrl='<%# Eval("validation_status").ToString().Trim() == "1" ? "~/images/green_status.png"  : "~/images/red_status.png"  %>' />

                                    <asp:HiddenField runat="server" ID="hiddenDaysCode" Value='<%# Eval("days_code").ToString()%>' />
                                    <asp:HiddenField runat="server" ID="hiddenSheetDate" Value='<%# Eval("sheet_date_inserted").ToString()%>' />
                                    <asp:HiddenField runat="server" ID="hiddenClassroomId" Value='<%# Eval("classroom_id").ToString()%>' />
                                    <asp:HiddenField runat="server" ID="hiddenCoursId" Value='<%# Eval("id_cours").ToString()%>' />
                                    <%--       <asp:HiddenField runat="server" ID="HiddenField3" Value='<%# Eval("sheet_date_inserted").ToString()%>' />
                                    <asp:HiddenField runat="server" ID="HiddenField4" Value='<%# Eval("sheet_date_inserted").ToString()%>' />--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Motifs" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadComboBox runat="server" ID="cboReasonType" Filter="StartsWith"
                                        CausesValidation="false" AutoPostBack="true" DropDownWidth="300"
                                        OnSelectedIndexChanged="cboReasonType_SelectedIndexChanged"
                                        Width="100%" MaxHeight="230px">
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="220px" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkPresenceStateAll" OnCheckedChanged="chkPresenceStateAll_CheckedChanged"
                                        runat="server" CausesValidation="false" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPresenceTeacher" OnCheckedChanged="chkPresenceTeacher_CheckedChanged"
                                        runat="server" CausesValidation="false" AutoPostBack="true"
                                        Checked='<%# Convert.ToInt32(Eval("presence_status").ToString().Trim()) == 1 ? true : false %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30px" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle Height="22px" CssClass="gridHeaderDesign" HorizontalAlign="Center" Width="100%" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                            HorizontalAlign="Center" />
                        <RowStyle Height="5px" Font-Size="Smaller" />
                        <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                            BorderStyle="None" />
                        <SortedAscendingCellStyle BackColor="SkyBlue" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </div>
                <asp:Label runat="server" ID="lblCounterTeacher" Visible="false" Font-Size="Small"
                    Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
            </asp:Panel>

        </div>
    </form>
</body>
</html>
