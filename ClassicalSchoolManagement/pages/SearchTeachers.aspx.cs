using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using System.IO;
using System.Drawing;
using Telerik.Web.UI;


public partial class SearchTeacher : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.HR;
    List<string> listCourseCode = new List<string>();
    int counter = 0;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;
        

        if (!IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;


                //// check login_user policy to grant or revoke access
                //List<Users> listGroupPolicy = Users.getListGroupPolicyByRoleId(user.role_id, menu_code);
                //if (listGroupPolicy != null && listGroupPolicy.Count > 0)
                //{
                //    if (listGroupPolicy[0].role_view == 0
                //        && listGroupPolicy[0].role_edit == 0
                //        && listGroupPolicy[0].role_delete == 0)
                //    {
                //        //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
                //        Response.Redirect("~/Default.aspx");
                //    }
                //    else
                //    {
                //        // edit
                //        if (listGroupPolicy[0].role_edit == 0)
                //        {
                //            disableEditOption();
                //        }
                //        // delete
                //        if (listGroupPolicy[0].role_delete == 0)
                //        {
                //            disableDeleteOption();
                //        }
                //    }
                //}
            }
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            //// loop through the grid to disable edit option
            //if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListTeacher.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn = gridListTeacher.Rows[i].Cells[9].FindControl("btnEdit") as ImageButton;
            //        imgBtn.Enabled = false;
            //    }
            //}
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
            //// loop through the grid to disable delete option
            //if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListTeacher.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn = gridListTeacher.Rows[i].Cells[10].FindControl("btnDelete") as ImageButton;
            //        imgBtn.Enabled = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gridTeacher.Rebind();
            Users user = Session["user"] as Users;
            // check login_user policy to grant or revoke access
            List<Users> listGroupPolicy = Users.getListUserAccessLevel(user.role_id, menu_code);
            if (listGroupPolicy != null && listGroupPolicy.Count > 0)
            {
                //if (listGroupPolicy[0].role_view == 0
                //    && listGroupPolicy[0].role_edit == 0
                //    && listGroupPolicy[0].role_delete == 0)
                //{
                //    //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
                //    Response.Redirect("~/Default.aspx");
                //}
                //else
                //{
                //    // edit
                //    if (listGroupPolicy[0].role_edit == 0)
                //    {
                //        disableEditOption();
                //    }
                //    // delete
                //    if (listGroupPolicy[0].role_delete == 0)
                //    {
                //        disableDeleteOption();
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    public void removeTeacher(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string teacherCode = dataItem.GetDataKeyValue("id").ToString();
            Teacher.deleteTeacherTemporarily(teacherCode);
            //refresh data of the gridview
            gridTeacher.Rebind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }
    
    protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {

    }
    
    protected void gridTeacher_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        Teacher teacher = new Teacher();
        //get the fields from the form
        teacher.id = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
        teacher.fullName = txtFullName.Text.Trim().Length <= 0 ? null : txtFullName.Text.Trim();
        teacher.phone1 = txtPhone.Text.Trim().Length <= 0 ? null : txtPhone.Text.Trim();
        teacher.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
        teacher.marital_status = ddlMaritalStatus.SelectedValue.ToString() == "-1" ? null : ddlMaritalStatus.SelectedValue;
        teacher.level = ddlLevel.SelectedValue.ToString() == "-1" ? null : ddlLevel.SelectedValue;

        //get list of students
        List<Teacher> listResult = Teacher.getListTeacher(teacher);
        gridTeacher.DataSource = listResult;
    }

    protected void gridTeacher_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridTeacher_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridTeacher.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnEdit_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string teacherCode = dataItem.GetDataKeyValue("id").ToString();
            Session["teacher_id"] = teacherCode;
            Response.Redirect("RegisterTeachers.aspx");
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnViewDetails_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        string teacherCode = dataItem.GetDataKeyValue("id").ToString();
        Session["teacher_id"] = teacherCode;
        string page_url = "DialogTeacherDetailsInfo.aspx";

        try
        {
            //Response.Redirect("DocumentDetail.aspx");
            //Session["type_detail"] = "endedit";
            //mp1.Show();
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1124, 650);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

    protected void btnExportExcel_ServerClick(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            Teacher teacher = new Teacher();
            //get the fields from the form
            teacher.id = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
            teacher.fullName = txtFullName.Text.Trim().Length <= 0 ? null : txtFullName.Text.Trim();
            teacher.phone1 = txtPhone.Text.Trim().Length <= 0 ? null : txtPhone.Text.Trim();
            teacher.sex = ddlSex.SelectedValue.ToString() == "-1" ? null : ddlSex.SelectedValue;
            teacher.marital_status = ddlMaritalStatus.SelectedValue.ToString() == "-1" ? null : ddlMaritalStatus.SelectedValue;
            teacher.level = ddlLevel.SelectedValue.ToString() == "-1" ? null : ddlLevel.SelectedValue;

            //get list of teaher
            List<Teacher> listResult = Teacher.getListTeacher(teacher);
            gridTeacher.DataSource = listResult;
            gridTeacher.Rebind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("liste_professeurs_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                gridTeacher.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gridTeacher.ExportSettings.IgnorePaging = true;
                gridTeacher.ExportSettings.FileName = Path;
                gridTeacher.ExportSettings.ExportOnlyData = true;
                gridTeacher.ExportSettings.OpenInNewWindow = true;
                gridTeacher.MasterTableView.ExportToExcel();

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
}