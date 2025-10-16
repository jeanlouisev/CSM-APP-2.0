using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using Telerik.Web.UI;
using Telerik.Web;
using Utilities;




public partial class PaymentManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.ECONOMAT;

    DataTable paymentDataTable = new DataTable();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        Session.Remove("student_code");

        if (!IsPostBack)
        {
            //disableFields();
            ViewState["data_table"] = null;
            addPredefinedPaymentType();
            loadListAcademicYear(ddlAcademicYear);
            loadListType();
            BindDataGridPaymentOthers();
        }

        if (Session["student_code"] != null)
        {
            //txtStudentCode.Text = "";
            txtStudentCode.Text = Session["student_code"].ToString();
            txtStudentCode_TextChanged(this, null);
        }
    }

    private void loadListAcademicYear(RadDropDownList ddl)
    {
        try
        {
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();
            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
            }
            else
            {
                ddl.Items.Clear();
                ddl = null;
            }
        }
        catch (Exception ex) { }
    }

    private void loadListAcademicYearForStudent(RadDropDownList ddl)
    {
        try
        {
            string id = txtStudentCode.Text.Trim().ToUpper();
            List<Settings> listAcademicYearForStudent = Settings.getAcademicYearForStudent(id);
            if (listAcademicYearForStudent != null && listAcademicYearForStudent.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYearForStudent;
                ddl.DataBind();
            }
            else
            {
                ddl.Items.Clear();
                ddl = null;
            }
        }
        catch (Exception ex) { }
    }

    private void addPredefinedPaymentType()
    {
        /* check predefined payment_type existence
       // List<Payments> list1 = Payments.getPaymentTypeById(-2);
        if (list1.Count <= 0)
        {
            Payments p = new Payments();
            p.id = -2;
            p.description = "MENSUEL";
            p.amount = 0;
            //add new type to table
            Payments.addPaymentType(p);
        }
        //
       // List<Payments> list2 = Payments.getPaymentTypeById(-3);
        if (list2.Count <= 0)
        {
            Payments p = new Payments();
            p.id = -3;
            p.description = "VERSEMENT";
            p.amount = 0;
            //add new type to table
            Payments.addPaymentType(p);
        }*/
    }

    private void loadListType()
    {
        Payments p = new Payments();
        p.description = null;// txtDescription.Text.Trim().Length <= 0 ? null : txtDescription.Text.Trim().ToLower();
        p.amount = 0;// txtAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtAmount.Text.Trim());
        p.academic_year = int.Parse(ddlAcademicYear.SelectedValue);

        List<Payments> listPayment = Payments.getAllPaymentType(p);
        if (listPayment != null && listPayment.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            ddlType.DataValueField = "id";
            ddlType.DataTextField = "description";
            ddlType.DataSource = listPayment;
            ddlType.DataBind();
        }
        ddlType.Items.Insert(0, new DropDownListItem("SCOLARITE", "-4"));
        ddlType.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlType.SelectedValue = "-1";
    }

    private void disableFields()
    {
        txtDefinedAmount.Text = string.Empty;
        txtPaidAmount.Text = string.Empty;
        txtBalance.Text = string.Empty;
        txtPaidAmount.Enabled = true;
    }

    public void resetPaymentForm()
    {
        loadListType();
        /* ddlMonth.SelectedValue = "-1";
         //uncheck all items
         foreach (ListItem l in listDeposit.Items)
         {
             l.Selected = false;

         }*/
        txtDefinedAmount.Text = string.Empty;
        txtPaidAmount.Text = string.Empty;
        txtBalance.Text = string.Empty;
        txtStudentCode.Text = string.Empty;
        // txtDescription.Text = string.Empty;
        txtStudentFullname.Text = string.Empty;
        txtClassroom.Text = string.Empty;

        //radTransactionDate.SelectedDate = DateTime.Now;
        ddlType.Enabled = false;
        //
        ViewState["data_table"] = null;
        pnlResultOthers.Visible = false;
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (txtStudentCode.Text != "")
        {
            Search_student_payment();
            loadListType();
            Type_payment_prices();
            txtDefinedAmount.Text = "";
        }

    }

    protected void txtStudentCode_TextChanged(object sender, EventArgs e)
    {

        Search_student_payment();
    }

    private void Search_student_payment()
    {
        try
        {
            if (txtStudentCode.Text.Trim().Length <= 0 || txtStudentCode.Text.Trim().ToString() == string.Empty)
            {
                MessageAlert.RadAlert("Erreur : veuillez saisir code eleve !", 300, 150, "Information", null);
                txtStudentCode.Focus();
            }
            else
            {
                //get Id Student
                string id = txtStudentCode.Text.Trim().ToUpper();
                //load list accademic year fro the specific user
                //loadListAcademicYearForStudent(ddlAcademicYear);


                int academic_year = int.Parse(ddlAcademicYear.SelectedValue);
                //search informations
                List<Student> listStudent = Student.getListStudentByIdandAccademic(id, academic_year);
                if (listStudent.Count > 0)
                {

                    //Fill the form 
                    ddlType.Enabled = true;
                    txtStudentFullname.Text = listStudent[0].last_name.ToUpper() + " " + listStudent[0].first_name.ToUpper();
                    int classroom_id = listStudent[0].class_id;
                    int academic_year_id = listStudent[0].academic_year;
                    Session["id_classe"] = classroom_id;
                    Session["academic_year_id"] = academic_year_id;
                    txtClassroom.Text = ClassRoom.getClassroomNameById(classroom_id);
                    switch (listStudent[0].privillege)
                    {
                        case "-2": txtPrivillege.Text = "BOURSE"; break;
                        case "-3": txtPrivillege.Text = "DEMI-BOURSE"; break;
                        default: txtPrivillege.Text = "AUCUN"; break;
                    }
                    txtVacation.Text = listStudent[0].vacation.ToUpper();
                    txtStatus.Text = listStudent[0].status == 1 ? "ACTIVE" : "DESACTIVE";
                    txtSexe.Text = listStudent[0].sex.ToUpper();
                    string vacation_id = listStudent[0].vacation_code;
                    string payment_type = ddlType.SelectedValue;
                }
                else
                {
                    MessageAlert.RadAlert("Code Eleve est introuvable !", 300, 150, "Information", null);
                    resetStaffCode();

                }
            }

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }

    }

    private void resetStaffCode()
    {
        txtStudentFullname.Text = "";
        txtClassroom.Text = "";
        txtVacation.Text = "";
        txtStatus.Text = "";
        txtPrivillege.Text = "";
        txtSexe.Text = "";
        txtStudentCode.Text = "";
        txtDefinedAmount.Text = "";
        txtPaidAmount.Text = "";
        txtBalance.Text = "";
        ddlType.Enabled = false;
        txtStudentCode.Focus();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void ddlType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        Search_student_payment();


        Type_payment_prices();
    }

    public void Type_payment_prices()
    {
        txtPaidAmount.Text = string.Empty;
        txtBalance.Text = string.Empty;
        Session["student_Code"] = txtStudentCode.Text;

        if (ddlType.SelectedValue == "-1" || ddlType.SelectedText == "--Tout Sélectionner--")
        {
            //disableFields();
        }
        else if (ddlType.SelectedValue == "-4" || ddlType.SelectedText == "SCOLARITE")
        {
            //Session["student_Code"] = txtStudentCode.Text;
            Session["student_FullName"] = txtStudentFullname.Text;
            Session["student_Class"] = txtClassroom.Text;
            Session["student_Privillege"] = txtPrivillege.Text;
            Session["student_academic_year"] = ddlAcademicYear.SelectedText;
            string page_url = "Execute_Scolarite.aspx";

            try
            {
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                              + "oWinn.SetSize(1010, 800);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }

            // ddlType.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlType.SelectedValue = "-1";
        }
        else
        {
            disableFields();
            // get the selected value
            int paymentId = int.Parse(ddlType.SelectedValue);
            string staff_code = Session["student_Code"].ToString();
            int academic_year = int.Parse(ddlAcademicYear.SelectedValue);
            List<Payments> listPayment = Payments.getPaymentTypeById(paymentId, staff_code, academic_year);
            if (listPayment.Count > 0)
            {
                txtDefinedAmount.Text = listPayment[0].amount.ToString();
                //Session["id_payment_type"]  = listPayment[0].id_payment_type.ToString();
                txtPaidAmount.Text = string.Empty;
                txtBalance.Text = listPayment[0].balance.ToString();
                double total_already_paid = double.Parse(listPayment[0].amount_paid.ToString());
                double total_should_paid = double.Parse(listPayment[0].amount.ToString());

                if (total_already_paid == total_should_paid)
                {
                    btnAddPayment.Enabled = false;
                    txtPaidAmount.Enabled = false;
                    txtPaidAmount.Text = listPayment[0].amount.ToString();

                }
                else
                {

                    txtPaidAmount.Enabled = true;
                    btnAddPayment.Enabled = true;
                }

            }
        }

    }

    //protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
    //{
    //    Add_payments();
    //}

    private double calculateBalance(double balanceAmount, double paid_amount)
    {
        return balanceAmount - paid_amount;
    }

    protected void btnValidateAddPayment_Click(object sender, EventArgs e)
    {
        Add_payments();
    }

    private void Add_payments()
    {
        if (Convert.ToInt32(txtPaidAmount.Value) <= 0 || txtPaidAmount.Text == "")
        {
            MessageAlert.RadAlert(" Le montant  saisi est incorrect ,Veuillez le saisir a nouveau!", 300, 200, "Information", null);
            txtPaidAmount.Text = "";
            txtPaidAmount.Focus();
        }
        else
        {
            int paymentId = int.Parse(ddlType.SelectedValue);
            string staff_code = txtStudentCode.Text.Trim().Length <= 0 ? string.Empty : txtStudentCode.Text.Trim().ToUpper();
            int academic_year = int.Parse(ddlAcademicYear.SelectedValue);
            List<Payments> listPayment = Payments.getPaymentTypeById(paymentId, staff_code, academic_year);
            int Amount_already_paid = Convert.ToInt32(listPayment[0].amount_paid.ToString());
            int Validate_paid_amount = (Convert.ToInt32(txtPaidAmount.Text) + Amount_already_paid);
            if (Validate_paid_amount > Convert.ToInt32(txtDefinedAmount.Value) || Convert.ToInt32(txtDefinedAmount.Value) <= 0)
            {
                MessageAlert.RadAlert(" Le montant  saisi est incorrect ,Veuillez le saisir a nouveau!", 300, 200, "Information", null);
                txtPaidAmount.Text = "";
                txtPaidAmount.Focus();
            }
            else
            {
                Users user = Session["user"] as Users;
                try
                {
                    Payments p = new Payments();
                    p.staff_code = txtStudentCode.Text.Trim().Length <= 0 ? string.Empty : txtStudentCode.Text.Trim();
                    p.id_payment_type = ddlType.SelectedValue;
                    p.amount_paid = txtPaidAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtPaidAmount.Text.Trim());
                    p.payment_type = "OTHER FEE";
                    p.login_user = user.username.ToUpper();
                    Payments.addPayment(p);
                    //
                    MessageAlert.RadAlert("Paiement ajouter avec Successful !", 300, 200, "Information", null);

                    txtPaidAmount.Text = "";
                    Type_payment_prices();
                    BindDataGridPaymentOthers();
                }
                catch (Exception ex)
                {
                    MessBox.Show("Error : " + ex.Message);
                }
            }
        }
        // reload
        BindDataGridPaymentOthers();
    }

    protected void btnSearchStudent_Click(object sender, ImageClickEventArgs e)
    {
        string page_url = "SearchStudentDetails.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btn_Define_payment_Type_Click(object sender, ImageClickEventArgs e)
    {
        string page_url = "btn_Define_payment_Type.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnDefinePaymentType_Click(object sender, EventArgs e)
    {
        string page_url = "DialogDefinepaymentType.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnScolariteAmountConfige_Click(object sender, EventArgs e)
    {
        string page_url = "DialogScolariteFeeConfiguration.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    //SEARCH PAYMENT AND DISPLAY IN GRIDVIEW
    protected void btnSearchPayment_Click(object sender, EventArgs e)
    {
        BindDataGridPaymentOthers();
    }

    private void BindDataGridPaymentOthers()
    {
        Payments p = new Payments();
        //get Id Student
        p.staff_code = txtStudentCode.Text.Trim().Length <= 0 ? null : txtStudentCode.Text.Trim();
        p.academic_year = ddlAcademicYear.SelectedValue == "-1" ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
        p.id = ddlType.SelectedValue == "-1" ? 0 : int.Parse(ddlType.SelectedValue);
        //search informations
        List<Payments> listResult = Payments.getAllPaymentOthers(p);

        if (listResult != null && listResult.Count > 0)
        {
            lblFound.Visible = false;
            pnlResultOthers.Visible = true;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
            lblTotalAmount.Visible = true;
            double _totalAmount = 0;
            foreach (Payments payment in listResult)
            {
                _totalAmount += payment.amount_paid;
            }
            lblTotalAmount.Text = "Montant Total : " + _totalAmount + " Gdes";
        }
        else
        {
            lblFound.Visible = true;
            lblCounter.Visible = false;
            lblTotalAmount.Visible = false;

        }
        gridListPaymentOthers.DataSource = listResult;
        gridListPaymentOthers.DataBind();
    }

    protected void gridListPaymentOthers_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListPaymentOthers.Rows[index];
        //string code = row.Cells[0].Text;

    }

    protected void gridListPaymentOthers_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                // e.Row.Style.Add("height", "50px");
                e.Row.Style.Add("vertical-align", "bottom");

            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }
    }

    protected void gridListPaymentOthers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  BindDataGriPaymentOthers();
    }

    //private void BindDataGriPaymentOthers()
    //{
    //    Payments p = new Payments();
    //    //p.id_classroom = 120; //int.Parse(Session["id_classe"].ToString());
    //    p.academic_year = 1; //int.Parse(Session["academic_year_id"].ToString());
    //    p.staff_code = "El-7"; //Session["student_Code"].ToString();
    //    List<Payments> listResult = Payments.getAllPaymentOthers(p);


    //    if (listResult != null && listResult.Count > 0)
    //    {

    //        lblFound.Visible = false;

    //        gridListPaymentOthers.Visible = true;
    //        lblCounter.Visible = true;
    //        lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
    //        lblTotalAmount.Visible = true;
    //        double _totalAmount = 0;
    //        foreach (Payments payment in listResult)
    //        {
    //            _totalAmount += payment.amount_paid;
    //        }
    //        lblTotalAmount.Text = "Montant Total : " + _totalAmount + " Gdes";
    //    }
    //    else
    //    {
    //        gridListPaymentOthers.Visible = true;
    //        lblCounter.Visible = false;
    //        lblTotalAmount.Visible = false;


    //    }
    //    gridListPaymentOthers.DataSource = listResult;
    //    gridListPaymentOthers.DataBind();
    //}

    public void removepaymentOthers(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListPaymentOthers.Rows[index];
                Label labelPaymentId = (row.Cells[5].FindControl("lblPaymentTypeId") as Label);
                int payment_type_id = int.Parse(labelPaymentId.Text);
                // first check if this payment is already assigned                    
                Payments.deletePaymentTypeScolarite(payment_type_id);
                BindDataGridPaymentOthers();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

}