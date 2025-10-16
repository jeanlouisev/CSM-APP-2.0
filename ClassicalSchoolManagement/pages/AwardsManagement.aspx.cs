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


public partial class AwardsManagement : System.Web.UI.Page
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
            loadListAcademicYear();
            loadActiveClassroom();
        }
    }

    private void loadListAcademicYear()
    {
        try
        {
            ddlAcademicYear.Items.Clear();
            List<Settings> listResult = Settings.getAcademicYearFull();
            if (listResult != null && listResult.Count > 0)
            {
                ddlAcademicYear.DataValueField = "id";
                ddlAcademicYear.DataTextField = "years";
                ddlAcademicYear.DataSource = listResult;
                ddlAcademicYear.DataBind();

                // get current academic year
                int currentAcdemicYear = Settings.getAcademicYear();
                ddlAcademicYear.SelectedValue = currentAcdemicYear.ToString();
            }
        }
        catch (Exception ex) { }
    }

    private void loadActiveClassroom()
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ddlClassroom.DataValueField = "id";
            ddlClassroom.DataTextField = "name";
        }
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        //

        //ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        //ddlClassroom.SelectedIndex = indexCnt;
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
            if (gridAwards.Visible && gridAwards.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridAwards.MasterTableView.Items)
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

    private void BindGrid()
    {
        int classId = int.Parse(ddlClassroom.SelectedValue);
        string vacation = ddlVacation.SelectedValue;
        int control = int.Parse(ddlControl.SelectedValue);
        int accYearId = int.Parse(ddlAcademicYear.SelectedValue);


        List<Student> listResult = Student.getListStudentsForPalmares(classId, vacation, accYearId);

        // reset the gridview
        gridAwards.MasterTableView.Columns.Clear();

        DataTable dt = new DataTable();
        dt.Clear();

        if (listResult != null && listResult.Count > 0)
        {
            // get list of headings
            List<Course> listCourse = Course.getListCourseForPalmares(classId);
            if (listCourse != null && listCourse.Count > 0)
            {
                GridBoundColumn _bColumn = null;

                // code
                _bColumn = new GridBoundColumn();
                gridAwards.MasterTableView.Columns.Add(_bColumn);
                //
                _bColumn.DataField = "id";
                _bColumn.HeaderText = "Code";
                _bColumn.UniqueName = "id";
                _bColumn.HeaderStyle.Width = 150;
                _bColumn.HeaderStyle.Height = 30;

                dt.Columns.Add("id");


                //fullname
                _bColumn = new GridBoundColumn();                
                gridAwards.MasterTableView.Columns.Add(_bColumn);
                //
                _bColumn.DataField = "fullname";
                _bColumn.HeaderText = "Nom & Prénom Élève";
                _bColumn.UniqueName = "fullname";
                _bColumn.HeaderStyle.Width = 150;
                _bColumn.HeaderStyle.Height = 30;
                //

                dt.Columns.Add("fullname");

                foreach (Course c in listCourse)
                {
                    _bColumn = new GridBoundColumn();
                    gridAwards.MasterTableView.Columns.Add(_bColumn);
                    //
                    _bColumn.DataField = c.cours_name;
                    _bColumn.HeaderText = c.cours_name;
                    _bColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    _bColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    _bColumn.HeaderStyle.Width = 150;
                    _bColumn.HeaderStyle.Height = 30;
                    //
                    dt.Columns.Add(c.cours_name);
                }



                List<Notes> listNotes = Notes.getListNotesForPalmares(classId, vacation, control, accYearId);
                foreach (Student st in listResult)
                {
                    DataRow _dr = dt.NewRow();
                    _dr["id"] = st.id;
                    _dr["fullname"] = st.fullName;

                    List<Notes> listTemp = new List<Notes>();
                    foreach (Notes n in listNotes)
                    {
                        if (st.id == n.student_id)
                        {
                            //listTemp.Add(n);
                            _dr[n.cours_name] = n.note_obtained;
                        }
                    }
                    dt.Rows.Add(_dr);

                    //if (listTemp != null && listTemp.Count > 0)
                    //{
                    //    int cnt = 0;
                    //    while(cnt < listTemp.Count) { 
                       

                       
                    //}
                }
                
            }
        }
        else
        {
            MessBox.Show("Pas de données !");
        }




        if (dt != null && dt.Rows.Count > 0)
        {
            gridAwards.DataSource = dt;
        }
        else
        {
            gridAwards.DataSource = null;
        }
        gridAwards.Rebind();


        /*
        // add notes obtained
        if(cnt > 0)
        {
            List<Notes> listNotes = Notes.getListNotesForPalmares(classId);
            if (listNotes != null && listNotes.Count >0)
            {
                foreach (GridDataItem item in gridAwards.MasterTableView.Items)
                {
                    HiddenField hiddenStudentId = (HiddenField)item.FindControl("hiddenStudentId");
                    string studentId = hiddenStudentId.Value;
                    
                    foreach(Notes n in listNotes)
                    {
                        int _cnt = 0;
                        if(n.student_id == studentId)
                        {
                            while(_cnt < gridAwards.MasterTableView.Items.Count)
                            {
                                string colName = gridAwards.Columns[_cnt].UniqueName;

                                _cnt++;
                            }
                        }
                    }
                }
            }
        }

        */

    }

    protected void gridAwards_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridAwards_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridAwards.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
        }
    }

    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        BindGrid();
    }
}
