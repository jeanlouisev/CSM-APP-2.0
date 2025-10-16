<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogSalaryConfigClass.aspx.cs" Inherits="DialogSalaryConfigClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration Salaire Par Classe</title>
    <!--connec to main css file-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        .labelDesign {
            font-size: small;
            font-family: sans-serif;
        }

        #tblEditClass tr > td {
            padding-bottom: 10px;
        }

        .mainDiv {
            width: 100%;
            margin: auto;
        }
    </style>


    <script type="text/javascript">
        function ClientCloseStaffAmount(oWnd, args) {

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tblStaffPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="divSearchPersonel" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
                    Skin="Office2007" DestroyOnClose="false" OnClientClose="ClientCloseStaffAmount">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <div class="mainDiv">

            <asp:Panel ID="pnlSearchClassroom" runat="server" GroupingText="Configurer salaire par classe" CssClass="panellDesign">
                <table border="0" style="width: 100%">
                    <tr style="height: 40px;">
                        <td colspan="1" style="width: 7%; text-align: right;">
                            <asp:Label ID="lblCode" runat="server" Text="Classe :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 10%;">
                            <telerik:RadDropDownList runat="server" ID="ddlClassroom" Width="300px"
                                OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged"
                                CausesValidation="false" AutoPostBack="true" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Text="--Tout Sélectionner--" Selected="true" Value="-1" />
                                    <telerik:DropDownListItem Text="1ere Annee Kindergarden " Value="1" />
                                    <telerik:DropDownListItem Text="2ieme Annee Kindergarden" Value="2" />
                                    <telerik:DropDownListItem Text="3ieme Annee Kindergarden " Value="3" />
                                    <telerik:DropDownListItem Text="1ere Annee Fondamentale" Value="10" />
                                    <telerik:DropDownListItem Text="2ieme Annee Fondamentale" Value="20" />
                                    <telerik:DropDownListItem Text="3ieme Annee Fondamentale" Value="30" />
                                    <telerik:DropDownListItem Text="4ieme Annee Fondamentale" Value="40" />
                                    <telerik:DropDownListItem Text="5ieme Annee Fondamentale" Value="50" />
                                    <telerik:DropDownListItem Text="6ieme Annee Fondamentale" Value="60" />
                                    <telerik:DropDownListItem Text="7ieme Annee Fondamentale" Value="70" />
                                    <telerik:DropDownListItem Text="8ieme Annee Fondamentale" Value="80" />
                                    <telerik:DropDownListItem Text="9ieme Annee Fondamentale" Value="90" />
                                    <telerik:DropDownListItem Text="3ieme Secondaire" Value="100" />
                                    <telerik:DropDownListItem Text="Seconde" Value="110" />
                                    <telerik:DropDownListItem Text="Retho" Value="120" />
                                    <telerik:DropDownListItem Text="Philo" Value="130" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />

            <asp:Panel ID="pnlResult" runat="server" Visible="false" CssClass="panellDesign">
                <div style="width: 100%; border: 0px solid orange; max-height: 400px; overflow-y: scroll;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red">PAS DE DONNEES</asp:Label>
                    <asp:GridView ID="gridListClassroom" runat="server"
                        AutoGenerateColumns="False" Width="100%"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                        OnSelectedIndexChanged="OnSelectedIndexChanged"
                        BorderWidth="1px" AllowPaging="false"
                        OnRowCommand="gridListClassroom_RowCommand"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" DataKeyNames="id"
                        PagerSettings-Mode="NumericFirstLast"
                        OnRowDataBound="gridListClassroom_RowDataBound">
                        <RowStyle Height="30px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="NOM CLASSE"
                                DataFormatString="{0:dd/MM/yyyy}" Visible="true">
                                <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="320px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="STATUT" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatusClass" Text='<%# Eval("status").ToString() == "1" ? "Actif" : "Inactif" %>'
                                        runat="server" HorizontalAlign="Center" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px" HeaderText="DEFINIR / VISUALISER MONTANT" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%--   <asp:ImageButton runat="server" ToolTip="Definir Montant" ID="btnAddCours"
                                        ImageUrl='<%# Eval("status").ToString() == "1" ? "~/images/AddNewitem.png" : "/images/AddNewitemIn.png" %>'
                                        Enabled='<%# Eval("status").ToString() == "1" ? true : false %>' />--%>

                                    <telerik:RadButton runat="server" Text="Definir Montant Salaire / Classe" Skin="Default"
                                        CommandName="define_class_amount" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>
                                    </telerik:RadButton>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle Height="22px" CssClass="gridHeaderDesign" />
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
                <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlEditClassroom" runat="server" GroupingText="" Visible="false" CssClass="panellDesign">
                <div style="border: 0px solid red; width: 450px; float: left; margin-left: 250px;">
                    <table border="0" align="center" style="width: 100%; margin-top: 1px;" id="tblEditClass">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label runat="server" ID="lblResultEditClassroom" Visible="false"
                                    Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" align="right" style="width: 30%">Code :
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtCodeClassroom"
                                    EmptyMessage="Empty field..." Width="100%" BorderWidth="1" ReadOnly="true">
                                </telerik:RadTextBox>
                        </tr>
                        <tr>
                            <td colspan="1" align="right">Nom Classe :
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtNameClassroom"
                                    EmptyMessage="Empty field..." Width="100%" BorderWidth="1" ReadOnly="true">
                                </telerik:RadTextBox>
                        </tr>
                        <tr>
                            <td colspan="1" align="right">Effectif Maximal :
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtMaxQuantityClassroom"
                                    EmptyMessage="Empty field..." Width="100%" BorderWidth="1">
                                </telerik:RadTextBox>
                        </tr>
                        <tr>
                            <td colspan="1" align="right">Effectif actuel :
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtActualQuantityClassroom"
                                    EmptyMessage="Empty field..." Width="100%" BorderWidth="1" ReadOnly="true">
                                </telerik:RadTextBox>
                        </tr>
                        <tr>
                            <td colspan="1" align="right">Statut :
                            </td>
                            <td colspan="1">
                                <asp:UpdatePanel runat="server" ID="udpCheckboxes" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkActive" CausesValidation="false" AutoPostBack="true" runat="server"
                                            Text="Actif" OnCheckedChanged="chkActive_CheckedChanged" />
                                        <asp:CheckBox ID="chkNotActive" CausesValidation="false" AutoPostBack="true"
                                            runat="server" Text="Inactif" OnCheckedChanged="chkNotActive_CheckedChanged" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" align="right">Vacation :
                            </td>
                            <td colspan="1">
                                <asp:UpdatePanel runat="server" ID="udpVacationChk" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkAM" CausesValidation="false" AutoPostBack="true" runat="server"
                                            Text="Matin" OnCheckedChanged="chkActive_CheckedChanged" Enabled="false" />
                                        <asp:CheckBox ID="chkPM" CausesValidation="false" AutoPostBack="true" runat="server"
                                            Text="Median" OnCheckedChanged="chkNotActive_CheckedChanged" />
                                        <asp:CheckBox ID="chkNG" CausesValidation="false" AutoPostBack="true" runat="server"
                                            Text="Soir" OnCheckedChanged="chkActive_CheckedChanged" />
                                        <asp:CheckBox ID="chkWK" CausesValidation="false" AutoPostBack="true" runat="server"
                                            Text="Weekend" OnCheckedChanged="chkNotActive_CheckedChanged" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                    </table>
                    <table border="0" style="margin-top: 20px; width: 50%;" align="center">
                        <tr>
                            <td align="center">
                                <telerik:RadButton Skin="Default" Text="Modifier"
                                    runat="server" ID="btnUpdate" Width="100"
                                    CommandName="updateClassroom" CausesValidation="false"
                                    AutoPostBack="true"
                                    OnClick="updateClassInfo">
                                </telerik:RadButton>
                            </td>
                            <td align="center">
                                <telerik:RadButton Skin="Default" Text="Retour"
                                    runat="server" ID="btnReturn" Width="100"
                                    CausesValidation="false" AutoPostBack="true"
                                    CommandName="finishReturn" OnClick="updateClassInfo">
                                </telerik:RadButton>
                            </td>

                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div style="width: 100%; text-align: center;">
                <asp:Label runat="server" ID="lblNoclasse" ForeColor="Purple" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                <asp:Label runat="server" ID="lblClassCode" Visible="false" ForeColor="green" Text=""></asp:Label>
                <asp:Label runat="server" ID="lblStatus" Visible="false" ForeColor="green" Text=""></asp:Label>
            </div>
            <asp:Panel ID="pnlAddCoursToClassroom" runat="server" GroupingText="Affecter cours a classe " Visible="false" CssClass="panellDesign">

                <asp:Panel ID="Selecttioner_Cours" runat="server" GroupingText="" CssClass="panellDesign">
                    <div runat="server" id="div1" class="divGridHeader">
                        <table border="0" runat="server" class="tblGridHeader" id="Table1" style="text-align: left;">
                            <tr>
                                <td style="width: 115px; font-weight: bold;">No</td>
                                <td style="width: 320px; font-weight: bold;">NOM COURS</td>
                                <td style="width: 170px; font-weight: bold; text-align: center;">PRIX / HEURE(HTG)</td>
                                <td style="width: 300px; font-weight: bold; text-align: center;">SELECTIONNER</td>
                            </tr>
                        </table>
                    </div>
                    <div style="overflow: scroll; max-height: 370px; overflow-x: hidden; height: auto; max-width: 1600px;">
                        <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                        <asp:GridView ID="gridListCourse" runat="server" AutoGenerateColumns="False"
                            Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                            BorderWidth="1px" AllowPaging="false" Width="100%"
                            OnRowCommand="gridListCourse_RowCommand" DataKeyNames="id"
                            BackColor="LightGoldenrodYellow" BorderColor="Tan"
                            GridLines="Both" HeaderStyle-CssClass="FixedHeader"
                            OnRowDataBound="gridListCourse_RowDataBound">
                            <RowStyle Height="10px" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="125px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="125px" />
                                </asp:TemplateField>
                                <%--   <asp:BoundField DataField="id" HeaderText="Code">
                        <HeaderStyle Width="125px" HorizontalAlign="center" BorderColor="White" BorderWidth="0" />
                        <ItemStyle Width="102px" HorizontalAlign="left" BorderWidth="1px" />
                    </asp:BoundField>--%>
                                <asp:BoundField DataField="name" HeaderText="Nom Cours">
                                    <HeaderStyle Width="380px" HorizontalAlign="left" BorderColor="White" BorderWidth="0" />
                                    <ItemStyle Width="300px" HorizontalAlign="left" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Prix / Heure (HTG)">
                                    <HeaderStyle Width="150px" HorizontalAlign="left" BorderColor="White" BorderWidth="0" />
                                    <ItemStyle Width="160px" HorizontalAlign="center" BorderWidth="1px" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="160px" HorizontalAlign="center" ID="ddlprice" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sélectionner">
                                    <HeaderStyle Width="400px" HorizontalAlign="center" BorderColor="White" BorderWidth="0" />
                                    <ItemStyle Width="300px" HorizontalAlign="center" BorderWidth="1px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" CssClass="gridCB" runat="server" HorizontalAlign="center"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="Navy" Font-Bold="True" Height="22px" HorizontalAlign="Left"
                                ForeColor="WhiteSmoke" BorderColor="Navy" VerticalAlign="Top" BorderWidth="2px" Width="942px" Font-Size="Small" />
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
                </asp:Panel>
                <div style="margin: auto; width: 200px;">
                    <table border="0" style="margin-top: 10px;" align="center">
                        <tr>
                            <td>
                                <telerik:RadButton Skin="Glow" Text="Affecter" runat="server"
                                    ID="btnAffecterCours" Width="100" OnClick="btnAffecterCours_click">
                                </telerik:RadButton>
                            </td>
                            <td style="width: 10px;"></td>
                            <td>
                                <telerik:RadButton Skin="Glow" Text="Retourner" runat="server"
                                    ID="btnBack" Width="100" OnClick="btnBack_click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
