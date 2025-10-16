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



public partial class ClassroomManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.CLASSROOM;
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
            //// VERIFY USER ACCESS LEVEL
            //List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
            //if (listResult != null && listResult.Count > 0)
            //{
            //    Users userAccess = listResult[0];
            //    int notGranted = (int)Users.ACCESS.NO;

            //    radGridClassroom.Rebind();

            //    // edit
            //    if (userAccess.edit_access == notGranted)
            //    {
            //        disableEditOption();
            //    }

            //    // delete
            //    if (userAccess.delete_access == notGranted)
            //    {
            //        disableDeleteOption();
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Pages/NoPrivilegeWarningPage.aspx");
            //}
        }


        if (!Page.IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                // SHOW ITEMS
                //pnlSearchClassroom.Visible = true;
                //pnlClassroomConfiguration.Visible = true;
                pnlAddCoursToClassroom.Visible = false;
                //pnlClassroomConfiguration.Visible = false;

                // load data
                ddlClassroom.SelectedValue = "-1";
                loadListAcademicYear(ddlAcademicYear);
                // load all classrooms
                //  BindDataGridClass();
            }

        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (radGridClassroom.Visible && radGridClassroom.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in radGridClassroom.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnConfigClass = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnConfigClass");
                    System.Web.UI.HtmlControls.HtmlButton btnAddCoursePrice = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnAddCoursePrice");
                    System.Web.UI.HtmlControls.HtmlButton btnActivateDesactivateClass = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnActivateDesactivateClass");
                    //
                    btnConfigClass.Attributes.Add("disabled", "disabled");
                    btnAddCoursePrice.Attributes.Add("disabled", "disabled");
                    btnActivateDesactivateClass.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete
    private void disableDeleteOption()
    {
            // do nothing
    }
    /******************************************* END USER POLICY **************************/

    protected void ddlClassroom_SelectedIndexChanged(object sender, EventArgs e)
    {
        radGridClassroom.Rebind();
    }

    private void loadDllActiveVaction(RadDropDownList dropDownList)
    {
        int classId = int.Parse(Session["id_classe"].ToString());
        List<ClassRoom> listClass = ClassRoom.getListVacationByClassroomId(classId);
        loadListVacation(listClass);
        ClassRoom classroom = new ClassRoom();
        dropDownList.DataValueField = "classroom_id";
        dropDownList.DataTextField = "name";
        dropDownList.DataBind();
        //get the select vation in ddlvacation
        //Session["vacation"] = ddlVacationConfig.SelectedValue;

    }

    private void loadListVacation(List<ClassRoom> listClassroom)
    {
        ////clear items
        //ddlVacation.Items.Clear();
        //if (listClassroom != null && listClassroom.Count > 0)
        //{
        //    // fill the ddl now
        //    ddlVacation.DataValueField = "vacation_type";
        //    ddlVacation.DataTextField = "vacation";
        //    ddlVacation.DataSource = listClassroom;
        //    ddlVacation.DataBind();
        //}

    }

    //private void BindDataGridClass()
    //{
    //    try
    //    {
    //        if (Session["user"] == null)
    //        {
    //            Response.Redirect("~/Error.aspx");
    //        }
    //        //
    //        Users user = Session["user"] as Users;

    //        int _id = 0;
    //        int _limit = 0;
    //        int _academicYear = ddlAcademicYear1.SelectedValue == null ? 0 : Convert.ToInt32(ddlAcademicYear1.SelectedValue.ToString());
    //        List<ClassRoom> listResult = new List<ClassRoom>();
    //        if (ddlClassroom.SelectedValue == "-1")
    //        {
    //            listResult = ClassRoom.getListAllClassroomWithCapacity(_academicYear);
    //        }
    //        else if (ddlClassroom.SelectedValue != "-1")
    //        {
    //            if (ddlClassroom.SelectedValue == "1"
    //                || ddlClassroom.SelectedValue == "2"
    //                || ddlClassroom.SelectedValue == "3")
    //            {
    //                _id = Convert.ToInt32(ddlClassroom.SelectedValue);
    //                _limit = _id;
    //            }
    //            else
    //            {
    //                _id = Convert.ToInt32(ddlClassroom.SelectedValue);
    //                _limit = _id + 9;
    //            }
    //            //
    //            listResult = ClassRoom.getListClassroomById(_id, _limit, _academicYear);
    //        }

    //        if (listResult != null && listResult.Count > 0)
    //        {
    //            //lblCounter.Text = listResult.Count + " Ligne(s)";
    //            pnlClassroomConfiguration.Visible = false;
    //            //tblGridHeader.Visible = true;
    //        }
    //        else
    //        {
    //            pnlClassroomConfiguration.Visible = false;
    //            //tblGridHeader.Visible = false;
    //        }
    //        pnlClassroomConfiguration.Visible = false;

    //        // check login_user policy to grant or revoke access
    //        List<Users> listGroupPolicy = Users.getListGroupPolicyByRoleId(user.role_id, menu_code);
    //        if (listGroupPolicy != null && listGroupPolicy.Count > 0)
    //        {
    //            if (listGroupPolicy[0].role_view == 0
    //                && listGroupPolicy[0].role_edit == 0
    //                && listGroupPolicy[0].role_delete == 0)
    //            {
    //                //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
    //                Response.Redirect("~/Default.aspx");
    //            }
    //            else
    //            {
    //                // edit
    //                if (listGroupPolicy[0].role_edit == 0)
    //                {
    //                    disableEditOption();
    //                }
    //                // delete
    //                if (listGroupPolicy[0].role_delete == 0)
    //                {
    //                    disableDeleteOption();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Error :" + ex.Message);
    //    }
    //}

    private void CheckClassCoursAffected()
    {

    }

    private void loadListAcademicYear(RadDropDownList ddl)
    {
        ddl.Items.Clear();
        List<Settings> listAcademicYear = Settings.getAcademicYearFull();
        if (listAcademicYear != null && listAcademicYear.Count > 0)
        {
            ddl.DataValueField = "id";
            ddl.DataTextField = "years";
            ddl.DataSource = listAcademicYear;
            ddl.DataBind();
        }
        else
        {
            ddl.Items.Insert(0, new DropDownListItem("--Selectionner--", "-1"));
            ddl.SelectedValue = "-1";
        }
    }

    private void loadClassroomListForPrice()
    {
        ddlClassroomPrice.Items.Clear();
        List<ClassRoom> classroomList = ClassRoom.getListActiveClassroom();
        if (classroomList != null && classroomList.Count > 0)
        {
            ddlClassroomPrice.DataValueField = "id";
            ddlClassroomPrice.DataTextField = "name";
            ddlClassroomPrice.DataSource = classroomList;
            ddlClassroomPrice.DataBind();
        }
        ddlClassroomPrice.Items.Insert(0, new RadComboBoxItem("--Selectionner--", "-1"));
        ddlClassroomPrice.SelectedValue = "-1";
    }

    protected void gridListAffectCourse_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        /*
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListReference.Rows[index];
        // Session["reference_code"] = row.Cells[1].Text;

        // If multiple buttons are used in a GridView control, use the
        // CommandName property to determine which button was clicked.
        if (e.CommandName == "affect")
        {
            MessBox.Show("Test affect ok");
        }*/
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        radGridClassroom.Rebind();
    }

    protected void ddlClassroomCurrentStatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        radGridClassroom.Rebind();
    }

    protected void radGridClassroom_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int classId = int.Parse(ddlClassroom.SelectedValue);
            int currentStatus = int.Parse(ddlClassroomCurrentStatus.SelectedValue);
            //
            List<ClassRoom> listResult = ClassRoom.getListClassroom(classId, currentStatus);
            radGridClassroom.DataSource = listResult;
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    protected void radGridClassroom_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridClassroom_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridClassroom.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }


    // Edit classroom configuration
    //protected void btnConfigClass_ServerClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        pnlClassroomList.Visible = false;
    //        pnlClassroomConfiguration.Visible = true;
    //        //
    //        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
    //        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
    //        int index = dataItem.RowIndex;
    //        int classroomId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
    //        Session["classroom_id_config"] = classroomId;
    //        //
    //        int academicYearId = int.Parse(ddlAcademicYear.SelectedValue);
    //        HiddenField hiddenClassName = (HiddenField)dataItem.FindControl("hiddenClassroomName");
    //        HiddenField hiddenStatus = (HiddenField)dataItem.FindControl("hiddenStatus");

    //        //string classroomName = 
    //        //int status = int.Parse(hiddenStatus.Value);
    //        //
    //        txtAcademicYearConfig.Text = ddlAcademicYear.SelectedText;
    //        txtClassNameConfig.Text = hiddenClassName.Value;
    //        ddlVacation.SelectedValue = "-1";
    //        txtFixedCapacityConfig.Value = null;
    //        txtCurrentCapacityConfig.Value = null;

    //        // set hidden values
    //        hiddenClassIdConfig.Value = classroomId.ToString();
    //        hiddenAcademicYearIdConfig.Value = academicYearId.ToString();

    //        // load list vacation data
    //        gridVacationConfig.Rebind();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Erreur : " + ex.Message);
    //    }
    //}
    /*
    protected void btnSaveClassConfig_ServerClick(object sender, EventArgs e)
    {
        if (ddlVacation.SelectedValue == "-1")
        {
            msgContent = "Erreur : veuillez sélectionner une vacation !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            string vacationCode = ddlVacation.SelectedValue;
            int fixedCapacity = int.Parse(txtFixedCapacityConfig.Value.ToString());
            int currentCapacity = int.Parse(txtCurrentCapacityConfig.Value.ToString());
            if (fixedCapacity < currentCapacity)
            {
                msgContent = "Erreur : Effectif maximal doit être supérieur à effectif actuel !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                int classId = int.Parse(hiddenClassIdConfig.Value.ToString());
                ClassRoom.updateClassroomVacationFixedCapacity(classId, fixedCapacity, vacationCode);
                // refresh the gridview value
                gridVacationConfig.Rebind();
            }
            //MessageAlert.RadAlert("Supprimé avec succès !", 350, 150, "Information", null, "../images/success_check.png");
        }



        //int classId = int.Parse(hiddenClassIdConfig.Value.ToString());
        //int currentStatus = 1;  //---> 0.- not active  | 1.- active  // int.Parse(ddlClassCurrentStatusConfig.SelectedValue);
        //ClassRoom.changeClassroomStatus(classId, currentStatus);
        //// refresh classroom grid
        //radGridClassroom.Rebind();
        ////
        //pnlClassroomConfiguration.Visible = false;
        //pnlClassroomList.Visible = true;
    }

    protected void ddlVacationConfig_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int classId = int.Parse(hiddenClassIdConfig.Value);
        int academicYearId = int.Parse(hiddenAcademicYearIdConfig.Value);
        string vacation = null; // ddlVacationConfig.SelectedValue;
        generateClassMaxAndActualQuantity(classId, academicYearId, vacation);
    }
  
    private void generateClassMaxAndActualQuantity(int classId, int academicYearId, string vacation)
    {

    }
     

    protected void gridVacationConfig_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["classroom_id_config"] != null)
        {
            int classId = int.Parse(Session["classroom_id_config"].ToString());
            List<ClassRoom> classroomList = ClassRoom.getListVacationConfig(classId);
            gridVacationConfig.DataSource = classroomList;
        }
        else
        {
            gridVacationConfig.DataSource = null;
        }
    }

    protected void gridVacationConfig_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridVacationConfig_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            string vacation = item.Cells[2].Text.Trim();
            switch (vacation)
            {
                case "AM": item.Cells[2].Text = "Matin"; break;
                case "PM": item.Cells[2].Text = "Mediant"; break;
                case "NG": item.Cells[2].Text = "Soir"; break;
                case "WK": item.Cells[2].Text = "Weekend"; break;
            }


        }
    }

    protected void btnEditVacationConfig_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        ddlVacation.SelectedValue = dataItem.Cells[3].Text;
        txtFixedCapacityConfig.Value = int.Parse(dataItem.Cells[4].Text);
        txtCurrentCapacityConfig.Value = int.Parse(dataItem.Cells[5].Text);
        //
        txtFixedCapacityConfig.Focus();
    }

    protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int classId = int.Parse(Session["classroom_id_config"].ToString());
        string vacation = ddlVacation.SelectedValue;
        //
        // set new values
        ClassRoom c = ClassRoom.getClassroomCapacityConfigByVacation(classId, vacation);
        if (c != null)
        {
            txtFixedCapacityConfig.Value = c.fixed_capacity;
            txtCurrentCapacityConfig.Value = c.current_capacity;
        }
        else
        {
            txtFixedCapacityConfig.Value = null;
            txtCurrentCapacityConfig.Value = null;
        }
    }
 */

    // Affect course and price to classroom
    protected void btnAddCoursePrice_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            // load active classroom
            loadClassroomListForPrice();

            HiddenField hiddenClassName = (HiddenField)dataItem.FindControl("hiddenClassroomName");
            HiddenField hiddenStatus = (HiddenField)dataItem.FindControl("hiddenStatus");
            //
            int classId = int.Parse(dataItem.GetDataKeyValue("id").ToString());

            ddlClassroomPrice.SelectedValue = classId.ToString();
            //hiddenAcademicYearId_price.Value = ddlAcademicYear.SelectedValue;
            //txtAcademicYear_price.Text = ddlAcademicYear.SelectedText;

            loadActiveCourses();
            //loadAllCoursePrice();

            //if (classId < 70)
            //{
            //    ddlCourseHour_price.SelectedIndex = 1;
            //    ddlCourseHour_price.Enabled = false;
            //}
            //else
            //{
            //    ddlCourseHour_price.SelectedValue = "-1";
            //    ddlCourseHour_price.Enabled = true;
            //}

            // hide | show panels
            pnlAddCoursToClassroom.Visible = true;
            pnlClassroomList.Visible = false;
            // reload cours & price grid
            radGridAffectedCourse.Rebind();

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadActiveCourses()
    {
        ddlCourseNamePrice.Items.Clear();
        List<Course> courseList = Course.getListAllCourseActive();
        if (courseList != null && courseList.Count > 0)
        {
            ddlCourseNamePrice.DataTextField = "name";
            ddlCourseNamePrice.DataValueField = "id";
            ddlCourseNamePrice.DataSource = courseList;
            ddlCourseNamePrice.DataBind();
        }
        //
        //ddlCourseNamePrice.Items.Insert(0, new RadComboBoxItem("--Selectionner--", "-1"));
        //ddlCourseNamePrice.SelectedValue = "-1";
    }

    protected void btnAffectCoursePrice_ServerClick(object sender, EventArgs e)
    {
        // verify class selection
        if (radGridAffectedCourse.Items.Count > 0
            && ddlClassroomPrice.SelectedValue == "-1")
        {
            msgContent = "Erreur : veuillez sélectionner une classe !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/warning.png");
        }
        else
        {
            try
            {
                List<Course> listCourseInfo = null;

                // get data from grid, if records found
                if (radGridAffectedCourse.Items.Count > 0)
                {
                    listCourseInfo = new List<Course>();
                    foreach (GridDataItem item in radGridAffectedCourse.Items)
                    {
                        HiddenField hiddenClassId = (HiddenField)item.FindControl("hiddenClassId");
                        HiddenField hiddenCourseId = (HiddenField)item.FindControl("hiddenCourseId");
                        RadNumericTextBox txtCoefficient = (RadNumericTextBox)item.FindControl("txtCoefficient");
                        //RadNumericTextBox txtPricePerHour = (RadNumericTextBox)item.FindControl("txtPricePerHour");
                        //
                        Course c = new Course();
                        c.class_id = int.Parse(hiddenClassId.Value);
                        c.cours_id = int.Parse(hiddenCourseId.Value);
                        c.coefficient = int.Parse(txtCoefficient.Value.ToString());
                        //c.course_price = double.Parse(txtPricePerHour.Value.ToString());
                        //
                        listCourseInfo.Add(c);
                    }
                    // save
                    Course.affectCoursePriceToClass(listCourseInfo);
                }

                // make sure user select a valid classroom before validation
                if (ddlClassroomPrice.SelectedValue != "-1")
                {
                    listCourseInfo = new List<Course>();
                    // get value from comboboxes
                    foreach (RadComboBoxItem item in ddlCourseNamePrice.Items)
                    {
                        Course c = new Course();
                        if (item.Checked)
                        {
                            c.class_id = int.Parse(ddlClassroomPrice.SelectedValue);
                            c.cours_id = int.Parse(item.Value);
                            c.coefficient = 0;
                            c.amount = 0;
                            listCourseInfo.Add(c);
                        }
                    }
                    // save
                    Course.affectCoursePriceToClass(listCourseInfo);
                }

                // reload the grid
                radGridAffectedCourse.Rebind();
                MessageAlert.RadAlert("Succès !", 350, 150, "Information", null, "../images/success_check.png");

            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
    }

    protected void btnViewListClass_ServerClick(object sender, EventArgs e)
    {
        // remove un-used sessions
        Session.Remove("classroom_id_config");

        // show & hide panels
        pnlClassroomList.Visible = true;
        pnlAddCoursToClassroom.Visible = false;
    }

    protected void radGridAffectedCourse_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<Course> listResult = new List<Course>();
        //
        if (pnlAddCoursToClassroom.Visible)
        {
            int classId = int.Parse(ddlClassroomPrice.SelectedValue);
            listResult = Course.getListAffectedCoursePrice(classId);
        }
        radGridAffectedCourse.DataSource = listResult;


        if (listResult != null && listResult.Count > 0)
        {
            // make sure the use wont be able to select a course already in grid from combo
            foreach (Course c in listResult)
            {
                // disable existing course from combo
                foreach (RadComboBoxItem item in ddlCourseNamePrice.Items)
                {
                    int cboCourseId = int.Parse(item.Value);
                    if (cboCourseId == c.cours_id)
                    {
                        item.Enabled = false;
                        item.Checked = false;
                    }
                }
            }
        }
        else
        {
            foreach (RadComboBoxItem item in ddlCourseNamePrice.Items)
            {
                item.Enabled = true;
                item.Checked = false;
            }
        }
    }

    protected void radGridAffectedCourse_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridAffectedCourse_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (pnlAddCoursToClassroom.Visible)
        {
            int cnt = radGridAffectedCourse.Items.Count + 1;
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Label lbl = (Label)item.FindControl("labelNo");
                lbl.Text = cnt.ToString();

                //HiddenField hiddenClassId = (HiddenField)item.FindControl("hiddenClassId");
                //RadNumericTextBox txtPricePerHour = (RadNumericTextBox)item.FindControl("txtPricePerHour");
                //int classId = int.Parse(hiddenClassId.Value);

                //if (classId < 70)
                //{
                //    txtPricePerHour.Enabled = false;
                //}
            }
        }
    }

    protected void btnRemoveAffectedCourse_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
        Course.removeAffectedCourseById(id);
        //
        radGridAffectedCourse.Rebind();
        MessageAlert.RadAlert("Supprimé avec succès !", 350, 150, "Information", null, "../images/success_check.png");

    }

    protected void ddlClassroomPrice_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        radGridAffectedCourse.Rebind();
    }

    protected void btnActivateDesactivateClass_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        HiddenField hiddenClassStatus = (HiddenField)dataItem.FindControl("hiddenClassStatus");
        int classroomId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
        int classStatus = int.Parse(hiddenClassStatus.Value);

        int changedStatus = classStatus == (int)ClassRoom.STATUS.ACTIVE ? (int)ClassRoom.STATUS.NOT_ACTIVE : (int)ClassRoom.STATUS.ACTIVE;
        ClassRoom.changeClassroomStatus(classroomId, changedStatus);
        // reload classroom grid.
        radGridClassroom.Rebind();
    }
}