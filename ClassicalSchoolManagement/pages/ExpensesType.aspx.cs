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



public partial class ExpensesType : System.Web.UI.Page
{
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
            txtdescription.Text = string.Empty;
            lblErrordescription.Visible = false;
            BindDataGridExpended();
        }

    }

    protected void btnAddExpensesType_Click(object sender, EventArgs e)
    {
        try
        {
            if (validateFields())
            {
                Expenditure p = new Expenditure();
                //get the values from the form                  
                p.description = txtdescription.Text.Trim();
                string description_ = txtdescription.Text.Trim();
                //check if not yet exist
                bool checkdesciption = Expenditure.checkExistedexpenseTypeByDescription(description_);

                if (checkdesciption)
                {
                    lblErrordescription.ForeColor = System.Drawing.Color.Red;
                    lblErrordescription.Text = "Desoler! Cette description a ete deja ajouter";

                }
                else
                {
                    Expenditure.addExpenseType(p);
                    txtdescription.Text = string.Empty;
                    lblErrordescription.Text = string.Empty;
                    lblErrordescription.Visible = false;
                    BindDataGridExpended();
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public bool validateFields()
    {
        bool result = true;

        if (txtdescription.Text.Trim().Length <= 0
              || txtdescription.Text.Trim().ToString() == string.Empty)
        {
            lblErrordescription.Text = " veuillez saisir une description !";
            txtdescription.Focus();
            result = false;
        }
        return result;
    }

    private void BindDataGridExpended()
    {
        List<Expenditure> listResult = Expenditure.getAllExpenseType();

        if (listResult.Count > 0)
        {
            pnlResult.Visible = true;
            lblCounter.Visible = true;
            // lblTotalAmount.Visible = true;
            lblCounter.Text = listResult.Count.ToString() + " Ligne (s)";
        }
        else
        {
            pnlResult.Visible = false;
            lblCounter.Visible = false;
        }
        gridListExpende.DataSource = listResult;
        gridListExpende.DataBind();
    }

    protected void gridListExpende_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // gridListExpende.PageIndex = e.NewPageIndex;
        // BindDataGridCourse();
    }

    protected void gridListExpende_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListExpende.Rows[index];


    }

    protected void gridListExpende_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  BindDataGridCourse();
    }

    protected void gridListExpende_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowIndex == 0)
        //        e.Row.Style.Add("height", "50px");
        //    e.Row.Style.Add("vertical-align", "bottom");
        //}
    }

    public void removeExpendeType(object sender, EventArgs e)
    {
        try
        {

            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListExpende.Rows[index];
                string ExpenseName = row.Cells[1].Text;
                Label labelExpenseId = (row.Cells[2].FindControl("lblExpenseTypeId") as Label);
                int id_Expended = int.Parse(labelExpenseId.Text);
                bool listExpenditure = Expenditure.checkIfDepenseTypeAlreadyAfected(id_Expended);
                if (listExpenditure)
                {
                    MessageAlert.RadAlert("Desole, vous ne pouvez pas supprimer " + ExpenseName
                      + ", car il a ete deja affecte a une depense !", 300, 200, "Information", null);
                }
                else
                {
                    Expenditure.deleteExpenseType(Convert.ToInt32(id_Expended));
                    //refresh the grid view
                    BindDataGridExpended();
                }

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        // call javaScript to close the window from code behind
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseDialog();", true);
    }
}