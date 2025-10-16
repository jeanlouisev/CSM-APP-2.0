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



public partial class Add_New_SMS : System.Web.UI.Page
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
            BindDataGridContent();
            /*loadPaiementInformation();
             lblContentCode.Text = Session["student_Code"].ToString();
             lblContentFullName.Text = Session["student_FullName"].ToString();
             lblContentClassName.Text = Session["student_Class"].ToString();
             lblContenPrivilege.Text = Session["student_Privillege"].ToString();
             lblcontentAnne.Text = Session["student_academic_year"].ToString();
             Check_balance_payments();
             Validate_And_Enable_filelds();*/
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
    }

    protected void btnAddNewSmsContent_Click(object sender, EventArgs e)
    {
        try
        {
            //BindDataGridContacts();

            if (txtMessage.Text.Trim().Length <= 0)
            {
                lblErrorSmsmEmpty.Text = "Veuillez saisir un contenu !";
                lblErrorSmsmEmpty.ForeColor = System.Drawing.Color.Red;
                lblErrorSmsmEmpty.Visible = true;
                txtMessage.Focus();
            }
            else
            {
                Universal p = new Universal();
                p.message_content = txtMessage.Text.Trim();
                Universal.addMessage(p);

                lblErrorSmsmEmpty.Text = "Contenu ajouter avec succès !";
                lblErrorSmsmEmpty.ForeColor = System.Drawing.Color.Green;
                lblErrorSmsmEmpty.Visible = true;
                txtMessage.Text = string.Empty;
                txtMessage.Focus();
                BindDataGridContent();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void gridListContent_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListContent.Rows[index];
        //string code = row.Cells[0].Text;

    }

    private void BindDataGridContent()
    {
        List<Universal> listResult = Universal.getListSmsContent();
        //
        if (listResult.Count > 0)
        {
            lblFound.Visible = false;
            pnlResult.Visible = true;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count + " Ligne(s)";
            //  tblGridHeader.Visible = true;
        }
        else
        {
            lblFound.Visible = true;
            pnlResult.Visible = true;
            lblCounter.Visible = false;
            //  tblGridHeader.Visible = false;
        }
        gridListContent.DataSource = listResult;
        gridListContent.DataBind();


    }

    protected void gridListContent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindDataGridContent();
    }

    protected void gridListContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string onmouseoverStyle = "this.style.backgroundColor='whitesmoke'";
            string onmouseoutStyle = "this.style.backgroundColor='white'";
            e.Row.Attributes.Add("onmouseover", onmouseoverStyle);
            e.Row.Attributes.Add("onmouseout", onmouseoutStyle);
        }

    }

    public void removeContent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListContent.Rows[index];
                int contentId = int.Parse(gridListContent.DataKeys[index].Value.ToString());
                Universal.deleteContent(contentId);
                BindDataGridContent();
            }

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }





    }

    protected void txtMessage_TextChanged(object sender, EventArgs e)
    {
        if (lblErrorSmsmEmpty.Visible)
        {
            lblErrorSmsmEmpty.Visible = false;
        }
    }
}