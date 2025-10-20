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



public partial class SearchStudents : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.STUDENT;


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
            Session.Remove("student_code");
            //
            loadActiveClassroom();
            loadListAcademicYear(ddlAcademicYear);
            //
            BindGridStudent();
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
            btnExportExcel.Attributes.Add("disabled", "disabled");

            // loop through the grid to disable delete option
            if (gridStudent.Visible && gridStudent.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridStudent.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnEdit = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnEdit");
                    //
                    btnEdit.Attributes.Add("disabled", "disabled");
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
            // loop through the grid to disable delete option
            if (gridStudent.Visible && gridStudent.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridStudent.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnDelete = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
                    //
                    btnDelete.Attributes.Add("disabled", "disabled");
                }
            }
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

            ddl.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        catch (Exception ex) { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //gridStudent.Rebind();
        BindGridStudent();
    }

    private void loadActiveClassroom()
    {
        int indexCnt = 0;
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
            indexCnt = 1;
        }
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        //
        
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlClassroom.SelectedIndex = indexCnt;
    }

    public void removeStudent(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string studentCode = dataItem.GetDataKeyValue("id").ToString();
            //this part only set status of student to 0
            Student.disableStudent(studentCode);
            // this part delete the student completely from the system
            // ------>  Student.deleteStudentPermanently(studentCode);
            //refresh data of the gridview
            BindGridStudent();
            MessageAlert.RadAlert("Désactivé avec succès !", 350, 150, "Information", null, "../images/success_check.png");
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnEdit_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string studentCode = dataItem.GetDataKeyValue("id").ToString();
            Session["student_code"] = studentCode;
            Response.Redirect("RegisterStudents.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnViewDetails_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string studentCode = dataItem.GetDataKeyValue("id").ToString();
            //int classroomId = int.Parse(((HiddenField)dataItem.FindControl("hiddenClassroomId")).Value.ToString());

            Session["student_code"] = studentCode;
            //Session["classroom_id"] = classroomId;

            string page_url = "DialogStudentDetailsInfo.aspx";

            //Response.Redirect("DocumentDetail.aspx");
            //Session["type_detail"] = "endedit";
            //mp1.Show();
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            //+ "oWinn.maximize();"       
                                            + "oWinn.SetSize(1124, 650);"
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

    public void disableStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value_disable"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                //GridViewRow row = gridListStudent.Rows[index];
                string studentCode = null; // row.Cells[1].Text;

                //this part only set status of student to 0
                Student.disableStudent(studentCode);

                // this part delete the student completely from the system
                //  Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                gridStudent.Rebind();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public void enableStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            int index = Convert.ToInt32(imageButton.CommandArgument);
            //  GridViewRow row = gridListStudent.Rows[index];
            string studentCode = null; // row.Cells[1].Text;
            Student.enableStudent(studentCode);
            gridStudent.Rebind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
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

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            Student st = new Student();
            st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
            st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
            st.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
            st.vacation = ddlVacation.SelectedValue.ToString() == "-1" ? null : ddlVacation.SelectedValue;
            st.classroom_id = int.Parse(ddlClassroom.SelectedValue);
            st.academic_year = ddlAcademicYear.SelectedValue == "-1" ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

            //get list of students
            List<Student> listResult = Student.getListStudent(st);
            gridStudent.DataSource = listResult;
            gridStudent.Rebind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("liste_eleves_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                gridStudent.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gridStudent.ExportSettings.IgnorePaging = true;
                gridStudent.ExportSettings.FileName = Path;
                gridStudent.ExportSettings.ExportOnlyData = true;
                gridStudent.ExportSettings.OpenInNewWindow = true;
                gridStudent.MasterTableView.ExportToExcel();

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

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    //protected void gridStudent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //{
    //    Student st = new Student();
    //    st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
    //    st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
    //    st.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
    //    st.vacation = ddlVacation.SelectedValue.ToString() == "-1" ? null : ddlVacation.SelectedValue;
    //    st.classroom = ddlClassroom.SelectedValue == "-1" ? null : ddlClassroom.SelectedValue;
    //    st.academic_year = ddlAcademicYear.SelectedValue == "-1" ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

    //    //get list of students
    //    List<Student> listResult = Student.getListStudent(st);
    //    gridStudent.DataSource = listResult;


    //    Users user = Session["user"] as Users;
    //    // check login_user policy to grant or revoke access
    //    List<Users> listGroupPolicy = Users.getListGroupPolicyByRoleId(user.role_id, menu_code);
    //    if (listGroupPolicy != null && listGroupPolicy.Count > 0)
    //    {
    //        if (listGroupPolicy[0].role_view == 0
    //            && listGroupPolicy[0].role_edit == 0
    //            && listGroupPolicy[0].role_delete == 0)
    //        {
    //            //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
    //            Response.Redirect("~/Default.aspx");
    //        }
    //        else
    //        {
    //            // edit
    //            if (listGroupPolicy[0].role_edit == 0)
    //            {
    //                disableEditOption();
    //            }
    //            // delete
    //            if (listGroupPolicy[0].role_delete == 0)
    //            {
    //                disableDeleteOption();
    //            }
    //        }
    //    }
    //}

    private void BindGridStudent()
    {
        Student st = new Student();
        st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
        st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
        st.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
        st.vacation = ddlVacation.SelectedValue.ToString() == "-1" ? null : ddlVacation.SelectedValue;
        st.classroom_id = int.Parse(ddlClassroom.SelectedValue);
        st.academic_year = ddlAcademicYear.SelectedValue == "-1" ? 0 : int.Parse(ddlAcademicYear.SelectedValue);

        //get list of students
        List<Student> listResult = Student.getListStudent(st);
        if (listResult == null && listResult.Count <= 0)
        {
            listResult = new List<Student>();
        }

        gridStudent.DataSource = listResult;
        gridStudent.DataBind();

        // check user access level
        verifyAccessLevel();

    }

    protected void gridListStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }

        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
            e.Row.Style.Add("vertical-align", "bottom");
        }
            */
    }

    protected void gridStudent_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridStudent_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridStudent.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}