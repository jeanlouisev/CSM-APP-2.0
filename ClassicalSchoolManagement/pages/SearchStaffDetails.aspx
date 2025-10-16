<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchStaffDetails.aspx.cs" Inherits="SearchStaffDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search staff Details</title>
    <link rel="icon" href="../images/saphir_logo.jpg">
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <!-- Main CSS-->
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <!-- Font-icon css-->
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <link rel="stylesheet" type="text/css" href="../bootstrap/css/font-awesome.min.css" />

    <style type="text/css">
        #MasterWrapper {
            margin: auto;
            border: 0px solid red;
            width: 960px;
            margin-top: 20px;
        }

        body {
            background-color: whitesmoke;
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
                top.location.href = top.location.href;
            }
        }

        //function refreshParentPage() {
        //    GetRadWindow().BrowserWindo.location.reload();
        //}

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="MasterWrapper">
            <asp:Panel ID="pnlSearchStaff" runat="server" GroupingText="Rechercher Personnel" CssClass="panellDesign">

                <table border="0" style="width: 100%;">
                    <tr style="height: 40px;">
                        <td colspan="1" style="width: 3%; text-align: left;">
                            <asp:Label ID="lblCode" runat="server" Text="Code :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%;">
                            <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 3%; text-align: left;">
                            <asp:Label ID="lblFirst" runat="server" Text="Nom :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadTextBox ID="txtFirst" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 3%; text-align: left;">
                            <asp:Label ID="lblLastName" runat="server" Text="Prenom :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 9%; text-align: left;">
                            <asp:Label ID="RadLabel1" runat="server" Text="Statut Mat. : " Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadDropDownList ID="ddlMaritalStatus" runat="server" Width="100%" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="C" Text="Célibataire" />
                                    <telerik:DropDownListItem Value="M" Text="Marié(e)" />
                                    <telerik:DropDownListItem Value="D" Text="Divorcé(e)" />
                                    <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="text-align: left;">
                            <asp:Label ID="lblFromDate" runat="server" Text="Du :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadDatePicker ID="radFromDate" runat="server" EnableTyping="false" Width="100%" Skin="Bootstrap">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td colspan="1"></td>
                        <td colspan="1" style="text-align: left;">
                            <asp:Label ID="lblToDate" runat="server" Text="Au :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadDatePicker ID="radToDate" runat="server" EnableTyping="false" Width="100%" Skin="Bootstrap">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td colspan="1"></td>
                        <td colspan="1" style="text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="Sexe :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 9%">
                            <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="M" Text="Masculin" />
                                    <telerik:DropDownListItem Value="F" Text="Feminin" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td colspan="1"></td>
                        <td colspan="1" style="text-align: left;">
                            <asp:Label ID="lblposition" runat="server" Text="Position :" Skin="Bootstrap" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1">
                            <telerik:RadDropDownList ID="dllposition" runat="server" Width="100%" Skin="Bootstrap">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="DR" Text="Directeur(trice)" />
                                    <telerik:DropDownListItem Value="SC" Text="Secretaire" />
                                    <telerik:DropDownListItem Value="GD" Text="Gardien(ene)" />
                                    <telerik:DropDownListItem Value="PD" Text="Prefert Dicipline" />
                                    <telerik:DropDownListItem Value="MG" Text="Menager(e)" />
                                    <telerik:DropDownListItem Value="CU" Text="Cuisinier(e)" />
                                    <telerik:DropDownListItem Value="AT" Text="Autre" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                </table>
                <table border="0" align="center" style="margin-top: 9px;">
                    <tr>
                        <td colspan="1">
                            <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                                Text="Rechercher" Width="100px" Skin="Glow">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste Personnels" Visible="False">
                <div style="overflow: scroll; max-height: 270px; overflow-x: hidden; height: auto; max-width: 950px;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListStaff" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListStaff_RowCommand" BackColor="White" BorderColor="Tan"
                        GridLines="Both" DataKeyNames="staff_code" OnRowDataBound="gridListStaff_RowDataBound">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="38px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="38px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="staff_code" HeaderText="CODE">
                                <HeaderStyle Width="101px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="82px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="first_name" HeaderText="NOM">
                                <HeaderStyle Width="110px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="last_name" HeaderText="PRENOM">
                                <HeaderStyle Width="110px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sex" HeaderText="SEXE">
                                <HeaderStyle Width="60px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="marital_status" HeaderText="STATUS MAT.">
                                <HeaderStyle Width="60px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="phone1" HeaderText="TELEPHONE">
                                <HeaderStyle Width="60px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="position" HeaderText="POSITION">
                                <HeaderStyle Width="89px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="89px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="STATUS" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Width="26px" />
                                <ItemStyle HorizontalAlign="center" Width="29px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/images/AddNewitem.jpg"
                                        OnClick="pickUpStaff" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="40px" HorizontalAlign="Center" />
                        <HeaderStyle Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign" Width="940px" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke" HorizontalAlign="Center" />
                        <RowStyle Height="5px" Font-Size="Smaller" />
                        <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                            BorderStyle="None" />
                        <SortedAscendingCellStyle BackColor="SkyBlue" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </div>
                <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
