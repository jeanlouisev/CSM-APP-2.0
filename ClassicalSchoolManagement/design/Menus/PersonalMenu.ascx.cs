using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class PersonalMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknAddPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPersonal.aspx?menu=Personnel");
        }

        protected void lknSearchPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPersonal.aspx?menu=Personnel");
        }

        protected void lknModifyPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModifyPersonal.aspx?menu=Personnel");
        }

        protected void lknTransferPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferPersonal.aspx?menu=Personnel");
        }

        protected void lknHistoryPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoryPersonal.aspx?menu=Personnel");
        }

        protected void lknPersonalDocumentManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("PersonalDocumentManagement.aspx?menu=Personnel");
        }
    }
}