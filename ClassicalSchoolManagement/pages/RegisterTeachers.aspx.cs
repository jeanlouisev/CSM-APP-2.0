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



public partial class RegisterTeacher : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.HR;
    string msgContent = "";

    List<Documents> listDocumentsAttach = new List<Documents>();

    string sqlTeacherNextval = @"select nextval_teacher('codeSeq') as student_nextval";

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;
        
        try
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Error.aspx");
                }
                else
                {
                    Users user = Session["user"] as Users;
                    imageUploader.Attributes["onchange"] = "UploadFile(this)";
                    emptyFields();
                    loadDocumentCategories();
                    loadListTaxGroup();
                    txtFirstName.Focus();

                    if (Session["teacher_id"] == null)
                    {
                        // kill all unwanted sessions
                        Session.Remove("list_documents_attach");
                    }
                    else
                    {
                        string teacherId = Session["teacher_id"].ToString();
                        loadTeacherPreviousInformation(teacherId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    // teacher basic information
        //    txtFirstName.ReadOnly = true;
        //    txtLastName.ReadOnly = true;
        //    txtBirthPlace.ReadOnly = true;
        //    radBirthDate.Enabled = false;
        //    txtPhone1.ReadOnly = true;
        //    txtAddress.ReadOnly = true;
        //    //chkNif.Enabled = false;
        //    //chkCin.Enabled = false;
        //    txtCardId.ReadOnly = true;
        //    txtEmail.ReadOnly = true;
        //    ddlSex.Enabled = false;
        //    ddlMaritalStatus.Enabled = false;
        //    ddlStudyLevel.Enabled = false;
        //    imageUploader.Enabled = false;

        //    // reference information
        //    txtParentFirstName.ReadOnly = true;
        //    txtParentLastName.ReadOnly = true;
        //    txtParentPhone.ReadOnly = true;
        //    txtParentAdress.ReadOnly = true;
        //    ddlParentSex.Enabled = false;
        //    ddlParentRelationship.Enabled = false;
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur :" + ex.Message);
        //}
    }

    // delete
    private void disableDeleteOption()
    {
        try
        {

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!validateFields())
        {
            msgContent = "Erreur : tous les champs ayant un asterix (*) sont obligatoires !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            Users user = Session["user"] as Users;
            //
            Teacher t = new Teacher();
            t.first_name = txtFirstName.Text.Trim();
            t.last_name = txtLastName.Text.Trim();
            t.sex = ddlSex.SelectedValue;
            t.marital_status = ddlMaritalStatus.SelectedValue;
            t.id_card = txtCardId.Text.Trim();
            t.birth_date = radBirthDate.SelectedDate.Value;
            t.birth_place = txtBirthPlace.Text.Trim();
            t.phone1 = txtPhone1.Text.Trim();
            t.adress = txtAddress.Text.Trim();
            t.email = txtEmail.Text.Trim();
            t.image_path = imgTeacher.Src.Replace("~/images/image_data/", "");
            t.study_level = ddlStudyLevel.SelectedValue;
            t.status = (int)Teacher.ACCOUNT_STATUS.Active;
            t.login_user_id = user.id;

            // contact information 
            t.ref_first_name = txtParentFirstName.Text.Trim();
            t.ref_last_name = txtParentLastName.Text.Trim();
            t.ref_sex = ddlParentSex.SelectedValue;
            t.ref_phone = txtParentPhone.Text.Trim();
            t.ref_adress = txtParentAdress.Text.Trim();
            t.ref_relationship = ddlParentRelationship.SelectedValue;



            if (Session["teacher_id"] == null)     // add new
            {
                string code = "PRO-" + Universal.getUniversalSequence(sqlTeacherNextval).ToString();
                t.id = code;

                // add new teacher
                Teacher.addTeacher(t);

                // salary configuration
                Salary sal = new Salary();
                sal.teacher_id = code;
                sal.tax_id = int.Parse(ddlTax.SelectedValue);
                Salary.AddTeacherToTaxGroup(sal);

                // clear fields
                emptyFields();

                msgContent = "Sauvegarder avec succès !!! \\rCode personnel : " + code;
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
            }
            else    // update new 
            {
                string code = Session["teacher_id"].ToString();
                // update staff
                t.id = code;
                Teacher.updateTeacher(t);

                // salary configuration
                Salary sal = new Salary();
                sal.teacher_id = code;
                sal.tax_id = int.Parse(ddlTax.SelectedValue);
                Salary.AddTeacherToTaxGroup(sal);

                // attach documents
                if (Session["list_documents_attach"] != null)
                {
                    listDocumentsAttach = Session["list_documents_attach"] as List<Documents>;
                    Documents.uploadDocument(listDocumentsAttach);
                }

                // clear fields
                emptyFields();
                Session.Remove("teacher_id");
                Session["teacher_id"] = null;

                // go back to search page after update
                Response.Redirect("SearchTeachers.aspx");
            }
        }
    }

    private bool validateParentFields()
    {
        bool result = true;
        if (txtParentFirstName.Text.Trim().Length <= 0
            && txtParentLastName.Text.Trim().Length <= 0
            && txtParentPhone.Text.Trim().Length <= 0
            && txtParentAdress.Text.Trim().Length <= 0
            && ddlParentSex.SelectedValue.ToString() == "-1"
            && ddlParentRelationship.SelectedValue.ToString() == "-1"
            )
        {
            result = false;
        }

        return result;
    }

    private void loadTeacherPreviousInformation(string teacherCode)
    {
        try
        {
            Teacher t = Teacher.getTeacherInfoById(teacherCode);
            if (t != null)
            {
                lblTeacherCode.Text = t.id;
                txtFirstName.Text = t.first_name;
                txtLastName.Text = t.last_name;
                ddlSex.SelectedValue = t.sex;
                txtBirthPlace.Text = t.birth_place;
                radBirthDate.SelectedDate = t.birth_date;
                txtPhone1.Text = t.phone1;
                txtAddress.Text = t.adress;
                txtCardId.Text = t.id_card;
                txtEmail.Text = t.email;
                ddlStudyLevel.SelectedValue = t.study_level;
                ddlMaritalStatus.SelectedValue = t.marital_status;

                string imagePaths = "~/images/image_data/" + t.image_path;
                imgTeacher.Attributes.Add("src", imagePaths);


                // contact information 
                txtParentFirstName.Text = t.ref_first_name;
                txtParentLastName.Text = t.ref_last_name;
                ddlParentSex.SelectedValue = t.ref_sex;
                txtParentPhone.Text = t.ref_phone;
                txtParentAdress.Text = t.ref_adress;
                ddlParentRelationship.SelectedValue = t.ref_relationship;

                // salary
                ddlTax.SelectedValue = t.tax_id.ToString();


                // documents
                Session["list_documents_attach"] = Documents.getListDocumentsByStaffCode(t.id);
                gridAttachDocuments.Rebind();
                pnlDocuments.Visible = true;

            }
        }
        catch(Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }

    }

    private void loadDocumentCategories()
    {
        List<Documents> listResult = Documents.getListDocumentCategory();
        ddlDocumentCategory.DataValueField = "description";
        ddlDocumentCategory.DataTextField = "description";
        ddlDocumentCategory.DataSource = listResult;
        ddlDocumentCategory.DataBind();
        //
        ddlDocumentCategory.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlDocumentCategory.SelectedValue = "-1";
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


    protected void btnClear_Click(object sender, EventArgs e)
    {
        emptyFields();
        if (Session["teacher_id"] != null)
        {
            Response.Redirect("SearchTeachers.aspx");
        }
    }

    private void emptyFields()
    {
        //empty Staff form
        //lblResult.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        ddlSex.SelectedIndex = 0;
        ddlMaritalStatus.SelectedIndex = 0;
        txtCardId.Text = "";
        ddlStudyLevel.SelectedIndex = 0;
        radBirthDate.Clear();
        txtBirthPlace.Text = "";
        txtAddress.Text = "";
        txtPhone1.Text = "";
        txtEmail.Text = "";
        //txtCode.Text = "PRO-" + Universal.getUniversalSequence(sqlTeacherNextval).ToString();
        imgTeacher.Attributes.Add("src", "../images/image_data/Default.png");


        //empty parent form
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        txtParentPhone.Text = "";
        txtParentAdress.Text = "";
        ddlParentRelationship.SelectedValue = "-1";

        //put the cursor over the first element of the form
        txtFirstName.Focus();

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
        else if (ddlStudyLevel.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (ddlTax.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else
        {
            result = true;
        }


        return result;
    }

    private bool validateReferenceFields()
    {
        bool result = true;
        if (txtParentFirstName.Text.Trim().Length <= 0
            && txtParentFirstName.Text.Trim().ToString() == "")
        {
            MessBox.Show("Erreur : Veuillez saisir le nom de la personne a contacter.");
            txtParentFirstName.Focus();
            result = false;
        }
        else if (txtParentLastName.Text.Trim().Length <= 0
         && txtParentLastName.Text.Trim().ToString() == "")
        {
            MessBox.Show("Erreur : Veuillez saisir le prenom de la personne a contacter.");
            txtParentLastName.Focus();
            result = false;
        }
        else if (txtParentPhone.Text.Trim().Length <= 0)
        {
            MessBox.Show("Erreur : Veuillez saisir le telephone de la personne a contacter.");
            txtParentPhone.Focus();
            result = false;
        }
        else if (txtParentAdress.Text.Trim().Length <= 0
                             && txtParentAdress.Text.Trim().ToString() == "")
        {
            MessBox.Show("Erreur : Veuillez saisir l\'adresse de la personne a contacter.");
            txtParentAdress.Focus();
            result = false;
        }
        else if (ddlParentSex.SelectedValue.ToString() == "-1"
                       && ddlParentSex.SelectedText.ToString() == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Veuillez selectionner le sexe de la personne a contacter.");
            ddlParentSex.Focus();
            result = false;
        }
        else if (ddlParentRelationship.SelectedValue.ToString() == "-1"
                   && ddlParentRelationship.SelectedText.ToString() == "--Tout Sélectionner--")
        {
            MessBox.Show("Erreur : Veuillez selectionner la relation de la personne a contacter.");
            ddlParentRelationship.Focus();
            result = false;
        }
        return result;
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ClassRoom classroom = new ClassRoom();
        classroom.id = -1;
        classroom.name = "--Tout Sélectionner--";
        listClassroom.Add(classroom);
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.SelectedValue = "-1";
    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        if (imageUploader.HasFile) // check fileUpload control for files
        {
            string file_extension = System.IO.Path.GetExtension(imageUploader.FileName); // get file extension
            if (file_extension.ToLower() == ".jpeg"
                || file_extension.ToLower() == ".jpg"
                || file_extension.ToLower() == ".png"
                || file_extension.ToLower() == ".bitmap") //check file extension
            {
                try
                {
                    if (imageUploader.PostedFile.ContentLength > 0)
                    {
                        string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssff") + "_" + Path.GetFileName(imageUploader.PostedFile.FileName);
                        string filePaths = "~/images/image_data/" + fileName;
                        imageUploader.SaveAs(Server.MapPath(filePaths)); //save file to folder
                        imgTeacher.Attributes.Add("src", filePaths);
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
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

    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        lblEmailError.Visible = false;
        RadTextBox email = sender as RadTextBox;
        if (email.Text.Trim().Length > 0)
        {
            bool checkEmail = Universal.IsValidEmailAddress(email.Text.Trim());
            if (!checkEmail)
            {
                lblEmailError.Visible = true;
                email.Text = string.Empty;
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        // do nothing
    }

    protected void btnAttachDocuments_ServerClick(object sender, EventArgs e)
    {
        if (ddlDocumentCategory.SelectedValue == "-1")
        {
            MessageAlert.RadAlert("Erreur : Veuillez saisir la description", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            //get new document path
            String teacherCode = lblTeacherCode.Text;
            string FolderPath = Server.MapPath("~/Uploaded_Documents/" + teacherCode);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploaded_Documents/" + teacherCode));
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
                        listDocumentsAttach = Session["list_documents_attach"] as List<Documents>;
                    }

                    HttpPostedFile userPostedFile = documentsAttachFile.PostedFile;
                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                            string fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(userPostedFile.FileName);
                            string filepath = "~/Uploaded_Documents/" + teacherCode + "/" + fileName;
                            userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                            Documents doc = new Documents();
                            doc.staff_code = teacherCode;
                            doc.document_path = filepath;
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

    protected void gridAttachDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

        if (Session["list_documents_attach"] != null)
        {
            listDocumentsAttach = Session["list_documents_attach"] as List<Documents>;
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
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        int index = dataItem.RowIndex;
        HiddenField hiddenRandCode = (HiddenField)dataItem.FindControl("hiddenRandCode");
        string randCode = hiddenRandCode.Value;
        //string stCode = txtCode.Text.Trim();
        //
        if (Session["list_documents_attach"] != null)
        {
            listDocumentsAttach = Session["list_documents_attach"] as List<Documents>;
        }

        List<Documents> listTemp = new List<Documents>();
        //foreach (Documents doc in listDocumentsAttach)
        //{
        //    if (doc.rand_code != randCode)
        //    {
        //        listTemp.Add(doc);
        //    }
        //}
        // udpate old list to temporary list
        listDocumentsAttach = new List<Documents>();
        listDocumentsAttach = listTemp;
        Session["list_documents_attach"] = listTemp;
        // refresh grid
        gridAttachDocuments.Rebind();
    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        emptyFields();

        if (Session["teacher_id"] != null)
        {
            Session.Remove("teacher_id");
            Session["teacher_id"] = null;
            //
            Response.Redirect("SearchTeachers.aspx");
        }
    }
}