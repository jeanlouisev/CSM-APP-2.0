using System;
using System.Collections.Generic;
using System.Linq;
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
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using Utilities;



public partial class Home : System.Web.UI.Page
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
            //loadDashboardInfo();
        }
    }

    private void loadDashboardInfo()
    {
        //// STUDENT COUNTER
        //List<Student> studentList = Student.getListAllActiveStudent();
        //if (studentList != null && studentList.Count > 0)
        //{
        //    lblCountStudent.Text = studentList.Count.ToString();
        //}

        //// TEACHER COUNTER
        //List<Teacher> teacherList = Teacher.getListAllTeacher();
        //if (teacherList != null && teacherList.Count > 0)
        //{
        //    lblCountTeacher.Text = teacherList.Count.ToString();
        //}

        //// STAFF COUNTER
        //List<Staff> staffList = Staff.getListAllStaff();
        //if (staffList != null && staffList.Count > 0)
        //{
        //    lblCountStaff.Text = staffList.Count.ToString();
        //}
    }
    
}
