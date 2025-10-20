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
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.tool.xml;



public partial class BulletinManagement : System.Web.UI.Page
{
    int menu_code = (int)Users.MENU.SECRETARIAT;
    string msgContent = "";


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

        if (!IsPostBack)
        {
            Session.Remove("academic_year");
            loadActiveClassroom();
            loadListAcademicYear();
            BindGridBulletin();
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
            // check if grid notes is visible
            if (gridBulletin.Visible && gridBulletin.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in gridBulletin.MasterTableView.Items)//Running all lines of grid
                {
                    System.Web.UI.HtmlControls.HtmlButton btnPrintPdf = (System.Web.UI.HtmlControls.HtmlButton)item.FindControl("btnPrintPdf");
                    //
                    btnPrintPdf.Attributes.Add("disabled", "disabled");
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur :" + ex.Message);
        }
    }

    // delete_access
    private void disableDeleteOption()
    {
        // nothing to change here
    }

    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        //if (validateFields())
        //{
        BindGridBulletin();
        //}

    }

    private bool validateFields()
    {
        if (ddlClassroom.SelectedValue == "-1")
        {
            MessBox.Show("Erreur : Selectionner une classe !");
            return false;
        }
        else if (ddlVacation.SelectedValue == "-1")
        {
            MessBox.Show("Erreur : Selectionner une vacation");
            return false;
        }
        return true;
    }

    private void loadActiveClassroom()
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        ClassRoom classroom = new ClassRoom();
        listClassroom.Add(classroom);
        ddlClassroom.DataValueField = "id";
        ddlClassroom.DataTextField = "name";
        ddlClassroom.DataSource = listClassroom;
        ddlClassroom.DataBind();
        ddlClassroom.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlClassroom.SelectedValue = "-1";
    }

    public void removeStudent(object sender, EventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //get row index
                int index = Convert.ToInt32(imageButton.CommandArgument);
                //GridViewRow row = gridListStudent.Rows[index];
                string studentCode = null; // row.Cells[1].Text;

                //this part only set status of student to 0
                Student.disableStudent(studentCode);

                // this part delete the student completely from the system
                //  Student.deleteStudentPermanently(studentCode);
                //refresh data of the gridview
                BindGridBulletin();
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    private void loadActiveClassroom(RadDropDownList dropDownList)
    {
        List<ClassRoom> listClassroom = ClassRoom.getListActiveClassroom();
        dropDownList.DataValueField = "id";
        dropDownList.DataTextField = "name";
        dropDownList.DataSource = listClassroom;
        dropDownList.DataBind();
        dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        dropDownList.SelectedValue = "-1";
    }

    private void loadListVacation(List<ClassRoom> listClassroom)
    {
        //clear items
        ddlVacation.Items.Clear();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            // fill the ddl now
            ddlVacation.DataValueField = "vacation_type";
            ddlVacation.DataTextField = "vacation";
            ddlVacation.DataSource = listClassroom;
            ddlVacation.DataBind();
        }
        ddlVacation.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
        ddlVacation.SelectedValue = "-1";
    }

    private void loadListAcademicYear()
    {
        // clear all items
        ddlAcademicYear.Items.Clear();
        //
        List<Settings> listAcaeemicYear = Settings.getAcademicYearFull();
        if (listAcaeemicYear != null && listAcaeemicYear.Count > 0)
        {
            ddlAcademicYear.DataValueField = "id";
            ddlAcademicYear.DataTextField = "years";
            ddlAcademicYear.DataSource = listAcaeemicYear;
            ddlAcademicYear.DataBind();
            // get current academic year
            int currentAcdemicYear = Settings.getAcademicYear();
            ddlAcademicYear.SelectedValue = currentAcdemicYear.ToString();
        }
        else
        {
            ddlAcademicYear.Items.Insert(0, new DropDownListItem(" ", "-1"));
            ddlAcademicYear.SelectedValue = "-1";
        }
    }

    private void loadListAcademicYearEnd(RadDropDownList dropDownList)
    {
        List<Settings> listClassroom = Settings.getListAcademicYearEnd();
        if (listClassroom != null && listClassroom.Count > 0)
        {
            ClassRoom classroom = new ClassRoom();
            dropDownList.DataValueField = "id";
            dropDownList.DataTextField = "end_date";
            dropDownList.DataSource = listClassroom;
            dropDownList.DataBind();
            int currentAcademicYear = Settings.getAcademicYear();
            dropDownList.SelectedValue = currentAcademicYear.ToString();
            //dropDownList.Items.Insert(0, new DropDownListItem("--Tout Sélectionner--", "-1"));
            //dropDownList.SelectedValue = "-1";
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }

    private bool GeneratePdfReportStudent(Student st)
    {
        try
        {
            iTextSharp.text.Rectangle pageSize;
            Document document;
            pageSize = iTextSharp.text.PageSize.A4;
            //document = new Document(pageSize, 88f, 42f, 56f, 56f);
            document = new Document(pageSize, 25f, 25f, 25f, 25f);


            string textSubbmit = "User administrator submit at " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            List<string> listTextSubmit = new List<string>();
            listTextSubmit.Add(textSubbmit);
            // get list sign staffs
            // listSignStaff = Session["listSignStaff"] as List<HrStaffs>;
            //
            PdfWriter writer;
            MemoryStream ms = null;
            ms = new MemoryStream();
            writer = PdfWriter.GetInstance(document, ms);
            //writer.PageEvent = new PDFWriterEvents(listTextSubmit, 10f, document.PageSize.Width,
            //    document.PageSize.Height, 55f);

            //PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            //BaseFont bf = BaseFont.CreateFont(fontpath + "times.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.EMBEDDED);
            //BaseFont bf = BaseFont.CreateFont(
            //Create a specific font object
            iTextSharp.text.Font fontContent = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontApprove = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontTableHeader = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontUnitSign = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontUnitSignUnder = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);
            iTextSharp.text.Font fontDate = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontDate2 = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontSymbol = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fonttxtMemo = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLDITALIC);
            iTextSharp.text.Font fontReceiveBy = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontUserSigning = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontTypeDocument = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontHeading = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontReceiveUnit = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font foneLine1 = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.STRIKETHRU | iTextSharp.text.Font.BOLD);

            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 20f, 40f, 40f };
            table.SetWidthPercentage(widths, document.PageSize);
            table.SpacingBefore = 0f;
            table.SpacingAfter = 0f;
            table.WidthPercentage = 100;
            //table.TotalWidth = 20;

            // name of school
            string imageURL = Server.MapPath("../") + "images/school_logo.jpg";
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageURL);
            PdfPCell cell = new PdfPCell(image, true);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 4;
            cell.Colspan = 1;
            cell.BorderWidth = 0;
            table.AddCell(cell);


            //get list student info
            Student stInfo = Student.getListStudentForBulletin(st)[0];




            /************ HEADER *******************/
            //table = new PdfPTable(1);
            //widths = new float[] { 300f };
            //table.SetWidthPercentage(widths, document.PageSize);
            //table.SpacingBefore = 0f;
            //table.SpacingAfter = 0f;
            //table.TotalWidth = 100;

            // name of school
            cell = new PdfPCell(new Phrase("COLLEGE MIXTE PAR LA FOI", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 1;
            cell.Colspan = 2;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // dirige par [" name of Organization behind the school "]
            cell = new PdfPCell(new Phrase("Dirigé par Dr Michio Kaku", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 1;
            cell.Colspan = 2;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // academic year
            cell = new PdfPCell(new Phrase("Année Académique " + stInfo.years, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 1;
            cell.Colspan = 2;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // Bulletin
            cell = new PdfPCell(new Phrase("BULLETIN", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 1;
            cell.Colspan = 2;
            cell.BorderWidth = 0f;
            table.AddCell(cell);
            document.Add(table);
            //

            Paragraph paragraph = new Paragraph(" ");
            document.Add(paragraph);

            /**************  ADMINISTRATIVE INFORMATION ***********/
            table = new PdfPTable(4);
            widths = new float[] { 100f, 200f, 100f, 200f };
            table.SetWidthPercentage(widths, document.PageSize);
            table.SpacingBefore = 0f;
            table.SpacingAfter = 0f;
            table.WidthPercentage = 100;

            // lblNom
            cell = new PdfPCell(new Phrase("Nom", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // txtNom
            cell = new PdfPCell(new Phrase(": " + stInfo.first_name.ToUpper(), fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // lblPrenom
            cell = new PdfPCell(new Phrase("Prénom", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // txtPrenom
            cell = new PdfPCell(new Phrase(": " + stInfo.last_name.ToUpper(), fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);
            document.Add(table);

            // lblIdentifiant
            cell = new PdfPCell(new Phrase("Identifiant", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // txtIdentifiant
            cell = new PdfPCell(new Phrase(": " + stInfo.id, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // lblClasse
            cell = new PdfPCell(new Phrase("Classe", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);


            // txtClasse
            cell = new PdfPCell(new Phrase(": " + stInfo.classroom_name, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);
            document.Add(table);

            // Control
            cell = new PdfPCell(new Phrase("Controle", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            string controlStr = "";
            switch (stInfo.control)
            {
                case 1:
                    controlStr = "1er"; break;
                case 2:
                    controlStr = "2ieme"; break;
                case 3:
                    controlStr = "3ieme"; break;
                case 4:
                    controlStr = "4ieme"; break;
            }

            cell = new PdfPCell(new Phrase(": " + controlStr, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);
            //
            // vacation
            cell = new PdfPCell(new Phrase("Vacation", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(": " + stInfo.vacation, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidth = 0f;
            table.AddCell(cell);
            document.Add(table);


            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            /****************** NOTES *******************/

            Notes n = new Notes();
            n.student_id = st.id;
            n.vacation = st.vacation;
            n.classroom_id = st.classroom_id;
            n.academic_year_id = st.academic_year_id;
            n.control = st.control;

            // get list notes
            List<Notes> listNotes = Notes.getListNotesForBulletin(n);

            double totalCoefficient = 0;
            double totalNotes = 0;
            int courseCounter = listNotes.Count;

            table = new PdfPTable(3);
            widths = new float[] { 300f, 200f, 100f };
            table.SetWidthPercentage(widths, document.PageSize);
            table.SpacingBefore = 0f;
            table.SpacingAfter = 0f;
            table.WidthPercentage = 100;

            // lblMatiere
            cell = new PdfPCell(new Phrase("Matière", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            // lblCoefficient
            cell = new PdfPCell(new Phrase("Coéfficient", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            // lblNotes
            cell = new PdfPCell(new Phrase("Notes", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);
            document.Add(table);
            //
            if (listNotes != null && listNotes.Count > 0)
            {
                for (int i = 0; i < listNotes.Count; i++)
                {
                    // lblMatiere value
                    cell = new PdfPCell(new Phrase(listNotes[i].cours_name, fontUnitSign));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Rowspan = 0;
                    table.AddCell(cell);

                    // lblCoefficient value
                    cell = new PdfPCell(new Phrase(listNotes[i].coefficient.ToString(), fontUnitSign));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Rowspan = 0;
                    table.AddCell(cell);
                    // add coefficient to get total
                    totalCoefficient += listNotes[i].coefficient;

                    // lblNotes
                    cell = new PdfPCell(new Phrase(listNotes[i].note_obtained.ToString(), fontUnitSign));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Rowspan = 0;
                    table.AddCell(cell);
                    document.Add(table);
                    // add notes to get total
                    totalNotes += listNotes[i].note_obtained;
                }
            }

            // total notes

            cell = new PdfPCell(new Phrase("TOTAL ", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(totalCoefficient.ToString(), fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(totalNotes.ToString(), fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);
            document.Add(table);

            // calulate the average of the student
            double totalAverage = 0;
            if (totalCoefficient > 0)
            {
                totalAverage = Math.Round(totalNotes * 10 / totalCoefficient, 2);
            }

            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            /****** MOYENNE calculation *******/
            table = new PdfPTable(3);
            widths = new float[] { 35f, 35f, 35f };
            table.SetWidthPercentage(widths, document.PageSize);
            table.SpacingBefore = 0f;
            table.SpacingAfter = 0f;
            table.WidthPercentage = 100;

            // lblTotal
            cell = new PdfPCell(new Phrase("Notes Total : " + totalNotes + " / " + totalCoefficient, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            // lblMoyenne
            cell = new PdfPCell(new Phrase("Moyenne : " + totalAverage + " / 10", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            string result = "";
            ClassRoom c = ClassRoom.getListClassroomAverage(st.academic_year_id, st.classroom_id)[0];
            if (c != null)
            {
                totalAverage = totalAverage * 10;
                if (totalAverage < c.success_percent)
                {
                    result = "Échec";
                }
                else
                {
                    result = "Succès";
                }
            }
            else
            {
                result = "N/A";
            }

            //int valueStatus = (totalAverage < 5) ? 0 : 1;
            //result = valueStatus == 0 ? "Echec" : "Succes";
            // status monyenne pass or fail
            cell = new PdfPCell(new Phrase("Status : " + result, fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            table.AddCell(cell);

            //
            document.Add(table);

            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            paragraph = new Paragraph(" ");
            document.Add(paragraph);

            /********** SIGNATURE *************/
            table = new PdfPTable(3);
            widths = new float[] { 100f, 40f, 100f };
            table.SetWidthPercentage(widths, document.PageSize);
            table.SpacingBefore = 0f;
            table.SpacingAfter = 0f;
            table.WidthPercentage = 100;

            // lblSignatureResponsable
            cell = new PdfPCell(new Phrase("Signature Direction", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthTop = 1f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.BorderWidth = 0f;
            table.AddCell(cell);

            // lblSignatureResponsable
            cell = new PdfPCell(new Phrase("Signature Responsable", fontUnitSign));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Rowspan = 0;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthTop = 1f;
            table.AddCell(cell);
            //
            document.Add(table);


            // close document
            document.Close();

            // download from stream
            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment;filename=preview.pdf");
            //Response.Buffer = true;
            //Response.Clear();
            var bytes = ms.ToArray();

            string path =
              Server.MapPath("..") + "\\preview\\Administrator";
            string filePath = path + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + genRandom() + ".pdf";
            Session["filePath"] = filePath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = File.Create(filePath))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            string url = "../preview/Administrator/" + Path.GetFileName(filePath);
            //FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            //ms.WriteTo(file);
            //file.Close();
            //ms.Close();
            //document.Close();

            string script = "function f(){var oWinn = window.radopen(\"" + url + "\",\"RadWindow1\"); "
                            + "oWinn.set_animation(Telerik.Web.UI.WindowAnimation.Fade); oWinn.setSize(900, 600); oWinn.center();"
                            + "Sys.Application.remove_load(f);} Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            //RadWindowViewPdf.NavigateUrl = filePath; 
            //Response.OutputStream.Write(bytes, 0, bytes.Length); 
            //Response.Redirect("CreateDocuments.aspx?type=preview");
            //Response.OutputStream.Flush();
            //Response.End(); 


            // close document
            //document.Close();
            return true;
        }
        catch (Exception ex)
        {
            //                return false;
            throw ex;
        }
    }

    private string genRandom()
    {
        try
        {
            Random rnd = new Random();
            String text = "abcdefghijklmnopqrstuvxyz0123456789";
            String result = "";
            for (int i = 0; i < 10; i++)
            {
                result += text[rnd.Next(35)];
            }
            return result;
        }
        catch
        {
            return "preview";
        }
    }

    private void BindGridBulletin()
    {
        Student st = new Student();
        //get the fields from the form
        st.id = txtCode.Text.Trim().Length <= 0 ? null : txtCode.Text.Trim();
        st.fullName = txtFullName.Text.Trim().Length <= 0 ? null : "%" + txtFullName.Text.Trim().ToLower() + "%";
        st.vacation = ddlVacation.SelectedValue;
        st.classroom_id = int.Parse(ddlClassroom.SelectedValue);
        st.academic_year_id = int.Parse(ddlAcademicYear.SelectedValue);
        st.control = int.Parse(ddlControl.SelectedValue);

        //Session["academic_year"] = st.academic_year;

        //get list of students
        List<Student> listResult = Student.getListStudentForBulletin(st);

        if (listResult == null)
        {
            listResult = new List<Student>();
        }

        gridBulletin.DataSource = listResult;
        gridBulletin.DataBind();

        //
        verifyAccessLevel();
    }

    protected void gridBulletin_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void gridBulletin_ItemDataBound(object sender, GridItemEventArgs e)
    {
        int cnt = gridBulletin.Items.Count + 1;
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl = (Label)item.FindControl("labelNo");
            lbl.Text = cnt.ToString();

            // control
            item.Cells[9].Text = ddlControl.SelectedText;
        }
    }

    protected void btnPrintPdf_ServerClick(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlButton btn = sender as System.Web.UI.HtmlControls.HtmlButton;
        GridDataItem dataItem = (GridDataItem)btn.NamingContainer;
        //
        HiddenField hiddenClassId = (HiddenField)dataItem.FindControl("hiddenClassId");
        HiddenField hiddenVacationCode = (HiddenField)dataItem.FindControl("hiddenVacationCode");
        HiddenField hiddenStudentId = (HiddenField)dataItem.FindControl("hiddenStudentId");
        HiddenField hiddenControl = (HiddenField)dataItem.FindControl("hiddenControl");
        HiddenField hiddenAcademicYearId = (HiddenField)dataItem.FindControl("hiddenAcademicYearId");
        HiddenField hiddenYears = (HiddenField)dataItem.FindControl("hiddenYears");

        Student st = new Student();
        st.id = hiddenStudentId.Value;
        st.vacation = hiddenVacationCode.Value;
        st.classroom_id = int.Parse(hiddenClassId.Value);
        st.academic_year_id = int.Parse(hiddenAcademicYearId.Value);
        st.control = int.Parse(hiddenControl.Value);
        st.years = hiddenYears.Value;
        //
        GeneratePdfReportStudent(st);

    }
}