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



public partial class AverageManagement : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {
            loadListAcademicYear();
            loadListActiveClassroom();
            loadPassingAverage();
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
            btnValidate1.Attributes.Add("disabled", "disabled");

            //// check if grid notes is visible
            //if (gridNotes.Visible && gridNotes.MasterTableView.Items.Count > 0)
            //{
            //    foreach (GridItem item in gridNotes.MasterTableView.Items)//Running all lines of grid
            //    {
            //        RadNumericTextBox txtNoteObtained = (item.Cells[9].FindControl("txtNoteObtained") as RadNumericTextBox);
            //        txtNoteObtained.Enabled = false;
            //    }
            //}
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

    //protected void txtAverage_TextChanged(object sender, EventArgs e)
    //{
    //    // hide the error label
    //    lblErrorMessage.Visible = false;
    //    //
    //    RadNumericTextBox txt = sender as RadNumericTextBox;
    //    if (txt.Value != null)
    //    {
    //        Double val = Double.Parse(txt.Value.ToString());
    //        if (val > 100 || val <= 0)
    //        {
    //            // clear the value from the textbox
    //            txt.Value = null;
    //            // display warning message
    //            lblErrorMessage.Text = "Le pourcentage de la moyenne doit être entre 0 et 100 %";
    //            lblErrorMessage.Visible = true;
    //            // MessageAlert.RadAlert("Pourcentage moyenne ne doit pas depasser 100 %", 200, 200, "Warning", null);
    //        }
    //    }
    //}

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    private void loadListActiveClassroom()
    {
        ddlClassroom.Items.Clear();
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
            ddlClassroom.DataSource = listClassroom;
            ddlClassroom.DataBind();
        }
        ddlClassroom.Items.Insert(0, new DropDownListItem("-- Tout selectionner --", "-1"));
        ddlClassroom.SelectedValue = "-1";
    }

    private void loadListAcademicYear()
    {
        ddlAcademicYear.Items.Clear();
        List<Settings> listClassroom = Settings.getAcademicYearFull();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ddlAcademicYear.DataValueField = "id";
            ddlAcademicYear.DataTextField = "years";
            ddlAcademicYear.DataSource = listClassroom;
            ddlAcademicYear.DataBind();
            // get current academic year
            int currentAcademicYear = Settings.getAcademicYear();
            ddlAcademicYear.SelectedValue = currentAcademicYear.ToString();
        }
        else
        {
            ddlAcademicYear.Items.Insert(0, new DropDownListItem("", "-1"));
            ddlAcademicYear.SelectedValue = "-1";
        }
    }

    private void loadPassingAverage()
    {
        for (int i = 1; i <= 100; i++)
        {
            ddlSuccessPercent.Items.Add(new DropDownListItem(i.ToString(), i.ToString()));
        }
        ddlSuccessPercent.Items.Insert(0, new DropDownListItem("-- Selectionner --", "-1"));
        ddlSuccessPercent.SelectedValue = "-1";
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        int successPercent = int.Parse(ddlSuccessPercent.SelectedValue);
        if (successPercent > 0)
        {
            Users user = Session["user"] as Users;
            List<ClassRoom> listClassroomAverage = new List<ClassRoom>();
            //
            if (gridAverage.Visible && gridAverage.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridAverage.MasterTableView.Items)
                {
                    HiddenField classId = (HiddenField)item.FindControl("hiddenClassroomId");
                    HiddenField accId = (HiddenField)item.FindControl("hiddenAcademicYearId");
                    //
                    ClassRoom classroom = new ClassRoom();
                    classroom.success_percent = successPercent;
                    classroom.id = int.Parse(classId.Value.ToString());
                    classroom.academic_year_id = int.Parse(accId.Value.ToString());
                    classroom.login_user = user.username.ToUpper();
                    //
                    listClassroomAverage.Add(classroom);
                }
                // add
                ClassRoom.AddClassroomAverage(listClassroomAverage);
                // reload grid
                gridAverage.Rebind();
                //
                MessageAlert.RadAlert("Succès !", 300, 200, "Information", null, "../images/success_check.png");
            }
        }
        else
        {
            msgContent = "Erreur : Veuillez  selectionner la moyenne de passage !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
    }
    
    protected void btnLoadFromPreviousConfig_Click(object sender, EventArgs e)
    {
        Session["current_academic_year"] = ddlAcademicYear.SelectedValue == null ? "0" : ddlAcademicYear.SelectedValue;
        string page_url = "AveragePreviousConfigurationDetails.aspx";
        try
        {
            //Response.Redirect("DocumentDetail.aspx");
            //Session["type_detail"] = "endedit";
            //mp1.Show();
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(650, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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
    
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        gridAverage.Rebind();
    }

    protected void gridAverage_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int _academicYear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
            int _classId = int.Parse(ddlClassroom.SelectedValue);
            //
            List<ClassRoom> listResult = ClassRoom.getListClassroomAverage(_academicYear, _classId);
            gridAverage.DataSource = listResult;



            //else if (ddlClassroom.SelectedValue != "-1")
            //{
            //    if (ddlClassroom.SelectedValue == "1"
            //        || ddlClassroom.SelectedValue == "2"
            //        || ddlClassroom.SelectedValue == "3")
            //    {
            //        _id = Convert.ToInt32(ddlClassroom.SelectedValue);
            //        _limit = _id;
            //    }
            //    else
            //    {
            //        _id = Convert.ToInt32(ddlClassroom.SelectedValue);
            //        _limit = _id + 9;
            //    }

            //    listResult = ClassRoom.getListActiveClassroomWithAverageById(_id, _limit, _academicYear);
            //}

            //tblGridHeader.Visible = true;

            //for (int i = 0; i < listResult.Count; i++)
            //{
            //    RadNumericTextBox txtSuccessPercent = gridListClassroom.Rows[i].Cells[2].FindControl("txtAverage") as RadNumericTextBox;
            //    if (listResult[i].success_percent > 0)
            //    {
            //        txtSuccessPercent.Value = listResult[i].success_percent;
            //    }
            //    //
            //    Label lblAcademicYDesc = gridListClassroom.Rows[i].Cells[2].FindControl("lblAcademicYear") as Label;
            //    lblAcademicYDesc.Text = ddlAcademicYear.SelectedText;
            //    //
            //    HiddenField academicYId = gridListClassroom.Rows[i].Cells[2].FindControl("hiddenAcademicYearId") as HiddenField;
            //    academicYId.Value = ddlAcademicYear.SelectedValue;
            //    //
            //    HiddenField classId = gridListClassroom.Rows[i].Cells[2].FindControl("hiddenClassroomId") as HiddenField;
            //    classId.Value = listResult[i].id.ToString();
            //}

        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    protected void gridAverage_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridAverage_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridAverage.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

}