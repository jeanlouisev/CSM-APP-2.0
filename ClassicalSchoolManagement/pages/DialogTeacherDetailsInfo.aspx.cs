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
using Utilities;
using Telerik.Web;
using Telerik.Web.UI;



public partial class DialogTeacherDetailsInfo : System.Web.UI.Page
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
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }

            if (Session["teacher_id"] != null)
            {
                loadTeacherInformation();
            }
        }
    }

    private void loadTeacherInformation()
    {
        try
        {

            string Code = Session["teacher_id"].ToString();

            Teacher t = Teacher.getTeacherInfoById(Code);
            //
            lblFirstName.Text = t.first_name.ToUpper() == null || t.id.Length <= 0 ? "N/A" : t.first_name.ToUpper();
            lblLastName.Text = t.last_name.ToUpper() == null || t.id.Length <= 0 ? "N/A" : t.last_name.ToUpper();
            lblCode.Text = t.id == null || t.id.Length <= 0 ? "N/A" : t.id;
            lblSex.Text = t.sex_definition == null || t.sex_definition.Length <= 0 ? "N/A" : t.sex_definition;
            lblEmail.Text = t.email == null || t.email.Length <= 0 ? "N/A" : t.email.ToLower();
            lblAdresse.Text = t.address == null || t.address.Length <= 0 ? "N/A" : t.address;
            lblIdCard.Text = t.id_card == null || t.id_card.Length <= 0 ? "N/A" : t.id_card;
            lblPhone.Text = t.phone1 == null || t.phone1.Length <= 0 ? "N/A" : t.phone1;
            lblLevel.Text = t.study_level == null || t.study_level.Length <= 0 ? "N/A" : t.study_level;

            lblBirthDate.Text = t.birth_date == null || t.birth_date.ToString().Length <= 0 ? "N/A" :
                t.birth_date.ToString("dd/MM/yyyy");

            lblBirthPlace.Text = t.birth_place == null || t.birth_place.Length <= 0 ? "N/A" :
                t.birth_place;

            lblMaritalStatus.Text = t.marital_status_definition == null || t.marital_status_definition.Length <= 0 ? "N/A" :
                t.marital_status_definition;

            imageKeeper.ImageUrl = "~/images/image_data/" + t.imagePath;
            //
            if (t.status == 1)
            {
                lblStatus.Text = "Activé";
            }
            if (t.status == 0)
            {
                lblStatus.Text = "Desactivé";
            }


            // information parents

            lblFirstNameCont.Text = t.ref_first_name;
            lblLastNameCont.Text = t.ref_last_name;
            lblSexCont.Text = t.ref_sex == "M" ? "Masculin" : "Feminin";
            lblPhoneCont.Text = t.ref_phone;
            lblAdressCont.Text = t.ref_adress;

            switch (t.ref_relationship)
            {
                case "FATHER": lblRelationshipCont.Text = "Père"; break;
                case "MOTHER": lblRelationshipCont.Text = "Mère"; break;
                case "UNCLE": lblRelationshipCont.Text = "Oncle"; break;
                case "AUNTIE": lblRelationshipCont.Text = "Tante"; break;
                case "BROTHER": lblRelationshipCont.Text = "Frere"; break;
                case "SISTER": lblRelationshipCont.Text = "Soeur"; break;
                case "COUSIN": lblRelationshipCont.Text = "Cousin (e)"; break;
                case "GOD_FATHER": lblRelationshipCont.Text = "Parrain"; break;
                case "GOD_MOTHER": lblRelationshipCont.Text = "Marraine"; break;
                case "HUSBAND": lblRelationshipCont.Text = "Epoux"; break;
                case "WIFE": lblRelationshipCont.Text = "Epouse"; break;
                case "BOYFRIEND": lblRelationshipCont.Text = "Copain"; break;
                case "GIRLFRIEND": lblRelationshipCont.Text = "Copine"; break;
                case "NEIGHBOR": lblRelationshipCont.Text = "Voisin(e)"; break;
                case "OTHER": lblRelationshipCont.Text = "Autre"; break;
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

}