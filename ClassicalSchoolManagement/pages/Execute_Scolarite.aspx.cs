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



    public partial class Execute_Scolarite : System.Web.UI.Page
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
                loadPaiementInformation();
                lblContentCode.Text = Session["student_Code"].ToString();
                lblContentFullName.Text = Session["student_FullName"].ToString();
                lblContentClassName.Text = Session["student_Class"].ToString();
                lblContenPrivilege.Text = Session["student_Privillege"].ToString();
                lblcontentAnne.Text = Session["student_academic_year"].ToString();
                Check_balance_payments();
                Validate_And_Enable_filelds();
            }
        }

        private void loadPaiementInformation()
        {
            try
            {
                int id_class = int.Parse(Session["id_classe"].ToString());
                int academic_year = int.Parse(Session["academic_year_id"].ToString());
                List<Payments> listpayment = Payments.getListPaymentConfigurationWithclassid(id_class, academic_year);
                string type_payment = listpayment[0].payment_type.ToString();
                int id = Convert.ToInt32(listpayment[0].id);
                Session["type_payment"] = type_payment;
                Session["payment_conf_id"] = id;
                if (listpayment != null && listpayment.Count > 0)
                {
                    txtInscriptionAmountPredefined.Text = listpayment[0].inscriptionFee.ToString();
                    txtFraisEntreeAmountPredefined.Text = listpayment[0].entreeFee.ToString();
                    //-2=MENSUEL and -3=VERSEMENT
                    if (type_payment == "-2")
                    {
                        pnl_versement.Visible = false;
                        pnl_mensuel.Visible = true;
                        tab_mensuel.Visible = true;
                        txtSeptemberAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtOctoberAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtNovemberAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtDecemberAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtJanuaryAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtFebuaryAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtMarchAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtAprilAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtMayAmountPredefined.Text = listpayment[0].classroom_amount.ToString();
                        txtJuneAmountPredefined.Text = listpayment[0].classroom_amount.ToString();

                    }
                    else
                    {
                        pnl_mensuel.Visible = false;
                        txtVersment_1.Text = listpayment[0].versement_1.ToString();
                        txtVersment_2.Text = listpayment[0].versement_2.ToString();
                        txtVersment_3.Text = listpayment[0].versement_3.ToString();
                        txtVersment_4.Text = listpayment[0].versement_4.ToString();
                        if (txtVersment_3.Text == "0")
                        {
                            txtVersement3.Enabled = false;
                        }
                        else if (txtVersment_4.Text == "0")
                        {
                            txtVersement4.Enabled = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }

        private void emptyFields()
        {
            //empty 

            txtInscriptionAmountPaid.Text = "";
            txtFraisEntreeAmountPaid.Text = "";
            txtSeptemberAmountPaid.Text = "";
            txtOctoberAmountPaid.Text = "";
            txtNovemberAmountPaid.Text = "";
            txtDecemberAmountPaid.Text = "";
            txtJanuaryAmountPaid.Text = "";
            txtFebuaryAmountPaid.Text = "";
            txtMarchAmountPaid.Text = "";
            txtAprilAmountPaid.Text = "";
            txtMayAmountPaid.Text = "";
            txtJuneAmountPaid.Text = "";
        }

        private bool Validate_And_Enable_filelds()
        {
            //SESSION FOR PAYMENT TYPE  -2=MENSUEL and -3=VERSEMENT  
            string type_payment = Session["type_payment"].ToString();
            bool result = true;

            if (txtInscriptionAmountPredefined.Value == Convert.ToInt32(lblInscriptionamountalreadyPaid.Text))
            {
                txtInscriptionAmountPaid.Enabled = false;
                txtFraisEntreeAmountPaid.Enabled = true;
                txtFraisEntreeAmountPaid.Focus();
                txtInscriptionAmountPaid.Text = Convert.ToString(txtInscriptionAmountPredefined.Value);
                txtInscriptionAmountPaid.ForeColor = Color.Blue;
                txtInscriptionAmountPaid.ReadOnly = true;
                checkinscription.Visible = true;
            }
            //Mensuel 
            if (txtFraisEntreeAmountPredefined.Value == Convert.ToInt32(lblEntreeFeeamountalreadyPaid.Text) && type_payment == "-2")
            {
                txtFraisEntreeAmountPaid.Enabled = false;
                txtSeptemberAmountPaid.Enabled = true;
                txtSeptemberAmountPaid.Focus();
                txtFraisEntreeAmountPaid.Text = Convert.ToString(txtFraisEntreeAmountPredefined.Value);
                txtFraisEntreeAmountPaid.ForeColor = Color.Blue;
                txtFraisEntreeAmountPaid.ReadOnly = true;
                checkEntree.Visible = true;
            }
            //Versement  
            if (txtFraisEntreeAmountPredefined.Value == Convert.ToInt32(lblEntreeFeeamountalreadyPaid.Text) && type_payment == "-3")
            {
                txtFraisEntreeAmountPaid.Enabled = false;
                //txtSeptemberAmountPaid.Enabled = true;
                // txtSeptemberAmountPaid.Focus();
                txtFraisEntreeAmountPaid.Text = Convert.ToString(txtFraisEntreeAmountPredefined.Value);
                txtFraisEntreeAmountPaid.ForeColor = Color.Blue;
                txtFraisEntreeAmountPaid.ReadOnly = true;
                checkEntree.Visible = true;
            }


            // For -2=MENSUEL  payments   //(txtFraisEntreeAmountPredefined.Value == Convert.ToInt32(lblEntreeFeeamountalreadyPaid.Text))

            if (type_payment == "-2")
            {

                /*if (lblContenPrivilege.Text == "AUCUN" && txtSeptemberAmountPredefined.Value == Convert.ToInt32(lblEntreeSeptemberalreadyPaid.Text))
                  {
                          txtSeptemberAmountPaid.Enabled = false;
                          txtOctoberAmountPaid.Enabled = true;
                          txtOctoberAmountPaid.Focus();
                          txtSeptemberAmountPaid.Text = Convert.ToString(txtSeptemberAmountPredefined.Value);
                          txtSeptemberAmountPaid.ForeColor = Color.Blue;
                          txtSeptemberAmountPaid.ReadOnly = true;
                          checkseptember.Visible = true;
                          lblErrorAmount.Visible = false;
                      
                  }
                  else if (lblContenPrivilege.Text == "DEMI-BOURSE" && txtSeptemberAmountPredefined.Value == Convert.ToInt32(lblEntreeSeptemberalreadyPaid.Text))
                  {

                  }
                  else if (lblContenPrivilege.Text == "BOURSE" && txtSeptemberAmountPredefined.Value == Convert.ToInt32(lblEntreeSeptemberalreadyPaid.Text))
                  {

                  }*/


            }

            else if (type_payment == "-3") //-3=VERSEMENT 
            {


            }
            return result;
        }

        private Payments validatePayments(int amountWillPay, string description,
            RadNumericTextBox txtAmountPredefined, RadNumericTextBox txtAmountPaid)
        {
            Payments p = null;
            Users user = Session["user"] as Users;

            if (amountWillPay > Convert.ToInt32(txtAmountPredefined.Value)
                    || txtAmountPaid.Value <= 0
                    || txtAmountPaid.Text == "")
            {
                lblErrorAmount.Visible = true;
                txtAmountPaid.Focus();
            }
            else
            {
                p = new Payments();
                p.staff_code = lblContentCode.Text;
                p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                p.description = description;
                p.amount_paid = txtAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtAmountPaid.Text.Trim());
                p.login_user = user.username.ToUpper();
                lblErrorAmount.Visible = false;
            }

            return p;
        }

        private void save_payments()
        {
            try
            {
                // inscription
                Users user = Session["user"] as Users;
                Payments p = null;
                List<Payments> listPayments = new List<Payments>();

                // inscription
                if (txtInscriptionAmountPaid.Enabled)
                {
                    int amountInscriptionalready_paid = lblInscriptionamountalreadyPaid.Text.Trim().Length <= 0 ? 0 : int.Parse(lblInscriptionamountalreadyPaid.Text);
                    int inscription_amount_willPay = (Convert.ToInt32(txtInscriptionAmountPaid.Value) + amountInscriptionalready_paid);
                    string description = "INSCRIPTION";

                    p = validatePayments(inscription_amount_willPay, description, txtInscriptionAmountPredefined, txtInscriptionAmountPaid);

                    if (p != null)
                    {
                        listPayments.Add(p);
                    }

                    //if (inscription_amount_willPay > Convert.ToInt32(txtInscriptionAmountPredefined.Value) 
                    //    || txtInscriptionAmountPaid.Value <= 0 
                    //    || txtInscriptionAmountPaid.Text == "")
                    //{
                    //    lblErrorAmount.Visible = true;
                    //    txtInscriptionAmountPaid.Focus();
                    //}
                    //else
                    //{
                    //    p = new Payments();
                    //    p.staff_code = lblContentCode.Text;
                    //    p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                    //    p.description = "INSCRIPTION";
                    //    p.amount_paid = txtInscriptionAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtInscriptionAmountPaid.Text.Trim());
                    //    p.login_user = user.username.ToUpper();
                    //    listPayments.Add(p);
                    //    lblErrorAmount.Visible = false;
                    //}
                }

                // frais d'entree
                if (txtFraisEntreeAmountPaid.Enabled)
                {
                    int amountEntrealready_paid = lblEntreeFeeamountalreadyPaid.Text.Trim().Length <= 0 ? 0 : int.Parse(lblEntreeFeeamountalreadyPaid.Text);
                    int Entree_Accept_amount_willPay = (Convert.ToInt32(txtFraisEntreeAmountPaid.Value) + amountEntrealready_paid);
                    string description = "FRAIS ENTRE";

                    p = validatePayments(Entree_Accept_amount_willPay, description, txtFraisEntreeAmountPredefined, txtFraisEntreeAmountPaid);

                    if (p != null)
                    {
                        listPayments.Add(p);
                    }


                    ////
                    //if (Entree_Accept_amount_willPay > Convert.ToInt32(txtFraisEntreeAmountPredefined.Value)
                    //    || txtFraisEntreeAmountPaid.Value <= 0
                    //    || txtFraisEntreeAmountPaid.Text == "")
                    //{
                    //    lblErrorAmount.Visible = true;
                    //    txtFraisEntreeAmountPaid.Focus();
                    //}
                    //else
                    //{
                    //    p = new Payments();
                    //    p.staff_code = lblContentCode.Text;
                    //    p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                    //    p.description = "FRAIS ENTRE";
                    //    p.amount_paid = txtFraisEntreeAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtFraisEntreeAmountPaid.Text.Trim());
                    //    p.login_user = user.username.ToUpper();
                    //    listPayments.Add(p);
                    //    lblErrorAmount.Visible = false;
                    //}
                }

                if (pnl_mensuel.Visible)
                {
                    #region monthly payment

                    // septembre
                    if (txtSeptemberAmountPaid.Enabled)
                    {


                        int amountSeptemberalready_paid = lblEntreeSeptemberalreadyPaid.Text.Trim().Length <= 0 ? 0 : int.Parse(lblEntreeSeptemberalreadyPaid.Text);
                        int Entree_Accept_amount_willPay = (Convert.ToInt32(txtSeptemberAmountPaid.Value) + amountSeptemberalready_paid);
                        string description = "septembre";

                        p = validatePayments(Entree_Accept_amount_willPay, description, txtSeptemberAmountPredefined, txtSeptemberAmountPaid);

                        if (p != null)
                        {
                            listPayments.Add(p);
                        }




                        //int september_Textbox_value = (Convert.ToInt32(txtSeptemberAmountPaid.Text) + amountSeptemberalready_paid);
                        //if (september_Textbox_value > Convert.ToInt32(txtSeptemberAmountPredefined.Value) || txtSeptemberAmountPaid.Value <= 0 || txtSeptemberAmountPaid.Text == "")
                        //{
                        //    lblErrorAmount.Visible = true;
                        //    txtSeptemberAmountPaid.Focus();
                        //}
                        //else
                        //{
                        //    p = new Payments();
                        //    p.staff_code = lblContentCode.Text;
                        //    p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                        //    p.description = "septembre";
                        //    p.amount_paid = txtSeptemberAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtSeptemberAmountPaid.Text.Trim());
                        //    p.login_user = user.username.ToUpper();
                        //    listPayments.Add(p);
                        //    lblErrorAmount.Visible = false;
                        //}
                    }
                    /*
                                       //
                                       p = new Payments();
                                       // octobre
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "octobre";
                                       p.amount_paid = txtOctoberAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtOctoberAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtOctoberAmountPaid.Value > 0 && txtOctoberBalance.Text == "" || txtOctoberBalance.Value > 0 && txtOctoberAmountPaid.Enabled == false)
                                       {
                                           listPayments.Add(p);
                                       }
                                       p = new Payments();
                                       // novembre
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "novembre";
                                       p.amount_paid = txtNovemberAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtNovemberAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtNovemberAmountPaid.Value > 0 && txtNovemberBalance.Text == "" || txtNovemberBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }

                                       p = new Payments();
                                       // decembre
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "decembre";
                                       p.amount_paid = txtDecemberAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtDecemberAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtDecemberAmountPaid.Value > 0 && txtDecemberBalance.Text == "" || txtDecemberBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }

                                       p = new Payments();
                                       // janvier
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "janvier";
                                       p.amount_paid = txtJanuaryAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtJanuaryAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtJanuaryAmountPaid.Value > 0 && txtJanuaryBalance.Text == "" || txtJanuaryBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }


                                       p = new Payments();
                                       // fevrier
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "fevrier";
                                       p.amount_paid = txtFebuaryAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtFebuaryAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtFebuaryAmountPaid.Value > 0 && txtFebuaryBalance.Text == "" || txtFebuaryBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }

                                       p = new Payments();
                                       // mars
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "mars";
                                       p.amount_paid = txtMarchAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtMarchAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtMarchAmountPaid.Value > 0 && txtMarchBalance.Text == "" || txtMarchBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }
                                       p = new Payments();
                                       // avril
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "avril";
                                       p.amount_paid = txtAprilAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtAprilAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtAprilAmountPaid.Value > 0 && txtAprilBalance.Text == "" || txtAprilBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }

                                       p = new Payments();
                                       // mai
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "mai";
                                       p.amount_paid = txtMayAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtMayAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtMayAmountPaid.Value > 0 && txtMayBalance.Text == "" || txtMayBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }
                                       p = new Payments();
                                       // juin
                                       p.staff_code = lblContentCode.Text;
                                       p.payment_configuration_id = int.Parse(Session["payment_conf_id"].ToString());
                                       p.description = "juin";
                                       p.amount_paid = txtJuneAmountPaid.Text.Trim().Length <= 0 ? 0 : Double.Parse(txtJuneAmountPaid.Text.Trim());
                                       p.login_user = user.username.ToUpper();
                                       if (txtJuneAmountPaid.Value > 0 && txtJuneBalance.Text == "" || txtJuneBalance.Value > 0)
                                       {
                                           listPayments.Add(p);
                                       }*/

                    #endregion
                }

                if (pnl_versement.Visible)
                {
                    #region verse payment

                    #endregion
                }

                // add to db
                Payments.addPaymentMontlyVersement(listPayments);
                emptyFields();
                Check_balance_payments();
                Validate_And_Enable_filelds();

            }

            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }

        private void Check_balance_payments()
        {
            try
            {
                int id_class = int.Parse(Session["id_classe"].ToString());
                string student_Code = Session["student_Code"].ToString();
                int academic_year = int.Parse(Session["academic_year_id"].ToString());
                List<Payments> list_balance_payment = Payments.getList_balance_payment(id_class, student_Code, academic_year);

                // int a = 0;
                // AmountInscriptionPaid = Convert.ToInt32(list_balance_payment[a].inscriptionFee.ToString());

                if (list_balance_payment != null & list_balance_payment.Count > 0)
                {
                    for (int i = 0; i < list_balance_payment.Count; i++)
                    {
                        switch (list_balance_payment[i].payment_type)
                        {
                            case "INSCRIPTION":

                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtInscriptionBalance.Text = Convert.ToString(Convert.ToInt32(txtInscriptionAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    lblInscriptionamountalreadyPaid.Text = Convert.ToString(list_balance_payment[i].amount_paid);
                                    Validate_And_Enable_filelds();

                                }
                                break;

                            case "FRAIS ENTRE":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtFraisEntreeBalance.Text = Convert.ToString(Convert.ToInt32(txtFraisEntreeAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    lblEntreeFeeamountalreadyPaid.Text = Convert.ToString(list_balance_payment[i].amount_paid);
                                    Validate_And_Enable_filelds();
                                }
                                break;

                            case "septembre":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0 && lblContenPrivilege.Text == "AUCUN")
                                {
                                    txtSeptemberBalance.Text = Convert.ToString(Convert.ToInt32(txtSeptemberAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    lblEntreeSeptemberalreadyPaid.Text = Convert.ToString(list_balance_payment[i].amount_paid);
                                    Validate_And_Enable_filelds();
                                }
                                else if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0 && lblContenPrivilege.Text == "DEMI-BOURSE")
                                {
                                    txtSeptemberBalance.Text = Convert.ToString((Convert.ToInt32(txtSeptemberAmountPredefined.Text) / 2) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    lblEntreeSeptemberalreadyPaid.Text = Convert.ToString(list_balance_payment[i].amount_paid);
                                    Validate_And_Enable_filelds();
                                }
                                else if (lblContenPrivilege.Text == "BOURSE")
                                {
                                    txtSeptemberBalance.Text = Convert.ToString(Convert.ToInt32(txtSeptemberAmountPredefined.Text) - (Convert.ToInt32(txtSeptemberAmountPredefined.Text)));
                                    lblEntreeSeptemberalreadyPaid.Text = Convert.ToString(Convert.ToInt32(txtSeptemberAmountPredefined.Text));
                                    Validate_And_Enable_filelds();
                                }
                                break;

                            case "octobre":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtOctoberBalance.Text = Convert.ToString(Convert.ToInt32(txtOctoberAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "novembre":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtNovemberBalance.Text = Convert.ToString(Convert.ToInt32(txtNovemberAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "decembre":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtDecemberBalance.Text = Convert.ToString(Convert.ToInt32(txtDecemberAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "janvier":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtJanuaryBalance.Text = Convert.ToString(Convert.ToInt32(txtJanuaryAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "fevrier":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtFebuaryBalance.Text = Convert.ToString(Convert.ToInt32(txtFebuaryAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "mars":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtMarchBalance.Text = Convert.ToString(Convert.ToInt32(txtMarchAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "avril":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtAprilBalance.Text = Convert.ToString(Convert.ToInt32(txtAprilAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "mai":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtMayBalance.Text = Convert.ToString(Convert.ToInt32(txtMayAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                            case "juin":
                                if (Convert.ToInt32(list_balance_payment[i].amount_paid) > 0)
                                {
                                    txtJuneBalance.Text = Convert.ToString(Convert.ToInt32(txtJuneAmountPredefined.Text) - Convert.ToInt32(list_balance_payment[i].amount_paid));
                                    Validate_And_Enable_filelds();
                                }
                                break;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void btSavePayment_Click(object sender, EventArgs e)
        {
            save_payments();
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            loadPaiementInformation();
            Validate_And_Enable_filelds();
            Check_balance_payments();
            Validate_And_Enable_filelds();
            //
            lblFound.Visible = false;
            pnleffectuer_payment.Visible = true;
            pnlResult.Visible = false;
            lblCounter.Visible = false;
            lblTotalAmount.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = true;
            btnModify.Visible = true;
            btSavePayment.Visible = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnModify_Click(object sender, EventArgs e)
        {

            BindDataGriPaymentScolarite();
        }

        protected void gridListPaymentScolarite_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridListPaymentType.Rows[index];
            //string code = row.Cells[0].Text;

        }

        protected void gridListPaymentScolarite_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    // e.Row.Style.Add("height", "50px");
                    e.Row.Style.Add("vertical-align", "bottom");
            }
        }

        protected void gridListPaymentScolarite_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindDataGriPaymentScolarite();
        }

        private void BindDataGriPaymentScolarite()
        {
            Payments p = new Payments();
            p.id_classroom = int.Parse(Session["id_classe"].ToString());
            p.academic_year = int.Parse(Session["academic_year_id"].ToString());
            p.staff_code = Session["student_Code"].ToString();
            List<Payments> listResult = Payments.getAllPaymentScolarite(p);


            if (listResult != null && listResult.Count > 0)
            {
                btnBack.Visible = true;
                btnCancel.Visible = false;
                btnModify.Visible = false;
                btSavePayment.Visible = false;

                lblFound.Visible = false;
                pnleffectuer_payment.Visible = false;
                pnlResult.Visible = true;
                lblCounter.Visible = true;
                lblCounter.Text = listResult.Count.ToString() + " Ligne(s)";
                lblTotalAmount.Visible = true;
                double _totalAmount = 0;
                foreach (Payments payment in listResult)
                {
                    _totalAmount += payment.amount_paid;
                }
                lblTotalAmount.Text = "Montant Total : " + _totalAmount + " Gdes";
            }
            else
            {
                pnlResult.Visible = true;
                lblCounter.Visible = false;
                lblTotalAmount.Visible = false;
                lblErrorAmount.Text = "Desoler ! transacion(s) de paiement introuvable.";
                lblErrorAmount.Visible = true;

            }
            gridListPaymentType.DataSource = listResult;
            gridListPaymentType.DataBind();
        }

        public void removepaymentScolarite(object sender, EventArgs e)

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
                    Label labelPaymentId = (row.Cells[5].FindControl("lblPaymentTypeId") as Label);
                    int payment_type_id = int.Parse(labelPaymentId.Text);
                    // first check if this payment is already assigned                    
                    Payments.deletePaymentTypeScolarite(payment_type_id);
                    BindDataGriPaymentScolarite();
                }


            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }

        //Events on textbox paid_Amount

        protected void txtInscriptionAmountPaid_TextChanged(object sender, EventArgs e)
        {
            save_payments();
        }
        protected void txtEntreeAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtSeptemberAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtOctoberAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtNovemberAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtDecemberAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtJanuaryAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtFebuaryAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtMarchAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtAprilAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtMayAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
        protected void txtJuneAmountPaid_TextChanged(object sender, EventArgs e)
        {

            save_payments();
        }
    }