using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class ScheduleMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknDefineShedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("DefineShedule.aspx?menu=Horaire");
        }

        protected void lknModifySchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModifySchedule.aspx?menu=Horaire");
        }

        protected void lknListSchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListSchedule.aspx?menu=Horaire");
        }
    }
}