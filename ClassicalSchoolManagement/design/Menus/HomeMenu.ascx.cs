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


namespace ClassicalSchoolManagement.design.Menus
{
    public partial class HomeMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataGridBirthday();
        }

        protected void gridListBirthday_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
             if (e.CommandName == "SendSMS")
            {
                
            }

        }

        protected void gridListBirthday_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (e.Row.RowType == DataControlRowType.DataRow)
              {
                  if (e.Row.RowIndex == 0)
                      e.Row.Style.Add("height", "60px");
                  e.Row.Style.Add("vertical-align", "bottom");
              }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {

            // lblNoclasse.Text = gridListClassroom.SelectedRow.Cells[2].Text;

            //Accessing BoundField Column
            //string name = gridListClassroom.SelectedRow.Cells[0].Text;

            //Accessing TemplateField Column controls
            //string country = (gridListClassroom.SelectedRow.FindControl("lblCountry") as Label).Text;

            //txtNoclasse.Text =  name ;
        }

        private void BindDataGridBirthday()
        {
           
            List<Student> listResult = null;           
            listResult = Student.getListBirthday();
           

            gridListBirthday.DataSource = listResult;
            gridListBirthday.DataBind();           
        }
    }
}