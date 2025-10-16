using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using System.Data;
using Db_Core;


public class Timesheet
{
    public int id { get; set; }
    public string description { get; set; }
    public string staff_code { get; set; }
    public TimeSpan time_in { get; set; }
    public TimeSpan time_out { get; set; }
    public double total_work_hour { get; set; }
    public int cours_id { get; set; }
    public int class_id { get; set; }
    public string vacation { get; set; }
    public string vacation_name { get; set; }
    public string class_name { get; set; }
    public string fullname { get; set; }
    public int presence_status { get; set; }
    public string presence_status_def { get; set; }
    public int absence_reason_id { get; set; }
    public string absence_reason { get; set; }
    public string week_day { get; set; }
    public string student_code { get; set; }
    public int validation_status { get; set; }
    public int academic_year_id { get; set; }
    public DateTime date_register { get; set; }
    public DateTime work_date { get; set; }
    public int login_user_id { get; set; }
    public DateTime sheet_date { get; set; }
    public int check_in { get; set; }
    public int check_out { get; set; }
    public string day_tag { get; set; }
    public TimeSpan entry_hour { get; set; }
    public TimeSpan exit_hour { get; set; }



    public enum STATUS
    {
        VALID = 1,
        NOT_VALID = -1
    }

    public enum PRESENCE_STATE
    {
        PRESENT = 1,
        ABSENT = -1
    }



