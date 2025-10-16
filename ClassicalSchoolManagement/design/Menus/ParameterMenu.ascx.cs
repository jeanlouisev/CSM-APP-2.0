using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class ParameterMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx?menu=Parametres");
        }

        protected void lknManageGroupe_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageGroupe.aspx?menu=Parametres");
        }

        protected void lknAccademicYear_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccademicYear.aspx?menu=Parametres");
        }
    }
}