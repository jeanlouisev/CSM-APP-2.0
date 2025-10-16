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


public partial class DefineShedule : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;


        if (!Page.IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;
                //
                ddlClassroom.SelectedValue = "-1";
                loadListActiveClassroom();
                List<Course> listCourse = Course.getListAllCourseAttachedToTeacher();
                //
                loadListAcademicYear();
                //BindDataGridClass();
            }
        }
    }

    private void loadListAcademicYear()
    {
        try
        {

            ddlAcademicYear.Items.Clear();
            // get list all academic  year
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();

            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddlAcademicYear.DataValueField = "id";
                ddlAcademicYear.DataTextField = "years";
                ddlAcademicYear.DataSource = listAcademicYear;
                ddlAcademicYear.DataBind();
                int maxAcademicYear = Settings.getAcademicYear();
                if (maxAcademicYear != 0)
                {
                    ddlAcademicYear.SelectedValue = maxAcademicYear.ToString();
                }
            }
        }
        catch (Exception ex) { }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            //// loop through the grid to disable delete option
            //if (gridListClassroom.Visible && gridListClassroom.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListClassroom.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn1 = gridListClassroom.Rows[i].Cells[5].FindControl("btnDefineSchedule") as ImageButton;
            //        imgBtn1.Enabled = false;
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
            //if (gridListReason.Visible && gridListReason.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListReason.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn1 = gridListReason.Rows[i].Cells[2].FindControl("btnDelete") as ImageButton;
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
    
    private void loadListVacation(List<ClassRoom> listClassroom)
    {
        //clear items
        ddlVacation.Items.Clear();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            // fill the ddl now
            ddlVacation.DataValueField = "vacation_type";
            ddlVacation.DataTextField = "vacation_name";
            ddlVacation.DataSource = listClassroom;
            ddlVacation.DataBind();
        }
    }

    private void loadListAcademicYear(RadDropDownList ddl)
    {
        try
        {
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();
            ddl.Items.Clear();
            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    
    //load all cours and all teachecr  in combo
    private void loadListAllCours(RadDropDownList ddl)
    {
        List<Course> courseList = Course.getListAllCourseActive();
        if (courseList.Count > 0)
        {
            ddl.DataSource = courseList;
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddl.SelectedValue = "-1";
        }
    }

    private void loadListAllTeacher(RadDropDownList ddl)
    {
        List<Teacher> teacherList = Teacher.getListActiveTeacher();
        if (teacherList.Count > 0)
        {
            ddl.DataSource = teacherList;
            ddl.DataTextField = "fullname";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        }
        else
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddl.SelectedValue = "-1";
        }
    }
    
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        radGridSchedule.Rebind();
    }

    private void loadListActiveClassroom()
    {
        try
        {
            List<ClassRoom> listActiveClassroom = ClassRoom.getListActiveClassroom();
            if (listActiveClassroom != null && listActiveClassroom.Count > 0)
            {
                // fill the ddl now
                ddlClassroom.DataValueField = "id";
                ddlClassroom.DataTextField = "name";
                ddlClassroom.DataSource = listActiveClassroom;
                ddlClassroom.DataBind();
            }
            ddlClassroom.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));
            ddlClassroom.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }
        
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // reload the grid
        radGridSchedule.Rebind();
    }

    protected void radGridSchedule_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            ClassRoom c = new ClassRoom();
            c.id = int.Parse(ddlClassroom.SelectedValue);
            c.vacation = ddlVacation.SelectedValue;
            c.academic_year_id = int.Parse(ddlAcademicYear.SelectedValue);

            List<ClassRoom> listResult = ClassRoom.getListActiveClassroomForSchedule(c);
            radGridSchedule.DataSource = listResult;
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void radGridSchedule_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridSchedule_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridSchedule.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnAddSchedule_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        int classId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
        HiddenField hiddenVacation = (HiddenField)dataItem.FindControl("hiddenVacation");
        HiddenField hiddenClassName = (HiddenField)dataItem.FindControl("hiddenClassName");

        ClassRoom c = new ClassRoom();
        c.id = classId;
        c.class_name = hiddenClassName.Value;
        c.vacation = hiddenVacation.Value;

        Session["classroom_obj"] = c;

        string page_url = "DialogAddSchedule.aspx";
        try
        {
            //Response.Redirect("DocumentDetail.aspx");
            //Session["type_detail"] = "endedit";
            //mp1.Show();
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow2\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            //+ "oWinn.maximize();"
                                            + "oWinn.SetSize(1124, 600);"
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    


}
