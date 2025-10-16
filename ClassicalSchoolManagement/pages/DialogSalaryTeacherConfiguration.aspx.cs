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



public partial class DialogSalaryTeacherConfiguration : System.Web.UI.Page
{
    int menu_code = 3; // parameter menu code, for more information see "menu" table.

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }


        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }


        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        if (!IsPostBack)   // -----> start postback
        {
            // Session["menu"] = queryMenu;
            Users user = Session["user"] as Users;

            // remove old sessions
            Session.Remove("staff_code");
            Session.Remove("first_name");
            Session.Remove("last_name");
            Session.Remove("position");
            //
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
        } // ---------------------> end postback

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

            List<Teacher> listResult = Teacher.getListTeacherTaxGroup(t);
            if (listResult != null)
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
                //  lnkExportExcel.Visible = false;
                //tblGridHeader.Visible = false;
            }
            gridListTeacher.DataSource = listResult;
            gridListTeacher.DataBind();
            //
            if (listResult != null && listResult.Count > 0)
            {
                // get list tax_group 
                List<Salary> listGroupTax = Salary.getListTax();
                //add taxgroup to each combo box
                foreach (GridViewRow row in gridListTeacher.Rows)
                {
                    HiddenField hiddenTaxGroupName = row.FindControl("hiddenTaxGroupName") as HiddenField;
                    RadComboBox cboTaxGroup = row.FindControl("ddlTaxGroup") as RadComboBox;
                    cboTaxGroup.Items.Clear();
                    if (listGroupTax != null && listGroupTax.Count > 0)
                    {
                        // fill the ddl now
                        cboTaxGroup.DataValueField = "id";
                        cboTaxGroup.DataTextField = "group_name";
                        cboTaxGroup.DataSource = listGroupTax;
                        cboTaxGroup.DataBind();
                    }
                    cboTaxGroup.Items.Insert(0, new RadComboBoxItem("--Tout Sélectionner--", "-1"));

                    if (hiddenTaxGroupName != null)
                    {
                        cboTaxGroup.SelectedValue = hiddenTaxGroupName.Value;
                    }
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void gridListTeacher_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListTeacher.Rows[index];
        string staffCode = row.Cells[1].Text.Replace("&nbsp;", "");

        if (e.CommandName == "validateAmount")
        {
            RadComboBox cbo = row.FindControl("ddlTaxGroup") as RadComboBox;
            int groupNameId = int.Parse(cbo.SelectedValue);
            //
            if (groupNameId <= 0)
            {
                MessBox.Show("Desolé, veuillez selectionner un groupe de taxe !");
            }
            else
            {
                Users user = Session["user"] as Users;
                Salary s = new Salary();
                s.staff_code = staffCode;
                s.tax_id = int.Parse(cbo.SelectedValue.ToString());
                //
                Salary.AddStaffToTaxGroup(s);
                // confirmation message
                MessageAlert.RadAlert("Succes !!!!", 250, 180, "Confirmation", null, "../images/success_check.png");
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

    public void removeStaff(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListTeacher.Rows[index];
                string staffCode = row.Cells[1].Text;
                // prevent user from deleting administrator account
                if (staffCode == "PS-1")
                {
                    MessBox.Show("Sorry, but you cannot delete this account.\nThis is the system admin account.\nPlease contact SAPHIR GIANT TECHNOLOGY.\nThank you !");
                }
                else
                {
                    Staff.disableStaff(staffCode);
                }
                //refresh data of the gridview
                BindDataGridTeacher();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void txtEmailStaffUpdate_TextChanged1(object sender, EventArgs e)
    {
        RadTextBox email = sender as RadTextBox;
        if (email.Text.Trim().Length > 0)
        {
            bool checkEmail = Universal.IsValidEmailAddress(email.Text.Trim());
            if (!checkEmail)
            {
                MessageAlert.RadAlert("Error : Adresse email est invalide !", 450, 200, "Information", null);
                email.Text = string.Empty;
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void lnkExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;
            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + user.username))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + user.username);
            }

            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\liste_personnel_{1}_{2}.xls",
                user.username, user.username, DateTime.Now.ToString("yyyyMMddHHmmss"));
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
                gridListTeacher.HeaderRow.Cells[0].Visible = false;
                gridListTeacher.HeaderRow.Cells[1].Width = 100;
                gridListTeacher.HeaderRow.Cells[2].Width = 100;
                gridListTeacher.HeaderRow.Cells[4].Width = 100;
                gridListTeacher.HeaderRow.Cells[5].Width = 200;
                gridListTeacher.HeaderRow.Cells[6].Width = 100;
                gridListTeacher.HeaderRow.Cells[7].Width = 100;
                //
                //design the outter border
                gridListTeacher.BorderStyle = BorderStyle.Solid;
                gridListTeacher.BorderColor = System.Drawing.Color.Black;
                gridListTeacher.BorderWidth = 1;
                //
                // design the rows
                gridListTeacher.RowStyle.BorderStyle = BorderStyle.Solid;
                gridListTeacher.RowStyle.BorderColor = System.Drawing.Color.Black;
                gridListTeacher.RowStyle.BorderWidth = 1;
                gridListTeacher.RowStyle.Height = 20;
                //
                for (int i = 0; i < gridListTeacher.Rows.Count; i++)
                {
                    GridViewRow row = gridListTeacher.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    row.Cells[10].Visible = false;
                    //row.Cells[1].FindControl("btnDelete").Visible = false;
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


}