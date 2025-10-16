using System;
using System.Collections.Generic;
using System.Linq;
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
using Telerik.Web.UI;
using System.IO;
using System.Drawing;

using Utilities;



public partial class SalaryClassConfiguration : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.ECONOMAT;
    string msgContent = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        if (!Page.IsPostBack)
        {

            //Session["menu"] = queryMenu;
            Users user = Session["user"] as Users;

            //ddlClassroom.SelectedValue = "-1";
            
            BindDataGridPrice();
            // clear sessions
            Session.Remove("classroom_id");
            Session.Remove("classroom_fullname");
        }
    }

    /*********************** SHOW OPTIONS THAT CAN BE SEEN ACCORDING TO USER POLICY *********/
    // edit
    private void disableEditOption()
    {
        //try
        //{
        //    // loop through the grid to disable edit option
        //    if (gridListClassroom.Visible && gridListClassroom.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListClassroom.Rows.Count; i++)
        //        {
        //            ImageButton imgBtn1 = gridListClassroom.Rows[i].Cells[5].FindControl("btnEdit") as ImageButton;
        //            imgBtn1.Enabled = false;
        //            //
        //            ImageButton imgBtn2 = gridListClassroom.Rows[i].Cells[6].FindControl("btnAddCours") as ImageButton;
        //            imgBtn2.Enabled = false;
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
        //    // do nothing
        //}
        //catch (Exception ex)
        //{
        //    MessBox.Show("Erreur : " + ex.Message);
        //}
    }

    /********************************* CLASSROOM *********************************/

    protected void ddlClassroom_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

        //lblNoclasse.Text = gridListClassroom.SelectedRow.Cells[2].Text;

        //Accessing BoundField Column
        //string name = gridListClassroom.SelectedRow.Cells[0].Text;

        //Accessing TemplateField Column controls
        //string country = (gridListClassroom.SelectedRow.FindControl("lblCountry") as Label).Text;

        //txtNoclasse.Text =  name ;
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        radGridClassroomList.Rebind();
    }

    protected void radGridClassroomList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int _id = 0;
            int _limit = 0;
            List<ClassRoom> listResult = new List<ClassRoom>();
            listResult = ClassRoom.getListActiveClassroom();
            radGridClassroomList.DataSource = listResult;
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    protected void radGridClassroomList_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridClassroomList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridClassroomList.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnDefineAmount_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            int classId = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            string classFullname = dataItem.Cells[1].Text;

            Session["classroom_id"] = classId;
            Session["classroom_fullname"] = classFullname;
            string customWidth = null;

            if (classId < 70)
            {
                 customWidth = "oWinn.SetSize(540, 350);";
            }
            else
            {
                 customWidth = "oWinn.SetSize(1024, 650);";
            }

            string page_url = "DialogSalaryClassConfigDetails.aspx";

            string script = "function f(){var oWinn = window.radopen(\"" + page_url + "\",\"RadWindow1\");"
                                            + "oWinn.show();"
                                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade);"
                                            + customWidth
                                            + "oWinn.center();"
                                            + "Sys.Application.remove_load(f);"
                                        + "}"
                                        + "Sys.Application.add_load(f);";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch { }
    }





    /********************************* LIST OF PRICES / COURS *********************************/


    protected void btnAddPrice_Click(object sender, EventArgs e)
    {
        if (txtCoursePrice.Text.Trim().Length <= 0)
        {
            msgContent = "Erreur : Veuillez entrer un prix !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
            txtCoursePrice.Focus();
        }
        else
        {
            try
            {
                //get the price and convert it to double
                double amount = Convert.ToDouble(txtCoursePrice.Text.Trim());
                List<Course> listPrice = Course.getListCoursePriceByAmount(amount);
                if (listPrice.Count <= 0)
                {
                    //add price
                    Course.addCoursePrice(amount);
                    //refresh gridview
                    BindDataGridPrice();
                    txtCoursePrice.Text = string.Empty;
                    txtCoursePrice.Focus();

                    msgContent = "Succès !";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
                }
                else
                {
                    msgContent = "Le prix " + txtCoursePrice.Text.Trim() + " a ete deja ajoute";
                    MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");

                    txtCoursePrice.Text = string.Empty;
                    txtCoursePrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessBox.Show("Erreur : " + ex.Message);
            }
        }
    }

    private void BindDataGridPrice()
    {
        Users user = Session["user"] as Users;

        List<Course> listResult = Course.getListCoursePrices();
        radGridPrices.DataSource = listResult;
        radGridPrices.DataBind();

    }

    /*
    protected void editPrice(object sender, EventArgs e)
    {
        try
        {
            pnlPriceFixation.Visible = false;
            pnlPriceUpdate.Visible = true;

            ImageButton imageButton = sender as ImageButton;
            //get row index
            int index = Convert.ToInt32(imageButton.CommandArgument);
            int id = int.Parse(gridListCourse.DataKeys[index].Value.ToString());
            GridViewRow row = gridListCourse.Rows[index];
            string amount = row.Cells[1].Text;
            hiddenPriceId.Value = id.ToString();
            txtOldPrice.Text = amount;
            txtNewPrice.Focus();
            //customize the gridview
            BindDataGridPrice2(id);
            gridListCourse.Columns[3].Visible = false;
            gridListCourse.Columns[4].Visible = false;

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnUpdatePrice_Click(object sender, EventArgs e)
    {

        lblError2.Text = string.Empty;
        lblError2.Visible = false;
        if (txtNewPrice.Text.Trim().Length <= 0)
        {
            lblError2.Text = "Echec : Veuillez entrer le nouveau prix.";
            lblError2.ForeColor = Color.Red;
            lblError2.Visible = true;
            txtNewPrice.Focus();
        }
        else
        {
            try
            {
                //get the price and convert it to double
                double newPrice = Convert.ToDouble(txtNewPrice.Text.Trim());
                //get the price id
                int id_price = int.Parse(hiddenPriceId.Value);
                List<Course> listPrice = Course.getListCoursePriceByAmount(newPrice);
                //case when amount does not exsit
                if (listPrice.Count <= 0)
                {
                    //add price
                    Course.updateCoursePrice(newPrice, id_price);
                    //  lblError2.Text = "Successful";
                    //  lblError2.ForeColor = Color.DarkGreen;
                    //   lblError2.Visible = true;
                    //refresh gridview
                    BindDataGridPrice2(id_price);
                    txtOldPrice.Text = txtNewPrice.Text.Trim();
                    txtNewPrice.Text = string.Empty;
                    txtNewPrice.Focus();
                }
                else
                {
                    MessBox.Show("Le prix " + txtNewPrice.Text.Trim() + " exsite, veuillez saisir un autre prix.");
                    txtCoursePrice.Text = string.Empty;
                    txtCoursePrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MessBox.Show("Error : " + ex.Message);
                txtCoursePrice.Focus();
            }
        }
    }
    */

    protected void radGridPrices_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridPrices_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridPrices.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            Course.deleteCoursePriceById(id);
            //refresh data of the gridview
            BindDataGridPrice();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }







}