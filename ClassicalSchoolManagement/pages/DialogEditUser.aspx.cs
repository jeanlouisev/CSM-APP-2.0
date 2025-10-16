using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Utilities;
using System.Collections.Generic;
using Db_Core;
//using System.Data.OracleClient;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Web.Services;



public partial class DialogEditUser : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // for telerik validation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;
        //
        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // add new
            if (Session["user_code"] == null)
            {
                txtCode.Enabled = true;
                // add 90 days as expiry date
                radExpiryDate.SelectedDate = DateTime.Now.AddDays(90);
            }
            else // update info
            {
                loadListRoles();
                //loadPrinters();
                loadPreviousData();
            }
        }
    }

    private void loadListRoles()
    {
        List<Users> listResult = Users.getAllRoleType();
        ddlRoles.DataSource = listResult;
        ddlRoles.DataValueField = "id";
        ddlRoles.DataTextField = "name";
        ddlRoles.DataBind();
        //
        //ddlRoles.Items.Insert(0, new RadComboBoxItem("--Sélectionner--", "-1"));
        //ddlRoles.SelectedValue = "-1";
    }

    private void loadPreviousData()
    {
        // get basic user info
        string userCode = Session["user_code"].ToString();
        Users _userInfo = Users.GetUserInfoByCode(userCode);

        txtCode.Text = _userInfo.username;
        txtFullname.Text = _userInfo.fullname;
        radExpiryDate.SelectedDate = _userInfo.expiry_date;
        ddlLockedStatus.SelectedValue = _userInfo.locked.ToString();
        ddlRoles.SelectedValue = _userInfo.role_id.ToString();
        //
        loadExistingAccessRights(_userInfo.role_id);
    }

    protected void clearDisableAccessRightsCheckbox()
    {
        bool result = false;

        // disable

        // ELEVE  menu-1
        ChkStudentView.Enabled = result;
        ChkStudentEdit.Enabled = result;
        ChkStudentDelete.Enabled = result;

        // CLASSE  menu-2
        ChkClasseView.Enabled = result;
        ChkClasseEdit.Enabled = result;
        ChkClasseDelete.Enabled = result;

        // SECRETARIAT  menu-3
        ChkSecretaryView.Enabled = result;
        ChkSecretaryEdit.Enabled = result;
        ChkSecretaryDelete.Enabled = result;

        // RESSOURCES HUMAINES  menu-4
        ChkHrView.Enabled = result;
        ChkHrEdit.Enabled = result;
        ChkHrDelete.Enabled = result;

        // ECONOMAT  menu-5
        ChkEconomatView.Enabled = result;
        ChkEconomatEdit.Enabled = result;
        ChkEconomatDelete.Enabled = result;

        // PARAMETRES  menu-6
        ChkParameterView.Enabled = result;
        ChkParameterEdit.Enabled = result;
        ChkParameterDelete.Enabled = result;






        // uncheck

        // ELEVE  menu-1
        ChkStudentView.Checked = result;
        ChkStudentEdit.Checked = result;
        ChkStudentDelete.Checked = result;

        // CLASSE  menu-2
        ChkClasseView.Checked = result;
        ChkClasseEdit.Checked = result;
        ChkClasseDelete.Checked = result;

        // SECRETARIAT  menu-3
        ChkSecretaryView.Checked = result;
        ChkSecretaryEdit.Checked = result;
        ChkSecretaryDelete.Checked = result;

        // RESSOURCES HUMAINES  menu-4
        ChkHrView.Checked = result;
        ChkHrEdit.Checked = result;
        ChkHrDelete.Checked = result;

        // ECONOMAT  menu-5
        ChkEconomatView.Checked = result;
        ChkEconomatEdit.Checked = result;
        ChkEconomatDelete.Checked = result;

        // PARAMETRES  menu-6
        ChkParameterView.Checked = result;
        ChkParameterEdit.Checked = result;
        ChkParameterDelete.Checked = result;
    }

    private void loadExistingAccessRights(int roleId)
    {
        try
        {
            List<Users> listResult = Users.getListUserAccessLevelByRoleId(roleId);
            if (listResult != null && listResult.Count > 0)
            {
                foreach (Users s in listResult)
                {
                    switch (s.menu_id)
                    {
                        // ELEVE
                        case (int)Users.MENU.STUDENT:
                            if (s.view_access == 1) ChkStudentView.Checked = true; else ChkStudentView.Checked = false;
                            if (s.edit_access == 1) ChkStudentEdit.Checked = true; else ChkStudentEdit.Checked = false;
                            if (s.delete_access == 1) ChkStudentDelete.Checked = true; else ChkStudentDelete.Checked = false;
                            break;

                        // CLASSE
                        case (int)Users.MENU.CLASSROOM:
                            if (s.view_access == 1) ChkClasseView.Checked = true; else ChkClasseView.Checked = false;
                            if (s.edit_access == 1) ChkClasseEdit.Checked = true; else ChkClasseEdit.Checked = false;
                            if (s.delete_access == 1) ChkClasseDelete.Checked = true; else ChkClasseDelete.Checked = false;
                            break;

                        // SECRETARIAT
                        case (int)Users.MENU.SECRETARIAT:
                            if (s.view_access == 1) ChkSecretaryView.Checked = true; else ChkSecretaryView.Checked = false;
                            if (s.edit_access == 1) ChkSecretaryEdit.Checked = true; else ChkSecretaryEdit.Checked = false;
                            if (s.delete_access == 1) ChkSecretaryDelete.Checked = true; else ChkSecretaryDelete.Checked = false;
                            break;

                        // RESSOURCES HUMAINES
                        case (int)Users.MENU.HR:
                            if (s.view_access == 1) ChkHrView.Checked = true; else ChkHrView.Checked = false;
                            if (s.edit_access == 1) ChkHrEdit.Checked = true; else ChkHrEdit.Checked = false;
                            if (s.delete_access == 1) ChkHrDelete.Checked = true; else ChkHrDelete.Checked = false;
                            break;

                        // ECONOMAT
                        case (int)Users.MENU.ECONOMAT:
                            if (s.view_access == 1) ChkEconomatView.Checked = true; else ChkEconomatView.Checked = false;
                            if (s.edit_access == 1) ChkEconomatEdit.Checked = true; else ChkEconomatEdit.Checked = false;
                            if (s.delete_access == 1) ChkEconomatDelete.Checked = true; else ChkEconomatDelete.Checked = false;
                            break;

                        // PARAMETRES
                        case (int)Users.MENU.SETTINGS:
                            if (s.view_access == 1) ChkParameterView.Checked = true; else ChkParameterView.Checked = false;
                            if (s.edit_access == 1) ChkParameterEdit.Checked = true; else ChkParameterEdit.Checked = false;
                            if (s.delete_access == 1) ChkParameterDelete.Checked = true; else ChkParameterDelete.Checked = false;
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // check if userCode already exist
        string userCode = txtCode.Text.Trim();
        //
        Users _userInfo = new Users();
        _userInfo.username = userCode;
        _userInfo.locked = int.Parse(ddlLockedStatus.SelectedValue);
        _userInfo.expiry_date = radExpiryDate.SelectedDate.Value;
        _userInfo.role_id = int.Parse(ddlRoles.SelectedValue);

        // update user info
        Users.UpdateUserInfo(_userInfo);


        // change staff status  
        int status = _userInfo.locked == (int)Users.ACCOUNT_LOCK_STATUS.LOCKED ? (int)Staff.STATUS.NOT_ACTIVE : (int)Staff.STATUS.ACTIVE;
        Staff.changeStatus(status, userCode);

        ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseDialog();", true);
    }

    protected void ddlRoles_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int roleId = int.Parse(ddlRoles.SelectedValue);
        clearDisableAccessRightsCheckbox();
        loadExistingAccessRights(roleId);
    }
}