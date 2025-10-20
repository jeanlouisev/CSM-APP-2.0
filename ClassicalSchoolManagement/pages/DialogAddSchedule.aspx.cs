using System;
using System.Collections.Generic;
using System.Linq;
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
using Telerik.Web.UI;
using System.IO;
using System.Drawing;

using Utilities;




public partial class DialogAddSchedule : System.Web.UI.Page
{

    string msgContent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }


        if (!Page.IsPostBack)
        {
            Users user = Session["user"] as Users;

            if (Session["classroom_obj"] != null)
            {
                LoadClassroomPreviousInfo();
            }
        }
    }

    private void LoadClassroomPreviousInfo()
    {
        ClassRoom c = Session["classroom_obj"] as ClassRoom;
        this.Title = "Horaire de la classe : " + c.classroom_name;
        hiddenClassId.Value = c.id.ToString();
        ddlVacation.SelectedValue = c.vacation;

        if (c.id < 70)
        {
            string vacation = c.vacation;
            switch (c.vacation)
            {
                // set time ranges for each vacation
                case "AM":
                    radStartHour.SelectedTime = TimeSpan.Parse("08:00");
                    radEndHour.SelectedTime = TimeSpan.Parse("12:00");
                    break;

                case "PM":
                    radStartHour.SelectedTime = TimeSpan.Parse("12:00");
                    radEndHour.SelectedTime = TimeSpan.Parse("16:00");
                    break;

                case "NG":
                    radStartHour.SelectedTime = TimeSpan.Parse("16:00");
                    radEndHour.SelectedTime = TimeSpan.Parse("20:00");
                    break;

            }
            radStartHour.Enabled = false;
            radEndHour.Enabled = false;
            calculateHours();
        }

        List<Course> listCourse = Course.getListAllCourseAttachedToTeacher();
        loadListCourse(listCourse);
        // refresh grid
        radGridScheduleDetails.Rebind();
    }

    private void loadListTeacherAll()
    {
        try
        {
            ddlTeacher.Items.Clear();
            // get list all academic  year
            List<Teacher> listTeacherInfo = Teacher.getListActiveTeacher();

            if (listTeacherInfo != null && listTeacherInfo.Count > 0)
            {
                ddlTeacher.DataValueField = "id";
                ddlTeacher.DataTextField = "fullname";
                ddlTeacher.DataSource = listTeacherInfo;
                ddlTeacher.DataBind();
            }
            ddlTeacher.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
            ddlTeacher.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

    protected void btnLoadFromPreviousSchedule_Click(object sender, ImageClickEventArgs e)
    {
        //
        Schedule sch = new Schedule();
        sch.code_classroom = null;
        sch.vacation = ddlVacation.SelectedValue;
        loadListPreviousScheduleConfiguation(sch);

    }

    protected void btnChangeFromPrevious_Click(object sender, EventArgs e)
    {
        //if (ddlAcademicyearPreviousConfig.SelectedValue != null)
        //{
        //    Schedule sch = new Schedule();
        //    sch.code_classroom = txtClassCode.Text.Trim();
        //    sch.vacation = ddlVacation.SelectedValue;
        //    sch.academic_year_old = int.Parse(ddlAcademicyearPreviousConfig.SelectedValue);
        //    sch.academic_year_new = int.Parse(ddlAcademicYear.SelectedValue);
        //    //
        //    Schedule.ChangeScheduleFromPrevious(sch);
        //}
        //else
        //{
        //    MessBox.Show("Désolé, veuillez sélectionner  l'année académique !");
        //}
    }

    private void loadListPreviousScheduleConfiguation(Schedule sch)
    {
        try
        {
            ////clear items
            //ddlAcademicyearPreviousConfig.Items.Clear();
            //List<Schedule> listSchedule = Schedule.GetListPreviousScheduleConfiguation(sch);
            ////
            //if (listSchedule != null && listSchedule.Count > 0)
            //{
            //    // fill the ddl now
            //    ddlAcademicyearPreviousConfig.DataValueField = "id";
            //    ddlAcademicyearPreviousConfig.DataTextField = "years";
            //    ddlAcademicyearPreviousConfig.DataSource = listSchedule;
            //    ddlAcademicyearPreviousConfig.DataBind();
            //}
        }
        catch (Exception ex) { }
    }

    private void loadListCourse(List<Course> listCourse)
    {
        try
        {
            //clear items
            ddlCourse.Items.Clear();
            if (listCourse != null && listCourse.Count > 0)
            {
                // fill the ddl now
                ddlCourse.DataValueField = "id";
                ddlCourse.DataTextField = "name";
                ddlCourse.DataSource = listCourse;
                ddlCourse.DataBind();
            }
            ddlCourse.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        // kill sessions
        Session.Remove("classroom_obj");
        Session["classroom_obj"] = null;
        // close modal
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "CloseDialog();", true);
    }

    protected void radStartHour_SelectedDateChanged(object sender, EventArgs e)
    {
        //lblError.Text = "";
        //lblError.Visible = false;
        if (radEndHour.SelectedTime != null)
        {
            if (radStartHour.SelectedTime.Value >= radEndHour.SelectedTime.Value)
            {
                //lblError.Text = "Heure invalide !";
                //lblError.Visible = true;
                txtDuration.Text = string.Empty;
                radStartHour.Focus();
            }
            else
            {
                calculateHours();
            }
        }

    }

    protected void radEndHour_SelectedDateChanged(object sender, EventArgs e)
    {
        //lblError.Text = "";
        //lblError.Visible = false;
        if (radStartHour.SelectedTime != null)
        {
            if (radEndHour.SelectedTime.Value <= radStartHour.SelectedTime.Value)
            {
                //lblError.Text = "Heure invalide !";
                //lblError.Visible = true;
                txtDuration.Text = string.Empty;
                radEndHour.Focus();
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

    protected void ddlTeacher_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTeacher.SelectedValue == "-1")
        {
            string teachId = ddlTeacher.SelectedValue;
            int classId = int.Parse(hiddenClassId.Value);
            List<Course> listCourse = Course.getListAllCourseAttachedToTeacher();
            loadListCourse(listCourse);
            ddlCourse.SelectedValue = "-1";
        }
        else
        {
            string teachId = ddlTeacher.SelectedValue;
            int classId = int.Parse(hiddenClassId.Value);
            loadListTeachCours(teachId, classId);
        }
    }

    //Action on combo teacher
    private void loadListTeachCours(string teacherCode, int classId)
    {
        List<Course> courseList = Course.getListAttachedCourseByTeacherId(teacherCode);
        if (courseList.Count > 0)
        {
            ddlCourse.DataSource = courseList;
            ddlCourse.DataTextField = "name";
            ddlCourse.DataValueField = "id";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
    }

    private void resetForms()
    {
        ddlDays.SelectedIndex = 0;
        ddlTeacher.SelectedValue = "-1";
        ddlCourse.SelectedValue = "-1";
        radStartHour.SelectedDate = null;
        radEndHour.SelectedDate = null;
        txtDuration.Text = string.Empty;

    }

    protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {

    }

    protected void gridListSChedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gridListSChedule_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        radGridScheduleDetails.Rebind();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue == "-1")
            {
                loadListTeacherAll();
            }
            else
            {
                int courseId = int.Parse(ddlCourse.SelectedValue);

                // load teacher's list for selected courses
                List<Teacher> listTeacher = Teacher.getListTeacherByCourseId(courseId);
                ddlTeacher.Items.Clear();
                if (listTeacher != null && listTeacher.Count > 0)
                {
                    ddlTeacher.DataSource = listTeacher;
                    ddlTeacher.DataTextField = "fullname";
                    ddlTeacher.DataValueField = "id";
                    ddlTeacher.DataBind();
                }
                ddlTeacher.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
                ddlTeacher.SelectedValue = "-1";
            }
        }
        catch (Exception ex) { }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!validateFields())  // check all required fields in the form
        {
            msgContent = "Erreur : Tous les champs sont obligatoires !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            Users user = Session["user"] as Users;
            //get the values of the form Shedule!
            Schedule s = new Schedule();
            s.class_id = int.Parse(hiddenClassId.Value);
            s.days = ddlDays.SelectedValue;
            s.cours_id = int.Parse(ddlCourse.SelectedValue);
            s.start_hour = radStartHour.SelectedTime.Value;
            s.end_hour = radEndHour.SelectedTime.Value;
            s.teacher_id = ddlTeacher.SelectedValue;
            s.vacation = ddlVacation.SelectedValue;
            s.login_user_id = user.id;

            if (s.class_id < 70)    // Primary Classes
            {
                if (Schedule.ScheduleAvailableForPrimaryclass(s))
                {
                    string coursName = ddlCourse.SelectedItem.Text;
                    string day = ddlDays.SelectedItem.Text;

                    msgContent = "Désolé, le cours " + coursName + " est déja enseigné le " + day + " !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
                }
                else
                {
                    // add schedule
                    Schedule.InsertSchedule(s);
                    // refresh grid
                    radGridScheduleDetails.Rebind();
                    //
                    msgContent = "Succès !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
                }
            }
            else
            {
                string coursName = ddlCourse.SelectedItem.Text;
                string day = ddlDays.SelectedItem.Text;
                string teacherName = ddlTeacher.SelectedItem.Text;
                string startHour = radStartHour.SelectedTime.ToString();
                string endHour = radEndHour.SelectedTime.ToString();

                //
                if (Schedule.TeacherScheduleAvailable(s))
                {
                    msgContent = "Désolé, Le professeur " + teacherName + " " +
                                  "ne sera pas disponible " + day + " entre " + startHour + " - " + endHour + " heure !";

                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
                }
                else if (Schedule.CoursAvailableForSecondaryClass(s))
                {
                    msgContent = "Désolé, cette Tranche d\\'heure n\\'est plus disponible !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
                }
                else
                {
                    // add schedule
                    Schedule.InsertSchedule(s);
                    // refresh grid
                    radGridScheduleDetails.Rebind();
                    //
                    msgContent = "Succès !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
                }
            }
        }
    }

    private bool validateFields()
    {
        bool result = true;
        if (ddlDays.SelectedItem.Value == "-1")
        {
            result = false;
        }
        if (ddlVacation.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        if (ddlCourse.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        if (ddlTeacher.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        if (radStartHour.SelectedDate == null)
        {
            result = false;
        }
        if (radEndHour.SelectedDate == null)
        {
            result = false;
        }

        return result;
    }

    public void removeSchedule(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            Schedule.DeleteShedule(id);
            // refresh grid
            radGridScheduleDetails.Rebind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void radGridScheduleDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int classId = int.Parse(hiddenClassId.Value);
            string vacation = ddlVacation.SelectedValue;
            //
            List<Schedule> listResult = Schedule.getListSHeduleClasse(classId, vacation);

            radGridScheduleDetails.DataSource = listResult;
            radGridScheduleDetails.DataBind();

            if (listResult != null && listResult.Count > 0)
            {
                int cnt = 0;
                foreach (Schedule s in listResult)
                {
                    if (s.teacher_cours_attach_status > 0)
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    divCoursAttached.Visible = true;
                }
                else
                {
                    divCoursAttached.Visible = false;
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void radGridScheduleDetails_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridScheduleDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridScheduleDetails.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();

            HiddenField hiddenCoursAttachStatus = (HiddenField)item.FindControl("hiddenCoursAttachStatus");
            int status = int.Parse(hiddenCoursAttachStatus.Value);
            if (status <= 0)
            {
                for (int i = 3; i < 9; i++)
                {
                    // backcolor
                    item.Cells[i].BackColor = Color.Red;
                    // forcolor
                    item.Cells[i].ForeColor = Color.WhiteSmoke;
                }

                divCoursAttached.Visible = true;
            }
        }
    }
}