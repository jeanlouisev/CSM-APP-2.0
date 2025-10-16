using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using System.Data;
using Db_Core;
using System.Data;



public class Payment
{
    public int id { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public double amount { get; set; }
    public string staff_code { get; set; }
    public string staff_fullname { get; set; }
    public string requester_code { get; set; }
    public string requester_fullname { get; set; }
    public DateTime transaction_date { get; set; }
    public double total_amount { get; set; }
    public string payment_type { get; set; }
    public string payment_type_definition { get; set; }
    public double classroom_amount { get; set; }
    public double penality_amount { get; set; }
    public string penality_start_day { get; set; }
    public string penality_end_day { get; set; }
    public int id_classroom { get; set; }
    public string vacation { get; set; }
    public string classroom_fullname { get; set; }
    public string vacation_fullname { get; set; }
    public double amount_defined { get; set; }
    public double amount_paid { get; set; }
    public double balance { get; set; }
    public string payment_month { get; set; }
    public string payment_verse { get; set; }
    public string login_user { get; set; }

    public static List<Payment> Parse(MySqlDataReader reader)
    {
        List<Payment> listPayment = new List<Payment>();
        try
        {
            while (reader.Read())
            {
                Payment payment = new Payment();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { payment.id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        try { payment.description = reader.GetString(i).ToUpper(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TYPE")
                    {
                        try { payment.type = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAYMENT_TYPE_DEFINITION")
                    {
                        try { payment.payment_type_definition = reader.GetString(i).ToUpper(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT")
                    {
                        try { payment.amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { payment.staff_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_FULLNAME")
                    {
                        try { payment.staff_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REQUESTER_CODE")
                    {
                        try { payment.requester_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REQUESTER_FULLNAME")
                    {
                        try { payment.requester_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TRANSACTION_DATE")
                    {
                        try { payment.transaction_date = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_AMOUNT")
                    {
                        try { payment.total_amount = double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAYMENT_TYPE")
                    {
                        try { payment.payment_type = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PENALITY_AMOUNT")
                    {
                        try { payment.penality_amount = double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PENALITY_START_DAY")
                    {
                        try { payment.penality_start_day = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PENALITY_END_DAY")
                    {
                        try { payment.penality_end_day = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CLASSROOM")
                    {
                        try { payment.id_classroom = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION")
                    {
                        try { payment.vacation = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_FULLNAME")
                    {
                        try { payment.classroom_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VACATION_FULLNAME")
                    {
                        try { payment.vacation_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT_DEFINED")
                    {
                        try { payment.amount_defined = Double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT_PAID")
                    {
                        try { payment.amount_paid = Double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BALANCE")
                    {
                        try { payment.balance = Double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAYMENT_MONTH")
                    {
                        try { payment.payment_month = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAYMENT_VERSE")
                    {
                        try { payment.payment_verse = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { payment.login_user = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_AMOUNT")
                    {
                        try { payment.classroom_amount = reader.GetDouble(i); } catch { }
                    }
                }
                listPayment.Add(payment);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listPayment;
    }

    public static void addPayment(Payment p)
    {
        string sql = @"INSERT INTO PAYMENT
                                (type,
                                    description,
                                    amount_defined,
                                    amount_paid,
                                    balance,
                                    payment_month,
                                    payment_verse,
                                    staff_code,
                                    login_user,
                                    transaction_date)
                           VALUES(?, -- type,
                                    ?, -- description,
                                    ?, -- amount_defined,
                                    ?, -- amount_paid,
                                    ?, -- balance,
                                    ?, -- payment_month,
                                    ?, -- payment_verse,
                                    ?, -- staff_code,
                                    ?, -- login_user,
                                    now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.payment_type,
                                p.description,
                                p.amount_defined,
                                p.amount_paid,
                                p.balance,
                                p.payment_month,
                                p.payment_verse,
                                p.staff_code.ToUpper(),
                                p.login_user);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /* this method must be reviewed
    public static bool addPayment(DataTable dt)
    {
        
        bool result = false;

        string sql = @"INSERT INTO PAYMENT
                            (type,
                                description,
                                amount_defined,
                                amount_paid,
                                balance,
                                payment_month,
                                payment_verse,
                                staff_code,
                                login_user,
                                transaction_date)
                       VALUES(@type,
                                @description,
                                @amount_defined,
                                @amount_paid,
                                @balance,
                                @payment_month,
                                @payment_verse,
                                @staff_code,
                                @login_user,
                                now())";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        con.Open();
        try
        {
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@type", dt.Rows[i]["payment_type"].ToString());
                    cmd.Parameters.AddWithValue("@description", dt.Rows[i]["description"].ToString());
                    cmd.Parameters.AddWithValue("@amount_defined", Double.Parse(dt.Rows[i]["amount_defined"].ToString()));
                    cmd.Parameters.AddWithValue("@amount_paid", Double.Parse(dt.Rows[i]["amount_paid"].ToString()));
                    cmd.Parameters.AddWithValue("@balance", Double.Parse(dt.Rows[i]["balance"].ToString()));
                    cmd.Parameters.AddWithValue("@payment_month", dt.Rows[i]["payment_month"].ToString());
                    cmd.Parameters.AddWithValue("@payment_verse", dt.Rows[i]["payment_verse"].ToString());
                    cmd.Parameters.AddWithValue("@staff_code", dt.Rows[i]["payment_type"].ToString().ToUpper());
                    cmd.Parameters.AddWithValue("@login_user", dt.Rows[i]["login_user"].ToString());
                }
            }
            cmd.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }
    */

    public static double getClassroomAmount(int classroomId, string vacation, string paymentType)
    {
        double result = 0;

        string sql = @"SELECT a.classroom_amount
                           FROM payment_configuration a
                                where a.id_classroom = ?
                                and a.vacation = ?
                                and a.payment_type = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(classroomId, vacation, paymentType);
            IDataReader reader = stmt.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        result = reader.GetDouble(0);
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

    public static void addPaymentConfiguration(Payment p)
    {
        string sql = @"INSERT INTO payment_configuration
                                (id_classroom, vacation, payment_type, classroom_amount,
                                penality_amount, penality_start_day, penality_end_day,
                                transaction_date)
                            VALUES(?, -- id_classroom,
                                    ?, -- vacation, 
                                    ?, -- payment_type, 
                                    ?, -- classroom_amount,
                                    ?, -- penality_amount, 
                                    ?, -- penality_start_day, 
                                    ?, -- penality_end_day,
                                    now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id_classroom,
                                p.vacation,
                                p.payment_type,
                                p.classroom_amount,
                                p.penality_amount,
                                p.penality_start_day,
                                p.penality_end_day);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void paymentConfigurationExist(Payment p)
    {
        string sql = @"select count(*) from payment_configuration
                            where id_classroom = ?
                                   and vacation = ?
                                    and payment_type = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id_classroom, p.vacation, p.payment_type);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool checkExistedPaymentTypeByDescription(string description)
    {
        bool result = false;

        string sql = @"select count(*) from payment_type where upper(description) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description.ToUpper());
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

    public static void addPaymentType(Payment p)
    {
        string sql = @"INSERT INTO PAYMENT_TYPE
                                VALUES(?,?,?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id, p.description.ToUpper(), p.amount);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payment> getAllPaymentType()
    {
        string sql = @"select a.id, a.description, a.amount,
                            (select sum(b.amount) from payment_type b ) as total_amount
                            from payment_type a 
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

    public static List<Payment> getListPaymentConfiguration()
    {
        List<Payment> listResult = null;

        string sql = @"select a.id, a.id_classroom, a.vacation, a.payment_type, a.classroom_amount,
                            a.penality_amount, a.penality_start_day, a.penality_end_day,
                            a.transaction_date,
                            b.name as classroom_fullname,
                            case when a.vacation ='AM' then 'Matin'
                                when a.vacation ='PM' then 'Median'
                                when a.vacation ='NG' then 'Soir'
                                when a.vacation ='WK' then 'Weekend'
                            end as vacation_fullname,
                            case when a.payment_type ='-2' then 'MENSUEL'
                                when a.payment_type ='-3' then 'VERSEMENT'
                            end as payment_type_definition
                             from payment_configuration a, classroom b
                             where a.id_classroom = b.id
                                order by id_classroom asc";
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

    public static List<Payment> getListPayment(
        string type,
        string description,
        string amount_defined,
        string amount_paid,
        string balance,
        string payment_month,
        string payment_verse,
        string staff_code,
        string login_user,
        string from_date,
        string to_date)
    {
        string sql = @"SELECT a.id,
                        a.type,
                        a.description,
                        a.amount_defined,
                        a.amount_paid,
                        a.balance,
                        a.payment_month,
                        a.payment_verse,
                        a.staff_code,
                        concat(c.Last_name,' ',c.First_name) as staff_fullname,
                        a.login_user,
                        a.transaction_date,
                        b.description as payment_type_definition
                        FROM payment a, payment_type b, student c
                        WHERE a.type = b.id";

        //AND c.Id = a.staff_code
        //AND a.type  like @type
        //AND a.description like @description
        //AND a.amount_defined like @amount_defined
        //AND a.amount_paid like @amount_paid
        //AND a.balance like @balance
        //AND a.payment_month  like @payment_month
        //AND a.payment_verse  like @payment_verse
        //AND a.staff_code like @staff_code
        //AND a.login_user like @login_user
        //AND date_format(a.transaction_date ,'%Y%m%d') >= @from_date
        //AND date_format(a.transaction_date ,'%Y%m%d') <= @to_date";



        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            //stmt.SetParameters(xxxxxxxxxxxxxxxxxxxxxxx);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payment> getListPaymentById(int paymentId)
    {
        string sql = @"SELECT a.id,
                                    a.type,
                                    a.description,
                                    a.amount_defined,
                                    a.amount_paid,
                                    a.balance,
                                    a.payment_month,
                                    a.payment_verse,
                                    a.staff_code,
                                    a.login_user,
                                    a.transaction_date, 
                                b.description as payment_type_definition
                            FROM payment a, payment_type b
                            WHERE a.type = b.id
                                and a.id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(paymentId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payment> getListPaymentByType(int _type)
    {
        string sql = @"SELECT a.id,
                                    a.type,
                                    a.description,
                                    a.amount_defined,
                                    a.amount_paid,
                                    a.balance,
                                    a.payment_month,
                                    a.payment_verse,
                                    a.staff_code,
                                    a.login_user,
                                    a.transaction_date, 
                                b.description as payment_type_definition
                            FROM payment a, payment_type b
                            WHERE a.type = b.id
                                and a.type = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(_type);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payment> getPaymentTypeById(int id)
    {
        string sql = @"select a.* from payment_type a where a.id = ?";

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

    public static void deletePaymentType(int id)
    {
        string sql = @"delete from payment_type where id = ?";
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

    public static void deletePayment(int id)
    {
        string sql = @"delete from payment where id = ?";
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
}