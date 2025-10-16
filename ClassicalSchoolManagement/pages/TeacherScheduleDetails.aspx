<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherScheduleDetails.aspx.cs" Inherits="TeacherScheduleDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Horaire | Professeur</title>
    <link rel="stylesheet" type="text/css" href="../css/Style1.css" />
    <style type="text/css">
        .divPhotoContainer {
            float: left;
            width: 150px;
            height: 150px;
            border: 0px solid red;
            -webkit-box-shadow: 0px 0px 30px silver;
            -moz-box-shadow: 0px 0px 30px silver;
            box-shadow: 0px 0px 30px silver;
        }

        .divFullName {
            float: left;
            margin-left: 10px;
            margin-bottom: 0px;
            width: 670px;
            height: 30px;
            margin-top: 0px;
            border: 0px solid purple;
        }

        .divInformation {
            float: left;
            margin-left: 10px;
            width: 790px;
            height: auto;
            border: 0px solid blue;
        }

        .labelDesign {
            color: maroon;
            font-size: 15px;
            font-weight: bold;
        }

        .infoDesign {
            font-size: 15px;
            color: black;
        }

        .fullNameDesign {
            font-size: 20px;
            font-weight: bold;
        }

        .masterPanelDesign {
            border: 0px solid red;
        }

        body {
            background-color: whitesmoke;
        }
    </style>



    <script type="text/javascript">

        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gridListSChedule");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
        }

        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gridListSChedule");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
        }

    </script>



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:Panel ID="pnlinfoteacher" runat="server" GroupingText="" Visible="false" CssClass="panellDesign">
            <table border="0" style="width: 100%">
                <tr>
                    <td colspan="1" style="width: 50%; text-align: right;">Code :</td>
                    <td colspan="1">
                        <asp:Label runat="server" ID="lblTeachCode" ForeColor="green" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="1" style="text-align: right;">Nom :</td>
                    <td colspan="1">
                        <asp:Label runat="server" ID="lblFullName" ForeColor="green" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <telerik:RadButton runat="server" Text="Exporter Vers Excel" Skin="Web20"
                            ID="btnExportExcel" OnClick="btnExportExcel_Click">
                        </telerik:RadButton>
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <asp:Panel ID="pnlresulTeacherSchedule" runat="server" Visible="false" CssClass="panellDesign" GroupingText="Liste Horaire">
            <div style="overflow: scroll; max-height: 269px; overflow-x: hidden; width: 100%; border: 0px solid red;">
                <asp:Label ID="lblScheduleResult" runat="server" Visible="false" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
                <asp:GridView ID="gridListSChedule" runat="server" AutoGenerateColumns="False" DataKeyNames="id_Schedule"
                    Style="overflow: auto;" CellPadding="2" ForeColor="Black" BorderWidth="1px"
                    AllowPaging="false" Width="100%" OnSelectedIndexChanged="OnSelectedIndexChanged"
                    OnRowCommand="gridListSChedule_RowCommand" BackColor="White" BorderColor="Tan"
                    GridLines="Both" OnRowDataBound="gridListSChedule_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" Width="60px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="class_name" HeaderText="CLASSE">
                            <HeaderStyle Width="200px" HorizontalAlign="left" />
                            <ItemStyle Width="200px" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="days" HeaderText="JOUR">
                            <HeaderStyle Width="100px" HorizontalAlign="left" />
                            <ItemStyle Width="100px" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cours_name" HeaderText="COURS">
                            <HeaderStyle Width="150px" HorizontalAlign="left" />
                            <ItemStyle Width="150px" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="start_hour" HeaderText="HEURE DEBUT">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="end_hour" HeaderText="HEURE FIN">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <%--      <asp:BoundField DataField="TOTAL_TIME" HeaderText="TOTAL HEURES">
                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:BoundField>--%>
                    </Columns>
                    <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
                    <HeaderStyle Height="22px" CssClass="gridHeaderDesign" Width="960px" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                        HorizontalAlign="Center" />
                    <RowStyle Height="30px" Font-Size="Small" />
                    <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" BorderColor="Silver"
                        BorderStyle="None" />
                    <SortedAscendingCellStyle BackColor="SkyBlue" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
            </div>
            <asp:Label runat="server" ID="Label2" Visible="false" Font-Size="Small"
                Font-Bold="true" Font-Names="sans-serif" ForeColor="Purple"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>

