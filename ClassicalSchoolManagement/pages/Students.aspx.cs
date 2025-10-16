using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




    public partial class Students : System.Web.UI.Page
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


        }
    }