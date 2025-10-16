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
using Telerik.Web.UI;
using Telerik.Web;
using Utilities;



public partial class ManageGroupe : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SETTINGS;


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
                txtRoleName.Focus();
                
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
                        gridRoles.Rebind();
                        disableDeleteOption();
                    }
                }
                else
                {
                    Response.Redirect("~/Pages/NoPrivilegeWarningPage.aspx");
                }
            }
        }
    }
    
    // edit_access
    private void disableEditOption()
    {
        // BUTTONS
        btnAddAccessRights.Attributes.Add("disabled", "disabled");
        btnAddRole.Attributes.Add("disabled", "disabled");


        // USER ACCESS RIGHTS

        bool result = false;

        chkFullAccessGrant.Enabled = result;

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
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridRoles.Visible && gridRoles.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridRoles.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btn = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
                    btn.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnAddRole_Click(object sender, EventArgs e)
    {
        if (txtRoleName.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Veuillez saisir le nom du role !", 300, 200, "Information", null, "../images/error.png");
            txtRoleName.Focus();
        }
        else
        {
            Users p = new Users();
            p.name = txtRoleName.Text.Trim();
            //check name
            bool checkType = Settings.checkExistedRole(p.name);
            if (!checkType)
            {
                Users.addRole(p);
                // resetTypeForm();
                gridRoles.Rebind();
                txtRoleName.Text = "";
                txtRoleName.Focus();
            }
            else
            {
                MessageAlert.RadAlert("Désolé, Ce role existe !", 300, 200, "Information", null, "../images/error.png");
            }

        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridRoles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<Users> listResult = Users.getAllRole();
        gridRoles.DataSource = listResult;
    }

    protected void gridRoles_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridRoles_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridRoles.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void chkFullAccessGrant_CheckedChanged(object sender, EventArgs e)
    {
        bool result = false;
        if (chkFullAccessGrant.Checked) { result = true; }

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

    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int roleId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            string roleName = dataItem.Cells[5].Text.ToString();

            //first check if this Role is already assigned
            bool roleAssign = Users.RoleAlreadyAssign(roleId);
            if (roleAssign)
            {
                string msgContent = "Desolé vous ne pouvez pas supprimer Le Groupe : " + roleName + ", il est déja assigné à un utilisateur.";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                Users.removeUserAccessLevelByRoleId(roleId);
                Users.deleteRole(roleId);
                //refresh data of the gridview
                gridRoles.Rebind();
                MessageAlert.RadAlert("Succès !", 300, 200, "Information", null, "../images/success_check.png");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnAffectRightsToRole_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        //int index = dataItem.RowIndex;
        int roleId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
        string roleName = dataItem.Cells[5].Text.ToString();

        Session["role_id"] = roleId;

        pnlCreateRole.Visible = false;
        pnlRoleAccess.Visible = true;
        lblAccessRightHeading.Text = "Définir les droits d’accès du role : " + roleName;
        clearAccessForm();
        loadExistingAccessRights();
    }

    private void loadExistingAccessRights()
    {
        try
        {
            if (Session["role_id"] != null)
            {
                int roleId = int.Parse(Session["role_id"].ToString());
                List<Users> listPolicy = Users.getListUserAccessLevelByRoleId(roleId);
                if (listPolicy != null && listPolicy.Count > 0)
                {
                    foreach (Users s in listPolicy)
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
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnAddAccessRights_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["role_id"] != null)
            {
                Users s = null;
                int roleId = int.Parse(Session["role_id"].ToString());
                List<Users> roleList = new List<Users>();

                // ELEVE
                if (ChkStudentView.Checked
                    || ChkStudentEdit.Checked
                    || ChkStudentDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.STUDENT;
                    s.view_access = ChkStudentView.Checked ? 1 : 0;
                    s.edit_access = ChkStudentEdit.Checked ? 1 : 0;
                    s.delete_access = ChkStudentDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                // CLASSE
                if (ChkClasseView.Checked
                    || ChkClasseEdit.Checked
                    || ChkClasseDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.CLASSROOM;
                    s.view_access = ChkClasseView.Checked ? 1 : 0;
                    s.edit_access = ChkClasseEdit.Checked ? 1 : 0;
                    s.delete_access = ChkClasseDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                // SECRETARIAT
                if (ChkSecretaryView.Checked
                    || ChkSecretaryEdit.Checked
                    || ChkSecretaryDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.SECRETARIAT;
                    s.view_access = ChkSecretaryView.Checked ? 1 : 0;
                    s.edit_access = ChkSecretaryEdit.Checked ? 1 : 0;
                    s.delete_access = ChkSecretaryDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                // RESSOURCES HUMAINES
                if (ChkHrView.Checked
                    || ChkHrEdit.Checked
                    || ChkHrDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.HR;
                    s.view_access = ChkHrView.Checked ? 1 : 0;
                    s.edit_access = ChkHrEdit.Checked ? 1 : 0;
                    s.delete_access = ChkHrDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                // ECONOMAT
                if (ChkEconomatView.Checked
                    || ChkEconomatEdit.Checked
                    || ChkEconomatDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.ECONOMAT;
                    s.view_access = ChkEconomatView.Checked ? 1 : 0;
                    s.edit_access = ChkEconomatEdit.Checked ? 1 : 0;
                    s.delete_access = ChkEconomatDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                // PARAMETRES
                if (ChkParameterView.Checked
                    || ChkParameterEdit.Checked
                    || ChkParameterDelete.Checked)
                {
                    s = new Users();
                    s.menu_id = (int)Users.MENU.SETTINGS;
                    s.view_access = ChkParameterView.Checked ? 1 : 0;
                    s.edit_access = ChkParameterEdit.Checked ? 1 : 0;
                    s.delete_access = ChkParameterDelete.Checked ? 1 : 0;
                    s.role_id = roleId;
                    roleList.Add(s);
                }

                Users.InsertAccessLevelByRole(roleId, roleList);

                //
                MessageAlert.RadAlert("Succès !", 300, 200, "Information", null, "../images/success_check.png");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnViewRoleList_ServerClick(object sender, EventArgs e)
    {
        pnlCreateRole.Visible = true;
        pnlRoleAccess.Visible = false;
        //
        Session.Remove("role_id");
        Session["role_id"] = null;
        //
        clearAccessForm();
    }

    protected void clearAccessForm()
    {
        bool result = false;

        chkFullAccessGrant.Checked = result;

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



}
