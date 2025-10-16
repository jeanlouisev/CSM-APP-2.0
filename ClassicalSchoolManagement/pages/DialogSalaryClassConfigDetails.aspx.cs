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




public partial class DialogSalaryClassConfigDetails : System.Web.UI.Page
{
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


        if (Session["user"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["classroom_id"] != null)
            {
                hiddenClassId.Value = Session["classroom_id"].ToString();
                int classroomId = int.Parse(Session["classroom_id"].ToString());
                //
                if (Universal.classroomIsPrimary(classroomId))
                {
                    divPrimary.Visible = true;
                    divSecondary.Visible = false;
                    loadPrimaryClassInfo();
                }
                else
                {
                    divPrimary.Visible = false;
                    divSecondary.Visible = true;
                    loadSecondaryClassInfo();
                    // load data in secondary gridview                    
                }
            }
        }
    }

    private void loadListAcademicYear(RadComboBox ddl)
    {
        try
        {
            List<Settings> listAcademicYear = Settings.getAcademicYearFull();
            ddl.Items.Clear();
            if (listAcademicYear != null && listAcademicYear.Count > 0)
            {
                ddl.DataValueField = "id";
                ddl.DataTextField = "years";
                ddl.DataSource = listAcademicYear;
                ddl.DataBind();
            }
        }
        catch (Exception ex) { }
    }

    private void loadPrimaryClassInfo()
    {
        int classId = int.Parse(Session["classroom_id"].ToString());
        ClassRoom c = ClassRoom.getListPrimaryClassInfo(classId);
        txtClassNamePrimary.Text = c.classroom_name;
        txtAmount.Value = c.amount;
    }

    private void loadSecondaryClassInfo()
    {
        int classId = int.Parse(Session["classroom_id"].ToString());
        ClassRoom c = ClassRoom.getClassroomInfoById(classId);
        txtClassNameSecondary.Text = c.classroom_name;
    }

    protected void btnValidatePrimarySalary_Click(object sender, EventArgs e)
    {
        //    Users user = Session["user"] as Users;
        //    List<ClassRoom> listClassroom = new List<ClassRoom>();

        //    if (gridListPrimary.Visible && gridListPrimary.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gridListPrimary.Rows.Count; i++)
        //        {
        //            RadNumericTextBox txtAmount = gridListPrimary.Rows[i].FindControl("txtPrimaryAmount") as RadNumericTextBox;
        //            HiddenField hiddenVacation = gridListPrimary.Rows[i].FindControl("hiddenVacationId") as HiddenField;
        //            //
        //            ClassRoom c = new ClassRoom();
        //            c.class_id = int.Parse(hiddenClassroomId.Value);
        //            c.amount = txtAmount.Value == null ? 0 : Double.Parse(txtAmount.Value.ToString());
        //            c.vacation_type = hiddenVacation.Value;
        //            c.status = 1; // the amount is the active amount
        //            c.login_user_id = user.id;
        //            //
        //            listClassroom.Add(c);
        //        }
        //    }

        //    if (listClassroom != null && listClassroom.Count > 0)
        //    {
        //        // add amount
        //        ClassRoom.insertSalaryAmountForClassroom(listClassroom);
        //        MessBox.Show("Valider avec succes !");
        //        //}
        //        //else
        //        //{
        //        //    MessBox.Show("Desole, le montant ne peut etre ajouter !");
        //        //}
        //    }
        //    else
        //    {
        //        MessBox.Show("Desole, il y a rien à valider !");
        //    }
    }

    //protected void cboVacation_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    if (hiddenClassroomId.Value != null)
    //    {
    //        int classroomId = int.Parse(hiddenClassroomId.Value);
    //        string vacationType = cboVacation.SelectedValue;

    //        if (Universal.classroomIsPrimary(classroomId))
    //        {
    //            List<ClassRoom> listClassroom = ClassRoom.getClassroomCurrentSalary(classroomId, vacationType);
    //            if (listClassroom != null && listClassroom.Count > 0)
    //            {
    //                txtAmount.Value = listClassroom[0].amount;
    //            }
    //        }
    //        else
    //        {
    //            // hide items
    //            tr_error_msg.Visible = false;
    //            tr_salary_actual.Visible = false;
    //            tr_validate_button.Visible = false;

    //            //  load the gridview
    //            
    //        }

    //    }
    //}

    protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {

    }

    protected void ddlVacation_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }

    protected void btnSavePrimaryClassInfo_ServerClick(object sender, EventArgs e)
    {
        if (txtAmount.Value <= 0)
        {
            msgContent = "Erreur : Veuillez saisir le montant !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            int classId = int.Parse(hiddenClassId.Value);
            double amount = double.Parse(txtAmount.Value.ToString());
            ClassRoom.InsertFixedPayment(classId, amount);

            // clear sessions
            Session["classroom_id"] = null;
            Session.Remove("classroom_id");

            // close modal window after validation
            this.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseDialog();", true);
        }
    }

    protected void btnSaveSecondaryClassInfo_ServerClick(object sender, EventArgs e)
    {
        if (radGridCourseSecondary.MasterTableView.Items.Count <= 0)
        {
            msgContent = "Désolé, rien à sauvegarder !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/error.png");
        }
        else
        {
            List<ClassRoom> listClassPrice = new List<ClassRoom>();
            int classId = int.Parse(hiddenClassId.Value);
            foreach(GridItem item in radGridCourseSecondary.MasterTableView.Items)
            {
                HiddenField hiddenCourseId = (HiddenField)item.FindControl("hiddenCourseId");
                RadComboBox cboPrices = (RadComboBox)item.FindControl("cboPrices");
                //
                ClassRoom c = new ClassRoom();
                c.id = classId;
                c.cours_id = int.Parse(hiddenCourseId.Value);
                c.amount = double.Parse(cboPrices.SelectedValue);
                if (c.amount > 0)
                {
                    listClassPrice.Add(c);
                }
            }
            // save
            ClassRoom.InsertHourlyPayment(listClassPrice);
            //
            msgContent = "Succès !";
            MessageAlert.RadAlert(msgContent, 300, 200, "Information", null, "../images/success_check.png");
        }
    }

    protected void radGridCourseSecondary_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        int classId = int.Parse(Session["classroom_id"].ToString());
        List<Course> listResult = listResult = Course.getListAffectedCoursePrice(classId);
        //
        radGridCourseSecondary.DataSource = listResult;
    }

    protected void radGridCourseSecondary_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void radGridCourseSecondary_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = radGridCourseSecondary.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();


            // add cours prices
            List<Course> listCoursPrices = Course.getListCoursePrices();
            HiddenField hiddenAmount = (HiddenField)item.FindControl("hiddenAmount");
            RadComboBox cbo = (RadComboBox)item.FindControl("cboPrices");
            double _amount = double.Parse(hiddenAmount.Value);

            // fill comboe with data
            if (listCoursPrices != null && listCoursPrices.Count > 0)
            {
                foreach (Course c in listCoursPrices)
                {
                    //cbo.Items.Add(c.amount.ToString());
                    cbo.Items.Insert(0, new RadComboBoxItem(c.amount.ToString(), c.amount.ToString()));
                }
            }
            // set value for 1st index
            cbo.Items.Insert(0, new RadComboBoxItem("","-1"));
            // chose the selected value
            if(_amount > 0)
            {
                cbo.SelectedValue = hiddenAmount.Value;
            }

        }
    }
}
