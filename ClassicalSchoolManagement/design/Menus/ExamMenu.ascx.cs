using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class ExamMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknAddExams_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddExams.aspx?menu=Examen");
        }

        protected void lknSearchExams_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchExams.aspx?menu=Examen");
        }
    }
}