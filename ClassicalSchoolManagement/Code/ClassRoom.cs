using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;

public class ClassRoom
{
    //getters and setters
    public int id { get; set; }
    public int academic_year_id { get; set; }
    public int class_id { get; set; }
    public string name { get; set; }
    public int fixed_capacity { get; set; }
    public int current_capacity { get; set; }
    public int status { get; set; }
    public DateTime update_time { get; set; }
    public string vacation_type { get; set; }
    public int vacation_status { get; set; }
    public string vacation { get; set; }
    public int capacity { get; set; }
    public int am_cc { get; set; }
    public int pm_cc { get; set; }
    public int ng_cc { get; set; }
    public int wk_cc { get; set; }
    public string academic_year_concat { get; set; }
    public string vacation_definition { get; set; }
    public double success_percent { get; set; }
    public DateTime date_register { get; set; }
    public string login_user { get; set; }
    public double amount { get; set; }
    public int login_user_id { get; set; }
    public int cours_id { get; set; }
    public string class_name { get; set; }
    public string course_name { get; set; }
    public string start_hour { get; set; }
    public string end_hour { get; set; }
    public string days { get; set; }
    public string work_hours { get; set; }
    public decimal price_per_hour { get; set; }
    public string staffCode { get; set; }
    public string years { get; set; }
    public string monday { get; set; }
    public string tuesday { get; set; }
    public string wednesday { get; set; }
    public string thursday { get; set; }
    public string friday { get; set; }
    public string saturday { get; set; }
    public string classroom_name { get; set; }
    public string cours_name { get; set; }
    public string vacation_name { get; set; }



    public enum STATUS
    {
        ACTIVE = 1,
        NOT_ACTIVE = 0
    }



