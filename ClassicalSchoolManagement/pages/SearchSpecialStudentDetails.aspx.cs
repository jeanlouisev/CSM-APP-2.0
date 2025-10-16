using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Linq;

using Utilities;




public partial class SearchSpecialStudentDetails : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            loadActiveClassroom();
            loadListAcademicYear(ddlAcademicYear);
            BindDataGridStudent();

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        /*check if any privillege  was affected to students

           List<Payments> listSpecialpayment = Payments.getListAffectedSpecialPaymentTostudent();

           if (listSpecialpayment.Count > 0)
           {
               foreach (var Payments in listSpecialpayment)
               {
                   foreach (GridViewRow row1 in gridListStudent.Rows)//Running all lines of grid
                   {
                       int index1 = row1.RowIndex;
                       string student = gridListStudent.DataKeys[index1].Value.ToString();
                       if (student.ToUpper() == Payments.staff_code.ToString().ToUpper())
                       {
                           if (row1.RowType == DataControlRowType.DataRow)
                           {
                               //CheckBox chkRow = (row1.Cells[8].FindControl("chkRow") as CheckBox);
                               //chkRow.Checked = true;
                               //DropDownList ddl = (row1.Cells[7].FindControl("ddlspecial") as DropDownList);
                               //ddl.Items.Add(Payments.payment_type_definition.ToString());
                           }


                            //check if any course was affected to the current Classroom
                   List<Course> listCourse = Course.getListAffectedCourseByClassroomCode(Convert.ToInt32(classroomId.ToString()));

                   if (listCourse.Count > 0)
                   {
                       foreach (var course in listCourse)
                       {
                           foreach (GridViewRow row1 in gridListCourse.Rows)//Running all lines of grid
                           {
                               int index1 = row1.RowIndex;
                               string _courseId = gridListCourse.DataKeys[index1].Value.ToString();
                               if (_courseId.ToUpper() == course.id_course.ToString().ToUpper())
                               {
                                   if (row1.RowType == DataControlRowType.DataRow)
                                   {
                                       CheckBox chkRow = (row1.Cells[3].FindControl("chkRow") as CheckBox);
                                       chkRow.Checked = true;

                                       DropDownList ddl = (row1.Cells[2].FindControl("ddlprice") as DropDownList);
                                       ddl.SelectedValue = course.id_course_price.ToString();

                                   }
                       }
                   }
               }
           }*/

        BindDataGridStudent();

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        BindDataGridStudent();
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

    private void loadActiveClassroom()
    {
        try
        {
            List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
            ddlClassroom.DataSource = listClassroom;
            ddlClassroom.DataBind();
            ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlClassroom.SelectedValue = "-1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDataGridStudent()
    {
        try
        {
            Student st = new Student();
            //get the fields from the form        
            st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
            st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
            st.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
            st.marital_status = ddlMaritalStatus.SelectedValue.ToString() == "-1" ? null : ddlMaritalStatus.SelectedValue;
            st.vacation = ddlVacation.SelectedValue.ToString() == "-1" ? null : ddlVacation.SelectedValue;
            st.class_id = int.Parse(ddlClassroom.SelectedValue);
            st.status = int.Parse(ddlActualStatus.SelectedValue);
            st.academic_year = int.Parse(ddlAcademicYear.SelectedValue);

            //get list of students
            List<Student> listResult = Student.getListStudent(st);

            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
            }
            gridListStudent.DataSource = listResult;
            gridListStudent.DataBind();

            foreach (GridViewRow row in gridListStudent.Rows)//Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string studentCode = row.Cells[1].Text.Trim();
                    DropDownList ddlspecial = (row.Cells[7].FindControl("ddlspecial") as DropDownList);
                    if (listResult != null && listResult.Count > 0)
                    {
                        foreach (Student student in listResult)
                        {
                            if (studentCode == student.id)
                            {
                                ddlspecial.SelectedValue = student.id_Special_payment.ToString();
                            }
                        }
                    }


                    HiddenField hiddenAcademicYear = row.Cells[5].FindControl("hiddenAcademicYearId") as HiddenField;
                    if (ddlAcademicYear.SelectedValue.Length > 0)
                    {
                        hiddenAcademicYear.Value = ddlAcademicYear.SelectedValue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void gridListStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListStudent.PageIndex = e.NewPageIndex;
        BindDataGridStudent();
    }

    protected void gridListStudent_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListStudent.Rows[index];
        String studentCode = row.Cells[1].Text;

        // If multiple buttons are used in a GridView control, use the
        // CommandName property to determine which button was clicked.
        if (e.CommandName == "ViewStudentDetails")
        {
            Session["user_code"] = studentCode;
            string page_url = "StudentDetailsInformation.aspx";
            try
            {
                //Response.Redirect("DocumentDetail.aspx");
                //Session["type_detail"] = "endedit";
                //mp1.Show();
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(1100, 500);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        if (e.CommandName == "EditStudent")
        {
            try
            {
                /*
                //load list of classroom available
                loadActiveClassroom(ddlClassroomStudent);

                MasterPanel1.Visible = true;
                pnlResult.Visible = false;
                pnlSearchStudent.Visible = false;

                List<Student> mylist = Student.getListStudentById(studentCode);
                //get student data
                txtCodeStudent.Text = mylist[0].id;
                txtFirstNameStudent.Text = mylist[0].first_name;
                ddlSexStudent.SelectedText = mylist[0].sex;
                txtCardIdStudent.Text = mylist[0].id_card;
                txtBirthPlaceStudent.Text = mylist[0].birth_place;
                txtPhone1Student.Text = mylist[0].phone1;
                txtLastNameStudent.Text = mylist[0].last_name;
                ddlMaritalStatusStudent.SelectedText = mylist[0].marital_status;
                radBirthDateStudent.SelectedDate = mylist[0].birth_date;
                txtAddressStudent.Text = mylist[0].address;
                ddlVacationStudent.SelectedText = mylist[0].vacation;
                txtEmailStudent.Text = mylist[0].email;
                imageKeeper.ImageUrl = "~/images/image_data/" + mylist[0].imagePath;
                ddlClassroomStudent.SelectedValue = mylist[0].classroom;
                Session["user_code"] = mylist[0].id;
                Session["fullname"] = mylist[0].last_name + " " + mylist[0].first_name;
                //get list of available documents
                List<Documents> documentList = Documents.getListDocumentsByStaffCode(txtCodeStudent.Text.Trim());
                lnkStudentDocumentsCounter.Text = documentList.Count + " documents trouve(e)";
                */
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
    }

    protected void gridListStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                //   e.Row.Style.Add("height", "50px");
                e.Row.Style.Add("vertical-align", "bottom");
        }
    }

    public void pickUpStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;

            //get row index
            int index = Convert.ToInt32(imageButton.CommandArgument);
            GridViewRow row = gridListStudent.Rows[index];
            Session["student_code"] = row.Cells[1].Text;

            //Response.Redirect("Payment.aspx");

            // call javaScript to close the window from code behind
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseDialog();", true);

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    protected void lnkStudentDocumentsCounter_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    protected void btnAffecteSpecialPayment_click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gridListStudent.Rows)//Running all lines of grid
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int index = row.RowIndex;
                DropDownList ddlspecial = (row.Cells[7].FindControl("ddlspecial") as DropDownList);
                HiddenField hiddenClasseId = row.Cells[5].FindControl("hiddenClassroomCode") as HiddenField;
                HiddenField hiddenAcademicYear = row.Cells[5].FindControl("hiddenAcademicYearId") as HiddenField;
                string id = gridListStudent.Rows[index].Cells[1].Text;
                int id_Special_payment = Convert.ToInt32(ddlspecial.SelectedValue);
                int academic_year = hiddenAcademicYear.Value.Length <= 0 ? 0 : int.Parse(hiddenAcademicYear.Value);
                int classroom_id = hiddenClasseId.Value.Length <= 0 ? 0 : int.Parse(hiddenClasseId.Value);
                //
                Payments.deletePreviouslyAffectedStudentsSpecialPayment(id, academic_year, classroom_id);
                //
                if (ddlspecial.SelectedValue != "-1")
                {
                    Payments.AffectedSpecialPaymentTostudent(id, id_Special_payment, academic_year, classroom_id);
                }
            }
        }


        lblErrorcoursEmpty.Text = "privilège(s) affecté(s) avec succès !";
        lblErrorcoursEmpty.ForeColor = System.Drawing.Color.Green;
        lblErrorcoursEmpty.Visible = true;

    }

    protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = int.Parse(ddlClassroom.SelectedValue);
                List<ClassRoom> listClass = ClassRoom.getListVacationByClassroomId(classId);
                loadListVacation(listClass);
            }
            else
            {
                ddlVacation.Items.Clear();
                ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
                ddlVacation.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadListVacation(List<ClassRoom> listClassroom)
    {
        //clear items
        ddlVacation.Items.Clear();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            // fill the ddl now
            ddlVacation.DataValueField = "vacation_type";
            ddlVacation.DataTextField = "vacation";
            ddlVacation.DataSource = listClassroom;
            ddlVacation.DataBind();
        }
        ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlVacation.SelectedValue = "-1";
    }

    protected void ddlspecialMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlspecialMaster = gridListStudent.HeaderRow.Cells[7].FindControl("ddlspecialMaster") as DropDownList;

        if (gridListStudent.Visible && gridListStudent.Rows.Count > 0)
        {
            foreach (GridViewRow row in gridListStudent.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlspecial = (row.Cells[7].FindControl("ddlspecial") as DropDownList);
                    //
                    ddlspecial.SelectedValue = ddlspecialMaster.SelectedValue;
                }
            }
        }
    }


}
