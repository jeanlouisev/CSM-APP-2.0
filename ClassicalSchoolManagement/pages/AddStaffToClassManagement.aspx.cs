using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;




public partial class AddStaffToClassManagement : System.Web.UI.Page
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
            //if (DefaultMenuContent.HasControls())
            //{
            //    foreach (Control c in DefaultMenuContent.Controls)
            //    {
            //        if (c.ID == "menu")
            //        {
            //            RadMenu menu = c as RadMenu;
            //            menu.FindItemByText("Classe").HighlightPath();
            //        }
            //    }
            //}

            //if (ClassroomMenuContent.HasControls())
            //{
            //    foreach (Control c in ClassroomMenuContent.Controls)
            //    {
            //        if (c.ID == "lknStaffToClassroomManagement")
            //        {
            //            LinkButton linkBtn = c as LinkButton;
            //            linkBtn.ForeColor = System.Drawing.Color.Gold;
            //            linkBtn.BackColor = System.Drawing.Color.Black;
            //        }
            //    }
            //}
        }
    }
}