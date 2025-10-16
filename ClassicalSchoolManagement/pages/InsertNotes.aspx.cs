using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

using System.IO;
using Telerik.Web.UI;
using System.Drawing;



public partial class InsertNotes : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;
    string msgContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        else
        {
            verifyAccessLevel();
        }

        if (!IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                loadActiveClassroom();
                loadListAcademicYear();
                // load data in gridview
                //BindDataGridNotes();
            }
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
            btnAddNotes.Attributes.Add("disabled", "disabled");
            btnExportPdf.Attributes.Add("disabled", "disabled");

            // check if grid notes is visible
            if (gridNotes.Visible && gridNotes.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridNotes.MasterTableView.Items)//Running all lines of grid
                {
                    RadNumericTextBox txtNoteObtained = (item.Cells[9].FindControl("txtNoteObtained") as RadNumericTextBox);
                    txtNoteObtained.Enabled = false;
                }
            }
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
            // nothing to change here
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private void loadListAcademicYear()
    {
        try
        {
            ddlAcademicYear.Items.Clear();
            List<Settings> listResult = Settings.getAcademicYearFull();
            if (listResult != null && listResult.Count > 0)
            {
                ddlAcademicYear.DataValueField = "id";
                ddlAcademicYear.DataTextField = "years";
                ddlAcademicYear.DataSource = listResult;
                ddlAcademicYear.DataBind();

                // get current academic year
                int currentAcdemicYear = Settings.getAcademicYear();
                ddlAcademicYear.SelectedValue = currentAcdemicYear.ToString();
            }
        }
        catch (Exception ex) { }
    }

    private void loadActiveClassroom()
    {
        ddlClassroom.Items.Clear();
        List<ClassRoom> listResult = ClassRoom.getListActiveClassroom();
        if (listResult != null && listResult.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
            ddlClassroom.DataSource = listResult;
            ddlClassroom.DataBind();
        }
        //
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlClassroom.SelectedValue = "-1";
    }

    protected void btnAddNotes_Click(object sender, EventArgs e)
    {
        try
        {
            List<Notes> listNotes = new List<Notes>();
            Users user = Session["user"] as Users;
            // check if grid notes is visible
            if (gridNotes.Visible && gridNotes.MasterTableView.Items.Count > 0)
            {
                //add new notes;
                foreach (GridItem item in gridNotes.MasterTableView.Items)//Running all lines of grid
                {
                    HiddenField hiddenStudentId = (HiddenField)item.FindControl("hiddenStudentId");
                    HiddenField hiddenClassId = (HiddenField)item.FindControl("hiddenClassId");
                    HiddenField hiddenVacation = (HiddenField)item.FindControl("hiddenVacation");
                    HiddenField hiddenAcademicYearId = (HiddenField)item.FindControl("hiddenAcademicYearId");
                    HiddenField hiddenControl = (HiddenField)item.FindControl("hiddenControl");
                    HiddenField hiddenCoursId = (HiddenField)item.FindControl("hiddenCoursId");
                    HiddenField hiddenCoefficient = (HiddenField)item.FindControl("hiddenCoefficient");
                    RadNumericTextBox txtNoteObtained = (item.Cells[9].FindControl("txtNoteObtained") as RadNumericTextBox);


                    Notes n = new Notes();
                    n.student_id = hiddenStudentId.Value;
                    n.class_id = int.Parse(hiddenClassId.Value);
                    n.vacation = hiddenVacation.Value;
                    n.control = int.Parse(hiddenControl.Value);
                    n.academic_year_id = int.Parse(hiddenAcademicYearId.Value);
                    n.cours_id = int.Parse(hiddenCoursId.Value);
                    n.coefficient = double.Parse(hiddenCoefficient.Value);
                    n.note_obtained = txtNoteObtained.Value == null ? 0 : double.Parse(txtNoteObtained.Value.ToString());
                    n.login_user = user.username;

                    if (n.note_obtained >= 0)
                    {
                        listNotes.Add(n);
                    }
                }
                //add notes to database
                Notes.addNotes(listNotes);
                // reload grid
                BindDataGridNotes();
                //
                msgContent = "Succès !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
            }
            else
            {
                msgContent = "Désolé, mais il n\\'y a rien à sauvegarder !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private bool validateValuesProvidedInGridNotes(GridView grid)
    {
        bool result = false;

        foreach (GridViewRow row in grid.Rows)//Running all lines of grid
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadTextBox txtStudentPoints = (row.Cells[4].FindControl("txtStPoints") as RadTextBox);

                double examPts = double.Parse(row.Cells[3].Text.ToString());
                double studentPts = double.Parse(txtStudentPoints.Text.Trim());
                if (studentPts > examPts)
                {
                    result = true;
                }
            }
        }
        return result;
    }

    public void remove(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                /*
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListNotes.Rows[index];
                string studentCode = row.Cells[1].Text;
                Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                BindDataGridStudent();**/
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataGridNotes();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void resetFormMaster1()
    {
        //txtCodeStudent.Text = string.Empty;
        //// txtCodeStudent.Focus();
        //txtFirstName.Text = string.Empty;
        //txtLastName.Text = string.Empty;
        //txtSex.Text = string.Empty;
        //txtVacation.Text = string.Empty;
        //txtClassroom.Text = string.Empty;
        //pnlResult.Visible = false;
        //ddlControl.SelectedValue = "-1";
    }

    private void BindDataGridNotes()
    {
        try
        {
            Users user = Session["user"] as Users;
            List<Notes> listResult = new List<Notes>();
            //
            Notes note = new Notes();
            note.class_id = ddlClassroom.SelectedValue == "-1" ? 0 : int.Parse(ddlClassroom.SelectedValue);
            note.vacation = ddlVacation.SelectedValue == "-1" ? null : ddlVacation.SelectedValue;
            note.control = ddlControl.SelectedValue == "-1" ? 0 : int.Parse(ddlControl.SelectedValue);
            note.cours_id = int.Parse(ddlCourse.SelectedValue);
            note.student_id = txtStudentCode.Text.Trim().Length <= 0 ? null : txtStudentCode.Text.Trim();
            note.academic_year_id = ddlAcademicYear.SelectedValue == "-1" ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

            listResult = Notes.getListNotes(note);
            gridNotes.DataSource = listResult;
            gridNotes.DataBind();

            //
            verifyAccessLevel();

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void gridNotes_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridNotes_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridNotes.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();


            // comtrol
            HiddenField hiddenControl = (HiddenField)item.FindControl("hiddenControl");
            hiddenControl.Value = ddlControl.SelectedValue;
            item.Cells[7].Text = ddlControl.SelectedText;
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        //  BindDataGridNotes();
        //  MessageAlert.RadAlert("Test ok!", 300, 150, "Information", null);
    }

    protected void txtStPoints_TextChanged(object sender, EventArgs e)
    {
        try
        {
            using (GridViewRow row = (GridViewRow)((RadNumericTextBox)sender).Parent.Parent)
            {
                int index = Convert.ToInt32(row.RowIndex);
                int examPoints = int.Parse(row.Cells[8].Text);
                RadNumericTextBox txt = row.Cells[9].FindControl("txtStPoints") as RadNumericTextBox;
                Double studentPoints = txt.Value.ToString().Length <= 0 ? 0 : Double.Parse(txt.Value.ToString());
                if (studentPoints > examPoints)
                {
                    txt.Text = string.Empty;
                    txt.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadCourses()
    {
        try
        {
            ddlCourse.Items.Clear();
            //
            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = ddlClassroom.SelectedValue == null ? 0 : int.Parse(ddlClassroom.SelectedValue);
                //int academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                //
                List<Course> listCourse = Course.getListAttachedCoursesByClassId(classId);
                if (listCourse != null && listCourse.Count > 0)
                {
                    // fill the ddl now
                    ddlCourse.DataValueField = "cours_id";
                    ddlCourse.DataTextField = "cours_name";
                    ddlCourse.DataSource = listCourse;
                    ddlCourse.DataBind();
                }
            }
            ddlCourse.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        BindDataGridNotes();
    }

    protected void txtStudentCode_TextChanged(object sender, EventArgs e)
    {
        txtStudentFullname.Text = "";
        string stCode = txtStudentCode.Text.Trim();
        if (stCode.Length > 0)
        {
            Student st = Student.getStudentInfoById(stCode);
            if (st != null)
            {
                txtStudentFullname.Text = st.fullName;
            }
        }
    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        exportToExcel();
    }

    private void exportToExcel()
    {
    }

    protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        loadCourses();
    }

    protected void btnExportPdf_ServerClick(object sender, EventArgs e)
    {

    }
}