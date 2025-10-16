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


public partial class SMSBroadcasts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        if (!Page.IsPostBack)
        {
            Session.Remove("phone_prefix"); // clear session
            udpDdlCourse.Update();
            loadActiveClassroom();
            loadSmsContent();
            BindDataGridContacts();
        }
    }

    protected void btnSearchContact_Click(object sender, EventArgs e)
    {
        BindDataGridContacts();
    }

    protected void btnSendMessag_Click(object sender, EventArgs e)
    {
        if (txtMessage.Text.Trim().Length <= 0)
        {
            lblContentSMS.Visible = true;
            txtMessage.Focus();

        }
        else
        {

            int countCheck = 0;
            List<Universal> listUniversal = new List<Universal>();
            Universal universal = null;

            foreach (GridViewRow row in gridListContact.Rows)//Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int index = row.RowIndex;
                    CheckBox chkRow = row.Cells[7].FindControl("chkBroascast") as CheckBox;
                    //DropDownList ddl = row.Cells[2].FindControl("ddlprice") as DropDownList;
                    // if (ddl.SelectedValue != null)
                    //  {
                    if (chkRow.Checked)
                    {
                        countCheck = countCheck + 1;

                        universal = new Universal();
                        // get all grid data one by one
                        universal.staff_code = row.Cells[1].Text;
                        //universal.fullName = row.Cells[2].Text;
                        universal.phone = row.Cells[3].Text;
                        //universal.level = row.Cells[4].Text;
                        // universal.classroom = row.Cells[5].Text;
                        //universal.vacation = row.Cells[6].Text;
                        universal.content = txtMessage.Text.Trim();
                        // add data to list



                        listUniversal.Add(universal);


                    }


                }
            }

            if (countCheck > 0)
            {

                Universal.sendSmsToContact(listUniversal);
                MessBox.Show("SMS envoyé (s) avec succès !");
                lblContentSMS.Visible = false;
                lblGridNotCheck.Visible = false;
                btnDeselectAllContact_Click(this, e);

                txtMessage.Text = "";
            }
            else
            {
                lblContentSMS.Visible = false;
                lblGridNotCheck.Visible = true;


            }
        }
    }

    private void BindDataGridContacts()
    {
        Universal uni = new Universal();
        uni.staff_code = txtCode.Text.Trim().Length <= 0 ? "%" : "%" + txtCode.Text.Trim().ToLower() + "%";
        uni.phone = txtTelephone.Text.Trim().Length <= 0 ? null : txtTelephone.Text.Trim();
        uni.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
        uni.level = ddlPosition.SelectedValue.ToString() == "-1" ? null : ddlPosition.SelectedValue.ToLower();
        uni.network = ddlNetwork.SelectedValue.ToString() == "-1" ? "%" : ddlNetwork.SelectedValue;
        uni.classroom = ddlClassroom.SelectedValue == "-1" ? null : ddlClassroom.SelectedText;
        uni.vacation = ddlVacation.SelectedValue.ToString() == "-1" ? null : ddlVacation.SelectedText.ToUpper();
        //uni.cours = ddlCourse.SelectedValue.ToString() == "-1" ? "%" : ddlCourse.SelectedValue;

        List<string> listPrefix = null;
        if (Session["phone_prefix"] != null)
        {
            listPrefix = Session["phone_prefix"] as List<string>;
        }

        try
        {
            //get list of Contacts
            List<Universal> listResult = Universal.getListContacts(uni, listPrefix);
            if (listResult.Count > 0 && listResult != null)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
                // lnkExportExcel.Visible = true;
                //tblGridHeader.Visible = true;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                //lnkExportExcel.Visible = false;
                //tblGridHeader.Visible = false;*/.
            }
            gridListContact.DataSource = listResult;
            gridListContact.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    /* protected void chkBroadcast_CheckedChanged(object sender, EventArgs e)
    {
        try
         {
             CheckBox chk = sender as CheckBox;
             GridViewRow row = (chk).Parent.Parent as GridViewRow;
             int index = row.RowIndex;
             RadComboBox combo = gridListReference.Rows[index].FindControl("cboReasonType") as RadComboBox;
             if (chk.Checked)
             {
                 combo.SelectedValue = "1";
             }
         }
         catch (Exception ex)
         {
             MessBox.Show("Erreur : " + ex.Message);
         }
}*/

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
    }

    protected void gridListContact_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gridListContact_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }
    }

    //protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    //{
    //    try
    //    {
    //        /*(vacation
    //        ddlVacation.Items.Clear();
    //        ddlVacation.Items.Insert(0, new DropDownListItem("", "-1"));
    //        ddlVacation.SelectedValue = "-1";

    //        // course
    //        ddlCourse.Items.Clear();
    //        ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
    //        ddlCourse.SelectedValue = "-1";*/


    //        //
    //        if (ddlClassroom.SelectedValue != "-1")
    //        {
    //            int classId = int.Parse(ddlClassroom.SelectedValue);
    //            List<ClassRoom> listVacation = ClassRoom.getListVacationByClassroomId(classId);
    //            //
    //            loadListVacation(listVacation);
    //            int academicYear = 11;// ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
    //            List<Course> listCourse = Course.getListAffectedCourseByClassroomCode(classId, ddlVacation.SelectedValue, academicYear);
    //            loadListCourse(listCourse);

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Erreur : " + ex.Message);
    //    }
    //}


    protected void ddlposition_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlPosition.SelectedValue == "PARENT")
        {
            //Disable cours
            ddlCourse.SelectedValue = "-1";
            ddlCourse.Enabled = false;
            //Disbale classroom
            ddlClassroom.SelectedValue = "-1";
            ddlClassroom.Enabled = false;
            //Disbale vacation
            ddlVacation.SelectedValue = "-1";
            ddlVacation.Enabled = false;

        }
        else if (ddlPosition.SelectedValue == "ELEVE")
        {
            ddlCourse.SelectedValue = "-1";
            ddlCourse.Enabled = false;
            ddlClassroom.Enabled = true;
        }
        else
        {
            ddlCourse.Enabled = true;
            ddlClassroom.Enabled = true;
            ddlVacation.Enabled = true;

        }

    }

    private void loadListCourse(List<Course> listCourse)
    {
        try
        {
            //clear items
            ddlCourse.Items.Clear();
            if (listCourse != null && listCourse.Count > 0)
            {
                // fill the ddl now
                ddlCourse.DataValueField = "id_course";
                ddlCourse.DataTextField = "course_fullname";
                ddlCourse.DataSource = listCourse;
                ddlCourse.DataBind();

            }
            ddlCourse.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlCourse.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

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
            ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlVacation.SelectedValue = "-1";

        }
    }

    private void loadActiveClassroom()
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ClassRoom classroom = new ClassRoom();
        ddlClassroom.DataValueField = "id";
        ddlClassroom.DataTextField = "name";
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlClassroom.SelectedValue = "-1";
    }

    private void loadSmsContent()
    {
        List<Universal> listSmsContent = Universal.getListSmsContent();
        // Universal SmsContent = new Universal();
        //dropDownList.DataValueField = "message_content";
        //dropDownList.DataTextField = "content";
        //dropDownList.DataSource = listcontent;
        //dropDownList.DataBind();
        //dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        //dropDownList.Items.Insert(1, new DropDownListItem("Nouveau", "-2"));
        //dropDownList.SelectedValue = "-1";
        //dropDownList.Items.ElementAt(1).Attributes.Add("font-weight", "bold");

        int k = 2;
        if (listSmsContent != null && listSmsContent.Count > 0)
        {
            foreach (Universal uni in listSmsContent)
            {
                ddlcontentSms.Items.Insert(k, new RadComboBoxItem(uni.content, uni.message_content));
            }
        }
    }

    //protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    //{
    //    try
    //    {
    //        // course
    //        ddlCourse.Items.Clear();
    //        ddlCourse.Items.Insert(0, new DropDownListItem("", "-1"));
    //        ddlCourse.SelectedValue = "-1";

    //        //
    //        if (ddlVacation.SelectedValue != "-1")
    //        {
    //            int classId = ddlClassroom.SelectedValue == null ? 0 : int.Parse(ddlClassroom.SelectedValue);
    //            int academicYear = 11;// ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
    //            List<Course> listCourse = Course.getListAffectedCourseByClassroomCode(classId, ddlVacation.SelectedValue, academicYear);
    //            loadListCourse(listCourse);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Erreur : " + ex.Message);
    //    }
    //}

    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        if (chk.Checked)
        {
            btnSelectAllContact_Click(this, e);
        }
        else
        {
            btnDeselectAllContact_Click(this, e);
        }
    }

    protected void btnSelectAllContact_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row2 in gridListContact.Rows)//Running all lines of grid
        {
            if (row2.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBroascast = (row2.Cells[7].FindControl("chkBroascast") as CheckBox);
                chkBroascast.Checked = true;
            }
        }
    }

    protected void btnDeselectAllContact_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row2 in gridListContact.Rows)//Running all lines of grid
        {
            if (row2.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBroascast = (row2.Cells[7].FindControl("chkBroascast") as CheckBox);
                chkBroascast.Checked = false;
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
    }

    protected void ddlNetwork_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        List<string> listPrefixPhone = null;
        int phoneOperator = int.Parse(ddlNetwork.SelectedValue);
        switch (phoneOperator)
        {
            case 1: listPrefixPhone = null; break; // digicel & natcom

            case 2:
                listPrefixPhone = new List<string>();
                listPrefixPhone.Add("22");
                listPrefixPhone.Add("32");
                listPrefixPhone.Add("33");
                listPrefixPhone.Add("40");
                listPrefixPhone.Add("41");
                listPrefixPhone.Add("42");
                listPrefixPhone.Add("43");
                break; // natcom only

            case 3:
                listPrefixPhone = new List<string>();
                listPrefixPhone.Add("38");
                listPrefixPhone.Add("36");
                listPrefixPhone.Add("37");
                listPrefixPhone.Add("39");
                listPrefixPhone.Add("31");
                listPrefixPhone.Add("34");
                listPrefixPhone.Add("44");
                break; // digicel only
        }

        Session["phone_prefix"] = listPrefixPhone;
    }

    protected void ddlcontentSms_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // clear previous values from txtmess
        txtMessage.Text = string.Empty;
        if (ddlcontentSms.SelectedValue != "-1" && ddlcontentSms.SelectedValue != "-2")
        {
            txtMessage.Text = ddlcontentSms.SelectedValue;
        }
        else if (ddlcontentSms.SelectedValue == "-2")
        {
            string page_url = "Add_New_SMS.aspx";
            try
            {
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                              + "oWinn.SetSize(1024, 600);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }

            // ddlType.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlcontentSms.SelectedValue = "-1";
        }
    }
}