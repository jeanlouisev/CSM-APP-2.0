using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Linq;

using Utilities;

namespace ClassicalSchoolManagement.master
{
    public partial class Master4 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Items["RadControlRandomNumber"] = 0;
            // load list staff having birthday today
            if (Session["user"] == null)
            {
                Response.Redirect("~/Error.aspx");
            }
          
            if (!IsPostBack)
            {
                loadUserBasicInfo();
                BindDataGridBirthday();
                //
                Users user = Session["user"] as Users;
                lblWelcomUser.Text = "Bienvenue : " + user.username;
            }
        }

        private void loadUserBasicInfo()
        {
            try
            {
                Users user = Session["user"] as Users;
                //
                List<Universal> listLoginUserInfo = Universal.getUserProfile(user.staff_code);
                if (listLoginUserInfo != null && listLoginUserInfo.Count > 0)
                {
                    //lblLoginUserFullname.Text = listLoginUserInfo[0].fullName == null || listLoginUserInfo[0].fullName == "" ? "N/A" : listLoginUserInfo[0].fullName;
                    //lblLoginUserPosition.Text = listLoginUserInfo[0].position_description == null || listLoginUserInfo[0].position_description == "" ? "N/A"
                    //                              : listLoginUserInfo[0].position_description;

                    if (user.image_path != null && user.image_path != string.Empty)
                    {
                      //  imgLoginUser.Src = "~/images/image_data/" + user.image_path;
                    }
                    else
                    {
                       // imgLoginUser.Src = "~/images/image_data/Default.png";
                    }
                }

            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
            }
        }

        private void BindDataGridBirthday()
        {
            List<Universal> listResult = Universal.getListBirthday();
            if (listResult != null && listResult.Count > 0)
            {
                lblBirthdayCounter.Text = listResult.Count + " anniversaire(s) aujourd'hui";
                //lblBirthdayBadge.Text = listResult.Count.ToString();
                gridListBirthday.Visible = true;
            }
            else
            {
                lblBirthdayCounter.Text = "0 anniversaire aujourd'hui";
                //lblBirthdayBadge.Text = "0";
                gridListBirthday.Visible = false;
            }
            gridListBirthday.DataSource = listResult;
            gridListBirthday.DataBind();
        }

        protected void gridListBirthday_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked
                // by the user from the Rows collection.
                GridViewRow row = gridListBirthday.Rows[index];
                Session["phone"] = gridListBirthday.DataKeys[index].Value == null ? null : gridListBirthday.DataKeys[index].Value.ToString();
                if (Session["phone"] != null)
                {
                    Session["staff_code"] = row.Cells[1].Text;
                    Session["fullname"] = row.Cells[2].Text;
                    Session["position"] = row.Cells[3].Text;

                    // If multiple buttons are used in a GridView control, use the
                    // CommandName property to determine which button was clicked.
                    if (e.CommandName == "sendSMS")
                    {
                        string page_url = "../pages/sendBirthdaySMS.aspx";

                        //Response.Redirect("DocumentDetail.aspx");
                        //Session["type_detail"] = "endedit";
                        //mp1.Show();
                        string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                        + "oWinn.show();"
                                                        + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                        + "oWinn.SetSize(600, 550);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                        + "oWinn.center();"
                                                        + "Sys.Application.remove_load(f);"
                                                    + "}"
                                                    + "Sys.Application.add_load(f);";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
                else
                {
                    MessBox.Show("Desole, aucun numero de telephone a ete trouve pour " + Session["fullname"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        protected void gridListBirthday_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSendSMSToAll_Click(object sender, EventArgs e)
        {
            if (gridListBirthday.Visible && gridListBirthday.Rows.Count > 0)
            {
                // get list all staff code
                List<Universal> listStaffsInfo = new List<Universal>();
                Universal uni = null;
                //
                for (int i = 0; i < gridListBirthday.Rows.Count; i++)
                {
                    uni = new Universal();
                    uni.staff_code = gridListBirthday.Rows[i].Cells[2].Text;
                    uni.fullName = gridListBirthday.Rows[i].Cells[3].Text;
                    uni.phone = gridListBirthday.Rows[i].Cells[4].Text;
                    uni.position = gridListBirthday.Rows[i].Cells[5].Text;

                    if (uni.phone.Length > 0)
                    {
                        listStaffsInfo.Add(uni);
                    }

                }
                Session["all_staffs_info"] = listStaffsInfo;


                string page_url = "../pages/sendBirthdaySMS.aspx";
                //Response.Redirect("DocumentDetail.aspx");
                //Session["type_detail"] = "endedit";
                //mp1.Show();
                string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                                + "oWinn.show();"
                                                + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                                + "oWinn.SetSize(600, 550);"           // old value ---->    "oWinn.SetSize(1024, 600);"
                                                + "oWinn.center();"
                                                + "Sys.Application.remove_load(f);"
                                            + "}"
                                            + "Sys.Application.add_load(f);";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //lblClockTicking.Text = DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToString("HH:mm:ss");
        }
    }
}