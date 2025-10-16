using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;

public class Teacher
{
    //getters and setters

    public string id { get; set; }
    public DateTime register_date { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string sex_definition { get; set; }
    public string marital_status { get; set; }
    public string marital_status_definition { get; set; }
    public string id_card { get; set; }
    public DateTime birth_date { get; set; }
    public string birth_place { get; set; }
    public string address { get; set; }
    public string phone1 { get; set; }
    public string position { get; set; }
    public string email { get; set; }
    public string imagePath { get; set; }
    public string level { get; set; }
    public string fullName { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }
    public string classroom { get; set; }
    public string vacation { get; set; }
    public int status { get; set; }
    public int cours_counter { get; set; }
    public DateTime register_date_timesheet { get; set; }
    public int presence_status { get; set; }
    public int validation_status { get; set; }
    public int absence_reason { get; set; }
    public int reference_status { get; set; }
    public string relationship { get; set; }
    public string date_register { get; set; }
    public string absence_reason_description { get; set; }
    public string register_date_string { get; set; }
    public int academic_year { get; set; }
    public string group_name { get; set; }
    public string classroom_name { get; set; }
    public int classroom_id { get; set; }
    public DateTime sheet_date { get; set; }
    public int group_name_id { get; set; }
    public string cours_name { get; set; }
    public string adress { get; set; }
    public string image_path { get; set; }
    public string study_level { get; set; }
    public int login_user_id { get; set; }
    public int tax_id { get; set; }
    //
    public string ref_first_name { get; set; }
    public string ref_last_name { get; set; }
    public string ref_sex { get; set; }
    public string ref_phone { get; set; }
    public string ref_adress { get; set; }
    public string ref_relationship { get; set; }
    //


    public enum ACCOUNT_STATUS
    {
        Active = 1,
        NotActive = 0
    }

    public static List<Teacher> Parse(MySqlDataReader reader)
    {
        List<Teacher> listTeacher = new List<Teacher>();
        try
        {
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { teacher.id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE")
                    {
                        try { teacher.register_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { teacher.first_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { teacher.last_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { teacher.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULL_NAME")
                    {
                        try { teacher.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEXE")
                    {
                        try { teacher.sex = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEXE")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "M": teacher.sex_definition = "Masculin"; break;
                                case "F": teacher.sex_definition = "Feminin"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try { teacher.marital_status = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "C": teacher.marital_status_definition = "Célibataire"; break;
                                case "M": teacher.marital_status_definition = "Marié(e)"; break;
                                case "D": teacher.marital_status_definition = "Divorcé(e)"; break;
                                case "V": teacher.marital_status_definition = "Veuf(ve)"; break;
                                case "UL": teacher.marital_status_definition = "Union libre"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { teacher.id_card = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_DATE")
                    {
                        try { teacher.birth_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_PLACE")
                    {
                        try { teacher.birth_place = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { teacher.address = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE1")
                    {
                        try { teacher.phone1 = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "POSITION")
                    {
                        try { teacher.position = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EMAIL")
                    {
                        try { teacher.email = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { teacher.imagePath = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LEVEL")
                    {
                        try { teacher.level = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { teacher.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { teacher.fromDate = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { teacher.toDate = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM")
                    {
                        try { teacher.classroom = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { teacher.vacation = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { teacher.status = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_COUNTER")
                    {
                        try
                        {
                            teacher.cours_counter = reader.GetInt32(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS")
                    {
                        try
                        {
                            if (reader.GetInt32(i) > 0)
                            {
                                teacher.presence_status = reader.GetInt32(i);
                            }
                            else
                            {
                                teacher.presence_status = 0;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VALIDATION_STATUS")
                    {
                        try
                        {
                            if (reader.GetInt32(i) > 0)
                            {
                                teacher.validation_status = reader.GetInt32(i);
                            }
                            else
                            {
                                teacher.validation_status = 0;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "absence_reason")
                    {
                        try
                        {
                            teacher.absence_reason = reader.GetInt32(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE_TIMESHEET")
                    {
                        try { teacher.register_date_timesheet = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_STATUS")
                    {
                        try { teacher.reference_status = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "RELATIONSHIP")
                    {
                        try { teacher.relationship = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { teacher.date_register = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON_DESCRIPTION")
                    {
                        try { teacher.absence_reason_description = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { teacher.academic_year = int.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "GROUP_NAME")
                    {
                        try { teacher.group_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_NAME")
                    {
                        try { teacher.classroom_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_ID")
                    {
                        try { teacher.classroom_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SHEET_DATE")
                    {
                        try { teacher.sheet_date = DateTime.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "GROUP_NAME_ID")
                    {
                        try { teacher.group_name_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { teacher.cours_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { teacher.adress = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { teacher.image_path = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDY_LEVEL")
                    {
                        try { teacher.study_level = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { teacher.login_user_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TAX_ID")
                    {
                        try { teacher.tax_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_FIRST_NAME")
                    {
                        try { teacher.ref_first_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_LAST_NAME")
                    {
                        try { teacher.ref_last_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_SEX")
                    {
                        try { teacher.ref_sex = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_PHONE")
                    {
                        try { teacher.ref_phone = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_ADRESS")
                    {
                        try { teacher.ref_adress = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REF_RELATIONSHIP")
                    {
                        try { teacher.ref_relationship = reader.GetValue(i).ToString(); } catch { }
                    }
                }
                //
                listTeacher.Add(teacher);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listTeacher;
    }

    public static void addTeacher(Teacher t)
    {
        try
        {
            string sql = @"insert into teacher values(                         
                            ?, -- Id,
                            ?, -- firstName,
                            ?, -- lastName,
                            ?, -- sex,
                            ?, -- marital_Status,
                            ?, -- cardId,
                            ?, -- birth_date,
                            ?, -- birth_place,
                            ?, -- adress,
                            ?, -- phone1,
                            ?, -- email, 
                            ?, -- imagePath,
                            ?, -- study_level,
                            ?, -- status,
                            ?, -- ref_first_name
                            ?, -- ref_last_name
                            ?, -- ref_sex
                            ?, -- ref_phone
                            ?, -- ref_adress
                            ?, -- ref_relationship
                            now(),
                            ? -- login_user_id
                           )";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.id, t.first_name, t.last_name, t.sex, t.marital_status,
                                t.id_card, t.birth_date.ToString("yyyyMMdd"), t.birth_place, t.adress, t.phone1, t.email,
                                t.image_path, t.study_level, t.status,
                                t.ref_first_name,
                                t.ref_last_name,
                                t.ref_sex,
                                t.ref_phone,
                                t.ref_adress,
                                t.ref_relationship,
                                t.login_user_id);

            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateTeacher(Teacher t)
    {
        try
        {
            string sql = @"UPDATE TEACHER SET
                                        first_name = ?,
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
                                        study_level = ?,
                                        ref_first_name = ?,
                                        ref_last_name = ?,
                                        ref_sex = ?,
                                        ref_phone = ?,
                                        ref_adress = ?,
                                        ref_relationship = ?
                                        where id = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.first_name, t.last_name, t.sex,
                                t.marital_status, t.id_card,
                                t.birth_date.ToString("yyyyMMdd"), t.birth_place, t.adress,
                                t.phone1, t.email, t.image_path, t.study_level,
                                t.ref_first_name,
                                t.ref_last_name,
                                t.ref_sex,
                                t.ref_phone,
                                t.ref_adress,
                                t.ref_relationship, t.id);

            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Teacher> getListTeacher(Teacher t)
    {
        string sql = @"SELECT  a.*,
                                    concat(a.first_name,' ',a.last_name) as fullname,
									case
										when a.id in(select b.teacher_id from teacher_cours_attach b where b.teacher_id = a.id)
										then (select count(b.cours_id)
												from teacher_cours_attach b where b.teacher_id = a.id)
										end as cours_counter
                             FROM TEACHER a
                             WHERE 1=1 and a.status = 1
                             ";


        if (t.id != null)
        {
            sql += @" and a.id = '" + t.id.ToUpper() + "'";
        }
        if (t.fullName != null)
        {
            sql += @" and concat(lower(a.first_name),' ',lower(a.last_name)) like '%" +
                        t.fullName.ToLower() + "%'";
        }
        if (t.phone1 != null)
        {
            sql += @" and a.phone1 = '" + t.phone1 + "'";
        }
        if (t.sex != null)
        {
            sql += @" and a.sexe = '" + t.sex + "'";
        }
        if (t.marital_status != null)
        {
            sql += @" and a.marital_status = '" + t.marital_status + "'";
        }
        if (t.level != null)
        {
            sql += @" and a.study_level = '" + t.level + "'";
        }

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

    public static List<Teacher> getListTeacherTaxGroup(Teacher t)
    {
        string sql = @"SELECT  a.id,
                                    a.date_register,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.first_name,
                                    a.last_name,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.Birthdate,
                                    a.Birthplace,
                                    a.Adress,
                                    a.Phone1,
                                    a.position,
                                    a.email,
                                    a.image_path,
                                    a.level,
                                    (select x.group_name from tax_configuration x, staff_tax y
											    where x.id = y.tax_id
												and y.staff_id = a.Id) AS group_name,
                                   (select x.id from tax_configuration x, staff_tax y
												where x.id = y.tax_id
												and y.staff_id = a.Id ) AS group_name_id
                             FROM TEACHER a
                             WHERE 1=1
                                AND a.status = 1
                             ";
        if (t.id != null)
        {
            sql += @" and a.id = '" + t.id.ToUpper() + "'";
        }
        if (t.fullName != null)
        {
            sql += @" and concat(lower(a.first_name), ' ', lower(a.last_name)) like '%" + t.fullName.ToLower() + "%'";
        }

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

    public static List<Teacher> getListActiveTeacher(Teacher t)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.first_name,
                                    a.last_name,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.Birthdate,
                                    a.Birthplace,
                                    a.Adress,
                                    a.Phone1,
                                    a.position,
                                    a.email,
                                    a.image_path,
                                    a.level,
									case
										when a.id in(select b.teacher_id from teacher_cours_attach b where b.teacher_id = a.id)
										then (select count(b.course_id)
												from teacher_cours_attach b where b.teacher_id = a.id)
										end as cours_counter,
                                    '' as classroom, '' as vacation
                             FROM TEACHER a
                             WHERE 1=1
                                AND a.id like ?
                                AND concat(lower(a.first_name),' ',lower(a.last_name)) like ? 
                                AND a.phone1 like ?
                                AND sexe like ?
                                AND a.marital_status like ?
                                AND a.level like ?
                                AND a.status = 1
                             ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.id, t.fullName, t.phone1, t.sex, t.marital_status, t.fromDate, t.toDate, t.level);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Teacher> getListTeacherCourse(Teacher t)
    {
        string sql = @"select t.id, concat(t.Last_name,' ',t.First_name) as fullname,
                        (select group_concat('-',' ',c.name)
                            from  teacher_cours_attach tca
                            inner join cours c on c.id = tca.cours_id
                            where tca.teacher_id = t.id) as cours_name
                        from teacher t
                        where t.status = 1";

        if (t.id != null)
        {
            sql += @" AND a.id = '" + t.id + "'";
        }

        if (t.fullName != null)
        {
            sql += @" AND concat(lower(a.Last_name),' ',lower(a.First_name)) like '%" + t.fullName.ToLower() + "%'";
        }

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

    public static List<Teacher> getListActiveTeacherFoSchedule(Teacher t)
    {
        string sql = @"SELECT  a.id, concat(a.Last_name,' ',a.First_name) as fullname,  
                            count(distinct(sch.Id_cours)) cours_counter FROM TEACHER a
                            inner join schedule sch on sch.Id_teacher = a.id
                               WHERE  a.status = 1 ";

        if (t.id != null)
        {
            sql += @" AND a.id = " + t.id + "";
        }

        if (t.academic_year != -1)
        {
            sql += @" AND sch.academic_year = " + t.academic_year + "";
        }

        if (t.fullName != null)
        {
            sql += @" AND concat(lower(a.first_name),' ',lower(a.last_name)) like " + t.fullName + "";
        }

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

    public static List<Teacher> getListActiveTeacherForTimesheet(Teacher teacher)
    {
        string sql = @"SELECT  a.id, a.Register_date, concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.level
                             FROM TEACHER a
                             WHERE a.status = 1
                                GROUP BY a.Last_name,a.First_name, a.level";

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

    public static List<Teacher> getListActiveTeacherForTimesheet_bk(Teacher t)
    {
        string sql = @"SELECT  a.id,
                                    a.Register_date,
                                    concat(a.Last_name,' ',a.First_name) as fullname,
                                    a.first_name,
                                    a.last_name,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.Birthdate,
                                    a.Birthplace,
                                    a.Adress,
                                    a.Phone1,
                                    a.position,
                                    a.email,
                                    a.image_path,
                                    a.level, null as register_date_string
                             FROM TEACHER a, teacher_cours_attach b
                             WHERE a.id = b.teacher_id
                                AND a.status = 1
                                AND upper(a.id) like ?
                                AND upper(a.first_name) like ?
                                AND upper(a.last_name) like ?
                                GROUP BY  a.Register_date,
                                    a.Last_name,a.First_name,
                                    a.first_name,
                                    a.last_name,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.Birthdate,
                                    a.Birthplace,
                                    a.Adress,
                                    a.Phone1,
                                    a.position,
                                    a.email,
                                    a.image_path,
                                    a.level";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.id, t.first_name, t.last_name);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Teacher> getListActiveTeacher()
    {
        string sql = @"SELECT  a.*,
                                    concat(a.first_name,' ',a.last_name) as fullname
                             FROM TEACHER a
                             WHERE  a.status = 1 order by a.first_name asc
                             ";
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

    public static List<Teacher> getListAllTeacher()
    {
        string sql = @"SELECT  a.*,
                            concat(a.Last_name,' ',a.First_name) as full_name
                        FROM TEACHER a 
                        where a.status = 1";
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

    public static List<Teacher> getListTeacherByReference(Teacher t)
    {
        string sql = @"SELECT  a.Id,
                                    a.Register_date,
                                    a.Last_name,
                                    a.First_name,
                                    concat(a.Last_name,' ',a.First_name) as full_name,
                                    a.Sexe,
                                    a.Marital_status,
                                    a.Id_card,
                                    a.Birthdate,
                                    a.Birthplace,
                                    a.Adress,
                                    a.Phone1,
                                    a.position,
                                    a.email,
                                    a.image_path,
                                    a.level
                                FROM TEACHER a
                                WHERE  a.status = 1
                                    AND a.id like ?
                                    AND concat(lower(a.first_name),' ',lower(a.last_name)) like ? 
                                    AND a.sexe like ?
                             ";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.id, t.fullName, t.sex);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Teacher getTeacherInfoById(string teacherId)
    {
        string sql = @"SELECT  a.*,
                            concat(a.first_name,' ',a.Last_name) as fullname,
						    ttx.tax_id
                             FROM teacher a
								left join teacher_tax ttx on ttx.teacher_id = a.id
                            WHERE a.status = 1
                            and a.id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherId);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public static List<Teacher> getListTeacherByCourseId(int idCourse)
    {
        string sql = @"SELECT a.id, concat(a.first_name,' ',a.last_name) as fullname
                            FROM TEACHER a
                                inner join teacher_cours_attach b on b.teacher_id = a.id
                            WHERE a.status = 1
                                AND b.cours_id = ? 
                            ORDER by a.first_name asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idCourse);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Teacher> getListTeacherForExam(int courseId, string vacationId, int classId, int academicYear)
    {
        string sql = @"SELECT a.id, concat(a.first_name,' ',a.last_name) as fullname
                            FROM TEACHER a, schedule b
                            WHERE a.status = 1
                                AND a.id = b.teacher_id";

        if (courseId != 0)
        {
            sql += @"  AND b.Id_cours = " + courseId + " ";
        }

        if (vacationId != null)
        {
            sql += @"  AND b.vacation = '" + vacationId + "' ";
        }

        if (classId != 0)
        {
            sql += @"  AND b.Id_class = " + classId + " ";
        }

        if (academicYear != 0)
        {
            sql += @"  AND b.academic_year = " + academicYear + " ";
        }

        sql += @" group by concat(a.first_name,' ',a.last_name), a.id";

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

    public static List<Teacher> getListTeacherByCourseIdAndClassroomId(int idCours, int idClassroom)
    {
        string sql = @"select a.id, concat(a.first_name,' ',a.last_name) as fullname
                            from teacher a, schedule b
                            where a.id = b.id_teacher
                            and b.id_cours = ?
                            and b.id_class = ?
                            and a.status = 1
                            GROUP by a.id, concat(a.first_name,' ',a.last_name) ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idCours, idClassroom);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteTeacherTemporarily(string teacherCode)
    {
        string sql = @"update teacher set status = 0 where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteTeacherPermanently(string teacherCode)
    {
        string sql = @"delete from teacher where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static void updateTeacher(Teacher teacher)
    //{
    //    
    //    string sql = @"UPDATE teacher set 
    //                            first_name = @first_name,
    //                            last_name = @last_name,
    //                            sexe = @sexe,
    //                            marital_status = @marital_status,
    //                            id_card = @id_card,
    //                            birthplace = @birthplace,
    //                            birthdate = @birthdate,
    //                            adress = @adress,
    //                            phone1 = @phone1,
    //                            email = @email,
    //                            image_path = @image_path,
    //                            level = @level,
    //                            reference_status = @reference_status
    //                        WHERE id = @id";
    //    MySqlConnection con = new MySqlConnection(constr);
    //    MySqlCommand cmd;
    //    try
    //    {
    //        con.Open();
    //        cmd = con.CreateCommand();
    //        cmd.CommandText = sql;
    //        cmd.Parameters.AddWithValue("@id", teacher.id);
    //        cmd.Parameters.AddWithValue("@first_name", teacher.first_name);
    //        cmd.Parameters.AddWithValue("@last_name", teacher.last_name);
    //        cmd.Parameters.AddWithValue("@sexe", teacher.sex);
    //        cmd.Parameters.AddWithValue("@marital_status", teacher.marital_status);
    //        cmd.Parameters.AddWithValue("@id_card", teacher.id_card);
    //        cmd.Parameters.AddWithValue("@birthdate", teacher.birth_date);
    //        cmd.Parameters.AddWithValue("@birthplace", teacher.birth_place);
    //        cmd.Parameters.AddWithValue("@adress", teacher.address);
    //        cmd.Parameters.AddWithValue("@phone1", teacher.phone1);
    //        cmd.Parameters.AddWithValue("@email", teacher.email.ToLower());
    //        cmd.Parameters.AddWithValue("@image_path", teacher.imagePath);
    //        cmd.Parameters.AddWithValue("@level", teacher.level);
    //        cmd.Parameters.AddWithValue("@reference_status", teacher.reference_status);

    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Error : " + ex.Message);
    //    }
    //    finally
    //    {
    //        // check connection state
    //        if (con != null)
    //        {
    //            try
    //            {
    //                con.Close();
    //                con.Dispose();
    //                MySqlConnection.ClearPool(con);
    //            }
    //            catch (Exception ex)
    //            {
    //                MessBox.Show("Error : " + ex.Message);
    //            }
    //        }
    //    }
    //}

    public static List<Teacher> getListTeacherFromExamByCourseId(int idCourse)
    {
        string sql = @"SELECT a.id, concat(a.first_name,' ',a.last_name) as fullname
                            FROM TEACHER a, exam b
                            WHERE a.status = 1
                                AND a.id = b.id_teacher
                                AND b.id_cours = ?
                            group by a.id, a.first_name, a.last_name ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idCourse);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool idCardExist(string idCard)
    {
        bool result = false;

        string sql = @"SELECT COUNT(*) FROM teacher a  WHERE a.id_card = @idCard ";

        try
        {
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Teacher> getListTeacherForPayrollWithAmount(Teacher t)
    {
        return null;
        // to be reviewed ...


        /*
        string sql = @"SELECT  a.id,
                            a.Register_date,                                   
                            a.first_name,
                            a.last_name,
                            concat(a.last_name,' ', a.First_name) as FULL_NAME,                                    
                            a.Sexe,
                            a.Marital_status,
                            a.Id_card,
                            a.birthDate,
                            a.Birthplace,
                            a.Adress,
                            a.Phone1,
                            a.Status,
                            a.position,                                 
                            a.Email,
                            a.Image_path,
                            a.reference_status,
                             (select x.group_name from tax_configuration x, staff_tax y
									                  WHERE x.id = y.tax_id and y.staff_id = a.Id ) AS group_name,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'septembre') as september_salary,
                            (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'septembre') as september_check,
                           (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'octobre') as october_salary,
                               (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'octobre') as october_check,
                            (SELECT (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'novembre') as november_salary,
                               (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'novembre') as november_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'decembre') as december_salary,
                                                           (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'decembre') as december_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'janvier') as january_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'janvier') as january_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'fevrier') as febuary_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'fevrier') as febuary_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mars') as march_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mars') as march_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'avril') as april_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'avril') as april_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mai') as may_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mai') as may_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juin') as june_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juin') as june_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juillet') as july_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juillet') as july_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'aout') as august_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'aout') as august_check
					             FROM teacher a, staff_salary b    
                                    WHERE 1=1  
                                        AND a.Status =1 ";

        if (t.id != null)
        {
            sql += @" AND upper(a.id) = '" + t.id.ToUpper() + "' ";
        }
        if (t.fullName != null)
        {
            sql += @" AND  lower(concat(a.last_name,' ', a.First_name)) like '%" + t.fullName.ToLower() + "%' ";
        }

        sql += @" group by 
                        a.id,
                        a.Register_date,                                   
                        a.first_name,
                        a.last_name,
                        a.last_name,
                        a.First_name,                                    
                        a.Sexe,
                        a.Marital_status,
                        a.Id_card,
                        a.birthDate,
                        a.Birthplace,
                        a.Adress,
                        a.Phone1,
                        a.Status,
                        a.position,                                 
                        a.Email,
                        a.Image_path,
                        a.reference_status";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@academic_year", t.academic_year);
            return parse(cmd.ExecuteReader());
        }
        catch (Exception ex)
        {
            return null;
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

        */
    }


}