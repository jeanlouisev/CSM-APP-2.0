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




public partial class SearchStudentDetails : System.Web.UI.Page
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
            //radFromDate.SelectedDate = DateTime.Today.AddDays(-365);
            //  radToDate.SelectedDate = DateTime.Now;
            loadListAcademicYear(ddlAcademicYear);
            loadActiveClassroom();
            BindDataGridStudent();
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

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        //ddlClassroom_OnSelectedIndexChanged(this, e);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataGridStudent();

    }

    private void loadActiveClassroom()
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ddlClassroom.DataValueField = "id";
        ddlClassroom.DataTextField = "name";
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlClassroom.SelectedValue = "-1";
    }

    protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        /*  try
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
          }*/
    }


    private void BindDataGridStudent()
    {
        try
        {
            Student st = new Student();
            //get the fields from the form
            //st.id = txtCode.Text.Trim().Length <= 0 ? 0 : Convert.ToInt32(txtCode.Text.Trim());
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
                divGridHeader.Visible = true;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                divGridHeader.Visible = false;
            }
            gridListStudent.DataSource = listResult;
            gridListStudent.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
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
}
