using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.Menus
{
    public partial class NotesMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknDownloadNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("DownloadNotes.aspx?menu=Notes");
        }

        protected void lknUploaderNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("UploaderNotes.aspx?menu=Notes");
        }

        protected void lknInsertNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("InsertNotes.aspx?menu=Notes");
        }

        protected void lknBulletinManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("BulletinManagement.aspx?menu=Notes");
        }

        protected void lknChangingClassManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangingClassManagement.aspx?menu=Notes");
        }

        protected void lknSearchNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchNotes.aspx?menu=Notes");
        }

        protected void lknAverageManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AverageManagement.aspx?menu=Notes");
        }
    }
}