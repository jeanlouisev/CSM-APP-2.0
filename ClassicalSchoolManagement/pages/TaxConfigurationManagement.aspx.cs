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


public partial class TaxConfigurationManagement : System.Web.UI.Page
{
    int menu_code = 11; // parameter menu code, for more information see "menu" table.
    string msgContent = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        if (!IsPostBack)
        {
            //insertDefaultTaxConfiguration();
            txtGroupName.Focus();
        }
    }

    //private void insertDefaultTaxConfiguration()
    //{
    //    Users user = Session["user"] as Users;

    //    Salary sal = new Salary();
    //    sal.group_name = "DEFAULT_TAX";
    //    sal.ona = 0;
    //    sal.iri = 0;
    //    sal.fdu = 0;
    //    sal.cas = 0;
    //    sal.fixed_tax = 0;
    //    sal.login_user_id = user.id;

    //    //check name
    //    bool checkType = Salary.taxGroupNameExist(sal.group_name);
    //    if (!checkType)
    //    {
    //        Salary.addNewTax(sal);
    //    }
    //}

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    txtGroupName.ReadOnly = true;
        //    txtFixedTaxAmount.ReadOnly = true;

        //    // loop through the grid to disable edit option
        //    if (gridListTax.Visible && gridListTax.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListTax.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListTax.Rows[i].Cells[3].FindControl("btnEdit") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur :" + ex.Message);
        //}
    }

    // delete
    private void disableDeleteOption()
    {
        //try
        //{
        //    // loop through the grid to disable delete option
        //    if (gridListTax.Visible && gridListTax.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListTax.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListTax.Rows[i].Cells[4].FindControl("btnDelete") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur : " + ex.Message);
        //}
    }

    /******************************************* END USER POLICY **************************/
    protected void btnAddTaxe_Click(object sender, EventArgs e)
    {
        if (txtGroupName.Text.Trim().Length <= 0)
        {
            msgContent = "Erreur :Veuillez saisir le nom du groupe  !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            txtGroupName.Focus();
        }
        else
        {
            Users user = Session["user"] as Users;
            //
            Salary sal = new Salary();
            sal.group_name = txtGroupName.Text.Trim().ToUpper();
            sal.ona = chkONA.Checked ? 1 : 0;
            sal.iri = chkIRI.Checked ? 1 : 0;
            sal.fdu = chkFDU.Checked ? 1 : 0;
            sal.cas = chkCAS.Checked ? 1 : 0;
            sal.fixed_tax = txtFixedTaxAmount.Value == null ? 0 : double.Parse(txtFixedTaxAmount.Value.ToString());
            sal.login_user_id = user.id;

            //check name
            bool checkType = Salary.taxGroupNameExist(sal.group_name);
            if (!checkType)
            {
                Salary.addNewTax(sal);
                txtGroupName.Text = "";
                txtFixedTaxAmount.Text = "";
                txtGroupName.Focus();
                // reload grid
                radGridTaxes.Rebind();

                msgContent = "Succès !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
            }
            else
            {
                msgContent = "Le nom " + sal.group_name + " existe. Veuillez ajouter un autren !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void radGridTaxes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<Salary> listResult = Salary.getListTax();
            radGridTaxes.DataSource = listResult;
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void radGridTaxes_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridTaxes_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridTaxes.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());

            // check if tax is attach to a staff
            List<Salary> listTax = Salary.getListAttachTaxById(id);
            if (listTax != null && listTax.Count > 0)
            {
                msgContent = "Désole vous ne pouvez supprimer ce groupe, car il est lié au salaire des personnels !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                // delete the group
                Salary.deleteTaxGroupById(id);
                //refresh data of the gridview
                radGridTaxes.Rebind();

                msgContent = "Succès !";
                MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }
}