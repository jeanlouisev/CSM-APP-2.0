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


public partial class SearchPersonal : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.HR;

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
            // remove old sessions
            Session.Remove("staff_code");
            Session.Remove("list_documents_attach");
            loadPositions();
            BindDataGridStaff();
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

    // edit_access
    private void disableEditOption()
    {
        try
        {
            //btnSearch.Attributes.Add("disabled", "disabled");
            btnExportExcel.Attributes.Add("disabled", "disabled");

            // loop through the grid to disable delete option
            if (gridStaff.Visible && gridStaff.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridStaff.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnEdit = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnEdit");
                    //
                    btnEdit.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridStaff.Visible && gridStaff.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridStaff.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnDelete = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
                    //
                    btnDelete.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
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

            ddlPosition.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlPosition.SelectedValue = "-1";
        }
        catch (Exception ex) { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataGridStaff();
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    private void BindDataGridStaff()
    {
        Staff st = new Staff();
        //get the fields from the form            
        st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
        st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : txtFullName.Text.Trim();
        st.sex = ddlSex.SelectedValue;
        st.marital_status = ddlMaritalStatus.SelectedValue;
        st.position_id = int.Parse(ddlPosition.SelectedValue);
        st.status = int.Parse(ddlStatus.SelectedValue);

        //get list of staff
        List<Staff> listResult = Staff.getListStaff(st);
        gridStaff.DataSource = listResult;
        gridStaff.DataBind();


        // check user access level
        //verifyAccessLevel();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

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

    protected void gridStaff_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridStaff_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridStaff.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
            //
            string status = item.Cells[12].Text;
            //item.Cells[12].Text = status == 1 ? "Activé" : "Désactivé";

            if(status == "Désactivé")
            {
                item.Cells[12].BackColor = Color.Red;
                item.Cells[12].ForeColor = Color.WhiteSmoke;
            }
        }
    }

    protected void btnExportExcel_ServerClick(object sender, EventArgs e)
    {
        try
        {
            Users user = new Users();
            Staff st = new Staff();
            //get the fields from the form            
            st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
            st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : txtFullName.Text.Trim();
            st.sex = ddlSex.SelectedValue;
            st.marital_status = ddlMaritalStatus.SelectedValue;
            st.position_id = int.Parse(ddlPosition.SelectedValue);
            st.status = int.Parse(ddlStatus.SelectedValue);

            //get list of staff
            List<Staff> listResult = Staff.getListStaff(st);
            gridStaff.DataSource = listResult;
            gridStaff.Rebind();


            if (listResult == null || listResult.Count <= 0)
            {
                MessBox.Show("No data to export");
            }
            else
            {
                string Path = string.Format("liste_employes_{0}_{1}", user.username,
                        DateTime.Now.ToString("yyyyMMddHHmmss"));

                gridStaff.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gridStaff.ExportSettings.IgnorePaging = true;
                gridStaff.ExportSettings.FileName = Path;
                gridStaff.ExportSettings.ExportOnlyData = true;
                gridStaff.ExportSettings.OpenInNewWindow = true;
                gridStaff.MasterTableView.ExportToExcel();

            }

        }
        catch (System.Threading.ThreadAbortException lException)
        {
            // do nothing
        }
        catch (Exception ex)
        {

        }
    }

    public void removeStaff(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string staffCode = dataItem.GetDataKeyValue("id").ToString();
            //this part only set status of student to 0
            Staff.disableStaff(staffCode);
            // this part delete the student completely from the system
            // ------>  Student.deleteStudentPermanently(studentCode);
            //refresh data of the gridview
            BindDataGridStaff();
            MessageAlert.RadAlert("Désactivé avec succès !", 350, 150, "Information", null, "../images/success_check.png");
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnEdit_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string staffCode = dataItem.GetDataKeyValue("id").ToString();
            Session["staff_code"] = staffCode;
            Response.Redirect("AddPersonal.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnViewDetails_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string staffCode = dataItem.GetDataKeyValue("id").ToString();
            //int classroomId = int.Parse(((HiddenField)dataItem.FindControl("hiddenClassroomId")).Value.ToString());

            Session["staff_code"] = staffCode;
            //Session["classroom_id"] = classroomId;

            string page_url = "DialogStaffDetailsInfo.aspx";

            //Response.Redirect("DocumentDetail.aspx");
            //Session["type_detail"] = "endedit";
            //mp1.Show();
            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            //+ "oWinn.maximize();"       
                                            + "oWinn.SetSize(1124, 650);"
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
}
