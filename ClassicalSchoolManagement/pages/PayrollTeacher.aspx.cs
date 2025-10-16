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



public partial class PayrollTeacher : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.ECONOMAT;

    List<Salary> listPayrollStaff = new List<Salary>();
    List<Universal> listPersonalPayrollSMS = new List<Universal>();

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


        if (!IsPostBack)
        {
            Users user = Session["user"] as Users;

            // remove old sessions
            Session.Remove("staff_code");
            Session.Remove("first_name");
            Session.Remove("last_name");
            Session.Remove("position");
            //
            loadListAcademicYear();
            BindDataGridTeacher();

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
        }

    }

    private void loadListAcademicYear()
    {
        //clear items
        ddlAcademicYear.Items.Clear();
        // get list academic year
        List<Settings> listResult = Settings.getAcademicYearFull();

        if (listResult != null && listResult.Count > 0)
        {
            // fill the ddl now
            ddlAcademicYear.DataValueField = "id";
            ddlAcademicYear.DataTextField = "years";
            ddlAcademicYear.DataSource = listResult;
            ddlAcademicYear.DataBind();
        }
        //ddlAcademicYear.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        //ddlAcademicYear.SelectedValue = "-1";
    }

    /*********************** SHOW OPTIONS THAT CAN tE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            // loop through the grid to disable edit option
            if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    ImageButton imgBtn = gridListTeacher.Rows[i].Cells[9].FindControl("btnEdit") as ImageButton;
                    imgBtn.Enabled = false;
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
            // loop through the grid to disable delete option
            if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    ImageButton imgBtn = gridListTeacher.Rows[i].Cells[10].FindControl("btnDelete") as ImageButton;
                    imgBtn.Enabled = false;
                }
            }
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
        try
        {
            Teacher t = new Teacher();
            //get the fields from the form            
            t.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
            t.fullName = txtFullname.Text.Trim().Length <= 0 ? null : txtFullname.Text.Trim();
            t.academic_year = int.Parse(ddlAcademicYear.SelectedValue);
            t.status = 1;

            //get list of staff
            List<Teacher> listResult = Teacher.getListTeacherForPayrollWithAmount(t);
            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
            }
            gridListTeacher.DataSource = listResult;
            gridListTeacher.DataBind();

            //freez checked fields from gridview
            if (gridListTeacher.Rows.Count > 0 && gridListTeacher.Visible)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    CheckBox chkSep = gridListTeacher.Rows[i].FindControl("chkSeptember") as CheckBox;
                    CheckBox chkOct = gridListTeacher.Rows[i].FindControl("chkOctober") as CheckBox;
                    CheckBox chkNov = gridListTeacher.Rows[i].FindControl("chkNovember") as CheckBox;
                    CheckBox chkDec = gridListTeacher.Rows[i].FindControl("chkDecember") as CheckBox;
                    CheckBox chkJan = gridListTeacher.Rows[i].FindControl("chkJanuary") as CheckBox;
                    CheckBox chkFeb = gridListTeacher.Rows[i].FindControl("chkFebuary") as CheckBox;
                    CheckBox chkMar = gridListTeacher.Rows[i].FindControl("chkMarch") as CheckBox;
                    CheckBox chkApr = gridListTeacher.Rows[i].FindControl("chkApril") as CheckBox;
                    CheckBox chkMay = gridListTeacher.Rows[i].FindControl("chkMay") as CheckBox;
                    CheckBox chkJun = gridListTeacher.Rows[i].FindControl("chkJune") as CheckBox;
                    CheckBox chkJul = gridListTeacher.Rows[i].FindControl("chkJuly") as CheckBox;
                    CheckBox chkAug = gridListTeacher.Rows[i].FindControl("chkAugust") as CheckBox;
                    //
                    chkSep.Enabled = chkSep.Checked ? false : true;
                    chkOct.Enabled = chkOct.Checked ? false : true;
                    chkNov.Enabled = chkNov.Checked ? false : true;
                    chkDec.Enabled = chkDec.Checked ? false : true;
                    chkJan.Enabled = chkJan.Checked ? false : true;
                    chkFeb.Enabled = chkFeb.Checked ? false : true;
                    chkMar.Enabled = chkMar.Checked ? false : true;
                    chkApr.Enabled = chkApr.Checked ? false : true;
                    chkMay.Enabled = chkMay.Checked ? false : true;
                    chkJun.Enabled = chkJun.Checked ? false : true;
                    chkJul.Enabled = chkJul.Checked ? false : true;
                    chkAug.Enabled = chkAug.Checked ? false : true;
                }
            }
        }
        catch (Exception ex) { throw ex; }
    }

    protected void gridListTeacher_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListTeacher.Rows[index];
        string staffCode = row.Cells[2].Text;
        string fullName = row.Cells[3].Text;
        string academicYear = ddlAcademicYear.SelectedText;
        int academicYearId = int.Parse(ddlAcademicYear.SelectedValue);

        if (e.CommandName == "viewDetailsStaffPayroll")
        {
            Session["staff_code"] = staffCode;
            Session["fullname"] = fullName;
            Session["academic_year"] = academicYear;
            Session["academic_year_id"] = academicYearId;

            string page_url = "PayrollPersonalDetails.aspx";
            try
            {
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow2\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(1200, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

        if (e.CommandName == "clearAnnualPayrollStaff")
        {
            // check end-user confirmation
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Contains("Yes"))
            {
                Salary.deleteAnnualPayrollByStaffCode(staffCode, academicYearId);
                BindDataGridTeacher();
            }
        }
    }

    protected void gridListTeacher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
            e.Row.Style.Add("vertical-align", "bottom");
        }*/
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            string userName = user.username;

            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + userName))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + userName);
            }

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\Payroll_Professeurs_{1}_{2}.xls",
                userName, userName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            gridListTeacher.AllowPaging = false;
            gridListTeacher.HeaderStyle.Font.Bold = true;
            gridListTeacher.GridLines = GridLines.Vertical;
            //gridListPolicy.DataBind();
            BindDataGridTeacher();
            //GetSalaryTable();
            gridListTeacher.HeaderRow.Visible = true;

            if (gridListTeacher.Visible == true)
            {
                // hide all checkAll checkboxes
                List<string> listCheckAllMonth = new List<string> { "chkSeptemberAll", "chkOctoberAll", "chkNovemberAll", "chkDecemberAll",
                             "chkJanuaryAll", "chkFebuaryAll", "chkMarchAll", "chkAprilAll", "chkMayAll", "chkJuneAll", "chkJulyAll", "chkAugustAll" };

                foreach (string month in listCheckAllMonth)
                {
                    // hide header checkboxes
                    CheckBox chkAllSeptember = gridListTeacher.HeaderRow.FindControl(month) as CheckBox;
                    chkAllSeptember.Visible = false;
                }


                gridListTeacher.HeaderRow.Cells[0].Visible = false;
                gridListTeacher.HeaderRow.Cells[1].Visible = false;
                gridListTeacher.HeaderRow.BorderStyle = BorderStyle.None;
                //
                gridListTeacher.HeaderRow.Cells[4].Text = "SEPTEMBRE";
                gridListTeacher.HeaderRow.Cells[5].Text = "OCTOBRE";
                gridListTeacher.HeaderRow.Cells[6].Text = "NOVEMBRE";
                gridListTeacher.HeaderRow.Cells[7].Text = "DECEMBRE";
                gridListTeacher.HeaderRow.Cells[8].Text = "JANVIER";
                gridListTeacher.HeaderRow.Cells[9].Text = "FEVRIER";
                gridListTeacher.HeaderRow.Cells[10].Text = "MARS";
                gridListTeacher.HeaderRow.Cells[11].Text = "AVRIL";
                gridListTeacher.HeaderRow.Cells[12].Text = "MAI";
                gridListTeacher.HeaderRow.Cells[13].Text = "JUIN";
                gridListTeacher.HeaderRow.Cells[14].Text = "JUILLET";
                gridListTeacher.HeaderRow.Cells[15].Text = "AOUT";

                for (int i = 2; i <= 15; i++)
                {
                    // setup header background color to navy
                    gridListTeacher.HeaderRow.Cells[i].BackColor = Color.Navy;

                    // setup header forecolor to white
                    gridListTeacher.HeaderRow.Cells[i].ForeColor = Color.White;
                }
                // set rows heights
                gridListTeacher.RowStyle.Height = 20;

                //
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    GridViewRow row = gridListTeacher.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[1].Visible = false;
                    row.Cells[4].Visible = false;
                    RadNumericTextBox txtSeptember = row.Cells[5].FindControl("txtSeptembre") as RadNumericTextBox;
                    row.Cells[5].Text = txtSeptember.Value == null ? "0" : txtSeptember.Value.ToString();
                    row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[6].Visible = false;
                    RadNumericTextBox txtOctober = row.Cells[7].FindControl("txtOctobre") as RadNumericTextBox;
                    row.Cells[7].Text = txtOctober.Value == null ? "0" : txtOctober.Value.ToString();
                    row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[8].Visible = false;
                    RadNumericTextBox txtNovember = row.Cells[9].FindControl("txtNovembre") as RadNumericTextBox;
                    row.Cells[9].Text = txtNovember.Value == null ? "0" : txtNovember.Value.ToString();
                    row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[10].Visible = false;
                    RadNumericTextBox txtDecember = row.Cells[11].FindControl("txtDecembre") as RadNumericTextBox;
                    row.Cells[11].Text = txtDecember.Value == null ? "0" : txtDecember.Value.ToString();
                    row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[12].Visible = false;
                    RadNumericTextBox txtJanuary = row.Cells[13].FindControl("txtJanvier") as RadNumericTextBox;
                    row.Cells[13].Text = txtJanuary.Value == null ? "0" : txtJanuary.Value.ToString();
                    row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[14].Visible = false;
                    RadNumericTextBox txtFebuary = row.Cells[15].FindControl("txtFevrier") as RadNumericTextBox;
                    row.Cells[15].Text = txtFebuary.Value == null ? "0" : txtFebuary.Value.ToString();
                    row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[16].Visible = false;
                    RadNumericTextBox txtMarch = row.Cells[17].FindControl("txtMars") as RadNumericTextBox;
                    row.Cells[17].Text = txtMarch.Value == null ? "0" : txtMarch.Value.ToString();
                    row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[18].Visible = false;
                    RadNumericTextBox txtApril = row.Cells[19].FindControl("txtAvril") as RadNumericTextBox;
                    row.Cells[19].Text = txtApril.Value == null ? "0" : txtApril.Value.ToString();
                    row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[20].Visible = false;
                    RadNumericTextBox txtMay = row.Cells[21].FindControl("txtMai") as RadNumericTextBox;
                    row.Cells[21].Text = txtMay.Value == null ? "0" : txtMay.Value.ToString();
                    row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[22].Visible = false;
                    RadNumericTextBox txtJune = row.Cells[23].FindControl("txtJuin") as RadNumericTextBox;
                    row.Cells[23].Text = txtJune.Value == null ? "0" : txtJune.Value.ToString();
                    row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[24].Visible = false;
                    RadNumericTextBox txtJuly = row.Cells[25].FindControl("txtJuillet") as RadNumericTextBox;
                    row.Cells[25].Text = txtJuly.Value == null ? "0" : txtJuly.Value.ToString();
                    row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[26].Visible = false;
                    RadNumericTextBox txtAugust = row.Cells[27].FindControl("txtAout") as RadNumericTextBox;
                    row.Cells[27].Text = txtAugust.Value == null ? "0" : txtAugust.Value.ToString();
                    row.Cells[27].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[28].Visible = false;
                    //row.Cells[1].FindControl("btnDelete").Visible = false;

                    for (int j = 2; j <= 28; j++)
                    {
                        // set cells border
                        row.Cells[j].BorderWidth = 1;
                    }
                }

                gridListTeacher.RenderControl(htmlWrite);
            }
            else
            {
                return;
            }

            System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();
            //Response.Redirect("nextpage.aspx", false);
            Universal.WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
            // set token
            //   string token = Token.generate(Token.TypeToken.Download, user.Username, Response);
            //  tokenField.Value = token;
            //
            gridListTeacher.HeaderRow.Visible = false;
        }
        catch (System.Threading.ThreadAbortException lException)
        {
            // do nothing
        }
        catch (Exception ex)
        {
            MessBox.Show("Error when export!");
        }
    }

    protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
    {
        // refresh the gridview information
        BindDataGridTeacher();
    }

    protected void checkAllByMonth(object sender, EventArgs e)
    {
        int _checkedColumn = 0;
        string _checkedElement = "";
        CheckBox chk = sender as CheckBox;
        switch (chk.ID)
        {
            case "chkSeptemberAll": _checkedColumn = 4; _checkedElement = "chkSeptember"; break;
            case "chkOctoberAll": _checkedColumn = 6; _checkedElement = "chkOctober"; break;
            case "chkNovemberAll": _checkedColumn = 8; _checkedElement = "chkNovember"; break;
            case "chkDecemberAll": _checkedColumn = 10; _checkedElement = "chkDecember"; break;
            case "chkJanuaryAll": _checkedColumn = 12; _checkedElement = "chkJanuary"; break;
            case "chkFebuaryAll": _checkedColumn = 14; _checkedElement = "chkFebuary"; break;
            case "chkMarchAll": _checkedColumn = 16; _checkedElement = "chkMarch"; break;
            case "chkAprilAll": _checkedColumn = 18; _checkedElement = "chkApril"; break;
            case "chkMayAll": _checkedColumn = 20; _checkedElement = "chkMay"; break;
            case "chkJuneAll": _checkedColumn = 22; _checkedElement = "chkJune"; break;
            case "chkJulyAll": _checkedColumn = 24; _checkedElement = "chkJuly"; break;
            case "chkAugustAll": _checkedColumn = 26; _checkedElement = "chkAugust"; break;
        }

        if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
        {
            for (int i = 0; i < gridListTeacher.Rows.Count; i++)
            {
                CheckBox chk1 = gridListTeacher.Rows[i].Cells[_checkedColumn].FindControl(_checkedElement) as CheckBox;
                chk1.Checked = chk.Checked;
            }
        }
        //
        chk.Focus();
    }

    protected void btnCalculatePayroll_Click(object sender, EventArgs e)
    {
        if (gridListTeacher.Rows.Count <= 0)
        {
            MessageAlert.RadAlert("Desole, Pas de donneees !", 350, 150, "", null, null);
        }
        else
        {
            List<string> listStaffCode = new List<string>();
            //get list staff_code from gridview
            if (gridListTeacher.Rows.Count > 0 && gridListTeacher.Visible)
            {
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    listStaffCode.Add(gridListTeacher.Rows[i].Cells[3].Text.ToString());
                }
            }

            // september
            addDataToPayrollList(5, "chkSeptember", "septembre");
            // october
            addDataToPayrollList(7, "chkOctober", "octobre");
            // november
            addDataToPayrollList(9, "chkNovember", "novembre");
            // december
            addDataToPayrollList(11, "chkDecember", "decembre");
            // january
            addDataToPayrollList(13, "chkJanuary", "janvier");
            // febuary
            addDataToPayrollList(15, "chkFebuary", "fevrier");
            // march
            addDataToPayrollList(17, "chkMarch", "mars");
            // april
            addDataToPayrollList(19, "chkApril", "avril");
            // may
            addDataToPayrollList(21, "chkMay", "mai");
            // june
            addDataToPayrollList(23, "chkJune", "juin");
            // july
            addDataToPayrollList(25, "chkJuly", "juillet");
            // august
            addDataToPayrollList(29, "chkAugust", "aout");

            // add payroll to db
            Salary.InsertNewPayroll(listPayrollStaff);
            List<string> listSMSContent = null;
            List<Universal> listPayroll = new List<Universal>();
            string phoneNumber = null;
            /************** SEND SMS PAYROLL STAFF *************/
            if (listStaffCode != null && listStaffCode.Count > 0)
            {
                foreach (string staffCode in listStaffCode)
                {
                    listSMSContent = new List<string>();
                   Teacher t = Teacher.getTeacherInfoById(staffCode);
                    string fullName = t.fullName;
                    listSMSContent.Add(" SAPHIR SCHOOL\nM / MS " + fullName.ToUpper() + " votre payroll pour le(s) mois:");
                    if (listPersonalPayrollSMS != null && listPersonalPayrollSMS.Count > 0)
                    {
                        foreach (Universal uni in listPersonalPayrollSMS)
                        {
                            // check if staff in new payment list
                            if (uni.staff_code.ToUpper() == staffCode.ToUpper())
                            {
                                listSMSContent.Add(uni.payroll_month_amount);
                                phoneNumber = uni.phone_number;
                            }
                        }
                        //
                        string[] arrayMessagecontent = listSMSContent.ToArray();
                        // get each sms content
                        Universal universal = new Universal();
                        universal.staff_code = staffCode.ToUpper();
                        universal.message_text = string.Join("\n", arrayMessagecontent);
                        universal.message_to = "+509" + phoneNumber;
                        universal.message_from = Universal.GetSMSNumberSender();
                        universal.message_type = "PAYROLL EMPLOYES";
                        // add to payroll list
                        listPayroll.Add(universal);
                    }
                }
            }
            if (listPayroll != null && chkSendSMS.Checked)
            {
                // send sms payroll
                Universal.SendPayrollSMS(listPayroll);
            }
            //reload gridview content
            BindDataGridTeacher();
            // confirmation message
            MessageAlert.RadAlert("Succes !", 250, 180, "Confirmation", null, "../images/success_check.png");
        }
    }

    private void addDataToPayrollList(int cellNumber, string controlId, string salaryMonth)
    {
        Salary sal = null;
        if (gridListTeacher.Visible && gridListTeacher.Rows.Count > 0)
        {
            for (int i = 0; i < gridListTeacher.Rows.Count; i++)
            {
                //
                CheckBox chk = gridListTeacher.Rows[i].Cells[cellNumber].FindControl(controlId) as CheckBox;
                //
                if (chk.Checked)
                {
                    // check if payroll was made
                    double totalTax = 0;
                    double onaTax = 0;
                    double iriTax = 0;
                    double fduTax = 0;
                    double casTax = 0;
                    double fixedTax = 0;
                    double fixedSalary = 0;
                    double variedSalary = 0;
                    double totalSalary = 0;
                    string phoneNumber = null;
                    string staffCode = gridListTeacher.Rows[i].Cells[3].Text;
                    int academicyear = ddlAcademicYear.SelectedValue == null ? 0 : int.Parse(ddlAcademicYear.SelectedValue);
                    string newMonth = string.Empty;
                    string newYear = string.Empty;
                    //
                    switch (salaryMonth)
                    {
                        case "janvier": newMonth = "01"; break;
                        case "fevrier": newMonth = "02"; break;
                        case "mars": newMonth = "03"; break;
                        case "avril": newMonth = "04"; break;
                        case "mai": newMonth = "05"; break;
                        case "juin": newMonth = "06"; break;
                        case "juillet": newMonth = "07"; break;
                        case "aout": newMonth = "08"; break;
                        case "septembre": newMonth = "09"; break;
                        case "octobre": newMonth = "10"; break;
                        case "novembre": newMonth = "11"; break;
                        case "decembre": newMonth = "12"; break;
                    }

                    List<Universal> listAcademicYear = Universal.getStartDate(academicyear);
                    if (listAcademicYear != null && listAcademicYear.Count > 0)
                    {
                        newYear = listAcademicYear[0].start_date;
                    }
                    //
                    string monthYearPayroll = newYear + newMonth;

                    // get primary salary
                    List<Salary> listFixedSalary = Salary.getFixedSalaryTeacher(staffCode, academicyear);
                    if (listFixedSalary != null && listFixedSalary.Count > 0)
                    {
                        fixedSalary = listFixedSalary[0].amount;
                        phoneNumber = listFixedSalary[0].phone_number;
                    }

                    // get secondary salary
                    List<Salary> listSecondarySalary = Salary.getSecondarySalaryTeacher(staffCode, academicyear, monthYearPayroll);
                    if (listSecondarySalary != null && listSecondarySalary.Count > 0)
                    {
                        variedSalary = listSecondarySalary[0].amount;
                        phoneNumber = listSecondarySalary[0].phone_number;
                    }

                    totalSalary = fixedSalary + variedSalary;

                    // get tax info by staff
                    List<Salary> listTaxInfo = Salary.getListTaxByStaffCode(staffCode);
                    if (listTaxInfo != null && listTaxInfo.Count > 0)
                    {
                        totalTax += listTaxInfo[0].fixed_tax;
                        fixedTax = listTaxInfo[0].fixed_tax;
                        if (listTaxInfo[0].ona == 1)
                        {
                            totalTax += totalSalary * Salary.tax.ona;
                            onaTax = totalSalary * Salary.tax.ona;
                        }
                        if (listTaxInfo[0].iri == 1)
                        {
                            totalTax += totalSalary * Salary.tax.iri;
                            iriTax = totalSalary * Salary.tax.iri;
                        }
                        if (listTaxInfo[0].fdu == 1)
                        {
                            totalTax += totalSalary * Salary.tax.fdu;
                            fduTax = totalSalary * Salary.tax.fdu;
                        }
                        if (listTaxInfo[0].cas == 1)
                        {
                            totalTax += totalSalary * Salary.tax.cas;
                            casTax = totalSalary * Salary.tax.cas;
                        }

                    }

                    Users user = Session["user"] as Users;
                    // add new payroll
                    sal = new Salary();
                    sal.staff_code = staffCode;
                    sal.salary_month = salaryMonth;
                    sal.contract_salary = totalSalary;
                    sal.ona_tax_amount = onaTax;
                    sal.iri_tax_amount = iriTax;
                    sal.fdu_tax_amount = fduTax;
                    sal.cas_tax_amount = casTax;
                    sal.fixed_tax = fixedTax;
                    sal.academic_year = int.Parse(ddlAcademicYear.SelectedValue);
                    sal.login_user = user.staff_code;
                    sal.phone_number = phoneNumber;

                    // check if payroll is already calculated for this month
                    //if (!Salary.staffPayrollExist(sal))
                    if(false)
                    {
                        // add to list
                        listPayrollStaff.Add(sal);
                        double _netSalary = sal.contract_salary - (sal.ona_tax_amount + sal.iri_tax_amount + sal.fdu_tax_amount + sal.fixed_tax);
                        Universal uni = new Universal();
                        String monthAbrevation = null;
                        switch (sal.salary_month.ToUpper())
                        {
                            case "SEPTEMBRE": monthAbrevation = "SEP"; break;
                            case "OCTOBRE": monthAbrevation = "OCT"; break;
                            case "NOVEMBRE": monthAbrevation = "NOV"; break;
                            case "DECEMBRE": monthAbrevation = "DEC"; break;
                            case "JANVIER": monthAbrevation = "JAN"; break;
                            case "FEVRIER": monthAbrevation = "FEB"; break;
                            case "MARS": monthAbrevation = "MAR"; break;
                            case "AVRIL": monthAbrevation = "APR"; break;
                            case "MAI": monthAbrevation = "MAY"; break;
                            case "JUIN": monthAbrevation = "JUN"; break;
                            case "JUILLET": monthAbrevation = "JUL"; break;
                            case "AOUT": monthAbrevation = "AUG"; break;
                        }

                        uni.payroll_month_amount = monthAbrevation + " : " + _netSalary + " Gdes";
                        uni.staff_code = sal.staff_code;
                        uni.phone_number = sal.phone_number;
                        listPersonalPayrollSMS.Add(uni);

                    }
                }
                else
                {
                    // delete existed payroll
                }
            }
        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        BindDataGridTeacher();
    }

    protected void btnSalaryConfig_Click(object sender, EventArgs e)
    {
        string page_url = "DialogSalaryTeacherConfiguration.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1000, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

    protected void btnTaxConfig_Click(object sender, EventArgs e)
    {
        string page_url = "DialogTaxConfigurationManagement.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1200, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

    protected void btnSalaryConfigClass_Click(object sender, EventArgs e)
    {
        string page_url = "DialogSalaryConfigClass.aspx";
        try
        {
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + "oWinn.SetSize(1200, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

    //protected void checkPersonalMonth(object sender, EventArgs e)
    //{
    //    CheckBox chk = sender as CheckBox;
    //    switch (chk.ID)
    //    {
    //        case "chkSeptember":
    //            MessBox.Show("September");

    //            break;
    //        case "chkOctober": MessBox.Show("october"); break;
    //        case "chkNovember": MessBox.Show("November"); break;
    //        case "chkDecember": MessBox.Show("December"); break;
    //        case "chkJanuary": MessBox.Show("January"); break;
    //        case "chkFebuary": MessBox.Show("Febuary"); break;
    //        case "chkMarch": MessBox.Show("March"); break;
    //        case "chkApril": MessBox.Show("April"); break;
    //        case "chkMay": MessBox.Show("May"); break;
    //        case "chkJune": MessBox.Show("June"); break;
    //        case "chkJuly": MessBox.Show("July"); break;
    //        case "chkAugust": MessBox.Show("August"); break;
    //    }
    //}

}
