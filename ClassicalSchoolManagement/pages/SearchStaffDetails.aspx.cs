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




public partial class SearchStaffDetails : System.Web.UI.Page
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
            Session.Remove("staff_code");
            radFromDate.SelectedDate = DateTime.Today.AddDays(-365);
            radToDate.SelectedDate = DateTime.Now;
            BindDataGridStaff();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataGridStaff();
    }

    private void loadActiveClassroom()
    {
        /* List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
         ddlClassroom.DataValueField = "id";
         ddlClassroom.DataTextField = "name";
         ddlClassroom.DataSource = listClassroom;
         ddlClassroom.DataBind();
         ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
         ddlClassroom.SelectedValue = "-1";*/
    }

    private void BindDataGridStaff()
    {
        Staff st = new Staff();
        //get the fields from the form            
        st.Staff_code = txtCode.Text.Trim().Length <= 0 ? "%" : txtCode.Text.Trim();
        st.first_name = txtFirst.Text.Trim().Length <= 0 ? "%" : "%" + txtFirst.Text.Trim().ToLower() + "%";
        st.last_name = txtLastName.Text.Trim().Length <= 0 ? "%" : "%" + txtLastName.Text.Trim().ToLower() + "%";
        st.sex = ddlSex.SelectedValue.ToString() == "-1" ? "%" : ddlSex.SelectedValue;
        st.fromDate = radFromDate.SelectedDate.Value == null ? "%" : radFromDate.SelectedDate.Value.ToString("yyyyMMdd");
        st.toDate = radToDate.SelectedDate.Value == null ? "%" : radToDate.SelectedDate.Value.ToString("yyyyMMdd");
        st.marital_status = ddlMaritalStatus.SelectedValue.ToString() == "-1" ? "%" : ddlMaritalStatus.SelectedValue;
        st.position_id = 0; // dllposition.SelectedValue.ToString() == "-1" ? "%" : dllposition.SelectedValue;
        st.Status = 1;

        //get list of staff
        List<Staff> listResult = Staff.getListStaff(st);
        if (listResult.Count > 0)
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
        gridListStaff.DataSource = listResult;
        gridListStaff.DataBind();

    }

    protected void gridListStaff_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /*gridListStudent.PageIndex = e.NewPageIndex;
       BindDataGridStudent();*/
    }

    protected void gridListStaff_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        /* // Convert the row index stored in the CommandArgument
         // property to an Integer.
         int index = Convert.ToInt32(e.CommandArgument);

         // Retrieve the row that contains the button clicked
         // by the user from the Rows collection.
         GridViewRow row = gridListStudent.Rows[index];
         String studentCode = row.Cells[1].Text;

         // If multiple buttons are used in a GridView control, use the
         // CommandName property to determine which button was clicked.
         if (e.CommandName == "ViewStudentDetails")
         {
             Session["user_code"] = studentCode;
             string page_url = "StudentDetailsInformation.aspx";
             try
             {
                 //Response.Redirect("DocumentDetail.aspx");
                 //Session["type_detail"] = "endedit";
                 //mp1.Show();
                 string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                 + "oWinn.show();"
                                                 + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                 + "oWinn.SetSize(1100, 500);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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
         if (e.CommandName == "EditStudent")
         {
             try
             {
                 /*
                 //load list of classroom available
                 loadActiveClassroom(ddlClassroomStudent);

                 MasterPanel1.Visible = true;
                 pnlResult.Visible = false;
                 pnlSearchStudent.Visible = false;

                 List<Student> mylist = Student.getListStudentById(studentCode);
                 //get student data
                 txtCodeStudent.Text = mylist[0].id;
                 txtFirstNameStudent.Text = mylist[0].first_name;
                 ddlSexStudent.SelectedText = mylist[0].sex;
                 txtCardIdStudent.Text = mylist[0].id_card;
                 txtBirthPlaceStudent.Text = mylist[0].birth_place;
                 txtPhone1Student.Text = mylist[0].phone1;
                 txtLastNameStudent.Text = mylist[0].last_name;
                 ddlMaritalStatusStudent.SelectedText = mylist[0].marital_status;
                 radBirthDateStudent.SelectedDate = mylist[0].birth_date;
                 txtAddressStudent.Text = mylist[0].address;
                 ddlVacationStudent.SelectedText = mylist[0].vacation;
                 txtEmailStudent.Text = mylist[0].email;
                 imageKeeper.ImageUrl = "~/images/image_data/" + mylist[0].imagePath;
                 ddlClassroomStudent.SelectedValue = mylist[0].classroom;
                 Session["user_code"] = mylist[0].id;
                 Session["fullname"] = mylist[0].last_name + " " + mylist[0].first_name;
                 //get list of available documents
                 List<Documents> documentList = Documents.getListDocumentsByStaffCode(txtCodeStudent.Text.Trim());
                 lnkStudentDocumentsCounter.Text = documentList.Count + " documents trouve(e)";
                 *//*
             }
             catch (Exception ex)
             {
                 MessBox.Show("Error : " + ex.Message);
             }
         }*/
    }

    protected void gridListStaff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              if (e.Row.RowIndex == 0)
               //   e.Row.Style.Add("height", "50px");
              e.Row.Style.Add("vertical-align", "bottom");
          }*/
    }

    public void pickUpStaff(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            int index = Convert.ToInt32(imageButton.CommandArgument);
            GridViewRow row = gridListStaff.Rows[index];
            Session["staff_code"] = gridListStaff.DataKeys[index].Value;
            // call javaScript to close the window from code behind
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseDialog();", true);
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    /*  private void loadActiveClassroom(RadDropDownList dropDownList)
      {
          List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
          dropDownList.DataValueField = "id";
          dropDownList.DataTextField = "name";
          dropDownList.DataSource = listClassroom;
          dropDownList.DataBind();
          dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
          dropDownList.SelectedValue = "-1";
      }*/

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    protected void lnkStudentDocumentsCounter_Click(object sender, EventArgs e)
    {
        // fake reason
    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        // fake reason
    }

}
