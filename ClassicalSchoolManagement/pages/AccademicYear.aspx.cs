using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using Telerik.Web.UI;
using Telerik.Web;
using Utilities;


public partial class AccademicYear : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SETTINGS;

    protected void Page_Load(object sender, EventArgs e)
    {
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

                // VERIFY USER ACCESS LEVEL
                List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
                if (listResult != null && listResult.Count > 0)
                {
                    Users userAccess = listResult[0];
                    int notGranted = (int)Users.ACCESS.NO;

                    gridAcademicYear.Rebind();

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
        }
    }

    // edit_access
    private void disableEditOption()
    {
        try
        {
            // BUTTONS
            btnAdd.Attributes.Add("disabled", "disabled");
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridAcademicYear.Visible && gridAcademicYear.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridAcademicYear.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnDelete = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
                    //
                    btnDelete.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (validateFields())
            {
                int OldMaxAcademicYear = Settings.getAcademicYear();

                Users user = Session["user"] as Users;
                Settings p = new Settings();
                //get the values from the form            
                p.start_date = radFromDate.SelectedDate.Value;
                p.end_date = radTodate.SelectedDate.Value;
                int _startYear = radFromDate.SelectedDate.Value.Year;
                int _endYear = radTodate.SelectedDate.Value.Year;
                p.login_user = user.username.ToUpper();

                // check if academic year already exsit in system
                if (!Settings.academicYearAlreadyExist(_startYear, _endYear))
                {
                    Settings.addAcademicYear(p);

                    // check for previous configuration
                    List<Settings> listAcademicYear = Settings.getAcademicYearFull();
                    if (listAcademicYear != null && listAcademicYear.Count > 0)
                    {
                        // get last academic Year id 
                        int NewMaxAcademicYear = Settings.getAcademicYear();
                        // get information from previous configuration and insert to new academic
                        //Settings.UpgradeToAllPreviousConfiguration(OldMaxAcademicYear, NewMaxAcademicYear, user.staff_code);
                    }

                    gridAcademicYear.Rebind();
                    MessageAlert.RadAlert("Succès !", 300, 200, "Information", null, "../images/success_check.png");
                }
                else
                {
                    MessageAlert.RadAlert("Succès !", 300, 200, "Information", null, "../images/error.png");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public bool validateFields()
    {
        bool result = true;
        if (radFromDate.IsEmpty)
        {
            MessageAlert.RadAlert("Erreur : Début Année Académique est obligatoire !", 300, 200, "Information", null, "../images/error.png");
            radFromDate.Focus();
            result = false;
        }
        else if (radTodate.IsEmpty)
        {
            MessageAlert.RadAlert("Erreur : Fin Année Académique est obligatoire !", 300, 200, "Information", null, "../images/error.png");
            radTodate.Focus();
            result = false;
        }
        return result;
    }

    public void removeAcademicYear(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            int academicYearId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            //
            Settings.deleteAcademicYear(academicYearId);
            //refresh data of the gridview
            gridAcademicYear.Rebind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void radFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //try
        //{
        //    lblError.Visible = false;
        //    if (radFromDate.SelectedDate != null)
        //    {
        //        if (radFromDate.SelectedDate.Value.Year != DateTime.Now.Year)
        //        {
        //            radFromDate.SelectedDate = null;
        //            lblError.Text = "Erreur : Début Année Académique doit etre en " + DateTime.Now.Year;
        //            lblError.Visible = true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblError.Text = ex.Message;
        //    lblError.Visible = true;
        //}
    }

    protected void radTodate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //try
        //{
        //    lblError.Visible = false;
        //    if (radTodate.SelectedDate != null)
        //    {

        //        //if (radTodate.SelectedDate.Value.Year > DateTime.Now.Year + 1)
        //        //{
        //        //    radTodate.SelectedDate = null;
        //        //    lblError.Text = "Erreur : L'année finale ne doit pas dépasser " + DateTime.Now.Year + 1;
        //        //    lblError.Visible = true;
        //        //}
        //        int _toDate = int.Parse(radTodate.SelectedDate.Value.ToString("yyyy"));
        //        int _nextDate = int.Parse(DateTime.Now.AddYears(1).ToString("yyyy"));
        //        if (_toDate != _nextDate)
        //        {
        //            string nextYear = DateTime.Now.AddYears(1).ToString("yyyy");
        //            radTodate.SelectedDate = null;
        //            lblError.Text = "Erreur : Fin Année Académique doit etre en " + nextYear;
        //            lblError.Visible = true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblError.Text = ex.Message;
        //    lblError.Visible = true;
        //}
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridAcademicYear_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<Settings> listResult = Settings.getListAcademicYear();
        gridAcademicYear.DataSource = listResult;
    }

    protected void gridAcademicYear_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridAcademicYear_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridAcademicYear.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}