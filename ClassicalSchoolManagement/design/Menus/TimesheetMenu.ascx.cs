using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class TimesheetMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknPresenceSheet_Click(object sender, EventArgs e)
        {
            Response.Redirect("PresenceSheets.aspx?menu=Timesheet");
        }

        protected void lknSearchSheets_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchSheets.aspx?menu=Timesheet");
        }

        protected void lknReasonSheets_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReasonSheets.aspx?menu=Timesheet");
        }
    }
}