using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Default : System.Web.UI.Page
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

        //
        //if (Session["menu"] != null)
        //{
        //    lblAnswer.Text = "Désolé, Vous n'avez pas droit au menu :" + Session["menu"].ToString() + " !";
        //}
    }

    protected void btnBackToLogin_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Response.Redirect("pages/login.aspx");
    }
}