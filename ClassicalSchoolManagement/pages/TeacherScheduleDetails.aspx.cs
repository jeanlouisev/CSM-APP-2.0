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



public partial class TeacherScheduleDetails : System.Web.UI.Page
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
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }

            if (Session["teacher_id"] != null)
            {
                string teacherCode = Session["teacher_id"].ToString();
                Teacher t = Teacher.getTeacherInfoById(teacherCode);
                if (t != null)
                {
                    pnlinfoteacher.Visible = true;
                    lblFullName.Text = t.fullName.ToUpper();
                    lblTeachCode.Text = teacherCode.ToUpper();
                    //  lblTotalHours.Text = "00:00:00";
                }
                BindDataGridSchedule();
            }
            //loadUserInformation();
        }
    }

    public void gridListSChedule_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gridListSChedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }
    }

    private void BindDataGridSchedule()
    {
        List<Schedule> listSchedule = Schedule.getListScheduleByTeacherCode(Session["teacher_id"].ToString());

        if (listSchedule.Count > 0 && listSchedule != null)
        {
            lblScheduleResult.Visible = false;
            pnlresulTeacherSchedule.Visible = true;
        }
        else
        {
            lblScheduleResult.Visible = false;
            pnlresulTeacherSchedule.Visible = true;
            lblScheduleResult.Visible = true;
        }
        gridListSChedule.DataSource = listSchedule;
        gridListSChedule.DataBind();
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {

            Users user = Session["user"] as Users;
            string userName = user.username;

            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + userName))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + userName);
            }

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\liste_horaies_professeur{1}_{2}.xls",
                userName, userName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            //gridListSChedule.HeaderRow.Visible = true;
            gridListSChedule.AllowPaging = false;
            //gridListSChedule.HeaderStyle.Font.Bold = true;
            //gridListSChedule.GridLines = GridLines.Vertical;
            //gridListPolicy.DataBind();

            //make header visible so that we can have it on the form
            //  gridListSChedule.HeaderRow.Visible = true;
            //GetSalaryTable();

            if (gridListSChedule.Visible == true)
            {
                // hide first column
                gridListSChedule.HeaderRow.Cells[0].Visible = false;

                // setup header background color to navy
                gridListSChedule.HeaderRow.Cells[1].BackColor = Color.Navy;
                gridListSChedule.HeaderRow.Cells[2].BackColor = Color.Navy;
                gridListSChedule.HeaderRow.Cells[3].BackColor = Color.Navy;
                gridListSChedule.HeaderRow.Cells[4].BackColor = Color.Navy;
                gridListSChedule.HeaderRow.Cells[5].BackColor = Color.Navy;

                // setup header forecolor to white
                gridListSChedule.HeaderRow.Cells[1].ForeColor = Color.White;
                gridListSChedule.HeaderRow.Cells[2].ForeColor = Color.White;
                gridListSChedule.HeaderRow.Cells[3].ForeColor = Color.White;
                gridListSChedule.HeaderRow.Cells[4].ForeColor = Color.White;
                gridListSChedule.HeaderRow.Cells[5].ForeColor = Color.White;
                //
                //
                gridListSChedule.RowStyle.Height = 20;
                for (int i = 0; i < gridListSChedule.Rows.Count; i++)
                {
                    GridViewRow row = gridListSChedule.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[1].BorderWidth = 1;
                    row.Cells[2].BorderWidth = 1;
                    row.Cells[3].BorderWidth = 1;
                    row.Cells[4].BorderWidth = 1;
                    row.Cells[5].BorderWidth = 1;
                    //row.Cells[1].FindControl("btnDelete").Visible = false;
                }
                gridListSChedule.RenderControl(htmlWrite);
                BindDataGridSchedule();
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}