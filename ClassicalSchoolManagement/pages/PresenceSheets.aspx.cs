using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Utilities;
using System.Collections.Generic;
using System.IO;
using Telerik.Web.UI;
using System.Drawing;



public partial class PresenceSheet : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;
    string msgContent = null;

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
            Users user = Session["user"] as Users;
            //
            radSheetDateStudent.SelectedDate = DateTime.Now;
            radSheetDateStudent.MaxDate = DateTime.Now;

            radFromDateReportSt.SelectedDate = DateTime.Now;
            radFromDateReportSt.MaxDate = DateTime.Now;

            radToDateReportSt.SelectedDate = DateTime.Now;
            radToDateReportSt.MaxDate = DateTime.Now;
            //
            loadActiveClassroom();
            loadPredefinedReason();
            Session["reason_list"] = null;
            Session.Remove("reason_list");

            //
            //loadListStaffs();
            //loadListYears();
            //
            radWeeks.SelectedDate = DateTime.Now;
            //cboMonth.SelectedValue = DateTime.Now.Month.ToString();
            //weeklyReportDate.SelectedDate = DateTime.Now;
            // prevent user from selecting future dates
            radWeeks.MaxDate = DateTime.Now;


            //weeklyReportDate.MaxDate = DateTime.Now;
            //
            //bntExportExcelReport.Attributes.Add("disabled", "disabled");
            bindDataGridTimesheetsStaff();


        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            //btnInsertAll.Enabled = false;
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
            //// loop through the grid to disable edit option
            //if (gridListStudent.Visible && gridListStudent.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListStudent.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn1 = gridListStudent.Rows[i].Cells[8].FindControl("btnDelete") as ImageButton;
            //        imgBtn1.Enabled = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    private void loadPredefinedReason()
    {
        List<Timesheet> listReason = Timesheet.getListReasonById(1);
        if (listReason == null || listReason.Count <= 0)
        {
            // add new reason_type
            Timesheet.addReason(1, "");
        }
    }

    private void loadActiveClassroom()
    {
        List<ClassRoom> listResult = ClassRoom.getListActiveClassroom();
        if (listResult != null && listResult.Count > 0)
        {
            // FP STUDENT
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
            ddlClassroom.DataSource = listResult;
            ddlClassroom.DataBind();


            // RP STUDENT
            ddlClassroomReportSt.DataValueField = "id";
            ddlClassroomReportSt.DataTextField = "name";
            ddlClassroomReportSt.DataSource = listResult;
            ddlClassroomReportSt.DataBind();
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }


    /********************* STUDENT ************************/

    public void deleteTImeSheetStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = null; // gridListStudent.Rows[index];
                string code = row.Cells[1].Text;
                DateTime timesheetDate = DateTime.Parse(row.Cells[5].Text);
                Timesheet.deleteTimeSheetsByCodeAndDate(code, timesheetDate);
                //refresh data of the gridview
                //BindDataGridAll();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void bindGridStudents()
    {
        // get values
        int classId = int.Parse(ddlClassroom.SelectedValue);
        string vacation = ddlVacation.SelectedValue;
        DateTime sheetDate = radSheetDateStudent.SelectedDate.Value;


        List<Timesheet> listSheets = Timesheet.getListTimesheetsStudent(classId, vacation, sheetDate);

        radGridStudentTimesheets.DataSource = listSheets;
        radGridStudentTimesheets.DataBind();



        /*
        Student st = new Student();
        //st.class_id = int.Parse(classRoom);
        st.vacation_code = vacation;
        st.register_date_timesheet = radSheetDateStudent.SelectedDate.Value;
        //
        // Button btnView = (Button)gridListStudent.FindControl("btnViewTimesheets");
        //CheckBox chkPres = (CheckBox)gridListStudent.FindControl("chkPresenceStudent");


        //get list of students
        List<Student> listStudent = Student.getListStudentForTimesheets(st);
        if (listStudent != null && listStudent.Count > 0)
        {
            List<Student> newListStudent = new List<Student>();
            foreach (Student student in listStudent)
            {                  // check info from timesheets

                Student _newStudentInfo = new Student();
                List<Timesheet> listSheets = Timesheet.getListTimesheetsStudent();
                if (listSheets != null && listSheets.Count > 0)
                {
                    _newStudentInfo.student_code = student.id;
                    _newStudentInfo.fullName = student.fullName;
                    _newStudentInfo.class_name = student.class_name;
                    _newStudentInfo.class_id = student.class_id;
                    _newStudentInfo.vacation = student.vacation;
                    _newStudentInfo.presence_status = listSheets[0].presence_status;
                    _newStudentInfo.validation_status = listSheets[0].validation_status;
                    _newStudentInfo.absence_reason_id = listSheets[0].absence_reason_id;
                    _newStudentInfo.sheet_date = sheetDate;
                }
                else
                {
                    _newStudentInfo.student_code = student.id;
                    _newStudentInfo.fullName = student.fullName;
                    _newStudentInfo.class_name = student.class_name;
                    _newStudentInfo.class_id = student.class_id;
                    _newStudentInfo.vacation = student.vacation;
                    _newStudentInfo.presence_status = 0;
                    _newStudentInfo.validation_status = 0;
                    _newStudentInfo.absence_reason_id = -1;
                    _newStudentInfo.sheet_date = sheetDate;
                }

                newListStudent.Add(_newStudentInfo);
                //_sheetDate.AddDays(-1);
            }
            //}
          



    }
        else
        {
            radGridStudentTimesheets.DataSource = listStudent;
            radGridStudentTimesheets.DataBind();
        }  */
    }

    protected void radGridStudentTimesheets_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridStudentTimesheets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridStudentTimesheets.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
            //


            // absence reason
            if (Session["reason_list"] != null)
            {
                // get the combobox
                RadComboBox cboReason = (RadComboBox)item.FindControl("cboReasonTypeStudent");
                RadComboBox cboPresenceStatusStudent = (RadComboBox)item.FindControl("cboPresenceStatusStudent");
                HiddenField hiddenAbsenceReasonId = (HiddenField)item.FindControl("hiddenAbsenceReasonId");
                HiddenField hiddenPresenceStatus = (HiddenField)item.FindControl("hiddenPresenceStatus");

                List<Timesheet> listReason = Session["reason_list"] as List<Timesheet>;
                if (listReason != null && listReason.Count > 0)
                {
                    cboReason.DataValueField = "id";
                    cboReason.DataTextField = "description";
                    cboReason.DataSource = listReason;
                    cboReason.DataBind();
                }
                //
                // absence reason
                cboReason.SelectedValue = hiddenAbsenceReasonId.Value;


                if (hiddenPresenceStatus.Value != null)
                {
                    // check for absent students
                    int pStatus = int.Parse(hiddenPresenceStatus.Value);
                    if (pStatus != 0)
                    {
                        cboPresenceStatusStudent.SelectedValue = hiddenPresenceStatus.Value;

                        if (pStatus == (int)Timesheet.PRESENCE_STATE.ABSENT)
                        {
                            // Motifs Absence	
                            item.Cells[9].BackColor = Color.Red;
                            // Status
                            item.Cells[10].BackColor = Color.Red;
                            cboReason.Enabled = true;
                        }
                    }
                }

            }

            // sheet_date
            Label lblSheetDate = (Label)item.FindControl("lblSheetDate");
            HiddenField hiddenSheeDateStudent = (HiddenField)item.FindControl("hiddenSheeDateStudent");
            //
            lblSheetDate.Text = radSheetDateStudent.SelectedDate.Value.ToString("dd/MM/yyyy");
            hiddenSheeDateStudent.Value = radSheetDateStudent.SelectedDate.Value.ToString();


        }
    }

    protected void btnValidateStudent_ServerClick(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            //string _weekDay = radSheetDateStudent.SelectedDate.Value.DayOfWeek.ToString().Substring(0, 2).ToUpper();

            List<Timesheet> tList = new List<Timesheet>();
            //check if gridview contains value
            if (radGridStudentTimesheets.MasterTableView.Items.Count > 0)
            {
                foreach (GridDataItem item in radGridStudentTimesheets.MasterTableView.Items)
                {
                    Timesheet t = new Timesheet();

                    HiddenField hiddenClassroomId = (HiddenField)item.FindControl("hiddenClassroomId");
                    HiddenField hiddenVacation = (HiddenField)item.FindControl("hiddenVacation");
                    HiddenField hiddenSheeDateStudent = (HiddenField)item.FindControl("hiddenSheeDateStudent");
                    RadComboBox cboReasonTypeStudent = (RadComboBox)item.FindControl("cboReasonTypeStudent");
                    RadComboBox cboPresenceStatusStudent = (RadComboBox)item.FindControl("cboPresenceStatusStudent");
                    DateTime _sheetDate = DateTime.Parse(hiddenSheeDateStudent.Value);
                    //string _vacation = null;
                    //switch (hiddenVacation.Value)
                    //{
                    //    case "Matin": _vacation = "AM"; break;
                    //    case "Mediant": _vacation = "PM"; break;
                    //    case "Soir": _vacation = "NG"; break;
                    //    case "Weekend": _vacation = "AM"; break;
                    //}
                    //int validationStatus = cboReasonTypeStudent.SelectedValue == "-1" ? (int)Timesheet.status.NOT_VALID : (int)Timesheet.status.VALID;
                    //
                    t.student_code = item.GetDataKeyValue("student_code").ToString();
                    t.class_id = int.Parse(hiddenClassroomId.Value);
                    t.vacation = hiddenVacation.Value;
                    t.presence_status = int.Parse(cboPresenceStatusStudent.SelectedValue);
                    t.absence_reason_id = int.Parse(cboReasonTypeStudent.SelectedValue);
                    t.sheet_date = _sheetDate;
                    t.validation_status = (int)Timesheet.STATUS.VALID;
                    t.login_user_id = user.id;
                    //
                    tList.Add(t);

                    //t.week_day = _sheetDate.DayOfWeek.ToString().Substring(0, 2).ToUpper();
                }

                //add timesheet
                Timesheet.InsertStudentTimesheets(tList);

                //reload the gridview
                bindGridStudents();

                msgContent = "Succès !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnSearchStudent_ServerClick(object sender, EventArgs e)
    {
        Session["reason_list"] = Timesheet.getListAllReasonForCombo();
        bindGridStudents();
    }

    protected void cboReasonTypeStudent_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox cboReason = sender as RadComboBox;
        GridDataItem dataItem = (GridDataItem)cboReason.NamingContainer;

        if (cboReason.SelectedValue != "-1")
        {
            RadComboBox cboPresence = dataItem.FindControl("cboPresenceStatusStudent") as RadComboBox;
            cboPresence.SelectedValue = ((int)Timesheet.PRESENCE_STATE.ABSENT).ToString();
        }
    }

    protected void cboPresenceStatusStudent_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox cboPresence = sender as RadComboBox;
        GridDataItem dataItem = (GridDataItem)cboPresence.NamingContainer;
        RadComboBox cboReason = dataItem.FindControl("cboReasonTypeStudent") as RadComboBox;
        cboReason.SelectedValue = "-1";
    }

    protected void cboReportType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddl = sender as RadDropDownList;
        if (ddl.SelectedValue == "INDIVIDUAL")
        {
            txtCodeReportSt.Text = string.Empty;
            txtCodeReportSt.Enabled = true;

            ddlVacationReportSt.SelectedIndex = 0;
            ddlVacationReportSt.Enabled = false;

            ddlClassroomReportSt.SelectedIndex = 0;
            ddlClassroomReportSt.Enabled = false;
        }

        if (ddl.SelectedValue == "GROUP")
        {
            txtCodeReportSt.Text = string.Empty;
            txtCodeReportSt.Enabled = false;

            ddlVacationReportSt.SelectedIndex = 0;
            ddlVacationReportSt.Enabled = true;

            ddlClassroomReportSt.SelectedIndex = 0;
            ddlClassroomReportSt.Enabled = true;
        }

    }

    protected void btnSearhStudentTimesheetsReport_ServerClick(object sender, EventArgs e)
    {
        bindGridStudentsReport();
    }

    private void bindGridStudentsReport()
    {
        List<Timesheet> listResult = null;

        if (cboReportType.SelectedValue == "INDIVIDUAL")
        {
            // get values
            string studentCode = txtCodeReportSt.Text.Trim();
            DateTime fromDate = radFromDateReportSt.SelectedDate.Value;
            DateTime toDate = radToDateReportSt.SelectedDate.Value;
            //
            listResult = Timesheet.getListStudentTimesheetsIndividualReport(studentCode, fromDate, toDate);
        }

        if (cboReportType.SelectedValue == "GROUP")
        {
            // get values
            int classId = int.Parse(ddlClassroomReportSt.SelectedValue);
            string vacation = ddlVacationReportSt.SelectedValue;
            DateTime fromDate = radFromDateReportSt.SelectedDate.Value;
            DateTime toDate = radToDateReportSt.SelectedDate.Value;
            //
            listResult = Timesheet.getListStudentTimesheetsGroupReport(classId, vacation, fromDate, toDate);
        }

        // fill grid
        radGridStudentTimesheetsReport.DataSource = listResult;
        radGridStudentTimesheetsReport.DataBind();
    }

    protected void radGridStudentTimesheetsReport_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridStudentTimesheetsReport_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridStudentTimesheetsReport.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
            //
        }
    }

    protected void btnExpPdfReportSt_ServerClick(object sender, EventArgs e)
    {

    }

    protected void btnExpExcelReportSt_ServerClick(object sender, EventArgs e)
    {
        try
        {
            List<Timesheet> listResult = null;
            string reportType = cboReportType.SelectedValue;

            if (cboReportType.SelectedValue == "INDIVIDUAL")
            {
                // get values
                string studentCode = txtCodeReportSt.Text.Trim();
                DateTime fromDate = radFromDateReportSt.SelectedDate.Value;
                DateTime toDate = radToDateReportSt.SelectedDate.Value;
                //
                listResult = Timesheet.getListStudentTimesheetsIndividualReport(studentCode, fromDate, toDate);
            }

            if (cboReportType.SelectedValue == "GROUP")
            {
                // get values
                int classId = int.Parse(ddlClassroomReportSt.SelectedValue);
                string vacation = ddlVacationReportSt.SelectedValue;
                DateTime fromDate = radFromDateReportSt.SelectedDate.Value;
                DateTime toDate = radToDateReportSt.SelectedDate.Value;
                //
                listResult = Timesheet.getListStudentTimesheetsGroupReport(classId, vacation, fromDate, toDate);
            }

            // fill grid
            radGridStudentTimesheetsReport.DataSource = listResult;
            radGridStudentTimesheetsReport.DataBind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("raport_presence_eleves_{0}_{1}", reportType,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                radGridStudentTimesheetsReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                radGridStudentTimesheetsReport.ExportSettings.IgnorePaging = true;
                radGridStudentTimesheetsReport.ExportSettings.FileName = Path;
                radGridStudentTimesheetsReport.ExportSettings.ExportOnlyData = true;
                radGridStudentTimesheetsReport.ExportSettings.OpenInNewWindow = true;
                radGridStudentTimesheetsReport.MasterTableView.ExportToExcel();

            }

        }
        catch (System.Threading.ThreadAbortException lException)
        {
            // do nothing
        }
        catch (Exception ex)
        {

        }
    }


    /********************* STAFF ************************/

    private void bindDataGridTimesheetsStaff()
    {
        string Code = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
        List<Staff> listStaff = Staff.getListActiveStaffForTimesheet(Code);
        radGridTimesheetsStaff.DataSource = listStaff;
        radGridTimesheetsStaff.DataBind();


        if (listStaff != null && listStaff.Count > 0)
        {
            /************************        fill grid with previous values         ********************/

            GridDataItem item = (GridDataItem)radGridTimesheetsStaff.MasterTableView.Items[0];
            HiddenField hiddenMonDate = (HiddenField)item.FindControl("hiddenMonDate");
            HiddenField hiddenSunDate = (HiddenField)item.FindControl("hiddenSunDate");
            string firstWeekDay = hiddenMonDate.Value;
            string lastWeekDay = hiddenSunDate.Value;

            // setup staff code list
            List<String> listStaffCode = new List<string>();
            foreach (Staff st in listStaff)
            {
                listStaffCode.Add(st.id);
            }

            //  get list timesheets for each staffCode
            List<Timesheet> listTimesheets = Timesheet.GetListTimesheetsForStaff(listStaffCode, firstWeekDay, lastWeekDay);

            // fill grid
            if (listTimesheets != null && listTimesheets.Count > 0)
            {
                foreach (GridDataItem item1 in radGridTimesheetsStaff.MasterTableView.Items)
                {
                    string staffCode = item1.Cells[3].Text.Trim();
                    //
                    foreach (Timesheet t in listTimesheets)
                    {
                        if (t.staff_code == staffCode)
                        {
                            HiddenField hiddenWorkDate = item1.FindControl("hidden" + t.day_tag + "Date") as HiddenField;
                            RadTimePicker radEntryHour = item1.FindControl("radEntryHour" + t.day_tag) as RadTimePicker;
                            CheckBox chkIn = item1.FindControl("chkIn" + t.day_tag) as CheckBox;
                            RadTimePicker radExitHour = item1.FindControl("radExitHour" + t.day_tag) as RadTimePicker;
                            CheckBox chkOut = item1.FindControl("chkOut" + t.day_tag) as CheckBox;
                            //
                            radEntryHour.SelectedTime = t.entry_hour;
                            radExitHour.SelectedTime = t.exit_hour;
                            chkIn.Checked = t.check_in == 1 ? true : false;
                            chkOut.Checked = t.check_out == 1 ? true : false;
                        }
                    }
                }
            }

        }
    }

    protected void radGridTimesheetsStaff_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridTimesheetsStaff_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridTimesheetsStaff.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();

        }


        var now = radWeeks.SelectedDate.Value;
        var currentDay = now.DayOfWeek;
        int days = (int)currentDay;
        DateTime sunday = now.AddDays(-days);

        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            HiddenField hiddenMonDate = (HiddenField)item.FindControl("hiddenMonDate");
            HiddenField hiddenTueDate = (HiddenField)item.FindControl("hiddenTueDate");
            HiddenField hiddenWedDate = (HiddenField)item.FindControl("hiddenWedDate");
            HiddenField hiddenThuDate = (HiddenField)item.FindControl("hiddenThuDate");
            HiddenField hiddenFriDate = (HiddenField)item.FindControl("hiddenFriDate");
            HiddenField hiddenSatDate = (HiddenField)item.FindControl("hiddenSatDate");
            HiddenField hiddenSunDate = (HiddenField)item.FindControl("hiddenSunDate");

            hiddenMonDate.Value = sunday.AddDays(1).ToString("yyyy-MM-dd");
            hiddenTueDate.Value = sunday.AddDays(2).ToString("yyyy-MM-dd");
            hiddenWedDate.Value = sunday.AddDays(3).ToString("yyyy-MM-dd");
            hiddenThuDate.Value = sunday.AddDays(4).ToString("yyyy-MM-dd");
            hiddenFriDate.Value = sunday.AddDays(5).ToString("yyyy-MM-dd");
            hiddenSatDate.Value = sunday.AddDays(6).ToString("yyyy-MM-dd");
            hiddenSunDate.Value = sunday.AddDays(7).ToString("yyyy-MM-dd");
        }

        // change column-group headertext content
        // monday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[0].HeaderText = "Lundi : " + sunday.AddDays(1).ToString("dd/MM/yyyy");
        // tuesday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[1].HeaderText = "Mardi : " + sunday.AddDays(2).ToString("dd/MM/yyyy");
        // wednesday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[2].HeaderText = "Mercredi : " + sunday.AddDays(3).ToString("dd/MM/yyyy");
        // thursday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[3].HeaderText = "Jeudi : " + sunday.AddDays(4).ToString("dd/MM/yyyy");
        // friday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[4].HeaderText = "Vendredi : " + sunday.AddDays(5).ToString("dd/MM/yyyy");
        // saturday
        radGridTimesheetsStaff.MasterTableView.ColumnGroups[5].HeaderText = "Samedi : " + sunday.AddDays(6).ToString("dd/MM/yyyy");
        // sunday
        //radGridTimesheetsStaff.MasterTableView.ColumnGroups[6].HeaderText = Resources.Resource.sunday + " : " + sunday.AddDays(7).ToString("dd/MM/yyyy");

    }

    protected void btnSearchStaff_ServerClick(object sender, EventArgs e)
    {
        bindDataGridTimesheetsStaff();
    }

    protected void btnValidateStaff_ServerClick(object sender, EventArgs e)
    {
        List<Timesheet> listTimesheet = new List<Timesheet>();
        List<String> listStaffCode = new List<string>();
        string firstWeekDay = null;
        string lastWeekDay = null;

        foreach (GridDataItem item in radGridTimesheetsStaff.MasterTableView.Items)
        {
            HiddenField hiddenMonDate = (HiddenField)item.FindControl("hiddenMonDate");
            HiddenField hiddenSunDate = (HiddenField)item.FindControl("hiddenSunDate");
            firstWeekDay = DateTime.Parse(hiddenMonDate.Value).ToString("yyyy-MM-dd");
            lastWeekDay = DateTime.Parse(hiddenSunDate.Value).ToString("yyyy-MM-dd");
            string staffCode = item.Cells[3].Text.Trim();
            //
            listStaffCode.Add(staffCode);
            //
            Users user = Session["user"] as Users;
            List<string> daysExtension = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            foreach (string day in daysExtension)
            {
                // get values from gridview
                HiddenField hiddenWorkDate = item.FindControl("hidden" + day + "Date") as HiddenField;
                RadTimePicker radEntryHour = item.FindControl("radEntryHour" + day) as RadTimePicker;
                CheckBox chkIn = item.FindControl("chkIn" + day) as CheckBox;
                RadTimePicker radExitHour = item.FindControl("radExitHour" + day) as RadTimePicker;
                CheckBox chkOut = item.FindControl("chkOut" + day) as CheckBox;

                Timesheet t = new Timesheet();
                t.staff_code = staffCode;
                t.work_date = DateTime.Parse(hiddenWorkDate.Value);
                t.entry_hour = radEntryHour.IsEmpty ? TimeSpan.MinValue : radEntryHour.SelectedTime.Value;
                t.exit_hour = radExitHour.IsEmpty ? TimeSpan.MinValue : radExitHour.SelectedTime.Value;
                t.check_in = chkIn.Checked == true ? 1 : 0;
                t.check_out = chkOut.Checked == true ? 1 : 0;
                t.login_user_id = user.id;
                t.day_tag = day;
                //

                if (t.entry_hour != TimeSpan.MinValue
                    || t.exit_hour != TimeSpan.MinValue)
                {
                    listTimesheet.Add(t);
                }
            }
        }

        Timesheet.InsertTimesheetStaff(listTimesheet, firstWeekDay, lastWeekDay, listStaffCode);

        msgContent = "Succes !";
        MessageAlert.RadAlert(msgContent, 350, 150, "Information", null, "../images/success_check.png");
    }
}
