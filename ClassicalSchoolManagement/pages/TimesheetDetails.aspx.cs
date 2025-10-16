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



public partial class TimesheetDetails : System.Web.UI.Page
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
            try
            {
                // set default timesheet date
                radFromDate.SelectedDate = DateTime.Now;
                radToDate.SelectedDate = DateTime.Now;
                loadListAcademicYear(ddlAcademicYear);
                loadListCourseByTeacher();
                loadListClassByTeacher();
                ddlAcademicYear_SelectedIndexChanged(this, null);

                if (Session["staff_code"] != null)
                {
                    loadTeacherInfo(Session["staff_code"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
    }

    private void loadTeacherInfo(string staffCode)
    {
        try
        {
            Teacher t = Teacher.getTeacherInfoById(staffCode);
            if (t != null)
            {
                string fullName = t.fullName;
                lblFullname.Text = fullName.ToUpper();
                lblTeacherId.Text = staffCode.ToUpper();
                BindDataGridTeacher();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private void BindDataGridTeacher()
    {
        try
        {
            string staffCode = Session["staff_code"].ToString();
            // create an object of type schedule
            Schedule schedule = new Schedule();
            schedule.teacher_id = staffCode;
            schedule.academic_year = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
            schedule.cours_id = ddlCourse.SelectedValue == "-1" ? 0 : int.Parse(ddlCourse.SelectedValue);
            schedule.class_id = ddlClassroom.SelectedValue == "-1" ? 0 : int.Parse(ddlClassroom.SelectedValue);
            //
            DateTime fromDate = radFromDate.SelectedDate.Value;
            DateTime toDate = radToDate.SelectedDate.Value;
            int days = int.Parse((toDate - fromDate).TotalDays.ToString());

            //get list of schedule for current teacher
            List<Schedule> listScheduleCours = Schedule.getListScheduleTeacherForTimesheet(schedule);

            if (listScheduleCours != null && listScheduleCours.Count > 0)
            {
                List<Schedule> newListCoursInfo = new List<Schedule>();
                //
                foreach (Schedule _scheduleCoursInfo in listScheduleCours)
                {
                    DateTime _sheetDate = fromDate;
                    // check info from timesheets
                    for (int i = 0; i <= days; i++)
                    {
                        // get new sheet date by looping
                        _scheduleCoursInfo.sheet_date = _sheetDate.AddDays(i);


                        Schedule _newScheduleCoursInfo = new Schedule();
                        //
                        List<Timesheet> listSheets = Timesheet.getListAffectedTimesheetsForCourses(_scheduleCoursInfo);

                        if (listSheets != null && listSheets.Count > 0)
                        {
                            string _newDays = _scheduleCoursInfo.sheet_date.ToString("dddd").ToUpper().Substring(0, 2);

                            if (_scheduleCoursInfo.days_code == _newDays)
                            {
                                _newScheduleCoursInfo.id = _scheduleCoursInfo.id;
                                _newScheduleCoursInfo.class_name = _scheduleCoursInfo.class_name;
                                _newScheduleCoursInfo.days = _scheduleCoursInfo.days;
                                _newScheduleCoursInfo.days_code = _scheduleCoursInfo.days_code;
                                _newScheduleCoursInfo.cours_name = _scheduleCoursInfo.cours_name;
                                _newScheduleCoursInfo.teacher_id = _scheduleCoursInfo.teacher_id;
                                _newScheduleCoursInfo.start_hour = _scheduleCoursInfo.start_hour;
                                _newScheduleCoursInfo.end_hour = _scheduleCoursInfo.end_hour;
                                _newScheduleCoursInfo.cours_id = _scheduleCoursInfo.cours_id;
                                _newScheduleCoursInfo.total_hour = _scheduleCoursInfo.total_hour;
                                _newScheduleCoursInfo.academic_year = _scheduleCoursInfo.academic_year;
                                _newScheduleCoursInfo.class_id = _scheduleCoursInfo.class_id;
                                _newScheduleCoursInfo.presence_status = listSheets[0].presence_status;
                                _newScheduleCoursInfo.validation_status = listSheets[0].validation_status;
                                _newScheduleCoursInfo.absence_reason = listSheets[0].absence_reason_id;
                                _newScheduleCoursInfo.sheet_date = listSheets[0].sheet_date;
                                _newScheduleCoursInfo.sheet_date_inserted = _newScheduleCoursInfo.sheet_date.ToString("MM/dd/yyyy");
                                newListCoursInfo.Add(_newScheduleCoursInfo);
                            }
                        }
                        else
                        {
                            string _newDays = _sheetDate.AddDays(i).ToString("dddd").ToUpper().Substring(0, 2);

                            if (_scheduleCoursInfo.days_code == _newDays)
                            {
                                _newScheduleCoursInfo.id = _scheduleCoursInfo.id;
                                _newScheduleCoursInfo.class_name = _scheduleCoursInfo.class_name;
                                _newScheduleCoursInfo.days = _scheduleCoursInfo.days;
                                _newScheduleCoursInfo.days_code = _scheduleCoursInfo.days_code;
                                _newScheduleCoursInfo.cours_name = _scheduleCoursInfo.cours_name;
                                _newScheduleCoursInfo.teacher_id = _scheduleCoursInfo.teacher_id;
                                _newScheduleCoursInfo.start_hour = _scheduleCoursInfo.start_hour;
                                _newScheduleCoursInfo.end_hour = _scheduleCoursInfo.end_hour;
                                _newScheduleCoursInfo.cours_id = _scheduleCoursInfo.cours_id;
                                _newScheduleCoursInfo.total_hour = _scheduleCoursInfo.total_hour;
                                _newScheduleCoursInfo.academic_year = _scheduleCoursInfo.academic_year;
                                _newScheduleCoursInfo.class_id = _scheduleCoursInfo.class_id;
                                _newScheduleCoursInfo.presence_status = 0;
                                _newScheduleCoursInfo.validation_status = 0;
                                _newScheduleCoursInfo.absence_reason = 1;
                                _newScheduleCoursInfo.sheet_date = _sheetDate.AddDays(i);
                                _newScheduleCoursInfo.sheet_date_inserted = _newScheduleCoursInfo.sheet_date.ToString("MM/dd/yyyy");
                                newListCoursInfo.Add(_newScheduleCoursInfo);
                            }
                        }

                    }
                }

                gridListTeacher.DataSource = newListCoursInfo;
                gridListTeacher.DataBind();

                //loop the gridview to select a reason or presence checkbox
                if (gridListTeacher.Rows.Count > 0)
                {
                    for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                    {
                        RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                        combo.SelectedValue = newListCoursInfo[i].absence_reason.ToString();
                        if (newListCoursInfo[i].presence_status == 1)
                        {
                            CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceTeacher"));
                            chk.Checked = true;
                        }

                        // change gridview background color when student not come to school
                        if (newListCoursInfo[i].presence_status == 0 && newListCoursInfo[i].validation_status == 1)
                        {
                            gridListTeacher.Rows[i].BackColor = Color.Red;
                            gridListTeacher.Rows[i].ForeColor = Color.White;
                            gridListTeacher.Rows[i].Font.Bold = true;
                        }
                    }
                }

                // SHOW or hide items.
                lblFoundTeacher.Visible = false;
                lnkExportExcel.Visible = true;
                lblCounterTeacher.Visible = true;
                lblCounterTeacher.Text = newListCoursInfo.Count.ToString() + " Ligne(s)";
                gridListTeacher.Visible = true;
            }
            else
            {
                lblFoundTeacher.Visible = true;
                lblCounterTeacher.Visible = false;
                gridListTeacher.Visible = false;
                //divGridHeaderTeacher.Visible = false;
                //btnInsertAll.Visible = false;
                //tblGridHeaderTeacher.Visible = false;
                gridListTeacher.DataSource = listScheduleCours;
                gridListTeacher.DataBind();
                gridListTeacher.Visible = false;
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void gridListTeacher_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListTeacher.Rows[index];
    }

    protected void gridListTeacher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e != null)
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    if (e.Row.RowIndex == 0)
                //        //     e.Row.Style.Add("height", "30px");
                //        e.Row.Style.Add("vertical-align", "bottom");
                //}

                // get the combobox
                RadComboBox cboReason = (e.Row.FindControl("cboReasonType") as RadComboBox);
                List<Timesheet> listReason = Timesheet.getListAllReasonForCombo();
                //
                if (listReason != null)
                {
                    if (listReason.Count > 0)
                    {
                        cboReason.DataValueField = "id";
                        cboReason.DataTextField = "description";
                        cboReason.DataSource = listReason;
                        cboReason.DataBind();
                        cboReason.SelectedValue = "1";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        // close window
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ClientCloseSearchStudent();", true);
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void cboReasonType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combo = sender as RadComboBox;
        GridViewRow row = (combo).Parent.Parent as GridViewRow;
        int index = row.RowIndex;
        CheckBox chk = gridListTeacher.Rows[index].FindControl("chkPresenceTeacher") as CheckBox;
        if (combo.SelectedValue == "1")
        {
            chk.Checked = true;
        }
        else
        {
            chk.Checked = false;
        }
        chk.Focus();
    }

    protected void btnInsertAll_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            string _weekDay = string.Empty; // radDateTimesheet.SelectedDate.Value.DayOfWeek.ToString().Substring(0, 2).ToUpper();
                                            //
            Timesheet timesheet = new Timesheet();
            List<Timesheet> listTimesheet = new List<Timesheet>();
            //
            //check if gridview contains value
            if (gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    if (Session["staff_code"] != null)
                    {
                        timesheet.staff_code = Session["staff_code"].ToString();
                    }
                    // timesheet.staff_code = _StaffCode.Length <= 0 ? string.Empty : _StaffCode.Trim();
                    HiddenField hiddenDaysCode = (HiddenField)(gridListTeacher.Rows[i].FindControl("hiddenDaysCode"));
                    HiddenField hiddenSheetDate = (HiddenField)(gridListTeacher.Rows[i].FindControl("hiddenSheetDate"));
                    HiddenField hiddenClassroomId = (HiddenField)(gridListTeacher.Rows[i].FindControl("hiddenClassroomId"));
                    HiddenField hiddenCoursId = (HiddenField)(gridListTeacher.Rows[i].FindControl("hiddenCoursId"));


                    timesheet.week_day = hiddenDaysCode.Value;
                    timesheet.sheet_date = DateTime.Parse(hiddenSheetDate.Value.ToString());
                    timesheet.class_id = int.Parse(hiddenClassroomId.Value.ToString());
                    timesheet.cours_id = int.Parse(hiddenCoursId.Value.ToString());

                    string _timeIn = gridListTeacher.Rows[i].Cells[5].Text.Trim().Replace(" & nbsp;", "");
                    string _timeOut = gridListTeacher.Rows[i].Cells[6].Text.Trim().Replace(" & nbsp;", "");
                    timesheet.time_in = _timeIn.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeIn);
                    timesheet.time_out = _timeOut.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeOut);

                    string _testWorkHours = gridListTeacher.Rows[i].Cells[7].Text.Replace("&nbsp;", "");

                    timesheet.total_work_hour = double.Parse(gridListTeacher.Rows[i].Cells[7].Text.Replace("&nbsp;", ""));


                    // checkbox for presence or absence
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceTeacher"));
                    RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                    if (chk.Checked || combo.SelectedValue == "1")
                    {
                        // present
                        timesheet.presence_status = 1;
                    }
                    else
                    {
                        // absent
                        timesheet.presence_status = 0;
                    }

                    timesheet.absence_reason_id = Convert.ToInt32(combo.SelectedValue);
                    timesheet.validation_status = 1;
                    timesheet.academic_year_id = int.Parse(ddlAcademicYear.SelectedValue);
                    timesheet.login_user_id = user.id;

                    // check timesheets
                    if (Timesheet.timesheetsExistForCours(timesheet))
                    {
                        // update exsisted timesheets
                        Timesheet.updateTimesheets(timesheet);
                        // code.Timesheet.validateTimesheetsTeacher(timesheet);
                    }
                    else
                    {
                        // make new timesheets
                        // Timesheet.InsertStudentTimesheets(timesheet);
                        //code.Timesheet.validateTimesheetsTeacher(timesheet);
                    }
                }
                string staffCode = lblTeacherId.Text.Trim().ToUpper();
                loadTeacherInfo(staffCode);
                MessageAlert.RadAlert("Success !", 300, 150, "Information", null);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    //protected void btnValidateAll_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        RadDatePicker radPresenceDate = null;
    //        if (Session["date_register"] != null)
    //        {
    //            radPresenceDate = (RadDatePicker)Session["date_register"];
    //        }

    //        string _weekDay = radPresenceDate.SelectedDate.Value.DayOfWeek.ToString().Substring(0, 2).ToUpper();
    //        code.Timesheet timesheet = new code.Timesheet();
    //        List<code.Timesheet> listTimesheet = new List<code.Timesheet>();
    //        //check if gridview contains value
    //        if (gridListTeacher.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < gridListTeacher.Rows.Count; i++)
    //            {
    //                if (Session["staff_code"] != null)
    //                {
    //                    timesheet.staff_code = Session["staff_code"].ToString();
    //                }
    //                //
    //                timesheet.cours_id = gridListTeacher.Rows[i].Cells[1].Text.Replace("&nbsp;", "").Length <= 0 ?
    //                                          0 : code.Course.getCourseIdByName(gridListTeacher.Rows[i].Cells[1].Text.Replace("&nbsp;", ""));
    //                // vacation
    //                string _vacation = gridListTeacher.Rows[i].Cells[3].Text.Replace("&nbsp;", "").Length <= 0 ?
    //                                          string.Empty : gridListTeacher.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
    //                if (_vacation != string.Empty || _vacation != null)
    //                {
    //                    switch (_vacation)
    //                    {
    //                        case "Matin": timesheet.vacation = "AM"; break;
    //                        case "Median": timesheet.vacation = "PM"; break;
    //                        case "Soir": timesheet.vacation = "NG"; break;
    //                        case "Weekend": timesheet.vacation = "WK"; break;
    //                        default: timesheet.vacation = string.Empty; break;
    //                    }
    //                }

    //                timesheet.class_id = gridListTeacher.Rows[i].Cells[4].Text.Replace("&nbsp;", "").Length <= 0 ?
    //                                     0 : ClassRoom.getClassroomIdByName(gridListTeacher.Rows[i].Cells[4].Text.Replace("&nbsp;", ""));
    //                string _timeIn = gridListTeacher.Rows[i].Cells[4].Text.Trim().Replace(" & nbsp;", "");
    //                string _timeOut = gridListTeacher.Rows[i].Cells[5].Text.Trim().Replace(" & nbsp;", "");
    //                timesheet.time_in = _timeIn.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeIn);
    //                timesheet.time_out = _timeOut.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeOut);
    //                timesheet.total_work_hour = gridListTeacher.Rows[i].Cells[5].Text.Replace("&nbsp;", "").Length <= 0 ?
    //                                          0 : Double.Parse(gridListTeacher.Rows[i].Cells[6].Text.Replace("&nbsp;", ""));
    //                timesheet.week_day = _weekDay;
    //                timesheet.validation_status = 1;
    //                timesheet.academic_year_id = Universal.getAcademicYear();
    //                timesheet.date_register = radPresenceDate.SelectedDate.Value;
    //                //add timesheet to db
    //                code.Timesheet.validateTimesheetsTeacher(timesheet);

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Error : " + ex.Message);
    //    }
    //}

    protected void radDateTimesheet_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //chkAll.Checked = false;
        string staffCode = lblTeacherId.Text.Trim().ToUpper();
        //if (radDateTimesheet.SelectedDate > DateTime.Now)
        //{
        //    radDateTimesheet.SelectedDate = DateTime.Now;
        //    MessageAlert.RadAlert("Erreur : Date invalide !", 300, 150, "Erreur", null);
        //}
        loadTeacherInfo(staffCode);
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkBtn = sender as CheckBox;
        if (chkBtn.Checked) // select all
        {
            // check all checkboxes in gridview
            if (gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceState"));
                    chk.Checked = true;
                    RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                    combo.SelectedValue = "1";
                }
            }
        }
        else  // unselect all
        {
            // check all checkboxes in gridview
            if (gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceState"));
                    chk.Checked = false;
                    RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                    combo.SelectedValue = "1";
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataGridTeacher();
    }

    private void loadListAcademicYear(RadComboBox ddl)
    {
        try
        {
            ddl.Items.Clear();
            // get list all academic  year
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();

            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
                int maxAcademicYear = Settings.getAcademicYear();
                if (maxAcademicYear != 0)
                {
                    ddl.SelectedValue = maxAcademicYear.ToString();
                }
            }

            //ddl.Items.Insert(0, new DropDownListItem("--Selectionnner--", "-1"));
        }
        catch (Exception ex) { }
    }

    private void loadListCourseByTeacher()
    {
        try
        {
            string teacherCode = Session["staff_code"].ToString();
            int academicYear = int.Parse(ddlAcademicYear.SelectedValue);
            //
            ddlCourse.Items.Clear();
            // get list all academic  year
            List<Course> listCourse = Course.getListScheduleCourseByTeachId(teacherCode, academicYear);

            if (listCourse != null && listCourse.Count > 0)
            {
                ddlCourse.DataValueField = "id";
                ddlCourse.DataTextField = "name";
                ddlCourse.DataSource = listCourse;
                ddlCourse.DataBind();
            }

            ddlCourse.Items.Insert(0, new RadComboBoxItem("--Selectionnner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

    private void loadListClassByTeacher()
    {
        try
        {
            string teacherCode = Session["staff_code"].ToString();
            int academicYear = int.Parse(ddlAcademicYear.SelectedValue);
            //
            ddlClassroom.Items.Clear();
            // get list all academic  year
            List<ClassRoom> listClass = ClassRoom.getListScheduleCourseByTeachId(teacherCode, academicYear);

            if (listClass != null && listClass.Count > 0)
            {
                ddlClassroom.DataValueField = "id";
                ddlClassroom.DataTextField = "name";
                ddlClassroom.DataSource = listClass;
                ddlClassroom.DataBind();
            }

            ddlClassroom.Items.Insert(0, new RadComboBoxItem("--Selectionnner--", "-1"));
            ddlClassroom.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int academicYearId = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
        List<Universal> listAcademicYear2 = Universal.getListAcademicYearFullInfoById(academicYearId);
        if (listAcademicYear2 != null && listAcademicYear2.Count > 0)
        {
            //radFromDate.SelectedDate = DateTime.Parse(listAcademicYear2[0].start_date);
            //radToDate.SelectedDate = DateTime.Parse(listAcademicYear2[0].start_date).AddDays(5); // DateTime.Parse(listAcademicYear2[0].end_date);
            BindDataGridTeacher();
        }
    }

    protected void chkPresenceStateAll_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkBtn = sender as CheckBox;
        if (chkBtn.Checked) // select all
        {
            // check all checkboxes in gridview
            if (gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceTeacher"));
                    chk.Checked = true;
                    RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                    combo.SelectedValue = "1";
                }
            }
        }
        else  // unselect all
        {
            // check all checkboxes in gridview
            if (gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceTeacher"));
                    chk.Checked = false;
                    RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                    combo.SelectedValue = "1";
                }
            }
        }

    }

    protected void chkPresenceTeacher_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = sender as CheckBox;
            GridViewRow row = (chk).Parent.Parent as GridViewRow;
            int index = row.RowIndex;
            RadComboBox combo = gridListTeacher.Rows[index].FindControl("cboReasonType") as RadComboBox;
            if (chk.Checked)
            {
                combo.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void radFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //// get academic year id
        //int id = int.Parse(ddlAcademicYear.SelectedValue);
        //string startDate = radFromDate.SelectedDate.Value.ToString("yyyyMMdd");
        //if (!Universal.isValidAcademicStartYear(startDate, id)
        //    || radFromDate.SelectedDate > radToDate.SelectedDate)
        //{
        //    ddlAcademicYear_SelectedIndexChanged(this, null);
        //    MessBox.Show("Erreur : Date Invalide");
        //}


        if (radFromDate.SelectedDate > DateTime.Now)
        {
            radFromDate.SelectedDate = DateTime.Now;
            MessBox.Show("Erreur : Date Invalide");
        }
    }

    protected void radToDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //// get academic year id
        //int id = int.Parse(ddlAcademicYear.SelectedValue);
        //string endDate = radFromDate.SelectedDate.Value.ToString("yyyyMMdd");
        //if (radToDate.SelectedDate > radFromDate.SelectedDate)
        //{
        //    ddlAcademicYear_SelectedIndexChanged(this, null);
        //    MessBox.Show("Erreur : Date Invalide");
        //}
        //else if (Universal.isNotValidAcademicEndYear(endDate, id))
        //{
        //    ddlAcademicYear_SelectedIndexChanged(this, null);
        //    MessBox.Show("Erreur : Date Invalide");
        //}

        if (radToDate.SelectedDate > DateTime.Now)
        {
            radToDate.SelectedDate = DateTime.Now;
            MessBox.Show("Erreur : Date Invalide");
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

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\Feuille_presence_professeur_{1}_{2}.xls",
                userName, userName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            gridListTeacher.AllowPaging = false;
            gridListTeacher.HeaderStyle.Font.Bold = true;
            gridListTeacher.GridLines = GridLines.Vertical;
            //gridListPolicy.DataBind();
            BindDataGridTeacher();
            //GetSalaryTable();
            gridListTeacher.HeaderRow.Visible = true;

            if (gridListTeacher.Visible == true)
            {
                gridListTeacher.HeaderRow.Cells[0].Visible = false;
                //
                gridListTeacher.HeaderRow.Cells[1].Width = 100;
                gridListTeacher.HeaderRow.Cells[2].Width = 100;
                gridListTeacher.HeaderRow.Cells[4].Width = 100;
                gridListTeacher.HeaderRow.Cells[5].Width = 200;
                gridListTeacher.HeaderRow.Cells[6].Width = 100;
                gridListTeacher.HeaderRow.Cells[7].Width = 100;
                gridListTeacher.HeaderRow.Cells[8].Width = 100;
                gridListTeacher.HeaderRow.Cells[9].Width = 100;
                gridListTeacher.HeaderRow.Cells[10].Width = 100;
                //

                gridListTeacher.RowStyle.Height = 20;

                // remove check all checkbox from header
                System.Web.UI.WebControls.CheckBox chkAll = ((System.Web.UI.WebControls.CheckBox)gridListTeacher.HeaderRow.Cells[10].FindControl("chkPresenceStateAll"));
                chkAll.Visible = false;

                // setup header background color to navy
                gridListTeacher.HeaderRow.Cells[1].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[2].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[3].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[4].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[5].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[6].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[7].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[8].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[9].BackColor = Color.Navy;
                gridListTeacher.HeaderRow.Cells[10].BackColor = Color.Navy;

                // setup header forecolor to white
                gridListTeacher.HeaderRow.Cells[1].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[2].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[3].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[4].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[5].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[6].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[7].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[8].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[9].ForeColor = Color.White;
                gridListTeacher.HeaderRow.Cells[10].ForeColor = Color.White;

                //
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    GridViewRow row = gridListTeacher.Rows[i];
                    row.Cells[0].Visible = false;
                    row.BackColor = Color.Transparent;
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
                    //
                    System.Web.UI.WebControls.Image activationImg = ((System.Web.UI.WebControls.Image)row.Cells[8].FindControl("validationImage"));
                    row.Cells[8].Text = activationImg.ImageUrl.Contains("red_status") ? "Non-Valide" : "Valide";
                    row.Cells[8].BorderWidth = 1;
                    //
                    RadComboBox cboReason = ((RadComboBox)row.Cells[9].FindControl("cboReasonType"));
                    row.Cells[9].Text = cboReason.SelectedItem.Text == "--Tout Sélectionner--" ? string.Empty : cboReason.SelectedItem.Text;
                    row.Cells[9].BorderWidth = 1;
                    //
                    System.Web.UI.WebControls.CheckBox chkPresence = ((System.Web.UI.WebControls.CheckBox)row.Cells[10].FindControl("chkPresenceTeacher"));
                    row.Cells[10].Text = chkPresence.Checked ? "Present(e)" : "Abscent(e)";
                    row.Cells[10].BorderWidth = 1;

                    // change back and fore color for row
                    if (!chkPresence.Checked)
                    {
                        row.ForeColor = System.Drawing.Color.Black;
                        row.Font.Bold = false;
                        row.Cells[10].BackColor = System.Drawing.Color.Red;
                        row.Cells[10].ForeColor = System.Drawing.Color.White;
                    }

                }
                gridListTeacher.RenderControl(htmlWrite);
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
            gridListTeacher.HeaderRow.Visible = false;
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // do nothing
    }
}