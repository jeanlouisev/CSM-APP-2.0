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
using Utilities;
using Telerik.Web;
using Telerik.Web.UI;



public partial class Parents : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.STUDENT;

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
            //Session.Remove("preload_id_class");
            //Session.Remove("preload_staff_code");
            //Session.Remove("preload_academic_year");
            ////
            //imageUploaderReference.Attributes["onchange"] = "UploadFileReference(this)";
            //BindDataGridParent();
            // hide elements
            lblError.Visible = false;
            lblSuccess.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataGridParent();
            Users user = Session["user"] as Users;
            // check login_user policy to grant or revoke access
            //List<Users> listGroupPolicy = Users.getListGroupPolicyByRoleId(user.id_role, menu_code);
            //if (listGroupPolicy != null && listGroupPolicy.Count > 0)
            //{
            //    if (listGroupPolicy[0].role_view == 0
            //        && listGroupPolicy[0].role_edit == 0
            //        && listGroupPolicy[0].role_delete == 0)
            //    {
            //        //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
            //        Response.Redirect("~/Default.aspx");
            //    }
            //    else
            //    {
            //        // edit
            //        if (listGroupPolicy[0].role_edit == 0)
            //        {
            //            disableEditOption();
            //        }
            //        // delete
            //        if (listGroupPolicy[0].role_delete == 0)
            //        {
            //            disableDeleteOption();
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void gridListParent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListParent.PageIndex = e.NewPageIndex;
        BindDataGridParent();
    }

    private void BindDataGridParent()
    {
        Users user = Session["user"] as Users;
        //
        StudentContactPerson p = new StudentContactPerson();
        p.first_name = txtParentFirstName.Text.Trim().Length <= 0 ? null : txtParentFirstName.Text.Trim();
        p.last_name = txtParentLastName.Text.Trim().Length <= 0 ? null : txtParentLastName.Text.Trim();
        p.phone = txtParentPhone.Text.Trim().Length <= 0 ? null : txtParentPhone.Text.Trim();
        p.address = txtParentAdress.Text.Trim().Length <= 0 ? null : txtParentAdress.Text.Trim();
        p.id_card = txtParentIdCard.Text.Trim().Length <= 0 ? null : txtParentIdCard.Text.Trim();
        p.sex = ddlParentSex.SelectedValue == "-1" ? null : ddlParentSex.SelectedValue;
        p.marital_status = ddlParentMaritalStatus.SelectedValue == "-1" ? null : ddlParentMaritalStatus.SelectedValue;
        //p.prefix_name = prefixName;

        try
        {
            List<StudentContactPerson> listResult = StudentContactPerson.getListParentsInfo(p);
            if (listResult != null && listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count + " Ligne(s)";
                btnExportExcel.Visible = true;
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                btnExportExcel.Visible = false;
            }
            gridListParent.DataSource = listResult;
            gridListParent.DataBind();


            // check login_user policy to grant or revoke access
            //List<Users> listGroupPolicy = Users.getListGroupPolicyByRoleId(user.id_role, menu_code);
            //if (listGroupPolicy != null && listGroupPolicy.Count > 0)
            //{
            //    if (listGroupPolicy[0].role_view == 0
            //        && listGroupPolicy[0].role_edit == 0
            //        && listGroupPolicy[0].role_delete == 0)
            //    {
            //        //MessageAlert.RadAlert("Desolé, vous ne pouvez acceder !", 350, 150, "Error", null, "/images/warning.png");
            //        Response.Redirect("~/Default.aspx");
            //    }
            //    else
            //    {
            //        // edit
            //        if (listGroupPolicy[0].role_edit == 0)
            //        {
            //            disableEditOption();
            //        }
            //        // delete
            //        if (listGroupPolicy[0].role_delete == 0)
            //        {
            //            disableDeleteOption();
            //        }
            //    }
            //}
        }
        catch (Exception ex) { throw ex; }
    }

    protected void gridListParent_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int index = Convert.ToInt32(e.CommandArgument);
        // Retrieve the row that contains the button clicked
        // by the user from the Rows collection.
        GridViewRow row = gridListParent.Rows[index];
        string parentId = gridListParent.DataKeys[index].Value.ToString();

        // If multiple buttons are used in a GridView control, use the
        // CommandName property to determine which button was clicked.
        if (e.CommandName == "ViewParentDetails")
        {
            Session["parent_id"] = parentId;
            //
            string page_url = "DialogParentDetailsInformation.aspx";
            try
            {
                //Response.Redirect("DocumentDetail.aspx");
                //Session["type_detail"] = "endedit";
                //mp1.Show();
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(800, 500);"           // old value ---->    "oWinn.SetSize(1024, 600);"
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

        if (e.CommandName == "editParent")
        {
            try
            {
                int id = int.Parse(parentId);
                StudentContactPerson p = StudentContactPerson.getListParentsById(id);
                //
                txtParentFirstName.Text = p.first_name;
                txtParentLastName.Text = p.last_name;
                txtParentPhone.Text = p.phone;
                txtParentAdress.Text = p.address;
                txtParentIdCard.Text = p.id_card;
                ddlParentSex.SelectedValue = p.sex;
                ddlParentMaritalStatus.SelectedValue = p.marital_status;
                imageKeeperReference.ImageUrl = "/images/image_data/" + p.image_path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

    public void removeParent(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            //
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListParent.Rows[index];
                string parentCode = row.Cells[1].Text;

                if (parentCode != null)
                {
                    // check if parent_code is referenced.
                    List<Universal> listUniversal = Universal.getListExternalUserByReferenceCode(parentCode);
                    if (listUniversal == null || listUniversal.Count <= 0)
                    {
                        Universal.removeExternalReferenceInformation(parentCode);
                        //  lblSuccess.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "Désolé, vous ne pouvez supprimer ce parent car il est deja referencé";
                        lblError.Visible = true;
                    }
                    // reload the grid
                    BindDataGridParent();

                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void gridListParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }


        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
            e.Row.Style.Add("vertical-align", "bottom");
        }
            */
    }

    private void fillParentForm(Responsible responsible)
    {
        try
        {
            txtParentFirstName.Text = responsible.first_name;
            txtParentLastName.Text = responsible.last_name;
            string idCardReference = responsible.id_card == null ? "" : responsible.id_card;
            if (idCardReference.Trim().Length <= 0)
            {
                //chkNifReference.Checked = true;
                //chkNifReference_CheckedChanged(this, null);
                txtParentIdCard.Text = string.Empty;
            }
            else if (idCardReference.Trim().Length == 10)
            {
                //chkNifReference.Checked = true;
                //chkNifReference_CheckedChanged(this, null);
                txtParentIdCard.Text = idCardReference.Trim();
            }
            else if (idCardReference.Trim().Length == 17)
            {
                //chkCinReference.Checked = true;
                //chkCinReference_CheckedChanged(this, null);
                txtParentIdCard.Text = idCardReference.Trim();
            }
            else
            {
                //chkNifReference.Checked = true;
                //chkNifReference_CheckedChanged(this, null);
                txtParentIdCard.Text = string.Empty;
            }

            txtParentPhone.Text = responsible.phone;
            txtParentAdress.Text = responsible.adress;
            ddlParentMaritalStatus.SelectedValue = responsible.marital_status_code.ToUpper();
            ddlParentSex.SelectedValue = responsible.sex_code.ToUpper();
            imageKeeperReference.ImageUrl = responsible.image_path == null ? "~/images/image_data/Default.png" : "~/images/image_data/" + responsible.image_path;
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void enableResetParentForm()
    {
        //empty parent form
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        ddlParentMaritalStatus.SelectedValue = "-1";
        txtParentIdCard.Text = "";
        txtParentPhone.Text = "";
        txtParentAdress.Text = "";

        //enabled all reference fields
        txtParentFirstName.Enabled = true;
        txtParentLastName.Enabled = true;
        txtParentIdCard.Enabled = true;
        txtParentPhone.Enabled = true;
        txtParentAdress.Enabled = true;
        ddlParentMaritalStatus.Enabled = true;
        ddlParentSex.Enabled = true;
        //chkNifReference.Enabled = true;
        //chkNifReference.Checked = true;
        //chkCinReference.Enabled = true;
    }

    private void disableParentForm()
    {
        //disabled all reference fields
        txtParentFirstName.Enabled = false;
        txtParentLastName.Enabled = false;
        txtParentIdCard.Enabled = false;
        txtParentPhone.Enabled = false;
        txtParentAdress.Enabled = false;
        ddlParentMaritalStatus.Enabled = false;
        ddlParentSex.Enabled = false;
        //chkNifReference.Enabled = false;
        //chkCinReference.Enabled = false;
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
                GridViewRow row = gridListParent.Rows[index];
                string studentCode = row.Cells[1].Text;

                //this part only set status of student to 0
                // Student.deleteStudentTemporarily(studentCode);

                // this part delete the student completely from the system
                Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                BindDataGridParent();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public void disableStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value_disable"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListParent.Rows[index];
                string studentCode = row.Cells[1].Text;

                //this part only set status of student to 0
                Student.disableStudent(studentCode);

                // this part delete the student completely from the system
                //  Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                BindDataGridParent();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    private void emptyFields()
    {
        //empty parent form
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        ddlParentMaritalStatus.SelectedValue = "-1";
        txtParentIdCard.Text = "";
        txtParentPhone.Text = "";
        txtParentAdress.Text = "";
        //chkNifReference.Checked = true;
        //chkCinReference.Checked = false;
        imageKeeperReference.ImageUrl = "~/images/image_data/Default.png";
        //Clear reference code
        Session.Remove("reference_code");
    }

    //private bool checkParentInfoBeforeValidation()
    //{
    //    bool result = false;

    //    if (txtParentFirstName.Text.Trim().Length > 0
    //        && txtParentFirstName.Text.Trim().ToString() != "")
    //    {
    //        result = true;
    //    }
    //    else if (txtParentLastName.Text.Trim().Length > 0
    //               && txtParentLastName.Text.Trim().ToString() != "")
    //    {
    //        result = true;
    //    }
    //    else if (ddlParentSex.SelectedValue.ToString() != "-1")
    //    {
    //        result = true;
    //    }
    //    else if (ddlParentMaritalStatus.SelectedValue.ToString() != "-1")
    //    {
    //        result = true;
    //    }

    //    //else if (txtParentIdCard.Text.Trim().Length > 0
    //    //         && txtParentIdCard.Text.ToString() != "")
    //    //{
    //    //    result = true;
    //    //}
    //    else if (txtParentPhone.Text.Trim().Length > 0
    //        && txtParentPhone.Text.ToString() != "")
    //    {
    //        result = true;
    //    }
    //    /**
    //    else if (txtParentAdress.Text.Trim().Length > 0
    //            && txtParentAdress.Text.ToString() != "")
    //    {
    //        result = true;
    //    }***/
    //    else if (ddlParentRelationship.SelectedValue.ToString() != "-1")
    //    {
    //        result = true;
    //    }

    //    return result;
    //}


    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    //protected void chkNifReference_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtParentIdCard.Text = string.Empty;
    //    if (chkNifReference.Checked)
    //    {
    //        txtParentIdCard.MaxLength = 10;
    //        chkCinReference.Checked = false;
    //    }
    //    else
    //    {
    //        txtParentIdCard.MaxLength = 17;
    //        chkCinReference.Checked = true;
    //    }
    //    updCardIdReferenceChk.Update();
    //}

    //protected void chkCinReference_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtParentIdCard.Text = string.Empty;
    //    if (chkCinReference.Checked)
    //    {
    //        txtParentIdCard.MaxLength = 17;
    //        chkNifReference.Checked = false;
    //    }
    //    else
    //    {
    //        txtParentIdCard.MaxLength = 10;
    //        chkNifReference.Checked = true;
    //    }
    //    updCardIdReferenceChk.Update();
    //}

    protected void txtParentIdCard_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            //
            string msg = null;
            if (txtParentIdCard.Text.Trim() != string.Empty)
            {
                // get parents information by card-id
                StudentContactPerson parent = StudentContactPerson.getParentInfoByIdCard(txtParentIdCard.Text.Trim());
                if (parent != null)
                {
                    txtParentFirstName.Text = parent.first_name;
                    txtParentLastName.Text = parent.last_name;
                    txtParentPhone.Text = parent.phone;
                    txtParentAdress.Text = parent.address;
                    ddlParentMaritalStatus.SelectedValue = parent.marital_status;
                    ddlParentSex.SelectedValue = parent.sex;
                    imageKeeperReference.ImageUrl = "/images/image_data/" + parent.image_path;
                }

                //if (chkCinReference.Checked)
                //{
                //    //check the length
                //    if (txtParentIdCard.Text.Trim().Length < 17)
                //    {
                //        msg = "Erreur : Le CIN " + txtParentIdCard.Text.Trim() + " est invalide !";
                //        txtParentIdCard.Text = "";
                //        txtParentIdCard.Focus();
                //        //MessageAlert.RadAlert(msg, 350, 200, "Information", null);
                //        lblError.Text = msg;
                //    }
                //    else
                //    {
                //        if (Universal.filterFoundIdCard(txtParentIdCard.Text.Trim()))
                //        {
                //            msg = "Erreur : Ce CIN " + txtParentIdCard.Text.Trim() + " a ete deja utilise !";
                //            txtParentIdCard.Text = "";
                //            txtParentIdCard.Focus();
                //            //MessageAlert.RadAlert(msg, 350, 200, "Information", null);
                //            lblError.Text = msg;
                //        }
                //    }
                //}
                //else
                //{
                //    //check the length
                //    if (txtParentIdCard.Text.Trim().Length < 10)
                //    {
                //        msg = "Erreur : le NIF " + txtParentIdCard.Text.Trim() + " est invalide !";
                //        txtParentIdCard.Text = "";
                //        txtParentIdCard.Focus();
                //        //MessageAlert.RadAlert(msg, 350, 200, "Information", null);
                //        lblError.Text = msg;
                //    }
                //    else
                //    {
                //        if (Universal.filterFoundIdCard(txtParentIdCard.Text.Trim()))
                //        {
                //            msg = "Erreur : le NIF " + txtParentIdCard.Text.Trim() + " a ete deja utilise !";
                //            txtParentIdCard.Text = "";
                //            txtParentIdCard.Focus();
                //            //MessageAlert.RadAlert(msg, 350, 200, "Information", null);
                //            lblError.Text = msg;
                //        }
                //    }
                //}
            }
            else
            {
                // empty the fields
                txtParentFirstName.Text = "";
                txtParentLastName.Text = "";
                txtParentPhone.Text = "";
                txtParentAdress.Text = "";
                ddlParentMaritalStatus.SelectedValue = "-1";
                ddlParentSex.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnUploadImageReference_Click(object sender, ImageClickEventArgs e)
    {
        if (imageUploaderReference.HasFile) // check fileUpload control for files
        {
            string file_extension = System.IO.Path.GetExtension(imageUploaderReference.FileName); // get file extension
            if (file_extension.ToLower() == ".jpeg"
                || file_extension.ToLower() == ".jpg"
                || file_extension.ToLower() == ".png"
                || file_extension.ToLower() == ".bitmap") //check file extension
                try
                {
                    if (imageUploaderReference.PostedFile.ContentLength > 0)
                    {
                        string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssff") + "_" + Path.GetFileName(imageUploaderReference.PostedFile.FileName);
                        string filePaths = "~/images/image_data/" + fileName;
                        imageUploaderReference.SaveAs(Server.MapPath(filePaths)); //save file to folder
                        imageKeeperReference.ImageUrl = filePaths;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

            else
            {
                MessBox.Show(file_extension + " --> Veuillez choisir un fichier d\'extension : .jpeg / .jpg / .png / .bitmap");
            }
        }
        else
        {
            MessBox.Show("Veuillez choisir une image.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateParentFields())
        {
            StudentContactPerson p = new StudentContactPerson();
            p.id_card = txtParentIdCard.Text.Trim();
            p.first_name = txtParentFirstName.Text.Trim();
            p.last_name = txtParentLastName.Text.Trim();
            p.marital_status = ddlParentMaritalStatus.SelectedValue;
            p.sex = ddlParentSex.SelectedValue;
            p.phone = txtParentPhone.Text.Trim();
            p.address = txtParentAdress.Text.Trim();
            p.image_path = Path.GetFileName(imageKeeperReference.ImageUrl).Replace("~/images/image_data/", "");

            StudentContactPerson parent = StudentContactPerson.getParentInfoByIdCard(txtParentIdCard.Text.Trim());
            if (parent != null)
            {
                // update existing parent information
                StudentContactPerson.updateParents(p);
            }
            else
            {
                // add new parent information
                //StudentContactPerson.insertParents(p);
            }
            // reset form
            resetMainForm();
            // reload grid
            BindDataGridParent();
        }
    }

    private void resetMainForm()
    {
        txtParentIdCard.Text = string.Empty;
        txtParentFirstName.Text = string.Empty;
        txtParentLastName.Text = string.Empty;
        ddlParentMaritalStatus.SelectedValue = "-1";
        ddlParentSex.SelectedValue = "-1";
        txtParentPhone.Text = string.Empty;
        txtParentAdress.Text = string.Empty;
        imageKeeperReference.ImageUrl = "~/images/image_data/Default.png";
    }

    private bool validateParentFields()
    {
        lblError.Visible = false;
        bool result = true;

        if (txtParentFirstName.Text.Trim().Length <= 0
            && txtParentFirstName.Text.Trim().ToString() == "")
        {
            lblError.Text = "Erreur : Veuillez saisir le nom du parent.";
            lblError.Visible = true;
            txtParentFirstName.Focus();
            result = false;
        }
        else if (txtParentLastName.Text.Trim().Length <= 0
         && txtParentLastName.Text.Trim().ToString() == "")
        {
            lblError.Text = "Erreur : Veuillez saisir le prenom du parent.";
            lblError.Visible = true;
            txtParentLastName.Focus();
            result = false;
        }
        else if (txtParentPhone.Text.Trim().Length <= 0
         && txtParentPhone.Text.Trim().ToString() == "")
        {
            lblError.Text = "Erreur : Veuillez saisir le telephone du parent.";
            lblError.Visible = true;
            txtParentPhone.Focus();
            result = false;
        }
        else if (txtParentAdress.Text.Trim().Length <= 0
                  && txtParentAdress.Text.Trim().ToString() == "")
        {
            lblError.Text = "Erreur : Veuillez saisir l\'adresse du parent.";
            lblError.Visible = true;
            txtParentAdress.Focus();
            result = false;
        }
        else if (ddlParentSex.SelectedValue.ToString() == "-1")
        {
            lblError.Text = "Erreur : Veuillez selectionner le sexe du parent.";
            lblError.Visible = true;
            ddlParentSex.Focus();
            result = false;
        }
        else if (ddlParentMaritalStatus.SelectedValue.ToString() == "-1")
        {
            lblError.Text = "Erreur : Veuillez selectionner le État Civil du parent.";
            lblError.Visible = true;
            ddlParentMaritalStatus.Focus();
            result = false;
        }
        return result;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Users user = Session["user"] as Users;

            if (!Directory.Exists(Request.PhysicalApplicationPath + @"..\downloads\" + user.username))
            {
                Directory.CreateDirectory(Request.PhysicalApplicationPath + @"..\downloads\" + user.username);
            }
            //
            string Path = string.Format(Request.PhysicalApplicationPath + @"..\downloads\{0}\liste_parents_{1}.xls",
                user.username, DateTime.Now.ToString("yyyyMMddHHmmss"));
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            gridListParent.AllowPaging = false;
            gridListParent.HeaderStyle.Font.Bold = true;
            gridListParent.GridLines = GridLines.Vertical;
            //gridListPolicy.DataBind();
            BindDataGridParent();
            //GetSalaryTable();
            gridListParent.HeaderRow.Visible = true;

            if (gridListParent.Visible == true)
            {

                gridListParent.HeaderRow.Cells[0].Visible = false;
                gridListParent.HeaderRow.Cells[8].Visible = false;
                gridListParent.HeaderRow.Cells[9].Visible = false;

                //design the gridview
                //gridListParent.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListParent.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
                //gridListParent.BorderWidth = 1;
                //gridListParent.BackColor = Color.Transparent;
                //gridListParent.RowStyle.BackColor = Color.Transparent;
                //gridListParent.BorderStyle = BorderStyle.None;
                gridListParent.RowStyle.Height = 20;


                // setup header background color to navy
                gridListParent.HeaderRow.Cells[1].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[2].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[3].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[4].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[5].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[6].BackColor = Color.Navy;
                gridListParent.HeaderRow.Cells[7].BackColor = Color.Navy;

                // setup header forecolor to white
                gridListParent.HeaderRow.Cells[1].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[2].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[3].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[4].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[5].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[6].ForeColor = Color.White;
                gridListParent.HeaderRow.Cells[7].ForeColor = Color.White;

                //
                for (int i = 0; i < gridListParent.Rows.Count; i++)
                {
                    GridViewRow row = gridListParent.Rows[i];
                    row.Cells[0].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    row.BackColor = Color.Transparent;
                    //
                    row.Cells[1].BorderWidth = 1;
                    row.Cells[2].BorderWidth = 1;
                    row.Cells[3].BorderWidth = 1;
                    row.Cells[4].BorderWidth = 1;
                    row.Cells[5].BorderWidth = 1;
                    row.Cells[6].BorderWidth = 1;
                    row.Cells[7].BorderWidth = 1;
                }
                gridListParent.RenderControl(htmlWrite);
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
            Universal.WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
            //string token = Token.generate(Token.TypeToken.Download, user.Username, Response);
            //tokenField.Value = token;
            //
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        resetMainForm();
    }
}