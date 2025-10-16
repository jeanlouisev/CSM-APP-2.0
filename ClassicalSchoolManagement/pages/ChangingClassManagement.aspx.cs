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




public partial class ChangingClassManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.CLASSROOM;

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
            Session.Remove("preload_id_class");
            Session.Remove("preload_staff_code");
            Session.Remove("preload_academic_year");

            Users user = Session["user"] as Users;
            loadActiveClassroom(ddlClassroom);
            loadListAcademicYear(ddlAcademicYear);
            //
            BindDataGridStudent();
            //
        }
    }

    private void verifyAccessLevel()
    {
        Users user = Session["user"] as Users;

        // VERIFY USER ACCESS LEVEL
        List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
        if (listResult != null && listResult.Count > 0)
        {
            Users userAccess = listResult[0];
            int notGranted = (int)Users.ACCESS.NO;

            // edit
            if (userAccess.edit_access == notGranted)
            {
                disableEditOption();
            }

            // delete
            if (userAccess.delete_access == notGranted)
            {
                disableDeleteOption();
            }
        }
        else
        {
            Response.Redirect("~/Pages/NoPrivilegeWarningPage.aspx");
        }
    }

    // edit_access
    private void disableEditOption()
    {
        try
        {
            //btnSearch.Attributes.Add("disabled", "disabled");
            //btnAdd.Attributes.Add("disabled", "disabled");

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            //if (gridAwards.Visible && gridAwards.MasterTableView.Items.Count > 0)
            //{
            //    foreach (GridItem item in gridAwards.MasterTableView.Items)
            //    {
            //        System.Web.UI.HtmlControls.HtmlButton btnDelete = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
            //        //
            //        btnDelete.Attributes.Add("disabled", "disabled");
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
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
                int maxAcademicYear = Settings.getAcademicYear();
                if (maxAcademicYear != 0)
                {
                    ddl.SelectedValue = maxAcademicYear.ToString();
                }
            }
            else
            {
                ddl.Items.Clear();
                ddl = null;
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            // vacation
            ddlVacation.Items.Clear();

            int academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
            //
            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = int.Parse(ddlClassroom.SelectedValue);
                List<ClassRoom> listVacation = ClassRoom.getListVacationByClassroomId(classId);
                //
                loadListVacation(listVacation);
            }
            else
            {
                ddlVacation.Items.Add(new DropDownListItem("Matin", "AM"));
                ddlVacation.Items.Add(new DropDownListItem("Median", "PM"));
                ddlVacation.Items.Add(new DropDownListItem("Soir", "NG"));
                ddlVacation.Items.Add(new DropDownListItem("Weekend", "WK"));
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        //if (e.CommandName == "ViewStudentDetails")
        //{
        //    Session["user_code"] = studentCode;
        //    string page_url = "StudentDetailsInformation.aspx";
        //    try
        //    {
        //        //Response.Redirect("DocumentDetail.aspx");
        //        //Session["type_detail"] = "endedit";
        //        //mp1.Show();
        //        string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
        //                                        + "oWinn.show();"
        //                                        + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
        //                                        + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
        //                                        + "oWinn.center();"
        //                                        + "Sys.Application.remove_load(f);"
        //                                    + "}"
        //                                    + "Sys.Application.add_load(f);";

        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //    }
        //    catch (Exception ex)
        //{
        //    throw ex;
        //}
        //   }
    }

    private void BindDataGridStudent()
    {
        try
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            //
            Users user = Session["user"] as Users;
            
            int classId = ddlClassroom.SelectedValue == "-1" ? 0 : int.Parse(ddlClassroom.SelectedValue);
            string vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;
            int accYearId = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

            //get list of students
            List<Student> listResult = Student.getListStudentForChangingClass(classId, vacation, accYearId);
            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                //pnlResult.Visible = true;
                //lblCounter.Visible = true;
                //lblCounter.Text = listResult.Count + " Ligne(s)";
                //lnkExportExcel.Visible = true;
                //tblGridHeader.Visible = true;
            }
            else
            {
                //lblFound.Visible = true;
                //pnlResult.Visible = true;
                //lblCounter.Visible = false;
                //lnkExportExcel.Visible = false;
                //tblGridHeader.Visible = false;
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

    protected void gridListStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.row.rowindex == 0)
            //    e.row.style.add("height", "50px");
            //e.row.style.add("vertical-align", "bottom");

            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }

    }

    private void loadActiveClassroom(RadDropDownList ddlTemp)
    {
        ddlTemp.Items.Clear();
        //
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ddlTemp.DataValueField = "id";
            ddlTemp.DataTextField = "name";
            ddlTemp.DataSource = listClassroom;
            ddlTemp.DataBind();
        }
        ddlTemp.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlTemp.SelectedValue = "-1";
    }

    private void loadListVacation(List<ClassRoom> listClassroom)
    {

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

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        List<Student> listStudentReportInfo = new List<Student>();
        //
        if (gridListStudent.Visible && gridListStudent.Rows.Count > 0)
        {
            // get list student to change class
            foreach (GridViewRow row in gridListStudent.Rows)
            {
                // check all selected student to change class for.
                CheckBox chk = row.FindControl("chkStudent") as CheckBox;
                // only checked student information will be added to the list
                if (chk.Checked)
                {
                    Student st = new Student();
                    HiddenField hiddenClassId = row.FindControl("hiddenClassroomId") as HiddenField;
                    HiddenField hiddenResultStat = row.FindControl("hiddenResultStatus") as HiddenField;
                    HiddenField hiddenVacationCode = row.FindControl("hiddenVacationCode") as HiddenField;
                    HiddenField hiddenAcademicYear = row.FindControl("hiddenAcademicYear") as HiddenField;
                    //
                    st.class_id = int.Parse(hiddenClassId.Value);
                    st.final_result_status = int.Parse(hiddenResultStat.Value);
                    st.staff_code = row.Cells[3].Text;
                    st.vacation = hiddenVacationCode.Value;
                    st.academic_year = int.Parse(hiddenAcademicYear.Value);
                    //
                    listStudentReportInfo.Add(st);
                }
            }

            changeStudentClass(listStudentReportInfo);
        }
    }

    private void changeStudentClass(List<Student> listStudentReportInfo)
    {
        // check available academic year
        int currentAcademicYear = Settings.getAcademicYear();
        int selectedAcademicYear = int.Parse(ddlAcademicYear.SelectedValue);
        if (selectedAcademicYear >= currentAcademicYear)
        {
            MessBox.Show("Désolé, mais vous ne pouvez pas changer de classe en ce moment.\n" +
                               "Veuillez demander à l’administrateur de l’application d’ajouter " +
                                "une nouvelle année académique.");
        }
        else
        {
            string reportMessage = "";

            List<Student> listStudent_1ereAnneeKindergarden = new List<Student>();
            List<Student> listStudent_2iemeAnneeKindergarden = new List<Student>();
            List<Student> listStudent_3iemeAnneeKindergarden = new List<Student>();
            List<Student> listStudent_1ereAnneeFondamentale = new List<Student>();
            List<Student> listStudent_2iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_3iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_4iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_5iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_6iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_7iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_8iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_9iemeAnneeFondamentale = new List<Student>();
            List<Student> listStudent_3iemeSecondaire = new List<Student>();
            List<Student> listStudent_seconde = new List<Student>();
            List<Student> listStudent_retho = new List<Student>();
            List<Student> listStudent_philo = new List<Student>();

            // filter the list of students and affect to respective classroom list
            if (listStudentReportInfo != null && listStudentReportInfo.Count > 0)
            {
                foreach (Student st in listStudentReportInfo)
                {
                    #region start loop
                    if (st.class_id == 1)
                    {
                        listStudent_1ereAnneeKindergarden.Add(st);
                    }
                    else if (st.class_id == 2)
                    {
                        listStudent_2iemeAnneeKindergarden.Add(st);
                    }
                    else if (st.class_id == 3)
                    {
                        listStudent_3iemeAnneeKindergarden.Add(st);
                    }
                    else if (st.class_id >= 10 && st.class_id <= 19)
                    {
                        listStudent_1ereAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 20 && st.class_id <= 29)
                    {
                        listStudent_2iemeAnneeKindergarden.Add(st);
                    }
                    else if (st.class_id >= 30 && st.class_id <= 39)
                    {
                        listStudent_3iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 40 && st.class_id <= 49)
                    {
                        listStudent_4iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 50 && st.class_id <= 59)
                    {
                        ClassRoom classR = new ClassRoom();
                        if (st.final_result_status <= 0)
                        {
                            classR.class_id = st.class_id;
                        }
                        else
                        {
                            classR.class_id = st.class_id + 10;
                        }
                        //
                        classR.staffCode = st.staff_code;
                        classR.vacation = st.vacation;
                        classR.academic_year_id = Settings.getAcademicYear();
                        // disable student from old classes
                        ClassRoom.disableStudentFromOldClassroom(classR.staffCode);
                        ClassRoom.changeClassroom(classR);
                    }
                    else if (st.class_id >= 60 && st.class_id <= 69)
                    {
                        listStudent_6iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 70 && st.class_id <= 79)
                    {
                        listStudent_7iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 80 && st.class_id <= 89)
                    {
                        listStudent_8iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 90 && st.class_id <= 99)
                    {
                        listStudent_9iemeAnneeFondamentale.Add(st);
                    }
                    else if (st.class_id >= 100 && st.class_id <= 109)
                    {
                        listStudent_3iemeSecondaire.Add(st);
                    }
                    else if (st.class_id >= 110 && st.class_id <= 119)
                    {
                        listStudent_seconde.Add(st);
                    }
                    else if (st.class_id >= 120 && st.class_id <= 129)
                    {
                        listStudent_retho.Add(st);
                    }
                    else if (st.class_id >= 130 && st.class_id <= 139)
                    {
                        listStudent_philo.Add(st);
                    }
                    #endregion end loop
                }
            }
            /*
            // check classrooms available place
            #region 
            if (listStudent_1ereAnneeKindergarden != null && listStudent_1ereAnneeKindergarden.Count > 0)
            {
                reportMessage += listStudent_1ereAnneeKindergarden.Count + " Eleve(s) en 1ere Annee Kindergarden\n";
            }
            if (listStudent_2iemeAnneeKindergarden != null && listStudent_2iemeAnneeKindergarden.Count > 0)
            {
                reportMessage += listStudent_2iemeAnneeKindergarden.Count + " Eleve(s) en 2ieme Annee Kindergarden\n";
            }
            if (listStudent_3iemeAnneeKindergarden != null && listStudent_3iemeAnneeKindergarden.Count > 0)
            {
                reportMessage += listStudent_3iemeAnneeKindergarden.Count + " Eleve(s) en 3ieme Annee Kindergarden\n";
            }
            if (listStudent_1ereAnneeFondamentale != null && listStudent_1ereAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_1ereAnneeFondamentale.Count + "  Eleve(s) en 1ere Annee Fondamentale\n";
            }
            if (listStudent_2iemeAnneeFondamentale != null && listStudent_2iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_2iemeAnneeFondamentale.Count + " Eleve(s) en 2ieme Annee Fondamentale\n";
            }
            if (listStudent_3iemeAnneeFondamentale != null && listStudent_3iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_3iemeAnneeFondamentale.Count + " Eleve(s) en 3ieme Annee Fondamentale\n";
            }
            if (listStudent_4iemeAnneeFondamentale != null && listStudent_4iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_4iemeAnneeFondamentale.Count + " Eleve(s) en 4ieme Annee Fondamentale\n";
            }
            if (listStudent_5iemeAnneeFondamentale != null && listStudent_5iemeAnneeFondamentale.Count > 0)
            {
                // create an object of type classroom
                ClassRoom classroom = null;

                reportMessage += listStudent_5iemeAnneeFondamentale.Count + " Eleve(s) en 5ieme Annee Fondamentale\n";
                //
                List<Student> listStudent_5iemeAnneeFondamentale_AM = new List<Student>();
                List<Student> listStudent_5iemeAnneeFondamentale_PM = new List<Student>();
                List<Student> listStudent_5iemeAnneeFondamentale_NG = new List<Student>();
                List<Student> listStudent_5iemeAnneeFondamentale_WK = new List<Student>();

                if (listStudent_5iemeAnneeFondamentale != null
                    && listStudent_5iemeAnneeFondamentale.Count > 0)
                {
                    foreach (Student st in listStudent_5iemeAnneeFondamentale)
                    {
                        switch (st.vacation)
                        {
                            case "AM": listStudent_5iemeAnneeFondamentale_AM.Add(st); break;
                            case "PM": listStudent_5iemeAnneeFondamentale_PM.Add(st); break;
                            case "NG": listStudent_5iemeAnneeFondamentale_NG.Add(st); break;
                            case "WK": listStudent_5iemeAnneeFondamentale_WK.Add(st); break;
                        }
                    }
                }

                reportMessage +=  listStudent_5iemeAnneeFondamentale_AM.Count + " Eleve(s) en 5ieme Annee Matin\n";
                reportMessage += listStudent_5iemeAnneeFondamentale_PM.Count + " Eleve(s) en 5ieme Annee Median\n";
                reportMessage += listStudent_5iemeAnneeFondamentale_NG.Count + " Eleve(s) en 5ieme Annee Soir\n";
                reportMessage += listStudent_5iemeAnneeFondamentale_WK.Count + " Eleve(s) en 5ieme Annee Weekend\n";

                //classroom = new ClassRoom();
                //classroom.classroom_id = 
                //cmd.Parameters.AddWithValue("@vacation_type", classroom.vacation_type.ToUpper());
                //int AM_capacity = ClassRoom.GetClassroomCurrentCapacity()


            }
            if (listStudent_6iemeAnneeFondamentale != null && listStudent_6iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_6iemeAnneeFondamentale.Count + " Eleve(s) en 6ieme Annee Fondamentale\n";
            }
            if (listStudent_7iemeAnneeFondamentale != null && listStudent_7iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_7iemeAnneeFondamentale.Count + " Eleve(s) en 7ieme Annee Fondamentale\n";
            }
            if (listStudent_8iemeAnneeFondamentale != null && listStudent_8iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_8iemeAnneeFondamentale.Count + " Eleve(s) en 8ieme Annee Fondamentale\n";
            }
            if (listStudent_9iemeAnneeFondamentale != null && listStudent_9iemeAnneeFondamentale.Count > 0)
            {
                reportMessage += listStudent_9iemeAnneeFondamentale.Count + " Eleve(s) en 9ieme Annee Fondamentale\n";
            }
            if (listStudent_3iemeSecondaire != null && listStudent_3iemeSecondaire.Count > 0)
            {
                reportMessage += listStudent_3iemeSecondaire.Count + " Eleve(s) en 3ieme Secondaire\n";
            }
            if (listStudent_seconde != null && listStudent_seconde.Count > 0)
            {
                reportMessage += listStudent_seconde.Count + " Eleve(s) en seconde\n";
            }
            if (listStudent_retho != null && listStudent_retho.Count > 0)
            {
                reportMessage += listStudent_retho.Count + " Eleve(s) en retho\n";
            }
            if (listStudent_philo != null && listStudent_philo.Count > 0)
            {
                reportMessage += listStudent_philo.Count + " Eleve(s) en philo\n";
            }

            #endregion
            */
            //MessageAlert.RadAlert(reportMessage, 600, 600, "Information", null, "/images/warning.png");
            MessBox.Show(reportMessage);
        }
    }

    protected void btnExportExcel_ServerClick(object sender, EventArgs e)
    {

    }
}