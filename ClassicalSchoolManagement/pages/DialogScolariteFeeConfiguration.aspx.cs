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



public partial class DialogScolariteFeeConfiguration : System.Web.UI.Page
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
            loadListAcademicYear(ddlAcademicYear);
            // loadListAcademicYearStart(ddlAcademicYearStart);
            // loadListAcademicYearEnd(ddlAcademicYearEnd);
            Session["academic_year_ID"] = int.Parse(ddlAcademicYear.SelectedValue);
            loadListClassroom();
            BindDataGridPayment();

            // hide versement fields
            trVersmnt1.Visible = false;
            trVersmnt2.Visible = false;
        }

    }

    private void loadListAcademicYear(RadDropDownList ddl)
    {
        try
        {
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();
            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
            }
            else
            {
                ddl.Items.Clear();
                ddl = null;
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        BindDataGridPayment();
    }

    protected void btnAddPayment_Click(object sender, EventArgs e)
    {

        //check choice on combo Type paiement

        if (ddlType.SelectedValue == "-2" && ddlType.SelectedText == "MENSUEL")
        {
            if (validatemonthFields())
            {
                savingPayments();
            }
        }
        else if (ddlType.SelectedValue == "-3" && ddlType.SelectedText == "VERSEMENT")
        {
            if (validateVersementFields())
            {
                savingPayments();
            }
        }


    }

    protected void btnSearchPayment_Click(object sender, EventArgs e)
    {

        BindDataGridPayment();

    }

    protected void btnModifyPayment_Click(object sender, EventArgs e)
    {

        //check choice on combo Type paiement

        if (ddlType.SelectedValue == "-2" && ddlType.SelectedText == "MENSUEL")
        {
            if (validatemonthFields())
            {
                ModifyPayments();
            }
        }
        else if (ddlType.SelectedValue == "-3" && ddlType.SelectedText == "VERSEMENT")
        {
            if (validateVersementFields())
            {

                ModifyPayments();
            }
        }


    }

    protected void btnCancelPayment_Click(object sender, EventArgs e)
    {
        resetForm();
        pnlAddCours.GroupingText = "Configurer Scolarite";
        txtClassroomAmount.ReadOnly = false;
        //txtClassroomAmount.BackColor = System.Drawing.Color.White;
        txtClassroomAmount.EmptyMessage = "0.00";
        lblversement_1.Visible = false;
        txtVersemet1.Visible = false;
        lblversement_2.Visible = false;
        txtVersemet2.Visible = false;
        lblversement_3.Visible = false;
        txtVersemet3.Visible = false;
        lblversement_4.Visible = false;
        txtVersemet4.Visible = false;
        btnAddPayment.Visible = true;
        btnModifyPayment.Visible = false;
        btnCancelPayment.Visible = false;
    }

    private bool ModifyPayments()
    {
        try
        {
            Payments p = new Payments();
            // get the values from the form
            //p.id_Special_payment = int.Parse(hiddenPaymentId.Value);
            p.id_classroom = int.Parse(ddlClassroom.SelectedValue);
            p.vacation = ddlVacation.SelectedValue;
            p.payment_type = ddlType.SelectedValue;
            p.classroom_amount = txtClassroomAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtClassroomAmount.Text.Trim());
            p.penality_amount = txtPenalityAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtPenalityAmount.Text.Trim());
            p.penality_start_day = txtStartDay.Text.Trim();
            p.penality_end_day = txtEndDay.Text.Trim();
            p.versement_1 = txtVersemet1.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet1.Text.Trim().ToString());
            p.versement_2 = txtVersemet2.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet2.Text.Trim().ToString());
            p.versement_3 = txtVersemet3.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet3.Text.Trim().ToString());
            p.versement_4 = txtVersemet4.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet4.Text.Trim().ToString());
            p.inscriptionFee = txtincriptionfee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtincriptionfee.Text.Trim().ToString());
            p.entreeFee = txtentreefee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtentreefee.Text.Trim().ToString());

            Payments.ModifyPaymentConfiguration(p);
            
                MessageAlert.RadAlert("Paiement modifer avec success !", 300, 150, "Information", null);
                resetForm();
                btnAddPayment.Visible = true;
                btnModifyPayment.Visible = false;
                btnCancelPayment.Visible = false;
                btnSearchPayment.Visible = true;
                pnlAddCours.GroupingText = "Configurer Scolarite";
                BindDataGridPayment();
            //}
            //else
            //{
            //    MessageAlert.RadAlert("Failed !", 300, 150, "Information", null);
            //}

        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
        return true;
    }

    private void loadListAcademicYearStart(RadDropDownList dropDownList)
    {
        List<Settings> listClassroom = Settings.getListAcademicYearStart();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            dropDownList.DataValueField = "id";
            dropDownList.DataTextField = "start_date";
            dropDownList.DataSource = listClassroom;
            dropDownList.DataBind();
            int currentAcademicYear = Settings.getAcademicYear();
            dropDownList.SelectedValue = currentAcademicYear.ToString();

        }
    }

    private void loadListAcademicYearEnd(RadDropDownList dropDownList)
    {
        List<Settings> listClassroom = Settings.getListAcademicYearEnd();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            dropDownList.DataValueField = "id";
            dropDownList.DataTextField = "end_date";
            dropDownList.DataSource = listClassroom;
            dropDownList.DataBind();
            int currentAcademicYear = Settings.getAcademicYear();
            dropDownList.SelectedValue = currentAcademicYear.ToString();

        }
    }

    public void removepayment(object sender, EventArgs e)
    {
        try
        {

            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //  MessageAlert.RadAlert("lap delete", 300, 150, "Information", null);
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListPayment.Rows[index];
                Label lblPaiementeId = (row.Cells[11].FindControl("labelpaymentId") as Label);
                int id = int.Parse(gridListPayment.DataKeys[index].Value.ToString());
                int id_Special_payment = int.Parse(lblPaiementeId.Text);
                //first check if this Payment is already assigned
                bool CheckIfPaymentAssgn = Payments.Check_If_PaymentAssign(id_Special_payment);
                if (!CheckIfPaymentAssgn)
                {

                    Payments.deletePayment_configuration(id);
                    //refresh data of the gridview
                    BindDataGridPayment();
                }
                else

                {

                    MessageAlert.RadAlert("Desoler ! Ce payment est assigne a un eleve!", 300, 150, "Information", null);

                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private bool savingPayments()
    {
        try
        {
            Payments p = new Payments();
            // get the values from the form
            // p.id_Special_payment = int.Parse(hiddenPaymentId.Value);
            p.id_classroom = int.Parse(ddlClassroom.SelectedValue);
            p.vacation = ddlVacation.SelectedValue;
            p.payment_type = ddlType.SelectedValue;
            p.classroom_amount = txtClassroomAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtClassroomAmount.Text.Trim());
            p.penality_amount = txtPenalityAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtPenalityAmount.Text.Trim());
            p.penality_start_day = txtStartDay.Text.Trim();
            p.penality_end_day = txtEndDay.Text.Trim();
            p.versement_1 = txtVersemet1.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet1.Text.Trim().ToString());
            p.versement_2 = txtVersemet2.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet2.Text.Trim().ToString());
            p.versement_3 = txtVersemet3.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet3.Text.Trim().ToString());
            p.versement_4 = txtVersemet4.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet4.Text.Trim().ToString());
            p.inscriptionFee = txtincriptionfee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtincriptionfee.Text.Trim().ToString());
            p.entreeFee = txtentreefee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtentreefee.Text.Trim().ToString());
            p.academic_year = int.Parse(ddlAcademicYear.SelectedValue);
            //Session["academic_year_ID"] = p.academic_year;

            //txtentreefee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtentreefee.Text.Trim().ToString());

            //
            //check if payment is already assign for selected classroom
            if (Payments.paymentConfigurationExist(p))
            {
                // get the name of the classroom
                string _classroom = ddlClassroom.SelectedText;
                string _vacation = ddlVacation.SelectedText;
                string _type = ddlType.SelectedText;
                MessageAlert.RadAlert("Desole, la classe " + _classroom
                                        + " de vacation " + _vacation + " a  deja ete configuree. Veuillez la modifier !",
                                        300, 150, "Information", null);
            }
            else
            {
                Payments.addPaymentConfiguration(p);

                MessageAlert.RadAlert("Paiement ajouter avec success !", 300, 150, "Information", null);
                resetForm();
                BindDataGridPayment();
                //}
                //else
                //{
                //    MessageAlert.RadAlert("Failed !", 300, 150, "Information", null);
                //}
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
        return true;
    }

    private void BindDataGridPayment()
    {
        Payments p = new Payments();
        p.id_classroom = int.Parse(ddlClassroom.SelectedValue);
        p.vacation = ddlVacation.SelectedValue == "-1" ? "%%" : ddlVacation.SelectedValue;
        p.payment_type = ddlType.SelectedValue;
        p.classroom_amount = txtClassroomAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtClassroomAmount.Text.Trim());
        p.penality_amount = txtPenalityAmount.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtPenalityAmount.Text.Trim());
        p.versement_1 = txtVersemet1.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet1.Text.Trim().ToString());
        p.versement_2 = txtVersemet2.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet2.Text.Trim().ToString());
        p.versement_3 = txtVersemet3.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet3.Text.Trim().ToString());
        p.versement_4 = txtVersemet4.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtVersemet4.Text.Trim().ToString());
        p.inscriptionFee = txtincriptionfee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtincriptionfee.Text.Trim().ToString());
        p.entreeFee = txtentreefee.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtentreefee.Text.Trim().ToString());
        p.academic_year = int.Parse(ddlAcademicYear.SelectedValue);

        try
        {
            //get list of payment configuration
            List<Payments> listResult = Payments.getListPaymentConfiguration(p);
            if (listResult.Count > 0)
            {
                lblFound.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
            }
            else
            {
                lblFound.Visible = true;
                pnlResult.Visible = true;
                lblCounter.Visible = false;
            }
            gridListPayment.DataSource = listResult;
            gridListPayment.DataBind();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "-2" && ddlType.SelectedText == "MENSUEL")
        {
            resetForm();
            pnlAddCours.GroupingText = "Configurer Scolarite";
            txtClassroomAmount.ReadOnly = false;
            //txtClassroomAmount.BackColor = System.Drawing.Color.White;
            txtClassroomAmount.EmptyMessage = "0.00";
            // hide versement fields
            trVersmnt1.Visible = false;
            trVersmnt2.Visible = false;



        }
        else if (ddlType.SelectedValue == "-3" && ddlType.SelectedText == "VERSEMENT")
        {
            resetForm();
            pnlAddCours.GroupingText = "Configurer Scolarite";
            txtClassroomAmount.ReadOnly = true;
            // txtClassroomAmount.BackColor = System.Drawing.Color.LightGray;
            txtClassroomAmount.EmptyMessage = null;
            // show versement fields
            trVersmnt1.Visible = true;
            trVersmnt2.Visible = true;

        }
    }

    protected void gridListPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridListPayment.PageIndex = e.NewPageIndex;
        BindDataGridPayment();
    }

    protected void gridListPayment_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gridListPayment.Rows[index];
        int id = int.Parse(gridListPayment.DataKeys[index].Value.ToString());
        if (e.CommandName == "EdithPaiement")
        {
            try
            {
                List<Payments> listPayment = Payments.getPaymentById(id);

                if (listPayment != null && listPayment.Count > 0)
                {
                    btnAddPayment.Visible = false;
                    btnModifyPayment.Visible = true;
                    btnSearchPayment.Visible = false;

                    pnlAddCours.GroupingText = "Modifier Scolarite";
                    // hiddenPaymentId.Value = id.ToString();
                    //load value in combo and textbox
                    ddlClassroom.SelectedValue = listPayment[0].id_classroom.ToString();
                    ddlVacation.SelectedValue = listPayment[0].vacation.ToString();
                    txtincriptionfee.Text = listPayment[0].inscriptionFee.ToString();
                    txtentreefee.Text = listPayment[0].entreeFee.ToString();
                    ddlType.SelectedValue = listPayment[0].payment_type.ToString();
                    txtClassroomAmount.Text = listPayment[0].classroom_amount.ToString();
                    txtPenalityAmount.Text = listPayment[0].penality_amount.ToString();
                    txtStartDay.Text = listPayment[0].penality_start_day.ToString();
                    txtEndDay.Text = listPayment[0].penality_end_day.ToString();

                    txtVersemet1.Text = listPayment[0].versement_1.ToString();
                    txtVersemet2.Text = listPayment[0].versement_2.ToString();
                    txtVersemet3.Text = listPayment[0].versement_3.ToString();
                    txtVersemet4.Text = listPayment[0].versement_4.ToString();


                    if (ddlType.SelectedValue == "-3")
                    {
                        // txtClassroomAmount.BackColor = System.Drawing.Color.LightGray;
                        txtClassroomAmount.ReadOnly = true;
                        lblversement_1.Visible = true;
                        txtVersemet1.Visible = true;
                        lblversement_2.Visible = true;
                        txtVersemet2.Visible = true;
                        lblversement_3.Visible = true;
                        txtVersemet3.Visible = true;
                        lblversement_4.Visible = true;
                        txtVersemet4.Visible = true;
                        MessageAlert.RadAlert("WHAT THE FUCKKKKKKKKKKKKKKKKKKKKKKKKK!", 300, 150, "Information", null);

                    }
                    else if (ddlType.SelectedValue == "-2")
                    {
                        //txtClassroomAmount.BackColor = System.Drawing.Color.White;
                        txtClassroomAmount.ReadOnly = false;
                        lblversement_1.Visible = false;
                        txtVersemet1.Visible = false;
                        lblversement_2.Visible = false;
                        txtVersemet2.Visible = false;
                        lblversement_3.Visible = false;
                        txtVersemet3.Visible = false;
                        lblversement_4.Visible = false;
                        txtVersemet4.Visible = false;

                    }

                }


            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }


    }

    protected void gridListPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                //    e.Row.Style.Add("height", "50px");
                e.Row.Style.Add("vertical-align", "bottom");
        }
    }

    public void removePaymentConfiguration(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                GridViewRow row = gridListPayment.Rows[index];
                int id = int.Parse(gridListPayment.DataKeys[index].Value.ToString());
                Payments.deletePayment(id);
                //refresh data of the gridview
                BindDataGridPayment();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadListClassroom()
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ddlClassroom.DataTextField = "name";
        ddlClassroom.DataValueField = "id";
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void ddlType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {

    }

    private bool validatemonthFields()
    {
        if (ddlClassroom.SelectedValue == "-1"
            || ddlClassroom.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner une classe !", 300, 150, "Information", null);
            ddlClassroom.Focus();
            return false;
        }
        if (ddlVacation.SelectedValue == "-1"
          || ddlVacation.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner une vacation !", 300, 150, "Information", null);
            ddlVacation.Focus();
            return false;
        }
        if (ddlType.SelectedValue == "-1"
        || ddlType.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner un type de paiement !", 300, 150, "Information", null);
            ddlType.Focus();
            return false;
        }
        if (txtClassroomAmount.Text.Trim() == string.Empty
         || txtClassroomAmount.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Erreur : veuillez saisir le prix de la classe !", 300, 150, "Information", null);
            txtClassroomAmount.Focus();
            return false;
        }
        /*  if (txtPenalityAmount.Text.Trim() == string.Empty
              || txtPenalityAmount.Text.Trim().Length <= 0)
          {
              MessageAlert.RadAlert("Erreur : veuillez saisir le montant de la penalite !", 300, 150, "Information", null);
              txtPenalityAmount.Focus();
              return false;
          }
          if (txtPenalityAmount.Text.Trim() == string.Empty
            || txtPenalityAmount.Text.Trim().Length <= 0)
          {
              MessageAlert.RadAlert("Erreur : veuillez saisir le montant de la penalite !", 300, 150, "Information", null);
              txtPenalityAmount.Focus();
              return false;
          }
          if (txtStartDay.Text.Trim() == string.Empty
          || txtStartDay.Text.Trim().Length <= 0)
          {
              MessageAlert.RadAlert("Erreur : veuillez saisir le jour debutant la penality!", 300, 150, "Information", null);
              txtStartDay.Focus();
              return false;
          }
          if (txtEndDay.Text.Trim() == string.Empty
                  || txtEndDay.Text.Trim().Length <= 0)
          {
              MessageAlert.RadAlert("Erreur : veuillez saisir le jour final la penality!", 300, 150, "Information", null);
              txtEndDay.Focus();
              return false;
          }*/

        return true;
    }

    private bool validateVersementFields()
    {
        if (ddlClassroom.SelectedValue == "-1"
            || ddlClassroom.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner une classe !", 300, 150, "Information", null);
            ddlClassroom.Focus();
            return false;
        }
        if (ddlVacation.SelectedValue == "-1"
          || ddlVacation.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner une vacation !", 300, 150, "Information", null);
            ddlVacation.Focus();
            return false;
        }
        if (ddlType.SelectedValue == "-1"
        || ddlType.SelectedText == "--Tout Sélectionner--")
        {
            MessageAlert.RadAlert("Erreur : veuillez selectionner un type de paiement !", 300, 150, "Information", null);
            ddlType.Focus();
            return false;
        }

        if (txtVersemet1.Text.Trim() == string.Empty
       || txtVersemet1.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Erreur : veuillez saisir le montant du premier versement !", 300, 150, "Information", null);
            txtClassroomAmount.Focus();
            return false;
        }
        if (txtVersemet2.Text.Trim() == string.Empty
     || txtVersemet2.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Erreur : veuillez saisir le montant du second versement !", 300, 150, "Information", null);
            txtClassroomAmount.Focus();
            return false;
        }
        if (txtVersemet3.Text.Trim() == string.Empty
         || txtVersemet3.Text.Trim().Length <= 0)
        {
            MessageAlert.RadAlert("Erreur : veuillez saisir le montant du troisième versement !", 300, 150, "Information", null);
            txtClassroomAmount.Focus();
            return false;
        }







        return true;
    }

    private void resetForm()
    {
        ddlClassroom.SelectedValue = "-1";
        ddlVacation.SelectedValue = "-1";
        //ddlType.SelectedValue = "-2";
        txtClassroomAmount.Text = string.Empty;
        txtPenalityAmount.Text = string.Empty;
        txtincriptionfee.Text = string.Empty;
        txtentreefee.Text = string.Empty;
        txtVersemet1.Text = string.Empty;
        txtVersemet2.Text = string.Empty;
        txtVersemet3.Text = string.Empty;
        txtVersemet4.Text = string.Empty;
        txtStartDay.Text = string.Empty;
        txtEndDay.Text = string.Empty;
    }

    protected void txtStartDay_TextChanged(object sender, EventArgs e)
    {
        if (txtStartDay.Text.Trim().Length > 0
            || txtStartDay.Text.Trim() != string.Empty)
        {
            //compare the two values
            evaluatePenalityDuration();
        }
    }

    protected void txtEndDay_TextChanged(object sender, EventArgs e)
    {
        if (txtEndDay.Text.Trim().Length > 0
            || txtEndDay.Text.Trim() != string.Empty)
        {
            //compare the two values
            evaluatePenalityDuration();
        }
    }

    private void evaluatePenalityDuration()
    {

        int _startDay = txtStartDay.Text.Trim().Length <= 0 ? 0 : int.Parse(txtStartDay.Text.Trim());
        int _endDay = txtEndDay.Text.Trim().Length <= 0 ? 0 : int.Parse(txtEndDay.Text.Trim());
        if (_startDay > 31)
        {
            MessageAlert.RadAlert("Erreur : la date debutant la penalite ne doit pas depasser 31 !", 300, 150, "Information", null);
            txtStartDay.Focus();
            return;
        }
        else if (_endDay > 31)
        {
            MessageAlert.RadAlert("Erreur : la date final de la penalite ne doit pas depasser 31 !", 300, 150, "Information", null);
            txtStartDay.Focus();
            return;
        }
    }

    protected void txtClassroomAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtClassroomAmount.Text.Trim().Length > 0)
        {
            // get the class amount
            double _classAmount = Double.Parse(txtClassroomAmount.Text.Trim());
            // calculate 10% from class amount
            double _10percent = _classAmount * 0.1;
            //add 10 percent as new penality value
            txtPenalityAmount.Text = _10percent.ToString();
            // set default value for penality time_interval
            txtStartDay.Text = 1.ToString();
            txtEndDay.Text = 5.ToString();
        }
        else
        {
            // empty the textboxes
            txtPenalityAmount.Text = string.Empty;
            txtStartDay.Text = string.Empty;
            txtEndDay.Text = string.Empty;
        }
    }

    protected void txtInscription_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtentrer_TextChanged(object sender, EventArgs e)
    {

    }
}