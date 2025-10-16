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
using Utilities;
using Telerik.Web.UI;
using System.Globalization;
using Microsoft.CSharp;



public partial class DownloadNotes : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;

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
            loadActiveClassroom(ddlClassroom);
            loadListAcademicYear(ddlAcademicYear);
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

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ClassRoom classroom = new ClassRoom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    private void loadListCours(int classId)
    {
        List<Course> courseList = Course.getListCourseFromExamByClassId(classId);
        if (courseList.Count > 0)
        {
            ddlCourse.DataSource = courseList;
            ddlCourse.DataTextField = "name";
            ddlCourse.DataValueField = "id";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
    }

    private void loadListTeacher(int courseId)
    {
        List<Teacher> teacherList = Teacher.getListTeacherFromExamByCourseId(courseId);
        if (teacherList.Count > 0)
        {
            ddlTeacher.DataSource = teacherList;
            ddlTeacher.DataTextField = "fullname";
            ddlTeacher.DataValueField = "id";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlTeacher.SelectedValue = "-1";
        }
    }

    private bool validateExamFields()
    {
        bool result = true;

        if (ddlClassroom.SelectedValue == "-1"
            && ddlClassroom.SelectedText == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Selectionner une classe");
            ddlClassroom.Focus();
            result = false;
        }
        else if (ddlCourse.SelectedValue == "-1"
           && ddlCourse.SelectedText == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Selectionner une matiere");
            ddlCourse.Focus();
            result = false;
        }
        else if (ddlTeacher.SelectedValue == "-1"
            && ddlTeacher.SelectedText == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Selectionner un professeur");
            ddlTeacher.Focus();
            result = false;
        }
        else if (ddlVacation.SelectedValue == "-1"
           && ddlVacation.SelectedText == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Selectionner une vacation");
            ddlVacation.Focus();
            result = false;
        }

        else if (ddlPeriod.SelectedValue == "-1"
          && ddlPeriod.SelectedText == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Selectionner une periode");
            ddlPeriod.Focus();
            result = false;
        }
        return result;
    }

    private void emptyFields()
    {
        ddlClassroom.SelectedValue = "-1";
        ddlCourse.SelectedValue = "-1";
        ddlCourse.Enabled = false;
        ddlTeacher.SelectedValue = "-1";
        ddlTeacher.Enabled = false;
        ddlVacation.SelectedValue = "-1";
        //
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (validateFields())
        {
            BindDataGridNotes();
        }
    }

    private bool validateFields()
    {
        lblError.Text = string.Empty;
        lblError.Visible = false;
        gridListNotes.DataSource = null;
        gridListNotes.DataBind();
        //hide some fields
        lblFound.Visible = false;
        pnlResult.Visible = false;
        lblCounter.Visible = false;
        lblExport.Visible = false;
        gridListNotes.Visible = false;


        //
        if (ddlClassroom.SelectedValue == "-1")
        {
            lblError.Text = "Erreur : Selectionner une classe !";
            lblError.Visible = true;
            return false;
        }
        else if (ddlVacation.SelectedValue == "-1")
        {
            lblError.Text = "Erreur : Selectionner une vacation !";
            lblError.Visible = true;
            return false;
        }
        else if (ddlCourse.SelectedValue == "-1")
        {
            lblError.Text = "Erreur : Selectionner une matiere !";
            lblError.Visible = true;
            return false;
        }
        else if (ddlTeacher.SelectedValue == "-1")
        {
            lblError.Text = "Erreur : Selectionner un professeur !";
            lblError.Visible = true;
            return false;
        }
        else if (ddlAcademicYear.SelectedValue == null)
        {
            lblError.Text = "Erreur: Selectionner debut annee academique !";
            lblError.Visible = true;
            return false;
        }
        return true;
    }

    private void BindDataGridNotes()
    {
        try
        {
            Notes notes = new Notes();
            //notes.teacher_id = ddlTeacher.SelectedValue;
            //notes.class_id = int.Parse(ddlClassroom.SelectedValue);
            //notes.vacation = ddlVacation.SelectedValue;
            ////notes.exam_period = ddlPeriod.SelectedValue;
            //notes.cours_id = int.Parse(ddlCourse.SelectedValue);
            //notes.academic_year_id = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

            //get list of students
            List<Notes> listResult = Notes.getListNotesTemplates(notes);
            if (listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
                lblExport.Visible = true;
                gridListNotes.Visible = true;


                gridListNotes.DataSource = listResult;
                gridListNotes.DataBind();

                // hide some unwanted columns
                //gridListNotes.Columns[1].Visible = false;
                //gridListNotes.Columns[9].Visible = false;
                //gridListNotes.Columns[10].Visible = false;
                //gridListNotes.Columns[11].Visible = false;
                //gridListNotes.Columns[12].Visible = false;
                //gridListNotes.Columns[13].Visible = false;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                lblExport.Visible = false;
                gridListNotes.Visible = false;
            }

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void gridListNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListNotes.PageIndex = e.NewPageIndex;
        BindDataGridNotes();
    }

    protected void gridListNotes_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListNotes.Rows[index];
        String studentCode = row.Cells[1].Text;
    }

    protected void gridListNotes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.RowIndex == 0)
            //    e.Row.Style.Add("height", "60px");
            //e.Row.Style.Add("vertical-align", "bottom");


            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    protected void ExportToExcel()
    {
        try
        {
            Users user = Session["user"] as Users;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="
                + "Palmares_" + ddlClassroom.SelectedText + "_" + ddlCourse.SelectedItem.Text + "_" +
                ddlAcademicYear.SelectedText + "_" + ddlPeriod.SelectedText + "_"
                + DateTime.Now.ToString("yyyyMMddHHmmss")
                + ddlVacation.SelectedText + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //
                Notes notes = new Notes();
                notes.teacher_id = ddlTeacher.SelectedValue;
                notes.class_id = int.Parse(ddlClassroom.SelectedValue);
                notes.vacation = ddlVacation.SelectedValue;
                //notes. = ddlPeriod.SelectedValue;
                notes.cours_id = int.Parse(ddlCourse.SelectedValue);
                notes.academic_year_id = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

                //get list of students
                List<Notes> listResult = Notes.getListNotesTemplates(notes);
                if (listResult != null && listResult.Count > 0)
                {
                    lblFound.Visible = false;
                    pnlResult.Visible = true;
                    lblCounter.Visible = true;
                    lblCounter.Text = listResult.Count + " Ligne(s)";
                    lblExport.Visible = true;
                    gridListNotes.Visible = true;
                    gridListNotes.DataSource = listResult;
                    gridListNotes.DataBind();
                }

                //To Export all pages
                gridListNotes.AllowPaging = false;
                //define the width for each column
                gridListNotes.HeaderRow.Cells[0].Visible = false;
                gridListNotes.HeaderRow.Cells[1].Width = 100;
                gridListNotes.HeaderRow.Cells[2].Width = 100;
                gridListNotes.HeaderRow.Cells[3].Width = 100;
                gridListNotes.HeaderRow.Cells[4].Width = 100;
                gridListNotes.HeaderRow.Cells[5].Width = 100;
                gridListNotes.HeaderRow.Cells[6].Width = 100;
                gridListNotes.HeaderRow.Cells[7].Width = 100;
                gridListNotes.HeaderRow.Cells[8].Width = 100;
                gridListNotes.HeaderRow.Cells[9].Width = 100;
                gridListNotes.HeaderRow.Cells[10].Width = 100;
                gridListNotes.HeaderRow.Cells[11].Width = 200;
                gridListNotes.HeaderRow.Cells[12].Width = 100;
                gridListNotes.HeaderRow.Cells[13].Width = 100;

                //design the gridview
                //gridListNotes.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListNotes.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListNotes.BackColor = Color.Transparent;
                //gridListNotes.RowStyle.BackColor = Color.Transparent;
                //gridListNotes.BorderStyle = BorderStyle.None;
                gridListNotes.RowStyle.Height = 20;


                // setup header background color to navy
                gridListNotes.HeaderRow.Cells[1].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[2].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[3].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[4].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[5].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[6].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[7].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[8].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[9].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[10].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[11].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[12].BackColor = Color.Navy;
                gridListNotes.HeaderRow.Cells[13].BackColor = Color.Navy;

                // setup header forecolor to white
                gridListNotes.HeaderRow.Cells[1].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[2].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[3].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[4].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[5].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[6].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[7].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[8].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[9].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[10].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[11].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[12].ForeColor = Color.White;
                gridListNotes.HeaderRow.Cells[13].ForeColor = Color.White;

                foreach (GridViewRow row in gridListNotes.Rows)
                {
                    // hide first column
                    row.Cells[0].Visible = false;
                    //row.BackColor = Color.Transparent;
                    //row.BackColor = Color.Transparent;

                    //
                    row.Cells[1].BorderWidth = 1;
                    row.Cells[2].BorderWidth = 1;
                    row.Cells[3].BorderWidth = 1;
                    row.Cells[4].BorderWidth = 1;
                    row.Cells[5].BorderWidth = 1;
                    row.Cells[6].BorderWidth = 1;
                    row.Cells[7].BorderWidth = 1;
                    row.Cells[8].BorderWidth = 1;
                    row.Cells[9].BorderWidth = 1;
                    row.Cells[10].BorderWidth = 1;
                    row.Cells[11].BorderWidth = 1;
                    row.Cells[12].BorderWidth = 1;
                    row.Cells[13].BorderWidth = 1;
                }

                //
                gridListNotes.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                //
                BindDataGridNotes();
            }
        }
        catch (System.Threading.ThreadAbortException lException)
        {
            // do nothing
        }
        catch (Exception ex)
        {
            MessBox.Show("Error when export!");
        }
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlPeriod.SelectedValue == "-1"
           && ddlPeriod.SelectedText == "--Tout Sélectionner--")
        {
            ddlClassroom.SelectedValue = "-1";
            ddlCourse.SelectedValue = "-1";
            ddlTeacher.SelectedValue = "-1";
            //
            ddlClassroom.Enabled = false;
            ddlCourse.Enabled = false;
            ddlTeacher.Enabled = false;
        }
        else
        {
            loadActiveClassroom(ddlClassroom);
            //
            ddlClassroom.SelectedValue = "-1";
            ddlCourse.SelectedValue = "-1";
            ddlTeacher.SelectedValue = "-1";
            //
            ddlClassroom.Enabled = true;
            ddlCourse.Enabled = false;
            ddlTeacher.Enabled = false;
        }
    }

    private void loadListAvailableCourse()
    {
        int classroomId = int.Parse(ddlClassroom.SelectedValue);
        int vacationId = int.Parse(ddlVacation.SelectedValue);
        //
        // get available course
        List<Course> listCourse = Course.getListCourseByClassId(classroomId);
        if (listCourse == null || listCourse.Count <= 0)
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddlCourse.DataTextField = "name";
            ddlCourse.DataValueField = "id";
            ddlCourse.DataSource = listCourse;
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
    }

    protected void ddlClassroom_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
            //
            ddlVacation.Items.Clear();
            ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlVacation.SelectedValue = "-1";
            //
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
            //
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlTeacher.SelectedValue = "-1";

            if (ddlClassroom.SelectedValue != "-1")
            {
                int classroomId = int.Parse(ddlClassroom.SelectedValue);
                // get available vacation
                List<ClassRoom> listVacation = null; // ClassRoom.getListActiveVacationByClass(classroomId);
                if (listVacation != null && listVacation.Count > 0)
                {
                    ddlVacation.DataTextField = "vacation_name";
                    ddlVacation.DataValueField = "vacation_type";
                    ddlVacation.DataSource = listVacation;
                    ddlVacation.DataBind();
                    ddlVacation.SelectedIndex = 0;
                }
                ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
                ddlVacation.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error " + ex.Message);
        }
    }

    protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
            //
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
            //
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlTeacher.SelectedValue = "-1";
            //
            if (ddlVacation.SelectedValue != "-1")
            {
                Course course = new Course();
                course.class_id = int.Parse(ddlClassroom.SelectedValue);
                course.academic_year = int.Parse(ddlAcademicYear.SelectedValue);
                course.vacation_code = ddlVacation.SelectedValue;

                List<Course> listCourse = Course.getListCourseForPalmares(course);

                if (listCourse.Count > 0 && listCourse != null)
                {
                    // clear old items
                    ddlCourse.Items.Clear();
                    // add new items
                    ddlCourse.DataSource = listCourse;
                    ddlCourse.DataTextField = "name";
                    ddlCourse.DataValueField = "id";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
                    ddlCourse.SelectedValue = "-1";
                }

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error " + ex.Message);
        }
    }

    protected void ddlCourse_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
            //
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlTeacher.SelectedValue = "-1";
            //
            if (ddlCourse.SelectedValue != "-1")
            {
                int classroomId = int.Parse(ddlClassroom.SelectedValue);
                int coursId = int.Parse(ddlCourse.SelectedValue);
                //
                List<Teacher> listTeacher = Teacher.getListTeacherByCourseIdAndClassroomId(coursId, classroomId);
                if (listTeacher != null && listTeacher.Count > 0)
                {
                    ddlTeacher.DataTextField = "fullname";
                    ddlTeacher.DataValueField = "id";
                    ddlTeacher.DataSource = listTeacher;
                    ddlTeacher.DataBind();
                    ddlTeacher.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        BindDataGridNotes();
    }

    protected void ddlPeriod_SelectedIndexChanged1(object sender, DropDownListEventArgs e)
    {
        lblError.Text = string.Empty;
        lblError.Visible = false;
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        lblError.Text = string.Empty;
        lblError.Visible = false;
    }
}
