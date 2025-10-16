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


public partial class ReasonSheets : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;
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
            //btnAddPayment.Enabled = false;
            txtDescription.ReadOnly = true;
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
            // loop through the grid to disable delete option
            //if (gridReason.Visible && gridReason.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gridReason.Rows.Count; i++)
            //    {
            //        ImageButton imgBtn1 = gridReason.Rows[i].Cells[2].FindControl("btnDelete") as ImageButton;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Error :  Veuillez saisir une description !", 350, 200, "Information", null, "../images/error.png");
            txtDescription.Focus();
        }
        else
        {
            string _description = txtDescription.Text.Trim();

            //check name
            bool checkType = Timesheet.reasonExist(_description);
            if (!checkType)
            {
                try
                {
                    Timesheet.addReason(_description);
                    resetForm();
                    gridReason.Rebind();
                    MessageAlert.RadAlert("Succès !", 350, 200, "Information", null, "../images/success_check.png");
                }
                catch { }
            }
            else
            {
                MessageAlert.RadAlert("Le type de motif " + _description.ToUpper() + " existe. Veuillez ajouter un autre.",
                                        350, 200, "Information", null, "../images/error.png");
            }
        }
    }

    private void resetForm()
    {
        txtDescription.Text = string.Empty;
        txtDescription.Focus();
    }

    public void removeReason(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int reasonId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            string reasonDescription = dataItem.Cells[2].Text;

            // first check if this reason is already assigned

            if (Timesheet.reasonTypeExistInTimesheets(reasonId))
            {
                MessageAlert.RadAlert("Desole, vous ne pouvez pas supprimer le motif : " + reasonDescription.ToUpper()
                    + ", car il a été déja affecté a une absence !", 350, 200, "Information", null, "../images/error.png");
            }
            else
            {
                Timesheet.deleteReasonById(reasonId);
                //refresh data of the gridview
                gridReason.Rebind();
                MessageAlert.RadAlert("Succès !", 350, 200, "Information", null, "../images/success_check.png");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void gridReason_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<Timesheet> listResult = Timesheet.getListAllReasonWithoutUndefined();
        gridReason.DataSource = listResult;
    }

    protected void gridReason_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridReason_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridReason.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}
