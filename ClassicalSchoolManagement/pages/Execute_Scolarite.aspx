<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Execute_Scolarite.aspx.cs" Inherits="Execute_Scolarite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">




<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true"
            Skin="Bootstrap">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            border: 0px solid red;
            width: 960px;
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
            var oWindow = GetRadWindow();
            if (oWindow != null) {
                oWindow.close();
            }
        }

    </script>


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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <%--<telerik:AjaxUpdatedControl ControlID="tblPayment" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>--%>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="IMG_NEXT">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tab_mensuel1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="tab_mensuel2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="txtInscriptionAmountPaid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtInscriptionAmountPaid"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="txtInscriptionBalance"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="checkinscription"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="txtFraisEntreeAmountPaid"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblErrorAmount"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="txtFraisEntreeAmountPaid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtFraisEntreeAmountPaid"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="txtFraisEntreeBalance"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="checkEntree"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="txtSeptemberAmountPaid"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblErrorAmount"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>

        <div id="MasterWrapper">
            <table border="0" style="width: 100%;">
                <tr style="height: 40px;">
                    <td colspan="1" style="width: 3%; text-align: left;">
                        <asp:Label ID="lblCode" runat="server" Text="Code:" Skin="Default" CssClass="labelDesign"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 5%;">
                        <asp:Label runat="server" ID="lblContentCode" Visible="true" Font-Size="Small"
                            Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 1%"></td>
                    <td colspan="1" style="width: 5%; text-align: left;">
                        <asp:Label ID="lblFullName" runat="server" Text="Nom :"
                            Skin="Default" CssClass="labelDesign"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 14%">
                        <asp:Label runat="server" ID="lblContentFullName" Visible="true" Font-Size="Small"
                            Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 1%"></td>

                    <td colspan="1" style="width: 6%; text-align: left;">
                        <asp:Label ID="lbl_Classroom" runat="server" Text="Classe :" Skin="Default" CssClass="labelDesign"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 19%">
                        <asp:Label runat="server" ID="lblContentClassName" Visible="true" Font-Size="Small"
                            Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 1%"></td>
                    <td colspan="1" style="width: 7%; text-align: left;">
                        <asp:Label ID="Label1" runat="server" Text="Privillege :" Skin="Default" CssClass="labelDesign"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 10%">
                        <asp:Label runat="server" ID="lblContenPrivilege" Visible="true"
                            Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 1%"></td>
                    <td colspan="1" style="width: 3%; text-align: left;">
                        <asp:Label ID="lblanne" runat="server" Text="Annee:" Skin="Default" CssClass="labelDesign"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 9%;">
                        <asp:Label runat="server" ID="lblcontentAnne" Visible="true" Font-Size="Small"
                            Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                    </td>

                </tr>
            </table>
            <br />
            <asp:Panel ID="pnleffectuer_payment" runat="server" GroupingText="Effectuer Paiement " Visible="true" Width="100%">
                <table border="0" style="width: 97%;" align="center">

                    <%-- ***************************** INSCRIPTION *********************--%>
                    <tr style="height: 40px;">
                        <td colspan="1" style="width: 4%; text-align: left;">
                            <asp:Label ID="lblinscription" runat="server" Text="Insccription :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 11%;">
                            <telerik:RadNumericTextBox ID="txtInscriptionAmountPredefined" runat="server" Enabled="false"
                                Font-Bold="true" Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50" Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 12%; text-align: left;">
                            <asp:Label ID="lbl2" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            <asp:Label ID="lblInscriptionamountalreadyPaid" runat="server" Text="0" Visible="false" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>

                        <td colspan="1" style="width: 19%">
                            <telerik:RadNumericTextBox ID="txtInscriptionAmountPaid" runat="server"
                                Font-Bold="true" Enabled="true"
                                Width="100%" EmptyMessage="0.00" ForeColor="Black" MaxLength="50"
                                CausesValidation="false" AutoPostBack="true" OnTextChanged="txtInscriptionAmountPaid_TextChanged" Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>

                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 6%; text-align: left;">
                            <asp:Label ID="Label5" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadNumericTextBox ID="txtInscriptionBalance" runat="server" Enabled="false"
                                Font-Bold="true" Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="width: 1%">
                            <asp:ImageButton runat="server" ID="checkinscription" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>


                    </tr>

                    <%-- ***************************** FRAIS D'ENTREE *********************--%>
                    <tr style="height: 40px;">
                        <td colspan="1" style="width: 4%; text-align: left;">
                            <asp:Label ID="lblcomming" runat="server" Text="Entree :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            <asp:Label ID="lblEntreeFeeamountalreadyPaid" runat="server" Text="0" Visible="false" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 11%;">
                            <telerik:RadNumericTextBox ID="txtFraisEntreeAmountPredefined" runat="server" Enabled="false"
                                Font-Bold="true"
                                Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 10%; text-align: left;">
                            <asp:Label ID="Label11" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 19%">
                            <telerik:RadNumericTextBox ID="txtFraisEntreeAmountPaid" runat="server"
                                Font-Bold="true" Enabled="false" Width="100%" EmptyMessage="0.00" ForeColor="Black" MaxLength="50"
                                CausesValidation="false" AutoPostBack="true" OnTextChanged="txtEntreeAmountPaid_TextChanged" Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>


                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 6%; text-align: left;">
                            <asp:Label ID="Label13" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadNumericTextBox ID="txtFraisEntreeBalance" runat="server" Enabled="false"
                                Font-Bold="true" Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                            </telerik:RadNumericTextBox>
                        </td>


                        <td colspan="1" style="width: 4%">
                            <asp:ImageButton runat="server" ID="checkEntree" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                    </tr>
                </table>

                <asp:Panel ID="pnl_versement" runat="server" Visible="true" GroupingText="VERSEMENT " Width="100%">
                    <table border="0" style="width: 100%;">

                        <%-- ***************************** VERSEMENT 1 *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="lblVersment_1" runat="server" Text="Versement 1 :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="rr" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtVersment_1" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label15" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label16" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox4" runat="server"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label17" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox5" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>

                        <%-- ***************************** VERSEMENT 2 *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label18" runat="server" Text="Versement 2 :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="l" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtVersment_2" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label20" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label21" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox7" runat="server"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>


                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label22" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox8" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>

                        <%-- ***************************** VERSEMENT 3 *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label23" runat="server" Text="Versement 3 :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label24" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtVersment_3" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label25" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label26" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtVersement3" runat="server"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label27" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox11" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>

                        <%-- ***************************** VERSEMENT 4 *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label28" runat="server" Text="Versement 4 :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label29" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtVersment_4" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label30" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label31" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtVersement4" runat="server"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label32" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="RadNumericTextBox14" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnl_mensuel" runat="server" GroupingText="MENSUEL" Visible="false" Width="100%">
                    <table border="0" style="width: 100%;" runat="server" id="tab_mensuel">

                        <%-- ***************************** SEPTEMBER *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label33" runat="server" Text="Septembre :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 11%;">
                                <telerik:RadNumericTextBox ID="txtSeptemberAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="6"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%">
                                <asp:Label runat="server" ID="lblEntreeSeptemberalreadyPaid" Visible="false"></asp:Label></td>
                            <td colspan="1" style="width: 13%; text-align: left;">
                                <asp:Label ID="Label35" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>

                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtSeptemberAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtSeptemberAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label37" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtSeptemberBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkseptember" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** OCTOBER *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label38" runat="server" Text="Octobre :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtOctoberAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%">
                                <asp:Label runat="server" ID="lblAmountPaid" Visible="false"></asp:Label></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label40" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtOctoberAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtOctoberAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label42" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtOctoberBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkoctober" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** NOVEMBER *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label43" runat="server" Text="Novembre :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtNovemberAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label45" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtNovemberAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtNovemberAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label47" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtNovemberBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checknovember" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** DECEMBER *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label48" runat="server" Text="Decembre :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtDecemberAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label50" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtDecemberAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtDecemberAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label52" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtDecemberBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkdecember" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** JANUARY *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label53" runat="server" Text="Janvier :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtJanuaryAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label55" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtJanuaryAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtJanuaryAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label57" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtJanuaryBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkjanuary" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** FEBUARY *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label58" runat="server" Text="Fevrier :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtFebuaryAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label60" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtFebuaryAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtFebuaryAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label62" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtFebuaryBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkfebruary" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** MARCH *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label63" runat="server" Text="Mars :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtMarchAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label65" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                                <asp:Label ID="Label66" runat="server" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtMarchAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtMarchAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label67" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtMarchBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkmarch" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** APRIL *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label68" runat="server" Text="Avril :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtAprilAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label70" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtAprilAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtAprilAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label72" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>

                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtAprilBalance" runat="server" Enabled="false"
                                    Font-Bold="true" Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkapril" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** MAY *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label73" runat="server" Text="Mai :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtMayAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label75" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtMayAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtMayAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label77" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtMayBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkmay" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>

                        <%-- ***************************** JUNE *********************--%>
                        <tr style="height: 40px;">
                            <td colspan="1" style="width: 8%; text-align: left;">
                                <asp:Label ID="Label78" runat="server" Text="Juin :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 9%;">
                                <telerik:RadNumericTextBox ID="txtJuneAmountPredefined" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Blue" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 10%; text-align: left;">
                                <asp:Label ID="Label80" runat="server" Text="Montant Paye :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 20%">
                                <telerik:RadNumericTextBox ID="txtJuneAmountPaid" runat="server"
                                    Font-Bold="true" Enabled="false" OnTextChanged="txtJuneAmountPaid_TextChanged"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 1%"></td>
                            <td colspan="1" style="width: 6%; text-align: left;">
                                <asp:Label ID="Label82" runat="server" Text="Balance :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            </td>
                            <td colspan="1" style="width: 10%">
                                <telerik:RadNumericTextBox ID="txtJuneBalance" runat="server" Enabled="false"
                                    Font-Bold="true"
                                    Width="100%" EmptyMessage="0.00" ForeColor="Red" MaxLength="50"
                                    Skin="Bootstrap" DisabledStyle-HorizontalAlign="Left">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="1" style="width: 4%">
                                <asp:ImageButton runat="server" ID="checkjune" Visible="false" ImageUrl="~/images/check.png" Width="30px" Height="20px" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <table border="0" style="width: 100%;">
                    <tr style="height: 20px;">
                        <%--  <td colspan="3">
                           <asp:Label ID="lblMoreMonth" runat="server" Visible="true" ForeColor="red" Text="Plus Mois :"
                               Skin="Default" CssClass="labelDesign"></asp:Label>
                       </td>
                       <td>
                           <asp:ImageButton runat="server" ID="Img_BAck"
                               OnClick="Img_BAck_Click"
                               ImageUrl="~/images/left-arrow.png" Width="30px"
                               Height="15px" />
                       <td>
                           <asp:ImageButton runat="server" ID="IMG_NEXT"
                               OnClientClick="IMG_NEXT_" OnClick="IMG_NEXT_Click"
                               ImageUrl="~/images/right-arrow.png" Width="30px"
                               Height="15px" /></td>--%>

                        <td style="width: 80%;" align="center">

                            <asp:Label runat="server" ID="lblErrorAmount" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="red" Text="Erreur : Le montant est incorrect ! "></asp:Label>
                        </td>

                    </tr>
                </table>
            </asp:Panel>


            <asp:Panel ID="pnlResult" runat="server" GroupingText="" Visible="false">
                <div style="width: 100%">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>

                    <asp:GridView ID="gridListPaymentType" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListPaymentScolarite_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" OnRowDataBound="gridListPaymentScolarite_RowDataBound"
                        OnRowDeleting="gridListPaymentScolarite_RowDeleting">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" Visible="true">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="payment_type" HeaderText="Description" ControlStyle-Font-Bold="true">
                                <HeaderStyle Width="500px" HorizontalAlign="Left" />
                                <ItemStyle Width="500px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="transaction_date" HeaderText="Date" ControlStyle-Font-Bold="true">
                                <HeaderStyle Width="390px" HorizontalAlign="Right" />
                                <ItemStyle Width="390px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="login_user" HeaderText="Utilisateur" ControlStyle-Font-Bold="true">
                                <HeaderStyle Width="390px" HorizontalAlign="Right" />
                                <ItemStyle Width="390px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount_paid" HeaderText="Montant" ControlStyle-Font-Bold="true">
                                <HeaderStyle Width="390px" HorizontalAlign="Right" />
                                <ItemStyle Width="390px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentTypeId" Visible="false" runat="server" Text='<%# Eval("id").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="0px" BorderStyle="None" />
                                <ItemStyle BorderStyle="None" Width="0px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg"
                                        OnClientClick="Confirm()" OnClick="removepaymentScolarite"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" BorderStyle="None" />
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
                <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small"
                    Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                <span style="float: right; border: 0px solid red; text-align: left; margin-right: 20px;">
                    <asp:Label runat="server" ID="lblTotalAmount" Visible="false" Font-Size="Small"
                        Font-Bold="true" Font-Names="sans-serif" ForeColor="Black" Text="Montant Total :"></asp:Label>
                </span>
            </asp:Panel>




            <table border="0" align="center" style="margin-top: 10px;">
                <tr>
                    <td colspan="1">
                        <telerik:RadButton ID="btnCancel" runat="server" CausesValidation="true" Visible="true"
                            Text="Annuler" Width="100px" OnClick="btnCancel_Click" Skin="Glow">
                        </telerik:RadButton>
                    </td>
                    <td colspan="30"></td>
                    <td colspan="1">
                        <telerik:RadButton ID="btnModify" runat="server" CausesValidation="true" Visible="true"
                            Text="Historique" Width="100px" OnClick="btnModify_Click" Skin="Glow">
                        </telerik:RadButton>
                    </td>
                    <td colspan="30"></td>
                    <td colspan="1">
                        <telerik:RadButton ID="btSavePayment" runat="server" CausesValidation="true" Visible="true"
                            Text="Valider" Width="100px" OnClick="btSavePayment_Click" Skin="Glow">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>

                    <td colspan="1">
                        <telerik:RadButton ID="btnBack" runat="server" CausesValidation="true" Visible="false"
                            Text="Retour" Width="100px" OnClick="btBack_Click" Skin="Glow">
                        </telerik:RadButton>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
