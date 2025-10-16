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



public partial class DocumentsManagement : System.Web.UI.Page
{
    int menu_code = 2; // parameter menu code, for more information see "menu" table.
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
            Users user = Session["user"] as Users;
            if (user == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                //
                //radFromDate.SelectedDate = DateTime.Now.AddDays(-360);
                //radToDate.SelectedDate = DateTime.Now;

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
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            txtCode.ReadOnly = true;
            documentUploader.Enabled = false;
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
            //// loop through the grid to disable delete option
            //if (gridListDocuments.Visible && gridListDocuments.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListDocuments.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn = gridListDocuments.Rows[i].Cells[5].FindControl("btnDelete") as ImageButton;
            //        imgBtn.Enabled = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    public void downloadDocuments(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            //Documents.deleteDocumentById(id);
            //refresh data of the gridview
            gridDocuments.Rebind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnUploadDocuments_Click(object sender, EventArgs e)
    {
        if (checkUploadFields())
        {
            //add documents to database
            addDocuments();
        }
    }

    private void addDocuments()
    {
        string code = txtCode.Text.Trim();
        string description = txtDescription.Text.Trim();

        // check code validity
        if (Universal.codeExist(code))
        {
            //get new document path
            string FolderStudents = Server.MapPath("~/Uploaded_Documents/" + code.ToUpper());

            if (!Directory.Exists(FolderStudents))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploaded_Documents/" + code.ToUpper()));
            }

            if (documentUploader.HasFile) // check fileUpload control for files
            {
                string file_extension = System.IO.Path.GetExtension(documentUploader.FileName); // get file extension
                if (file_extension.ToLower() != ".pdf") //check file extension
                {
                    MessBox.Show("Seul les fichiers PDF peuvend etre attaches");
                }
                else
                {
                    HttpFileCollection uploadedFiles = Request.Files;

                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];
                        try
                        {
                            if (userPostedFile.ContentLength > 0)
                            {
                                string fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + Path.GetFileName(userPostedFile.FileName);
                                string filepath = "~/Uploaded_Documents/" + code.ToUpper() + "/" + fileName;
                                userPostedFile.SaveAs(Server.MapPath(filepath)); //save file to folder
                                Documents doc = new Documents();
                                doc.staff_code = code.ToUpper();
                                doc.document_path = filepath;
                                doc.document_name = description;
                                //doc.document_type = int.Parse(category);
                                //Documents.uploadDocument(doc);
                                // refresh the gridview
                                gridDocuments.Rebind();
                                //
                                MessageAlert.RadAlert("Succès !", 350, 200, "Information", null, "../images/success_check.png");
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
        else
        {
            MessBox.Show("Erreur : Code Invalide !");
            txtCode.Focus();
        }
    }

    private bool checkUploadFields()
    {
        if (txtCode.Text.Trim().Length <= 0)
        {
            MessBox.Show("Veuillez taper le code !");
            txtCode.Focus();
            return false;
        }
        else if (!documentUploader.HasFile)
        {
            MessBox.Show("Veuillez Attacher un document !");
            documentUploader.Focus();
            return false;
        }
        else if (txtDescription.Text.Trim().Length <= 0)
        {
            MessBox.Show("Veuillez Tapez une description !");
            txtDescription.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<Documents> listResult = new List<Documents>();
        if (txtCode.Text.Trim().Length > 0)
        {
            Documents doc = new Documents();
            doc.staff_code = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
            //get documents list
            listResult = Documents.getListUploadedDocuments(doc);
        }
        gridDocuments.DataSource = listResult;
    }

    protected void gridDocuments_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridDocuments_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridDocuments.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnSearchByCode_ServerClick(object sender, EventArgs e)
    {
        string imagePaths = "../images/image_data/Default.png";
        if (txtCode.Text.Trim().Length <= 0)
        {
            txtFullname.Text = "";
            imgStaffs.Attributes.Add("src", imagePaths);
            MessageAlert.RadAlert("Code invalide !", 350, 150, "Error", null, "/images/warning.png");
        }
        else
        {
            string staffCode = txtCode.Text.Trim();
            List<Universal> staffInfoList = Universal.getListAllExceptParentsByCode(staffCode);
            if (staffInfoList == null || staffInfoList.Count <= 0)
            {
                txtFullname.Text = "";
                imgStaffs.Attributes.Add("src", imagePaths);
                MessageAlert.RadAlert("Code invalide !", 350, 150, "Error", null, "/images/warning.png");
            }
            else
            {
                Universal uni = staffInfoList[0];
                txtFullname.Text = uni.fullName;
                imagePaths = "../images/image_data/" + uni.image_path;
                imgStaffs.Attributes.Add("src", imagePaths);
            }
        }
        gridDocuments.Rebind();
    }
}
