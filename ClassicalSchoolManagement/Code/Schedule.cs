using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Schedule
{
    //getters and setters herdj
    public string id { get; set; }
    public TimeSpan time_in { get; set; }
    public TimeSpan time_out { get; set; }
    public double total_work_hour { get; set; }
    public int class_id { get; set; }
    public int login_user_id { get; set; }
    public string description { get; set; }
    public DateTime exam_date { get; set; }
    public string code_classroom { get; set; }
    public int schedule_id { get; set; }
    public string class_name { get; set; }
    public int cours_id { get; set; }
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
    public int period { get; set; }
    public string period_name { get; set; }
    public string days { get; set; }
    public string day_code { get; set; }
    public int academic_year { get; set; }
    public string academic_year_start { get; set; }
    public string academic_year_end { get; set; }
    public string fullName { get; set; }
    public TimeSpan Total_Time { get; set; }
    public double Total_time_converted { get; set; }
    public string hour_counter { get; set; }
    public DateTime register_date_timesheet { get; set; }
    public int presence_status { get; set; }
    public string presence_status_definition { get; set; }
    public int validation_status { get; set; }
    public int absence_reason { get; set; }
    public string absence_reason_definition { get; set; }
    public string total_hour { get; set; }
    public DateTime from_date { get; set; }
    public DateTime to_date { get; set; }
    public string years { get; set; }
    public int academic_year_old { get; set; }
    public int academic_year_new { get; set; }
    public int teacher_cours_attach_status { get; set; }
    public DateTime sheet_date { get; set; }
    public string staff_code { get; set; }
    public string days_code { get; set; }
    public string sheet_date_inserted { get; set; }



    public static List<Schedule> Parse(MySqlDataReader reader)
    {
        List<Schedule> listSchedule = new List<Schedule>();
        try
        {
            while (reader.Read())
            {
                Schedule schedule = new Schedule();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { schedule.id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TIME_IN")
                    {
                        try { schedule.time_in = reader.GetTimeSpan(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TIME_OUT")
                    {
                        try { schedule.time_out = reader.GetTimeSpan(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        try { schedule.description = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "cours_id")
                    {
                        try { schedule.cours_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try { schedule.class_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { schedule.login_user_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SCHEDULE_ID")
                    {
                        try { schedule.schedule_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_ID")
                    {
                        try { schedule.teacher_id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "AM": schedule.vacation = "Matin"; break;
                                case "PM": schedule.vacation = "Median"; break;
                                case "NG": schedule.vacation = "Soir"; break;
                                case "WK": schedule.vacation = "Weekend"; break;
                                default: schedule.vacation = ""; break;
                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "START_HOUR")
                    {
                        try { schedule.start_hour = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "END_HOUR")
                    {
                        try { schedule.end_hour = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EXAM_DATE")
                    {
                        try { schedule.exam_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_REQUEST")
                    {
                        try { schedule.staff_request = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE")
                    {
                        try { schedule.register_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try { schedule.class_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_NAME")
                    {
                        try { schedule.cours_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_NAME")
                    {
                        try { schedule.teacher_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "POINTS")
                    {
                        try { schedule.points = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_POINTS")
                    {
                        try { schedule.student_points = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PERIOD")
                    {
                        try { schedule.period = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PERIOD_NAME")
                    {
                        try
                        {
                            switch (reader.GetInt32(i))
                            {
                                case 1: schedule.period_name = "1ere"; break;
                                case 2: schedule.period_name = "2ieme"; break;
                                case 3: schedule.period_name = "3ieme"; break;
                                case 4: schedule.period_name = "4ieme"; break;
                                default: schedule.period_name = ""; break;
                            }
                        }

                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "DAYS")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "SU": schedule.days = "Dimanche"; break;
                                case "MO": schedule.days = "Lundi"; break;
                                case "TU": schedule.days = "Mardi"; break;
                                case "WE": schedule.days = "Mercredi"; break;
                                case "TH": schedule.days = "Jeudi"; break;
                                case "FR": schedule.days = "Vendredi"; break;
                                case "SA": schedule.days = "Samedi"; break;
                                default: schedule.days = ""; break;
                            }
                        }

                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DAYS")
                    {
                        try { schedule.day_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { schedule.academic_year = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_START")
                    {
                        try { schedule.academic_year_start = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_END")
                    {
                        try { schedule.academic_year_end = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { schedule.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_TIME")
                    {
                        try { schedule.Total_Time = TimeSpan.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_TIME_CONVERTED")
                    {
                        try { schedule.Total_time_converted = reader.GetDouble(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "HOUR_COUNTER")
                    {
                        try { schedule.hour_counter = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS")
                    {
                        try
                        {
                            schedule.presence_status = reader.GetInt32(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS_DEFINITION")
                    {
                        try
                        {
                            schedule.presence_status_definition = reader.GetString(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VALIDATION_STATUS")
                    {
                        try
                        {
                            if (reader.GetInt32(i) > 0)
                            {
                                schedule.validation_status = reader.GetInt32(i);
                            }
                            else
                            {
                                schedule.validation_status = 0;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON")
                    {
                        try
                        {
                            schedule.absence_reason = reader.GetInt32(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON_DEFINITION")
                    {
                        try
                        {
                            schedule.absence_reason_definition = reader.GetString(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_HOUR")
                    {
                        try
                        {
                            schedule.total_hour = reader.GetString(i);
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE_TIMESHEET")
                    {
                        try { schedule.register_date_timesheet = reader.GetDateTime(i); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { schedule.from_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { schedule.to_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { schedule.years = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_OLD")
                    {
                        try { schedule.academic_year_old = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_NEW")
                    {
                        try { schedule.academic_year_new = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_COURS_ATTACH_STATUS")
                    {
                        try { schedule.teacher_cours_attach_status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SHEET_DATE")
                    {
                        try { schedule.sheet_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DAYS_CODE")
                    {
                        try { schedule.days_code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SHEET_DATE_INSERTED")
                    {
                        try { schedule.sheet_date_inserted = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                }
                listSchedule.Add(schedule);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listSchedule;
    }

    public static void InsertSchedule(Schedule s)
    {
        string sql = @"insert into schedule (class_id, days, cours_id, 
                        teacher_id, vacation, start_hour, end_hour, date_register, login_user_id ) 
                        VALUES(
                            ?, -- class_id,
                            ?, -- days,
                            ?, -- cours_id,
                            ?, -- teacher_id,
                            ?, -- vacation, 
                            ?, -- start_hour, 
                            ?, -- end_hour,                                                   
                            now(),  -- date_register
                            ? -- login_user_id
                            )";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.class_id,
                                s.days,
                                s.cours_id,
                                s.teacher_id,
                                s.vacation,
                                s.start_hour.ToString(),
                                s.end_hour.ToString(),
                                s.login_user_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool ScheduleAvailableForPrimaryclass(Schedule s)
    {
        bool result = false;

        string sql = @"select count(*) from schedule
                            Where class_id = ?
                                and Days= ?
                                and cours_id = ?
                                and vacation = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.class_id,
                                s.days,
                                s.cours_id,
                                s.vacation);


            IDataReader reader = stmt.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        if (reader.GetInt32(0) > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }

        return result;

    }

    public static bool TeacherScheduleAvailable(Schedule s)
    {
        bool result = false;

        string sql = @"select count(*) from schedule
                            Where Days= ?
                                and teacher_id = ?
                                and TIME_FORMAT(start_hour, '%H:%m:%s') <= ?
                                and TIME_FORMAT(end_hour, '%H:%m:%s') >= ? ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.days,
                                s.teacher_id,
                                string.Format("{0:hh\\:mm\\:ss}", s.start_hour),
                                string.Format("{0:hh\\:mm\\:ss}", s.end_hour));


            IDataReader reader = stmt.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        if (reader.GetInt32(0) > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
        return result;
    }

    public static bool CoursAvailableForSecondaryClass(Schedule s)
    {
        bool result = false;

        string sql = @"select count(*) from schedule
                            Where class_id = ?
                                and Days= ?
                                and cours_id = ?
                                and TIME_FORMAT(start_hour, '%H:%m:%s') < ?
                                and TIME_FORMAT(end_hour, '%H:%m:%s') > ? ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.class_id,
                                s.days,
                                s.cours_id,
                                string.Format("{0:hh\\:mm\\:ss}", s.start_hour),
                                string.Format("{0:hh\\:mm\\:ss}", s.end_hour));


            IDataReader reader = stmt.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        if (reader.GetInt32(0) > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Erreur : " + ex.Message);
        }
        return result;
    }
    
    public static bool teacherScheduleAvailable(Schedule _schedule)
    {
        // to be reviewed ...

        /*
        bool result = false;

        string sql = @"select count(*) from schedule
                                Where Id_class = @idClass 
                                and Days= @Days
                                and id_teacher = @idTeacher
                                and TIME_FORMAT(start_hour, '%H:%m:%s') < @endHours
                                and TIME_FORMAT(end_hour, '%H:%m:%s') > @startHours 
                                and academic_year = @academicYear";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        MySqlDataReader reader = null;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idClass", _schedule.class_id);
            cmd.Parameters.AddWithValue("@Days", _schedule.days);
            cmd.Parameters.AddWithValue("@idTeacher", _schedule.teacher_id);
            cmd.Parameters.AddWithValue("@endHours", string.Format("{0:hh\\:mm\\:ss}", _schedule.end_hour));
            cmd.Parameters.AddWithValue("@startHours", string.Format("{0:hh\\:mm\\:ss}", _schedule.start_hour));
            cmd.Parameters.AddWithValue("@academicYear", _schedule.academic_year);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        if (reader.GetInt32(0) > 0)
                        {
                            result = true;
                        }
                    }
                }
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
        return result;
        */

        return false;
    }

    // method to be reviewed
    public static void DeleteShedule(int id)
    {
        string sql = @"delete FROM schedule Where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
    }

    public static void updateSchedule(Schedule s)
    {
        string sql = @"update schedule set             
                        Days = ?,
                        Id_teacher = ?,
                        Id_cours = ?,             
                        start_hour = ?,
                         end_hour = ?                                                   
                       Where id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.days, s.teacher_id, s.cours_id,
                s.start_hour, s.end_hour, s.schedule_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> getSHeduleById(int id_Schedule)
    {
        string sql = @"select a.Days, a.id_teacher, a.Id_cours, TIME_FORMAT(a.start_hour, '%H:%m') as Start_hour, 
                            TIME_FORMAT(a.end_hour, '%H:%m') as End_hour, TIMEDIFF(a.end_hour,a.start_hour) AS Total_Time
                            FROM schedule a
                            Where   a.id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id_Schedule);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> getListSHeduleClasse(int classId, string vacation)
    {
        string sql = @"select a.id, a.Days, b.Name as cours_name, 
                        concat(c.Last_name, ' ', c.First_name) as fullname , c.id as teacher_id,
                         TIME_FORMAT(a.start_hour, '%H:%m') as Start_hour, TIME_FORMAT(a.end_hour, '%H:%m') as End_hour,
                        a.start_hour as Start_hour,a.end_hour as End_hour,
                        TIMEDIFF(a.end_hour,a.start_hour) AS Total_Time, a.vacation,
						(select count(*) from teacher_cours_attach 
                                where teacher_id = a.teacher_id 
                                 and cours_id = a.cours_id) as teacher_cours_attach_status
                        FROM schedule a, cours b,teacher c
                        WHERE 1=1  
						    AND a.class_id = ?  
                            AND a.vacation = ? 
                        AND a.cours_id = b.Id AND a.teacher_id = c.Id 
                        ORDER by FIELD(a.Days,'MO','TU','WE','TH','FR')";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId, vacation.ToUpper());
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> getListScheduleByTeacherCode(string teacherCode)
    {
        string sql = @"select a.Days,a.id as id_Schedule,d.name as class_name,b.Name as COURS_NAME,
                               TIME_FORMAT(a.start_hour, '%H:%m') as Start_hour, 
			                   TIME_FORMAT(a.end_hour, '%H:%m') as End_hour, 
			                   TIMEDIFF(a.end_hour,a.start_hour) AS Total_Time,
			                   (select 
				                SUM(TIMEDIFF(end_hour,start_hour))
				                from schedule 
				                where id_teacher = @teacherCode) AS HOUR_COUNTER
                                      FROM schedule a, cours b,teacher c,classroom d
                                        Where 1=1 
                                        and a.Id_cours = b.Id 
                                        and a.Id_teacher = c.Id 
                                        and a.Id_class=d.id 
                                        and Id_teacher = ?
                           ORDER by FIELD(a.Days,'MO','TU','WE','TH','FR')";

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

    public static List<Schedule> getListScheduleTeacherForTimesheet(Schedule s)
    {
        string sql = @"SELECT
                                a.id as id_schedule,
                                (select name from classroom where id = a.id_class) as class_name,
                                a.days, a.days as days_code,
                                (select name from cours where id = a.id_cours) as cours_name,
                                a.id_teacher,
                                a.start_hour as start_hour,
                                a.end_hour as end_hour,
                                a.id_cours, (select id from classroom where id = a.id_class) as classroom_id,
                                round((TIME_TO_SEC(a.end_hour) / 3600) - (TIME_TO_SEC(a.start_hour) / 3600), 2) AS total_hour,
                                -- now() as register_date,
                                -- substr(upper(dayname(now())),1,2) as register_day,
                                a.academic_year
                                    FROM schedule a
								        left join teacher b on b.id = a.Id_teacher
								        left join cours c on c.id = a.Id_cours
                                WHERE 1=1
                                and b.status = 1 
                                and a.Id_teacher = @teacherCode
                                and a.academic_year = @academicYear";

        if (s.cours_id != 0)
        {
            sql += @" and a.id_cours = " + s.cours_id + "";
        }
        if (s.class_id != 0)
        {
            sql += @" and a.id_class = " + s.class_id + "";
        }

        sql += @" order by field(a.days, 'MO', 'TU', 'WE', 'TH', 'FR', 'SA', 'SU')";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.teacher_id, s.academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> getListScheduleTeacherForTimesheet_bk(Schedule s)
    {
        string sql = @"SELECT
                            a.id as id_schedule,
                            (select name from classroom where id = a.id_class) as class_name,
                            a.days,
                            (select name from cours where id = a.id_cours) as cours_name,
                            a.id_teacher,
                            a.start_hour as start_hour,
                            a.end_hour as end_hour,
                            a.id_cours,
                            concat(time_format(TIMEDIFF(a.end_hour,a.start_hour),'%H'),'h ',
                                   time_format(TIMEDIFF(a.end_hour,a.start_hour),'%i'),' mn') AS total_hour,
                            (SELECT PRESENCE_STATUS FROM TIMESHEETS WHERE STAFF_CODE = a.Id_teacher
	                            AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') =  @register_date_timesheet
                                AND time_in = a.start_hour
                                AND time_out = a.end_hour
                                AND id_course = a.id_cours
                                AND id_class = a.id_class ) AS PRESENCE_STATUS,
			                            (SELECT VALIDATION_STATUS FROM TIMESHEETS WHERE STAFF_CODE = a.Id_teacher
			                                AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') =  @register_date_timesheet
                                            AND time_in = a.start_hour
                                            AND time_out = a.end_hour
                                            AND id_course = a.id_cours
                                            AND id_class = a.id_class) AS VALIDATION_STATUS,
					                            (SELECT CASE WHEN trim(absence_reason) = '' THEN ''
					                                ELSE absence_reason end as absence_reason
					                                FROM TIMESHEETS WHERE STAFF_CODE = a.Id_teacher
					                                AND DATE_FORMAT(DATE_REGISTER ,'%Y%m%d') =  ?
                                                    AND time_in = a.start_hour
                                                    AND time_out = a.end_hour
                                                    AND id_course = a.id_cours
                                                    AND id_class = a.id_class) AS absence_reason
                            FROM schedule a, teacher b, cours c
                            WHERE a.Id_teacher = ?
                            and a.id_teacher = b.id
                            and upper(a.days) = ?
                            and b.status = 1 -- check only active teacher in system
                            and a.id_cours = c.id
                            and c.status = 1 -- check only active course in system";

        //	   	-- and a.days = @days     ORDER by start_hour asc

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.register_date_timesheet.ToString("yyyyMMdd"), s.days.ToUpper(), s.teacher_id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> SearchTeacherTimesheet(Schedule s)
    {
        string sql = @"SELECT a.*,a.date_register as register_date,b.start_hour as start_hour, b.end_hour as end_hour,
                            (SELECT name from classroom where id = a.id_class) as class_name,
                            (SELECT name from cours where id = a.id_course) as cours_name,
                            (SELECT case when id = 1 then '' else description end as description
                              from reason_type where id = a.absence_reason) as absence_reason_definition,
                            case when a.presence_status = 0 then 'Present' else 'Absent' end as presence_status_definition
                            FROM timesheets a , schedule b
                            WHERE a.staff_code = ?
                                AND a.staff_code = b.id_teacher
                                AND date_format(a.date_register,'%Y%m%d') >= ?
                                AND date_format(a.date_register,'%Y%m%d') <= ?
                            GROUP by a.date_register, a.id_course";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.teacher_id, s.from_date.ToString("yyyyMMdd"),
                                s.to_date.ToString("yyyyMMdd"));
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Schedule> GetListPreviousScheduleConfiguation(Schedule s)
    {
        string sql = @" select id, concat(extract(YEAR from start_date),' - ',extract(YEAR from end_date)) as years
                                    from academic_year where id 
                                        in(select distinct(academic_year) from `schedule` 
                                                 where Id_class = ? and vacation = ?) order by id asc";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.code_classroom, s.vacation);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void ChangeScheduleFromPrevious(Schedule sch)
    {

        string sql = @"insert into schedule (id_class, days, id_cours, Id_teacher, start_hour, end_hour, Transaction_date, vacation, academic_year)
                               select id_class, days, id_cours, Id_teacher, start_hour, end_hour, Transaction_date, vacation, ?
                                     from schedule where Id_class = ? and vacation = ? and academic_year = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sch.academic_year_new, sch.code_classroom,
                                sch.vacation, sch.academic_year_old);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}