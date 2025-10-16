using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class EconomatMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknPaymentType_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentType.aspx?menu=Economat");
        }

        protected void lknPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payment.aspx?menu=Economat");
        }

        protected void lknPaymentConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentConfiguration.aspx?menu=Economat");
        }

        protected void lknExpenses_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expenses.aspx?menu=Economat");
        }

        protected void lknSearchExpense_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchExpenses.aspx?menu=Economat");
        }

        protected void lknExpenseType_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExpensesType.aspx?menu=Economat");
        }

        protected void lknPayrollPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("PayrollPersonal.aspx?menu=Economat");
        }

        protected void lnkPayrollTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("PayrollTeacher.aspx?menu=Economat");
        }

        protected void lnkSalaryPersonalConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalaryPersonalConfiguration.aspx?menu=Economat");
        }

        protected void lnkSalaryClassConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalaryClassConfiguration.aspx?menu=Economat");
        }
    }
}