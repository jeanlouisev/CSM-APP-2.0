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



public partial class AveragePreviousConfigurationDetails : System.Web.UI.Page
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
            BindDataGridAverage();
        }
    }

    private void BindDataGridAverage()
    {
        int _currentAcademicYear = 0;
        if (Session["current_academic_year"] != null)
        {
            _currentAcademicYear = int.Parse(Session["current_academic_year"].ToString());
        }

        List<Notes> listResult = Notes.getListPreviousAverageConfiguration(_currentAcademicYear);
        //
        if (listResult.Count > 0 && listResult != null)
        {
            lblFound.Visible = false;
            pnlResult.Visible = true;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count + " Ligne(s)";
            tblGridHeader.Visible = true;
        }
        else
        {
            lblFound.Visible = true;
            pnlResult.Visible = true;
            lblCounter.Visible = false;
            tblGridHeader.Visible = false;
        }
        gridListAverage.DataSource = listResult;
        gridListAverage.DataBind();
    }

    protected void gridListAverage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListAverage.PageIndex = e.NewPageIndex;
        BindDataGridAverage();
    }

    protected void gridListAverage_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        try
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button clicked
            // by the user from the Rows collection.
            GridViewRow row = gridListAverage.Rows[index];
            String studentCode = row.Cells[1].Text;

            // If multiple buttons are used in a GridView control, use the
            // CommandName property to determine which button was clicked.
            if (e.CommandName == "ViewStudentDetails")
            {
                Session["user_code"] = studentCode;
                string page_url = "StudentDetailsInformation.aspx";

                //Response.Redirect("DocumentDetail.aspx");
                //Session["type_detail"] = "endedit";
                //mp1.Show();
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(1100, 600);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            if (e.CommandName == "loadConfiguration")
            {
                MessBox.Show("Test ok");
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    protected void gridListAverage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
            e.Row.Style.Add("vertical-align", "bottom");
        }
            */
    }

    public void removeStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListAverage.Rows[index];
                string studentCode = row.Cells[1].Text;

                //this part only set status of student to 0
                Student.disableStudent(studentCode);

                // this part delete the student completely from the system
                //  Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                BindDataGridAverage();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }
}