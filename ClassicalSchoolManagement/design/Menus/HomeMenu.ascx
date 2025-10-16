<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeMenu.ascx.cs" Inherits="ClassicalSchoolManagement.design.Menus.HomeMenu" %>
<style type="text/css">
    .labelDesign {
        font-size: small;
        --font-weight: bold;
        font-family: sans-serif;
    }

    .FixedHeader {
        position: absolute;
        font-weight: bold;
        overflow: hidden;
        z-index: 10;
        top: expression(<%= gridListBirthday.HeaderRow %>.offsetParent.scrollTop-2);
    }

    .EditClassroomTableCells {
        width: 243px;
    }
</style>


<table border="0" style="width: 100%">
    <tr>
        <td colspan="3">
            <asp:Image ID="Aniversary" runat="server"
                Width="100%" Height="60%" ImageUrl="~/images/birthday.gif" /><br />

        </td>
    </tr>
</table>

<asp:Panel ID="pnlBirthday" runat="server" GroupingText="" Visible="true">
    <div style="overflow: scroll; overflow-x: hidden; max-height: 320px; max-width: 960px;">
        <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red">PAS DE DONNEES</asp:Label>
        <asp:GridView ID="gridListBirthday" runat="server" AutoGenerateColumns="False" Width="100%"
            Style="overflow: auto;" CellPadding="2" ForeColor="Black" OnSelectedIndexChanged="OnSelectedIndexChanged"
            BorderWidth="1px" AllowPaging="false" OnRowCommand="gridListBirthday_RowCommand"
            BackColor="LightGoldenrodYellow" BorderColor="Tan" GridLines="Both"
            PagerSettings-Mode="NumericFirstLast" HeaderStyle-CssClass="FixedHeader"
            OnRowDataBound="gridListBirthday_RowDataBound">
            <RowStyle Height="30px" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="15px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="25px" />
                </asp:TemplateField>
                <asp:BoundField DataField="fullName" HeaderText="Nom"
                    DataFormatString="{0:dd/MM/yyyy}" Visible="true">
                    <HeaderStyle Width="95px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="115px" />
                </asp:BoundField>

                <asp:BoundField DataField="id" HeaderText="Status" Visible="true">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>

                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/SMS.png" HeaderText="SMS"
                    CommandName="AddSchedule" ItemStyle-CssClass="cursorDesign">
                    <HeaderStyle Width="20px" />
                    <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Bottom" BorderWidth="1px" />
                </asp:ButtonField>


                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="labelScheduleId" Visible="false" runat="server" Text='<%# Eval("id").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" BorderStyle="None" />
                    <ItemStyle BorderStyle="None" Width="0px" />
                </asp:TemplateField>


            </Columns>
            <FooterStyle BackColor="Tan" Height="20px" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#3366ff" Font-Bold="True" Height="20px" HorizontalAlign="Left"
                ForeColor="WhiteSmoke" VerticalAlign="Top" BorderWidth="2px" Width="214px" Font-Size="Small" />
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
    <asp:Label runat="server" ID="lblCounter" Visible="true" Font-Size="Small" Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
</asp:Panel>
