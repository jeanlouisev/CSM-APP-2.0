<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parents.aspx.cs" EnableEventValidation="false" 
    Inherits="Parents" MasterPageFile="~/master/Master3.Master" %>

<%@ Register TagPrefix="art" TagName="SideBarContainer" Src="~/design/SideMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/design/DefaultHeader.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
   Parent
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>

<asp:Content ID="SideBarContent" ContentPlaceHolderID="SideBarPlaceHolder" runat="Server">
    <art:SideBarContainer runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptIncludePlaceHolder" runat="Server">
    <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <!-- FastClick -->
    <script src="../plugins/fastclick/fastclick.min.js"></script>
    <!-- AdminLTE App -->
    <script src="../dist/js/app.min.js"></script>
    <!-- Sparkline -->
    <script src="../plugins/sparkline/jquery.sparkline.min.js"></script>
    <!-- jvectormap -->
    <script src="../plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="../plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
    <!-- SlimScroll 1.3.0 -->
    <script src="../plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- ChartJS 1.0.1 -->
    <script src="../plugins/chartjs/Chart.min.js"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="../dist/js/demo.js"></script>
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../bootstrap/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../bootstrap/css/ionicons.min.css">
    <!-- jvectormap -->
    <link rel="stylesheet" href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="../plugins/datatables/dataTables.bootstrap.css">
    <!-- daterange picker -->
    <link rel="stylesheet" href="../plugins/daterangepicker/daterangepicker-bs3.css">
    <link rel="stylesheet" href="../plugins/datepicker/css/bootstrap-datepicker3.css">

    
<style type="text/css">
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
    function ConfirmDisable() {
        var confirm_value_disable = document.createElement("INPUT");
        confirm_value_disable.type = "hidden";
        confirm_value_disable.name = "confirm_value_disable";
        if (confirm("Voulez-vous vraiment desactiver cet eleve ?")) {
            confirm_value_disable.value = "Yes";
        } else {
            confirm_value_disable.value = "No";
        }
        document.forms[0].appendChild(confirm_value_disable);
    }

    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Voulez-vous vraiment supprimer cet eleve ?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }

    function UploadFileReference(fileUpload) {
        if (fileUpload.value != '') {
            document.getElementById("<%=btnUploadImageReference.ClientID %>").click();
        }
    }

    function keyPress(sender, args) {
        var text = sender.get_value() + args.get_keyCharacter();
        if (!text.match('^[0-9]+$'))
            args.set_cancel(true);
    }


    function onMouseOver(rowIndex) {
        var gv = document.getElementById("gridListParent");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#c8e4b6";
    }

    function onMouseOut(rowIndex) {
        var gv = document.getElementById("gridListParent");
        var rowElement = gv.rows[rowIndex];
        rowElement.style.backgroundColor = "#fff";
    }

</script>

</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="DynamicContainerContentPlaceHolder" runat="Server">
    
<telerik:RadWindowManager ID="MessageAlert" ShowContentDuringLoad="false" VisibleStatusbar="false"
    ReloadOnShow="true" BackColor="White" runat="server" EnableShadow="true" Skin="Bootstrap" DestroyOnClose="false">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Skin="Bootstrap">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridListParent" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblFound"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="lblCounter"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="gridListParent">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divFormHeaderContainer" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="imageKeeperReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtParentIdCard">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="imageKeeperReference" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtParentFirstName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtParentLastName" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlParentSex" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ddlParentMaritalStatus" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtParentPhone" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtParentAdress" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Bootstrap">
</telerik:RadAjaxLoadingPanel>

