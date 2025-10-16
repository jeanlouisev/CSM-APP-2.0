using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Settings
{
    public int id { get; set; }
    public string name { get; set; }
    public DateTime transaction_date { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public string fullname { get; set; }
    public string group_name { get; set; }
    public string login_user { get; set; }
    public string years { get; set; }


    public static List<Settings> Parse(MySqlDataReader reader)
    {
        List<Settings> listSettings = new List<Settings>();
        try
        {
            while (reader.Read())
            {
                Settings Setting = new Settings();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { Setting.id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NAME")
                    {
                        try { Setting.name = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TRANSACTION_DATE")
                    {
                        try { Setting.transaction_date = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "START_DATE")
                    {
                        try { Setting.start_date = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "END_DATE")
                    {
                        try { Setting.end_date = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { Setting.fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "GROUP_NAME")
                    {
                        try { Setting.group_name = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { Setting.login_user = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { Setting.years = reader.GetString(i); } catch { }
                    }

                }
                listSettings.Add(Setting);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listSettings;
    }

    public static void addAcademicYear(Settings p)
    {
        string sql = @"INSERT INTO academic_year(start_date,end_date,transaction_date, login_user)
                                VALUES(?,?,now(),?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.start_date.ToString("yyyyMMdd"), p.end_date.ToString("yyyyMMdd"), p.login_user);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void UpgradeToAllPreviousConfiguration(int oldMaxAcademicYear, int NewMaxAcademicYear, string loginUser)
    {
        string sql = @"call load_previous_configuration_proc(?,?,?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(oldMaxAcademicYear, NewMaxAcademicYear, loginUser);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Settings> getListAcademicYear()
    {
        string sql = @"SELECT  a.*, (select concat(first_name,' ',last_name) from staff where id = a.login_user) as user_fullname,
                             concat(EXTRACT(year FROM a.start_date),'-',EXTRACT(year FROM a.end_date)) as years
                                FROM academic_year a  ORDER by a.start_date asc ";
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

    public static Settings getCurrentAcademicYear()
    {
        string sql = @"select *, concat(EXTRACT(year FROM start_date),'-',EXTRACT(year FROM end_date)) as years
                        from academic_year where id = (select max(id) from academic_year)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteAcademicYear(int id)
    {
        string sql = @"call clear_system_by_academic_year_proc(?)";

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

    public static bool checkExistedRole(string role_name)
    {
        bool result = false;

        string sql = @"select count(*) from user_role where upper(name) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(role_name.ToUpper());
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

    public static List<Settings> getAcademicYearFull()
    {
        string sql = @"select id,
                        concat(extract(YEAR from start_date),' - ',extract(YEAR from end_date)) as years from academic_year 
                       order by start_date desc";
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

    public static int getAcademicYear()
    {
        int result = 0;
        string sql = @"select max(id) from academic_year";

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

    public static List<Settings> getAcademicYearForStudent(string id)
    {

        string sql = @"select concat(extract(YEAR from start_date),' - ',extract(YEAR from end_date)) as years 
                            from academic_year a,classroom_staff_management b
                            where a.id in (b.academic_year) and b.staff_code = ?  order by start_date desc";
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

    public static int getAcademicYear(int academicYearStart, int academicYearEnd)
    {
        int result = 0;

        string sql = @"select id from academic_year
                            where extract(year from start_date) = ?
                            and extract(year from end_date) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(academicYearStart.ToString("yyyyMMdd"), academicYearEnd.ToString("yyyyMMdd"));
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

    public static List<Settings> getListAcademicYearStart()
    {
        string sql = @"select id, extract(YEAR FROM start_date) as start_date
                            from academic_year
                             order by start_date asc";

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

    public static List<Settings> getListAcademicYearEnd()
    {
        string sql = @"select id, extract(YEAR FROM end_date) as end_date
                            from academic_year
                            order by end_date asc";
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

    public static bool academicYearAlreadyExist(int startYear, int endYear)
    {
        bool result = false;

        string sql = @"select * from academic_year where EXTRACT(year FROM start_date) = ?
                                    and EXTRACT(year FROM end_date) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(startYear, endYear);
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

}