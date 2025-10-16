<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchSpecialStudentDetails.aspx.cs" Inherits="SearchSpecialStudentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affecter privilège a élève</title>
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
            <asp:Panel ID="pnlSearchStudent" runat="server" GroupingText="Rechercher Elève" Width="100%">
                <table border="0" style="width: 100%;">
                    <tr style="height: 40px;">
                        <td colspan="1" style="width: 4%; text-align: left;">
                            <asp:Label ID="lblCode" runat="server" Text="Code :" Skin="Default" CssClass="labelDesign"></asp:Label>
                            <asp:Label ID="lblIdPrivelege" runat="server" Text="" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="4" style="width: 6%;">
                            <telerik:RadTextBox ID="txtCode" runat="server" Width="100%" Skin="Default"></telerik:RadTextBox>
                        </td>
                        <td colspan="1" style="width: 4%; text-align: left;">
                            <asp:Label ID="lblFullName" runat="server" Text="Nom :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="2" style="width: 10%">
                            <telerik:RadTextBox ID="txtFullName" runat="server" Width="98%" Skin="Default"></telerik:RadTextBox>
                        </td>

                        <td colspan="1" style="width: 2%; text-align: left;">
                            <asp:Label ID="lblSex" runat="server" Text="Sexe " Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td> <td colspan="1" style="width: 10%">
                            <telerik:RadDropDownList ID="ddlSex" runat="server" Width="100%" Skin="Bootstrap" CausesValidation="false" >
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                    <telerik:DropDownListItem Value="M" Text="Masculin" />
                                    <telerik:DropDownListItem Value="F" Text="Feminin" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td> <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 6%; text-align: left;">
                            <asp:Label ID="RadLabel1" runat="server" Text="Statut Mat. " Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 7%">
                            <telerik:RadDropDownList ID="ddlMaritalStatus" runat="server" Width="100%" Skin="Bootstrap" CausesValidation="false">
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
                        <td colspan="4">
                            <telerik:RadDropDownList ID="ddlClassroom" runat="server"
                                Width="98%" Skin="Bootstrap" ExpandDirection="Down"
                                CausesValidation="false" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlClassroom_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>


                        <td colspan="1" style="width: 49px">
                            <asp:Label ID="Label5" runat="server" Text="Annee :" Enabled="false"></asp:Label></td>


                        <td colspan="2" style="width: 10%">
                            <telerik:RadDropDownList ID="ddlAcademicYear" Skin="Bootstrap" runat="server" Width="98%"
                                CausesValidation="false" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>

                        <td colspan="1" style="text-align: left; text-align: center;">Status </td>
                        <td colspan="1">
                            <telerik:RadDropDownList ID="ddlActualStatus" Skin="Bootstrap" runat="server" Width="100%" CausesValidation="false">
                                <Items>
                                    <telerik:DropDownListItem Value="1" Text="Activé" Selected="true" />
                                    <telerik:DropDownListItem Value="0" Text="Desactivé" />
                                    <telerik:DropDownListItem Value="-1" Text="-- Tout --" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>

                        <td colspan="1" style="width: 1%"></td>
                        <td colspan="1" style="width: 6%; text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="Vacation :" Skin="Default" CssClass="labelDesign"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 7%;">
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
                    </tr>
                </table>
                <table border="0" align="center" style="margin-top: 10px;">
                    <tr>
                        <td colspan="1">
                            <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                                Text="Rechercher" Width="100px" Skin="Glow">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste élèves" Visible="false" Width="100%">
                <div style="overflow: scroll; max-height: 300px; overflow-x: hidden; height: auto; max-width: 960px;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListStudent" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        OnRowCommand="gridListStudent_RowCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" OnRowDataBound="gridListStudent_RowDataBound">
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
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
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
                            <asp:TemplateField HeaderText="Classe">
                                <HeaderStyle Width="110px" HorizontalAlign="center" BorderColor="White" BorderWidth="0" />
                                <ItemStyle Width="110px" HorizontalAlign="center" BorderWidth="1px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblClassroomName" Text='<%# Eval("classroom") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hiddenClassroomCode" Value='<%# Eval("classroom_id") %>' />
                                    <asp:HiddenField runat="server" ID="hiddenAcademicYearId" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="vacation" HeaderText="Vacation">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Special">
                                <HeaderStyle Width="70px" HorizontalAlign="center" BorderColor="White" BorderWidth="0" />
                                <ItemStyle Width="70px" HorizontalAlign="center" BorderWidth="1px" />
                                <HeaderTemplate>
                                    <asp:DropDownList Width="120px" AutoPostBack="true" HorizontalAlign="center"
                                        ID="ddlspecialMaster" runat="server" OnSelectedIndexChanged="ddlspecialMaster_SelectedIndexChanged">
                                        <Items>
                                            <asp:ListItem Value="-1" Text="--Tout Sélectionner--" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="BOURSE"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="DEMI-BOURSE"></asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList Width="120px" HorizontalAlign="center" ID="ddlspecial" runat="server">
                                        <Items>
                                            <asp:ListItem Value="-1" Text="--Tout Sélectionner--" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="BOURSE"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="DEMI-BOURSE"></asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/AddNewitem.jpg"
                                CommandName="ViewStudentDetails" ItemStyle-CssClass="cursorDesign" Visible="false">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="bottom" />
                            </asp:ButtonField>
                            <%--
                            <asp:BoundField DataField="classroom_id" HeaderText="">
                                <HeaderStyle Width="0px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="0px" />
                            </asp:BoundField>--%>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle Height="22px" HorizontalAlign="Center" CssClass="gridHeaderDesign" />
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
                <table border="0" align="center" style="margin-top: 10px;">
                    <tr>
                        <td colspan="1" align="center">
                            <asp:Label Font-Size="Medium" Visible="true" Width="250px" runat="server" ID="lblErrorcoursEmpty" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" align="center">
                            <telerik:RadButton ID="RadButton1" runat="server" CausesValidation="true"
                                Text="Valider" Width="100px" OnClick="btnAffecteSpecialPayment_click" Skin="Glow">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
