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



public partial class AddPersonal : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.HR;
    string msgContent = "";

    List<StaffDocuments> listDocumentsAttach = new List<StaffDocuments>();

    //string sqlStaffCurval = @"select currval_staff('codeSeq') as staff_curval";
    string sqlStafftNextval = @"select nextval_staff('codeSeq') as staff_nextval";


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
            verifyAccessLevel();
        }

        if (!IsPostBack)
        {
            loadPositions();
            loadDocumentCategories();
            loadListRoles();
            loadListTaxGroup();
            imageUploader.Attributes["onchange"] = "UploadFile(this)";
            

            if (Session["staff_code"] == null)
            {
                // kill all unwanted sessions
                Session.Remove("list_documents_attach");
            }
            else
            {
                string staffCode = Session["staff_code"].ToString();
                loadStaffPreviousInformation(staffCode);
            }
        }
    }

    private void loadListTaxGroup()
    {
        // get list academic year
        List<Salary> listResult = Salary.getListTax();

        if (listResult != null && listResult.Count > 0)
        {
            // fill the ddl now
            ddlTax.DataValueField = "id";
            ddlTax.DataTextField = "group_name";
            ddlTax.DataSource = listResult;
            ddlTax.DataBind();
        }
        //
        ddlTax.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlTax.SelectedValue = "-1";
    }

    private void loadListRoles()
    {
        try
        {
            List<Users> listResult = Users.getAllRoleType();
            if (listResult != null && listResult.Count > 0)
            {
                ddlRoles.DataSource = listResult;
                ddlRoles.DataValueField = "id";
                ddlRoles.DataTextField = "name";
                ddlRoles.DataBind();
            }
            //
            ddlRoles.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
            ddlRoles.SelectedValue = "-1";
        }
        catch { }
    }

    private void loadPositions()
    {
        try
        {
            ddlPosition.Items.Clear();
            // get list all academic  year
            List<Staff> listResult = Staff.getListPositions();

            if (listResult != null && listResult.Count > 0)
            {
                ddlPosition.DataValueField = "id";
                ddlPosition.DataTextField = "name";
                ddlPosition.DataSource = listResult;
                ddlPosition.DataBind();
            }
        }
        catch (Exception ex) { }
    }

    private void verifyAccessLevel()
    {
        Users user = Session["user"] as Users;

        // VERIFY USER ACCESS LEVEL
        List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
        if (listResult != null && listResult.Count > 0)
        {
            Users userAccess = listResult[0];
            int notGranted = (int)Users.ACCESS.NO;

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

    // edit_access
    private void disableEditOption()
    {
        try
        {
            btnAttachDocuments.Attributes.Add("disabled", "disabled");
            btnSave.Attributes.Add("disabled", "disabled");
            btnBack.Attributes.Add("disabled", "disabled");
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        // nothing here
    }

    private void loadStaffPreviousInformation(string staffCode)
    {
        List<Staff> listStaffInfo = Staff.getListStaffById(staffCode);
        if (listStaffInfo != null && listStaffInfo.Count > 0)
        {
            Staff st = listStaffInfo[0];
            lblStaffCode.Text = st.id;
            txtFirstName.Text = st.first_name;
            txtLastName.Text = st.last_name;
            ddlSex.SelectedValue = st.sex_code;
            txtBirthPlace.Text = st.birth_place;
            radBirthDate.SelectedDate = st.birth_date;
            txtPhone1.Text = st.phone1;
            txtAddress.Text = st.adress;
            txtCardId.Text = st.id_card;
            txtEmail.Text = st.email;
            ddlStudyLevel.SelectedValue = st.study_level;
            ddlMaritalStatus.SelectedValue = st.marital_status;
            ddlPosition.SelectedValue = st.position_id.ToString();
            //
            ddlRoles.SelectedValue = st.role_id.ToString();

            string imagePaths = "~/images/image_data/" + st.image_path;
            imgStaff.Attributes.Add("src", imagePaths);


            // contact information 
            txtParentFirstName.Text = st.ref_first_name;
            txtParentLastName.Text = st.ref_last_name;
            ddlParentSex.SelectedValue = st.ref_sex;
            txtParentPhone.Text = st.ref_phone;
            txtParentAdress.Text = st.ref_adress;
            ddlParentRelationship.SelectedValue = st.ref_relationship;

            // salary
            txtSalary.Value = st.salary_amount;
            ddlTax.SelectedValue = st.tax_id.ToString();

            // positions
            List<Staff> listPositions = Staff.getListPositionsByStaffCode(st.id);
            if(listPositions != null && listPositions.Count > 0)
            {
                foreach(Staff pos in listPositions)
                {
                    foreach(RadComboBoxItem item in ddlPosition.Items)
                    {
                        if(item.Value == pos.position_id.ToString())
                        {
                            item.Checked = true;
                        }
                    }
                }
            }


            // documents
            Session["list_documents_attach"] = StaffDocuments.getListDocumentsByStaffCode(staffCode);
            gridAttachDocuments.Rebind();
            pnlDocuments.Visible = true;

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validateFields())
            {
                msgContent = "Erreur : tous les champs ayant un asterix (*) sont obligatoires !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                Users user = Session["user"] as Users;


                Staff st = new Staff();
                //get student data
                st.first_name = txtFirstName.Text.Trim();
                st.last_name = txtLastName.Text.Trim();
                st.sex = ddlSex.SelectedValue;
                st.marital_status = ddlMaritalStatus.SelectedValue;
                st.id_card = txtCardId.Text.Trim();
                st.birth_date = radBirthDate.SelectedDate.Value;
                st.birth_place = txtBirthPlace.Text.Trim();
                st.phone1 = txtPhone1.Text.Trim();
                st.adress = txtAddress.Text.Trim();
                st.email = txtEmail.Text.Trim();
                st.image_path = imgStaff.Src.Replace("~/images/image_data/", "");
                st.study_level = ddlStudyLevel.SelectedValue;
                st.Status = 1;
                st.login_user = user.username;
                st.role_id = int.Parse(ddlRoles.SelectedValue);

                // contact information 
                st.ref_first_name = txtParentFirstName.Text.Trim();
                st.ref_last_name = txtParentLastName.Text.Trim();
                st.ref_sex = ddlParentSex.SelectedValue;
                st.ref_phone = txtParentPhone.Text.Trim();
                st.ref_adress = txtParentAdress.Text.Trim();
                st.ref_relationship = ddlParentRelationship.SelectedValue;


                if (Session["staff_code"] == null)     // add new
                {
                    string code = "PS-" + Universal.getUniversalSequence(sqlStafftNextval).ToString();
                    st.id = code;

                    // Salary 
                    Salary s = new Salary();
                    s.staff_code = code;
                    s.amount = double.Parse(txtSalary.Value.ToString());
                    s.status = 1;
                    s.login_user_id = user.id;
                    s.tax_id = int.Parse(ddlTax.SelectedValue.ToString());

                    // position
                    List<Staff> listPosition = new List<Staff>();
                    foreach (RadComboBoxItem item in ddlPosition.Items)
                    {
                        if (item.Checked)
                        {
                            Staff _st = new Staff();
                            _st.id = code;
                            _st.position_id = int.Parse(item.Value);
                            listPosition.Add(_st);
                        }
                    }

                    // add new teacher
                    Staff.addPersonal(st);

                    // attach staff to position
                    Staff.attachStaffToPosition(listPosition);
                    
                    // salary configuration
                    Salary.AddStaffToTaxGroup(s);
                    Salary.InsertStaffSalaryInfo(s);

                    // check if staff already has a login_user account
                    List<Users> listUserInfo = Users.getListUsersByCode(st.id);
                    if (listUserInfo == null || listUserInfo.Count <= 0)
                    {
                        // get default system password from config file
                        string defaultPasswd = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_DEFAULT_PASSWD"];
                        // create login user account for staff
                        Users _userInfo = new Users();
                        _userInfo.username = st.id;
                        _userInfo.password = Hash.EncodePasswordSH1(defaultPasswd); ; // default passwd
                        _userInfo.locked = 0; // 0. unlocked    /   1. locked
                        _userInfo.expiry_date = DateTime.Now.AddYears(1); // 1 year after that password will be expired
                        _userInfo.role_id = st.role_id;
                        // add new user
                        Users.addUser(_userInfo);
                    }

                    // clear fields
                    emptyFields();

                    msgContent = "Sauvegarder avec succès !!! \\rCode personnel : " + code;
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
                }
                else    // update new 
                {
                    string code = Session["staff_code"].ToString();

                    // Salary 
                    Salary s = new Salary();
                    s.staff_code = code;
                    s.amount = double.Parse(txtSalary.Value.ToString());
                    s.status = 1;
                    s.login_user_id = user.id;
                    s.tax_id = int.Parse(ddlTax.SelectedValue.ToString());

                    // position
                    List<Staff> listPosition = new List<Staff>();
                    foreach (RadComboBoxItem item in ddlPosition.Items)
                    {
                        if (item.Checked)
                        {
                            Staff _st = new Staff();
                            _st.id = code;
                            _st.position_id = int.Parse(item.Value);
                            listPosition.Add(_st);
                        }
                    }                    

                    // update staff
                    st.id = code;
                    Staff.updateStaff(st);

                    // attach staff to position
                    Staff.attachStaffToPosition(listPosition);

                    // salary configuration
                    Salary.AddStaffToTaxGroup(s);
                    Salary.InsertStaffSalaryInfo(s);

                    // attach documents
                    if (Session["list_documents_attach"] != null)
                    {
                        listDocumentsAttach = Session["list_documents_attach"] as List<StaffDocuments>;
                        StaffDocuments.uploadStaffDocuments(listDocumentsAttach, st.id);
                    }
                    // clear fields
                    emptyFields();
                    Session.Remove("staff_code");
                    Session["staff_code"] = null;

                    // go back to search page after update
                    Response.Redirect("SearchPersonal.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        emptyFields();

        if (Session["staff_code"] != null)
        {
            Session.Remove("staff_code");
            Session["staff_code"] = null;
            //
            Response.Redirect("SearchPersonal.aspx");
        }
    }

    private void loadDocumentCategories()
    {
        List<StaffDocuments> listResult = StaffDocuments.getListDocumentType();
        ddlDocumentCategory.DataValueField = "description";
        ddlDocumentCategory.DataTextField = "description";
        ddlDocumentCategory.DataSource = listResult;
        ddlDocumentCategory.DataBind();
        //
        ddlDocumentCategory.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlDocumentCategory.SelectedValue = "-1";
    }

    protected void btnAttachDocuments_ServerClick(object sender, EventArgs e)
    {
        if (ddlDocumentCategory.SelectedValue == "-1")
        {
            MessageAlert.RadAlert("Erreur : Veuillez saisir la description", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            string _code = lblStaffCode.Text;
            //get new document path
            string FolderPath = Server.MapPath("~/Uploaded_Documents/" + _code);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploaded_Documents/" + _code));
            }

            if (!documentsAttachFile.HasFile) // check fileUpload control for files
            {
                MessageAlert.RadAlert("Erreur : attacher un fichier PDF", 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                string file_extension = System.IO.Path.GetExtension(documentsAttachFile.FileName); // get file extension
                if (file_extension.ToLower() != ".pdf") //check file extension
                {
                    MessageAlert.RadAlert("Erreur : Seul les fichiers PDF peuvend être attachés", 300, 200, "Information", null, "../images/error.png");
                }
                else
                {
                    if (Session["list_documents_attach"] != null)
                    {
                        listDocumentsAttach = Session["list_documents_attach"] as List<StaffDocuments>;
                    }

                    HttpPostedFile userPostedFile = documentsAttachFile.PostedFile;
                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                            string fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(userPostedFile.FileName);
                            string filepath = "~/Uploaded_Documents/" + _code + "/" + fileName;
                            userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                            StaffDocuments doc = new StaffDocuments();
                            doc.staff_id = _code;
                            doc.document_name = ddlDocumentCategory.SelectedValue;
                            doc.upload_time = DateTime.Now;
                            listDocumentsAttach.Add(doc);
                            Session["list_documents_attach"] = listDocumentsAttach;
                            // add to gridview
                            gridAttachDocuments.Rebind();
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
            }
        }
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        emptyFields();
        if (Session["staff_code"] != null)
        {
            Response.Redirect("SearchPersonal.aspx");
        }
    }

    private void emptyFields()
    {
        //empty Staff form
        txtFirstName.Text = "";
        ddlSex.SelectedIndex = 0;
        txtBirthPlace.Text = "";
        txtPhone1.Text = "";
        txtLastName.Text = "";
        ddlMaritalStatus.SelectedIndex = 0;
        radBirthDate.Clear();
        txtAddress.Text = "";
        ddlPosition.SelectedIndex = 0;
        txtEmail.Text = "";
        txtCardId.Text = "";
        ddlStudyLevel.SelectedValue = "-1";
        ddlRoles.SelectedIndex = 0;
        // clear image
        imgStaff.Attributes.Add("src", "../images/image_data/Default.png");

        // clear contact fields
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        txtParentPhone.Text = "";
        txtParentAdress.Text = "";
        ddlParentRelationship.SelectedValue = "-1";

        // salary config
        txtSalary.Value = null;
        ddlTax.SelectedValue = "-1";
        // clear position
        foreach (RadComboBoxItem item in ddlPosition.Items)
        {
            item.Checked = false;
        }


        // kill sessions
        Session.Remove("list_documents_attach");
        Session["list_documents_attach"] = null;
        listDocumentsAttach = null;

        // reload documents grid
        gridAttachDocuments.Rebind();
    }

    private bool validateFields()
    {
        bool result = true;

        if (txtFirstName.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtLastName.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtBirthPlace.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (radBirthDate.SelectedDate >= DateTime.Now || radBirthDate.IsEmpty)
        {
            result = false;
        }
        else if (txtAddress.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (ddlSex.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlMaritalStatus.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlPosition.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (txtParentFirstName.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentLastName.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (ddlParentSex.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (txtParentPhone.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentAdress.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (ddlParentRelationship.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlRoles.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlStudyLevel.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlTax.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (txtSalary.Value == null)
        {
            result = false;
        }
        else
        {
            result = true;
        }


        return result;
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        try
        {
            /**

            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {

                picturebphoto.Image = new Bitmap(open.FileName);
                picturebphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


            }
         **/

        }

        catch (Exception)
        {

            //  throw new ApplicationException("Failed loading image");

        }
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ClassRoom classroom = new ClassRoom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    protected void txtRefIdCard_OnTextChanged(object sender, EventArgs e)
    {

    }

    protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
    {

        /*MaskedTextBoxRequiredFieldValidator.EnableClientScript = CheckBox1.Checked;

        DateInputRangeValidator.EnableClientScript = CheckBox1.Checked;

        PickerRequiredFieldValidator.EnableClientScript = CheckBox1.Checked;

        TextBoxRequiredFieldValidator.EnableClientScript = CheckBox1.Checked;

        NumercTextBoxRequiredFieldValidator.EnableClientScript = CheckBox1.Checked;

        MaskedTextBoxRegularExpressionValidator.EnableClientScript = CheckBox1.Checked;

        NumericTextBoxRangeValidator.EnableClientScript = CheckBox1.Checked;*/

        //Requiredfieldvalidator1.EnableClientScript = CheckBox1.Checked;


    }

    protected void gridListReference_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "30px");
            e.Row.Style.Add("vertical-align", "bottom");
        }
    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        if (imageUploader.HasFile) // check fileUpload control for files
        {
            string file_extension = System.IO.Path.GetExtension(imageUploader.FileName); // get file extension
            if (file_extension.ToLower() == ".jpeg"
                || file_extension.ToLower() == ".jpg"
                || file_extension.ToLower() == ".png"
                || file_extension.ToLower() == ".bitmap"
                || file_extension.ToLower() == ".jfif") //check file extension
                try
                {
                    if (imageUploader.PostedFile.ContentLength > 0)
                    {
                        string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssff") + "_" + Path.GetFileName(imageUploader.PostedFile.FileName);
                        string filePaths = "~/images/image_data/" + fileName;
                        imageUploader.SaveAs(Server.MapPath(filePaths)); //save file to folder
                                                                         //imageKeeper.ImageUrl = filePaths;
                        imgStaff.Attributes.Add("src", filePaths);
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

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void radBirthDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        RadDatePicker dpicker = sender as RadDatePicker;
        if (dpicker.DateInput.IsEmpty || dpicker.SelectedDate > DateTime.Now)
        {
            MessageAlert.RadAlert("Erreur : Date de naissance invalide !", 350, 200, "Information", null);
            radBirthDate.SelectedDate = null;
        }
        //else
        //{
        //    DateTime _birthDate = DateTime.Parse(dpicker.SelectedDate.Value.ToString());
        //    int days = (DateTime.Now - _birthDate).Days;
        //    if (days < 365) // check if student is at least 1 year old
        //    {
        //        MessageAlert.RadAlert("Erreur : Date de naissance invalide. Personnel doit avoir un (1) an ou plus !", 350, 200, "Information", null);
        //        radBirthDate.SelectedDate = null;
        //    }
        //}

    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (validateParentFields())
        //{
        //get external reference information
        //parent.id_card = txtParentIdCard.Text.Trim();
        //parent.first_name = txtParentFirstName.Text.Trim();
        //parent.last_name = txtParentLastName.Text.Trim();
        //parent.marital_status = ddlParentMaritalStatus.SelectedValue;

        //switch (parent.marital_status)
        //{
        //    case "C": parent.marital_status_definition = "Célibataire"; break;
        //    case "M": parent.marital_status_definition = "Marié(e)"; break;
        //    case "D": parent.marital_status_definition = "Divorcé(e)"; break;
        //    case "V": parent.marital_status_definition = "Veuf(ve)"; break;
        //    case "U": parent.marital_status_definition = "Union Libre"; break;
        //}
        //parent.sex = ddlParentSex.SelectedValue;
        //parent.phone = txtParentPhone.Text.Trim();
        //parent.adress = txtParentAdress.Text.Trim();
        //parent.image_path = Path.GetFileName(imageKeeperReference.ImageUrl).Replace("~/images/image_data/", "");
        //parent.relationship = ddlParentRelationship.SelectedValue;
        //listParentAttach.Add(parent);
        //}
    }

    protected void gridAttachDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["list_documents_attach"] != null)
        {
            listDocumentsAttach = Session["list_documents_attach"] as List<StaffDocuments>;
        }
        gridAttachDocuments.DataSource = listDocumentsAttach;
    }

    protected void gridAttachDocuments_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridAttachDocuments_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridAttachDocuments.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnRemoveDocuments_ServerClick(object sender, EventArgs e)
    {

    }
    

}