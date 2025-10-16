using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;



public class Course
{
    public int id { get; set; }
    public string name { get; set; }
    public double amount { get; set; }
    public string teacher_id { get; set; }
    public string teacher_fullname { get; set; }
    public int class_id { get; set; }
    public string class_name { get; set; }
    public int cours_id { get; set; }
    public string cours_name { get; set; }
    public int cours_id_price { get; set; }
    public double course_price { get; set; }
    public string vacation_code { get; set; }
    public string vacation_string { get; set; }
    public int status { get; set; }
    public int academic_year { get; set; }
    public int academic_year_id { get; set; }
    public int coefficient { get; set; }
    public string academic_year_concat { get; set; }
    public string course_fullname { get; set; }



    public static List<Course> Parse(MySqlDataReader reader)
    {
        List<Course> listCourse = new List<Course>();
        try
        {
            while (reader.Read())
            {
                Course course = new Course();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { course.id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NAME")
                    {
                        try { course.name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT")
                    {
                        try { course.amount = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_ID")
                    {
                        try { course.teacher_id = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try { course.class_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_ID")
                    {
                        try { course.cours_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_ID_PRICE")
                    {
                        try { course.cours_id_price = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURSE_PRICE")
                    {
                        try { course.course_price = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { course.vacation_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { course.status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { course.cours_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { course.academic_year_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COEFFICIENT")
                    {
                        try { course.coefficient = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_CONCAT")
                    {
                        try { course.academic_year_concat = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { course.class_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURSE_FULLNAME")
                    {
                        try { course.course_fullname = reader.GetValue(i).ToString(); }
                        catch { }
                    }

                }
                listCourse.Add(course);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listCourse;
    }

    public static void addCourse(Course c)
    {
        string sql = @"INSERT INTO COURS(name,update_time,status)
                                VALUES(?, now(),1)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.name);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addCoursePrice(double amount)
    {
        string sql = @"insert into cours_price(amount) values(?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(amount);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static void updateCoursePrice(double price, int id)
    //{

    //    string sql = @"update cours_price set price = ?, update_time = now() where id = ?";

    //    try
    //    {
    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(price, id);
    //        stmt.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static void addNewCourseManagement(Course c)
    {

        string sql = @"INSERT INTO COURS_MANAGEMENT(id_teacher,id_class,id_cours,id_cours_price,vacation,update_time)
                                VALUES(?,?,?,?,?,now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.teacher_id,
                                c.class_id,
                                c.cours_id,
                                c.cours_id_price,
                                c.vacation_code);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void affectCourseToTeacher(string teacherId, int courseCode)
    {
        string sql = @"INSERT INTO teacher_cours_attach(
                                teacher_id, cours_id)
                            VALUES(?,?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherId, courseCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void affectCourseToTeacher(string teacherId, List<String> listCourse)
    {
        if (listCourse != null && listCourse.Count > 0)
        {
            foreach (string coursId in listCourse)
            {
                string sql = @"INSERT INTO teacher_cours_attach(teacher_id, cours_id, update_time)
                                          VALUES(?,?,now())";

                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(teacherId, coursId);
                    stmt.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    public static void affectCoursePriceToClass(List<Course> listCourseInfo)
    {
        if (listCourseInfo != null && listCourseInfo.Count > 0)
        {
            // remove old affected course
            deletePreviouslyAffectedCourseToClassroom(listCourseInfo);

            foreach (Course c in listCourseInfo)
            {
                string sql = @"INSERT INTO classroom_cours_management(
                                        class_id,
                                        cours_id,
                                        coefficient)
                                 VALUES(?,?,?)";

                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(c.class_id, c.cours_id, c.coefficient);
                    stmt.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    public static void deleteCourse(int id)
    {
        string sql = @"delete from cours where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void CourseAssignToTeacher(int courseId)
    {
        string sql = @"select count(*) from teacher_cours_attach a
                             WHERE 1=1
                                AND a.cours_id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(courseId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void CourseAssignToClass(int courseId)
    {
        string sql = @"select count(*) from classroom_cours_management
                             where cours_id = ? ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(courseId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool CourseAssignToExam(int courseId)
    {
        bool result = false;

        string sql = @"select count(*) from exam
                             where cours_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(courseId);
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

    public static void reactivateCourse(string code)
    {
        string sql = @"update cours set status = 1 where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(code);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteCoursePriceById(int id)
    {
        string sql = @"delete from cours_price where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deletePreviouslyAffectedCourseToTeacher(string teacherCode)
    {
        string sql = @"delete from teacher_cours_attach where teacher_id = ?";

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

    public static void deletePreviouslyAffectedCourseToClassroom(List<Course> listCourseInfo)
    {
        if (listCourseInfo != null && listCourseInfo.Count > 0)
        {
            foreach (Course c in listCourseInfo)
            {
                string sql = @"delete from classroom_cours_management 
                                    WHERE class_id = ? and cours_id = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                stmt.SetParameters(c.class_id, c.cours_id);
                stmt.ExecuteNonQuery();
            }
        }
    }

    public static void removeAffectedCourseById(int id)
    {
        string sql = @"delete from classroom_cours_management where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> checkExistedCourseByName(string name)
    {
        string sql = @"select * from cours where lower(name) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(name.ToLower());
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListCourse(Course c)
    {
        string sql = @"SELECT a.* FROM COURS a
                                WHERE 1=1
                                    AND lower(a.name) like @_Name
                                    ORDER by id desc";
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

    public static List<Course> getListAffectedCourseByTeacherCode(string teacherCode)
    {
        string sql = @"select cours_id from teacher_cours_attach 
                        where teacher_id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherCode);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListAffectedCoursePrice(int classId)
    {
        string sql = @"select ccm.*,c.name as class_name, co.name as cours_name, chp.amount
                        from classroom_cours_management ccm
                        inner join classroom c on c.id = ccm.class_id
                        inner join cours co on co.id = ccm.cours_id
						left join classroom_hourly_payment chp on chp.class_id = c.id and chp.cours_id = co.id
                        where 1=1
							and c.id = ?
							order by co.name";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListAttachedCoursesByClassId(int classId)
    {
        string sql = @"SELECT a.*, b.name as cours_name
                            FROM classroom_cours_management a
							inner join cours b on b.id = a.cours_id
                            where a.class_id = ?
                            order by b.name asc";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListScheduledCourse(int classId, string vacation, int academicYear)
    {
        string sql = @"select a.*, (select b.name from cours b where b.Id = a.cours_id) as course_fullname
                            from `schedule` a
                            where a.class_id = ?
                            and a.vacation = ?
                            and a.academic_year = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListScheduledCourseForExam(int classId, string vacation, int academicYear)
    {
        string sql = @"select DISTINCT(a.cours_id) as cours_id, b.name as course_fullname
                            from `schedule` a
                            inner join cours b on b.id = a.cours_id
                            where a.Id_class = ?
                                and a.vacation = ?
                                and a.academic_year = ?
                            order by b.name asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListScheduledCourseByAcademicYear(int academicYear)
    {
        string sql = @"select a.*, (select b.name from cours b where b.Id = a.cours_id) as course_fullname
                            from `schedule` a
                            where a.academic_year = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListScheduledCourseByTeacherCode(
        int academicYear, string vacation, string teacherCode, int classId)
    {
        string sql = @"select DISTINCT(a.id_cours) as id_cours, b.name as course_fullname
                            from `schedule` a
                              inner join cours b on b.id = a.Id_cours
                           where 1=1 ";

        if (academicYear != 0)
        {
            sql += @" and a.academic_year =" + academicYear + "";
        }
        if (vacation != null)
        {
            sql += @" and a.vacation ='" + vacation + "'";
        }
        if (teacherCode != null)
        {
            sql += @" and a.Id_teacher ='" + teacherCode + "'";
        }
        if (classId != 0)
        {
            sql += @" and a.Id_class =" + classId + "";
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

    public static List<Course> getListScheduledCourse(int academicYear)
    {
        string sql = @"select a.id as cours_id, a.name as course_fullname from cours a order by a.name";

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

    public static List<Course> getAllListCourseInverse()
    {
        string sql = @"SELECT  a.* FROM COURS a
                              where a.status = 1   
                            ORDER by name asc";
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

    public static List<Course> getAllListScheduledCourse(Course c)
    {
        string sql = @"select * from cours where id 
                            in(select Id_cours from schedule where vacation = ? 
                            and Id_class = ? and academic_year = ?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.vacation_code, c.class_id, c.academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getAllListCourseOrderedByNameAsc()
    {
        string sql = @"select  * from cours order by name asc";
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

    public static List<Course> getListAllCourse()
    {
        string sql = @"SELECT  a.* FROM COURS a ORDER by a.name asc";
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

    public static List<Course> getListCourseByName(string name)
    {
        string sql = @"SELECT  a.* FROM COURS a
                             where 1=1";
        if (name != null)
        {
            sql += @" and upper(a.name) like '%" + name.ToUpper() + "%'";
        }
        sql += @" ORDER by a.name asc";

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

    public static List<Course> getListAllCourseAttachedToTeacher()
    {
        string sql = @"select distinct(c.name), c.id from cours c
                        inner join teacher_cours_attach tca on tca.cours_id = c.id
                        order by c.name asc";

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

    public static List<Course> getListCoursePrices()
    {
        string sql = @"select * from cours_price order by amount asc";

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

    public static List<Course> getListCoursePriceOrderedPrimaryClass()
    {
        string sql = @"SELECT a.* FROM COURS_PRICE a where id = -2";

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

    public static List<Course> getListCoursePriceByAmount(double amount)
    {
        string sql = @"SELECT  a.* FROM COURS_PRICE a
                          WHERE 1=1 AND a.amount = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(amount);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListCoursePriceById(int id)
    {
        string sql = @"SELECT  a.*
                                FROM COURS_PRICE a
                                    WHERE 1=1
                                     AND a.id = ?
                                  ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListCourseByClassId(int idClass)
    {
        string sql = @"SELECT  a.id,a.name,a.update_time,a.status
                            FROM COURS a, classroom_cours_management b
                            WHERE 1=1
                            and a.id = b.id_cours
                            and b.id_class = ?
                            group by a.name,a.id,a.update_time,a.status";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idClass);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListAvailableCourse(Course c)
    {
        string sql = @"SELECT  a.id,a.name
                            FROM COURS a, classroom_cours_management b, 
                             classroom_vacation_management c
                            WHERE 1=1
                            and a.id = b.id_cours
                            and b.id_class = c.classroom_id
                            and c.classroom_id = b.Id_Class
                            and c.vacation_status = 1";

        if (c.class_id != 0)
        {
            sql += @" and c.classroom_id = " + c.class_id + " ";
        }

        sql += @" group by a.id,a.name";

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

    public static List<Course> getListCourseForPalmares(Course c)
    {
        string sql = @"SELECT a.id, a.name from cours a 
                                where a.id in(
                                select Id_Cours from exam
                                where Id_class = ?
                                and academic_year = ?
                                and vacation = ?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.class_id, c.academic_year, c.vacation_code);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListAttachedCourseByTeacherId(string teacherId)
    {
        string sql = @"select a.id, a.name
                            from cours a
                              inner join teacher_cours_attach b on b.cours_id = a.id
                            where b.teacher_id = ? 
                            group by a.name";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListScheduleCourseByTeachId(string teacherCode, int academicYear)
    {
        string sql = @"SELECT * from cours 
                            where id in (select distinct(id_cours) from schedule 
                                            where Id_teacher = ? and academic_year = ?)
                            order by name asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherCode, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Course> getListAllCourseActive()
    {
        string sql = @"select * from cours
                        order by `name`";

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

    public static List<Course> getListCourseFromExamByClassId(int idClass)
    {
        string sql = @"SELECT  a.id,a.name
                            FROM COURS a, exam b
                            WHERE 1=1
                                and a.id = b.id_cours
                                and b.id_class = ?
                            order by a.name asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idClass);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int getCourseIdByName(string name)
    {
        string sql = @"SELECT  a.id FROM COURS a  WHERE upper(a.name) = upper(?) and a.status = 1";
        int result = 0;

        try
        {
            try
            {
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                stmt.SetParameters(name.ToUpper());
                IDataReader reader = stmt.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) > 0)
                        {
                            result = reader.GetInt32(0);
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool checkPriceInCoursManagement(int id_price)
    {
        bool result = false;

        string sql = @"select count(*) from classroom_cours_management where id_cours_price = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id_price);
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

    public static bool coursIsScheduledForTeacher(int coursId, string teacherCode)
    {
        bool result = false;

        string sql = @"select count(*) from `schedule` where id_teacher = ? and Id_cours = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherCode, coursId);
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

    public static List<Course> getListCourseForPalmares(int classId)
    {
        string sql = @"select c.name as cours_name, ccm.coefficient
                        from cours c 
                        inner join classroom_cours_management ccm on ccm.cours_id = c.Id
                        where ccm.class_id = ?
                        order by c.name";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}