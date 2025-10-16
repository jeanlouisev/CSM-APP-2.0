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



public partial class AffectAmountHistoric : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            if (Session["staff_code"] != null
                && Session["fullname"] != null
                && Session["position"] != null)
            {
                lblStaffCode.Text = Session["staff_code"].ToString();
                lblFullname.Text = Session["fullname"].ToString().ToUpper();// + " " + Session["first_name"].ToString().ToUpper();
                lblPosition.Text = Session["position"].ToString();
            }

            BindDataGridStaff();
        }

    }

    private void BindDataGridStaff()
    {
        string staffCode = null;
        if (Session["staff_code"] != null)
        {
            staffCode = Session["staff_code"].ToString().ToUpper();
        }

        //get list of staff
        List<Salary> listResult = Salary.getListBasicAmountHisByStaffCode(staffCode);
        if (listResult.Count > 0 && listResult != null)
        {
            lblFound.Visible = false;
            pnlResult.Visible = true;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count + " Ligne(s)";
            // lnkExportExcel.Visible = true;
            tblGridHeader.Visible = true;
        }
        else
        {
            lblFound.Visible = true;
            pnlResult.Visible = true;
            lblCounter.Visible = false;
            //  lnkExportExcel.Visible = false;
            tblGridHeader.Visible = false;
        }
        gridListStaff.DataSource = listResult;
        gridListStaff.DataBind();
    }

}