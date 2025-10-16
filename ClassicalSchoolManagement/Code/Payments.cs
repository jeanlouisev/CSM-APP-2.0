using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using System.Data;
using Db_Core;


public class Payments
{
    public int id { get; set; }
    public int id_Special_payment { get; set; }
    public string id_payment_type { get; set; }
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
    public double inscriptionFee { get; set; }
    public double entreeFee { get; set; }
    public double monthlyFee { get; set; }
    public double versement_1 { get; set; }
    public double versement_2 { get; set; }
    public double versement_3 { get; set; }
    public double versement_4 { get; set; }
    public int academic_year { get; set; }
    public int payment_configuration_id { get; set; }
    public string years { get; set; }

    public static List<Payments> Parse(MySqlDataReader reader)
    {
        List<Payments> listPayment = new List<Payments>();
        try
        {
            while (reader.Read())
            {
                Payments payment = new Payments();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { payment.id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_SPECIAL_PAYMENT")
                    {
                        try { payment.id_Special_payment = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_PAYMENT_TYPE")
                    {
                        try { payment.id_payment_type = reader.GetString(i); } catch { }
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

                    if (reader.GetName(i).ToUpper() == "INSCRIPTIONFEE")
                    {
                        try { payment.inscriptionFee = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ENTREEFEE")
                    {
                        try { payment.entreeFee = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MONTHLYFEE")
                    {
                        try { payment.monthlyFee = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VERSEMENT_1")
                    {
                        try { payment.versement_1 = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VERSEMENT_2")
                    {
                        try { payment.versement_2 = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VERSEMENT_3")
                    {
                        try { payment.versement_3 = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "VERSEMENT_4")
                    {
                        try { payment.versement_4 = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { payment.academic_year = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_PAYMENT_CONF")
                    {
                        try { payment.payment_configuration_id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { payment.years = reader.GetString(i); } catch { }
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

    public static void addPayment(Payments p)
    {
        string sql = @"INSERT INTO PAYMENTS
                                   (Id_student,
                                    Id_payment_conf,
                                    payment_type,                                   
                                    amount_paid,
                                    login_user,                                   
                                    transaction_date)
                             VALUES(?, -- staff_code,
                                    ?, -- id_payment_type, 
                                    ?, -- payment_type,                                                                  
                                    ?, -- amount_paid,                                   
                                    ?, -- login_user,
                                    now())";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.staff_code, p.id_payment_type,
                                p.payment_type, p.amount_paid, p.login_user);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addPaymentMontlyVersement(List<Payments> listPayments)
    {
        if (listPayments != null && listPayments.Count > 0)
        {
            foreach (Payments p in listPayments)
            {
                string sql = @"INSERT INTO PAYMENTS
                                   (Id_student,
                                    Id_payment_conf,
                                    payment_type,
                                    amount_paid,
                                    login_user,                                   
                                    transaction_date)
                             VALUES(?,?,?,?,?, now())";

                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(p.staff_code.ToUpper(), p.payment_configuration_id,
                                        p.description, p.amount_paid, p.login_user.ToUpper());
                    stmt.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
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
                    if (reader.GetDouble(0) > 0)
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

    public static void addPaymentConfiguration(Payments p)
    {
        string sql = @"INSERT INTO payment_configuration
                                (id_classroom, vacation, payment_type, classroom_amount,
                                penality_amount, penality_start_day, penality_end_day,
                                transaction_date,versement_1,versement_2,versement_3,versement_4,inscriptionFee,entreeFee,academic_year)
                            VALUES(?, -- id_classroom, 
                                    ?, -- vacation, 
                                    ?, -- payment_type, 
                                    ?, -- classroom_amount,
                                    ?, -- penality_amount, 
                                    ?, -- penality_start_day, 
                                    ?, -- penality_end_day,
                                    now(),
                                    ?,-- versement_1,
                                    ?,-- versement_2,
                                    ?,-- versement_3,
                                    ?,-- versement_4,
                                    ?,-- inscriptionFee,
                                    ?,-- entreeFee,
                                    ?-- academic_year
                                   )";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id_classroom,
                                p.vacation,
                                p.payment_type,
                                p.classroom_amount,
                                p.penality_amount,
                                p.penality_start_day,
                                p.penality_end_day,
                                p.versement_1,
                                p.versement_2,
                                p.versement_3,
                                p.versement_4,
                                p.inscriptionFee,
                                p.entreeFee,
                                p.academic_year);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool paymentConfigurationExist(Payments p)
    {
        bool result = false;

        string sql = @"select count(*) from payment_configuration
                            where id_classroom = ?
                                   and vacation = ?                                    
                                    and academic_year = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id_classroom, p.vacation, p.academic_year);
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

    public static bool checkExistedPaymentTypeByDescription(string description, int academic_year)
    {
        bool result = false;

        string sql = @"select count(*) from payment_type where upper(description) = ? 
                           and accademicyear = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description, academic_year);
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

    public static bool checkExistedspecialePaymentTypeByDescription(string description)
    {
        bool result = false;

        string sql = @"select count(*) from payment_special_type where upper(description) = @Description";
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

    public static void addPaymentSpecialType(Payments p)
    {
        string sql = @"INSERT INTO payment_special_type
                                VALUES(@id,@Description,@Amount)";

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

    public static List<Payments> getListSpecialpaymentOrdered()
    {
        string sql = @"SELECT a.* FROM payment_special_type a";

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

    public static void addPaymentType(Payments p)
    {
        string sql = @"INSERT INTO PAYMENT_TYPE
                                VALUES(?, -- Id,
                                        ?, -- Description,
                                        ?, -- Amount,
                                        ? -- academic_year
                                        )";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id, p.description.ToUpper(), p.amount, p.academic_year);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getAllPaymentType(Payments p)
    {
        List<Payments> listResult = null;

        string sql = @"select a.id, a.description, a.amount,a.academic_year,
                            (select sum(b.amount) from payment_type b  
                                where b.academic_year =  a.academic_year  
                                and b.description = a.description) as total_amount
                            from payment_type a where a.id not in(-2,-3,-4,-5) 
                          and  a.academic_year = ?";


        if (p.amount != 0)
        {
            sql += @" and a.amount = " + p.amount + " ";
        }
        if (p.description != null)
        {
            sql += @" and a.description like '%" + p.description + "%' ";
        }
        sql += @" ORDER by a.description asc";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getAllPaymentScolarite(Payments p)
    {
        List<Payments> listResult = null;

        string sql = @"select a.id,a.payment_type,a.amount_paid,a.transaction_date,a.login_user  from payments a ,payment_configuration b
                          where a.Id_student = ? 
                            and b.academic_year = ? 
                            and b.id_classroom = ? 
                            and a.payment_type != 'OTHER FEE' 
                            and a.id_payment_conf=b.id ORDER BY id desc";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.staff_code, p.academic_year, p.id_classroom);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getAllPaymentOthers(Payments p)
    {
        string sql = @"select a.id, a.Id_student as staff_code,
                            a.payment_type, a.amount_paid,a.transaction_date, a.login_user,b.description,
                            concat(extract(YEAR from c.start_date),' - ',extract(YEAR from c.end_date)) as years
                              FROM payments a ,payment_type b,academic_year c
                              WHERE a.payment_type ='OTHER FEE'
                                and a.id_payment_conf=b.id 
                                and b.academic_year=c.id ";

        if (p.staff_code != null)
        {
            sql += @" AND a.Id_student = '" + p.staff_code.ToUpper() + "' ";
        }
        if (p.academic_year != 0)
        {
            sql += @" AND b.academic_year = " + p.academic_year + " ";
        }
        if (p.id != 0)
        {
            sql += @" AND b.id = " + p.id + " ";
        }
        sql += @" ORDER BY a.id desc";


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

    public static List<Payments> getListPaymentConfiguration(Payments p)
    {
        string sql = @"select a.id, a.id_classroom, a.vacation, a.payment_type, a.classroom_amount,
                            a.penality_amount, a.penality_start_day, a.penality_end_day,
                            a.transaction_date,a.versement_1,a.versement_2,a.versement_3,a.versement_4,
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
                             where 1=1  and  a.id_classroom =b.id                           
                             and a.academic_year like @academic_year
                            ";

        if (p.id_classroom != -1)
        {
            sql += @" and a.id_classroom = " + p.id_classroom + " ";
        }

        if (p.classroom_amount != 0)
        {
            sql += @" and a.classroom_amount = " + p.classroom_amount + " ";
        }

        if (p.versement_1 != 0)
        {
            sql += @" and a.versement_1 = " + p.versement_1 + " ";
        }
        if (p.versement_2 != 0)
        {
            sql += @" and a.versement_2 = " + p.versement_2 + " ";
        }
        if (p.versement_3 != 0)
        {
            sql += @" and a.versement_3 = " + p.versement_3 + " ";
        }
        if (p.versement_4 != 0)
        {
            sql += @" and a.versement_4 = " + p.versement_4 + " ";
        }
        if (p.inscriptionFee != 0)
        {
            sql += @" and a.inscriptionFee = " + p.inscriptionFee + " ";
        }
        if (p.entreeFee != 0)
        {
            sql += @" and a.entreeFee = " + p.entreeFee + " ";
        }
        sql += @"  order by id_classroom asc";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /*   public static List<Payments> getListPayment(string staff_code, string type,int academic_year)
        {
            List<Payments> listResult = null;
            
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
                                    WHERE a.type = b.id
                                        AND c.Id = a.staff_code
                                        AND a.type  like @type
                                        AND a.description like @description
                                        AND a.amount_defined like @amount_defined
                                        AND a.amount_paid like @amount_paid
                                        AND a.balance like @balance
                                        AND a.payment_month  like @payment_month
                                        AND a.payment_verse  like @payment_verse
                                        AND a.staff_code like @staff_code
                                        AND a.login_user like @login_user
                                        AND date_format(a.transaction_date ,'%Y%m%d') >= @from_date
                                        AND date_format(a.transaction_date ,'%Y%m%d') <= @to_date";

            MySqlConnection con = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;

            try
            {
               con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@amount_defined", amount_defined);
                cmd.Parameters.AddWithValue("@amount_paid", amount_paid);
                cmd.Parameters.AddWithValue("@balance", balance);
                cmd.Parameters.AddWithValue("@payment_month", payment_month);
                cmd.Parameters.AddWithValue("@payment_verse", payment_verse);
                cmd.Parameters.AddWithValue("@staff_code", staff_code);
                cmd.Parameters.AddWithValue("@login_user", login_user);
                cmd.Parameters.AddWithValue("@from_date", from_date);
                cmd.Parameters.AddWithValue("@to_date", to_date);
                reader = cmd.ExecuteReader();
              (  if (reader != null)
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
    }*/

    public static List<Payments> getListPayment()
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
                            WHERE a.type = b.id";

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

    public static List<Payments> getListPaymentById(int paymentId)
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

    public static List<Payments> getListPaymentByType(int payment_type, int academic_year)
    {
        string sql = @"SELECT a.*, b.description as payment_type_definition
                            FROM payments a, payment_type b
                            WHERE a.payment_type = b.id and a.payment_type = ?
                            and b.accademicyear = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(payment_type, academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getListspecialPaymentById(int id)
    {
        string sql = @"SELECT a.id,                                   
                            a.description,
                            a.inscriptionFee,
                            a.entreeFee,
                            a.monthlyFee
                            FROM payment_special_type a
                            WHERE a.id = ?";

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

    public static List<Payments> getPaymentTypeById(int id, string staff_code, int academic_year)
    {
        string sql = @"select a.id,a.description,a.amount,
                            (a.amount - (select sum(b.amount_paid) from payments b 
                                            where b.id_payment_conf = a.id and b.id_student = ?)) as balance,
                            (select sum(b.amount_paid)  from payments b 
                                where b.id_payment_conf = a.id and b.id_student = ?) as amount_paid
                            from payment_type a 
                            where a.id = ? and a.accademicyear = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staff_code, staff_code, id, academic_year);
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

    public static void deletePaymentTypeScolarite(int id)
    {
        string sql = @"delete from payments where id = ?";
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

    public static void deletePreviouslyAffectedStudentsSpecialPayment(string id, int academic_year, int classroom_id)
    {
        string sql = @"delete from Payment_Special_Students_management  where 
                        id_student = ?
                        and academic_year = ?
                        and classroom_id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id, academic_year, classroom_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void AffectedSpecialPaymentTostudent(
        string id, int id_Special_payment, int academic_year, int classroom_id)
    {
        string sql = @"INSERT INTO Payment_Special_Students_management(
                        id_Student,id_Special_payment,Register_date,academic_year,classroom_id)
                        VALUES(?,?,now(), ?,?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id, id_Special_payment, academic_year, classroom_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getListAffectedSpecialPaymentTostudent()
    {
        string sql = @"SELECT a.id,a.id_Student staff_code,a.id_Special_payment id_Special_payment
                            FROM Payment_Special_Students_management a
                            where 1=1";

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

    public static void ModifyPaymentConfiguration(Payments p)
    {
        string sql = @"UPDATE payment_configuration SET
                                        id_classroom = ?,
                                        vacation = ?,
                                        payment_type = ?,
                                        classroom_amount = ?,
                                        penality_amount = ?,
                                        penality_start_day = ?,
                                        penality_end_day = ?,
                                        transaction_date = now(),
                                        versement_1 = ?,
                                        versement_2 = ?,
                                        versement_3 = ?,
                                        versement_4 = ?,
                                        inscriptionFee = ?,
                                        entreeFee = ?
                                        where id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.id_classroom,
                                p.vacation,
                                p.payment_type,
                                p.classroom_amount,
                                p.penality_amount,
                                p.penality_start_day,
                                p.penality_end_day,
                                p.versement_1,
                                p.versement_2,
                                p.versement_3,
                                p.versement_4,
                                p.inscriptionFee,
                                p.entreeFee,
                                p.id_Special_payment);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool Check_If_PaymentAssign(int id_Special_payment)
    {
        bool result = false;

        string sql = @"select count(*) from payment_special_students_management a
                                      where id_Special_payment = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id_Special_payment);
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

    public static void deletePayment_configuration(int id)
    {
        string sql = @"delete from payment_configuration where id = ?";
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

    public static List<Payments> getPaymentById(int id)
    {
        string sql = @"select a.id_classroom, a.vacation, a.payment_type, a.classroom_amount,
                                a.penality_amount, a.penality_start_day, a.penality_end_day,
                                a.transaction_date,a.versement_1,a.versement_2,a.versement_3,
                                a.versement_4,a.inscriptionFee,a.entreeFee
                            FROM payment_configuration a
                            Where   a.id = ?";
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

    public static List<Payments> getListPaymentConfigurationWithclassid(int id_class, int academic_year)
    {
        string sql = @"select a.id, a.id_classroom, a.vacation, a.payment_type, a.classroom_amount,
                     a.penality_amount, a.penality_start_day, a.penality_end_day,
                     a.transaction_date,a.versement_1,versement_2,versement_3,versement_4,inscriptionFee,entreeFee
                      from payment_configuration a
                      where a.id_classroom = ?
                      and academic_year= ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id_class, academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Payments> getList_balance_payment(int id_class, string student_Code, int academic_year)
    {
        string sql = @"select a.payment_type,
                            sum(a.amount_paid) as amount_paid,b.id,
                            b.id_classroom,
                            b.vacation,
                            b.classroom_amount,
                            b.penality_amount,
                            b.penality_start_day,
                            b.penality_end_day,
                            b.transaction_date,
                            b.versement_1,
                            b.versement_2,
                            b.versement_3,
                            b.versement_4,
                            b.inscriptionFee,
                            b.entreeFee,
                            b.academic_year
                            from payments a,payment_configuration b
                            where Id_student = ?
                            and b.id_classroom = ?
                            and academic_year = ?
                            and a.payment_type != 'OTHER FEE'
                            and a.id_payment_conf = b.id
                            group by a.payment_type";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(student_Code, id_class, academic_year);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}