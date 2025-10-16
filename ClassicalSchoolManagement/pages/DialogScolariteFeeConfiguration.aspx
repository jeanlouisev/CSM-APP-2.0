<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogScolariteFeeConfiguration.aspx.cs" Inherits="DialogScolariteFeeConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration Frais Scolarite</title>
    <!--connec to main css file-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />

    <style type="text/css">
        .labelDesign {
            font-size: small;
            font-family: sans-serif;
        }

        #photoContainer {
            width: 100%;
            float: right;
            height: 70%;
            border: 1px solid silver;
            margin-bottom: 20px;
            -webkit-box-shadow: 0px 0px 30px silver;
            -moz-box-shadow: 0px 0px 30px silver;
            box-shadow: 0px 0px 30px silver;
        }

        .hideUploadButton {
            visibility: hidden;
        }

        .mainDiv {
            margin: auto;
            width: 1100px;

        }
    </style>


    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Voulez-vous vraiment supprimer ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

 </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

      
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtStartDay">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtStartDay" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtEndDay">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtEndDay" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtClassroomAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtPenalityAmount" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtStartDay" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtEndDay" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="txtincriptionfee">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtincriptionfee" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="txtentreefee">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtentreefee" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtClassroomAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtClassroomAmount" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="txtPenalityAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtPenalityAmount" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="txtVersemet1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="txtVersemet2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="txtVersemet3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet3" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="txtVersemet4">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet4" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        
         <telerik:AjaxSetting AjaxControlID="gridListPayment">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblversement_1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblversement_2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblversement_3" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet3" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblversement_4" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtVersemet4" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>


        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false"
            ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Office2007" DestroyOnClose="false">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Office2007" DestroyOnClose="false">
                </telerik:RadWindow>
                <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" MaxWidth="500" MaxHeight="500" MinHeight="500" MinWidth="500"
                    Skin="Office2007" DestroyOnClose="false">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

       <table border="0" width="100%">
    <tr>
        <td>
            <asp:Panel ID="pnlAddCours" runat="server" GroupingText="Configurer frais">
                <table border="0" align="center" width="100%">
                    <tr class="trDesign">
                        <td colspan="1" style="width: 10%;">
                            <asp:Label ID="Label2" runat="server" Text="Classe : ">
                            </asp:Label>
                        </td>
                        <td colspan="1" style="width: 20%;">
                            <telerik:RadDropDownList runat="server" ID="ddlClassroom" Skin="Bootstrap"
                                Width="100%" DropDownHeight="200px" DropDownWidth="300px">
                            </telerik:RadDropDownList>
                        </td>
                        <td colspan="1" style="width: 10%; text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text="Vacation : "></asp:Label>
                        </td>
                        <td colspan="1" style="width: 20%; vertical-align: top;">
                            <telerik:RadDropDownList ID="ddlVacation" runat="server"
                                Width="100%" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="AM" Text="Matin" />
                                    <telerik:DropDownListItem Value="PM" Text="Median" />
                                    <telerik:DropDownListItem Value="NG" Text="Soir" />
                                    <telerik:DropDownListItem Value="WK" Text="Weekend" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>

                        <td colspan="1" style="width: 10%; text-align: center;">
                            <asp:Label ID="Label3" runat="server" Text="Type : "></asp:Label>
                        </td>
                        <td colspan="2" style="width: 20%; vertical-align: top;">
                            <telerik:RadDropDownList runat="server" ID="ddlType" Width="100%" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-2" Text="MENSUEL" Selected="true" />
                                    <telerik:DropDownListItem Value="-3" Text="VERSEMENT" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1">
                            <asp:Label ID="lbincription" runat="server" Text="Inscription : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtincriptionfee" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="4"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" OnTextChanged="txtInscription_TextChanged"
                                Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                            </td>
                        <td colspan="1" style="text-align: center;">
                            <asp:Label ID="lblfraisentre" runat="server" Text="Entrer : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtentreefee" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" OnTextChanged="txtentrer_TextChanged" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>

                        <td colspan="1" style="text-align: center;">
                <asp:Label ID="Label7" runat="server" Text="Annee :" Enabled="false"></asp:Label></td>

            <td colspan="2" style="text-align: center;">
                <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="100%"
                    CausesValidation="false" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>
                        <%-- %><td colspan="1" style="text-align: center;">
                            <asp:Label ID="Label7" Width="100px" runat="server" Text="Annee Academique: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDropDownList ID="ddlAcademicYearStart" Skin="Bootstrap"
                                OnSelectedIndexChanged="ddlAcademicYearStart_OnSelectedIndexChanged"
                                CausesValidation="false" AutoPostBack="true" runat="server" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td>
                            <telerik:RadDropDownList ID="ddlAcademicYearEnd" Skin="Bootstrap"
                                OnSelectedIndexChanged="ddlAcademicYearEnd_SelectedDateChanged"
                                CausesValidation="false" AutoPostBack="true" runat="server" Width="100%">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td> --%>
                    </tr>
                    <tr class="trDesign">
                        <td colspan="1">
                            <asp:Label ID="lblMonth" runat="server" Text="Mensualité : " Visible="true">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtClassroomAmount" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" OnTextChanged="txtClassroomAmount_TextChanged"
                                Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <asp:Label ID="Label4" runat="server" Text="Pénalité : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtPenalityAmount" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="3"
                                EnabledStyle-HorizontalAlign="Right" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <asp:Label ID="Label5" runat="server" Text="Du / Au : "></asp:Label>

                        </td>
                        <td colspan="1">
                            <telerik:RadTextBox runat="server" ID="txtStartDay" Skin="Bootstrap"
                                MaxLength="2" Width="100%" EnabledStyle-HorizontalAlign="Center"
                                Font-Bold="true" CausesValidation="false"
                                ForeColor="Black" OnTextChanged="txtStartDay_TextChanged">
                            </telerik:RadTextBox>
                        </td>
                        <td colspan="1">
                            <telerik:RadTextBox runat="server" ID="txtEndDay" Skin="Bootstrap"
                                MaxLength="2" Width="100%" EnabledStyle-HorizontalAlign="Center"
                                Font-Bold="true" CausesValidation="false"
                                ForeColor="Black" OnTextChanged="txtEndDay_TextChanged">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="trDesign" runat="server" id="trVersmnt1">
                        <td colspan="1">
                            <asp:Label ID="lblversement_1" runat="server" Text="Versement 1 : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtVersemet1" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="text-align: center;">
                            <asp:Label ID="lblversement_2" runat="server" Text="Versement 2 : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtVersemet2" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr class="trDesign" runat="server" id="trVersmnt2">
                        <td colspan="1">
                            <asp:Label ID="lblversement_3" runat="server" Text="Versement 3 : ">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtVersemet3" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                            </td>
                        <td colspan="1" style="text-align: center;">
                            <asp:Label ID="lblversement_4" runat="server" Text="Versement 4  :">
                            </asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadNumericTextBox ID="txtVersemet4" runat="server"
                                Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="5"
                                EnabledStyle-HorizontalAlign="Right" CausesValidation="false"
                                AutoPostBack="true" Skin="Bootstrap">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
                <table border="0" align="center" style="margin-top: 10px;">
                    <tr>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnAddPayment" runat="server"
                                Text="Ajouter" Width="100px" Skin="Glow"
                                OnClick="btnAddPayment_Click" Visible="true">
                            </telerik:RadButton>
                        </td>
                        <td colspan="1" style="text-align: center; width:5px"></td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnSearchPayment" runat="server"
                                Text="Rechercher" Width="100px" Skin="Glow"
                                OnClick="btnSearchPayment_Click" Visible="true">
                            </telerik:RadButton>
                        </td>
                        <td colspan="1" style="text-align: center; width:5px"></td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnCancelPayment" runat="server"
                                Text="Annuler" Width="100px" Skin="Glow"
                                OnClick="btnCancelPayment_Click" Visible="false">
                            </telerik:RadButton>
                        </td>
                        <td colspan="1" style="text-align: center; width:5px"></td>
                        <td colspan="1" style="text-align: center;">
                            <telerik:RadButton ID="btnModifyPayment" runat="server"
                                Text="Modifier" Width="100px" Skin="Glow"
                                OnClick="btnModifyPayment_Click" Visible="false">
                            </telerik:RadButton>
                        </td>

                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste frais configurer" Visible="false">
                <%--  <div runat="server" id="divGridHeader" class="divGridHeader">
                    <table border="0" runat="server" id="tblGridHeader" class="tblGridHeader" style="text-align: left;">
                        <tr>
                            <td style="width: 50px; font-weight: bold;">No</td>
                            <td style="width: 180px; font-weight: bold;">Classe</td>
                            <td style="width: 100px; font-weight: bold; text-align: center;">Vacation</td>
                            <td style="width: 110px; font-weight: bold; text-align: center;">Type Paiement</td>
                            <td style="width: 60px; font-weight: bold; text-align: center;">Mensuel</td>
                            <td style="width: 70px; font-weight: bold; text-align: center;">1er Vers.</td>
                            <td style="width: 80px; font-weight: bold; text-align: center;">2eme Vers.</td>
                            <td style="width: 80px; font-weight: bold; text-align: center;">3eme Vers.</td>
                            <td style="width: 80px; font-weight: bold; text-align: center;">4eme Vers.</td>

                        </tr>
                    </table>
                </div>--%>
                <div style="width: 100%">
                    <asp:Label ID="lblFound" runat="server" Visible="False"
                        ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListPayment" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black"
                        BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListPayment_RowCommand" BackColor="LightGoldenrodYellow"
                        BorderColor="Tan" DataKeyNames="id" GridLines="Both"
                        OnRowDataBound="gridListPayment_RowDataBound">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Left" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" CssClass="gridBorderDesign" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="classroom_fullname" HeaderText="Classe">
                                <HeaderStyle Width="320px" HorizontalAlign="Left" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Left" Width="320px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vacation_fullname" HeaderText="Vacation">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="payment_type_definition" HeaderText="Type paiement">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="classroom_amount" HeaderText="Frais Mensuel">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="center" Width="60px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="versement_1" HeaderText="1er Versement">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="center" Width="70px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="versement_2" HeaderText="2eme Versement">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="versement_3" HeaderText="3eme Versement">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:BoundField DataField="versement_4" HeaderText="4eme versement">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="gridBorderDesign" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/Edit.jpg" HeaderText="Modifier"
                                CommandName="EdithPaiement" ItemStyle-CssClass="cursorDesign">
                                <HeaderStyle Width="20px" HorizontalAlign="Center" CssClass="gridBorderDesign" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="bottom" CssClass="gridBorderDesign" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="supprimer" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg"
                                        OnClientClick="Confirm()" OnClick="removepayment"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" HorizontalAlign="center" CssClass="borderDesign" />
                                <ItemStyle HorizontalAlign="center" Width="20px" CssClass="borderDesign" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="labelpaymentId" Visible="false" runat="server" Text='<%# Eval("payment_type").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="0px" BorderStyle="None" />
                                <ItemStyle BorderStyle="None" Width="0px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Navy" Font-Bold="True" Height="22px"
                            ForeColor="WhiteSmoke" VerticalAlign="Middle" Font-Size="Small" />
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
        </td>
    </tr>
</table>
    </form>
</body>
</html>
