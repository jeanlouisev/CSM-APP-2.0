using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Telerik.Web.UI;
using Db_Core;
using System.Data;




public class Users
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public int role_id { get; set; }
    public DateTime transaction_date { get; set; }
    public int locked { get; set; }
    public DateTime locked_time { get; set; }
    public int log_error { get; set; }
    public DateTime expiry_date { get; set; }
    public string fullname { get; set; }
    public string login_user { get; set; }
    public string staff_code { get; set; }
    public string group_name { get; set; }
    public string phone { get; set; }
    public int view_access { get; set; }
    public int edit_access { get; set; }
    public int delete_access { get; set; }
    public string image_path { get; set; }
    public string url_path { get; set; }
    public DateTime log_time { get; set; }
    public string log_status { get; set; }
    public DateTime from_date { get; set; }
    public DateTime to_date { get; set; }
    public string name { get; set; }
    public int menu_id { get; set; }





    public enum ACCOUNT_LOCK_STATUS
    {
        LOCKED = 1,
        UNLOCKED = 0
    }

    public enum ACCESS
    {
        YES = 1,
        NO = 0
    }

    public enum MENU
    {
        STUDENT = 1,
        CLASSROOM = 2,
        SECRETARIAT = 3,
        HR = 4,
        ECONOMAT = 5,
        SETTINGS = 12
    }


    public static List<Users> Parse(MySqlDataReader reader)
    {
        List<Users> listUsers = new List<Users>();
        try
        {
            while (reader.Read())
            {
                Users user = new Users();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { user.id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "USERNAME")
                    {
                        try { user.username = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PASSWORD")
                    {
                        try { user.password = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ROLE_ID")
                    {
                        try { user.role_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TRANSACTION_DATE")
                    {
                        try { user.transaction_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOCKED")
                    {
                        try { user.locked = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOCKED_TIME")
                    {
                        try { user.locked_time = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOG_ERROR")
                    {
                        try { user.log_error = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EXPIRY_DATE")
                    {
                        try { user.expiry_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { user.fullname = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { user.login_user = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { user.staff_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "GROUP_NAME")
                    {
                        try { user.group_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE")
                    {
                        try { user.phone = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VIEW_ACCESS")
                    {
                        try { user.view_access = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EDIT_ACCESS")
                    {
                        try { user.edit_access = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DELETE_ACCESS")
                    {
                        try { user.delete_access = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { user.image_path = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOG_STATUS")
                    {
                        try { user.log_status = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "URL_PATH")
                    {
                        try { user.url_path = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOG_TIME")
                    {
                        try { user.log_time = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { user.from_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { user.to_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NAME")
                    {
                        try { user.name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MENU_ID")
                    {
                        try { user.menu_id = reader.GetInt32(i); }
                        catch { }
                    }
                }
                listUsers.Add(user);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
        return listUsers;
    }

    public static void addUser(Users user)
    {
        string sql = @"INSERT INTO users(username,password,role_id,locked,expiry_date,transaction_date)
                                VALUES(?, -- username,
                                        ?, -- password,
                                        ?, -- role_id,
                                        ?, -- locked, 
                                        ?, -- expiry_date,
                                        now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(user.username,
                                user.password,
                                user.role_id,
                                user.locked,
                                user.expiry_date.ToString("yyyyMMdd"));
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateUser(Users user)
    {
        string sql = @"UPDATE users SET 
                            username            = ?,
                            password            = ?,
                            role_id             = ?,
                            transaction_date    = now(),
                            locked              = ?,
                            locked_time         = ?,
                            expiry_date         = ?";

        // case when account is locked
        if (user.locked == 0)
        {
            sql += @" , log_error = null ";
        }

        sql += " WHERE upper(username) = ?";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(user.username,
                                user.password,
                                user.role_id,
                                user.locked,
                                user.locked_time,
                                user.expiry_date);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void increaseLogError(int logErrorCounter, string pseudo)
    {
        string sql = @"UPDATE users SET log_error = ? ";

        // condition to lock account
        if (logErrorCounter >= 5)
        {
            sql += @" , locked = 1 , locked_time = now()";
        }

        sql += @" where upper(username) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(logErrorCounter, pseudo);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUsers()
    {
        string sql = @"select a.*, concat(b.Last_name,' ',b.First_name) as fullname,
                         (select concat(b.Last_name,' ',b.First_name) 
                             from staff where upper(id) = upper(a.requester_code)) as requester_fullname,
                            (select upper(username) from users where upper(staff_code) = upper(a.requester_code)) as login_user,
                            upper(c.name) as group_name
                            from users a, staff b, role c
                            where a.staff_code = b.Id
                            and a.role_id = c.id";
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

    public static List<Users> GetListUserAll(Users user)
    {
        try
        {
            String sql = @"select concat(s.FIRST_NAME,' ',s.LAST_NAME) as fullname, u.*
                                from users u
                                inner join staff s on s.id = u.username
                                where 1=1
                                [ and u.username = ? ]";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (user.username != null)
            {
                stmt.SetParameter(0, user.username);
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUsersById(int id)
    {
        string sql = @"select a.*, concat(b.Last_name,' ',b.First_name) as fullname,b.Phone1 as phone,
                         (select concat(b.Last_name,' ',b.First_name) 
                             from staff where upper(id) = upper(a.requester_code)) as requester_fullname,
                            (select upper(username) from users where upper(staff_code) = upper(a.requester_code)) as login_user,
                            upper(c.name) as group_name
                            from users a, staff b, role c
                            where a.staff_code = b.Id 
                            and a.role_id = c.id
                            and a.id = ?";

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

    public static List<Users> getListUsersByPseudo(string pseudo)
    {
        try
        {
            string sql = @"select a.*, st.*,
                            upper(r.name) as group_name
                            from users a
							inner join staff st on st.id = a.username
							inner join user_role r on r.id = a.role_id														
							where a.username = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(pseudo);
            return Parse(stmt.ExecuteReader());

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUsersByCode(string userCode)
    {
        string sql = @"select * from users where upper(username) = upper(?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(userCode);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListRequesterByCode(string requester_code)
    {
        string sql = @"select * from users where upper(staff_code) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(requester_code);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool checkExistedUser(string staff_code)
    {
        bool result = false;

        string sql = @"select count(*) from users where upper(staff_code) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staff_code.ToUpper());
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

    public static bool checkExistedUserName(string username)
    {
        bool result = false;

        string sql = @"select count(*) from users where upper(username) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(username.ToUpper());
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

    public static bool checkExistedUserNameWithStaffCode(string username, string staff_code)
    {
        bool result = false;

        string sql = @"select count(*) from users where upper(username) = ?
                               and upper(staff_code) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(username.ToUpper(), staff_code.ToUpper());
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

    public static bool checkUsers(Users user)
    {
        bool result = false;

        string sql = @"select count(*) from users where username = ? and password= ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(user.username.ToUpper(), user.password);
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

    public static void deleteUsers(int id)
    {
        string sql = @"delete from users where id = ?";

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

    public static void unlockUserAccount(string pseudo)
    {
        string sql = @"UPDATE users SET 
                            locked              = 0 ,
                            locked_time         = null ,
                            log_error           = null
                            where username       = ? ";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(pseudo);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUserAccessLevel(int role_id, int menu_id)
    {
        string sql = @"select a.* from user_access_level a 
                            where a.role_id = ? and menu_id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(role_id, menu_id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void writeUserLogs(Users user)
    {
        string sql = @"INSERT INTO user_logs(login_user_id, url_path, log_time, log_status)
                                VALUES(?, ?, now(), ?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(user.id, user.url_path, user.log_status);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUserLogs(Users user)
    {
        string sql = @"select a.login_user_id, b.id as staff_code, concat(b.First_name,' ', b.Last_name) as fullname, a.log_status, a.log_time 
                        from user_logs a
                        join staff b on b.id = a.staff_code
                        where 1=1 
                        [ and a.staff_code = ? ]  -- 0
                        [ and DATE_FORMAT(a.log_time, '%Y%m%d') >= ? ] -- 1
                        [ and DATE_FORMAT(a.log_time, '%Y%m%d') <= ? ] -- 2
                        order by log_time desc";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            //
            if (user.staff_code != null)
            {
                stmt.SetParameter(0, user.staff_code);
            }
            if (user.from_date != DateTime.MinValue)
            {
                stmt.SetParameter(1, user.from_date.ToString("yyyyMMdd"));
            }
            if (user.to_date != DateTime.MinValue)
            {
                stmt.SetParameter(2, user.to_date.ToString("yyyyMMdd"));
            }
            //
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Users GetUserInfoByCode(string username)
    {
        try
        {
            String sql = @"select concat(s.FIRST_NAME,' ',s.LAST_NAME) as fullname, u.*
                                 from users u
                                inner join staff s on s.id = u.username
                                where u.username = ?";
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(username);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void UpdateUserInfo(Users user)
    {
        try
        {
            String sql = null;

            if (user.locked == (int)Users.ACCOUNT_LOCK_STATUS.LOCKED)
            {
                sql = @"UPDATE users
                                set transaction_date = now(),
                                role_id = ?, locked = ?, locked_time = now(), log_error = null, expiry_date = ?
                               WHERE username = ?";
            }
            else
            {
                sql = @"UPDATE users
                                set transaction_date = now(),
                                role_id = ?, locked = ?, locked_time = null, log_error = null, expiry_date = ?
                               WHERE username = ?";
            }

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(user.role_id, user.locked, user.expiry_date.ToString("yyyyMMdd"), user.username);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addRole(Users u)
    {
        string sql = @"INSERT INTO user_role(name) VALUES(?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(u.name);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getAllRole()
    {
        string sql = @"select a.*
                     from  user_role a
                          ORDER by a.name asc";

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

    public static bool RoleAlreadyAssign(int roleId)
    {
        bool result = false;

        string sql = @"select count(*) from users a
                        Where a.role_id = ? ";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(roleId);
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

    public static void InsertAccessLevelByRole(int roleId, List<Users> roleList)
    {
        try
        {
            // remove existing 
            removeUserAccessLevelByRoleId(roleId);

            if (roleList != null && roleList.Count > 0)
            {
                foreach (Users u in roleList)
                {
                    // add new class info
                    string sql = @"INSERT INTO user_access_level(role_id, menu_id, view_access, edit_access, delete_access)
                                   VALUES(?,?,?,?,?)";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(u.role_id, u.menu_id, u.view_access, u.edit_access, u.delete_access);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListPolicyByMenuCode(Users u)
    {
        string sql = @"select * from user_access_level
                            where role_id = ? and menu_code = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(u.role_id, u.menu_id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getListUserAccessLevelByRoleId(int roleId)
    {
        string sql = @"select * from user_access_level
                            where role_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(roleId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void removeUserAccessLevelByRoleId(int roleId)
    {
        string sql = @"delete from user_access_level where role_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(roleId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteRole(int role_id)
    {
        string sql = @"delete from user_role where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(role_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Users> getAllRoleType()
    {
        string sql = @"select a.id, a.name
                          from  user_role a
                          ORDER by a.name desc";

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

    public static void ResetUserByCode(string userCode, string encryptedPasswd)
    {
        try
        {
            // remove user from users table
            String sql = @"update users set password = ?, locked = 0,
                                locked_time = null, log_error = null, expiry_date = ?
                                where username = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(encryptedPasswd, DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd"), userCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }







}