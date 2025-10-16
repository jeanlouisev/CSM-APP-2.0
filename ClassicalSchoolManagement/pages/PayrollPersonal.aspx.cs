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


public partial class PayrollPersonal : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.ECONOMAT;
    string msgContent = null;

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
            BindDataGridPayrollStaff();
            BindDataGridPayrollHis();



            //// check login_user policy to grant or revoke access
            //List<Users> listGroupPolicy = Users.getListUserAccessLevel(user.role_id, menu_code);
            //if (listGroupPolicy != null && listGroupPolicy.Count > 0)
            //{
            //    if (listGroupPolicy[0].view_access == 0
            //        && listGroupPolicy[0].edit_access == 0
            //        && listGroupPolicy[0].delete_access == 0)
            //    {
            //        //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
            //        Response.Redirect("~/Default.aspx");
            //    }
            //    else
            //    {
            //        // edit
            //        if (listGroupPolicy[0].edit_access == 0)
            //        {
            //            disableEditOption();
            //        }
            //        // delete
            //        if (listGroupPolicy[0].delete_access == 0)
            //        {
            //            disableDeleteOption();
            //        }
            //    }
            //}
        } // ---------------------> end postback

    }

    private void loadListAcademicYear()
    {
        //clear items
        ddlAcademicYear.Items.Clear();
        // get list academic year
        List<Settings> listResult = Settings.getAcademicYearFull();

        if (listResult != null && listResult.Count > 0)
        {
            // combo calculate payroll
            ddlAcademicYear.DataValueField = "id";
            ddlAcademicYear.DataTextField = "years";
            ddlAcademicYear.DataSource = listResult;
            ddlAcademicYear.DataBind();


            // combo historic payroll
            ddlAcademicYearHistoric.DataValueField = "id";
            ddlAcademicYearHistoric.DataTextField = "years";
            ddlAcademicYearHistoric.DataSource = listResult;
            ddlAcademicYearHistoric.DataBind();
        }
        //ddlAcademicYear.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        //ddlAcademicYear.SelectedValue = "-1";
    }

    /*********************** SHOW OPTIONS THAT CAN tE SEEN ACCORDING TO USER POLICY *********/

    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    // loop through the grid to disable edit option
        //    if (gridListStaff.Visible && gridListStaff.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListStaff.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListStaff.Rows[i].Cells[9].FindControl("btnEdit") as ImageButton;
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
        //try
        //{
        //    // loop through the grid to disable delete option
        //    if (gridListStaff.Visible && gridListStaff.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListStaff.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListStaff.Rows[i].Cells[10].FindControl("btnDelete") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur : " + ex.Message);
        //}
    }
    /******************************************* END USER POLICY **************************/




    /*********************    PAYROLL CALCULATION    *******************/

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataGridPayrollStaff();
    }

    private void BindDataGridPayrollStaff()
    {
        try
        {
            Salary s = new Salary();
            s.academic_year_id = int.Parse(ddlAcademicYear.SelectedValue);
            s.salary_month = ddlMonths.SelectedValue;
            //
            List<Salary> listResult = Salary.getListStaffPayroll(s);
            radGridPayrollPersonal.DataSource = listResult;
            radGridPayrollPersonal.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void gridListStaff_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        /*
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListStaff.Rows[index];
        string staffCode = row.Cells[3].Text;
        string fullName = row.Cells[4].Text;
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

        if (e.CommandName == "clearAnnualPayrollStaff")
        {
            // check end-user confirmation
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Contains("Yes"))
            {
                Salary.deleteAnnualPayrollByStaffCode(staffCode, academicYearId);
                BindDataGridPayrollStaff();
            }
        }

        */
    }

    protected void gridListStaff_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnExportExcelStaffPayroll_ServerClick(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            Salary s = new Salary();
            s.academic_year_id = int.Parse(ddlAcademicYear.SelectedValue);
            s.salary_month = ddlMonths.SelectedValue;
            //
            List<Salary> listResult = Salary.getListStaffPayroll(s);
            radGridPayrollPersonal.DataSource = listResult;
            radGridPayrollPersonal.DataBind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("staff_payroll_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                radGridPayrollPersonal.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                radGridPayrollPersonal.ExportSettings.IgnorePaging = true;
                radGridPayrollPersonal.ExportSettings.FileName = Path;
                radGridPayrollPersonal.ExportSettings.ExportOnlyData = true;
                radGridPayrollPersonal.ExportSettings.OpenInNewWindow = true;
                radGridPayrollPersonal.MasterTableView.ExportToExcel();

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

    protected void btnCalculatePayroll_Click(object sender, EventArgs e)
    {
        Users user = Session["user"] as Users;
        int _accId = int.Parse(ddlAcademicYear.SelectedValue);
        string _month = ddlMonths.SelectedValue;
        // get basic salary info
        List<Salary> listSalaryInfoTemp = Salary.getStaffSalaryInfoForPayroll();
        List<Salary> listSalaryPayroll = new List<Salary>();

        // verify if payroll status is already made
        Salary pStatus = new Salary();
        pStatus.academic_year_id = _accId;
        pStatus.salary_month = _month;
        pStatus.status = Salary.payroll_status.active;
        pStatus.login_user_id = user.id;
        //
        bool pCheck = Salary.StaffPayrollAlreadyExist(pStatus);

        if (pCheck)
        {
            msgContent = "Désolé, ce payroll a été déja calculé !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            if (listSalaryInfoTemp != null && listSalaryInfoTemp.Count > 0)
            {
                foreach (Salary s in listSalaryInfoTemp)
                {
                    s.ona_tax_amount = s.ona == 1 ? s.contract_salary * Salary.tax.ona : 0;
                    s.iri_tax_amount = s.iri == 1 ? s.contract_salary * Salary.tax.iri : 0;
                    s.fdu_tax_amount = s.fdu == 1 ? s.contract_salary * Salary.tax.fdu : 0;
                    s.cas_tax_amount = s.cas == 1 ? s.contract_salary * Salary.tax.cas : 0;
                    s.academic_year_id = _accId;
                    s.salary_month = _month;
                    s.login_user_id = user.id;
                    //
                    listSalaryPayroll.Add(s);
                }

                // add salary
                Salary.InsertNewPayroll(listSalaryPayroll);

                // add payroll status [this will prevent the user from calculating the same payroll again]
                Salary.InsertStaffPayrollStatus(pStatus);

                // reload the gridview
                BindDataGridPayrollStaff();

                msgContent = "Succès !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void radGridPayrollPersonal_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridPayrollPersonal_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridPayrollPersonal.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }




    /**************************    PAYROLL HISTORIC *******************/

    protected void btnSearchHistoric_ServerClick(object sender, EventArgs e)
    {
        if (txtCodeHis.Text.Trim().Length <= 0)
        {
            msgContent = "Désolé, veuillez tapez le code du personnel !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        BindDataGridPayrollHis();
    }

    private void BindDataGridPayrollHis()
    {
        try
        {
            List<Salary> listResult = new List<Salary>();
            if (txtCodeHis.Text.Trim().Length > 0)
            {
                String staffCode = txtCodeHis.Text.Trim();
                int accId = int.Parse(ddlAcademicYearHistoric.SelectedValue);
                if (Staff.isValidStaff(staffCode))
                {
                    listResult = Salary.getListPayrollHistoricForStaff(staffCode, accId);
                }
                else
                {
                    msgContent = "Désolé, code invalide !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
                }
            }
            radGridPayrollHis.DataSource = listResult;
            radGridPayrollHis.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnExportExcelHis_ServerClick(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            List<Salary> listResult = new List<Salary>();
            if (txtCodeHis.Text.Trim().Length > 0)
            {
                String staffCode = txtCodeHis.Text.Trim();
                int accId = int.Parse(ddlAcademicYearHistoric.SelectedValue);
                //
                if (Staff.isValidStaff(staffCode))
                {
                    listResult = Salary.getListPayrollHistoricForStaff(staffCode, accId);
                }
            }
            radGridPayrollHis.DataSource = listResult;
            radGridPayrollHis.DataBind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("staff_payroll_historics_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                radGridPayrollHis.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                radGridPayrollHis.ExportSettings.IgnorePaging = true;
                radGridPayrollHis.ExportSettings.FileName = Path;
                radGridPayrollHis.ExportSettings.ExportOnlyData = true;
                radGridPayrollHis.ExportSettings.OpenInNewWindow = true;
                radGridPayrollHis.MasterTableView.ExportToExcel();

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

    protected void radGridPayrollHis_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridPayrollHis_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridPayrollHis.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}
