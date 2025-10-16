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


public partial class Profile : System.Web.UI.Page
{
    int menu_code = 11; // parameter menu code, for more information see "menu" table.

    protected void Page_Load(object sender, EventArgs e)
    {

        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        try
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Error.aspx");
                }
                else
                {
                    Users user = Session["user"] as Users;
                    loadUserInfo(user);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void loadUserInfo(Users user)
    {
        try
        {
            List<Universal> listLoginUserInfo = Universal.getUserProfile(user.staff_code);
            if (listLoginUserInfo != null && listLoginUserInfo.Count > 0)
            {
                imgUser.ImageUrl = listLoginUserInfo[0].image_path == null ? "~/images/image_data/Default.png" : "~/images/image_data/" + listLoginUserInfo[0].image_path;
                lblFullname.Text = listLoginUserInfo[0].fullName == null || listLoginUserInfo[0].fullName == "" ? "N/A" : listLoginUserInfo[0].fullName;
                lblSex.Text = listLoginUserInfo[0].sex.Length <= 0 ? "N/A" : listLoginUserInfo[0].sex == "M" ? "Masculin" : "Feminin";
                lblBirthDate.Text = listLoginUserInfo[0].birth_date == null || listLoginUserInfo[0].birth_date == DateTime.MinValue ? "N/A"
                                        : listLoginUserInfo[0].birth_date.ToString("dd/MM/yyyy");
                //lblAge.Text = listLoginUserInfo[0].birth_date == null || listLoginUserInfo[0].birth_date == DateTime.MinValue ? "N/A"
                //                       : (DateTime.Now.Year - listLoginUserInfo[0].birth_date.Year).ToString() + " AN(S)";
                lblBirthPlace.Text = listLoginUserInfo[0].birth_place == null || listLoginUserInfo[0].birth_place == "" ? "N/A" : listLoginUserInfo[0].birth_place;
                lblPHone.Text = listLoginUserInfo[0].phone.Length <= 0 ? "N/A" : listLoginUserInfo[0].phone;
                lblPosition.Text = listLoginUserInfo[0].position_description == null || listLoginUserInfo[0].position_description == "" ? "N/A"
                                      : listLoginUserInfo[0].position_description;
                lblEmail.Text = listLoginUserInfo[0].email == null || listLoginUserInfo[0].email == "" ? "N/A" : listLoginUserInfo[0].email;
                lblAddress.Text = listLoginUserInfo[0].address == null || listLoginUserInfo[0].address == "" ? "N/A" : listLoginUserInfo[0].address;
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

}