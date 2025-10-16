using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Utilities;
using System.Data;

using Telerik.Web.UI;
using System.Drawing;


public partial class UploaderNotes : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;

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

        if (!IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;
                lblResult.Visible = false;
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
            }
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        try
        {
            NoteUploader.Enabled = false;
            // btnUploadExam.Enabled = false;
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
            //// loop through the grid to disable edit option
            //if (gridListExam.Visible && gridListExam.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridListExam.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn1 = gridListExam.Rows[i].Cells[8].FindControl("btnDelete") as ImageButton;
            //        imgBtn1.Enabled = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }
    /******************************************* END USER POLICY **************************/

    protected void lnkDownLoad_Click(object sender, EventArgs e)
    {
        lnkDownLoad.CommandArgument = "design/Templates/Notes.xls";
        if (lnkDownLoad.CommandArgument != null && lnkDownLoad.CommandArgument != "")
        {
            DownloadFile(lnkDownLoad.CommandArgument);
        }
    }

    private void DownloadFile(string path)
    {
        try
        {
            FileInfo file = new FileInfo(Request.PhysicalApplicationPath + path);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(Server.MapPath("~/" + path));
                Response.Flush();
                Response.End();
            }
            else
            {
                MessBox.Show("File not found: " + Path.GetFileName(path));
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error download file: " + ex.Message);
        }
    }

    protected void btnUploadExam_Click(object sender, EventArgs e)
    {
        Session["FILE_NAME"] = null;
        Session["location"] = null;
        string fileName = UploadFile();//Upload file to server
        DataTable dt = new DataTable();

        string location = Session["location"].ToString();

        dt = Universal.ReadExcelData(Session["location"].ToString());//Read file in server
                                                                     //
        if (dt != null && dt.Rows.Count > 0)
        {
            ImportPalmares(dt, fileName);
        }
    }

    public string UploadFile()
    {
        if (!Directory.Exists(Server.MapPath("..") + "\\" + "App_Data\\Import"))
        {
            Directory.CreateDirectory(Server.MapPath("..") + "\\" + "App_Data\\Import");//create folder
        }
        if ((NoteUploader.PostedFile != null) && (NoteUploader.PostedFile.ContentLength > 0))
        {
            try
            {
                String filename = System.IO.Path.GetFileName(NoteUploader.PostedFile.FileName);
                filename = DateTime.Now.ToString("yyyMMdd_HH-mm") + "_" + filename;
                String SaveLocation = Server.MapPath("..") + "\\" + "App_Data\\Import" + "\\" + filename;
                Session["location"] = SaveLocation;//link save file
                NoteUploader.PostedFile.SaveAs(SaveLocation);//upload file to server
                return SaveLocation;
            }
            catch (Exception ex)
            {
                MessBox.Show("ERROR: " + ex.Message);
                return "";
            }
        }
        return "";
    }

    protected void ImportPalmares(DataTable dt, string filename)
    {
        //create a instance of class Notes
        List<Notes> listNotes = new List<Notes>();

        try
        {
            //check if datatable contains value.
            if (dt != null && dt.Rows.Count > 0)
            {
                //loop through each roww in datatable
                foreach (DataRow row in dt.Rows)
                {
                    Notes notes = new Notes();
                    //
                    if (row.ItemArray[12].ToString() != "")
                    {
                        notes.student_id = row.ItemArray[12].ToString();
                        //notes.id_exam = row.ItemArray[11].ToString();
                        notes.coefficient = row.ItemArray[7].ToString().Length <= 0 ? 0 : int.Parse(row.ItemArray[7].ToString());
                        notes.note_obtained = row.ItemArray[8].ToString().Length <= 0 ? 0.0 : double.Parse(row.ItemArray[8].ToString());
                        notes.academic_year_id = int.Parse(row.ItemArray[10].ToString());
                        notes.control = int.Parse(row.ItemArray[9].ToString());
                        //delete previously inserted notes
                        Notes.deleteNotes(notes);
                        //
                        if (notes.note_obtained > 0)
                        {
                            //add notes to database
                            //Notes.addNotes(notes);
                        }
                    }
                }
            }
            //
            lblResult.Text = "Successful";
            lblResult.ForeColor = Color.DarkGreen;
            lblResult.Visible = true;
        }
        catch (Exception ex)
        {
            MessBox.Show("Error: " + ex.Message);
            return;
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }


}