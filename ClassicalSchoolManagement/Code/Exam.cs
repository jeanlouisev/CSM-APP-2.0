using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Exam
{

    //getters and setters herdj
    public string id { get; set; }
    public string description { get; set; }
    public DateTime exam_date { get; set; }
    public int class_id { get; set; }
    public int schedule_id { get; set; }
    public string class_name { get; set; }
    public int course_id { get; set; }
    public string cours_name { get; set; }
    public string teacher_id { get; set; }
    public string teacher_name { get; set; }
    public string vacation { get; set; }
    public TimeSpan start_hour { get; set; }
    public TimeSpan end_hour { get; set; }
    public string staff_request { get; set; }
    public DateTime register_date { get; set; }
    public DateTime exam_start_date { get; set; }
    public DateTime exam_end_date { get; set; }
    public double points { get; set; }
    public double student_points { get; set; }
    public int control { get; set; }
    public string control_name { get; set; }
    public string days { get; set; }
    public int academic_year { get; set; }
    public string academic_year_description { get; set; }
    public string fullName { get; set; }
    public TimeSpan Total_Time { get; set; }
    public string student_code { get; set; }
    public string file_path { get; set; }
    public string file_name { get; set; }
    public string years { get; set; }
    public int coefficient { get; set; }



    public static List<Exam> Parse(MySqlDataReader reader)
    {
        List<Exam> listExam = new List<Exam>();
        try
        {
            while (reader.Read())
            {
                Exam exam = new Exam();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { exam.id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        try { exam.description = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURSE_ID")
                    {
                        try { exam.course_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try { exam.class_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SCHEDULE_ID")
                    {
                        try { exam.schedule_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_ID")
                    {
                        try { exam.teacher_id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COEFFICIENT")
                    {
                        try { exam.coefficient = Convert.ToInt32(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "AM": exam.vacation = "Matin"; break;
                                case "PM": exam.vacation = "Median"; break;
                                case "NG": exam.vacation = "Soir"; break;
                                case "WK": exam.vacation = "Weekend"; break;
                                default: exam.vacation = ""; break;
                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "START_HOUR")
                    {
                        try { exam.start_hour = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "END_HOUR")
                    {
                        try { exam.end_hour = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EXAM_DATE")
                    {
                        try { exam.exam_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_REQUEST")
                    {
                        try { exam.staff_request = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE")
                    {
                        try { exam.register_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { exam.class_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { exam.cours_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_NAME")
                    {
                        try { exam.teacher_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "POINTS")
                    {
                        try { exam.points = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_POINTS")
                    {
                        try { exam.student_points = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTROL")
                    {
                        try { exam.control = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTROL_NAME")
                    {
                        try
                        {
                            exam.control_name = reader.GetValue(i).ToString();
                        }

                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "DAYS")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "SU": exam.days = "Dimanche"; break;
                                case "MO": exam.days = "Lundi"; break;
                                case "TU": exam.days = "Mardi"; break;
                                case "WE": exam.days = "Mercredi"; break;
                                case "TH": exam.days = "Jeudi"; break;
                                case "FR": exam.days = "Vendredi"; break;
                                case "SA": exam.days = "Samedi"; break;
                                default: exam.days = ""; break;
                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { exam.academic_year = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_DESCRIPTION")
                    {
                        try { exam.academic_year_description = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { exam.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_CODE")
                    {
                        try { exam.student_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_TIME")
                    {
                        try { exam.Total_Time = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FILE_PATH")
                    {
                        try { exam.file_path = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FILE_NAME")
                    {
                        try { exam.file_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { exam.years = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                }
                listExam.Add(exam);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listExam;
    }

    public static void addExam(Exam exam)
    {
        bool result = false;

        string sql = @"insert into exam values(
                            ?, -- Id,
                            ?, -- Description,
                            ?, -- Exam_Date,
                            ?, -- Start_Hour,
                            ?, -- End_Hour,
                            ?, -- Vacation,
                            ?, -- Id_Class,
                            ?, -- Id_Cours,
                            ?, -- Id_Teacher,
                            ?, -- Staff_Request,
                            now(),
                            ?, -- Points,
                            ?, -- control,
                            ?, -- AcademicYear,
                            ?, -- filePath,
                            ? -- fileName
                            )";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(exam.id,
                                exam.description,
                                exam.exam_date,
                                exam.start_hour,
                                exam.end_hour,
                                exam.vacation,
                                exam.class_id,
                                exam.course_id,
                                exam.teacher_id,
                                exam.staff_request,
                                exam.points,
                                exam.control,
                                exam.academic_year,
                                exam.file_path,
                                exam.file_name);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> getListActiveExam()
    {

        string sql = @"select a.id, a.description, a.exam_date,
                a.start_hour, a.end_hour, a.vacation, a.id_class,
                a.id_cours, a.id_teacher, a.staff_request, a.register_date,
                (select name from classroom where id = a.id_class) as class_name,
                (select name from cours where id = a.id_cours) as cours_name,
                (select concat(first_name,' ',last_name) from teacher 
                    where id = a.id_teacher) as teacher_name
                from exam a order by cours_name asc";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> getListActiveExam(Exam exam)
    {

        string sql = @"SELECT a.id, a.description, a.exam_date,
                                a.start_hour, a.end_hour, 
                                a.vacation, a.class_id,
                                a.course_id, a.teacher_id, 
                                a.staff_request, a.register_date,
                                a.points, a.control, a.file_path, a.file_name,
                                case
                                when a.control = 1 then '1er'
                                when a.control = 2 then '2ieme'
                                when a.control = 3 then '3ieme'
                                when a.control = 4 then '4ieme'
                                end as control_name, a.academic_year,
                                  (select concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date)) 
                                    from academic_year where id = a.academic_year) as academic_year_description,
                                (select name from classroom where id = a.class_id) as class_name,
                                (select name from cours where id = a.course_id) as cours_name,
                                (select concat(first_name,' ',last_name) 
                                        from teacher WHERE id = a.teacher_id) as teacher_name
                                FROM exam a where 1=1";


        if (exam.class_id != 0)
        {
            sql += @" AND a.id_class =" + exam.class_id + " ";
        }
        if (exam.vacation != null && exam.vacation.Length > 0)
        {
            sql += @" AND a.vacation ='" + exam.vacation + "' ";
        }
        if (exam.control != 0)
        {
            sql += @" AND a.control =" + exam.control + " ";
        }
        if (exam.course_id != 0)
        {
            sql += @" AND a.id_cours =" + exam.course_id + " ";
        }
        if (exam.teacher_id != null && exam.teacher_id.Length > 0)
        {
            sql += @" AND a.id_teacher ='" + exam.teacher_id + "' ";
        }
        if (exam.coefficient != 0)
        {
            sql += @" AND a.points ='" + exam.coefficient + "' ";
        }
        //if (exam.exam_date != 0)
        //{
        //    sql += @" AND a.points ='" + exam.coefficient + "' ";
        //}
        //if (exam.start_hour != 0)
        //{
        //    sql += @" AND a.points ='" + exam.coefficient + "' ";
        //}
        //if (exam.end_hour != 0)
        //{
        //    sql += @" AND a.points ='" + exam.coefficient + "' ";
        //}
        if (exam.description != null)
        {
            sql += @" AND a.description like '%" + exam.description + "%' ";
        }
        if (exam.academic_year != 0)
        {
            sql += @" AND a.academic_year = " + exam.academic_year + " ";
        }

        sql += @" ORDER by a.exam_date asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> getListExamById(string examId)
    {
        string sql = @"select a.id, a.description, a.exam_date,
                                a.start_hour, a.end_hour, a.vacation, a.id_class,
                                a.id_cours, a.id_teacher, a.staff_request, a.register_date,
                                a.points, a.control, a.academic_year, a.file_path, a.file_name,
                                (select name from classroom where id = a.id_class) as class_name,
                                (select name from cours where id = a.id_cours) as cours_name,
                                (select concat(first_name,' ',last_name) from teacher WHERE id = a.id_teacher) as teacher_name
                                    FROM exam a 
                                        Where a.id = ?
                                ORDER by cours_name asc";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(examId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> getListExamForStudentByClassroomId(Notes note)
    {
        string sql = @"select b.id as student_code,
                            concat(b.First_name,' ',b.Last_name) as fullname,
                            a.id, a.description, a.exam_date,
                            a.start_hour, a.end_hour, a.vacation, a.id_class,
                            a.id_cours, a.id_teacher, a.staff_request, a.register_date,
                            a.points, a.control,
                            case
                            when a.control = 1 then '1er'
                            when a.control = 2 then '2ieme'
                            when a.control = 3 then '3ieme'
                            when a.control = 4 then '4ieme'
                            end as control_name, a.academic_year,
                            (select name from classroom where id = a.id_class) as class_name,
                            (select name from cours where id = a.id_cours) as cours_name,
                            (select concat(first_name,' ',last_name) from teacher WHERE id = a.id_teacher) as teacher_name,
                            (select student_points from notes where id_exam = a.id and id_student = b.id) as student_points,
							(select concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date)) 
								from academic_year where id = a.academic_year) as years
                            FROM exam a, student b,classroom_staff_management c, classroom d
                            WHERE b.id = c.staff_code 
                                and a.Vacation = c.vacation
                                and a.Id_Class =  c.Id_class
                                and c.Id_class = d.Id";

        if (note.class_id != 0)
        {
            sql += @" AND a.id_class = " + note.class_id + " ";
        }

        if (note.vacation != null)
        {
            sql += @" AND a.vacation = '" + note.vacation + "' ";
        }

        if (note.student_id != null)
        {
            sql += @" AND b.id = '" + note.student_id + "' ";
        }

        if (note.control != 0)
        {
            sql += @" AND a.control = " + note.control + " ";
        }

        if (note.academic_year_id != 0)
        {
            sql += @" AND a.academic_year = " + note.academic_year_id + " ";
        }

        if (note.cours_id != 0)
        {
            sql += @" AND a.id_cours = " + note.cours_id + " ";
        }

        sql += @" group by a.id";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> getListExamByCourseId(int courseId)
    {
        string sql = @"select a.id, a.description, a.exam_date,
                a.start_hour, a.end_hour, a.vacation, a.id_class,
                a.id_cours, a.id_teacher, a.staff_request, a.register_date,
                a.points, a.control, a.academic_year_start, a.academic_year_end,
                (select name from classroom where id = a.id_class) as class_name,
                (select name from cours where id = a.id_cours) as cours_name,
                (select concat(first_name,' ',last_name) from teacher WHERE id = a.id_teacher) as teacher_name
                    FROM exam a 
                        Where a.id_cours = ?
                            ORDER by cours_name asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(courseId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteExamPermanently(string examId)
    {
        string sql = @"delete from exam where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(examId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateExam(Exam exam)
    {
        string sql = @"update exam set description = ?,
                            exam_date = ?,
                            start_hour = ?,
                            end_hour = ?,
                            vacation = ?,
                            id_class = ?,
                            id_cours = ?,
                            id_teacher = ?,
                            staff_request = ?
                        where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(exam.description,
                                exam.exam_date,
                                exam.start_hour,
                                exam.end_hour,
                                exam.vacation,
                                exam.class_id,
                                exam.course_id,
                                exam.teacher_id,
                                exam.staff_request,
                                exam.id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Exam> GetListAlreadyAffectedExam(Exam exam)
    {
        string startHour = string.Format("{0:hh\\:mm\\:ss}", exam.start_hour);
        string endHour = string.Format("{0:hh\\:mm\\:ss}", exam.end_hour);
        

        string sql = @"select * FROM exam
                            Where 1=1";

        if (exam.class_id != 0)
        {
            sql += @" and id_class = " + exam.class_id + " ";
        }

        if (exam.course_id != 0)
        {
            sql += @" and id_cours = " + exam.course_id + " ";
        }

        if (exam.exam_date != null)
        {
            sql += @" and DATE_FORMAT(exam_date,'%d%m%Y') = '" + exam.exam_date.ToString("ddMMyyyy") + "' ";
        }

        if (exam.vacation != null)
        {
            sql += @" and vacation = '" + exam.vacation + "' ";
        }

        if (exam.academic_year != 0)
        {
            sql += @" and academic_year = " + exam.academic_year + " ";
        }

        //if (exam.start_hour != null)
        //{
        //    sql += @" and TIME_FORMAT(End_hour, '%H:%m:%s') > '" + startHour + "' ";
        //}

        //if (exam.end_hour != null)
        //{
        //    sql += @" and TIME_FORMAT(Start_hour, '%H:%m:%s') <  '" + endHour + "' ";
        //}

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}