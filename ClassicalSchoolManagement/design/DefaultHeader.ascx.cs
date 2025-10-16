using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;

using Utilities;


public partial class DefaultHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Users user = Session["user"] as Users;
            if (user != null)
            {
                loadUserInformation();
            }

            if (Session["language"] != null)
            {
                //btnLang.InnerHtml = Session["ddindex"] as String;
                //btnLang.Attributes["value"] = Session["language"] as String;
                //ddlanguage.SelectedIndex = Convert.ToInt32(Session["ddindex"].ToString());
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["CultureInfo"];
                if (cookie != null && cookie.Value != null)
                {
                    //btnLang.InnerHtml = "<span class='flag-icon flag-icon-gb'></span>";
                    // btnLang.Attributes["value"] = cookie.Value;
                    Session["language"] = cookie.Value;
                    if (cookie.Value == "en")
                    {
                        Session["ddindex"] = "<span class='flag-icon flag-icon-gb'></span>";
                    }
                    else if (cookie.Value == "fr")
                    {
                        Session["ddindex"] = "<span class='flag-icon flag-icon-fr'></span>";
                    }
                }
                else
                {
                    //btnLang.InnerHtml = "<span class='flag-icon flag-icon-gb'></span>";
                    //btnLang.Attributes["value"] = "en";
                }
            }
        }

    }

    private void loadUserInformation()
    {
        //try
        //{
        //    Users user = Session["user"] as Users;
        //    //
        //    if (user.username.StartsWith("EL"))
        //    {
        //        Student studentInfo = Student.getListStudentByCode(user.username)[0];
        //        lblWelcome.Text = "Bienvenue : " + studentInfo.first_name.ToUpper() + " " + studentInfo.last_name;
        //        lblFullName.Text = studentInfo.first_name.ToUpper() + " " + studentInfo.last_name;
        //        lblAddress.Text = studentInfo.address;
        //        String imgPath = studentInfo.image_path == null ? "../images/image_data/Default.png" : "../images/image_data/" + studentInfo.image_path;
        //        imgLoginUser.Attributes.Add("src", imgPath);
        //    }
        //    else if (user.username.StartsWith("PS"))
        //    {
        //        Staff staffInfo = Staff.getListStaffById(user.username)[0];
        //        lblWelcome.Text = "Bienvenue : " + staffInfo.first_name.ToUpper() + " " + staffInfo.last_name;
        //        lblFullName.Text = staffInfo.first_name.ToUpper() + " " + staffInfo.last_name;
        //        lblAddress.Text = staffInfo.adress;
        //        String imgPath = staffInfo.image_path == null ? "../images/image_data/Default.png" : "../images/image_data/" + staffInfo.image_path;
        //        imgLoginUser.Attributes.Add("src", imgPath);
        //    }
        //    else if (user.username.StartsWith("PRO"))
        //    {
        //        Teacher teacherInfo = Teacher.getTeacherInfoById(user.username);
        //        lblWelcome.Text = "Bienvenue : " + teacherInfo.first_name.ToUpper() + " " + teacherInfo.last_name;
        //        lblFullName.Text = teacherInfo.first_name.ToUpper() + " " + teacherInfo.last_name;
        //        lblAddress.Text = teacherInfo.address;
        //        String imgPath = teacherInfo.imagePath == null ? "../images/image_data/Default.png" : "../images/image_data/" + teacherInfo.imagePath;
        //        imgLoginUser.Attributes.Add("src", imgPath);
        //    }
        //    else
        //    {
        //        lblWelcome.Text = "Bienvenue : " + user.username;
        //        String imgPath = "../images/image_data/Default.png";
        //        imgLoginUser.Attributes.Add("src", imgPath);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur : " + ex.Message);
        //}
    }

    protected void lblLogout_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session != null)
            {
                Users user = Session["user"] as Users;
                // write login logs
                Users userLog = new Users();
                userLog.staff_code = user.username;
                userLog.url_path = HttpContext.Current.Request.Url.AbsolutePath;
                userLog.log_status = "logout";
                Users.writeUserLogs(userLog);
                //
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
        }
        catch { }
        Response.Redirect("../Pages/Login.aspx");
    }

    protected void ddlanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (btnLang.Attributes["value"] == "en")
        //{
        //    btnLang.Attributes["value"] = "fr";
        //}
        //else
        //{
        //    btnLang.Attributes["value"] = "en";
        //}
        //Session["language"] = btnLang.Attributes["value"];
        ////Sets the cookie that is to be used by Global.asax
        //HttpCookie cookie = new HttpCookie("CultureInfo");
        //cookie.Value = btnLang.Attributes["value"];
        //Response.Cookies.Add(cookie);
        ////Set the culture and reload for immediate effect.
        ////Future effects are handled by Global.asax
        //Thread.CurrentThread.CurrentCulture = new CultureInfo(btnLang.Attributes["value"]);
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo(btnLang.Attributes["value"]);
        //if (cookie.Value == "en")
        //{
        //    Session["ddindex"] = "<span class='flag-icon flag-icon-gb'></span>";
        //}
        //else if (cookie.Value == "fr")
        //{
        //    Session["ddindex"] = "<span class='flag-icon flag-icon-fr'></span>";
        //}
        //Server.Transfer(Request.Path);
    }
}
