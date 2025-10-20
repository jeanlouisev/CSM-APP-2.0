using iTextSharp.tool.xml.xtra.xfa.js;
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


public partial class StudentContributionType : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.STUDENT;

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
            // initialize elements
            hiddenId.Value = null;
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
            btnAdd.Attributes.Add("disabled", "disabled");

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
            if (gridContributionType.Visible && gridContributionType.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridContributionType.MasterTableView.Items)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim().Length <= 0
            || txtPrice.Value == null)
        {
            MessageAlert.RadAlert("Erreur : Veuillez remplir tous les champs !", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            // check if user wants to update or add new
            if (hiddenId.Value == null || hiddenId.Value == "") // then add new
            {
                string description = txtDescription.Text.Trim();
                double price = 0;

                //check if description already exists
                List<StudentContribution> listResult = StudentContribution.descriptionAlreadyExist(description);
                if (listResult.Count <= 0)
                {
                    price = double.Parse(txtPrice.Value.ToString());

                    StudentContribution.addContributionType(description.ToUpper(), price);
                    // clear form
                    txtDescription.Text = string.Empty;
                    txtPrice.Value = null;
                    txtDescription.Focus();
                    gridContributionType.Rebind();

                    MessageAlert.RadAlert("Enregistré avec succès !", 300, 200, "Information", null, "../images/success_check.png");
                }
                else
                {
                    MessageAlert.RadAlert("Erreur : La contribution : " + description + " Existe !", 300, 200, "Information", null, "../images/error.png");

                }
            }
            else  // then update
            {

                StudentContribution sc = new StudentContribution();
                sc.id = int.Parse(hiddenId.Value);
                sc.description = txtDescription.Text.Trim();
                sc.price = double.Parse(txtPrice.Value.ToString());
                // update information
                StudentContribution.updateContributionType(sc);
                // cear forms
                hiddenId.Value = null;
                txtDescription.Text = "";
                txtDescription.Enabled = true;
                txtDescription.Focus();
                txtPrice.Value = null;
                gridContributionType.Rebind();

                MessageAlert.RadAlert("Modifié avec succès !", 300, 200, "Information", null, "../images/success_check.png");
            }

        }
    }

    public void removeContributionType(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            StudentContribution.deleteContributionType(id);

            //refresh data of the gridview
            gridContributionType.Rebind();

            MessageAlert.RadAlert("Supprimé avec succès !", 300, 200, "Information", null, "../images/success_check.png");
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public void editContributionType(object sender, EventArgs e)
    {

        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            //
            StudentContribution sc = StudentContribution.getListContributionTypeById(id);
            txtDescription.Text = sc.description;
            txtDescription.Enabled = false;
            txtPrice.Value = sc.price;
            hiddenId.Value = id.ToString();

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnSearchCours_Click(object sender, EventArgs e)
    {
        gridContributionType.Rebind();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridContributionType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        string description = txtDescription.Text.Trim().Length <= 0 ? null : txtDescription.Text.Trim();
        //
        List<StudentContribution> listResult = StudentContribution.getListContributionTypeByDescription(description);
        gridContributionType.DataSource = listResult;
    }

    protected void gridContributionType_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridContributionType_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridContributionType.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}