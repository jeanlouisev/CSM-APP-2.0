<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AffectAmountHistoric.aspx.cs" Inherits="AffectAmountHistoric" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Liste des Montant affectes</title>
    <link rel="icon" href="../images/saphir_logo.jpg">
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" align="center">
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lblFullname"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lblStaffCode"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lblPosition"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />


            <asp:Panel ID="pnlResult" runat="server" GroupingText=" " Visible="False" CssClass="panellDesign">
                <div runat="server" id="divGridHeader" class="divGridHeader">
                    <table border="0" runat="server" class="tblGridHeader" id="tblGridHeader" style="text-align: left;">
                        <tr>
                            <td style="width: 60px; font-weight: bold; text-align: center;">No</td>
                            <td style="width: 170px; font-weight: bold; text-align: center;">MONTANT</td>
                            <td style="width: 160px; font-weight: bold; text-align: center;">DATE</td>
                        </tr>
                    </table>
                </div>
                <div style="overflow: scroll; max-height: 240px; overflow-x: hidden; height: auto; max-width: 970px;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListStaff" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                        Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px" AllowPaging="false" Width="100%"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" HeaderStyle-CssClass="FixedHeader">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="30px" HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="amount" HeaderText="MONTANT" DataFormatString="{0:###,###,###.00}" HtmlEncode="false">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date_register" HeaderText="DATE">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="40px" HorizontalAlign="Center" />
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
                <asp:Label runat="server" ID="lblCounter" Visible="false" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
            </asp:Panel>

        </div>
    </form>
</body>
</html>
