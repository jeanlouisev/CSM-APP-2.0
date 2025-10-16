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



public partial class Expenses : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.ECONOMAT;


    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }


        if (!IsPostBack)
        {
            // Session.Remove("staff_code");
            Users user = Session["user"] as Users;
            //
            radFromDate.SelectedDate = DateTime.Now.AddMonths(-3);
            radToDate.SelectedDate = DateTime.Now;
            addPredefinedExpensesType();
            loadListType();
            //txtStaffRequestCode.Text = user.staff_code.ToUpper();
            //txtStaffRequestCode_TextChanged(this, null);
            //
            BindDataGridExpense();
            RadAjaxManager1_AjaxRequest(this, null);
        }
    }

    private void addPredefinedExpensesType()
    {
        // check predefined payment_type existence
        List<Expenditure> list1 = Expenditure.getExpensesTypeByName("AUTRE");
        if (list1.Count <= 0)
        {
            Expenditure p = new Expenditure();
            p.id = -2;
            p.description = "AUTRE";
            p.amount = 0;
            //add new type to table                
            Expenditure.addExpenseType(p);
        }
    }

    private void loadListType()
    {
        List<Expenditure> listExpenses = Expenditure.getAllExpenseType();
        if (listExpenses.Count > 0)
        {
            Expenditure expenditure = new Expenditure();
            ddlType.DataValueField = "id";
            ddlType.DataTextField = "description";
            ddlType.DataSource = listExpenses;
            ddlType.DataBind();
            //ddlType.Items.Insert(0, new DropDownListItem("AUTRE", "-2"));
            ddlType.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlType.SelectedValue = "-1";

        }
    }

    protected void btnAddExpenses_Click(object sender, EventArgs e)
    {
        try
        {
            if (validateFields())
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Error.aspx");
                }

                Users user = Session["user"] as Users;
                Expenditure expense = new Expenditure();
                //get the values from the form
                expense.type = int.Parse(ddlType.SelectedValue);
                expense.details = txtDetails.Text.Trim();
                expense.amount = double.Parse(txtAmount.Text.Trim());
                expense.staff_request_code = txtStaffRequestCode.Text.Trim();
                expense.staff_received = user.staff_code.ToUpper();

                // add payment
                if (ddlType.SelectedText == "AUTRE" && txtDetails.Text == "")
                {
                    MessBox.Show("Erreur : Veuillez saisir la decription !");
                }
                else
                {
                    Expenditure.addExepenses(expense);
                    // display confirmation message
                    MessBox.Show("Successful !");
                    // reset the form
                    resetExepenseForm();
                    BindDataGridExpense();
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public bool validateFields()
    {
        bool result = true;
        if (ddlType.SelectedValue == "-1")
        {
            MessBox.Show("Erreur : Veuillez selectionner le type de paiement !");
            result = false;
        }
        else if (txtAmount.Text.Trim().Length <= 0)
        {
            MessBox.Show("Erreur : Veuillez saisir un montant !");
            result = false;
        }
        else if (txtStaffRequestCode.Text.Trim().Length <= 0)
        {
            MessBox.Show("Erreur : Veuillez saisir code ordonnateur !");
            result = false;
        }
        return result;
    }

    public void resetExepenseForm()
    {
        loadListType();
        txtDetails.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtAmount.EmptyMessage = "0.00";
        txtStaffRequestCode.Text = string.Empty;
        txtNomOrdonnateur.Text = string.Empty;
        txtPosition.Text = string.Empty;
    }

    protected void txtStaffRequestCode_TextChanged(object sender, EventArgs e)
    {
        //clear staff status
        //txtStaffStatus.Text = string.Empty;

        if (txtStaffRequestCode.Text.Trim().Length <= 0)
        {
            //MessageAlert.RadAlert("Erreur : veuillez saisir le staff_code !", 300, 150, "Information", null);
            //txtStaffRequestCode.Focus();
            txtNomOrdonnateur.Text = string.Empty;
            txtPosition.Text = string.Empty;
        }
        else
        {
            string staffCode = txtStaffRequestCode.Text.Trim().ToUpper();
            if (staffCode.StartsWith("EL"))  //this is a student
            {
                // check the existence of the code in student table
                List<Student> listStudent = Student.getListStudentByCode(staffCode);
                if (listStudent.Count > 0)
                {
                    //get the fullname of the student
                    txtNomOrdonnateur.Text = listStudent[0].last_name + " " + listStudent[0].first_name;
                    txtPosition.Text = "Eleve";
                }
                else
                {
                    resetStaffCode();
                }
            }
            else if (staffCode.StartsWith("PRO"))  //this is a teacher
            {
                // check the existence of the code in teacher table
                Teacher t = Teacher.getTeacherInfoById(staffCode);
                if (t != null)
                {
                    //get the fullname of the teacher
                    txtNomOrdonnateur.Text = t.last_name + " " + t.first_name;
                    txtPosition.Text = "Professeur";
                }
                else
                {
                    resetStaffCode();
                }
            }
            else if (staffCode.StartsWith("PS"))  //this is a staff
            {
                // check the existence of the code in staff table
                List<Staff> listStaff = Staff.getListStaffById(staffCode);
                if (listStaff != null && listStaff.Count > 0)
                {
                    //get the fullname of the teacher
                    txtNomOrdonnateur.Text = listStaff[0].last_name + " " + listStaff[0].first_name;
                    txtPosition.Text = "Personnel";
                }
                else
                {
                    resetStaffCode();
                }
            }
            else
            {
                resetStaffCode();
            }
        }
    }

    private void resetStaffCode()
    {
        txtStaffRequestCode.Text = string.Empty;
        MessageAlert.RadAlert("Erreur : staff_code est invalide !", 300, 150, "Information", null);

    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        // refresh expense type
        loadListType();

        if (Session["staff_code"] != null)
        {
            txtStaffRequestCode.Text = Session["staff_code"].ToString();
            txtStaffRequestCode_TextChanged(this, null);
        }
        else
        {
            //txtStaffRequestCode_TextChanged(this, null);
            txtStaffRequestCode.Text = string.Empty;
        }
    }

    protected void btnSearchStaff_Click(object sender, ImageClickEventArgs e)
    {
        string page_url = "SearchStaffDetails.aspx";
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

    private void BindDataGridExpense()
    {
        Expenditure expense = new Expenditure();
        expense.type = int.Parse(ddlType.SelectedValue.ToString());
        expense.details = txtDetails.Text.Trim().Length <= 0 ? null : txtDetails.Text.Trim();
        expense.amount = txtAmount.Text.Trim().Length <= 0 ? 0 : double.Parse(txtAmount.Text.Trim());
        expense.staff_request_code = txtStaffRequestCode.Text.Trim().Length <= 0 ? null : txtStaffRequestCode.Text.Trim();
        expense.from_date = radFromDate.SelectedDate.Value;
        expense.to_date = radToDate.SelectedDate.Value;

        //get list of students
        List<Expenditure> listResult = Expenditure.getListExpensesAdvanced(expense);
        if (listResult != null && listResult.Count > 0)
        {
            lblFound.Visible = false;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count + " Ligne(s)";
            lnkExportExcel.Visible = true;
        }
        else
        {
            lblFound.Visible = true;
            lblCounter.Visible = false;
            lnkExportExcel.Visible = false;
        }
        gridListExpenses.DataSource = listResult;
        gridListExpenses.DataBind();
    }

    protected void gridListExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }


    Double totalAmount = 0;
    protected void gridListExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);

            // calculate total amount
            string amount = ((Label)e.Row.FindControl("lblAmount")).Text;
            totalAmount += Double.Parse(amount);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalAmount");
            lbl.Text = totalAmount.ToString();
        }
    }

    protected void removeExpense(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListExpenses.Rows[index];
                string id = gridListExpenses.DataKeys[index].Value.ToString();
                //
                Expenditure.deleteExpenseById(id);
                //refresh data of the gridview
                BindDataGridExpense();
                // confirmation message
                //MessBox.Show("Supprimé avec Succes !");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    //private void loadListLoginUser()
    //{
    //    ddlLoginUser.Items.Clear();
    //    List<Users> listUser = Users.getListUsers();
    //    if (listUser != null && listUser.Count > 0)
    //    {
    //        ddlLoginUser.DataTextField = "fullname";
    //        ddlLoginUser.DataValueField = "id";
    //        ddlLoginUser.DataSource = listUser;
    //        ddlLoginUser.DataBind();
    //    }
    //}

    protected void btnSearchExpense_Click(object sender, EventArgs e)
    {
        BindDataGridExpense();
    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            string userName = user.username;

            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + userName))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + userName);
            }

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\liste_depenses_{1}_{2}.xls",
                userName, userName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            //gridListExpenses.HeaderRow.Visible = true;
            gridListExpenses.AllowPaging = false;
            gridListExpenses.HeaderStyle.Font.Bold = true;
            gridListExpenses.GridLines = GridLines.Vertical;
            BindDataGridExpense();

            //make header visible so that we can have it on the form
            gridListExpenses.HeaderRow.Visible = true;
            gridListExpenses.FooterRow.Visible = false;
            //GetSalaryTable();

            if (gridListExpenses.Visible == true)
            {
                // hide row counter and delete button
                gridListExpenses.HeaderRow.Cells[0].Visible = false;
                gridListExpenses.HeaderRow.Cells[9].Visible = false;

                // define new row heights for the report
                gridListExpenses.RowStyle.Height = 40;

                for (int i = 0; i <= 8; i++)
                {
                    // setup header background color to navy
                    gridListExpenses.HeaderRow.Cells[i].BackColor = Color.Navy;

                    // setup header forecolor to white
                    gridListExpenses.HeaderRow.Cells[i].ForeColor = Color.White;
                }


                for (int i = 0; i < gridListExpenses.Rows.Count; i++)
                {
                    GridViewRow row = gridListExpenses.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[9].Visible = false;

                    Label lblAmount = row.Cells[3].FindControl("lblAmount") as Label;
                    row.Cells[3].Text = Decimal.Parse(lblAmount.Text) + "";


                    row.BackColor = Color.Transparent;
                    //
                    for (int j = 0; j <= 8; j++)
                    {
                        row.Cells[j].BorderWidth = 1;
                    }
                }
                gridListExpenses.RenderControl(htmlWrite);

            }
            else
            {
                return;
            }

            System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();
            //Response.Redirect("nextpage.aspx", false);
            Universal.WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());

            BindDataGridExpense();

        }
        catch (System.Threading.ThreadAbortException lException)
        {
            // do nothing
        }
        catch (Exception ex)
        {

        }
    }
}
