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


public partial class SideMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadUserInfo();
    }

    private void loadUserInfo()
    {
        try
        {
            Users user = Session["user"] as Users;
            //
            if (user.username.StartsWith("EL"))
            {
                Student studentInfo = Student.getListStudentByCode(user.username)[0];

                // GENERAL MENU
                lblFullname.Text = studentInfo.first_name.ToUpper() + " <br/> " + studentInfo.last_name;
                String imgPath = studentInfo.image_path == null ? "../images/image_data/Default.png" : "../images/image_data/" + studentInfo.image_path;
                imgLoginUser.Attributes.Add("src", imgPath);
            }
            else if (user.username.StartsWith("PS"))
            {
                Staff staffInfo = Staff.getListStaffById(user.username)[0];
                // GENERAL MENU
                lblFullname.Text = staffInfo.first_name.ToUpper() + " <br/> " + staffInfo.last_name;
                String imgPath = staffInfo.image_path == null ? "../images/image_data/Default.png" : "../images/image_data/" + staffInfo.image_path;
                imgLoginUser.Attributes.Add("src", imgPath);
            }
            else if (user.username.StartsWith("PRO"))
            {
                Teacher teacherInfo = Teacher.getTeacherInfoById(user.username);
                // GENERAL MENU
                lblFullname.Text = teacherInfo.first_name.ToUpper() + " <br/> " + teacherInfo.last_name;
                String imgPath = teacherInfo.imagePath == null ? "../images/image_data/Default.png" : "../images/image_data/" + teacherInfo.imagePath;
                imgLoginUser.Attributes.Add("src", imgPath);
            }
            else
            {
                lblFullname.Text = user.username;
                String imgPath = "../images/image_data/Default.png";
                imgLoginUser.Attributes.Add("src", imgPath);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
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
}