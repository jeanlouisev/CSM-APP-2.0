<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AveragePreviousConfigurationDetails.aspx.cs" Inherits="AveragePreviousConfigurationDetails" %>

<%--@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            overflow: hidden;
            z-index: 10;
            top: expression(<%= gridListAverage.HeaderRow %>.offsetParent.scrollTop-2);
        }

        .labelDesign {
            font-size: small;
            --font-weight: bold;
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


        .lower {
            text-transform: lowercase;
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
    </script>

    <script type="text/javascript">

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 600px; border: 0px solid red; margin: auto;">
            <asp:Panel ID="pnlResult" runat="server" GroupingText="Liste des configurations precedentes" CssClass="panellDesign">
                <div runat="server" id="divGridHeader" class="divGridHeader">
                    <table border="0" runat="server" class="tblGridHeader" id="tblGridHeader" style="text-align: left;">
                        <tr>
                            <td style="width: 40px; font-weight: bold; text-align: center;">No</td>
                            <td style="width: 150px; font-weight: bold; text-align: center;">ANNE ACADEMIQUE</td>
                            <td style="width: 190px; font-weight: bold; text-align: center;">NOM COMPLET</td>
                            <td style="width: 90px; font-weight: bold; text-align: center;">DATE</td>
                            <td style="width: 70px; font-weight: bold; text-align: center;">AFFECTER</td>
                        </tr>
                    </table>
                </div>
                <div style="overflow: scroll; max-height: 270px; overflow-x: hidden; height: auto; max-width: 980px; border: 0px solid red;">
                    <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                    <asp:GridView ID="gridListAverage" runat="server" AutoGenerateColumns="False"
                        Style="overflow: auto;" CellPadding="2"
                        ForeColor="Black" BorderWidth="1px" DataKeyNames="academic_year"
                        AllowPaging="false" Width="100%" ShowHeader="false"
                        OnRowCommand="gridListAverage_RowCommand"
                        BackColor="LightGoldenrodYellow" BorderColor="Tan"
                        GridLines="Both" HeaderStyle-CssClass="FixedHeader"
                        OnRowDataBound="gridListAverage_RowDataBound">
                        <RowStyle Height="10px" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="academic_year_start" Visible="true">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="academic_year_end" Visible="true">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="login_user" Visible="true">
                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="400px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date_register" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ToolTip="Charger Configuration" ID="btnAddCours"
                                        ImageUrl="~/images/AddNewitem.png"
                                        CommandName="loadConfiguration" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Navy" Font-Bold="True" Height="22px" HorizontalAlign="Left"
                            ForeColor="WhiteSmoke" BorderColor="Navy" VerticalAlign="Top" BorderWidth="2px" Width="960px" Font-Size="Small" />
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
