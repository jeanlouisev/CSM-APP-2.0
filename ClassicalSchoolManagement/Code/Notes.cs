using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities;
using Telerik.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Db_Core;



public class Notesxxx
{
    //getters and setters
    public int id { get; set; }
    public string student_id { get; set; }
    public double note_obtained { get; set; }
    public string teacher_id { get; set; }
    public int classroom_id { get; set; }
    public string vacation { get; set; }
    public string vacation_code { get; set; }
    public int academic_year_id { get; set; }
    public int cours_id { get; set; }
    public int control { get; set; }
    public string student_fullname { get; set; }
    public string cours_name { get; set; }
    public string teacher_fullname { get; set; }
    public double success_percent { get; set; }
    public string academic_year_start { get; set; }
    public string academic_year_end { get; set; }
    public string login_user { get; set; }
    public DateTime date_register { get; set; }
    public double coefficient { get; set; }
    public string class_name { get; set; }
    public string years { get; set; }

    // exam part
    //public string id_exam { get; set; }
    //public double exam_points { get; set; }
    //public string exam_period { get; set; }
    //public string exam_code { get; set; }
    //public DateTime exam_date { get; set; }


    public static List<Notes> Parse(MySqlDataReader reader)
    {
        List<Notes> listNotes = new List<Notes>();
        try
        {
            while (reader.Read())
            {
                Notes notes = new Notes();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { notes.id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_ID")
                    {
                        try { notes.student_id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NOTE_OBTAINED")
                    {
                        try { notes.note_obtained = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { notes.date_register = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { notes.academic_year_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTROL")
                    {
                        try { notes.control = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_FULLNAME")
                    {
                        try { notes.student_fullname = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { notes.class_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { notes.years = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { notes.cours_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_ID")
                    {
                        try { notes.cours_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_FULLNAME")
                    {
                        try { notes.teacher_fullname = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_VACATION")
                    {
                        try { notes.vacation = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_ID")
                    {
                        try { notes.teacher_id = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "classroom_id")
                    {
                        try { notes.classroom_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SUCCESS_PERCENT")
                    {
                        try { notes.success_percent = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_START")
                    {
                        try { notes.academic_year_start = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_END")
                    {
                        try { notes.academic_year_end = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { notes.login_user = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COEFFICIENT")
                    {
                        try { notes.coefficient = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "AM": notes.vacation = "Matin"; break;
                                case "PM": notes.vacation = "Median"; break;
                                case "NG": notes.vacation = "Soir"; break;
                                case "WK": notes.vacation = "Weekend"; break;
                                default: notes.vacation = ""; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { notes.vacation_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { notes.class_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { notes.cours_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }



                    // exam purpose

                    //if (reader.GetName(i).ToUpper() == "EXAM_DATE")
                    //{
                    //    try { notes.exam_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                    //    catch { }
                    //}
                    //if(reader.GetName(i).ToUpper() == "EXAM_CODE")
                    //{
                    //    try { notes.exam_code = reader.GetValue(i).ToString(); }
                    //    catch { }
                    //}
                    //if (reader.GetName(i).ToUpper() == "EXAM_POINTS")
                    //{
                    //    try { notes.exam_points = reader.GetDouble(i); }
                    //    catch { }
                    //}
                    //if (reader.GetName(i).ToUpper() == "ID_EXAM")
                    //{
                    //    try { notes.id_exam = reader.GetString(i); }
                    //    catch { }
                    //}
                }
                listNotes.Add(notes);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listNotes;
    }

    public static void addNotes(List<Notes> listNotes)
    {    try
        {
            if (listNotes != null && listNotes.Count > 0)
            {
                // delete existing notes
                Notes.deleteNotes(listNotes);

                // insert new notes
                foreach (Notes n in listNotes)
                {
                    string sql = @"INSERT INTO notes(student_id, classroom_id, vacation, control, academic_year_id, 
                                        cours_id, coefficient, note_obtained, date_register, login_user)
                                        values(?,?,?,?,?,?,?,?,now(),?)";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(n.student_id, n.classroom_id, n.vacation, n.control, n.academic_year_id, 
                                        n.cours_id, n.coefficient, n.note_obtained, n.login_user);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Notes> getListNotes(Notes note)
    {
        try
        {
            string sql = @"select st.id as student_id, 
                            concat(st.First_name, ' ', st.Last_name) as student_fullname, 
                            csm.vacation,
							cr.id as classroom_id,
                            cr.name as class_name, 
							acc.id as academic_year_id,
                            concat(EXTRACT(year FROM acc.start_date), '-', EXTRACT(year FROM acc.end_date)) as years,
							c.id as cours_id,
                            c.name as cours_name, 
                            ccm.coefficient,
                            (select coalesce(n.note_obtained,0) from notes n
                                where n.student_id = st.id
                                and n.classroom_id = cr.id
                                and n.vacation = csm.vacation
                                and n.academic_year_id = acc.Id
                                and n.cours_id = c.id
                                and n.control = " + note.control+ @"
                                ) as note_obtained
                            FROM student st
                                inner join classroom_staff_management csm on csm.staff_code = st.id
                                inner join classroom cr on cr.id = csm.classroom_id
                                inner join academic_year acc on acc.id = csm.academic_year_id
								inner join classroom_cours_management ccm on ccm.classroom_id = csm.classroom_id
								inner join cours c on c.id = ccm.cours_id
                            WHERE st.status = 1
                                [  and csm.classroom_id = ? ] -- 0
                                [  AND csm.vacation = ? ]  -- 1
                                [  AND st.id = ? ]  -- 2
                                [  AND acc.id = ? ]  -- 3
                                [  AND ccm.cours_id = ? ]  -- 4
                            order by student_fullname, cours_name asc";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);

            

            if (note.classroom_id > 0)
            {
                stmt.SetParameter(0, note.classroom_id);
            }
            if (note.vacation != null)
            {
                stmt.SetParameter(1, note.vacation);
            }
            if (note.student_id != null)
            {
                stmt.SetParameter(2, note.student_id);
            }
            if (note.academic_year_id > 0)
            {
                stmt.SetParameter(3, note.academic_year_id);
            }
            if (note.cours_id > 0)
            {
                stmt.SetParameter(4, note.cours_id);
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Notes> getListNotesTemplates(Notes n)
    {
        string sql = @"SELECT 
                            a.id as student_code,
                            concat(a.Last_name,' ',a.First_name) as student_fullname,
                            b.id_class as classroom_id,
                            f.name as classroom_fullname,
                            c.id as exam_code, 
                            c.points as exam_points,
                            (select student_points from notes where id_exam = c.id
                              and Id_student = a.id and academic_year = c.academic_year 
                               and control = c.control) as student_points,
                            c.control as exam_period,
                            c.exam_date,
                            d.name as cours_fullname,
                            d.id as cours_id,
                            c.vacation,
                            concat(e.Last_name,' ',e.First_name) as teacher_fullname,
                            e.id as id_teacher,c.control,c.academic_year
                            from student a , classroom_staff_management b, 
                            exam c, cours d, teacher e, classroom f
                            WHERE a.status = 1
                               
                            order by a.last_name asc";



                            //AND admission_status = 1
                            //AND a.id = b.staff_code
                            //AND c.id_class = b.id_class
                            //AND d.id = c.id_cours
                            //AND e.id = c.id_teacher
                            //AND f.id = c.id_class
                            //AND b.status = 1
                            //AND c.id_teacher = ?
                            //AND c.id_class = ?
                            //AND c.vacation = ?
                            //AND c.control = ?
                            //AND c.id_cours = ?
                            //AND c.academic_year = ?
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(n.teacher_id.ToUpper(),
                                n.classroom_id,
                                n.vacation.ToUpper(),
                                //n.exam_period,
                                n.cours_id,
                                n.academic_year_id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Notes> getListPreviousAverageConfiguration(int currentAcademicYear)
    {
        string sql = @"select distinct(a.academic_year) as academic_year,
                                    (select concat(first_name,' ',Last_name) from staff
                                           where id = a.login_user) as login_user,
                                    extract(YEAR from b.start_date) as academic_year_start,
                                    extract(YEAR from b.end_date) as academic_year_end,
                                    a.date_register
                                FROM classroom_average_management a, academic_year b
                                WHERE 1=1
                                AND a.academic_year = b.id
                                AND a.academic_year not in (?)
                                ORDER by a.academic_year asc ";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(currentAcademicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteNotes(Notes n)
    {
        string sql = @"delete from notes 
                            where id_student = ?
                            and control = ?
                            and academic_year_id = ?
                            and id_exam = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(n.student_id,
                                n.control,
                                n.academic_year_id
                                //n.id_exam
                                );
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteNotes(List<Notes> listNotes)
    {
        try
        {
            if (listNotes != null && listNotes.Count > 0)
            {
                foreach (Notes n in listNotes)
                {
                    string sql = @"delete from notes 
                            where student_id = ?
                                and classroom_id = ?
                                and vacation = ?
                                and control = ?
                                and academic_year_id = ?
                                and cours_id = ?";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(n.student_id, n.classroom_id, n.vacation, n.control, n.academic_year_id, n.cours_id );
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Notes> getListNotesForBulletin(Notes n)
    {
        try
        {
            string sql = @"select concat(st.First_name,' ',st.Last_name) as fullname,
                                c.name as cours_name,
                                concat(EXTRACT(year FROM acc.start_date),'-',EXTRACT(year FROM acc.end_date)) as years,
                                n.*
                            FROM notes n
                                inner join student st on st.id = n.student_id
                                inner join cours c on c.id = n.cours_id
                                inner join academic_year acc on acc.id = n.academic_year_id
                            WHERE n.student_id = ?
                                and n.vacation = ?
                                and n.classroom_id = ?
                                and n.academic_year_id = ?
                                and n.control = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(n.student_id, n.vacation, n.classroom_id, n.academic_year_id, n.control);
            //
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Notes> getListNotesForPalmares(
        int classId, string vacation, int control, int accYearId)
    {
        string sql = @"select st.id as student_id, concat(st.first_name,' ',st.last_name) as fullname,
                        ccm.cours_id, c.name as cours_name, ccm.coefficient,
                        n.note_obtained
                        from student st
                        inner join classroom_staff_management csm on csm.staff_code = st.id
                        inner join classroom_cours_management ccm on ccm.classroom_id = csm.classroom_id
                        inner join cours c on c.id = ccm.cours_id
                        left join notes n on n.classroom_id = csm.classroom_id and n.student_id = st.id and n.cours_id = c.id
                        where  n.classroom_id = ?
						   and n.vacation = ?
						   and n.control = ?
						   and n.academic_year_id = ?
                        group by st.id, c.id
                        order by fullname, c.name";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation, control, accYearId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    //public static bool ExamAssignToNote(string id_exam)
    //{
    //    bool result = false;

    //    string sql = @"SELECT COUNT(*) FROM NOTES WHERE upper(id_exam) = upper(?)";

    //    try
    //    {
    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(id_exam);
    //        IDataReader reader = stmt.ExecuteReader();
    //        if (reader != null)
    //        {
    //            while (reader.Read())
    //            {
    //                if (reader.GetInt32(0) > 0)
    //                {
    //                    result = true;
    //                }
    //            }
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}