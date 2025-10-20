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


public partial class AddExams : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;

    //GLOBAL VARIABLES
    string sqlExamNextval = @"select nextval_exam('codeSeq') as exam_nextval";

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        //// verify mac adress
        //if (!Universal.MACAddressCompatible())
        //{
        //    Response.Redirect("~/WrongServerError.aspx");
        //}

        if (!IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;

                loadActiveClassroom(ddlClassroom);
                loadListAcademicYear(ddlAcademicYear);
                int currentAcademicYear = Settings.getAcademicYear();

                List<Course> listCourse = Course.getListScheduledCourse(currentAcademicYear);
                loadListCourse(listCourse);
                loadListTeacher(0, null, 0, 0);
                BindDataGridExam();
            }
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            //ddlClassroom.Enabled = false;
            //ddlCourse.Enabled = false;
            //ddlTeacher.Enabled = false;
            //ddlVacation.Enabled = false;
            //ddlControl.Enabled = false;
            //txtPoints.ReadOnly = true;
            //radExamDate.Enabled = false;
            //radStartHour.Enabled = false;
            //radEndHour.Enabled = false;
            //txtDuration.ReadOnly = true;
            //txtDescription.ReadOnly = true;
            //examUploader.Enabled = false;
            btnAddExams.Enabled = false;
            //btnClear.Enabled = false;

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridListExam.Visible && gridListExam.Rows.Count > 0)
            {
                for (int i = 0; i < gridListExam.Rows.Count; i++)
                {
                    ImageButton imgBtn = gridListExam.Rows[i].Cells[14].FindControl("btnDelete") as ImageButton;
                    imgBtn.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    protected void btnAddExams_Click(object sender, EventArgs e)
    {
        if (validateExamFields())
        {
            try

            {
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Error.aspx");
                }
                Users user = Session["user"] as Users;
                //creation of an exam object
                Exam exam = new Exam();
                //get the values of the form
                exam.id = "EX-" + Universal.getUniversalSequence(sqlExamNextval).ToString();
                exam.description = txtDescription.Text;
                exam.classroom_id = Convert.ToInt32(ddlClassroom.SelectedValue);
                exam.course_id = Convert.ToInt32(ddlCourse.SelectedValue);
                exam.teacher_id = ddlTeacher.SelectedValue;
                exam.vacation = ddlVacation.SelectedValue;
                exam.start_hour = radStartHour.SelectedTime.Value;
                exam.end_hour = radEndHour.SelectedTime.Value;
                exam.exam_date = radExamDate.SelectedDate.Value;
                exam.points = Convert.ToDouble(txtPoints.Text.Trim());
                exam.control = Convert.ToInt32(ddlControl.SelectedValue);
                exam.staff_request = user.username;
                exam.academic_year = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                // check attach documents
                if (examUploader.HasFile)
                {
                    // upload document & get file_path.
                    exam.file_path = uploadDocument(exam);
                    // get the file name by extracting it from the file path.
                    exam.file_name = exam.file_path.Replace("~/Uploaded_Exams/", "");
                }

                // check if exam is already assigned.
                List<Exam> listExamAlreadyTaken = Exam.GetListAlreadyAffectedExam(exam);
                if (listExamAlreadyTaken == null || listExamAlreadyTaken.Count <= 0)
                {
                    //add new exam
                    Exam.addExam(exam);

                    MessageAlert.RadAlert("Examen ajouté avec succès !", 350, 200, "Information", null);
                    emptyFields();
                    BindDataGridExam();
                    //}
                    //else
                    //{
                    //    MessageAlert.RadAlert("Echec !", 350, 200, "Information", null);
                    //}
                }
                else
                {
                    MessageAlert.RadAlert("Erreur : Cet Examen a ete deja enregistre !", 350, 200, "Information", null);
                }
            }
            catch (Exception ex)
            {
                MessBox.Show("Erreur : " + ex.Message);
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        emptyFields();
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        if (listClassroom.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            dropDownList.DataValueField = "id";
            dropDownList.DataTextField = "name";
            dropDownList.DataSource = listClassroom;
            dropDownList.DataBind();
        }

        dropDownList.Items.Insert(0, new DropDownListItem("", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    private void loadListCours(int classId)
    {
        List<Course> courseList = Course.getListCourseByClassId(classId);
        if (courseList.Count > 0)
        {
            ddlCourse.DataSource = courseList;
            ddlCourse.DataTextField = "name";
            ddlCourse.DataValueField = "id";
            ddlCourse.DataBind();
        }
        else
        {
            ddlCourse.Items.Clear();
        }
        ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
        ddlCourse.SelectedValue = "-1";
    }

    private void loadListTeacher(int courseId, string vacationId, int classId, int academicYear)
    {
        ddlTeacher.Items.Clear();
        List<Teacher> listTeacher = Teacher.getListTeacherForExam(courseId, vacationId, classId, academicYear);
        if (listTeacher != null && listTeacher.Count > 0)
        {
            ddlTeacher.DataSource = listTeacher;
            ddlTeacher.DataTextField = "fullname";
            ddlTeacher.DataValueField = "id";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
        }
        else
        {
            ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
            ddlTeacher.SelectedValue = "-1";
        }
    }

    protected void ddlClassroom_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // vacation
            ddlVacation.Items.Clear();
            // course
            ddlCourse.Items.Clear();
            // teacher
            ddlTeacher.Items.Clear();

            //
            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = int.Parse(ddlClassroom.SelectedValue);
                // vacation
                List<ClassRoom> listVacation = ClassRoom.getListVacationByClassroomId(classId);
                loadListVacation(listVacation);

                // course
                int academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                List<Course> listCourse2 = Course.getListScheduledCourseForExam(classId, ddlVacation.SelectedValue, academicYear);
                loadListCourse(listCourse2);


                // teacher
                int courseId = ddlCourse.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlCourse.SelectedValue);
                int academicyear = ddlAcademicYear.SelectedValue == null ? 0 : Convert.ToInt32(ddlAcademicYear.SelectedValue);
                string vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;

                loadListTeacher(courseId, vacation, classId, academicyear);
                ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlTeacher.SelectedValue = "-1";

            }
            else
            {
                // vacation
                ddlVacation.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlVacation.SelectedValue = "-1";

                // course
                int currentAcademicYear = Settings.getAcademicYear();
                List<Course> listCourse = Course.getListScheduledCourse(currentAcademicYear);
                loadListCourse(listCourse);
                ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlCourse.SelectedValue = "-1";

                // teacher
                loadListTeacher(0, null, 0, 0);
                ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlTeacher.SelectedValue = "-1";
            }

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
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
            ddlVacation.DataTextField = "vacation_name";
            ddlVacation.DataSource = listClassroom;
            ddlVacation.DataBind();
        }
        else
        {
            ddlVacation.Items.Add(new DropDownListItem("Matin", "AM"));
            ddlVacation.Items.Add(new DropDownListItem("Median", "PM"));
            ddlVacation.Items.Add(new DropDownListItem("Soir", "NG"));
            ddlVacation.Items.Add(new DropDownListItem("Weekend", "WK"));
        }
        ddlVacation.Items.Insert(0, new DropDownListItem("", "-1"));
    }

    private void loadListCourse(List<Course> listCourse)
    {
            //clear items
            ddlCourse.Items.Clear();
            if (listCourse != null && listCourse.Count > 0)
            {
                // fill the ddl now
                ddlCourse.DataValueField = "id";
                ddlCourse.DataTextField = "course_fullname";
                ddlCourse.DataSource = listCourse;
                ddlCourse.DataBind();
            }
            ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
            ddlCourse.SelectedValue = "-1";
    }

    protected void ddlCourse_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue == "-1"
               && ddlCourse.SelectedText == "")
            {
                ddlTeacher.Items.Clear();

                loadListTeacher(0, null, 0, 0);
                //ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
                //ddlTeacher.SelectedValue = "-1";
            }
            else
            {
                int courseId = ddlCourse.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlCourse.SelectedValue);
                int academicyear = ddlAcademicYear.SelectedValue == null ? 0 : Convert.ToInt32(ddlAcademicYear.SelectedValue);
                string vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;
                int classId = ddlClassroom.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlClassroom.SelectedValue);
                //
                loadListTeacher(courseId, vacation, classId, academicyear);
            }
        }
        catch (Exception ex) { }
    }

    protected void radStartHour_SelectedDateChanged(object sender, EventArgs e)
    {
        if (radEndHour.SelectedTime != null)
        {
            if (radStartHour.SelectedTime.Value >= radEndHour.SelectedTime.Value)
            {
                txtDuration.Text = string.Empty;
                radStartHour.SelectedTime = null;
                MessageAlert.RadAlert("Erreur : Heure debut invalide !", 350, 200, "Information", null);
            }
            else
            {
                calculateHours();
            }
        }
    }

    protected void radEndHour_SelectedDateChanged(object sender, EventArgs e)
    {
        if (radStartHour.SelectedTime != null)
        {
            if (radEndHour.SelectedTime.Value <= radStartHour.SelectedTime.Value)
            {
                txtDuration.Text = string.Empty;
                radEndHour.SelectedTime = null;
                MessageAlert.RadAlert("Erreur : Heure fin invalide !", 350, 200, "Information", null);
            }
            else
            {
                calculateHours();
            }
        }
    }

    private void calculateHours()
    {
        try
        {
            TimeSpan tsp = radEndHour.SelectedTime.Value - radStartHour.SelectedTime.Value;
            txtDuration.Text = tsp.Hours + " heure(s) " + tsp.Minutes + " minute(s)";
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private string uploadDocument(Exam exam)
    {
        string filepath = null;
        //get new document path
        string FolderExams = Server.MapPath("~/Uploaded_Exams");

        if (!Directory.Exists(FolderExams))
        {
            Directory.CreateDirectory(Server.MapPath("~/Uploaded_Exams"));
        }

        if (examUploader.HasFile) // check fileUpload control for files
        {
            string file_extension = System.IO.Path.GetExtension(examUploader.FileName); // get file extension
            if (file_extension.ToLower() != ".pdf") //check file extension
            {
                MessBox.Show("Seul les fichiers PDF peuvend etre attaches");
            }
            else
            {
                HttpFileCollection uploadedFiles = Request.Files;
                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];
                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                            string fileName = exam.id + "_" + Path.GetFileName(userPostedFile.FileName);
                            //
                            filepath = "~/Uploaded_Exams/" + fileName;
                            userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                            ExamDocuments doc = new ExamDocuments();
                            //doc.staff_code = exam.teacher_id;
                            //doc.document_path = filepath;
                            doc.document_name = fileName;
                            //doc.document_type = 3; // 0 is for student / 1 is for teacher / 2 is for staff / 3 is for Exam
                            //ExamDocuments.uploadDocument(doc);
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
            }
        }
        return filepath;
    }

    private bool validateExamFields()
    {
        bool result = true;

        if (ddlClassroom.SelectedValue == "-1"
            && ddlClassroom.SelectedText == "")
        {
            MessageAlert.RadAlert("Erreur : Selectionner une classe !", 300, 150, "Information", null);
            ddlClassroom.Focus();
            result = false;
        }
        else if (ddlCourse.SelectedValue == "-1"
           && ddlCourse.SelectedText == "")
        {
            MessageAlert.RadAlert("Erreur : Selectionner une matiere !", 300, 150, "Information", null);
            ddlCourse.Focus();
            result = false;
        }
        else if (ddlTeacher.SelectedValue == "-1"
            && ddlTeacher.SelectedText == "")
        {
            MessageAlert.RadAlert("Erreur : Selectionner un professeur !", 300, 150, "Information", null);
            ddlTeacher.Focus();
            result = false;
        }
        else if (ddlVacation.SelectedValue == "-1"
           && ddlVacation.SelectedText == "")
        {
            MessageAlert.RadAlert("Erreur : Selectionner une vacation !", 300, 150, "Information", null);
            ddlVacation.Focus();
            result = false;
        }
        else if (ddlControl.SelectedValue == "-1"
          && ddlControl.SelectedText == "")
        {
            MessageAlert.RadAlert("Erreur : Selectionner un control !", 300, 150, "Information", null);
            ddlControl.Focus();
            result = false;
        }
        else if (txtPoints.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Erreur : Entrer la note !", 300, 150, "Information", null);
            txtPoints.Focus();
            result = false;
        }
        else if (int.Parse(txtPoints.Text.Trim()) <= 0)
        {
            MessageAlert.RadAlert("Erreur : La note doit etre supereure a zero !", 300, 150, "Information", null);
            txtPoints.Focus();
            result = false;
        }
        else if (radExamDate.IsEmpty)
        {
            MessageAlert.RadAlert("Erreur : Choisir date examen !", 300, 150, "Information", null);
            radExamDate.Focus();
            result = false;
        }
        else if (radStartHour.IsEmpty)
        {
            txtDuration.Text = string.Empty;
            radStartHour.Focus();
            result = false;
        }
        else if (radEndHour.IsEmpty)
        {
            txtDuration.Text = string.Empty;
            radEndHour.Focus();
            result = false;
        }
        else if (radStartHour.SelectedTime.Value > radEndHour.SelectedTime.Value)
        {
            txtDuration.Text = string.Empty;
            radStartHour.Focus();
            result = false;
        }
        else if (radEndHour.SelectedTime.Value <= radStartHour.SelectedTime.Value)
        {
            txtDuration.Text = string.Empty;
            radEndHour.Focus();
            result = false;
        }
        return result;
    }

    private void emptyFields()
    {
        //  ddlClassroom.SelectedValue = "-1";
        //  ddlVacation.Items.Clear();
        //   ddlVacation.Items.Insert(0, new DropDownListItem("", "-1"));
        //
        //   ddlControl.SelectedValue = "-1";
        txtDescription.Text = string.Empty;
        ddlCourse.SelectedValue = "-1";
        ddlTeacher.SelectedValue = "-1";
        radStartHour.Clear();
        radEndHour.Clear();
        txtDuration.Text = string.Empty;
        radExamDate.Clear();
        txtPoints.Text = string.Empty;
        loadListAcademicYear(ddlAcademicYear);
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    private void loadListAcademicYear(RadDropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();
            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
                // get current academic year
                int currentAcdemicYear = Settings.getAcademicYear();
                ddl.SelectedValue = currentAcdemicYear.ToString();
            }
            else
            {
                ddl = null;
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            // course
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
            ddlCourse.SelectedValue = "-1";

            // teacher
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
            ddlTeacher.SelectedValue = "-1";

            //
            if (ddlVacation.SelectedValue != "-1")
            {
                int classId = ddlClassroom.SelectedValue == null ? 0 : int.Parse(ddlClassroom.SelectedValue);
                int academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                List<Course> listCourse = Course.getListScheduledCourseForExam(classId, ddlVacation.SelectedValue, academicYear);
                loadListCourse(listCourse);

                loadListTeacher(0, ddlVacation.SelectedValue, classId, academicYear);
                ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlTeacher.SelectedValue = "-1";
            }

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            // GET PREVIOUSLY SELECTED VACATION
            string _oldVacation = ddlVacation.SelectedValue;

            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = int.Parse(ddlClassroom.SelectedValue);
                // vacation
                List<ClassRoom> listVacation = ClassRoom.getListVacationByClassroomId(classId);
                loadListVacation(listVacation);
                ddlVacation.SelectedValue = _oldVacation;

                // course
                int academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                List<Course> listCourse2 = Course.getListScheduledCourseForExam(classId, ddlVacation.SelectedValue, academicYear);
                loadListCourse(listCourse2);

                // teacher
                int courseId = ddlCourse.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlCourse.SelectedValue);
                int academicyear = ddlAcademicYear.SelectedValue == null ? 0 : Convert.ToInt32(ddlAcademicYear.SelectedValue);
                string vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;
                //
                loadListTeacher(courseId, vacation, classId, academicyear);
                ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
                ddlTeacher.SelectedValue = "-1";

            }
            //else
            //{
            //    // vacation
            //    ddlVacation.Items.Insert(0, new DropDownListItem("", "-1"));
            //    ddlVacation.SelectedValue = "-1";

            //    // course
            //    int currentAcademicYear = Universal.getAcademicYear();
            //    List<Course> listCourse = Course.getListScheduledCourse(currentAcademicYear);
            //    loadListCourse(listCourse);
            //    ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
            //    ddlCourse.SelectedValue = "-1";

            //    // teacher
            //    loadListTeacher(0, null, 0, 0);
            //    ddlTeacher.Items.Insert(0, new DropDownListItem("", "-1"));
            //    ddlTeacher.SelectedValue = "-1";
            //}

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataGridExam();
    }

    private void BindDataGridExam()
    {
        try
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            Users user = Session["user"] as Users;
            //
            Exam exam = new Exam();
            exam.classroom_id = ddlClassroom.SelectedValue == "-1" ? 0 : int.Parse(ddlClassroom.SelectedValue);
            exam.vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;
            exam.control = ddlControl.SelectedValue == "-1" ? 0 : int.Parse(ddlControl.SelectedValue);
            exam.course_id = ddlCourse.SelectedValue == "-1" ? 0 : int.Parse(ddlCourse.SelectedValue);
            exam.teacher_id = ddlTeacher.SelectedValue == "-1" ? null : ddlTeacher.SelectedValue;
            exam.coefficient = txtPoints.Text.Trim().Length <= 0 ? 0 : int.Parse(txtPoints.Text.Trim());
            if (radExamDate.SelectedDate != null)
            {
                exam.exam_date = radExamDate.SelectedDate.Value;
            }

            exam.description = txtDescription.Text.Trim().Length <= 0 ? null : txtDescription.Text.Trim();
            exam.academic_year = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

            //get list of exams
            List<Exam> listResult = Exam.getListActiveExam(exam);
            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
                //tblGridHeader.Visible = true;
                gridListExam.Visible = true;
                lnkExportExcel.Visible = true;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                //tblGridHeader.Visible = false;
                gridListExam.Visible = false;
                lnkExportExcel.Visible = false;
            }
            gridListExam.DataSource = listResult;
            gridListExam.DataBind();

            // check login_user policy to grant or revoke access
            List<Users> listGroupPolicy = Users.getListUserAccessLevel(user.role_id, menu_code);
            if (listGroupPolicy != null && listGroupPolicy.Count > 0)
            {
                if (listGroupPolicy[0].view_access == 0
                    && listGroupPolicy[0].edit_access == 0
                    && listGroupPolicy[0].delete_access == 0)
                {
                    //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // edit
                    if (listGroupPolicy[0].edit_access == 0)
                    {
                        disableEditOption();
                    }
                    // delete
                    if (listGroupPolicy[0].delete_access == 0)
                    {
                        disableDeleteOption();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error " + ex.Message);
        }
    }

    protected void gridListExam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListExam.PageIndex = e.NewPageIndex;
        BindDataGridExam();
    }

    protected void gridListExam_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListExam.Rows[index];
        string examCode = gridListExam.DataKeys[index].Value.ToString();

        if (e.CommandName == "viewPdf")
        {
            string page_url = null;
            List<Exam> listExamInfo = Exam.getListExamById(examCode);
            if (listExamInfo != null && listExamInfo.Count > 0)
            {
                page_url = "../Uploaded_Exams/" + listExamInfo[0].file_name;
            }

            try
            {
                if (page_url != null)
                {
                    //Response.Redirect("DocumentDetail.aspx");
                    //Session["type_detail"] = "endedit";
                    //mp1.Show();
                    string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                    + "oWinn.show();"
                                                    + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                    + "oWinn.SetSize(1024, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                    + "oWinn.center();"
                                                    + "Sys.Application.remove_load(f);"
                                                + "}"
                                                + "Sys.Application.add_load(f);";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

    }

    protected void gridListExam_RowDataBound(object sender, GridViewRowEventArgs e)
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

    public void removeExam(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                string examCode = gridListExam.DataKeys[index].Value.ToString();
                // check if exam code is already asign to a note
                //if (!Notes.ExamAssignToNote(examCode))
                //{
                //    Exam.deleteExamPermanently(examCode);
                //    //refresh data of the gridview
                //    BindDataGridExam();
                //}
                //else
                //{
                //    MessageAlert.RadAlert("Desole, mais vous ne pouvez pas supprimer cet examen car il a ete affecte a une note", 500, 150, "Information", null);
                //}

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            string userName = user.username.ToUpper(); // this one should be replaced with the login user

            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + userName))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + userName);
            }

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\liste_Examens_{1}_{2}.xls",
                userName, userName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            //gridListExam.AllowPaging = false;
            //gridListExam.HeaderStyle.Font.Bold = true;
            //gridListExam.Style.Add("font-size", "12px");
            //gridListExam.GridLines = GridLines.Vertical;
            ////gridListPolicy.DataBind();
            //BindDataGridExam();
            //GetSalaryTable();
            // gridListExam.HeaderRow.Visible = true;

            if (gridListExam.Visible == true)
            {

                gridListExam.HeaderRow.Cells[0].Visible = false;
                gridListExam.HeaderRow.Cells[1].Visible = false;
                gridListExam.HeaderRow.Cells[2].Visible = false;
                gridListExam.HeaderRow.Cells[14].Visible = false;
                //
                gridListExam.HeaderRow.Cells[3].Width = 100;
                gridListExam.HeaderRow.Cells[4].Width = 100;
                gridListExam.HeaderRow.Cells[5].Width = 200;
                gridListExam.HeaderRow.Cells[6].Width = 100;
                gridListExam.HeaderRow.Cells[7].Width = 100;
                gridListExam.HeaderRow.Cells[8].Width = 100;
                gridListExam.HeaderRow.Cells[9].Width = 200;
                gridListExam.HeaderRow.Cells[10].Width = 100;
                gridListExam.HeaderRow.Cells[11].Width = 100;
                gridListExam.HeaderRow.Cells[12].Width = 100;
                gridListExam.HeaderRow.Cells[13].Width = 100;
                //

                //design the gridview
                //gridListExam.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListExam.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListExam.BorderWidth = 1;
                //gridListExam.BackColor = Color.Transparent;
                //gridListExam.RowStyle.BackColor = Color.Transparent;
                //gridListExam.BorderStyle = BorderStyle.None;
                gridListExam.RowStyle.Height = 40;

                // setup header background color to navy
                gridListExam.HeaderRow.Cells[3].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[4].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[5].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[6].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[7].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[8].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[9].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[10].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[11].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[12].BackColor = Color.Navy;
                gridListExam.HeaderRow.Cells[13].BackColor = Color.Navy;

                // setup header forecolor to white
                gridListExam.HeaderRow.Cells[3].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[4].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[5].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[6].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[7].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[8].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[9].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[10].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[11].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[12].ForeColor = Color.White;
                gridListExam.HeaderRow.Cells[13].ForeColor = Color.White;

                //
                for (int i = 0; i < gridListExam.Rows.Count; i++)
                {
                    GridViewRow row = gridListExam.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[14].Visible = false;
                    row.BackColor = Color.Transparent;
                    //
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
                gridListExam.RenderControl(htmlWrite);
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
            // set token
            //   string token = Token.generate(Token.TypeToken.Download, user.Username, Response);
            //  tokenField.Value = token;
            //
            gridListExam.HeaderRow.Visible = false;
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

}