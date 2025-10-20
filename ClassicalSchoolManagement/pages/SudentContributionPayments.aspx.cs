using iTextSharp.tool.xml.xtra.xfa.js;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web;
using Telerik.Web.UI;
using Utilities;


public partial class SudentContributionPayments : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.STUDENT;

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

        // check pay button's status
        if (txtStudentId.Text.Trim().Length > 0)
        {
            btnPay.Attributes.Remove("disabled");
        }

        if (!IsPostBack)
        {
            loadContrybutionTypes();


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
            //btnAdd.Attributes.Add("disabled", "disabled");

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
            if (gridContributionPayment.Visible && gridContributionPayment.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridContributionPayment.MasterTableView.Items)
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

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (txtDescription.Text.Trim().Length <= 0)
    //    {
    //        MessageAlert.RadAlert("Erreur : Veuillez remplir tous les champs !", 300, 200, "Information", null, "../images/error.png");
    //    }
    //    else
    //    {
    //        // check if user wants to update or add new
    //        if (hiddenId.Value == null || hiddenId.Value == "") // then add new
    //        {
    //            string description = txtDescription.Text.Trim();
    //            double price = 0;

    //            //check if description already exists
    //            List<StudentContribution> listResult = StudentContribution.descriptionAlreadyExist(description);
    //            if (listResult.Count <= 0)
    //            {
    //                StudentContribution.addContributionType(description.ToUpper(), price);
    //                // clear form
    //                txtDescription.Text = string.Empty;
    //                txtDescription.Focus();
    //                gridContributionPayment.Rebind();

    //                MessageAlert.RadAlert("Enregistré avec succès !", 300, 200, "Information", null, "../images/success_check.png");
    //            }
    //            else
    //            {
    //                MessageAlert.RadAlert("Erreur : La contribution : " + description + " Existe !", 300, 200, "Information", null, "../images/error.png");

    //            }
    //        }
    //        else  // then update
    //        {

    //            StudentContribution sc = new StudentContribution();
    //            sc.id = int.Parse(hiddenId.Value);
    //            sc.description = txtDescription.Text.Trim();
    //            // update information
    //            StudentContribution.updateContributionType(sc);
    //            // cear forms
    //            hiddenId.Value = null;
    //            txtDescription.Text = "";
    //            txtDescription.Enabled = true;
    //            txtDescription.Focus();
    //            gridContributionPayment.Rebind();

    //            MessageAlert.RadAlert("Modifié avec succès !", 300, 200, "Information", null, "../images/success_check.png");
    //        }

    //    }
    //}

    private void loadContrybutionTypes()
    {
        List<StudentContribution> listResult = StudentContribution.getListContributionTypes();
        ddlContributionType.DataValueField = "id";
        ddlContributionType.DataTextField = "description";
        ddlContributionType.DataSource = listResult;
        ddlContributionType.DataBind();
        //
        ddlContributionType.Items.Insert(0, new DropDownListItem("--Sélectionner--", "-1"));
        ddlContributionType.SelectedValue = "-1";
    }
    public void removePayment(object sender, EventArgs e)
    {

        try
        {
            ImageButton imageButton = sender as ImageButton;
            //get row index
            System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
            GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
            //int index = dataItem.RowIndex;
            int id = int.Parse(dataItem.GetDataKeyValue("id").ToString());
            //
            StudentContribution.removePayments(id);
            // reload the grid
            gridContributionPayment.Rebind();

            MessageAlert.RadAlert("Supprimé avec succès !", 300, 200, "Information", null, "../images/success_check.png");

        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    protected void gridContributionPayment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        List<StudentContribution> listResult = new List<StudentContribution>();
        // get student code
        if (txtStudentId.Text.Trim().Length > 0)
        {
            Student st = new Student();
            st.id = hiddenStudentId.Value;
            st.academic_year_id = int.Parse(hiddentAccYearId.Value);
            st.classroom_id = int.Parse(hiddenClassroomId.Value);

            listResult = StudentContribution.getListContributionForPayment(st);
        }

        gridContributionPayment.DataSource = listResult;
    }

    protected void gridContributionPayment_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridContributionPayment_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridContributionPayment.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();
        }
    }

    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        if (txtStudentId.Text.Trim().Length <= 0) // request student id from user
        {
            btnPay.Attributes.Add("disabled", "disabled");
            //
            MessageAlert.RadAlert("Erreur : Veuillez tapez le code de l\\'élève !", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            string studentId = txtStudentId.Text.Trim();
            // check if student code is valid or not
            if (Student.StudentExist(studentId))
            {
                Student st = Student.getStudentFullDetailsById(studentId);
                if (st != null)
                {
                    // fill form
                    txtFullname.Text = st.last_name + " " + st.first_name;
                    txtClassroomName.Text = st.classroom_name;
                    txtAcademicYear.Text = st.years;
                    txtVacation.Text = st.vacation;
                    //
                    hiddenStudentId.Value = st.id;
                    hiddenClassroomId.Value = st.classroom_id.ToString();
                    hiddentAccYearId.Value = st.academic_year_id.ToString();
                    // enable pay button
                    btnPay.Attributes.Remove("disabled");
                    // realod the grid
                    gridContributionPayment.Rebind();
                }
            }
            else
            {
                MessageAlert.RadAlert("Erreur : Ce code n\\'éxiste pas !", 300, 200, "Information", null, "../images/error.png");
            }
        }
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {

    }

    protected void btnPay_ServerClick(object sender, EventArgs e)
    {
        // step 1.- check that user select a contribution type
        if (ddlContributionType.SelectedValue == "-1")
        {
            MessageAlert.RadAlert("Information : Veuillez tapez le code de l\\'élève !", 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            Users user = Session["user"] as Users;

            // step 2.- verify that student did not already pay
            StudentContribution sc = new StudentContribution();
            sc.student_id = txtStudentId.Text.Trim();
            sc.academic_year_id = int.Parse(hiddentAccYearId.Value);
            sc.classroom_id = int.Parse(hiddenClassroomId.Value);
            sc.contribution_type_id = int.Parse(ddlContributionType.SelectedValue);
            sc.paid_amount = double.Parse(txtContributionAmount.Value.ToString());
            sc.status = (int)StudentContribution.PAYMENT_STATUS.Paid;
            sc.login_user_id = user.id;
            //
            if (StudentContribution.isAlreadyPaid(sc))
            {
                MessageAlert.RadAlert("Information : Ce type de contribution a été déja payé !", 300, 200, "Information", null, "../images/error.png");
            }
            else
            {
                StudentContribution.makePayments(sc);
                // reload the grid
                gridContributionPayment.Rebind();
                //
                MessageAlert.RadAlert("Payé avec succès !", 300, 200, "Information", null, "../images/success_check.png");
            }
        }
    }

    protected void ddlContributionType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddl = sender as RadDropDownList;

        if (ddl.SelectedValue == "-1")
        {
            txtContributionAmount.Value = null;
        }
        else
        {
            // get current contribution price
            int contId = int.Parse(ddl.SelectedValue);
            double price = StudentContribution.getContributionPriceById(contId);
            txtContributionAmount.Value = price;
        }

    }

}