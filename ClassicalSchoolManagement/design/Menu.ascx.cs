using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Utilities;


namespace ClassicalSchoolManagement.design
{
    public partial class Menu1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Users user = Session["user"] as Users;

                if (user != null)
                {
                    txtUserLoginFullName.Text = user.username;
                }
                


                //menu.FindItemByText("Acceuil").HighlightPath();
            }
        }
        /**
        protected void RadMenu1_ItemDataBound(object sender, RadMenuEventArgs e)
        {
            e.Item.Attributes["NavigateUrl"] = e.Item.NavigateUrl;
            e.Item.NavigateUrl = "";
        }

        protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
        {
            string _content = e.Item.Text;
            //Custom code here    
            menu.FindItemByText(_content).HighlightPath();
            //Navigate          
            Response.Redirect("~/Home.aspx");
        }**/
    }
}