    public static List<Timesheet> Parse(MySqlDataReader reader)
    {
        List<Timesheet> result = new List<Timesheet>();
        try
        {
            while (reader.Read())
            {
                Timesheet timesheet = new Timesheet();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try
                        {
                            timesheet.id = Int32.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        timesheet.description = reader.GetValue(i).ToString();
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try
                        {
                            timesheet.staff_code = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TIME_IN")
                    {
                        try
                        {
                            timesheet.time_in = TimeSpan.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TIME_OUT")
                    {
                        try
                        {
                            timesheet.time_out = TimeSpan.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_WORK_HOUR")
                    {
                        try
                        {
                            timesheet.total_work_hour = double.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS_ID")
                    {
                        try
                        {
                            timesheet.cours_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_ID")
                    {
                        try
                        {
                            timesheet.class_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            timesheet.vacation = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try
                        {
                            timesheet.vacation = reader.GetValue(i).ToString();

                            switch (timesheet.vacation)
                            {
                                case "AM": timesheet.vacation_name = "Matin"; break;
                                case "PM": timesheet.vacation_name = "Median"; break;
                                case "NG": timesheet.vacation_name = "Soir"; break;
                                case "WK": timesheet.vacation_name = "Weekend"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASS_NAME")
                    {
                        try
                        {
                            timesheet.class_name = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try
                        {
                            timesheet.fullname = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS")
                    {
                        try
                        {
                            timesheet.presence_status = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRESENCE_STATUS")
                    {
                        try
                        {
                            int pStatus = int.Parse(reader.GetValue(i).ToString());

                            if (pStatus <= 0)
                            {
                                timesheet.presence_status_def = "Absent";
                            }
                            else
                            {
                                timesheet.presence_status_def = "Présent";
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON_ID")
                    {
                        try
                        {
                            timesheet.absence_reason_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ABSENCE_REASON")
                    {
                        try
                        {
                            timesheet.absence_reason = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "WEEK_DAY")
                    {
                        try
                        {
                            timesheet.week_day = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_CODE")
                    {
                        try
                        {
                            timesheet.student_code = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VALIDATION_STATUS")
                    {
                        try
                        {
                            timesheet.validation_status = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try
                        {
                            timesheet.academic_year_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try
                        {
                            timesheet.date_register = DateTime.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "WORK_DATE")
                    {
                        try
                        {
                            timesheet.work_date = DateTime.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try
                        {
                            timesheet.login_user_id = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SHEET_DATE")
                    {
                        try
                        {
                            timesheet.sheet_date = DateTime.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CHECK_IN")
                    {
                        try
                        {
                            timesheet.check_in = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CHECK_OUT")
                    {
                        try
                        {
                            timesheet.check_out = int.Parse(reader.GetValue(i).ToString());
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DAY_TAG")
                    {
                        try
                        {
                            timesheet.day_tag = reader.GetValue(i).ToString();
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ENTRY_HOUR")
                    {
                        try { timesheet.entry_hour = TimeSpan.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EXIT_HOUR")
                    {
                        try { timesheet.exit_hour = TimeSpan.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                }
                result.Add(timesheet);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return result;
    }

    public static void InsertStudentTimesheets(List<Timesheet> tList)
    {

        try
        {
            if (tList != null && tList.Count > 0)
            {
                // remove previous timesheet
                Timesheet _t = tList[0];
                RemoveOldStudentTimesheet(_t);


                // add new timesheet
                foreach (Timesheet t in tList)
                {
                    string sql = @"INSERT INTO student_timesheets 
                                       (student_code,
                                        class_id,
                                        vacation,
                                        presence_status,
                                        absence_reason_id,
                                        sheet_date,
                                        validation_status,
                                        date_register,
                                        login_user_id)
                                VALUES (?, -- student_code
                                        ?,  -- class_id
                                        ?,  -- vacation
                                        ?,  -- presence_status
                                        ?,  -- absence_reason_id
                                        ?,  -- sheet_date
                                        ?,  -- validation_status
                                        now(),
                                        ?   -- login_user_id
                                        )";


                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(t.student_code,
                                        t.class_id,
                                        t.vacation,
                                        t.presence_status,
                                        t.absence_reason_id,
                                        t.sheet_date.ToString("yyyyMMdd"),
                                        t.validation_status,
                                        t.login_user_id);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveOldStudentTimesheet(Timesheet t)
    {
        try
        {
            string sql = @"Delete from student_timesheets 
                        where class_id = ?
                        and vacation = ?
                        and date_format(sheet_date, '%Y%m%d') = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.class_id, t.vacation, t.sheet_date.ToString("yyyyMMdd"));
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /*
    public static void InsertTimesheets(Timesheet t)
    {
        string sql = @"INSERT INTO timesheets (staff_code, time_in, time_out, total_work_hour,
                               cours_id, vacation, class_id, presence_status, absence_reason, 
                                week_day, validation_status, academic_year_id, date_register, 
                                  login_user_id, sheet_date)
                                VALUES (?, -- staff_code
                                        ?, -- time_in
                                        ?, -- time_out
                                        ?, -- total_work_hour
                                        ?, -- cours_id
                                        ?,  -- vacation
                                        ?,  -- class_id
                                        ?,  -- presence_status
                                        ?,  -- absence_reason
                                        ?,  -- week_day
                                        ?,  -- validation_status
                                        ?,  -- academic_year_id
                                        now(),
                                        ?,   -- login_user_id
                                        ?   -- sheet_date
                                     )";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.staff_code.ToUpper(),
                                t.time_in,
                                t.time_out,
                                t.total_work_hour,
                                t.cours_id,
                                t.vacation,
                                t.class_id,
                                t.presence_status,
                                t.absence_reason_id,
                                t.week_day,
                                t.validation_status,
                                t.academic_year_id,
                                t.login_user_id,
                                t.sheet_date);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    */
    public static bool timesheetsExist(Timesheet timesheet)
    {
        bool result = false;

        string sql = @"select count(*) from timesheets
                            WHERE 1=1";

        if (timesheet.staff_code != null)
        {
            sql += @" and staff_code = '" + timesheet.staff_code.ToUpper() + "'";
        }
        if (timesheet.sheet_date != null)
        {
            sql += @" and date_format(sheet_date, '%Y%m%d') = '" + timesheet.sheet_date.ToString("yyyyMMdd") + "'";
        }

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
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

    public static bool timesheetsExistForTeacher(Timesheet timesheet)
    {
        // to be reviewed ......
        bool result = false;

        string sql = @"select count(*) from timesheets
                            WHERE staff_code = @staff_code
                            AND date_format(date_register, '%Y%m%d') = @date_register
                            AND academic_year_id = @academic_year_id
                            AND cours_id = @cours_id
                            AND class_id = @class_id
                            AND time_in = @time_in
                            AND time_out = @time_out";


        /*

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        MySqlDataReader reader = null;
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@staff_code", timesheet.staff_code.ToUpper());
            cmd.Parameters.AddWithValue("@date_register", timesheet.date_register.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@academic_year_id", timesheet.academic_year_id);
            cmd.Parameters.AddWithValue("@cours_id", timesheet.cours_id);
            cmd.Parameters.AddWithValue("@class_id", timesheet.class_id);
            cmd.Parameters.AddWithValue("@time_in", timesheet.time_in);
            cmd.Parameters.AddWithValue("@time_out", timesheet.time_out);
            //
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
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

        */

        return result;
    }

    public static bool timesheetsExistForCours(Timesheet timesheet)
    {

        // to be reviewed ......
        bool result = false;

        string sql = @"select count(*) from timesheets
                            WHERE staff_code = @staff_code
                            AND date_format(sheet_date, '%Y%m%d') = @sheet_date
                            AND academic_year_id = @academic_year_id
                            AND cours_id = @cours_id
                            AND class_id = @class_id
                            AND time_in = @time_in
                            AND time_out = @time_out";


        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        MySqlDataReader reader = null;
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@staff_code", timesheet.staff_code.ToUpper());
            cmd.Parameters.AddWithValue("@sheet_date", timesheet.sheet_date.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@academic_year_id", timesheet.academic_year_id);
            cmd.Parameters.AddWithValue("@cours_id", timesheet.cours_id);
            cmd.Parameters.AddWithValue("@class_id", timesheet.class_id);
            cmd.Parameters.AddWithValue("@time_in", timesheet.time_in);
            cmd.Parameters.AddWithValue("@time_out", timesheet.time_out);
            //
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
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
        */
        return result;
    }

    public static void updateTimesheets(Timesheet t)
    {
        string sql = @"update timesheets set presence_status = @presence_status,
                                absence_reason = @absence_reason,
                                validation_status = @validation_status
                             WHERE 1=1";

        if (t.staff_code != null)
        {
            sql += @" and staff_code = '" + t.staff_code.ToUpper() + "'";
        }
        if (t.sheet_date != null)
        {
            sql += @" AND date_format(sheet_date, '%Y%m%d') ='" + t.sheet_date.ToString("yyyyMMdd") + "'";
        }
        if (t.class_id != 0)
        {
            sql += @" and class_id = " + t.class_id + "";
        }
        if (t.cours_id != 0)
        {
            sql += @" and cours_id = " + t.cours_id + "";
        }
        if (t.academic_year_id != 0)
        {
            sql += @" and academic_year_id = " + t.academic_year_id + "";
        }


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(t.presence_status, t.absence_reason_id, t.validation_status);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteTimesheets(Timesheet timesheet)
    {
        // to be reviewed ...

        /*
        string sql = @"INSERT INTO timesheets (staff_code, time_in, time_out, total_work_hour, 
                            cours_id, vacation, class_id, presence_status, absence_reason, 
                            week_day, validation_status, academic_year_id, date_register, login_user_id)
                                VALUES (
                                        @staff_code, -- staff_code
                                        @time_in, -- time_in
                                        @time_out, -- time_out
                                        @total_work_hour,  -- total_work_hour
                                        @cours_id, -- cours_id
                                        @vacation,  -- vacation
                                        @class_id,  -- class_id
                                        @presence_status,  -- presence_status
                                        @absence_reason,  -- absence_reason
                                        @week_day,  -- week_day
                                        @validation_status,  -- validation_status
                                        @academic_year_id,  -- academic_year_id
                                        now(),  -- date_register
                                        @login_user_id   -- login_user_id
                                     )";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@staff_code", timesheet.staff_code.ToUpper());
            cmd.Parameters.AddWithValue("@time_in", timesheet.time_in);
            cmd.Parameters.AddWithValue("@time_out", timesheet.time_out);
            cmd.Parameters.AddWithValue("@total_work_hour", timesheet.total_work_hour);
            cmd.Parameters.AddWithValue("@cours_id", timesheet.cours_id);
            cmd.Parameters.AddWithValue("@vacation", timesheet.vacation);
            cmd.Parameters.AddWithValue("@class_id", timesheet.class_id);
            cmd.Parameters.AddWithValue("@presence_status", timesheet.presence_status);
            cmd.Parameters.AddWithValue("@absence_reason", timesheet.absence_reason);
            cmd.Parameters.AddWithValue("@week_day", timesheet.week_day);
            cmd.Parameters.AddWithValue("@validation_status", timesheet.validation_status);
            cmd.Parameters.AddWithValue("@academic_year_id", timesheet.academic_year_id);
            //cmd.Parameters.AddWithValue("@date_register", listTimesheet[i].date_register);
            cmd.Parameters.AddWithValue("@login_user_id", timesheet.login_user_id);
            //
            cmd.ExecuteNonQuery();

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
        */
    }

    //public static void validateTimesheets(Timesheet t)
    //{
    //    string sql = @"UPDATE timesheets set validation_status = 1
    //                        WHERE upper(staff_code) = upper(?)
    //                        AND academic_year_id = ?
    //                        AND date_format(date_register ,'%Y%m%d') = date_format(?,'%Y%m%d')";

    //    try
    //    {
    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(t.staff_code.ToUpper(), t.academic_year_id, t.date_register);
    //        stmt.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static void validateTimesheetsTeacher(Timesheet timesheet)
    {
        // to be reviewed ...

        /*
        string sql = @"UPDATE timesheets set validation_status = 1
                            WHERE upper(staff_code) = upper(@staff_code)
                            AND academic_year_id = @academic_year_id
                            AND date_format(date_register ,'%Y%m%d') = date_format(@date_register,'%Y%m%d')
                            AND cours_id = @cours_id
                            AND vacation = @vacation
                            AND class_id = @class_id
                            AND time_in = @time_in
                            AND time_out = @time_out";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@staff_code", timesheet.staff_code.ToUpper());
            cmd.Parameters.AddWithValue("@date_register", timesheet.date_register);
            cmd.Parameters.AddWithValue("@academic_year_id", timesheet.academic_year_id);
            cmd.Parameters.AddWithValue("@cours_id", timesheet.cours_id);
            cmd.Parameters.AddWithValue("@vacation", timesheet.vacation);
            cmd.Parameters.AddWithValue("@class_id", timesheet.class_id);
            cmd.Parameters.AddWithValue("@time_in", timesheet.time_in);
            cmd.Parameters.AddWithValue("@time_out", timesheet.time_out);
            //
            cmd.ExecuteNonQuery();

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
        */
    }

    public static List<Timesheet> getListAllReasonWithoutUndefined()
    {
        string sql = @"select a.id, a.description
                            from reason_type a 
                             where a.id not in (1)
                            ORDER by a.description asc";

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

    public static List<Timesheet> getListAllReasonForCombo()
    {
        string sql = @"select a.id, a.description
                            from reason_type a 
                            ORDER by a.description asc";
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

    public static List<Timesheet> getListReasonById(int id)
    {
        string sql = @"select a.id, a.description
                            from reason_type a 
                            where a.id = ?
                            ORDER by a.description asc";

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

    public static bool reasonExist(string description)
    {
        bool result = false;

        string sql = @"select count(*) from reason_type where upper(description) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description);
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

    public static bool reasonTypeExistInTimesheets(int reason_id)
    {
        bool result = false;

        string sql = @"select count(*) from timesheets where absence_reason = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(reason_id);
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

    public static void addReason(string description)
    {
        string sql = @"INSERT INTO REASON_TYPE(description) VALUES(?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addReason(int id, string description)
    {
        // to be reviewed ...

        /*
        string sql = @"INSERT INTO REASON_TYPE(id,description) VALUES(@id,@Description)";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;

        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Description", description.ToUpper());
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                try
                {
                    con.Close();
                    con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch { }
            }
        }
        */
    }

    public static void deleteReasonById(int reason_id)
    {
        string sql = @"delete from reason_type where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(reason_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static List<Timesheet> getListAffectedTimesheets()
    {
        try
        {
            string sql = @"select a.*, c.name as class_name,  c.id as classroom_id 
                                from student_timesheets a
                             left join classroom c on c.id = a.class_id
                            where 1 = 1 ";

            //if (staffCode != null)
            //{
            //    sql += @" and a.staff_code =  '" + staffCode + "' ";
            //}
            //if (sheetDate != null)
            //{
            //    sql += @" and DATE_FORMAT(a.sheet_date ,'%Y%m%d') =  '" + sheetDate + "' ";
            //}
            //if (presenceStatus > -1)
            //{
            //    sql += @" and a.presence_status =  " + presenceStatus + " ";
            //}

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Timesheet> getListTimesheetsStudent(
        int classId, string vacation, DateTime sheetDate)
    {
        try
        {
            string sql = @"SELECT a.id as student_code, 
                           concat(upper(a.first_name),' ', upper(a.last_name)) as fullname,
                                c.id as class_id, c.name as class_name, b.vacation,
                                d.presence_status, d.absence_reason_id, d.validation_status
                            FROM STUDENT a
                                inner join classroom_staff_management b on b.staff_code = a.Id
                                inner join classroom c on c.id = b.class_id
								left join student_timesheets d on d.student_code = a.id 
										and date_format(d.sheet_date, '%Y%m%d') = ?
								where a.status = 1
								and c.id = ?
								and b.vacation = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sheetDate.ToString("yyyyMMdd"), classId, vacation);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Timesheet> getListStudentTimesheetsIndividualReport(
        string studentCode, DateTime fromDate, DateTime toDate)
    {
        try
        {
            string sql = @"SELECT a.id as student_code, 
                                concat(upper(a.first_name),' ', upper(a.last_name)) as fullname,
                                c.id as class_id, c.name as class_name, b.vacation,
                                d.presence_status, 
								rt.description as absence_reason,
								d.sheet_date
                            FROM STUDENT a
                                inner join classroom_staff_management b on b.staff_code = a.Id
                                inner join classroom c on c.id = b.class_id
								inner join student_timesheets d on d.student_code = a.id 
								inner join reason_type rt on rt.id = d.absence_reason_id
								where a.status = 1
                                and a.id = ?
                                and date_format(d.sheet_date, '%Y%m%d') >= ?
                                and date_format(d.sheet_date, '%Y%m%d') <= ? ";



            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(studentCode,
                                fromDate.ToString("yyyyMMdd"),
                                toDate.ToString("yyyyMMdd"));
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Timesheet> getListStudentTimesheetsGroupReport(
        int classId, string vacation, DateTime fromDate, DateTime toDate)
    {
        try
        {
            string sql = @"SELECT a.id as student_code, 
                                concat(upper(a.first_name),' ', upper(a.last_name)) as fullname,
                                c.id as class_id, c.name as class_name, b.vacation,
                                d.presence_status, 
																rt.description as absence_reason,
																d.sheet_date
                            FROM STUDENT a
                                inner join classroom_staff_management b on b.staff_code = a.Id
                                inner join classroom c on c.id = b.class_id
								inner join student_timesheets d on d.student_code = a.id 
								inner join reason_type rt on rt.id = d.absence_reason_id
								where a.status = 1
								and c.id = ?
								and b.vacation = ?														
								and date_format(d.sheet_date, '%Y%m%d') >= ?
								and date_format(d.sheet_date, '%Y%m%d') <= ?
								order by d.sheet_date";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classId,
                                vacation,
                                fromDate.ToString("yyyyMMdd"),
                                toDate.ToString("yyyyMMdd"));
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Timesheet> getListAffectedTimesheetsForCourses(Schedule sheets)
    {
        string sql = @"select a.*, c.name as class_name,  c.id as classroom_id from timesheets a
                             left join classroom c on c.id = a.class_id
                            where 1 = 1 
                                and a.staff_code = ?
                                and a.cours_id = ?
                                and a.class_id = ?
                                and a.academic_year_id = ?
                                and DATE_FORMAT(a.sheet_date ,'%Y%m%d') = ?";

        string _newSheetDate = sheets.sheet_date.ToString("yyyyMMdd");

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sheets.teacher_id, sheets.cours_id, sheets.class_id,
                                sheets.academic_year, _newSheetDate);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteTimeSheetsByCodeAndDate(string code, DateTime timesheetDate)
    {
        string sql = @"delete from timesheets where staff_code = ? 
                            and DATE_FORMAT(sheet_date ,'%Y%m%d') = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(code, timesheetDate.ToString("yyyyMMdd"));
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Timesheet> GetListTimesheetsForStaff(List<String> listStaffCode,
        string firstWeekDay, string lastWeekDay)
    {
        try
        {
            int cnt = 1;

            String sql = @"select * from staff_timesheets 
                            where work_date between ? and ? ";
            sql += @"and staff_code in(";
            foreach (string code in listStaffCode)
            {
                if (cnt < listStaffCode.Count)
                {

                    sql += @"'" + code + "',";
                }
                else
                {
                    sql += @"'" + code + "'";
                }
                cnt++;
            }
            sql += @")";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(firstWeekDay, lastWeekDay);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void InsertTimesheetStaff(List<Timesheet> listTimesheet,
        string firstWeekDay, string lastWeekDay, List<String> listStaffCode)
    {
        try
        {
            /********** remove previous timesheet for one week   **********/
            int cnt = 1;

            String sql1 = @"delete from timesheets 
                                where work_date between ? and ? ";
            sql1 += @"and staff_code in(";
            foreach (string code in listStaffCode)
            {
                if (cnt < listStaffCode.Count)
                {

                    sql1 += @"'" + code + "',";
                }
                else
                {
                    sql1 += @"'" + code + "'";
                }
                cnt++;
            }
            sql1 += @")";


            SqlStatement stmt1 = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
            stmt1.SetParameters(firstWeekDay, lastWeekDay);
            stmt1.ExecuteNonQuery();




            /********** insert timesheet for one week   **********/
            foreach (Timesheet t in listTimesheet)
            {
                // add new timesheet
                String sql2 = @"INSERT INTO timesheets
                            (staff_code, work_date, entry_hour, check_in, exit_hour,
                                check_out, login_user, transaction_date, day_tag)
                            VALUES(?, -- staff_code
                                    ?, -- work_date
                                    ?, -- entry_hour
                                    ?, -- check_in
                                    ?, -- exit_hour
                                    ?, -- check_out
                                    ?, -- login_user
                                    NOW(),  -- transaction_date
                                    ?  -- day_tag
                                    )";

                SqlStatement stmt2 = SqlStatement.FromString(sql2, SqlConnString.CSM_APP);


                // check for valid hour
                string entryHour = t.entry_hour == TimeSpan.MinValue ? null : t.entry_hour.ToString(@"hh\:mm\:ss\.ff");
                string exitHour = t.exit_hour == TimeSpan.MinValue ? null : t.exit_hour.ToString(@"hh\:mm\:ss\.ff");

                stmt2.SetParameters(t.staff_code,
                                    t.work_date.ToString("yyyy/MM/dd"),
                                    entryHour,
                                    t.check_in,
                                    exitHour,
                                    t.check_out,
                                    t.login_user_id,
                                    t.day_tag);


                stmt2.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}