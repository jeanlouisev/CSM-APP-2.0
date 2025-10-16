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



public partial class LogsManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SETTINGS;

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
            // VERIFY USER ACCESS LEVEL
            List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
            if (listResult != null && listResult.Count > 0)
            {
                Users userAccess = listResult[0];
                int notGranted = (int)Users.ACCESS.NO;

                gridLogs.Rebind();

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

        if (!IsPostBack)
        {
            //radDateHeureDebut.SelectedDate = DateTime.Now;
            //radDateHeureFin.SelectedDate = DateTime.Now;
        }
    }

    // edit_access
    private void disableEditOption()
    {
        try
        {
            // BUTTONS
            btnSearch.Attributes.Add("disabled", "disabled");
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
       // no action is required
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridLogs.Rebind();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            user.staff_code = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
            user.from_date = radFromDate.SelectedDate.Value;
            user.to_date = radToDate.SelectedDate.Value;
            //
            List<Users> logsList = Users.getListUserLogs(user);
            //
            gridLogs.DataSource = logsList;
            gridLogs.Rebind();

            if (logsList == null || logsList.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("logsInformation_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                gridLogs.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gridLogs.ExportSettings.IgnorePaging = true;
                gridLogs.ExportSettings.FileName = Path;
                gridLogs.ExportSettings.ExportOnlyData = true;
                gridLogs.ExportSettings.OpenInNewWindow = true;
                gridLogs.MasterTableView.ExportToExcel();

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

    protected void gridLogs_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        Users user = new Users();
        user.staff_code = txtStaffCode.Text.Trim().Length <= 0 ? null : txtStaffCode.Text.Trim();
        user.from_date = radFromDate.SelectedDate == null ? DateTime.MinValue : radFromDate.SelectedDate.Value;
        user.to_date = radToDate.SelectedDate == null ? DateTime.MinValue : radToDate.SelectedDate.Value;
        //
        List<Users> logsList = Users.getListUserLogs(user);
        //
        gridLogs.DataSource = logsList;
    }

    protected void gridLogs_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridLogs_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridLogs.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}
