using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class AffectAmountDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }


        if (!IsPostBack)
        {
            hiddenCode.Value = Session["staff_code"].ToString().ToUpper();
            lblFullname.Text = Session["fullname"].ToString().ToUpper();  // + " " + Session["first_name"].ToString().ToUpper();
            lblPosition.Text = Session["position"].ToString();

            List<Salary> listSalary = Salary.getStaffCurrentSalary(hiddenCode.Value.ToUpper());
            if (listSalary.Count > 0 && listSalary != null)
            {
                txtAmount.Value = listSalary[0].amount;
            }
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Visible = false;
        lblSuccess.Visible = false;
        if (txtAmount.Value != null && txtAmount.Text.Trim().Length > 0)
        {
            Users user = Session["user"] as Users;
            Salary s = new Salary();
            s.staff_code = hiddenCode.Value;
            s.amount = Convert.ToDouble(txtAmount.Value);
            s.status = 1; // the amount is the active amount
            s.login_user_id = user.id;

            List<Salary> listCurrentSalary = Salary.getStaffCurrentSalary(s.staff_code);
            if (listCurrentSalary != null && listCurrentSalary.Count > 0)
            {
                if (listCurrentSalary[0].amount == s.amount)
                {
                    lblErrorMsg.Text = "Desole, le montant ne peut etre le meme !";
                    lblErrorMsg.Visible = true;
                }
                else
                {
                    // update already assigned amount
                    Salary.updateExistingSalaryAmountForStaff(s);
                    // add amount
                    Salary.InsertStaffSalaryInfo(s);

                    lblSuccess.Text = "Valider avec succes !";
                    lblSuccess.Visible = true;
                    //}
                    //else
                    //{
                    //    lblErrorMsg.Text = "Desole, le montant ne peut etre ajouter !";
                    //    lblErrorMsg.Visible = true;
                    //}
                }
            }
            else
            {
                // add amount
                Salary.InsertStaffSalaryInfo(s);

                lblSuccess.Text = "Valider avec succes !";
                lblSuccess.Visible = true;
                //}
                //else
                //{
                //    lblErrorMsg.Text = "Desole, le montant ne peut etre ajouter !";
                //    lblErrorMsg.Visible = true;
                //}


                //lblErrorMsg.Text = "Erreur : Veuillez saisir le montant actuel !";
                //lblErrorMsg.Visible = true;
            }
        }
    }
}