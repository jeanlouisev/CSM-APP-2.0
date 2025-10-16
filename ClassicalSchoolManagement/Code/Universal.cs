using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.NetworkInformation;
using Db_Core;



public class Universal
{
    //getters and setters
    public string staff_code { get; set; }
    public string id { get; set; }
    public string id_card { get; set; }
    public string fullName { get; set; }
    public int status { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string sex_code { get; set; }
    public string marital_status { get; set; }
    public string marital_status_code { get; set; }
    public string phone { get; set; }
    public string adress { get; set; }
    public string relationship { get; set; }
    public DateTime update_time { get; set; }
    public string level { get; set; }
    public string vacation { get; set; }
    public string image_path { get; set; }
    public string classroom { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public int classroom_id { get; set; }
    public string years { get; set; }
    public string reference_code { get; set; }
    public string code { get; set; }
    public string cours { get; set; }
    public string network { get; set; }
    public string content { get; set; }
    public string position { get; set; }
    public string position_description { get; set; }
    public string message_content { get; set; }
    public DateTime sent_date { get; set; }
    public string login_user { get; set; }
    public string payroll_month_amount { get; set; }
    public double net_salary { get; set; }
    public string phone_number { get; set; }
    public string message_from { get; set; }
    public string message_to { get; set; }
    public string message_type { get; set; }
    public string message_text { get; set; }
    public DateTime dateRegister { get; set; }
    public DateTime birth_date { get; set; }
    public string birth_place { get; set; }
    public string address { get; set; }
    public string email { get; set; }
    public int reference_status { get; set; }
    public int code_counter { get; set; }
    public string keys_valid { get; set; }
    public int month_num { get; set; }
    public string prefix_name { get; set; }


    public static List<Universal> Parse(MySqlDataReader reader)
    {
        List<Universal> listUniversal = new List<Universal>();
        try
        {
            while (reader.Read())
            {
                Universal universal = new Universal();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { universal.staff_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LEVEL")
                    {
                        try { universal.level = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { universal.fullName = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { universal.id = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { universal.id_card = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { universal.status = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { universal.first_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { universal.last_name = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try { universal.sex = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try { universal.sex_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try { universal.marital_status_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "C": universal.marital_status = "Célibataire"; break;
                                case "M": universal.marital_status = "Marié(e)"; break;
                                case "D": universal.marital_status = "Divorcé(e)"; break;
                                case "V": universal.marital_status = "Veuf(ve)"; break;
                                case "U": universal.marital_status = "Union Libre"; break;
                            }
                        }
                        catch { }

                    }
                    if (reader.GetName(i).ToUpper() == "PHONE")
                    {
                        try { universal.phone = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { universal.adress = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "RELATIONSHIP")
                    {
                        try { universal.relationship = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "UPDATE_TIME")
                    {
                        try { universal.update_time = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { universal.vacation = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { universal.image_path = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM")
                    {
                        try { universal.classroom = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "START_DATE")
                    {
                        try { universal.start_date = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "END_DATE")
                    {
                        try { universal.end_date = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { universal.years = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_CODE")
                    {
                        try { universal.reference_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CODE")
                    {
                        try { universal.code = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "COURS")
                    {
                        try { universal.cours = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NETWORK")
                    {
                        try { universal.network = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTENT")
                    {
                        try { universal.content = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "POSITION")
                    {
                        try { universal.position = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "POSITION")
                    {
                        try
                        {
                            switch (reader.GetString(i))
                            {
                                case "DR": universal.position_description = "Directeur(trice)"; break;
                                case "SC": universal.position_description = "Secretaire"; break;
                                case "GD": universal.position_description = "Gardien(ene)"; break;
                                case "PD": universal.position_description = "Prefert Dicipline"; break;
                                case "MG": universal.position_description = "Menager(e)"; break;
                                case "CU": universal.position_description = "Cuisinier(e)"; break;
                                case "CS": universal.position_description = "Censeur"; break;
                                case "AT": universal.position_description = "Autre"; break;

                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MESSAGE_CONTENT")
                    {
                        try { universal.message_content = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SENT_DATE")
                    {
                        try { universal.sent_date = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { universal.login_user = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAYROLL_MONTH_AMOUNT")
                    {
                        try { universal.payroll_month_amount = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NET_SALARY")
                    {
                        try { universal.net_salary = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE_NUMBER")
                    {
                        try { universal.phone_number = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MESSAGE_FROM")
                    {
                        try { universal.message_from = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MESSAGE_TO")
                    {
                        try { universal.message_to = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MESSAGE_TYPE")
                    {
                        try { universal.message_type = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MESSAGE_TEXT")
                    {
                        try { universal.message_text = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REGISTER_DATE")
                    {
                        try { universal.dateRegister = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_STATUS")
                    {
                        try { universal.reference_status = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTHDATE")
                    {
                        try { universal.birth_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTHPLACE")
                    {
                        try { universal.birth_place = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE1")
                    {
                        try { universal.phone = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EMAIL")
                    {
                        try { universal.email = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { universal.address = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CODE_COUNTER")
                    {
                        try { universal.code_counter = int.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MONTH_NUM")
                    {
                        try { universal.month_num = int.Parse(reader.GetString(i)); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "KEYS_VALID")
                    {
                        try { universal.keys_valid = reader.GetString(i); }
                        catch { }
                    }
                }
                //
                listUniversal.Add(universal);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listUniversal;
    }

    // this part take a query string 
    // and return whether the nextval or currval of a sequence

    public static Int32 getUniversalSequence(string sqlQuery)
    {
        int result = 0;

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sqlQuery, SqlConnString.CSM_APP);
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

    //this method retrieve 'staff or student or teacher' code and id_card number
    //they are all considered as internal references

    public static List<Universal> getListAllPeopleTogether(Universal uni)
    {
        string sql = @"select a.staff_code as id,
                            case
	                            when a.staff_code like 'EL%' then (select concat(st.last_name,' ',st.first_name) from student st where st.id=a.staff_code)
	                            when a.staff_code like 'PRO%' then (select concat(t.last_name,' ',t.first_name) from teacher t where t.id=a.staff_code)
	                            when a.staff_code like 'PS%' then (select concat(sta.last_name,' ',sta.first_name) from staff sta where sta.id=a.staff_code)
	                            END
	                            AS fullname
                            FROM reference_internal_information a
                            WHERE a.status = 1
                            ORDER by fullname asc";

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

    public static List<Universal> getListAllExceptParentsByCode(string code)
    {
        string sql = @"select a.id, a.fullname, a.image_path from(
                            select st.id, concat(st.last_name,' ',st.first_name) fullname, st.image_path
			                            from student st where st.status = 1 UNION all
                            select t.id, concat(t.last_name,' ',t.first_name) fullname, t.image_path
			                            from teacher t where t.status = 1  UNION all
                            select sta.id, concat(sta.last_name,' ',sta.first_name) fullname, sta.image_path
			                            from staff sta where sta.status = 1 ) a
                        where a.id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(code);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Universal> getListExternalUserByReferenceCode(string referenceCode)
    {
        string sql = @"SELECT a.*
                            FROM reference_external a
                            WHERE trim(a.reference_code) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(referenceCode);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool staffHasInternalReference(string Code)
    {
        bool result = false;

        string sql = @"SELECT count(*)
                            FROM reference_internal a
                            WHERE 1=1
                             AND trim(a.staff_code) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(Code);
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

    public static bool staffHasExternalReference(string Code)
    {
        bool result = false;

        string sql = @"SELECT count(*)
                            FROM reference_external a
                            WHERE 1=1
                             AND trim(a.staff_code)= ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(Code);
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

    public static void addStaffToInternalReferenceList(string staffCode, string idCardNumber, int status)
    {
        string sql = @"INSERT INTO reference_internal_information
                            VALUES (?,?,?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, idCardNumber, status);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addInternalReference(String staffCode, string refrerenceCode, string relationship)
    {
        string sql = @"INSERT INTO reference_internal
                            VALUES (?,?,?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, refrerenceCode, relationship);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void removeInternalReference(String staffCode)
    {
        string sql = @"delete from reference_internal where staff_code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void removeExternalReference(String staffCode)
    {
        string sql = @"delete from reference_external where staff_code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void removeExternalReferenceInformation(String referenceCode)
    {
        string sql = @"delete from reference_external_information where code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(referenceCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addExternalReference(String staffCode, string referenceCode, string relationship)
    {
        string sql = @"INSERT INTO reference_external
                            VALUES (?,?,now(),?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, referenceCode, relationship);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addExternalStaffInformation(Universal uni)
    {
        string sql = @"INSERT INTO reference_external_information 
                            VALUES (?, -- code,
                                    ?, -- idCardNumber,
                                    ?, -- firstName,
                                    ?, -- lastName,
                                    ?, -- sex,
                                    ?, -- maritalStatus,
                                    ?, -- phone,
                                    ?, -- adress,
                                    now(),
                                    ? -- image_path
                                    )";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(uni.reference_code,
                                uni.id_card,
                                uni.first_name,
                                uni.last_name,
                                uni.sex,
                                uni.marital_status,
                                uni.phone,
                                uni.adress,
                                uni.image_path);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void sendSmsToContact(List<Universal> listUniversal)
    {
        // to be reviewed ...

        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        //
        try
        {
            con.Open();

            if (listUniversal != null && listUniversal.Count > 0)
            {
                foreach (Universal universal in listUniversal)
                {
                    string sql = @"INSERT INTO messageout (MessageFrom,MessageTo,messagetype,MessageText,staffcode)VALUES (
                                    'SAPHIR_SCHOOL',
                                    @phone,
                                    'Other_BROADCAST',
                                    @content,
                                    @staff_code)";

                    cmd = con.CreateCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@phone", "+509" + universal.phone);
                    cmd.Parameters.AddWithValue("@content", universal.content);
                    cmd.Parameters.AddWithValue("@staff_code", universal.staff_code);
                    cmd.ExecuteNonQuery();
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
    }

    public static void sendBirthdaySMS(Universal uni)
    {
        // to be reviewed ...

        /*

        bool result = false;
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        //
        try
        {
            // open connection
            con.Open();
            cmd = con.CreateCommand();

            // add data to historic table
            string sql = @"insert into sms_birthday_management(staff_code, phone, message_content, sent_date, login_user)
                                          values(@staff_code, @phone, @message_content, now(), @login_user)";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@staff_code", uni.staff_code);
            cmd.Parameters.AddWithValue("@phone", uni.phone);
            cmd.Parameters.AddWithValue("@message_content", uni.message_content);
            cmd.Parameters.AddWithValue("@login_user", uni.login_user);
            cmd.ExecuteNonQuery();


            // send sms to phone
            string sql2 = @"insert into messageout (MessageFrom,MessageTo,Messagetype,MessageText,Staffcode) 
                      values('" + Universal.GetSMSNumberSender() + "', '" + uni.phone + "', 'BIRTHDAY SMS', '" + uni.message_content + "', '" + uni.staff_code + "')";
            //
            cmd.CommandText = sql2;
            cmd.ExecuteNonQuery();


            result = true;
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
    }

    public static void updateExternalStaffInformation(Universal uni)
    {
        string sql = @"update reference_external_information
                            set id_card = ?,
                                first_name = ?,
                                last_name = ?,
                                sex = ?,
                                marital_status = ?,
                                phone = ?,
                                adress = ?,
                                update_time = now(),
                                image_path =  ?
                             where  code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(uni.id_card,
                                uni.first_name,
                                uni.last_name,
                                uni.sex,
                                uni.marital_status,
                                uni.phone,
                                uni.adress,
                                uni.image_path,
                                uni.reference_code);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateInternalCodeStatus(Universal uni)
    {
        string sql = @"update reference_internal_information a
                            set a.status = ?
                            where 1=1
                            and a.staff_code = ?
                            and a.id_card = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(uni.status,
                                uni.staff_code,
                                uni.id_card);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string genRandom()
    {
        string result = null;
        try
        {
            Random rnd = new Random();
            String text = "abcdefghijklmnopqrstuvxyz0123456789";
            result = "";
            for (int i = 0; i < 10; i++)
            {
                result += text[rnd.Next(35)];
            }

        }
        catch
        {
            result = "preview";
        }
        return result;
    }

    public static DataTable ReadExcelData(string filePath)
    {
        try
        {
            DataTable worksheets;
            string connectionString = null;
            if (System.IO.Path.GetExtension(filePath) == ".xls"
                || System.IO.Path.GetExtension(filePath) == ".xlsx")
            {
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filePath + ";" + @"Extended Properties=""Excel 12.0;""";
            }
            else
            {
                connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + filePath + ";" + @"Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
            }


            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                worksheets = connection.GetSchema("Tables");

                try
                {
                    foreach (DataRow row in worksheets.Rows)
                    {
                        // For Sheets: 0=Table_Catalog,1=Table_Schema,2=Table_Name,3=Table_Type
                        // For Columns: 0=Table_Name, 1'umn_Name, 2=Ordinal_Position
                        string SheetName = (string)row[2];
                        OleDbCommand command = new OleDbCommand(@"SELECT * FROM [" + SheetName + "]", connection);
                        OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                        oleAdapter.SelectCommand = command;
                        DataTable dt = new DataTable(SheetName);
                        oleAdapter.FillSchema(dt, SchemaType.Source);
                        oleAdapter.Fill(dt);
                        connection.Close();
                        return dt;
                    }
                }
                catch (Exception ex) { return new DataTable(); }
                finally { connection.Close(); }

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error excel file format!");
            return new DataTable();
        }
        return new DataTable();
        //throw new Exception();
    }

    public static bool filterFoundIdCard(string idCardNumber)
    {
        bool result = false;

        if (Student.idCardExist(idCardNumber))
        {
            result = true;
        }
        else if (Staff.idCardExist(idCardNumber))
        {
            result = true;
        }
        else if (Teacher.idCardExist(idCardNumber))
        {
            result = true;
        }
        else if (Universal.idCardExistExternal(idCardNumber))
        {
            result = true;
        }

        return result;
    }

    public static bool idCardExistExternal(string idCard)
    {
        bool result = false;

        string sql = @"SELECT COUNT(*) FROM reference_external_information a  WHERE a.id_card = @idCard ";

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
    /**
public static DataTable ReadExcelFile(string filePath)
{
        try
        {
            DataTable worksheets;
            string connectionString = System.IO.Path.GetExtension(filePath) == ".xlsx" ?
                @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filePath + ";" + @"Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1""" :
                @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + filePath + ";" + @"Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                worksheets = connection.GetSchema("Tables");

                try
                {
                    foreach (DataRow row in worksheets.Rows)
                    {
                        // For Sheets: 0=Table_Catalog,1=Table_Schema,2=Table_Name,3=Table_Type
                        // For Columns: 0=Table_Name, 1=Column_Name, 2=Ordinal_Position
                        string SheetName = (string)row[2];
                        OleDbCommand command = new OleDbCommand(@"SELECT * FROM [" + SheetName + "]", connection);
                        OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                        oleAdapter.SelectCommand = command;
                        DataTable dt = new DataTable(SheetName);
                        oleAdapter.FillSchema(dt, SchemaType.Source);
                        oleAdapter.Fill(dt);
                        connection.Close();
                        return dt;
                    }
                }
                catch (Exception ex) { return new DataTable(); }
                finally { connection.Close(); }

            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error excel file format!" + ex.Message);
            return new DataTable();
        }
        return new DataTable();
}***/

    public static bool IsValidEmailAddress(string email)
    {
        try
        {
            var emailChecked = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        try
        {
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = FileType;
            Response.Write(content);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
    }

    public static bool classroomIsPrimary(int classroomId)
    {
        bool result = false;
        if (classroomId < 70)
        {
            result = true;
        }
        return result;
    }

    public static List<Universal> getListContacts(Universal uni, List<string> listPrefixPhone)
    {
        List<Universal> listResult = null;


        string sql = @"SELECT ID as staff_code , FULL_NAME as fullName, PHONE, POSITION level, CLASSROOM,VACATION, cours
                            FROM( select id, concat(Last_name,' ',First_name) as full_name, Phone1 as phone, 
                            'PERSONNEL' AS POSITION, 'N/A' AS CLASSROOM, 'N/A' AS VACATION,  'N/A' AS COURS  
                            from staff where `status` = 1
                            UNION ALL
                            select code as id, concat(Last_name,' ',First_name) as full_name, Phone, 
                            'PARENT' AS POSITION, 'N/A' AS CLASSROOM, 'N/A' AS VACATION,  'N/A' AS COURS  
                            from reference_external_information
                            UNION ALL
                            select a.id, concat(a.Last_name,' ',a.First_name) as full_name, a.Phone1 as phone, 
                            'ELEVE' AS POSITION, c.Name AS CLASSROOM, 
                             case 
                                 when b.vacation ='AM' then 'MATIN'
                                 when b.vacation ='PM' then 'MEDIAN'
                                 when b.vacation ='NG' then 'SOIR'
                                 when b.vacation ='WK' then 'WEEKEND' 
                               END AS VACATION,  'N/A' AS COURS 
                            from student a , classroom_staff_management b, classroom c
                              where a.Status = 1 and c.id
                            and a.Id = b.staff_code
                            and b.Id_class = c.Id
                            union ALL
                            select t.id as id, concat(Last_name,' ',First_name) as full_name, Phone1 as phone, 
                            'PROFESSEUR' AS POSITION, GROUP_CONCAT(DISTINCT(cla.Name) SEPARATOR ' , ') AS CLASSROOM, 
                            GROUP_CONCAT(DISTINCT(  case 
                                 when sch.vacation ='AM' then 'MATIN'
                                 when sch.vacation ='PM' then 'MEDIAN'
                                 when sch.vacation ='NG' then 'SOIR'
                                 when sch.vacation ='WK' then 'WEEKEND' 
                               END ) SEPARATOR '-') AS VACATION, 
                            GROUP_CONCAT(distinct(c.Name) SEPARATOR '-') AS COURS  
                            from TEACHER t, teacher_cours_attach ctm, cours c, schedule sch, classroom cla
                            where t.status = 1
                            and t.Id = ctm.teacher_id
                            and ctm.cours_id = c.Id
                            and t.id = sch.Id_teacher
                            and sch.Id_class =  cla.Id 
                            and cla.id                     
                            GROUP BY t.id
                            ) ALL_STAFF
                            where 1=1 and phone not in ('')";


        if (listPrefixPhone != null)
        {
            List<string> listConcatPrefix = new List<string>();
            sql += @" and(";
            foreach (string prefix in listPrefixPhone)
            {
                listConcatPrefix.Add("phone like '" + prefix + "%'");
            }
            // convert listconcat to array
            string[] arrayConcatPrefix = listConcatPrefix.ToArray();
            // add or separator to array
            sql += string.Join(" or ", arrayConcatPrefix); ;
            sql += @" )";
        }

        if (uni.phone != null)
        {
            sql += @" and phone = " + uni.phone + "";
        }
        if (uni.fullName != null)
        {
            sql += @" and lower(FULL_NAME) like '" + uni.fullName + "'";
        }
        if (uni.level != null)
        {
            sql += @" and lower(position) = '" + uni.level + "'";
        }
        if (uni.classroom != null)
        {
            sql += @" and lower(classroom) = '" + uni.classroom.ToLower() + "'";
        }
        if (uni.vacation != null)
        {
            sql += @" and vacation = '" + uni.vacation + "'";
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

    public static List<Universal> getListBirthday()
    {
        string sql = @"select id, fullname, position, phone from(
                            select id, CONCAT(upper(first_name),' ', upper(Last_name)) as fullname, 'Eleve' as position, phone1 as phone from student
                            union all
                            select id, CONCAT(upper(first_name),' ', upper(Last_name)) as fullname,
                            case 
                                  when position = 'DR' then 'Directeur(trice)' 
                                  when position = 'SC' then 'Secretaire' 
                                  when position = 'CS' then 'Censeur' 
                                  when position = 'PD' then 'Prefert Dicipline' 
                                  when position = 'MG' then 'Menager(e)' 
                                  when position = 'CU' then 'Cuisinier(e)' 
                                  when position = 'GD' then 'Gardien(ene)'  
                                END as position, phone1 as phone From staff WHERE ID NOT IN('PS-1')
                            union all
                            select id, CONCAT(upper(first_name),' ', upper(Last_name)) as fullname, 'Professeur' as position, phone1 as phone from teacher
                            ) all_birthday group by id,fullname, position";

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

    public static List<Universal> getListSmsContent()
    {
        string sql = @"SELECT a.id, CONCAT(substr(a.message_content,1,30),' ...') content,
                               a.message_content
                            FROM sms_template a
                            WHERE 1=1                            
                            ORDER BY a.message_content";
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

    public static List<Universal> getListBirthdaySmsAlreadySent(string staff_code, string login_user)
    {
        string sql = @"SELECT * from sms_birthday_management 
                             where staff_code = ? and login_user = ?
                                 and EXTRACT(year FROM sent_date) = EXTRACT(year FROM now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staff_code, login_user);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Universal> getListAcademicYearFullInfoById(int id)
    {
        string sql = @"select * from academic_year where id = @id";

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

    public static List<Universal> getStartDate(int id)
    {
        string sql = @"select EXTRACT(year FROM start_date) as start_date from academic_year where id = ?";
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

    public static void SendPayrollSMS(List<Universal> listPayroll)
    {
        // to be reviewed ...

        /*
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();

            foreach (Universal uni in listPayroll)
            {
                string sql = @"insert into messageout (MessageFrom,MessageTo,Messagetype,MessageText,Staffcode) 
                      values('" + uni.message_from + "', '" + uni.message_to + "','" + uni.message_type + "', '" + uni.message_text + "', '" + uni.staff_code + "')";
                //
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
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
    }

    public static string GetSMSNumberSender()
    {
        return "43103030";
    }

    public static string GetPhoneNumberByStaffCode(string staffCode)
    {
        try
        {
            String result = null;
            string sql = null;
            if (staffCode.ToUpper().StartsWith("EL"))
            {
                sql = @"select phone1 as phone_number from student where id = ?";
            }
            if (staffCode.ToUpper().StartsWith("PRO"))
            {
                sql = @"select phone1 as phone_number from teacher where id = ?";
            }
            if (staffCode.ToUpper().StartsWith("PS"))
            {
                sql = @"select phone1 as phone_number from staff where id = ?";
            }

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            List<Universal> listPhone = Parse(stmt.ExecuteReader());

            if (listPhone != null && listPhone.Count > 0)
            {
                result = listPhone[0].phone_number;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool isValidAcademicStartYear(string startDate, int id)
    {
        bool result = false;
        string sql = @"select count(*) from academic_year 
                        where date_format(start_date,'%Y%m%d') < ? and id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(startDate, id);
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

    public static bool isNotValidAcademicEndYear(string endDate, int id)
    {
        bool result = false;

        string sql = @"select count(*) from academic_year 
                        where date_format(end_date, '%Y%m%d') < ? and id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(endDate, id);
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

    public static List<Universal> getUserProfile(string staffCode)
    {
        string sql = @"SELECT  a.*,
                                    concat(a.Last_name,' ',a.First_name) as fullname
                                   FROM staff a
                                WHERE a.Id = ? 
                                    AND a.Status =1";

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

    //private void getClassroomCoursManagementConfig(int oldMaxAcademicYear, int NewMaxAcademicYear)
    //{
    //    try
    //    {
    //        
    //        MySqlConnection con = new MySqlConnection(constr);
    //        MySqlCommand cmd = new MySqlCommand();


    //        string sql = @"insert into classroom_cours_management
    //                         select 0 as id,Id_Class, id_cours_price, Id_Cours, Register_date, vacation, @NewMaxAcademicYear as academic_year
    //                        from classroom_cours_management where academic_year = @oldMaxAcademicYear ";
    //        //
    //        con.Open();
    //        cmd = con.CreateCommand();
    //        cmd.CommandText = sql;
    //        cmd.Parameters.AddWithValue("oldMaxAcademicYear", oldMaxAcademicYear);
    //        cmd.Parameters.AddWithValue("NewMaxAcademicYear", NewMaxAcademicYear);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessBox.Show("Error : " + ex.Message);
    //    }
    //}

    
    public static bool codeExist(string code)
    {
        bool result = false;

        string sql = @"select sta.id from staff sta where sta.id = ?
                                union
                                select t.id from teacher t WHERE t.id = ?
                                union 
                                select stu.id from student stu where stu.id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(code, code, code);
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

    public static string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        return sMacAddress;
    }

    public static bool MACAddressCompatible()
    {
        //bool result = false;
        //string sysMacAd = Universal.GetMACAddress();
        //if (sysMacAd == "8434972011DD")
        //{
        //    result = true;
        //}
        //return result;

        return true;
    }

    public static bool checkAppPaymentStatus()
    {
        bool result = false;
        DateTime registeredDate = DateTime.Now.AddDays(-30);
        DateTime currentDate = DateTime.Now;
        List<Universal> listAppPayment = Universal.GetListAppPayment();
        if (listAppPayment == null && listAppPayment.Count <= 0)
        {

        }
        else
        {

        }
        
        return result;
    }

    public static List<Universal> GetListAppPayment()
    {
        string sql = @"select * from lock_mgnt order by id";

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

    public static void addMessage(Universal p)
    {
        string sql = @"INSERT INTO sms_template(message_content)
                                VALUES(@message_content)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.message_content);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteContent(int contentId)
    {
        string sql = @"delete from sms_template where id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(contentId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool AcademicYearConfigExist()
    {
        bool result = false;
        string sql = @"select count(*) from academic_year";

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


}