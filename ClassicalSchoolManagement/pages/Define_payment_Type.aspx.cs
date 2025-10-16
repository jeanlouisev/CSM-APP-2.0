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



public partial class Define_payment_Type : System.Web.UI.Page
{
    string sqlPaymentNextval = @"select nextval_payment('codeSeq') as payment_nextval";

    /*  protected void Page_Load(object sender, EventArgs e)
      {
          //for telerik activation purpose
          HttpContext.Current.Items["RadControlRandomNumber"] = 0;
          if (!IsPostBack)
          {
              resetTypeForm();
              BindDataGriPaymentType();
          }

          //Txtpercentage_Inscription.Text = "100";
          //Txtpercentage_CommingFee.Text = "100";
          //Txtpercentage_Monthly.Text = "100";
      }*/

    protected void btnAddPayment_Click(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Error :  Veuillez saisir le type de paiement!", 300, 150, "Information", null);
            txtDescription.Focus();
        }
        else if (txtAmount.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Error :  Veuillez saisir le montant !", 300, 150, "Information", null);
            txtDescription.Focus();
        }
        else if (Double.Parse(txtAmount.Text.Trim()) <= 0)
        {
            MessageAlert.RadAlert("Error :  Le montant doit etre superieur a zero (0) !", 300, 150, "Information", null);
            txtAmount.Text = string.Empty;
            txtDescription.Focus();
        }
        else
        {
            Payments p = new Payments();
            p.id = Universal.getUniversalSequence(sqlPaymentNextval);
            p.description = txtDescription.Text.Trim();
            p.amount = Double.Parse(txtAmount.Text.Trim());

            //check name
            bool checkType = Payments.checkExistedPaymentTypeByDescription(p.description, 2);
            if (!checkType)
            {
                Payments.addPaymentType(p);

                resetTypeForm();
                BindDataGriPaymentType();
                //}
                //else
                //{
                //    txtDescription.Focus();
                //    MessBox.Show("Failed !");
                //}
            }
            else
            {
                MessageAlert.RadAlert("Le type de paiement " + p.description + " existe. Veuillez ajouter un autre.", 300, 150, "Information", null);
            }
        }
    }

    protected void btnAddPaymentSpeciale_Click(object sender, EventArgs e)
    {
    }

    private void loadListTypeSpecial()
    {
        Payments p = new Payments();
        List<Payments> listPayment = Payments.getAllPaymentType(p);
        if (listPayment.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            ddlTypeSpecial.DataValueField = "id";
            ddlTypeSpecial.DataTextField = "description";
            ddlTypeSpecial.DataSource = listPayment;
            ddlTypeSpecial.DataBind();
            //  ddlType.Items.Insert(0, new DropDownListItem("MENSUEL", "-2"));
            //  ddlType.Items.Insert(0, new DropDownListItem("VERSEMENT", "-3"));
            ddlTypeSpecial.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            ddlTypeSpecial.SelectedValue = "-1";
        }
    }

    protected void ddlTypeSpecial_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Txtpercentage_Inscription.Text = string.Empty;
        Txtpercentage_CommingFee.Text = string.Empty;
        Txtpercentage_Monthly.Text = string.Empty;
        //

        if (ddlTypeSpecial.SelectedValue == "-2"
            && ddlTypeSpecial.SelectedText == "BOURSE")
        {
            int specialtype_id = Convert.ToInt32(ddlTypeSpecial.SelectedValue);
            List<Payments> listspecialPayment = Payments.getListspecialPaymentById(specialtype_id);
            if (listspecialPayment != null && listspecialPayment.Count > 0)
            {
                Txtpercentage_Inscription.Text = Convert.ToString(listspecialPayment[0].inscriptionFee);
                Txtpercentage_CommingFee.Text = Convert.ToString(listspecialPayment[0].entreeFee);
                Txtpercentage_Monthly.Text = Convert.ToString(listspecialPayment[0].monthlyFee);
            }
        }
        else
        {
            int specialtype_id = Convert.ToInt32(ddlTypeSpecial.SelectedValue);

            List<Payments> listspecialPayment = Payments.getListspecialPaymentById(specialtype_id);
            if (listspecialPayment != null && listspecialPayment.Count > 0)
            {
                Txtpercentage_Inscription.Text = Convert.ToString(listspecialPayment[0].inscriptionFee);
                Txtpercentage_CommingFee.Text = Convert.ToString(listspecialPayment[0].entreeFee);
                Txtpercentage_Monthly.Text = Convert.ToString(listspecialPayment[0].monthlyFee);
            }
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    private void resetTypeForm()
    {
        txtDescription.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtDescription.Focus();
    }

    private void BindDataGriPaymentType()
    {
        Payments p = new Payments();
        // string description = txtDescription.Text.Trim().Length <= 0 ? "%" : txtDescription.Text.Trim().ToLower() + "%";
        List<Payments> listResult = Payments.getAllPaymentType(p);

        if (listResult.Count > 0)
        {
            lblFound.Visible = false;
            pnlResult.Visible = true;
            lblCounter.Visible = true;
            lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
            lblTotalAmount.Visible = true;
            lblTotalAmount.Text = "Montant Total : " + listResult[0].total_amount + " Gdes";
        }
        else
        {
            pnlResult.Visible = true;
            lblCounter.Visible = false;
            lblTotalAmount.Visible = false;
        }
        gridListPaymentType.DataSource = listResult;
        gridListPaymentType.DataBind();
    }

    private void BindDataGriPaymentSpecialType()
    {
        /* // string description = txtDescription.Text.Trim().Length <= 0 ? "%" : txtDescription.Text.Trim().ToLower() + "%";
         List<Payment> listResult = Payment.getAllPaymentType();

         if (listResult.Count > 0)
         {
             lblFound.Visible = false;
             pnlResult.Visible = true;
             lblCounter.Visible = true;
             lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
             lblTotalAmount.Visible = true;
             lblTotalAmount.Text = "Montant Total : " + listResult[0].total_amount + " Gdes";
         }
         else
         {
             lblFound.Visible = true;
             pnlResult.Visible = true;
             lblCounter.Visible = false;
             lblTotalAmount.Visible = false;
         }
         gridListPaymentType.DataSource = listResult;
         gridListPaymentType.DataBind();*/
    }

    protected void gridListPaymentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListPaymentType.PageIndex = e.NewPageIndex;
        BindDataGriPaymentType();
    }

    protected void gridListPaymentType_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListPaymentType.Rows[index];
        //string code = row.Cells[0].Text;

    }

    protected void gridListPaymentType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindDataGriPaymentType();
    }

    protected void gridListPaymentType_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                // e.Row.Style.Add("height", "50px");
                e.Row.Style.Add("vertical-align", "bottom");
        }
    }

    public void removePaymentType(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListPaymentType.Rows[index];
                Label labelPaymentId = (row.Cells[3].FindControl("lblPaymentTypeId") as Label);
                string paymentName = row.Cells[1].Text;
                int payment_type_id = int.Parse(labelPaymentId.Text);
                // first check if this payment is already assigned
                List<Payments> listPayment = Payments.getListPaymentByType(payment_type_id, 2);
                if (listPayment.Count > 0)
                {
                    MessageAlert.RadAlert("Desole, vous ne pouvez pas supprimer " + paymentName
                        + ", car il a ete deja affecte a un paiement !", 300, 200, "Information", null);
                }
                else
                {
                    Payments.deletePaymentType(payment_type_id);
                    //refresh data of the gridview
                    BindDataGriPaymentType();
                }

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }
}