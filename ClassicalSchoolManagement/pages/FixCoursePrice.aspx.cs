using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using Utilities;
using Telerik.Web.UI;
using Telerik.Web;




public partial class FixCoursePrice : System.Web.UI.Page
{
    int menu_code = 4; // parameter menu code, for more information see "menu" table.
    string msgContent = null;

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
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                Users user = Session["user"] as Users;
                //reload the gridview
                //put the cursor on the price textbox
            }
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    txtCoursePrice.ReadOnly = true;
        //    btnAddPrice.Enabled = false;
        //    //
        //    // loop through the grid to disable edit option
        //    if (gridListCourse.Visible && gridListCourse.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListCourse.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListCourse.Rows[i].Cells[3].FindControl("btnDelete") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur :" + ex.Message);
        //}
    }

    // delete
    private void disableDeleteOption()
    {
        //try
        //{
        //    // loop through the grid to disable delete option
        //    if (gridListCourse.Visible && gridListCourse.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListCourse.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn = gridListCourse.Rows[i].Cells[4].FindControl("btnDelete") as ImageButton;
        //            imgBtn.Enabled = false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur : " + ex.Message);
        //}
    }
    /******************************************* END USER POLICY **************************/

}