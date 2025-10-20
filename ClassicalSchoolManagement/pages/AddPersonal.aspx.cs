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
            //loadPositions();
            loadDocumentTypes();
            loadListPosition();
           

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

    //private void loadListTaxGroup()
    //{
    //    // get list academic year
    //    List<Salary> listResult = Salary.getListTax();

    //    if (listResult != null && listResult.Count > 0)
    //    {
    //        // fill the ddl now
    //        ddlTax.DataValueField = "id";
    //        ddlTax.DataTextField = "group_name";
    //        ddlTax.DataSource = listResult;
    //        ddlTax.DataBind();
    //    }
    //    //
    //    ddlTax.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
    //    ddlTax.SelectedValue = "-1";
    //}

    private void loadListPosition()
    {
        try
        {
            List<Staff> listResult = Staff.getListPositions();
            if (listResult != null && listResult.Count > 0)
            {
                ddlPosition.DataSource = listResult;
                ddlPosition.DataValueField = "id";
                ddlPosition.DataTextField = "name";
                ddlPosition.DataBind();
            }
            //
            ddlPosition.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
            ddlPosition.SelectedValue = "-1";
        }
        catch { }
    }

    //private void loadPositions()
    //{
    //    try
    //    {
    //        ddlPosition.Items.Clear();
    //        // get list all academic  year
    //        List<Staff> listResult = Staff.getListPositions();

    //        if (listResult != null && listResult.Count > 0)
    //        {
    //            ddlPosition.DataValueField = "id";
    //            ddlPosition.DataTextField = "name";
    //            ddlPosition.DataSource = listResult;
    //            ddlPosition.DataBind();
    //        }
    //    }
    //    catch (Exception ex) { }
    //}

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
            //lblStaffCode.Text = st.id;
            txtFirstName.Text = st.first_name;
            txtLastName.Text = st.last_name;
            ddlSex.SelectedValue = st.sex;
            txtBirthPlace.Text = st.birth_place;
            radBirthDate.SelectedDate = st.birth_date;
            txtPhone1.Text = st.phone;
            txtAddress.Text = st.address;
            txtCardId.Text = st.id_card;
            txtEmail.Text = st.email;
            ddlStudyLevel.SelectedValue = st.study_level;
            ddlMaritalStatus.SelectedValue = st.marital_status;
            //  ddlPosition.SelectedValue = st.position_id.ToString();
            //
            ddlPosition.SelectedValue = st.position_id.ToString();

            string imagePaths = "~/images/image_data/" + st.image_path;
            imgStaff.Attributes.Add("src", imagePaths);


            // contact information 


            // documents
            Session["list_documents_attach"] = Documents.getListDocumentsByStaffCode(staffCode);
            gridAttachDocuments.Rebind();
            //pnlDocuments.Visible = true;

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
                st.phone = txtPhone1.Text.Trim();
                st.address = txtAddress.Text.Trim();
                st.email = txtEmail.Text.Trim();
                st.image_path = imgStaff.Src.Replace("~/images/image_data/", "");
                st.study_level = ddlStudyLevel.SelectedValue;
                st.status = Convert.ToInt32(Staff.STATUS.ACTIVE);
                st.login_user_id = user.id;
                st.position_id = int.Parse(ddlPosition.SelectedValue);
                // parent information
                StudentContactPerson p = new StudentContactPerson();
                p.first_name = txtParentFirstName.Text.Trim();
                p.last_name = txtParentLastName.Text.Trim();
                p.sex = ddlParentSex.SelectedValue;
                p.birth_place = txtParentBirthPlace.Text.Trim();
                p.birth_date = radParentBirthDate.SelectedDate.Value;
                p.occupation = txtParentOccupation.Text.Trim();
                p.marital_status = ddlParentMaritalStatus.SelectedValue;
                p.id_card = txtParentIdCard.Text.Trim();
                p.phone = txtParentPhone.Text.Trim();
                p.email = txtParentEmail.Text.Trim();
                p.address = txtParentAddress.Text.Trim();
                p.job_title = txtParentJobTitle.Text.Trim();
                p.relationship = ddlParentRelationship.SelectedValue;
                p.image_path = "";



                if (Session["staff_code"] == null)     // add new
                {
                    string staff_code = "PS-" + Universal.getUniversalSequence(sqlStafftNextval).ToString();
                    st.id = "PS-" + Universal.getUniversalSequence(sqlStafftNextval).ToString();

                    string code_staff = st.id;

                    Staff.addPersonal(st);



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
                        _userInfo.role_id = 2;   // Administrator
                        // add new user
                        Users.addUser(_userInfo);
                    }

                    // clear fields
                    emptyFields();

                    //msgContent = "Sauvegarder avec succès !!! \\rCode personnel : ";
                    //MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
                    MessageAlert.RadAlert("Personnel sauvegarder avec succès ! Code personnel  :" + code_staff, 300, 200, "Information", null, "../images/success_check.png");

                }


                else    // update new 
                {
                    string code = Session["staff_code"].ToString();


                    st.id = code;
                    Staff.updateStaff(st);



                    // attach documents
                    if (Session["list_documents_attach"] != null)
                    {
                        listDocumentsAttach = Session["list_documents_attach"] as List<StaffDocuments>;
                        //StaffDocuments.uploadSudentDocuments(listDocumentsAttach,code_staff);
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

    private void loadDocumentTypes()
    {
        List<StaffDocuments> listResult = StaffDocuments.getListDocumentType();
        ddlDocumentType.DataValueField = "id";
        ddlDocumentType.DataTextField = "description";
        ddlDocumentType.DataSource = listResult;
        ddlDocumentType.DataBind();
        ddlDocumentType.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlDocumentType.SelectedValue = "-1";
    }


    protected void btnAttachDocuments_ServerClick(object sender, EventArgs e)
    {
        if (ddlDocumentType.SelectedValue == "-1")
        {
            MessageAlert.RadAlert("Erreur : Veuillez saisir la description", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            //get new document path
            string FolderPath = Server.MapPath("~/staff_uploaded_documents/");

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(Server.MapPath("~/staff_uploaded_documents/"));
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
                            string fileName = "sta_"+DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(userPostedFile.FileName);
                            string filepath = "~/staff_uploaded_documents/" + fileName;
                            userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                            StaffDocuments doc = new StaffDocuments();

                            doc.document_name = fileName;
                            doc.document_type_def = ddlDocumentType.SelectedItem.Text;
                            doc.document_type_id = int.Parse(ddlDocumentType.SelectedValue);
                            doc.upload_time = DateTime.Now;
                            listDocumentsAttach.Add(doc);
                            Session["list_documents_attach"] = listDocumentsAttach;
                            // add to gridview
                            gridAttachDocuments.Rebind();
                            //
                            ddlDocumentType.SelectedValue = "-1";
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
        //ddlPosition.SelectedIndex = 0;
        txtEmail.Text = "";
        txtCardId.Text = "";
        ddlStudyLevel.SelectedValue = "-1";
        ddlPosition.SelectedIndex = 0;
        // clear image
        imgStaff.Attributes.Add("src", "../images/image_data/Default.png");

        // clear contact fields
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        txtParentPhone.Text = "";
       // txtParentAdress.Text = "";
        ddlParentRelationship.SelectedValue = "-1";

      

        // kill sessions
        Session.Remove("list_documents_attach");
        Session["list_documents_attach"] = null;
        listDocumentsAttach = new List<StaffDocuments>();

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
        //else if (ddlPosition.SelectedValue.ToString() == "-1")
        //{
        //    result = false;
        //}
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
        //else if (txtParentAdress.Text.Trim().Length <= 0)
        //{
        //    result = false;
        //}
        else if (ddlParentRelationship.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlPosition.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlStudyLevel.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        //else if (ddlTax.SelectedValue.ToString() == "-1")
        //{
        //    result = false;
        //}
        //else if (txtSalary.Value == null)
        //{
        //    result = false;
        //}
        else
        {
            result = true;
        }


        return result;
    }

   /* protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        try
        {
            

            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {

                picturebphoto.Image = new Bitmap(open.FileName);
                picturebphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


            }
         

        }

        catch (Exception)
        {

            //  throw new ApplicationException("Failed loading image");

        }
    }*/

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