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



public partial class sendBirthdaySMS : System.Web.UI.Page
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

        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        if (!IsPostBack)
        {
            loadBasicInfo();
        }
    }

    private void loadBasicInfo()
    {
        if (Session["all_staffs_info"] != null)
        {
            tbl_multiple_sms.Visible = true;
            tbl_single_sms.Visible = false;
            List<string> listFullname = new List<string>();
            List<Universal> listStaff = Session["all_staffs_info"] as List<Universal>;

            int k = 1;
            foreach (Universal uni in listStaff)
            {
                listFullname.Add(k + ") " + uni.staff_code + " | " + uni.fullName + " | " + uni.phone + " | " + uni.position);
                k++;
            }
            txtAllStaff.InnerText = string.Join("\n", listFullname);
        }
        else
        {
            tbl_multiple_sms.Visible = false;
            tbl_single_sms.Visible = true;
            lblError.Visible = false;
            txtMessageContent.Disabled = false;
            //
            Users user = Session["user"] as Users;
            lblPhone.Text = Session["phone"].ToString();
            lblCode.Text = Session["staff_code"].ToString();
            lblFullname.Text = Session["fullname"].ToString();
            lblPosition.Text = Session["position"].ToString();
            //txtMessageContent.Value = "Joyeux aniversaire " + lblFullname.Text.Trim() + "\nDu : College Saphir Giant Technologie";
            //txtMessageContent.Focus();

            //check if this user already sent birthday sms to staff
            //List<Universal> listUni = Universal.getListBirthdaySmsAlreadySent(lblCode.Text.Trim(), user.username.ToUpper());
            //if (listUni != null && listUni.Count > 0)
            //{
            //    txtMessageContent.InnerText = listUni[0].message_content;
            //    txtMessageContent.Disabled = true;

            //    btnSend.Enabled = false;
            //    lblError.Text = "Désolé, vous avez déjà envoyé un sms vers " + lblFullname.Text.Trim();
            //    lblError.Visible = true;
            //}
        }

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        Users user = Session["user"] as Users;
        btnSend.Enabled = true;
        lblError.Text = string.Empty;
        lblError.Visible = false;
        try
        {
            if (txtMessageContent.Value == null
                  || txtMessageContent.Value.Length <= 0)
            {
                lblError.Text = "Erreur : Veuillez taper le texte";
                lblError.Visible = true;
            }
            else
            {
                if (Session["all_staffs_info"] != null)
                {
                    List<Universal> listStaff = Session["all_staffs_info"] as List<Universal>;

                    foreach (Universal uni in listStaff)
                    {
                        //string phone = Universal.GetPhoneNumberByStaffCode(uni.staff_code);
                        if (uni.phone.Length > 0)
                        {
                            Universal universal = new Universal();
                            universal.staff_code = uni.staff_code;
                            universal.phone = "+509" + uni.phone;
                            universal.message_content = "Joyeux Anniversaire \n" + txtMessageContent.Value.Trim() + "\nSAPHIR SCHOOL";
                            universal.login_user = user.username;

                            Universal.sendBirthdaySMS(universal);

                            // close the window & update the birthday gridview
                            lblError.Text = "Success !";
                            lblError.ForeColor = Color.DarkGreen;
                            lblError.Visible = true;
                            //}
                            //else
                            //{
                            //    lblError.Text = "Fail to send SMS !";
                            //    lblError.ForeColor = Color.Red;
                            //    lblError.Visible = true;
                            //}
                        }
                    }
                }
                else
                {
                    // send sms 
                    Universal uni = new Universal();
                    uni.staff_code = lblCode.Text.Trim();
                    uni.phone = "+509" + lblPhone.Text.Trim();
                    uni.message_content = "Joyeux Anniversaire \n" + txtMessageContent.Value.Trim() + "\nSAPHIR SCHOOL";
                    uni.login_user = user.username;

                    Universal.sendBirthdaySMS(uni);

                    // close the window & update the birthday gridview
                    lblError.Text = "Success !";
                    lblError.ForeColor = Color.DarkGreen;
                    lblError.Visible = true;
                    //}
                    //else
                    //{
                    //    lblError.Text = "Fail to send SMS !";
                    //    lblError.ForeColor = Color.Red;
                    //    lblError.Visible = true;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void txtMessage_TextChanged(object sender, EventArgs e)
    {
        //  lblMessageTextCounter.Text = txtMessage.MaxLength - txtMessage.Text.Length + " Charactere(s)";
        lblError.Text = string.Empty;
        lblError.Visible = false;
    }
}