using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using Utilities;
using System.Data;

public class Student
{
    //getters and setters
    public string id { get; set; }
    public DateTime register_date { get; set; }
    public string student_code { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string sex_code { get; set; }
    public string sex_definition { get; set; }
    public string age { get; set; }
    public string marital_status { get; set; }
    public string marital_status_code { get; set; }
    public string id_card { get; set; }
    public string fullName { get; set; }
    public DateTime birth_date { get; set; }
    public string birth_place { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string vacation { get; set; }
    public string vacation_code { get; set; }
    public string email { get; set; }
    public string staff_code { get; set; }
    public string id_reference { get; set; }
    public int status { get; set; }
    public int admission_status { get; set; }
    public string relationship { get; set; }
    public String fromDate { get; set; }
    public String toDate { get; set; }
    public string image_path { get; set; }
    public string documentPath { get; set; }
    public int class_id { get; set; }
    public string cours_counter { get; set; }
    public DateTime register_date_timesheet { get; set; }
    public int presence_status { get; set; }
    public int validation_status { get; set; }
    public int absence_reason_id { get; set; }
    public string absence_reason_description { get; set; }
    public int reference_status { get; set; }
    public int control { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public int academic_year { get; set; }
    public int academic_year_id { get; set; }
    public int academic_year_start { get; set; }
    public int academic_year_end { get; set; }
    public DateTime date_register { get; set; }
    public string idprivillege { get; set; }
    public string privillege { get; set; }
    public DateTime update_time { get; set; }
    public string reference_code { get; set; }
    public int preload_class_id { get; set; }
    public string preload_staff_code { get; set; }
    public int preload_academic_year { get; set; }
    public int id_Special_payment { get; set; }
    public string years { get; set; }
    public Double total_exam_point { get; set; }
    public Double total_student_point { get; set; }
    public Double student_average_percent { get; set; }
    public Double success_percent { get; set; }
    public Double student_points { get; set; }
    public Double exam_points { get; set; }
    public string final_result { get; set; }
    public int final_result_status { get; set; }
    public int role_id { get; set; }
    public string class_name { get; set; }
    public int login_user_id { get; set; }
    public DateTime sheet_date { get; set; }
    //







    public enum StudenStatus
    {
        Active = 1,
        NotActive = 0
    }

    public enum AdmissionStatus
    {
        Repetition = -1,
        Pass = 1,
        Failed = 0
    }


    public static List<Student> Parse(MySqlDataReader reader)
    {
        List<Student> listStudent = new List<Student>();
        try
        {
            while (reader.Read())
            {
                Student student = new Student();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { student.id = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_SPECIAL_PAYMENT")
                    {
                        try { student.id_Special_payment = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE")
                    {
                        try { student.register_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { student.first_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_CODE")
                    {
                        try { student.student_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { student.last_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEXE")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "M": student.sex = "Masculin"; break;
                                case "F": student.sex = "Feminin"; break;
                                default: student.sex = ""; break;
                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEXE")
                    {
                        try { student.sex_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEXE")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "M": student.sex_definition = "Masculin"; break;
                                case "F": student.sex_definition = "Feminin"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AGE")
                    {
                        try { student.age = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "C": student.marital_status = "Célibataire"; break;
                                case "M": student.marital_status = "Marié(e)"; break;
                                case "D": student.marital_status = "Divorcé(e)"; break;
                                case "V": student.marital_status = "Veuf(ve)"; break;
                                case "U": student.marital_status = "Union Libre"; break;

                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try { student.marital_status_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { student.id_card = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { student.fullName = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_DATE")
                    {
                        try { student.birth_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_PLACE")
                    {
                        try { student.birth_place = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { student.address = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE1")
                    {
                        try { student.phone = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "AM": student.vacation = "Matin"; break;
                                case "PM": student.vacation = "Median"; break;
                                case "NG": student.vacation = "Soir"; break;
                                case "WK": student.vacation = "Weekend"; break;
                                default: student.vacation = ""; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { student.vacation_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EMAIL")
                    {
                        try { student.email = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { student.staff_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_REFERENCE")
                    {
                        try { student.id_reference = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { student.status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADMISSION_STATUS")
                    {
                        try { student.admission_status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "RELATIONSHIP")
                    {
                        try { student.relationship = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { student.fromDate = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { student.toDate = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { student.image_path = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DOCUMENT_PATH")
                    {
                        try { student.documentPath = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try { student.class_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS")
                    {
                        try
                        {
                            if (int.Parse(reader.GetValue(i).ToString()) > 0)
                            {
                                student.presence_status = int.Parse(reader.GetValue(i).ToString());
                            }
                            else
                            {
                                student.presence_status = 0;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VALIDATION_STATUS")
                    {
                        try
                        {
                            if (int.Parse(reader.GetValue(i).ToString()) > 0)
                            {
                                student.validation_status = int.Parse(reader.GetValue(i).ToString());
                            }
                            else
                            {
                                student.validation_status = 0;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON")
                    {
                        try
                        {
                            student.absence_reason_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON_DESCRIPTION")
                    {
                        try
                        {
                            student.absence_reason_description = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE_TIMESHEET")
                    {
                        try { student.register_date_timesheet = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_STATUS")
                    {
                        try { student.reference_status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "CONTROL")
                    {
                        try { student.control = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "START_DATE")
                    {
                        try { student.start_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "END_DATE")
                    {
                        try { student.end_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { student.academic_year = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_START")
                    {
                        try { student.academic_year_start = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { student.academic_year_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_END")
                    {
                        try { student.academic_year_end = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { student.date_register = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IDPRIVILEGE")
                    {
                        try { student.idprivillege = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRIVILLEGE")
                    {
                        try { student.privillege = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "UPDATE_TIME")
                    {
                        try { student.update_time = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_CODE")
                    {
                        try { student.reference_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { student.years = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_EXAM_POINT")
                    {
                        try { student.total_exam_point = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_STUDENT_POINT")
                    {
                        try { student.total_student_point = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_AVERAGE_PERCENT")
                    {
                        try { student.student_average_percent = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SUCCESS_PERCENT")
                    {
                        try { student.success_percent = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_POINTS")
                    {
                        try { student.student_points = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EXAM_POINTS")
                    {
                        try { student.exam_points = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FINAL_RESULT")
                    {
                        try { student.final_result = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FINAL_RESULT_STATUS")
                    {
                        try { student.final_result_status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ROLE_ID")
                    {
                        try { student.role_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { student.class_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { student.login_user_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SHEET_DATE")
                    {
                        try { student.sheet_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                }
                listStudent.Add(student);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
        return listStudent;
    }

    // @vacation,
    public static void addStudent(Student st)
    {

        string sql = @"INSERT INTO STUDENT
                                VALUES(?, -- studentCode,
                                        ?, -- firstName,
                                        ?, -- lastName,
                                        ?, -- sex,
                                        ?, -- maritalStatus,
                                        ?, -- id_card,
                                        ?, -- birth_date,
                                        ?, -- birth_place,
                                        ?, -- adress,
                                        ?, -- phone,                                       
                                        ?, -- email,
                                        ?, -- image_path,
                                        ?, -- status,
                                        now(), -- date_register,
                                        ? -- login_user_id
                                        )";


        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(st.id, st.first_name, st.last_name, st.sex, st.marital_status,
                            st.id_card, st.birth_date.ToString("yyyyMMdd"), st.birth_place, st.address,
                            st.phone, st.email, st.image_path, st.status,
                            st.login_user_id
                            );

        stmt.ExecuteNonQuery();
    }

    public static void updateStudent(Student st)
    {
        try
        {
            string sql = @"UPDATE STUDENT
                               set first_name = ?,
                                last_name = ?,
                                sexe = ?,
                                marital_status = ?,
                                id_card = ?,
                                birth_date = ?,
                                birth_place = ?,
                                adress = ?,
                                phone1 = ?,
                                email = ?,
                                image_path = ?,
                                ref_first_name = ?,
                                ref_last_name = ?,
                                ref_sex = ?,
                                ref_phone = ?,
                                ref_adress = ?,
                                ref_relationship = ?
                            WHERE id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(st.first_name,
                                st.last_name,
                                st.sex,
                                st.marital_status,
                                st.id_card,
                                st.birth_date.ToString("yyyyMMdd"),
                                st.birth_place,
                                st.address,
                                st.phone,
                                st.email,
                                st.id);

            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    //-------list--real search-----
    public static List<Student> getListStudent(Student st)
    {

        string sql = @"SELECT  a.*,
                                    concat(a.first_name,' ',a.last_name) as fullname,
                                    c.name as class_name,
                                    c.id as class_id,
									b.vacation,
                                    (select concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date))
								             from academic_year where id = b.academic_year_id) as years,
                                    b.academic_year_id as academic_year_id
                                FROM STUDENT a
								inner join classroom_staff_management b on b.staff_code = a.Id
								inner join classroom c on c.id = b.class_id
                                WHERE a.Status = 1
                                    [ and a.id = ? ]    -- 0
                                    [ and concat(lower(a.first_name),' ',lower(a.last_name)) like ? ]   -- 1
                                    [ and b.vacation = ? ]  -- 2
                                    [ and b.class_id = ? ] -- 3
                                    [ and a.Sexe = ? ]  -- 4
                                    [ and b.academic_year_id = ? ]   -- 5
                                 ORDER BY c.name";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);

        if (st.id != null)
        {
            stmt.SetParameter(0, st.id.ToUpper());
        }
        if (st.fullName != null)
        {
            stmt.SetParameter(1, st.fullName.ToLower());
        }
        if (st.vacation != null)
        {
            stmt.SetParameter(2, st.vacation);
        }
        if (st.class_id > 0)
        {
            stmt.SetParameter(3, st.class_id);
        }
        if (st.sex != null)
        {
            stmt.SetParameter(4, st.sex);
        }
        if (st.academic_year != 0)
        {
            stmt.SetParameter(5, st.academic_year);
        }

        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudent_privilege(Student st)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                   upper( concat(a.Last_name,' ',a.First_name)) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    b.vacation,
                                    b.class_id as class_id,
                                    a.email,
                                    a.image_path, a.status,
                                    c.name as class_name,
                                   (select d.id_Special_payment from Payment_Special_Students_management d 
                                             where d.id_student = a.id and d.academic_year=@academic_year) as id_Special_payment
                                FROM STUDENT a, CLASSROOM_STAFF_MANAGEMENT b, CLASSROOM c
                                WHERE 1=1
                                    AND a.id = b.staff_code
                                    AND b.class_id = c.id
                                    AND  b.academic_year_id= ?";

        if (st.id != null)
        {
            sql += @" AND a.id = '" + st.id.ToUpper() + "' ";
        }

        if (st.fullName != null)
        {
            sql += @" and concat(lower(a.first_name),' ',lower(a.last_name)) like '" + st.fullName.ToLower() + "' ";
        }

        if (st.vacation != null)
        {
            sql += @" AND b.vacation = '" + st.vacation + "' ";
        }

        if (st.marital_status != null)
        {
            sql += @" AND a.Marital_status = '" + st.marital_status + "' ";
        }

        if (st.class_id > 0)
        {
            sql += @" AND b.class_id = " + st.class_id + " ";
        }

        if (st.sex != null)
        {
            sql += @" AND a.Sexe = '" + st.sex + "' ";
        }
        if (st.status != -1)
        {
            sql += @" AND a.status = " + st.status + " ";
        }

        sql += @" order by c.name";



        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(st.academic_year);
        return Parse(stmt.ExecuteReader());

    }

    public static List<Student> getListStudentForChangingClass(
        int classId, string vacation, int accYearId)
    {
        List<Student> listResult = null;


        string sql = @"SELECT  d.academic_year_id, a.id, c.id as class_id, c.name as class_name, b.vacation,
                            concat(a.Last_name,' ',a.First_name) as fullname,
                            (select sum(coefficient) from notes where student_id = a.id) as total_exam_point,
                            (select sum(note_obtained) from notes where student_id = a.id) as total_student_point,
                                round(((select sum(note_obtained) from notes where student_id = a.id) / 
                                                    (select sum(coefficient) from notes where student_id = a.id)) * 100, 2) as student_average_percent,
                                   f.success_percent,
                                case when round(((select sum(note_obtained) from notes where student_id = a.id) / 
                                 (select sum(coefficient) from notes where student_id = a.id)) * 100, 2) <  f.success_percent then 0 else 1 end as final_result_status
                            FROM STUDENT a
                            inner join classroom_staff_management b on b.staff_code = a.Id
                            inner join classroom c on c.id = b.class_id
                            inner join notes d on d.student_id = a.id
                            inner join classroom_average_management f on f.class_id = c.id
                            WHERE 1=1
                            and b.status = 1
                            and a.status = 1
                            b.class_id = ?
                            b.vacation = ?
                            b.academic_year_id = ?
                            group by  a.id, c.name, b.vacation, a.Last_name,a.First_name
                              order by c.name asc";

        //if (st.class_id != 0)
        //{
        //    sql += @" AND b.class_id = " + st.class_id + " ";
        //}

        //if (st.vacation != null)
        //{
        //    sql += @" AND b.vacation = '" + st.vacation + "' ";
        //}

        //if (st.academic_year != 0)
        //{
        //    sql += @" AND f.academic_year_id = " + st.academic_year + " ";
        //    sql += @" AND d.academic_year = " + st.academic_year + " ";
        //    sql += @" AND b.academic_year = " + st.academic_year + " ";
        //}

        //sql += @" group by  a.id, c.name, b.vacation,
        //                    a.Last_name,a.First_name
        //                      order by c.name asc";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(classId, vacation, accYearId);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudentForBulletin(Student st)
    {
        string sql = @"SELECT  st.*, upper(concat(st.Last_name,' ',st.First_name)) as fullname, csm.vacation,
                                cr.name as class_name,
								concat(extract(year from acc.start_date),'-', extract(year from acc.end_date)) years,
								n.control, csm.class_id, csm.academic_year_id
                            FROM STUDENT st
								inner join classroom_staff_management csm on csm.staff_code = st.id
								inner join classroom cr on cr.id = csm.class_id
								inner join academic_year acc on acc.id = csm.academic_year_id
								inner join notes n on n.class_id = cr.id and n.academic_year_id = acc.id and n.vacation = csm.vacation
                            where st.status = 1
                                [ and st.id = ? ]  --  0
                                [ and upper(concat(st.Last_name,' ',st.First_name)) like ? ]  --  1
                                [ and csm.vacation = ? ]  --  2
                                [ and csm.class_id = ? ]  --  3
                                [ and csm.academic_year_id = ? ]  --  4
                                [ and n.control = ? ]  -- 5
							GROUP BY st.id, st.last_name, st.first_name";



        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);

        if (st.id != null)
        {
            stmt.SetParameter(0, st.id);
        }
        if (st.fullName != null)
        {
            stmt.SetParameter(1, st.fullName);
        }

        stmt.SetParameter(2, st.vacation);
        if (st.class_id > 0)
        {
            stmt.SetParameter(3, st.class_id);
        }
        stmt.SetParameter(4, st.academic_year_id);
        stmt.SetParameter(5, st.control);

        return Parse(stmt.ExecuteReader());

    }

    public static List<Student> getListStudentsForPalmares(int classId, string vacation, int accYearId)
    {
        string sql = @"select st.id, concat(st.first_name,' ',st.last_name) as fullname
                        from student st
                        inner join classroom_staff_management csm on csm.staff_code = st.id
                        where csm.`status` = 1
                        and csm.class_id = ?
                        and csm.vacation = ?
                        and csm.academic_year_id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation, accYearId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /*        public static List<Student> getListStudentById(string id)
    {
        List<Student> listResult = null;
        
        string sql = @"SELECT  a.id,
                                a.Register_date,
                                a.first_name,
                                a.last_name,
                                concat(a.Last_name,' ',a.First_name) as fullname,
                                a.Sexe,
                                a.Marital_status,
                                a.Id_card,
                                a.BIRTH_DATE,
                                a.BIRTH_PLACE,
                                a.Adress,
                                a.Phone1,
                                f.vacation,
                                a.email,
                                a.image_path,
                                a.status,
                                (select b.id from classroom b, classroom_staff_management c 
                                   where b.id = c.class_id and c.staff_code = @_studentCode)
                                as class_name,
                                 a.reference_status,
                                d.id AS idprivillege,
                                e.description AS privillege                                   
                            FROM STUDENT a,payment_special_students_management d,payment_special_type e ,classroom_staff_management f
                            WHERE 1=1 
                                AND a.id like  @_studentCode                                    
                                AND a.Status =1
                                AND a.id=d.id_Student
                                AND a.id=f.staff_code
                                AND d.id_Special_payment=e.id
                         ";
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        con.Open();
        try
        {
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@_studentCode", id);

            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                listResult = parse(reader);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        finally
        {
            // check connection state
            if (con != null)
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch (Exception ex)
                {
                    MessBox.Show("Error : " + ex.Message);
                }
            }
        }
        return listResult;
    }
    */


    public static List<Student> getListStudentByCode(string studentCode)
    {
        string sql = @"SELECT  a.*,
                                    concat(a.first_name,' ',a.last_name) as fullname,
                                    c.name as class_name,
                                    c.id as class_id,
									b.vacation,
                                    (select concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date))
								             from academic_year where id = b.academic_year_id) as years,
                                    b.academic_year_id as academic_year_id
                                FROM STUDENT a
								inner join classroom_staff_management b on b.staff_code = a.Id
								inner join classroom c on c.id = b.class_id
                                WHERE a.Status = 1 
                            AND a.id = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        return Parse(stmt.ExecuteReader());
    }

    public static Student getStudentInfoById(string id)
    {
        string sql = @"SELECT a.*, concat(upper(a.first_name),' ',upper(a.last_name)) as fullname       
                            FROM student a
                            WHERE a.id = ?
                            AND a.status = 1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(id);
        return Parse(stmt.ExecuteReader())[0];
    }

    public static Student getStudentFullDetailsByCode(string studentCode)
    {
        string sql = @"SELECT  a.*,
                                    concat(a.first_name,' ',a.last_name) as fullname,
                                    c.name as class_name,
                                    c.id as class_id,
									b.vacation,
                                    (select concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date))
								             from academic_year where id = b.academic_year_id) as years,
                                    b.academic_year_id as academic_year_id
                                FROM STUDENT a
								inner join classroom_staff_management b on b.staff_code = a.Id
								inner join classroom c on c.id = b.class_id
                                WHERE a.Status = 1
                                and a.id = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        return Parse(stmt.ExecuteReader())[0];
    }

    public static List<Student> getListStudentByIdandAccademic(string id, int academic_year)
    {
        // TO BE REVIEWED

        string sql = @"SELECT  a.id,
                            a.Register_date,
                            a.first_name,
                            a.last_name,
                            concat(a.Last_name,' ',a.First_name) as fullname,
                            a.Sexe,
                            a.Marital_status,
                            a.Id_card,
                            a.BIRTH_DATE,
                            a.BIRTH_PLACE,
                            a.Adress,
                            a.Phone1,
                            b.vacation,
                            a.email,
                            a.image_path,
                            a.status,
                            (select b.id from classroom b, classroom_staff_management c 
                            where b.id = c.class_id and c.staff_code =@id  and c.academic_year=@academic_year)
                            as class_name,
                           (select e.id_Special_payment
                                          from  payment_special_students_management e
                            where e.id_Student = @id and e.academic_year=@academic_year)
                            as privillege,                       
                            b.academic_year_id,
                            a.reference_status           
                            FROM STUDENT a,classroom_staff_management b
                            WHERE 1=1 
                            AND a.id like  @id                                  
                            AND a.id=b.staff_code and b.academic_year_id = @academic_year";


        return null;

        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        con.Open();
        try
        {
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@academic_year", academic_year);
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                listResult = parse(reader);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        finally
        {
            // check connection state
            if (con != null)
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch (Exception ex)
                {
                    MessBox.Show("Error : " + ex.Message);
                }
            }
        }
        return listResult;
        */
    }

    public static List<Student> getListStudentByIdWithException(string studentCode)
    {

        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    b.vacation,
                                    a.email,
                                    a.image_path,
                                    a.status,
                                    (select b.id from classroom b, classroom_staff_management c 
                                       where b.id = c.class_id and c.staff_code = a.id )
                                    as class_name,
                                     a.reference_status
                                                                
                                FROM STUDENT a,classroom_staff_management b
                                WHERE 1=1 
                                    AND a.id NOT IN (?)                                  
                                    AND a.id=b.staff_code";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudentNOprivillegeById(string id)
    {
        List<Student> listResult = null;

        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,                                   
                                    a.email,
                                    a.image_path,
                                    a.status,
                                    (select b.id from classroom b, classroom_staff_management c 
                                       where b.id = c.class_id and c.staff_code = a.id  )
                                    as class_name,
                                    (c.academic_year from classroom b, classroom_staff_management c 
                                       where b.id = c.class_id and c.staff_code = a.id  )
                                    as class_name,
                                     a.reference_status                                 
                                                                     
                                FROM STUDENT a
                                WHERE 1=1
                                    AND a.id = ?
                                    AND a.Status =1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(id);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> checkExistedStudentPrivillege(string staffCode)
    {
        string sql = @"select * from payment_special_students_management where id_Student= ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(staffCode);
        return Parse(stmt.ExecuteReader());
    }


    public static List<Student> getListAllStudent()
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    a.vacation,
                                    a.email,
                                    a.image_path
                             FROM STUDENT a";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListBirthday()
    {
        string sql = @"SELECT  a.id,
                                    lower(concat(a.Last_name,' ',a.First_name)) as fullname,                                   
                                    a.Phone1,                                  
                                    a.email
                                    FROM STUDENT a
                        WHERE DATE_FORMAT(SYSDATE(), '%m-%d') = DATE_FORMAT(BIRTH_DATE,'%m-%d')

                        UNION all
                        SELECT  a.id,
                                lower(concat(a.Last_name,' ',a.First_name)) as fullname,                                   
                                a.Phone1,                                  
                                a.email
                                FROM staff a
                        WHERE DATE_FORMAT(SYSDATE(), '%m-%d') = DATE_FORMAT(BIRTH_DATE,'%m-%d')

                        UNION all
                        SELECT  a.id,
                                lower(concat(a.Last_name,' ',a.First_name)) as fullname,                                   
                                a.Phone1,                                  
                                a.email
                                FROM teacher a
                        WHERE DATE_FORMAT(SYSDATE(), '%m-%d') = DATE_FORMAT(BIRTH_DATE,'%m-%d')";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListAllActiveStudent()
    {
        string sql = @"SELECT  a.*,concat(a.Last_name,' ',a.First_name) as fullname
                        FROM STUDENT a
                        WHERE a.status =1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudentByReference(Student st)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    a.email,
                                    a.image_path,
                                    c.name as class_name
                                FROM STUDENT a, CLASSROOM_STAFF_MANAGEMENT b, CLASSROOM c
                                WHERE 1=1
                                    AND a.id = b.staff_code
                                    AND b.class_id = c.id
                                    AND a.Status =1
                                    AND a.id = ?
                                    AND upper(concat(a.Last_name,' ',a.First_name)) like ?
                                    AND b.class_id = ?
                                    AND a.Sexe = ?
                                    AND a.status = 1
                                    AND b.status = 1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(st.id, st.fullName.ToUpper(), st.class_id, st.sex);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudentByReferencewithException(Student st, string studentCode)
    {
        List<Student> listResult = null;

        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    a.email,
                                    a.image_path,
                                    c.name as class_name
                                FROM STUDENT a, CLASSROOM_STAFF_MANAGEMENT b, CLASSROOM c
                                WHERE 1=1
                                    AND a.id = b.staff_code
                                    AND b.class_id = c.id
                                    AND a.Status =1
                                    AND a.id = ?
                                    AND upper(concat(a.Last_name,' ',a.First_name)) like ?
                                    AND b.class_id = ?
                                    AND a.Sexe = ?
                                    AND a.id not in (?)
                                    AND b.status = 1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(st.id, st.fullName.ToUpper(), st.class_id, st.sex, studentCode);
        return Parse(stmt.ExecuteReader());
    }

    public static List<Student> getListStudentForTimesheets(Student st)
    {
        try
        {
            string sql = @"SELECT  a.id, concat(upper(a.first_name),' ', upper(a.last_name)) as fullname, 
                                    c.name as class_name, c.id as class_id , b.vacation
                            FROM STUDENT a
                            left join classroom_staff_management b on b.staff_code = a.Id
                            left join classroom c on c.id = b.class_id
                            WHERE b.status = 1";

            //if (st.id != null)
            //{
            //    sql += @" AND a.id = '" + st.id.ToUpper() + "' ";
            //}
            //if (st.fullName != null)
            //{
            //    sql += @" AND concat(upper(a.first_name),' ', upper(a.last_name)) like '%" + st.fullName.ToUpper() + "'% ";
            //}
            //if (st.classroom != null)
            //{
            //    sql += @" AND b.class_id  = " + int.Parse(st.classroom) + " ";
            //}
            //if (st.vacation_id != null)
            //{
            //    sql += @" AND b.vacation = '" + st.vacation_id + "' ";
            //}

            sql += @" order by  c.id, concat(upper(a.first_name),' ', upper(a.last_name))";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Student> getListStudentForTimesheets_bk(Student st)
    {
        return null;
        // to be reviewed

        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    b.vacation,
                                    a.email,
                                    a.image_path,
                                    c.name as class_name,
									c.id as class_id_id,
									(SELECT PRESENCE_STATUS FROM TIMESHEETS 
									      WHERE STAFF_CODE = a.id
										    AND class_id = c.id
											 AND VACATION = b.vacation
											  AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') = @register_date_timesheet) AS PRESENCE_STATUS,
									(SELECT VALIDATION_STATUS FROM TIMESHEETS 
									      WHERE STAFF_CODE = a.id
										    AND class_id = c.id
											 AND VACATION = b.vacation
											  AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') = @register_date_timesheet) AS VALIDATION_STATUS,
									(SELECT CASE WHEN trim(absence_reason) = '' THEN ''
                                            ELSE absence_reason end as absence_reason FROM TIMESHEETS 
									      WHERE STAFF_CODE = a.id
										    AND class_id = c.id
											 AND VACATION = b.vacation
											  AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') = @register_date_timesheet) AS absence_reason
                                FROM STUDENT a, CLASSROOM_STAFF_MANAGEMENT b, CLASSROOM c
                                WHERE a.id = b.staff_code
                                    AND b.class_id = c.id
                                    AND a.id like @_studentCode
                                    AND lower(a.first_name) like @_firstName
                                    AND lower(a.last_name) like @_lastName 
                                    AND b.vacation like @_vacation
                                    AND b.class_id like @_classroom
                                    AND a.Status = 1
                                    AND b.status = 1";



        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;

        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@_studentCode", st.id);
            cmd.Parameters.AddWithValue("@_firstName", st.first_name.ToLower());
            cmd.Parameters.AddWithValue("@_lastName", st.last_name.ToLower());
            cmd.Parameters.AddWithValue("@_vacation", st.vacation_id);
            cmd.Parameters.AddWithValue("@_classroom", st.class_id);
            cmd.Parameters.AddWithValue("@register_date_timesheet", st.register_date_timesheet.ToString("yyyyMMdd"));

            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                listStudent = parse(reader);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        finally
        {
            // check connection state
            if (con != null)
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch (Exception ex)
                {
                    MessBox.Show("Error : " + ex.Message);
                }
            }
        }
        return listStudent;
        */
    }

    public static List<Student> SearchStudentTimesheets(Student st)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                  --  a.vacation,
                                    a.email,
                                    a.image_path,
                                    c.name as class_name,
									c.id as class_id,d.presence_status
									,d.validation_status,d.date_register as register_date_string,
									d.absence_reason, case when e.id = 1 then '' else e.description end as absence_reason_description
                                FROM STUDENT a, CLASSROOM_STAFF_MANAGEMENT b, CLASSROOM c, timesheets d, reason_type e
                                WHERE a.id = b.staff_code
                                    AND b.class_id = c.id
                                    AND a.id = d.staff_code
                                    AND d.absence_reason = e.id
                                    AND a.id like @_studentCode
                                    AND lower(a.first_name) like @_firstName
                                    AND lower(a.last_name) like @_lastName 
                                  --  AND a.vacation like @_vacation
                                    AND b.class_id like @_classroom
                                    AND d.presence_status = @presence_status
                                    AND a.Status =1
                                  ";

        if (st.fromDate != null)
        {
            sql += @" AND DATE_FORMAT(d.DATE_REGISTER ,'%Y%m%d') >= '" + st.fromDate + "' ";
        }
        if (st.toDate != null)
        {
            sql += @" AND DATE_FORMAT(d.DATE_REGISTER ,'%Y%m%d') <= '" + st.toDate + "' ";
        }



        return null;


        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;

        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@_studentCode", st.id);
            cmd.Parameters.AddWithValue("@_firstName", st.first_name.ToLower());
            cmd.Parameters.AddWithValue("@_lastName", st.last_name.ToLower());
            cmd.Parameters.AddWithValue("@_vacation", st.vacation_id);
            cmd.Parameters.AddWithValue("@_classroom", st.class_id);
            cmd.Parameters.AddWithValue("@register_date_timesheet", st.register_date_timesheet.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@presence_status", st.presence_status);

            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                listStudent = parse(reader);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        finally
        {
            // check connection state
            if (con != null)
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch (Exception ex)
                {
                    MessBox.Show("Error : " + ex.Message);
                }
            }
        }
        return listStudent;
        */

    }

    public static void deleteStudentPermanently(string studentCode)
    {
        string sql = @"delete from student where id = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        stmt.ExecuteNonQuery();
    }

    public static void disableStudent(string studentCode)
    {
        string sql = @"update student set status = 0 where id = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        stmt.ExecuteNonQuery();
    }

    public static void enableStudent(string studentCode)
    {
        string sql = @"update student set status = 1 where id = @studentCode";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(studentCode);
        stmt.ExecuteNonQuery();
    }

    public static bool idCardExist(string idCard)
    {
        bool result = false;

        string sql = @"SELECT COUNT(*) FROM student a  WHERE a.id_card = ? ";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(idCard);
        IDataReader reader = stmt.ExecuteReader();
        if (reader != null)
        {
            while (reader.Read())
            {
                if (reader.GetInt32(0) > 0)
                {
                    result = true;
                }
            }
        }
        return result;
    }

    public static List<Student> GetListStudentByFullName(string fullName)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    a.first_name,
									a.last_name,
                                    concat(a.First_name,' ',a.Last_name) as fullname,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.BIRTH_DATE,
                                    a.BIRTH_PLACE,
                                    a.Adress,
                                    a.Phone1,
                                    a.email,
                                    a.image_path
                             FROM STUDENT a
                                   where concat(a.Last_name,' ',a.First_name) like ?
                             ";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(fullName);
        return Parse(stmt.ExecuteReader());
    }


}