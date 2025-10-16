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



public partial class DialogStudentDetailsInfo : System.Web.UI.Page
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

            loadStudentInformation();
        }
    }

    private void loadStudentInformation()
    {
        try
        {
            string studentCode = Session["student_code"].ToString();
            //int classId = int.Parse(Session["classroom_id"].ToString());

            Student st = Student.getStudentFullDetailsByCode(studentCode);
            //
            if (st != null)
            {
                // information personnelles
                lblFirstName.Text = st.first_name.ToUpper() == null || st.id.Length <= 0 ? "N/A" : st.first_name.ToUpper();
                lblLastName.Text = st.last_name.ToUpper() == null || st.id.Length <= 0 ? "N/A" : st.last_name.ToUpper();
                lblCode.Text = st.id == null || st.id.Length <= 0 ? "N/A" : st.id;
                lblSex.Text = st.sex_definition == null || st.sex_definition.Length <= 0 ? "N/A" : st.sex_definition;
                lblEmail.Text = st.email == null || st.email.Length <= 0 ? "N/A" : st.email.ToLower();
                lblAdresse.Text = st.address == null || st.address.Length <= 0 ? "N/A" : st.address;
                lblVacation.Text = st.vacation == null || st.vacation.Length <= 0 ? "N/A" : st.vacation;
                lblIdCard.Text = st.id_card == null || st.id_card.Length <= 0 ? "N/A" : st.id_card;
                lblPhone.Text = st.phone == null || st.phone.Length <= 0 ? "N/A" : st.phone;

                lblBirthDate.Text = st.birth_date == null || st.birth_date.ToString().Length <= 0 ? "N/A" :
                    st.birth_date.ToString("dd/MM/yyyy");

                lblBirthPlace.Text = st.birth_place == null || st.birth_place.Length <= 0 ? "N/A" :
                    st.birth_place;

                lblMaritalStatus.Text = st.marital_status == null || st.marital_status.Length <= 0 ? "N/A" :
                    st.marital_status;

                lblClassroom.Text = st.class_name;
                //ClassRoom.getClassroomNameById(int.Parse(st.classroom)).Length <= 0 ? "N/A" :
                //                    ClassRoom.getClassroomNameById(int.Parse(st.classroom));

                imageKeeper.ImageUrl = "~/images/image_data/" + st.image_path;
                //
                if (st.status == 1)
                {
                    lblStatus.Text = "Activé";
                }
                if (st.status == 0)
                {
                    lblStatus.Text = "Desactivé";
                }

                // information parents

                //lblFirstNameCont.Text = st.ref_first_name;
                //lblLastNameCont.Text = st.ref_last_name;
                //lblSexCont.Text = st.ref_sex == "M" ? "Masculin" : "Feminin";
                //lblPhoneCont.Text = st.ref_phone;
                //lblAdressCont.Text = st.ref_adress;

                //switch (st.ref_relationship)
                //{
                //    case "FATHER": lblRelationshipCont.Text = "Père"; break;
                //    case "MOTHER": lblRelationshipCont.Text = "Mère"; break;
                //    case "UNCLE": lblRelationshipCont.Text = "Oncle"; break;
                //    case "AUNTIE": lblRelationshipCont.Text = "Tante"; break;
                //    case "BROTHER": lblRelationshipCont.Text = "Frere"; break;
                //    case "SISTER": lblRelationshipCont.Text = "Soeur"; break;
                //    case "COUSIN": lblRelationshipCont.Text = "Cousin (e)"; break;
                //    case "GOD_FATHER": lblRelationshipCont.Text = "Parrain"; break;
                //    case "GOD_MOTHER": lblRelationshipCont.Text = "Marraine"; break;
                //    case "HUSBAND": lblRelationshipCont.Text = "Epoux"; break;
                //    case "WIFE": lblRelationshipCont.Text = "Epouse"; break;
                //    case "BOYFRIEND": lblRelationshipCont.Text = "Copain"; break;
                //    case "GIRLFRIEND": lblRelationshipCont.Text = "Copine"; break;
                //    case "NEIGHBOR": lblRelationshipCont.Text = "Voisin(e)"; break;
                //    case "OTHER": lblRelationshipCont.Text = "Autre"; break;
                //}
                
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

}