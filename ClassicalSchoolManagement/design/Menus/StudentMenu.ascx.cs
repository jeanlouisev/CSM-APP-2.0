using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassicalSchoolManagement.design.menus
{
    public partial class StudentMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lknRegisterStudents_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterStudents.aspx");
        }

        protected void lknStudentDocumentManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDocumentManagement.aspx?menu=Eleve");
        }

        protected void lknSearchStudents_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchStudents.aspx?menu=Eleve");
        }

        protected void lknSearchReference_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SearchStudentReference.aspx?menu=Eleve");
        }

        protected void lknTransferStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferStudents.aspx?menu=Eleve");
        }

        protected void lknHistoryStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoryStudents.aspx?menu=Eleve");
        }

        protected void lknStudentReferenceManagement_Click(object sender, EventArgs e)
        {

        }

        protected void lnkParentManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParentManagement.aspx?menu=Eleve");
        }
    }
}