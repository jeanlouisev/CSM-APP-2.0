<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchStudentDetails.aspx.cs" Inherits="SearchStudentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Liste des élèves</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="MasterWrapper">

<asp:Panel ID="Panel1" runat="server" GroupingText="Rechercher Eleve" CssClass="panellDesign">
    <table border="0" style="width: 100%;">
        <tr class="trDesign">
            <td colspan="1" style="width: 8%; text-align: left;">Code : 
            </td>
            <td colspan="1" style="width: 18%">
                <telerik:RadTextBox ID="txtCode" runat="server"
                    Width="100%" Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1" style="width: 5%; text-align: center;">Nom : 
            </td>
            <td colspan="1" style="width: 18%">
                <telerik:RadTextBox ID="txtFullName" runat="server"
                    Width="100%"
                    Skin="Bootstrap">
                </telerik:RadTextBox>
            </td>
            <td colspan="1" style="width: 10%; text-align: center;">Sexe : 
            </td>
            <td colspan="1" style="width: 15%">
                <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Bootstrap">
                    <Items>
                        <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                        <telerik:DropDownListItem Value="M" Text="Masculin" />
                        <telerik:DropDownListItem Value="F" Text="Feminin" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
            <td colspan="1" style="width: 15%; text-align: center;">Statut Mat.
            </td>
            <td colspan="1">
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
        <tr class="trDesign">
            <td colspan="1" style="text-align: left;">Classe : 
            </td>
            <td colspan="1">
                <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                    Width="100%" Skin="Bootstrap" ExpandDirection="Down"
                    CausesValidation="false" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged">
                    <Items>
                        <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
                    <td colspan="1" style="text-align: center;">
                <asp:Label ID="Label7" runat="server" Text="Annee :" Enabled="false"></asp:Label></td>

            <td colspan="1" style="text-align: center;">
                <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="100%"
                    CausesValidation="false" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                </telerik:RadDropDownList>
            </td>
            <td colspan="1" style="text-align: center;">Vacation : 
            </td>
            <td colspan="1">
                <telerik:RadDropDownList ID="ddlVacation" runat="server" Width="100%" Skin="Bootstrap">
                    <Items>
                        <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                        <telerik:DropDownListItem Value="AM" Text="Matin" />
                        <telerik:DropDownListItem Value="PM" Text="Median" />
                        <telerik:DropDownListItem Value="NG" Text="Soir" />
                        <telerik:DropDownListItem Value="WK" Text="Weekend" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
            <td colspan="1" style="text-align: left; text-align: center;">Statut : 
            </td>
            <td colspan="1">
                <telerik:RadDropDownList ID="ddlActualStatus" runat="server" Width="100%" Skin="Bootstrap">
                    <Items>
                        <telerik:DropDownListItem Value="1" Text="Activé" Selected="true" />
                        <telerik:DropDownListItem Value="0" Text="Desactivé" />
                        <telerik:DropDownListItem Value="-1" Text="-- Tout --" />
                    </Items>
                </telerik:RadDropDownList>
            </td>
        </tr>
    </table>
    <table border="0" style="margin-top: 10px; width: 100%;">
        <tr>
            <td colspan="1" style="width: 45%"></td>
            <td colspan="1" style="width: 10%">
                <telerik:RadButton ID="RadButton1" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                    Text="Rechercher" Width="100%" Skin="Glow">
                </telerik:RadButton>
            </td>
       
        </tr>
    </table>
</asp:Panel>

            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des eleves" Visible="false" Width="100%">
                <div class="divGridHeader" runat="server" id="divGridHeader">
                    <table border="0" class="tblGridHeader">
                        <tr>
                            <td style="width: 55px;">No</td>
                            <td style="width: 80px;">Code</td>
                            <td style="width: 220px;">Nom Eleve</td>
                            <td style="width: 80px; text-align: center;">Sexe</td>
                            <td style="width: 70px; text-align: center;">Statut</td>
                            <td style="width: 180px;">Classe</td>
                            <td style="width: 90px; text-align: center;">Telephone</td>
                            <td style="width: 70px; text-align: center;">Vacation</td>
                        </tr>
                    </table>
                </div>
                <div style="overflow: scroll; max-height: 250px; overflow-x: hidden; height: auto; max-width: 960px;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListStudent" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListStudent_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" HeaderStyle-CssClass="FixedHeader" OnRowDataBound="gridListStudent_RowDataBound">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="55px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="id" HeaderText="Code">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fullName" HeaderText="Nom Eleve">
                                <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="220px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sex" HeaderText="Sexe">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="marital_status" HeaderText="Statut">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="classroom" HeaderText="Classe">
                                <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="phone1" HeaderText="Telephone">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vacation" HeaderText="Vacation">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/AddNewitem.jpg"
                                CommandName="ViewStudentDetails" ItemStyle-CssClass="cursorDesign" Visible="false">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="bottom" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/images/AddNewitem.jpg"
                                        OnClick="pickUpStudent"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Navy" Font-Bold="True" Height="22px" HorizontalAlign="Left"
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
                <span style="text-align: left;">
                    <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
                </span>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
