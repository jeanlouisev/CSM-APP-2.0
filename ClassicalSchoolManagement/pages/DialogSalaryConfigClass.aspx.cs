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



public partial class DialogSalaryConfigClass : System.Web.UI.Page
{
    int menu_code = 5; // parameter menu code, for more information see "menu" table.

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

        //string queryMenu = Request.QueryString["menu"];
        //if (queryMenu != "Economat")
        //{
        //    Response.Redirect("~/Error.aspx");
        //}

        if (!Page.IsPostBack)
        {

            // Session["menu"] = queryMenu;
            Users user = Session["user"] as Users;

            ddlClassroom.SelectedValue = "-1";
            BindDataGridClass();
            //  BindDataGridCourse();
            //loadListPrice();

            //
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

            // clear sessions
            Session.Remove("classroom_id");
            Session.Remove("classroom_fullname");
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            // loop through the grid to disable edit option
            if (gridListClassroom.Visible && gridListClassroom.Rows.Count > 0)
            {
                for (int i = 0; i < gridListClassroom.Rows.Count; i++)
                {
                    ImageButton imgBtn1 = gridListClassroom.Rows[i].Cells[5].FindControl("btnEdit") as ImageButton;
                    imgBtn1.Enabled = false;
                    //
                    ImageButton imgBtn2 = gridListClassroom.Rows[i].Cells[6].FindControl("btnAddCours") as ImageButton;
                    imgBtn2.Enabled = false;
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
    protected void ddlClassroom_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGridClass();
    }

    private void BindDataGridClass()
    {
        try
        {
            int _id = 0;
            int _limit = 0;
            List<ClassRoom> listResult = new List<ClassRoom>();
            if (ddlClassroom.SelectedValue == "-1")
            {
                listResult = ClassRoom.getListAllClassroom();
            }
            else if (ddlClassroom.SelectedValue != "-1")
            {
                if (ddlClassroom.SelectedValue == "1"
                    || ddlClassroom.SelectedValue == "2"
                    || ddlClassroom.SelectedValue == "3")
                {
                    _id = Convert.ToInt32(ddlClassroom.SelectedValue);
                    _limit = _id;
                }
                else
                {
                    _id = Convert.ToInt32(ddlClassroom.SelectedValue);
                    _limit = _id + 9;
                }

                listResult = ClassRoom.getListClassroomForSalaryById(_id, _limit);
            }

            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
                pnlEditClassroom.Visible = false;
                //tblGridHeader.Visible = true;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                pnlEditClassroom.Visible = false;
                //tblGridHeader.Visible = false;
            }
            gridListClassroom.DataSource = listResult;
            gridListClassroom.DataBind();
            pnlEditClassroom.Visible = false;
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    protected void gridListClassroom_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        try
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button clicked
            // by the user from the Rows collection.
            GridViewRow row = gridListClassroom.Rows[index];
            int classroomId = int.Parse(gridListClassroom.DataKeys[index].Value.ToString());
            string classFullname = gridListClassroom.Rows[index].Cells[1].Text;

            if (e.CommandName == "define_class_amount")
            {
                Session["classroom_id"] = classroomId;
                Session["classroom_fullname"] = classFullname;
                int winHeight = 350;
                int winWidth = 750;

                if (!Universal.classroomIsPrimary(classroomId))
                {
                    winHeight = 500;
                    winWidth = 850;
                }

                string page_url = "SalaryClassConfigDetails.aspx";

                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(" + winWidth + ", " + winHeight + ");"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        catch { }
    }

    protected void gridListClassroom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label a = e.Row.FindControl("lblstatusClass") as Label;
            if (a.Text == "Inactif")
            {
                //e.Row.Cells[7].Enabled = false;
                //e.Row.Cells[7].Visible = false;
                //e.Row.Cells[7].BackColor = Color.LightGray;
            }

            if (e.Row.RowIndex == 0)
            {
                e.Row.Style.Add("height", "55px");
                e.Row.Style.Add("vertical-align", "bottom");
            }
        }

        */
    }

    protected void updateClassInfo(object sender, EventArgs e)
    {
        RadButton button = sender as RadButton;
        if (button.CommandName == "finishReturn")
        {
            //refresh the gridview
            BindDataGridClass();
        }
        if (button.CommandName == "updateClassroom")
        {
            lblResultEditClassroom.Visible = false;
            lblResultEditClassroom.Text = string.Empty;
            ClassRoom classroom = new ClassRoom();
            classroom.fixed_capacity = Convert.ToInt32(txtMaxQuantityClassroom.Text);
            classroom.id = Convert.ToInt32(txtCodeClassroom.Text);
            //
            int actualCapacity = int.Parse(txtActualQuantityClassroom.Text.Trim());
            //
            if (chkActive.Checked)
            {
                classroom.status = 1;
            }
            if (chkNotActive.Checked)
            {
                classroom.status = 0;
            }

            if (classroom.fixed_capacity < 50)
            {
                MessBox.Show("Effectif Maximal ne doit etre inferieur a 50");
            }
            else if (classroom.fixed_capacity < actualCapacity)
            {
                MessBox.Show("Effectif Maximal ne doit etre inferieur a l\'effectif actuel");
            }

            else
            {
                //update information for classroom
                ClassRoom.updateClassroomInfo(classroom);
                //update vacation information
                /* if (chkAM.Checked)
                 {
                     string vacationType = "AM";
                     int vacationStatus = 1;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 else
                 {
                     string vacationType = "AM";
                     int vacationStatus = 0;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 if (chkPM.Checked)
                 {
                     string vacationType = "PM";
                     int vacationStatus = 1;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 else
                 {
                     string vacationType = "PM";
                     int vacationStatus = 0;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 if (chkNG.Checked)
                 {
                     string vacationType = "NG";
                     int vacationStatus = 1;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 else
                 {
                     string vacationType = "NG";
                     int vacationStatus = 0;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 if (chkWK.Checked)
                 {
                     string vacationType = "WK";
                     int vacationStatus = 1;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }
                 else
                 {
                     string vacationType = "WK";
                     int vacationStatus = 0;
                     ClassRoom.updateClassroomVacation(vacationStatus, vacationType, classroom.id);
                 }*/

                //
                lblResultEditClassroom.Text = "Classe modifier avec succes !";
                lblResultEditClassroom.Visible = true;
            }
        }
    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        if (chkActive.Checked)
        {
            chkNotActive.Checked = false;
            udpCheckboxes.Update();
        }
        else
        {
            chkNotActive.Checked = true;
            udpCheckboxes.Update();
        }
    }

    protected void chkNotActive_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNotActive.Checked)
        {
            chkActive.Checked = false;
            udpCheckboxes.Update();
        }
        else
        {
            chkActive.Checked = true;
            udpCheckboxes.Update();
        }
    }

    protected void gridListCourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
            e.Row.Style.Add("vertical-align", "bottom");
        }*/
    }

    protected void gridListCourse_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListCourse.Rows[index];
        int code = int.Parse(row.Cells[0].Text);

        if (e.CommandName == "Edit")
        {

        }
        if (e.CommandName == "Delete")
        {
            Course.deleteCourse(code);
        }
    }

    protected void btnBack_click(object sender, EventArgs e)
    {
        pnlResult.Visible = true;
        pnlSearchClassroom.Visible = true;
        pnlEditClassroom.Visible = true;
        pnlAddCoursToClassroom.Visible = false;
        pnlEditClassroom.Visible = false;

    }

    private void BindDataGridCourse()
    {
        Course c = new Course();
        //c.name = txtCourseName.Text.Trim().Length <= 0 ? "%" : txtCourseName.Text.Trim().ToLower() + "%";
        // string vacation = Session["vacation"].ToString();
        List<Course> listResult = Course.getAllListCourseInverse();

        if (listResult.Count > 0)
        {
            // lblFound.Visible = false;
            //  pnlResult.Visible = true;
            //  lblCounter.Visible = true;
            //  lblCounter.Text = "Record : " + listResult.Count.ToString();
            Table1.Visible = true;
            gridListCourse.Visible = true;
        }
        else
        {
            //  lblFound.Visible = true;
            //   pnlResult.Visible = true;
            // lblCounter.Visible = false;
            Table1.Visible = false;
            gridListCourse.Visible = false;
        }
        gridListCourse.DataSource = listResult;
        gridListCourse.DataBind();
        gridListCourse.HeaderRow.Visible = false;
        foreach (GridViewRow row in gridListCourse.Rows)//Running all lines of grid
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlprice = (row.Cells[0].FindControl("ddlprice") as DropDownList);
                List<Course> listPrice = Course.getListCoursePrices();
                ddlprice.DataValueField = "id";
                ddlprice.DataTextField = "price";
                ddlprice.DataSource = listPrice;
                ddlprice.DataBind();
            }
        }
    }

    protected void btnAffecterCours_click(object sender, EventArgs e)
    {
        // string vacation = ddlVacation.SelectedValue;
        //Session["vacation"] = ddlVacation.SelectedValue;
        string vacation = Session["vacation"].ToString();

        //Course.deletePreviouslyAffectedCourseToClassroom(Convert.ToInt32(lblClassCode.Text), vacation);

        // Course.deletePreviouslyAffectedCourseToClassroom(Convert.ToInt32(lblClassCode.Text), vacation);
        foreach (GridViewRow row in gridListCourse.Rows)//Running all lines of grid
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int index = row.RowIndex;
                CheckBox chkRow = row.Cells[0].FindControl("chkRow") as CheckBox;
                DropDownList ddl = row.Cells[2].FindControl("ddlprice") as DropDownList;
                if (ddl.SelectedValue != null)
                {
                    if (chkRow.Checked)
                    {
                        int classCode = Convert.ToInt32(lblClassCode.Text);
                        int courseId = Convert.ToInt32(gridListCourse.DataKeys[index].Value.ToString());
                        int idCoursePrice = Convert.ToInt32(ddl.SelectedValue);
                        //  Course.affectCourseToclasse(classCode, idCoursePrice, courseId, vacation);
                    }
                }
                else
                {

                }
            }
        }
        MessBox.Show("Cours ajouté (s) avec succès !");
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

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

        lblNoclasse.Text = gridListClassroom.SelectedRow.Cells[2].Text;

        //Accessing BoundField Column
        //string name = gridListClassroom.SelectedRow.Cells[0].Text;

        //Accessing TemplateField Column controls
        //string country = (gridListClassroom.SelectedRow.FindControl("lblCountry") as Label).Text;

        //txtNoclasse.Text =  name ;
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

}