    public static List<ClassRoom> Parse(MySqlDataReader reader)
    {
        List<ClassRoom> listClassroom = new List<ClassRoom>();
        try
        {
            while (reader.Read())
            {
                ClassRoom classroom = new ClassRoom();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { classroom.id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { classroom.academic_year_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try { classroom.class_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NAME")
                    {
                        try { classroom.name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_CONCAT")
                    {
                        try { classroom.academic_year_concat = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIXED_CAPACITY")
                    {
                        try { classroom.fixed_capacity = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CURRENT_CAPACITY")
                    {
                        try { classroom.current_capacity = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { classroom.status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "UPDATE_TIME")
                    {
                        try { classroom.update_time = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION_TYPE")
                    {
                        try { classroom.vacation_type = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION_STATUS")
                    {
                        try { classroom.vacation_status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { classroom.vacation = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "AM": classroom.vacation_definition = "Matin"; break;
                                case "PM": classroom.vacation_definition = "Median"; break;
                                case "NG": classroom.vacation_definition = "Soir"; break;
                                case "WK": classroom.vacation_definition = "Weekend"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CAPACITY")
                    {
                        try { classroom.capacity = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AM_CC")
                    {
                        try { classroom.am_cc = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PM_CC")
                    {
                        try { classroom.pm_cc = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NG_CC")
                    {
                        try { classroom.ng_cc = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "WK_CC")
                    {
                        try { classroom.wk_cc = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SUCCESS_PERCENT")
                    {
                        try { classroom.success_percent = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { classroom.date_register = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { classroom.login_user = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT")
                    {
                        try { classroom.amount = Double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { classroom.login_user_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_ID")
                    {
                        try { classroom.cours_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { classroom.class_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURSE_NAME")
                    {
                        try { classroom.course_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "start_hour")
                    {
                        try { classroom.start_hour = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "end_hour")
                    {
                        try { classroom.end_hour = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DAYS")
                    {
                        try { classroom.days = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "WORK_HOURS")
                    {
                        try { classroom.work_hours = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRICE_PER_HOUR")
                    {
                        try { classroom.price_per_hour = Decimal.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { classroom.staffCode = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { classroom.years = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MONDAY")
                    {
                        try { classroom.monday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TUESDAY")
                    {
                        try { classroom.tuesday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "WEDNESDAY")
                    {
                        try { classroom.wednesday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "THURSDAY")
                    {
                        try { classroom.thursday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FRIDAY")
                    {
                        try { classroom.friday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SATURDAY")
                    {
                        try { classroom.saturday = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_NAME")
                    {
                        try { classroom.classroom_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_NAME")
                    {
                        try { classroom.cours_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION_NAME")
                    {
                        try { classroom.vacation_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                }
                listClassroom.Add(classroom);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listClassroom;
    }

    //public static List<ClassRoom> getListActiveClassroom()
    //{
    //    try
    //    {
    //        string sql = @"SELECT a.*
    //                        FROM classroom a
    //                        WHERE a.status =1
    //                        ORDER BY a.id asc";

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static List<ClassRoom> getListNewClassRoomById(List<int> listNewClassroomId)
    {
        try
        {
            string sql = @"SELECT a.id, a.name, a.static_quantity, a.status, a.update_time
                            FROM classroom a
                            WHERE 1=1
                            AND a.status =1";

            if (listNewClassroomId != null && listNewClassroomId.Count > 0)
            {
                sql += @" and a.id in (" + String.Join(",", listNewClassroomId) + ")";
            }

            sql += " ORDER BY a.id asc";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static List<ClassRoom> getListActiveClassroomVactaion(int class_id)
    //{
    //    try
    //    {
    //        string sql = @"select a.vacation_type 
    //                       from classroom_vacation_management a,classroom b
    //                       where a.vacation_status=1 and b.Status=1
    //                       and a.class_id=b.Id and b.id = ?";

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(class_id);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static List<ClassRoom> getListVacationByClassroomId(int id)
    {
        try
        {
            string sql = @"SELECT a.*, 
                                case when vacation_type ='AM' then 'Matin'
                                when vacation_type ='PM' then 'Median'
                                when vacation_type ='NG' then 'Soir'
                                when vacation_type ='WK' then 'Weekend'
                                end as vacation
                            FROM classroom_vacation_management a
                            WHERE class_id = ?
                            AND vacation_status = 1";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static List<ClassRoom> getListVacationConfig(int classId)
    //{
    //    try
    //    {
    //        string sql = @"select cvm.vacation_type as vacation, cvm.capacity as fixed_capacity,
    //                    (select count(*) from classroom_staff_management
    //                        where class_id = cvm.class_id
    //                               and vacation = cvm.vacation_type) current_capacity
    //                    from classroom_vacation_management cvm
    //                    where cvm.class_id = ?";

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(classId);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public static ClassRoom getClassroomCapacityConfigByVacation(
    //    int classId, string vacation)
    //{
    //    try
    //    {
    //        string sql = @"select cvm.vacation_type as vacation, cvm.capacity as fixed_capacity,
    //                    (select count(*) from classroom_staff_management 
    //                        where class_id = cvm.class_id
    //                               and vacation = cvm.vacation_type) current_capacity
    //                    from classroom_vacation_management cvm
    //                    where 1=1
    //                        and cvm.class_id = ?
    //                        and cvm.vacation_type = ?";

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(classId, vacation);
    //        return Parse(stmt.ExecuteReader())[0];
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static List<ClassRoom> getListActiveClassroomAfectTo_teacher()
    {
        try
        {
            string sql = @"SELECT distinct(a.name), a.id,b.class_id 
                            FROM classroom a,cours_management b
                            WHERE a.id=b.class_id
                            ORDER BY a.id asc";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListClassroom(int classId, int currentStatus)
    {
        try
        {
            string sql = @"SELECT a.*,
                            (select concat(extract(YEAR from start_date),' - ',extract(YEAR from end_date)) 
                                from academic_year where id = 26) as academic_year_concat,
                            (select count(*) from classroom_staff_management b
			                             where b.class_id = a.id
				                            and b.status = 1
				                            and b.staff_code like 'EL-%'
				                            and b.vacation = 'AM') as am_cc, -- AM current_capacity
                            (select count(*) from classroom_staff_management b
			                             where b.class_id = a.id
				                            and b.status = 1
				                            and b.staff_code like 'EL-%'
				                            and b.vacation = 'PM') as pm_cc, -- PM current_capacity
                            (select count(*) from classroom_staff_management b
			                             where b.class_id = a.id
				                            and b.status = 1
				                            and b.staff_code like 'EL-%'
				                            and b.vacation = 'NG') as ng_cc, -- NG current_capacity
                            (select count(*) from classroom_staff_management b
			                             where b.class_id = a.id
				                            and b.status = 1
				                            and b.staff_code like 'EL-%'
				                            and b.vacation = 'WK') as wk_cc -- WK current_capacity
			                        FROM classroom a
		                         wHERE 1=1";

            if (currentStatus >= 0)
            {
                sql += @" and a.status = " + currentStatus + " ";
            }

            if (classId > 0 && classId <= 3)
            {
                sql += @" and a.id = " + classId + " ";
            }

            if (classId > 3)
            {
                sql += @" and a.id between " + classId + " and " + (classId + 9) + " ";
            }


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static ClassRoom getClassCapacityInfo(int classId, string vacation, int academicYearId)
    //{
    //    List<ClassRoom> listResult = null;
    //    
    //    string sql = @"SELECT a.capacity as static_quantity,(select count(*) from classroom_staff_management b
    //                      where b.class_id =@id1 and b.academic_year = @academicYear and b.vacation = @vacation_type1) as capacity
    //                     FROM classroom_vacation_management a
    //                     WHERE a.class_id =@id2 and a.vacation_type = @vacation_type2";

    //    MySqlConnection con = new MySqlConnection(constr);
    //    MySqlCommand cmd = new MySqlCommand();
    //    MySqlDataReader reader = null;

    //    try
    //    {
    //        con.Open();
    //        cmd = con.CreateCommand();
    //        cmd.CommandText = sql;
    //        cmd.Parameters.AddWithValue("@id1", classId);
    //        cmd.Parameters.AddWithValue("@id2", classId);
    //        cmd.Parameters.AddWithValue("@vacation_type1", vacation);
    //        cmd.Parameters.AddWithValue("@vacation_type2", vacation);
    //        cmd.Parameters.AddWithValue("@academicYear", academicYearId);

    //        reader = cmd.ExecuteReader();
    //        if (reader != null)
    //        {
    //            listResult = parse(reader);
    //        }
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
    //    return listResult[0];
    //}

    public static List<ClassRoom> getListClassroomForSalaryById(int _id, int _limit)
    {
        try
        {
            string sql = @"SELECT a.id, a.name, a.static_quantity, a.status, a.update_time,
                            (select count(*) from classroom_staff_management b, student c
                               where b.class_id = a.id 
		                            and b.staff_code = c.Id
		                           and c.Status =1
                                and b.staff_code like 'EL-%') as capacity
                            FROM classroom a
                           wHERE a.id between ? and ? and a.status = 1
                            ORDER BY a.id asc";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(_id, _limit);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListactifClassroomById(int _id, int _limit)
    {
        try
        {
            string sql = @"SELECT a.id, a.name, a.status, a.update_time
                            FROM classroom a
                            WHERE a.id between ? and ? and a.status='1'
                            ORDER BY a.id asc";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(_id, _limit);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListAllClassroom()
    {
        try
        {
            string sql = @"SELECT a.*,
                            (SELECT count(*) from classroom_staff_management b, student c
                               WHERE b.class_id = a.id 
		                            and b.staff_code = c.Id
		                            and c.Status =1
                                and b.staff_code like 'EL-%') as capacity
                            FROM classroom a
							where group by a.id
                                ORDER BY a.id asc";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListActiveClassroom()
    {
        try
        {
            string sql = @"SELECT a.*,
                            (SELECT count(*) from classroom_staff_management b, student c
                               WHERE b.class_id = a.id 
		                            and b.staff_code = c.Id
		                            and c.Status =1
                                and b.staff_code like 'EL-%') as capacity,
							cfp.amount
                            FROM classroom a
							left join classroom_fixed_payment cfp on cfp.class_id = a.id														
							where status = 1
                                group by a.id
                                ORDER BY a.id asc";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static List<ClassRoom> getListActiveClassroomWithAverageById(int _id, int _limit, int academicYear)
    //{
    //    try
    //    {
    //        string sql = @"SELECT a.id, a.name, a.static_quantity, a.status, a.update_time,
    //                            (select b.success_percent from classroom_average_management b
    //                                 where 1=1 
    //                                    and b.class_id = a.id
    //                                       and b.academic_year = ?) as success_percent
    //                        FROM classroom a
    //                          WHERE 1=1
    //                            AND a.id between ? AND ?
    //                            AND a.status = 1
    //                          ORDER BY a.id asc";


    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(academicYear, _id, _limit);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public static List<ClassRoom> getListAllClassroomWithAverage(int academicYear)
    //{
    //    try
    //    {
    //        string sql = @"SELECT a.id, a.name, a.status,
    //                            (select b.success_percent from classroom_average_management b
    //                                 where 1=1 
    //                                    and b.class_id = a.id
    //                                       and b.academic_year = ?) as success_percent
    //                            FROM classroom a
    //                            WHERE 1=1
    //                            and a.status= 1
    //                            ORDER BY a.id asc";


    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(academicYear);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static List<ClassRoom> getListClassroomAverage(int accademicYearId, int classId)
    {
        try
        {
            string sql = @"SELECT a.id, a.name as class_name,
                                (select b.success_percent from classroom_average_management b
                                     where 1=1
                                        and b.class_id = a.id
                                           and b.academic_year = " + accademicYearId + @") as success_percent,
							(select concat(extract(YEAR from start_date),' - ',extract(YEAR from end_date)) 
                                    from academic_year where id = " + accademicYearId + @") as years,
							(select id from academic_year where id = " + accademicYearId + @") as academic_year_id
                                FROM classroom a
                                WHERE 1=1
                                and a.status= 1
								[ and a.id = ? ]  -- 0
                                ORDER BY a.id asc";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (classId > 0)
            {
                stmt.SetParameter(0, classId);
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListAllactifClassroom()
    {

        try
        {
            string sql = @"SELECT a.id, a.name, a.status, a.update_time
                            FROM classroom a where a.status = 1
                            ORDER BY a.id asc";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListActiveClassroomForSchedule(ClassRoom c)
    {
        string sql = @"SELECT a.id, a.name as class_name, cvm.vacation_type as vacation,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'MO') as monday,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'TU') as tuesday,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'WE') as wednesday,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'TH') as thursday,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'FR') as friday,
												(select  group_concat('','<br/>',CONCAT(time_format(sch.start_hour,'%H:%m'),'-',time_format(sch.end_hour,'%H:%m'),' : ', c.name)) 
																			from schedule sch
																inner join cours c on c.id = sch.cours_id
																where sch.class_id = a.id
																and sch.vacation = cvm.vacation_type
																and sch.Days = 'SA') as saturday
			            FROM classroom a
			                inner join classroom_vacation_management cvm on cvm.class_id = a.id
			            where a.status = 1 
                        and cvm.vacation_status = 1 
                        and a.id = ?";


        if (c.vacation != "-1")
        {
            sql += @" and cvm.vacation_type = '" + c.vacation + "' ";
        }

        sql += @" ORDER BY a.id asc";


        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.id);

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string getClassroomNameById(int id)
    {
        string sql = @"SELECT a.name FROM classroom a
                            WHERE a.id = ? and a.status= 1";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            return Parse(stmt.ExecuteReader())[0].name;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int getClassroomIdByName(string name)
    {

        // to be reviewed ...

        int result = 0;

        string sql = @"SELECT id FROM classroom a
                            WHERE a.name = @name and a.status= 1";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            //return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return 0;
    }

    public static int getStatusByClassId(int id)
    {
        return 0;

        // to be reviewed ...

        /*
        int result = 0;

        string sql = @"SELECT a.status
                            FROM classroom a
                            Where a.id =@id";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        con.Open();
        try
        {
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", id);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result = reader.GetInt32(0);
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show(ex.Message);
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
        return 0;
        */
    }

    //public static void addStaffToClass(int classId, string staff_code, int academic_year)
    //{
    //    
    //    string sql = @"INSERT INTO classroom_staff_management
    //                    VALUES(@_classId,@_studentCode,now(),@academic_year)";

    //    MySqlConnection con = new MySqlConnection(constr);
    //    MySqlCommand cmd = new MySqlCommand();
    //    cmd = con.CreateCommand();
    //    cmd.CommandText = sql;
    //    cmd.Parameters.AddWithValue(@"_classId", classId);
    //    cmd.Parameters.AddWithValue(@"_studentCode", staff_code);
    //    cmd.Parameters.AddWithValue(@"academic_year", academic_year);
    //    //
    //    try
    //    {
    //        con.Open();
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


    public static void attachStudentToClassroom(Student st)
    {
        string sql = @"INSERT INTO student_classroom_attach
                            VALUES(?, ?, ?, ?, 1)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(st.class_id, st.id, st.academic_year_id, st.vacation);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addStaffToClass(int classId, string staff_code, int academic_year, string vacation)
    {
        string sql = @"INSERT INTO classroom_staff_management
                            VALUES(?, ?, ?, ?, 1)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, staff_code, academic_year, vacation);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateStaffClassInfo(int classId, string staff_code, string vacation)
    {

        string sql = @"UPDATE classroom_staff_management set vacation = @vacation, class_id = @class_id,
                             update_time = now()
                            where staff_code = @staff_code and status = 1";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(vacation, classId, staff_code);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateClassroomInfo(ClassRoom c)
    {

        string sql = @"update classroom set status= @status where id= @id";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.status, c.id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static void updateClassroomVacation(int class_id, int vacation_status, string vacation_type)
    //{

    //    string sql = @"update classroom_vacation_management
    //                        set vacation_status = ?                                                  
    //                        where vacation_type = ?
    //                        and class_id = ?";

    //    try
    //    {

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(vacation_status, vacation_type, class_id);
    //        stmt.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public static void updateClassroomVacationFixedCapacity(int classId, int fixedCapacity, string vacationType)
    //{

    //    string sql = @"update classroom_vacation_management
    //                        set capacity = ?                           
    //                        where vacation_type = ? 
    //                        and class_id = ?";

    //    try
    //    {
    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(fixedCapacity, vacationType, classId);
    //        stmt.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static void updateClassroomStaffManagement(int idClass, string staffCode)
    {
        string sql = @"UPDATE classroom_staff_management set 
                            class_id = ?, update_time = now()
                            WHERE staff_code= ?";

        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idClass, staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int GetClassAvailableQuantity(int classId)
    {
        int result = 0;

        string sql = @"select a.static_quantity from classroom a
                            where  a.id = ? and status =1; ";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(classId);
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

    public static List<ClassRoom> getListClassroomByStaffCode(string staffCode)
    {
        string sql = @"SELECT a.id, a.name, a.static_quantity, a.status, a.update_time
                            FROM classroom a, classroom_staff_management b
                            where a.id = b.class_id
                            and b.staff_code = ?";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> GetListClassroomByExam(string academicYearStart,
        string academicYearEnd, int control)
    {
        string sql = @"SELECT a.id, a.name
                            FROM classroom a, exam b
                            where a.id = b.class_id
                                and b.academic_year_start = ?
                                and b.academic_year_end = ?
                                and period = ?
                            group by a.id, a.name";

        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(academicYearStart, academicYearEnd, control);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static List<ClassRoom> getListActiveVacationByClass(int id)
    //{
    //    string sql = @"select * from classroom_vacation_management
    //                        where class_id = ?
    //                        and vacation_status = 1";

    //    try
    //    {

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(id);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static void AddClassroomAverage(List<ClassRoom> listClassroom)
    {
        try
        {
            if (listClassroom != null && listClassroom.Count > 0)
            {
                // delete previous
                DeleteClassroomAverage(listClassroom);

                // add new
                foreach (ClassRoom c in listClassroom)
                {
                    string sql = @"INSERT INTO classroom_average_management
                                         (success_percent,class_id,academic_year,date_register,login_user)
                                            values(?, ?, ?, now(), ?)";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(c.success_percent, c.id, c.academic_year_id, c.login_user);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void DeleteClassroomAverage(List<ClassRoom> listClassroom)
    {
        try
        {
            if (listClassroom != null && listClassroom.Count > 0)
            {
                // add new
                foreach (ClassRoom c in listClassroom)
                {
                    string sql = @"DELETE FROM classroom_average_management
                                         WHERE class_id = ? and academic_year = ?";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(c.id, c.academic_year_id);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getClassroomCurrentSalary(int classId, string vacation_type)
    {

        string sql = @"SELECT  * from classroom_salary_management where class_id = ?
                              and status = 1 and vacation_type = ?";
        try
        {

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation_type);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void updateExistingSalaryAmountForClassroom(ClassRoom c)
    {
        string sql = @"UPDATE classroom_salary_management set status = 0
                             WHERE class_id = ? and vacation_type = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.class_id, c.vacation_type);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteExistingSalaryAmountForClassroom(ClassRoom c)
    {
        string sql = @"DELETE FROM classroom_salary_management
                             WHERE class_id = ? and vacation_type = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.class_id, c.vacation_type);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void insertSalaryAmountForClassroom(List<ClassRoom> listClassroom)
    {
        try
        {
            if (listClassroom != null && listClassroom.Count > 0)
            {
                foreach (ClassRoom c in listClassroom)
                {
                    // remove old class info
                    deleteExistingSalaryAmountForClassroom(c);

                    // add new class info
                    string sql = @"INSERT INTO classroom_salary_management(class_id,amount,status,date_register,login_user_id,vacation_type)
                                VALUES(?,?,1,now(),?,?)";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(c.class_id, c.amount, c.login_user_id, c.vacation_type);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex);
        }
    }

    public static ClassRoom getListPrimaryClassInfo(int classroomId)
    {
        try
        {
            string sql = @"select c.name as classroom_name,
					        (select COALESCE(amount,0) from classroom_fixed_payment
							        where class_id = c.id ) as amount
					         from classroom c where c.id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classroomId);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static ClassRoom getClassroomInfoById(int classId)
    {
        try
        {
            string sql = @"select c.name as classroom_name
					         from classroom c where c.id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool checkExistingPrimaryClassPrice(int priceId)
    {
        bool result = false;

        string sql = @"SELECT count(*) FROM COURS_PRICE WHERE ID = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(priceId);
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

    public static void InsertDefaultPriceForPrimaryClass(int priceId, int amount)
    {

        string sql = @"INSERT INTO cours_price
                            VALUES(?, ?, now())";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(priceId, amount);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void InsertFixedPayment(int classId, double amount)
    {
        // remove old fixed payment
        RemoveOldFixedPayment(classId);


        // add new fixed payment
        string sql = @"INSERT INTO classroom_fixed_payment(class_id, amount)
                            VALUES(?, ?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, amount);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveOldFixedPayment(int classId)
    {

        string sql = @"DELETE FROM classroom_fixed_payment
                            WHERE class_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void InsertHourlyPayment(List<ClassRoom> listClassPrice)
    {
        // remove old fixed payment
        RemoveOldHourlyPayment(listClassPrice[0].id);

        try
        {
            foreach (ClassRoom c in listClassPrice)
            {
                // add new fixed payment
                string sql = @"INSERT INTO classroom_hourly_payment(class_id, cours_id, amount)
                            VALUES(?,?,?)";


                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                stmt.SetParameters(c.id, c.cours_id, c.amount);
                stmt.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveOldHourlyPayment(int classId)
    {

        string sql = @"DELETE FROM classroom_hourly_payment
                            WHERE class_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void disableStudentFromOldClassroom(string studentCode)
    {

        // add new class info
        string sql = @"update classroom_staff_management set status = 0 where staff_code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(studentCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void changeClassroom(ClassRoom c)
    {

        // add new class info
        string sql = @"insert into classroom_staff_management
                            values(?, ?, now(), ?, ?, 1)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(c.class_id, c.staffCode, c.academic_year_id, c.vacation);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void changeClassroomStatus(int classId, int status)
    {

        // add new class info
        string sql = @"update classroom set status = ? where id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(status, classId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<ClassRoom> getListScheduleCourseByTeachId(string teacherCode, int academicYear)
    {
        string sql = @"SELECT * from classroom 
                            where id in (select distinct(class_id) from schedule 
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

}