using Db_Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Utilities;


public class StudentContactPerson
{
    public int id { get; set; }
    public string student_id { get; set; }
    public string parent_code { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string birth_place { get; set; }
    public DateTime birth_date { get; set; }
    public string occupation { get; set; }
    public string marital_status { get; set; }
    public string marital_status_definition { get; set; }
    public string id_card { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string job_title { get; set; }
    public string address { get; set; }
    public string image_path { get; set; }
    public string relationship { get; set; }
    public DateTime date_register { get; set; }


    public static List<StudentContactPerson> parse(MySqlDataReader reader)
    {
        List<StudentContactPerson> listParents = new List<StudentContactPerson>();
        try
        {
            while (reader.Read())
            {
                StudentContactPerson cp = new StudentContactPerson();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { cp.id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PARENT_CODE")
                    {
                        try { cp.parent_code = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { cp.first_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { cp.last_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try { cp.sex = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_PLACE")
                    {
                        try { cp.birth_place = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_DATE")
                    {
                        try { cp.birth_date = DateTime.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "OCCUPATION")
                    {
                        try { cp.occupation = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try { cp.marital_status = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { cp.id_card = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try
                        {
                            switch (reader.GetValue(i).ToString())
                            {
                                case "C": cp.marital_status_definition = "Célibataire"; break;
                                case "M": cp.marital_status_definition = "Marié(e)"; break;
                                case "D": cp.marital_status_definition = "Divorcé(e)"; break;
                                case "V": cp.marital_status_definition = "Veuf(ve)"; break;
                            }
                        }
                        catch { }

                    }
                    if (reader.GetName(i).ToUpper() == "PHONE")
                    {
                        try { cp.phone = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EMAIL")
                    {
                        try { cp.email = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADDRESS")
                    {
                        try { cp.address = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "JOB_TITLE")
                    {
                        try { cp.job_title = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "RELATIONSHIP")
                    {
                        try { cp.relationship = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { cp.image_path = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { cp.date_register = DateTime.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_ID")
                    {
                        try { cp.student_id = reader.GetValue(i).ToString(); } catch { }
                    }
                }

                listParents.Add(cp);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listParents;
    }

    public static StudentContactPerson getParentInfoByIdCard(string idCard)
    {
        StudentContactPerson parent = null;
        List<Student> listResult = null;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        string sql = @"select * from parents where id_card = @idCard";

        //
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        con.Open();
        try
        {
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idCard", idCard);

            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                parent = parse(reader)[0];
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
        return parent;
    }

    public static void Add(StudentContactPerson p)
    {

        string sql = @"INSERT INTO STUDENT_CONTACT_PERSON(
parent_code,
first_name,
last_name,
sex,
birth_place,
birth_date,
occupation,
marital_status,
id_card,
phone,
email,
address,
job_title,
relationship,
image_path,
student_id)
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
        stmt.SetParameters(p.id, p.first_name, p.last_name, p.sex, p.marital_status,
                            p.id_card, p.birth_date.ToString("yyyyMMdd"), p.birth_place, p.address,
                            p.phone, p.email, p.image_path
                            );

        stmt.ExecuteNonQuery();
    }

    public static void updateParents(StudentContactPerson p)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        try
        {
            con.Open();
            string sql = @"update parents set first_name = @first_name, last_name = @last_name, sex = @sex, 
                            marital_status = @marital_status, phone = @phone, adress = @adress, image_path = @image_path
                              where id_card =  @id_card";

            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@first_name", p.first_name);
            cmd.Parameters.AddWithValue("@last_name", p.last_name);
            cmd.Parameters.AddWithValue("@sex", p.sex);
            cmd.Parameters.AddWithValue("@marital_status", p.marital_status);
            cmd.Parameters.AddWithValue("@phone", p.phone);
            cmd.Parameters.AddWithValue("@adress", p.address);
            cmd.Parameters.AddWithValue("@image_path", p.image_path);
            cmd.Parameters.AddWithValue("@id_card", p.id_card);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
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
                catch (Exception ex)
                {
                    MessBox.Show("Error :" + ex.Message);
                }
            }
        }
    }

    public static void attachParentsToReference(string referenceCode, string parentIdCard, string relationship)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        try
        {

            con.Open();

            // remove existing relation
            string sql = @"delete from parents_reference_attach
                           where reference_code = @reference_code 
                            and parent_id_card = @parent_id_card ";

            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@reference_code", referenceCode);
            cmd.Parameters.AddWithValue("@parent_id_card", parentIdCard);
            cmd.ExecuteNonQuery();

            // add new relation
            sql = @"insert into parents_reference_attach(reference_code, parent_id_card,
                            relationship,date_register)
                            values(@reference_code, @parent_id_card, @relationship, now())";

            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@reference_code", referenceCode);
            cmd.Parameters.AddWithValue("@parent_id_card", parentIdCard);
            cmd.Parameters.AddWithValue("@relationship", relationship);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
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
                catch (Exception ex)
                {
                    MessBox.Show("Error :" + ex.Message);
                }
            }
        }
    }

    public static List<StudentContactPerson> getListParentsInfo(StudentContactPerson p)
    {
        List<StudentContactPerson> listResult = null;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        string sql = @"select distinct p.* from parents p 
                       -- inner join parents_reference_attach pra on pra.parent_id_card = p.id_card
                        where 1=1 ";

        if (p.first_name != null)
        {
            sql += @" and upper(p.first_name) like '%" + p.first_name.ToUpper() + "%' ";
        }
        if (p.last_name != null)
        {
            sql += @" and upper(p.last_name) like '%" + p.last_name.ToUpper() + "%' ";
        }
        if (p.phone != null)
        {
            sql += @" and p.phone = '" + p.phone + "' ";
        }
        if (p.address != null)
        {
            sql += @" and p.adress like '%" + p.address + "%' ";
        }
        if (p.id_card != null)
        {
            sql += @" and p.id_card = '" + p.id_card + "' ";
        }
        if (p.sex != null)
        {
            sql += @" and p.sex = '" + p.sex + "' ";
        }
        if (p.marital_status != null)
        {
            sql += @" and p.marital_status = '" + p.marital_status + "' ";
        }
        //if (p.prefix_name != null)
        //{
        //    sql += @" and pra.reference_code like '" + p.prefix_name + "%' ";
        //}

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
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

    public static StudentContactPerson getListParentsById(int id)
    {
        StudentContactPerson p = null;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        string sql = @"select * from parents where id = @id";
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", id);
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                p = parse(reader)[0];
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
        return p;
    }

    public static StudentContactPerson getParentsInfoByReferenceCode(string code)
    {
        StudentContactPerson parentInfo = null;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        string sql = @"select distinct p.*, pra.relationship
                        from parents p 
                        inner join parents_reference_attach pra on pra.parent_id_card = p.id_card
                        where pra.reference_code = @code";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@code", code);
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                parentInfo = parse(reader)[0];
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
        return parentInfo;
    }

    public static bool checkParentExistenceByIdCard(string idCard)
    {
        int result = 0;
        bool _eval = false;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        string sql = @"select count(*) from parents where id_card = @idCard";
        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        MySqlDataReader reader = null;
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idCard", idCard);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        result = reader.GetInt32(0);
                    }
                }
            }
            if (result > 0) { _eval = true; }
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
                    // close connection and clear pool for current connection.
                    con.Close();
                    //  con.Dispose();
                    MySqlConnection.ClearPool(con);
                }
                catch { }
            }
        }
        return _eval;
    }




}