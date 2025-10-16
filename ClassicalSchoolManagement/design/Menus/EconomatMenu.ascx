<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EconomatMenu.ascx.cs"
    Inherits="ClassicalSchoolManagement.design.Menus.EconomatMenu" %>


<asp:Panel runat="server" GroupingText="Gestion Payroll" Font-Size="15px">
    <table border="0" style="width: 100%;" class="xxDesign">
        <tr>
            <td>
                <asp:LinkButton ID="lknPayrollPersonal" Width="100%" runat="server" Font-Underline="false"
                    Text="Payroll Personnel" OnClick="lknPayrollPersonal_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkPayrollTeacher" Width="100%" runat="server" Font-Underline="false"
                    Text="Payroll Professeur" OnClick="lnkPayrollTeacher_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkSalaryPersonalConfiguration" Width="100%" runat="server" Font-Underline="false"
                    Text="Configurer Salaire du Personnel" OnClick="lnkSalaryPersonalConfiguration_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkSalaryClassConfiguration" Width="100%" runat="server" Font-Underline="false"
                    Text="Configurer Salaire par Classe" OnClick="lnkSalaryClassConfiguration_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel runat="server" GroupingText="Gestion Paiement" Font-Size="15px">
    <table border="0" style="width: 100%;" class="xxDesign">
        <tr>
            <td>
                <asp:LinkButton ID="lknPayment" Width="100%" runat="server" Font-Underline="false"
                    Text="Gestion Paiement" PostBackUrl="~/pages/Payment.aspx"
                    OnClick="lknPayment_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lknSearchPayment" Width="100%" runat="server" Font-Underline="false"
                    Text="Rechercher Paiement" PostBackUrl="~/pages/SearchPayment.aspx"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lknPaymentType" Width="100%" runat="server" Font-Underline="false"
                    Text="Type de Paiement" PostBackUrl="~/pages/PaymentType.aspx"
                    OnClick="lknPaymentType_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lknPaymentConfiguration" Width="100%" runat="server" Font-Underline="false"
                    Text="Configurer Scolarite" PostBackUrl="~/pages/PaymentConfiguration.aspx"
                    OnClick="lknPaymentConfiguration_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel runat="server" GroupingText="Gestion Depense" Font-Size="15px">
    <table border="0" style="width: 100%;" class="xxDesign">
        <tr>
            <td>
                <asp:LinkButton ID="lknExpenses" Width="100%" runat="server"
                    Text="Decaisser" PostBackUrl="~/pages/Expenses.aspx" Font-Underline="false"
                    OnClick="lknExpenses_Click"></asp:LinkButton>
            </td>
        </tr>

        <tr>
            <td>
                <asp:LinkButton ID="lknSearchExpense" Width="100%" runat="server" Font-Underline="false"
                    Text="Rechercher Depense" PostBackUrl="~/pages/SearchExpenses.aspx"
                    OnClick="lknSearchExpense_Click"></asp:LinkButton>
            </td>
        </tr>

        <tr>
            <td>
                <asp:LinkButton ID="lknExpenseType" Width="100%" runat="server" Font-Underline="false"
                    Text="Type Depense" PostBackUrl="~/pages/ExpensesType.aspx"
                    OnClick="lknExpenseType_Click"></asp:LinkButton>
            </td>
        </tr>

    </table>
</asp:Panel>
