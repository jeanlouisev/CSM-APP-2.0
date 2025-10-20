using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web;
using Telerik.Web.UI;
using Utilities;
using static Telerik.Web.UI.ComboBox.ComboBoxStyles;


public partial class RegisterStudents : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.STUDENT;

    List<StudentDocuments> listDocumentsAttach = new List<StudentDocuments>();
    string sqlStudentNextval = @"select nextval_student('codeSeq') as student_nextval";
    string sqlContactPersonNextval = @"select nextval_student_contact_person('codeSeq') as contact_person_nextval";
    string msgContent = "";



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
            // check academic year configuration
            Settings acc = Settings.getCurrentAcademicYear();
            if (acc == null)
            {
                msgContent = "Erreur : Veuillez configurer l\\'année académique, Veuillez signaler l\\'administrateur du system. !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                // get current academic year
                txtAcademicYear.Text = acc.years.ToString();
                hiddenAcademicYearId.Value = acc.id.ToString();
                //
                loadActiveClassroom(ddlClassroom); 
                loadDocumentTypes();
                loadRegistrationFees();

                imageUploader.Attributes["onchange"] = "UploadFile(this)";
                //
                if (Session["student_id"] == null)
                {
                    // kill all unwanted sessions
                    Session.Remove("list_documents_attach");
                    //txtCode.Text = "EL-" + Universal.getUniversalSequence(sqlStudentNextval).ToString();
                }
                else
                {
                    string studentCode = Session["student_id"].ToString();
                    loadPreviousStudentInformation(studentCode);
                }
            }
        }
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

    private void loadPreviousStudentInformation(string studentCode)
    {
        try
        {
            //    List<Student> listStudentInfo = Student.getListStudentByCode(studentCode);
            //    Student st = listStudentInfo[0];
            //    //get student data
            //    //txtCode.Text = st.id;
            //    //txtCode.Visible = true;
            //    txtFirstName.Text = st.first_name;
            //    txtLastName.Text = st.last_name;
            //    ddlSex.SelectedValue = st.sex_code;
            //    txtCardId.Text = st.id_card;
            //    txtBirthPlace.Text = st.birth_place;
            //    radBirthDate.SelectedDate = st.birth_date;
            //    //txtphone.Text = st.phone;
            //    txtAddress.Text = st.address;
            //    txtEmail.Text = st.email;
            //    ddlClassroom.SelectedValue = st.classroom_id.ToString();
            //    ddlClassroom.Enabled = false;
            //    ddlVacation.SelectedValue = st.vacation_code;
            //    string imagePaths = "~/images/image_data/" + st.image_path;
            //    imgStudent.Attributes.Add("src", imagePaths);

            //    // parent information
            //    //StudentContactPerson p = new StudentContactPerson();
            //    //txtParentFirstName.Text = st.ref_first_name;
            //    //txtParentLastName.Text = st.ref_last_name;
            //    //txtParentPhone.Text = st.ref_phone;
            //    //txtParentAddress.Text = st.ref_adress;
            //    //ddlParentSex.SelectedValue = st.ref_sex;
            //    //ddlParentRelationship.SelectedValue = st.ref_relationship;

            //    //get list of available documents
            //    Session["list_documents_attach"] = Documents.getListDocumentsByStaffCode(studentCode);
            //    gridAttachDocuments.Rebind();

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
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

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        //
        dropDownList.Items.Insert(0, new DropDownListItem("-- Sélectionner --", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    private void loadDocumentTypes()
    {
        List<StudentDocuments> listResult = StudentDocuments.getListDocumentType();
        ddlDocumentType.DataValueField = "id";
        ddlDocumentType.DataTextField = "description";
        ddlDocumentType.DataSource = listResult;
        ddlDocumentType.DataBind();
        //
        ddlDocumentType.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlDocumentType.SelectedValue = "-1";
    }

    private void loadRegistrationFees()
    {
        StudentContribution sc = StudentContribution.getRegistrationFee();
        if(sc != null)
        {
            txtContributionPaymentType.Text = sc.description;
            hiddenContributionTypeId.Value = sc.id.ToString();
            txtContributionPaidAmount.Value = sc.price;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validateFields())  // check all required fields in the form
            {
                msgContent = "Erreur : tous les champs ayant un asterix (*) sont obligatoires !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                Users user = Session["user"] as Users;

                // student information
                Student st = new Student();
                st.first_name = txtFirstName.Text.Trim().Length <= 0 ? "" : txtFirstName.Text.Trim();
                st.sex = ddlSex.SelectedValue;
                st.id_card = txtCardId.Text.Trim().Length <= 0 ? "" : txtCardId.Text.Trim();
                st.birth_place = txtBirthPlace.Text.Trim().Length <= 0 ? "" : txtBirthPlace.Text.Trim();
                st.phone = txtPhone.Text.Trim().Length <= 0 ? "" : txtPhone.Text.Trim();
                st.last_name = txtLastName.Text.Trim().Length <= 0 ? "" : txtLastName.Text.Trim();
                st.birth_date = radBirthDate.SelectedDate.Value;
                st.address = txtAddress.Text.Trim().Length <= 0 ? "" : txtAddress.Text.Trim();
                st.vacation = ddlVacation.SelectedValue;
                st.email = txtEmail.Text.Trim().Length <= 0 ? "" : txtEmail.Text.Trim().ToLower();
                st.image_path = imgStudent.Src.Replace("~/images/student_profile/", "");
                st.status = Convert.ToInt32(Student.StudenStatus.Active);
                st.login_user_id = user.id;
                st.classroom_id = int.Parse(ddlClassroom.SelectedValue);
                st.academic_year_id = int.Parse(hiddenAcademicYearId.Value);

                // parent information
                StudentContactPerson cp = new StudentContactPerson();
                cp.first_name = txtParentFirstName.Text.Trim();
                cp.last_name = txtParentLastName.Text.Trim();
                cp.sex = ddlParentSex.SelectedValue;
                cp.birth_place = txtParentBirthPlace.Text.Trim();
                cp.birth_date = radParentBirthDate.SelectedDate.Value;
                cp.occupation = txtParentOccupation.Text.Trim();
                cp.marital_status = ddlParentMaritalStatus.SelectedValue;
                cp.id_card = txtParentIdCard.Text.Trim();
                cp.phone = txtParentPhone.Text.Trim();
                cp.email = txtParentEmail.Text.Trim();
                cp.address = txtParentAddress.Text.Trim();
                cp.job_title = txtParentJobTitle.Text.Trim();

                // registration fees
                StudentContribution sc = new StudentContribution();
                sc.contribution_type_id = int.Parse(hiddenContributionTypeId.Value);
                sc.paid_amount = double.Parse(txtContributionPaidAmount.Value.ToString());
                sc.classroom_id = int.Parse(ddlClassroom.SelectedValue);
                sc.academic_year_id = int.Parse(hiddenAcademicYearId.Value);
                sc.status = chkRegPaymentStatus.Checked == true ? 1 : 0;     // 0.- not paid / 1.- Paid
                sc.login_user_id = user.id;





                if (hiddenAcademicYearId.Value == null)
                {
                    msgContent = "Erreur : Veuillez configurer l\\'année académique, Veuillez signaler l\\'administrateur du system. !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
                }
                else
                {
                    /*************** ADD NEW STUDENT ***************/

                    //if (Session["student_id"] == null)
                    //{
                    string studentId = "EL-" + Universal.getUniversalSequence(sqlStudentNextval).ToString();
                    string parentCode = "PA-" + Universal.getUniversalSequence(sqlContactPersonNextval).ToString();

                    // student id
                    st.id = studentId;
                    sc.student_id = studentId;
                    // generate parent's code for online purpose
                    cp.parent_code = parentCode;

                    Student.addStudent(st);
                    //attach student to selected class
                    ClassRoom.attachStudentToClassroom(st);
                    // insert contact person
                    StudentContactPerson.Add(cp);
                    // attach student to contact person
                    string relationship = ddlParentRelationship.SelectedValue;
                    StudentContactPerson.Attach(parentCode, studentId, relationship);
                    // insert contribution fees
                    StudentContribution.makePayments(sc);

                    // attach documents
                    if (Session["list_documents_attach"] != null)
                    {
                        listDocumentsAttach = Session["list_documents_attach"] as List<StudentDocuments>;
                        StudentDocuments.uploadSudentDocuments(listDocumentsAttach, studentId);
                    }

                    // clear student form
                    emptyFields();

                    //get the student nextval and add it to the code
                    //txtCode.Text = "EL-" + Universal.getUniversalSequence(sqlStudentNextval).ToString();

                    MessageAlert.RadAlert("Enregistrer avec succès ! Code Eleve : " + studentId, 300, 200, "Information", null, "../images/success_check.png");
                    //}
                    //else
                    //{
                    //    /*************** UPDATE EXISTING STUDENT ***************/
                    //    Student.updateStudent(st);

                    //    // attach documents
                    //    if (Session["list_documents_attach"] != null)
                    //    {
                    //        listDocumentsAttach = Session["list_documents_attach"] as List<Documents>;
                    //        Documents.uploadSudentDocuments(listDocumentsAttach, st.id);
                    //    }

                    //    // clear document grid
                    //    listDocumentsAttach = null;
                    //    listDocumentsAttach = new List<Documents>();
                    //    gridAttachDocuments.Rebind();
                    //    // clear fields
                    //    emptyFields();

                    //    MessageAlert.RadAlert("Modifié avec succès !", 300, 200, "Information", null, "../images/success_check.png");

                    //    //Response.Redirect("SearchStudents.aspx");
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error " + ex.Message);
        }
    }

    private void emptyFields()
    {
        //empty student fields
        txtFirstName.Text = "";
        txtLastName.Text = "";
        ddlSex.SelectedValue = "-1";
        txtCardId.Text = "";
        txtBirthPlace.Text = "";
        radBirthDate.Clear();
        txtPhone.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        ddlClassroom.SelectedValue = "-1";
        ddlVacation.SelectedValue = "-1";
        //imageKeeper.ImageUrl = "~/images/image_data/Default.png";
        imgStudent.Attributes.Add("src", "../images/image_data/Default.png");

        // student contact person
        txtParentFirstName.Text = "";
        txtParentLastName.Text = "";
        ddlParentSex.SelectedValue = "-1";
        txtParentBirthPlace.Text = "";
        radParentBirthDate.Clear();
        txtParentOccupation.Text = "";
        ddlParentMaritalStatus.SelectedValue = "-1";
        txtParentIdCard.Text = "";
        txtParentPhone.Text = "";
        txtParentEmail.Text = "";
        txtParentAddress.Text = "";
        txtParentJobTitle.Text = "";
        ddlParentRelationship.SelectedValue = "-1";

        // kill sessions
        Session["student_id"] = null;
        Session["list_documents_attach"] = null;
        listDocumentsAttach = new List<StudentDocuments>();

        // reload documents grid
        gridAttachDocuments.Rebind();
    }

    private bool validateFields()
    {
        bool result = true;

        // check student info

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
        else if (ddlSex.SelectedValue == "-1")
        {
            result = false;
        }
        else if (ddlClassroom.SelectedValue == "-1")
        {
            result = false;
        }
        else if (ddlVacation.SelectedValue == "-1")
        {
            result = false;
        }

        // check student contact person

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
        else if (txtParentBirthPlace.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (radParentBirthDate.SelectedDate == null)
        {
            result = false;
        }
        else if (txtParentLastName.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentOccupation.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (ddlParentMaritalStatus.SelectedValue.ToString() == "-1")
        {
            result = false;
        }
        else if (txtParentIdCard.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentPhone.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentEmail.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentAddress.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (txtParentJobTitle.Text.Trim().Length <= 0)
        {
            result = false;
        }
        else if (ddlParentRelationship.SelectedValue == "-1")
        {
            result = false;
        }


        return result;
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
                        string filePaths = "~/images/student_profile/" + fileName;
                        imageUploader.SaveAs(Server.MapPath(filePaths)); //save file to folder
                                                                         //imageKeeper.ImageUrl = filePaths;
                        imgStudent.Attributes.Add("src", filePaths);
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

    protected void ddlClassroom_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        try
        {
            //
            if (ddlClassroom.SelectedValue != "-1")
            {
                int classId = int.Parse(ddlClassroom.SelectedValue);
                //List<ClassRoom> listClass = ClassRoom.getListVacationByClassroomId(classId);
                //loadListVacation(listClass);

            }
            else
            {
                ddlVacation.Items.Clear();
                ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
                ddlVacation.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadListVacation(List<ClassRoom> listClassroom)
    {
        //clear items
        ddlVacation.Items.Clear();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            // fill the ddl now
            ddlVacation.DataValueField = "vacation_type";
            ddlVacation.DataTextField = "vacation";
            ddlVacation.DataSource = listClassroom;
            ddlVacation.DataBind();
        }
        ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlVacation.SelectedValue = "-1";
    }

    protected void radBirthDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        RadDatePicker dpicker = sender as RadDatePicker;
        if (dpicker.DateInput.IsEmpty || dpicker.SelectedDate > DateTime.Now)
        {
            MessageAlert.RadAlert("Erreur : Date de naissance invalide !", 350, 200, "Information", null);
            radBirthDate.SelectedDate = null;
        }
        else
        {
            DateTime _birthDate = DateTime.Parse(dpicker.SelectedDate.Value.ToString());
            int days = (DateTime.Now - _birthDate).Days;
            if (days < 730) // check if student is at least 2 years old or 730 days old
            {
                MessageAlert.RadAlert("Erreur : Date de naissance invalide. Eleve doit avoir un (2) an ou plus !", 350, 200, "Information", null);
                radBirthDate.SelectedDate = null;
            }
        }
    }

    protected void ddlVacation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddl = sender as RadDropDownList;
        //
        if (ddl.SelectedValue != "-1" && ddlClassroom.SelectedValue != "-1")
        {
            try
            {
                ClassRoom _classroom = new ClassRoom();
                _classroom.classroom_id = int.Parse(ddlClassroom.SelectedValue);
                _classroom.vacation_type = ddlVacation.SelectedValue;
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }
    }

    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        lblEmailError.Visible = false;
        if (txtEmail.Text.Trim().Length > 0)
        {
            bool checkEmail = Universal.IsValidEmailAddress(txtEmail.Text.Trim());
            if (!checkEmail)
            {
                lblEmailError.Visible = true;
                txtEmail.Text = string.Empty;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        emptyFields();

        //if (Session["student_id"] != null)
        //{
        //    Session.Remove("student_id");
        //    Session["student_id"] = null;
        //    //
        //    Response.Redirect("SearchStudents.aspx");
        //}
    }


    protected void gridAttachDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["list_documents_attach"] != null)
        {
            listDocumentsAttach = Session["list_documents_attach"] as List<StudentDocuments>;
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

    protected void btnAttachDocuments_ServerClick(object sender, EventArgs e)
    {
        if (ddlDocumentType.SelectedValue == "-1")
        {
            MessageAlert.RadAlert("Erreur : Veuillez selectionner la categorie du document", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            //get new document path
            string FolderStudents = Server.MapPath("~/student_uploaded_documents/");

            if (!Directory.Exists(FolderStudents))
            {
                Directory.CreateDirectory(Server.MapPath("~/student_uploaded_documents/"));
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
                        listDocumentsAttach = Session["list_documents_attach"] as List<StudentDocuments>;
                    }

                    HttpPostedFile userPostedFile = documentsAttachFile.PostedFile;
                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                            string fileName = "st_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(userPostedFile.FileName);
                            string filepath = "~/student_uploaded_documents/" + fileName;
                            userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                            StudentDocuments doc = new StudentDocuments();
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
            listDocumentsAttach = Session["list_documents_attach"] as List<StudentDocuments>;
        }

        List<StudentDocuments> listTemp = new List<StudentDocuments>();
        //foreach (Documents doc in listDocumentsAttach)
        //{
        //    if (doc.rand_code != randCode)
        //    {
        //        listTemp.Add(doc);
        //    }
        //}
        // udpate old list to temporary list
        listDocumentsAttach = new List<StudentDocuments>();
        listDocumentsAttach = listTemp;
        Session["list_documents_attach"] = listTemp;
        // refresh grid
        gridAttachDocuments.Rebind();

    }

}
