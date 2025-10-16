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
using Telerik.Web.UI;
using Telerik.Web;
using Utilities;


public partial class AddCours : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;

    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }

        Users user = Session["user"] as Users;
        if (user == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }
        else
        {
            verifyAccessLevel();
        }


        if (!IsPostBack)
        {
            
        }
    }

    private void verifyAccessLevel()
    {
        Users user = Session["user"] as Users;

        // VERIFY USER ACCESS LEVEL
        List<Users> listResult = Users.getListUserAccessLevel(user.role_id, menu_code);
        if (listResult != null && listResult.Count > 0)
        {
            Users userAccess = listResult[0];
            int notGranted = (int)Users.ACCESS.NO;

            // edit
            if (userAccess.edit_access == notGranted)
            {
                disableEditOption();
            }

            // delete
            if (userAccess.delete_access == notGranted)
            {
                disableDeleteOption();
            }
        }
        else
        {
            Response.Redirect("~/Pages/NoPrivilegeWarningPage.aspx");
        }
    }

    // edit_access
    private void disableEditOption()
    {
        try
        {
            //btnSearch.Attributes.Add("disabled", "disabled");
            btnAdd.Attributes.Add("disabled", "disabled");
            
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        try
        {
            // loop through the grid to disable delete option
            if (gridCours.Visible && gridCours.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridCours.MasterTableView.Items)
                {
                    System.Web.UI.HtmlControls.HtmlButton btnDelete = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnDelete");
                    //
                    btnDelete.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
    }

    protected void btnAddCours_Click(object sender, EventArgs e)
    {
        if (txtCourseName.Text.Trim().Length <= 0)
        {
            //lblErrorcoursEmpty.Text = "Veuillez remplir le champs svp !";
            //lblErrorcoursEmpty.ForeColor = System.Drawing.Color.Red;
            //lblErrorcoursEmpty.Visible = true;
            txtCourseName.Focus();
            MessBox.Show("Veuillez remplir le champs svp !");
        }
        else
        {
            Course c = new Course();
            c.name = txtCourseName.Text.Trim();

            //check name
            List<Course> listCourse = Course.checkExistedCourseByName(c.name);
            if (listCourse.Count <= 0)
            {
                Course.addCourse(c);
                //lblErrorcoursEmpty.Text = "Cours ajouter avec succès !";
                //lblErrorcoursEmpty.ForeColor = System.Drawing.Color.Green;
                //lblErrorcoursEmpty.Visible = true;
                txtCourseName.Text = string.Empty;
                txtCourseName.Focus();
                gridCours.Rebind();
                //MessBox.Show("Cours ajouter avec succès !");
            }
            else
            {
                //lblErrorcoursEmpty.Text = "Le cours " + c.name + " a déjà été inséré!";
                //lblErrorcoursEmpty.ForeColor = System.Drawing.Color.Red;
                //lblErrorcoursEmpty.Visible = true;
                MessBox.Show("Le cours " + c.name + " a déjà été inséré!");
            }
        }
    }

    public void removeCourse(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int courseId = int.Parse(dataItem.GetDataKeyValue("id").ToString());

            //if (Course.CourseAssignToTeacher(courseId))
            //{
            //    MessageAlert.RadAlert("Desolé, vous ne pouvez pas supprimer ce cours, Car il est déja assigné a un professeur.",
            //                             350, 250, "Information", null);
            //}
            ////check if cours already affrect to classe
            //else if (Course.CourseAssignToClass(courseId))
            //{
            //    MessageAlert.RadAlert("Desolé, vous ne pouvez pas supprimer ce cours, Car il est déja affecté a une classe.",
            //                           350, 250, "Information", null);
            //}
            ////check if cours already affrect to exam
            //else if (Course.CourseAssignToExam(courseId))
            //{
            //    MessageAlert.RadAlert("Desolé, vous ne pouvez pas supprimer ce cours, Car il est déja affecté a un examen.",
            //                           350, 250, "Information", null);
            //}
            //else
            //{

            Course.deleteCourse(courseId);
            //refresh data of the gridview
            gridCours.Rebind();


            //}
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void btnSearchCours_Click(object sender, EventArgs e)
    {
        gridCours.Rebind();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridCours_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        string courseName = txtCourseName.Text.Trim().Length <= 0 ? null : txtCourseName.Text.Trim();
        //
        List<Course> listResult = Course.getListCourseByName(courseName);
        gridCours.DataSource = listResult;
    }

    protected void gridCours_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridCours_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridCours.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }
}