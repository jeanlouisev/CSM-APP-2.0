using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class CoursMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknAddCours_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCours.aspx?menu=Cours");
        }

        protected void lknFixPrice_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModifyCours.aspx?menu=Cours");
        }
    }
}