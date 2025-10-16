using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class TeacherMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknRegisterTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterTeachers.aspx?menu=Professeur");
        }

        protected void lknSearchTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchTeachers.aspx?menu=Professeur");
        }

        protected void lknAffectCousrsToTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("AffectCoursToTeacher.aspx?menu=Professeur");
        }

        protected void lknTeacherDocumentManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherDocumentManagement.aspx?menu=Professeur");
        }

        protected void lknTransferTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferTeachers?menu=Professeur");
        }
    }
}