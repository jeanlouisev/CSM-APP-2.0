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
using Db_Core;
//using System.Data.OracleClient;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Web.Services;



public partial class DialogAffectCourseToTeacher : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // for telerik validation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;
        //
        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindDataGridCourse();
            LoadPreviousData();
        }
    }

    private void LoadPreviousData()
    {
        String teacherId = Session["teacher_id"].ToString();
        Teacher t = Teacher.getTeacherInfoById(teacherId);
        if (t != null)
        {
            lblTeacherCode.Text = teacherId;
            lblTeacherFullName.Text = t.fullName;

            // get all course attached
            List<Course> listAttachedCourse = Course.getListAttachedCourseByTeacherId(teacherId);
            if(listAttachedCourse != null && listAttachedCourse.Count > 0)
            {
                foreach(Course c in listAttachedCourse)
                {
                    // loop through the grid
                    foreach (GridItem item in radGridCours.MasterTableView.Items)//Running all lines of grid
                    {
                        CheckBox chkRow = (item.FindControl("chkRow") as CheckBox);
                        HiddenField hiddenCourseId = (HiddenField)item.FindControl("hiddenCourseId");
                        int courseId = int.Parse(hiddenCourseId.Value);
                        if(c.id == courseId)
                        {
                            chkRow.Checked = true;
                        }
                    }
                }
            }
        }

    }

    private void BindDataGridCourse()
    {
        try
        {
            //get list of students
            List<Course> listResult = Course.getAllListCourseOrderedByNameAsc();

            radGridCours.DataSource = listResult;
            radGridCours.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void radGridCours_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridCours_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridCours.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnDeleteCourse_ServerClick(object sender, EventArgs e)
    {

    }

    protected void btnAffect_ServerClick(object sender, EventArgs e)
    {
        string teacherId = Session["teacher_id"].ToString();
        List<int> listCourseId = new List<int>();

        // get all checked items
        foreach (GridItem item in radGridCours.MasterTableView.Items)//Running all lines of grid
        {
            CheckBox chkRow = (item.FindControl("chkRow") as CheckBox);
            HiddenField hiddenCourseId = (HiddenField)item.FindControl("hiddenCourseId");
            if (chkRow.Checked)
            {
                int courseId = int.Parse(hiddenCourseId.Value);
                listCourseId.Add(courseId);
            }
        }

        
        if (listCourseId.Count > 0)
        {
            try
            {
                //delete previously affected course for current teacher
                Course.deletePreviouslyAffectedCourseToTeacher(teacherId);
                //affect new course to teacher
                foreach (var id in listCourseId)
                {
                    Course.affectCourseToTeacher(teacherId, Convert.ToInt32(id));
                }
                //remove session
                Session.Remove("teacher_id");
                Session["teacher_id"] = null;
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
        else
        {
            try
            {
                //delete previously affected course for current teacher
                Course.deletePreviouslyAffectedCourseToTeacher(teacherId);
                //remove session
                Session.Remove("teacher_id");
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
        

        // close modal window after validation
        this.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseDialog();", true);
    }

    protected void btnCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        if (chk.Checked)
        {
            //check all boxes in grid
            foreach (GridItem item in radGridCours.MasterTableView.Items)//Running all lines of grid
            {
                CheckBox chkRow = (CheckBox)item.FindControl("chkRow");
                chkRow.Checked = true;
            }
        }
        else
        {
            // uncheck all boxes in grid
            foreach (GridItem item in radGridCours.MasterTableView.Items)//Running all lines of grid
            {
                CheckBox chkRow = (CheckBox)item.FindControl("chkRow");
                chkRow.Checked = false;
            }
        }
    }
}