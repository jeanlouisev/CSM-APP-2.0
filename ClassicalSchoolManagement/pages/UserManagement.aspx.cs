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
using System.Net; // for network purpose
using System.Net.Sockets;

public partial class UserManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SETTINGS;
    string language = "";
    string msgContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        language = Session["language"] == null ? "" : Session["language"].ToString();
        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        else
        {
            // VERIFY USER ACCESS LEVEL
            List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
            if (listResult != null && listResult.Count > 0)
            {
                Users userAccess = listResult[0];
                int notGranted = (int)Users.ACCESS.NO;
                
                gridUsers.Rebind();

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

        if (!IsPostBack)
        {

        }
    }

    // edit_access
    private void disableEditOption()
    {
        try
        {
            // BUTTONS
            btnSearch.Attributes.Add("disabled", "disabled");

            // loop through the grid to disable delete option
            if (gridUsers.Visible && gridUsers.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridUsers.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnEdit = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnEdit");
                    //
                    btnEdit.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridUsers.Visible && gridUsers.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridUsers.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnResetAccount = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnResetAccount");
                    //
                    btnResetAccount.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void gridUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        Users user = new Users();
        user.username = txtCodeSearch.Text.Trim().Length <= 0 ? null : txtCodeSearch.Text.Trim().ToUpper();
        List<Users> listResult = Users.GetListUserAll(user);
        gridUsers.DataSource = listResult;
    }

    protected void gridUsers_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridUsers_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridUsers.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();

            Literal lblStatusLock = (Literal)item.FindControl("lblLockedStatus");
            if (lblStatusLock.Text == "Vérouillé")
            {
                item["lock_status"].BackColor = System.Drawing.ColorTranslator.FromHtml("#E74C3C");
                item["lock_status"].ForeColor = Color.White;
            }
            if (lblStatusLock.Text == "Dé-vérouillé")
            {
                item["lock_status"].BackColor = System.Drawing.ColorTranslator.FromHtml("#2ECC71");
                item["lock_status"].ForeColor = Color.White;
            }
        }
    }

    protected void btnDesactivate_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            string code = dataItem.GetDataKeyValue("code").ToString();
            //Student.DeactivateStudentByCode(code);
            gridUsers.Rebind();
            MessageAlert.RadAlert("Deactivate Success !", 250, 150, "Information", null, "../images/success_check.png");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        // int index = dataItem.RowIndex;
        string userCode = dataItem.GetDataKeyValue("username").ToString().ToUpper();
        //UserInfo.DeleteUserByCode(userCode);
        gridUsers.Rebind();
        // show confirmation message
        if (language == "fr")
        {
            MessageAlert.RadAlert("Compte supprimé avec succès !", 300, 200, "Information", null, "../images/success_check.png");
        }
        else
        {
            MessageAlert.RadAlert("Account deleted successfuly !", 300, 200, "Information", null, "../images/success_check.png");
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        gridUsers.Rebind();
    }

    protected void btnEdit_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            Session["user_code"] = dataItem.GetDataKeyValue("username").ToString();
            //
            string page_url = "DialogEditUser.aspx";
            //Response.Redirect(page_url);
            try
            {
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(800, 650); oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnResetAccount_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        // int index = dataItem.RowIndex;
        string userCode = dataItem.GetDataKeyValue("username").ToString().ToUpper();
        string defaultPasswd = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_DEFAULT_PASSWD"];
        //
        Users.ResetUserByCode(userCode, Hash.EncodePasswordSH1(defaultPasswd));
        int status = (int)Staff.STATUS.ACTIVE;
        Staff.changeStatus(status, userCode);
        // refresh grid
        gridUsers.Rebind();
        msgContent = "Réinitialisation du compte réussie ! le mot de passe par défaut est " + defaultPasswd;
        MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");


        //
        //if (language == "fr")
        //{
        //msgContent = "Réinitialisation du compte réussie ! le mot de passe par défaut est 123";
        //    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
        //}
        //else
        //{
        //msgContent = "Account reset successfuly ! default password is 123";
        //MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridUsers.Rebind();
    }

}