<asp:Panel runat="server" ID="pnlResponsiblePeople" GroupingText="Gestion Parent / Personne a contacter" CssClass="panellDesign">
    <hr style="margin-top: 0px; width: 100%;" />
    <div style="width: 100%; border: 0px solid purple; text-align: center;">
        <asp:Label runat="server" Text="error tag " ForeColor="Red" Visible="false" ID="lblError"></asp:Label>
        <asp:Label runat="server" Text="Succes ! " ForeColor="DarkGreen" Visible="false" ID="lblSuccess"></asp:Label>
    </div>
    <div style="width: 100%; border: 0px solid red;">
        <div style="width: 20%; float: left;">
            <table border="0" style="width: 100%;">
                <tr>

                    <td colspan="1" style="border: 0px solid purple; height: 100px;">
                        <telerik:RadImageAndTextTile ID="imageKeeperReference" BackColor="WhiteSmoke" Width="200px" Height="200px"
                            runat="server" ImageHeight="100%" ImageWidth="100%" ImageUrl="/images/image_data/Default.png">
                        </telerik:RadImageAndTextTile>
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td colspan="1" style="text-align: center;">
                        <asp:FileUpload runat="server" ID="imageUploaderReference" Width="80px" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="text-align: center;">
                        <asp:ImageButton ID="btnUploadImageReference" OnClick="btnUploadImageReference_Click" CssClass="hideUploadButton"
                            runat="server" ImageUrl="~/images/uploadButton1.png" Width="0px" Height="0px" />
                    </td>
                </tr>
            </table>
        </div>

        <div style="border: 0px solid red; width: 75%; margin-left: 5%; float: left;" runat="server" id="divFormHeaderContainer">
            <table border="0" style="width: 100%; text-align: left" align="left" class="tableRegisterStudent">
                <tr class="trDesign">
                    <td colspan="1" style="width: 15%;">
                        <asp:Label ID="RadLabel1" runat="server" Text="Nom :" Skin="Bootstrap">
                        </asp:Label>
                    </td>
                    <td colspan="1" style="width: 34%;">
                        <telerik:RadTextBox ID="txtParentFirstName" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                        </telerik:RadTextBox>
                    </td>
                    <td colspan="1" style="width: 2%;"></td>
                    <td colspan="1" style="width: 15%;">
                        <asp:Label ID="RadLabel3" runat="server" Text="Prénom :" Skin="Bootstrap"></asp:Label>
                    </td>
                    <td colspan="1" style="width: 34%">
                        <telerik:RadTextBox ID="txtParentLastName" MaxLength="30" runat="server" Width="100%" Skin="Bootstrap">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr class="trDesign">
                    <td>
                        <asp:Label ID="RadLabel4" runat="server" Text="Téléphone :" Skin="Bootstrap"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtParentPhone" runat="server"
                            Font-Size="Small" Width="100%" Skin="Bootstrap" ForeColor="Black" Type="Number">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="false" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="CIN / NIF :" Skin="Bootstrap"></asp:Label>
                       <%-- <asp:UpdatePanel ID="updCardIdReferenceChk" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:CheckBox runat="server" ID="chkNifReference" Text="NIN"
                                    CausesValidation="false" AutoPostBack="true" Checked="true"
                                    OnCheckedChanged="chkNifReference_CheckedChanged" />
                                <asp:CheckBox runat="server" ID="chkCinReference" Text="CIN"
                                    CausesValidation="false" AutoPostBack="true"
                                    OnCheckedChanged="chkCinReference_CheckedChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtParentIdCard" runat="server"
                            Width="100%" Skin="Bootstrap" EmptyMessageStyle-Font-Italic="true"
                            CausesValidation="false" AutoPostBack="true"
                            OnTextChanged="txtParentIdCard_TextChanged">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr class="trDesign">
                    <td colspan="1" rowspan="2">
                        <asp:Label ID="RadLabel6" runat="server" Text="Adresse :" Skin="Bootstrap"></asp:Label>
                    </td>
                    <td colspan="1" rowspan="2">
                        <telerik:RadTextBox ID="txtParentAdress" TextMode="MultiLine" Height="90px"
                            runat="server" Width="100%" Skin="Bootstrap"></telerik:RadTextBox>
                    </td>
                    <td colspan="1" rowspan="2"></td>
                    <td>
                        <asp:Label ID="RadLabel5" runat="server" Text="Sexe :" Skin="Bootstrap"></asp:Label>
                    </td>
                    <td colspan="1">
                        <telerik:RadDropDownList ID="ddlParentSex" runat="server" Width="100%" Skin="Bootstrap">
                            <Items>
                                <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                <telerik:DropDownListItem Value="M" Text="Masculin" />
                                <telerik:DropDownListItem Value="F" Text="Feminin" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                </tr>
                <tr class="trDesign">
                    <td>
                        <asp:Label ID="RadLabel7" runat="server" Text="État Civil :" Skin="Bootstrap"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDropDownList ID="ddlParentMaritalStatus" runat="server" Width="100%" Skin="Bootstrap">
                            <Items>
                                <telerik:DropDownListItem Value="-1" Text="--Tout Sélectionner--" Selected="true" />
                                <telerik:DropDownListItem Value="C" Text="Célibataire" />
                                <telerik:DropDownListItem Value="M" Text="Marié(e)" />
                                <telerik:DropDownListItem Value="D" Text="Divorcé(e)" />
                                <telerik:DropDownListItem Value="V" Text="Veuf(ve)" />
                                <telerik:DropDownListItem Value="U" Text="Union Libre" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <table border="0" style="width: 100%;">
        <tr class="trDesign">
            <td style="width: 35%;"></td>
            <td colspan="1" style="width: 10%; text-align: center;">
                <telerik:RadButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CausesValidation="true"
                    Text="Rechercher" Width="95%" Skin="Web20">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="width: 10%; text-align: left;">
                <telerik:RadButton ID="btnSave" OnClick="btnSave_Click" runat="server" CausesValidation="true"
                    Text="Sauvegarder" Width="95%" Skin="Web20">
                </telerik:RadButton>
            </td>
            <td colspan="1" style="width: 10%; text-align: left;">
                <telerik:RadButton ID="btnClear" OnClick="btnClear_Click" runat="server" CausesValidation="true"
                    Text="Nettoyer" Width="95%" Skin="Web20">
                </telerik:RadButton>
            </td>
            <td style="width: 35%; text-align: right;">
                <telerik:RadButton ID="btnExportExcel" OnClick="btnExportExcel_Click" runat="server" CausesValidation="true"
                    Text="Exporter en excel" Skin="Default" Font-Bold="true" Visible="false">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnlResult" runat="server" GroupingText="Liste Parent / Personne a contacter" Visible="false" CssClass="panellDesign">
    <hr style="margin-top: 0px; width: 100%;" />
    <div style="overflow: scroll; overflow-x: hidden; height: auto; width: 100%; border: 0px solid red;">
        <asp:Label ID="lblFound" runat="server" Visible="False" ForeColor="Red" Text="PAS DE DONNEES"></asp:Label>
        <asp:GridView ID="gridListParent" runat="server" AutoGenerateColumns="False"
            Style="overflow: auto;" CellPadding="2"
            ForeColor="Black" BorderWidth="1px"
            AllowPaging="true" Width="100%"
            OnRowCommand="gridListParent_RowCommand" DataKeyNames="id"
            BackColor="White" BorderColor="Tan"
            GridLines="Both" OnRowDataBound="gridListParent_RowDataBound">
            <RowStyle Height="10px" Font-Size="Small" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                </asp:TemplateField>
             <%--   <asp:BoundField DataField="code" HeaderText="CODE">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" Font-Size="Smaller" />
                </asp:BoundField>--%>
                <asp:BoundField DataField="first_name" HeaderText="Nom">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" Font-Size="Smaller" />
                </asp:BoundField>
                <asp:BoundField DataField="last_name" HeaderText="Prénom">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" Font-Size="Smaller" />
                </asp:BoundField>
                <asp:BoundField DataField="phone" HeaderText="Téléphone">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Size="Smaller" />
                </asp:BoundField>
                <asp:BoundField DataField="adress" HeaderText="Adresse">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" Font-Size="Smaller" />
                </asp:BoundField>
                <asp:BoundField DataField="id_card" HeaderText="CIN/ NIF">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" Font-Size="Smaller" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Sexe" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Font-Size="Smaller" runat="server" Text='<%# Eval("sex").ToString() == "M" ? "Masculin" : "Feminin" %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="marital_status_definition" HeaderText="État Civil">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Size="Smaller" />
                </asp:BoundField>
            <%--    <asp:BoundField DataField="image_path" HeaderText="IMAGE">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" Font-Size="Smaller" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/search_icon1.png"
                            ToolTip="Visualiser" CommandName="ViewParentDetails"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/Edit.jpg"
                            ToolTip="Modifier" CommandName="editParent" ID="btnEdit"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" Width="25px" />
                </asp:TemplateField>
       <%--         <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/delete.jpeg" ID="deleteParent"
                            OnClientClick="Confirm()" OnClick="removeParent" ToolTip="Supprimer"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Visible='<%# Convert.ToInt32(Eval("status").ToString()) == 1 ? false : true %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" HorizontalAlign="Center" CssClass="gridHeaderDesign" />
                    <ItemStyle HorizontalAlign="Left" Width="25px" />
                </asp:TemplateField>--%>
            </Columns>
            <FooterStyle BackColor="Tan" Height="30px" HorizontalAlign="Center" />
            <HeaderStyle Height="22px" HorizontalAlign="Center"
                Width="960px" CssClass="gridHeaderDesign" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="SkyBlue" ForeColor="WhiteSmoke"
                HorizontalAlign="Center" />
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

</asp:Content>
