using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using Telerik.Web.UI;



public partial class AffectCoursToTeacher : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.HR;


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

            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;
                //fire the search button event
                btnSearch_Click(this, e);
            }
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    txtStaffCode.ReadOnly = true;
        //    txtFullname.ReadOnly = true;
        //    btnSearch.Enabled = false;

        //    // loop through the grid to disable edit option
        //    if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListTeacher.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListTeacher.Rows[i].Cells[4].FindControl("btnAffectCours") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur :" + ex.Message);
        //}
    }

    // delete
    private void disableDeleteOption()
    {
        try
        {
            // do nothing
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
            BindDataGridTeacher();
            Users user = Session["user"] as Users;
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
            MessBox.Show("Erreur : " + ex.Message);

        }
    }

    private void BindDataGridTeacher()
    {
        Teacher teacher = new Teacher();
        //get the fields from the form
        teacher.id = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
        teacher.fullName = txtFullname.Text.Trim().Length <= 0 ? null : txtFullname.Text.Trim(); ;

        //get list of students
        List<Teacher> listResult = Teacher.getListTeacherCourse(teacher);
        radGridTeachers.DataSource = listResult;
        radGridTeachers.DataBind();
    }
        
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        BindDataGridTeacher();
    }

    protected void radGridTeachers_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridTeachers_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridTeachers.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnAffectCourse_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            Session["teacher_id"] = dataItem.GetDataKeyValue("id").ToString();
            //
            string page_url = "DialogAffectCourseToTeacher.aspx";
            //Response.Redirect(page_url);
            try
            {
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(800, 650); oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}