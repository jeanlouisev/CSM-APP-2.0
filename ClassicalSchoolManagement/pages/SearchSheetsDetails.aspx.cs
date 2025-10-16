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



public partial class SearchSheetsDetails : System.Web.UI.Page
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
                radFromDate.SelectedDate = DateTime.Now.AddDays(-8);
                radToDate.SelectedDate = DateTime.Now;
                //
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
                pnlTeacherInfo.Visible = true;
                lblFullname.Text = fullName.ToUpper();
                lblTeacherId.Text = staffCode.ToUpper();
                BindDataGridTeacher(staffCode.ToUpper());
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private void BindDataGridTeacher(string staffCode)
    {
        try
        {
            // create an object of type schedule
            Schedule schedule = new Schedule();
            schedule.teacher_id = staffCode;
            //schedule.register_date_timesheet = radFromDate.SelectedDate.Value;
            //schedule.days = radFromDate.SelectedDate.Value.DayOfWeek.ToString().Substring(0, 2).ToUpper();
            schedule.from_date = radFromDate.SelectedDate.Value;
            schedule.to_date = radToDate.SelectedDate.Value;

            //get list of schedule for current teacher
            List<Schedule> listResult = new List<Schedule>();
            listResult = Schedule.SearchTeacherTimesheet(schedule);

            if (listResult.Count > 0)
            {
                lblFoundTeacher.Visible = false;
                lblCounterTeacher.Visible = true;
                lblCounterTeacher.Text = listResult.Count.ToString() + " Ligne(s)";
                divGridHeaderTeacher.Visible = true;
                tblGridHeaderTeacher.Visible = true;
                gridListTeacher.DataSource = listResult;
                gridListTeacher.DataBind();
                gridListTeacher.Visible = true;

                //loop the gridview to select a reason or presence checkbox
                ////if (gridListTeacher.Rows.Count > 0)
                ////{
                ////    for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                ////    {
                ////        //RadComboBox combo = (RadComboBox)(gridListTeacher.Rows[i].FindControl("cboReasonType"));
                ////        //combo.SelectedValue = listResult[i].absence_reason.ToString();
                ////        if (listResult[i].presence_status == 1)
                ////        {
                ////            CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceState"));
                ////            chk.Checked = true;
                ////        }
                ////    }
                ////}
            }
            else
            {
                lblFoundTeacher.Visible = true;
                lblCounterTeacher.Visible = false;
                gridListTeacher.Visible = false;
                divGridHeaderTeacher.Visible = false;
                //btnInsertAll.Visible = false;
                tblGridHeaderTeacher.Visible = false;
                gridListTeacher.DataSource = listResult;
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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowIndex == 0)
                        //     e.Row.Style.Add("height", "30px");
                        e.Row.Style.Add("vertical-align", "bottom");
                }

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

    protected void chkPresenceState_CheckedChanged(object sender, EventArgs e)
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

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void cboReasonType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combo = sender as RadComboBox;
        GridViewRow row = (combo).Parent.Parent as GridViewRow;
        int index = row.RowIndex;
        CheckBox chk = gridListTeacher.Rows[index].FindControl("chkPresenceState") as CheckBox;
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
            string _weekDay = radFromDate.SelectedDate.Value.DayOfWeek.ToString().Substring(0, 2).ToUpper();
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
                    timesheet.cours_id = gridListTeacher.Rows[i].Cells[1].Text.Replace("&nbsp;", "").Length <= 0 ?
                                              0 : Course.getCourseIdByName(gridListTeacher.Rows[i].Cells[1].Text.Replace("&nbsp;", ""));
                    timesheet.class_id = gridListTeacher.Rows[i].Cells[2].Text.Replace("&nbsp;", "").Length <= 0 ?
                                              0 : ClassRoom.getClassroomIdByName(gridListTeacher.Rows[i].Cells[2].Text.Replace("&nbsp;", ""));
                    //// vacation
                    //string _vacation = gridListTeacher.Rows[i].Cells[3].Text.Replace("&nbsp;", "").Length <= 0 ?
                    //                          string.Empty : gridListTeacher.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                    //if (_vacation != string.Empty || _vacation != null)
                    //{
                    //    switch (_vacation)
                    //    {
                    //        case "Matin": timesheet.vacation = "AM"; break;
                    //        case "Median": timesheet.vacation = "PM"; break;
                    //        case "Soir": timesheet.vacation = "NG"; break;
                    //        case "Weekend": timesheet.vacation = "WK"; break;
                    //        default: timesheet.vacation = string.Empty; break;
                    //    }
                    //}
                    timesheet.vacation = null;
                    string _timeIn = gridListTeacher.Rows[i].Cells[4].Text.Trim().Replace(" & nbsp;", "");
                    string _timeOut = gridListTeacher.Rows[i].Cells[5].Text.Trim().Replace(" & nbsp;", "");
                    timesheet.time_in = _timeIn.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeIn);
                    timesheet.time_out = _timeOut.Length <= 0 ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(_timeOut);
                    //timesheet.total_work_hour = gridListTeacher.Rows[i].Cells[6].Text.Replace("&nbsp;", "").Length <= 0 ?
                    //                          0 : Double.Parse(gridListTeacher.Rows[i].Cells[7].Text.Replace("&nbsp;", ""));

                    // checkbox for presence or absence
                    CheckBox chk = (CheckBox)(gridListTeacher.Rows[i].FindControl("chkPresenceState"));
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
                    timesheet.week_day = _weekDay;
                    timesheet.validation_status = 1;
                    timesheet.academic_year_id = Settings.getAcademicYear();
                    timesheet.login_user_id = user.id;
                    timesheet.date_register = radFromDate.SelectedDate.Value;
                    //

                    // check timesheets
                    if (Timesheet.timesheetsExistForTeacher(timesheet))
                    {
                        // update exsisted timesheets
                        Timesheet.updateTimesheets(timesheet);
                        // code.Timesheet.validateTimesheetsTeacher(timesheet);
                    }
                    else
                    {
                        // make new timesheets
                        //Timesheet.InsertStudentTimesheets(timesheet);
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

    //protected void radFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    //{
    //    chkAll.Checked = false;
    //    string staffCode = lblTeacherId.Text.Trim().ToUpper();
    //    if (radFromDate.SelectedDate > DateTime.Now)
    //    {
    //        radFromDate.SelectedDate = DateTime.Now;
    //        MessageAlert.RadAlert("Erreur : Date invalide !", 300, 150, "Erreur", null);
    //    }
    //    loadTeacherInfo(staffCode);
    //}

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

    protected void radFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        if (radFromDate.SelectedDate > DateTime.Now
            || radFromDate.SelectedDate > radToDate.SelectedDate)
        {
            MessBox.Show("Erreur : date debut invalide !");
            pnlTeacherInfo.Visible = false;
        }
        else
        {
            pnlTeacherInfo.Visible = true;
            string teacherId = lblTeacherId.Text.Trim();
            BindDataGridTeacher(teacherId);
        }
    }

    protected void radToDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        if (radToDate.SelectedDate > DateTime.Now
            || radToDate.SelectedDate < radFromDate.SelectedDate)
        {
            MessBox.Show("Erreur : date fin invalide !");
            pnlTeacherInfo.Visible = false;
        }
        else
        {
            pnlTeacherInfo.Visible = true;
            string teacherId = lblTeacherId.Text.Trim();
            BindDataGridTeacher(teacherId);
        }
    }
}
