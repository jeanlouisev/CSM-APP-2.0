using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Expenditure
{
    public int id { get; set; }
    public string description { get; set; }
    public int type { get; set; }
    public string type_full { get; set; }
    public string details { get; set; }
    // public string payment_type_definition { get; set; }
    public double amount { get; set; }
    public string staff_code { get; set; }
    public string staff_fullname { get; set; }
    public string requester_code { get; set; }
    public string requester_fullname { get; set; }
    public DateTime transaction_date { get; set; }
    public double total_amount { get; set; }
    public String fromDate { get; set; }
    public String toDate { get; set; }
    public string staff_received { get; set; }
    public string staff_request_name { get; set; }
    public string staff_request_position { get; set; }
    public string staff_received_name { get; set; }
    public string staff_request_code { get; set; }
    public DateTime from_date { get; set; }
    public DateTime to_date { get; set; }


    public static List<Expenditure> Parse(MySqlDataReader reader)
    {
        List<Expenditure> listExpenses = new List<Expenditure>();
        try
        {
            while (reader.Read())
            {
                Expenditure Expense = new Expenditure();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { Expense.id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        try { Expense.description = reader.GetString(i).ToUpper(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TYPE")
                    {
                        try { Expense.type = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DETAILS")
                    {
                        try { Expense.details = reader.GetString(i).ToUpper(); } catch { }
                    }
                    /* if (reader.GetName(i).ToUpper() == "PAYMENT_TYPE_DEFINITION")
                     {
                         try { Expense.payment_type_definition = reader.GetString(i).ToUpper(); } catch { }
                     }*/
                    if (reader.GetName(i).ToUpper() == "AMOUNT")
                    {
                        try { Expense.amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { Expense.staff_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_FULLNAME")
                    {
                        try { Expense.staff_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REQUESTER_CODE")
                    {
                        try { Expense.requester_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REQUESTER_FULLNAME")
                    {
                        try { Expense.requester_fullname = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TRANSACTION_DATE")
                    {
                        try { Expense.transaction_date = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TOTAL_AMOUNT")
                    {
                        try { Expense.total_amount = double.Parse(reader.GetString(i)); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { Expense.fromDate = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { Expense.toDate = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_RECEIVED")
                    {
                        try { Expense.staff_received = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_REQUEST_NAME")
                    {
                        try { Expense.staff_request_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_REQUEST_POSITION")
                    {
                        try { Expense.staff_request_position = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_RECEIVED_NAME")
                    {
                        try { Expense.staff_received_name = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_REQUEST_CODE")
                    {
                        try { Expense.staff_request_code = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FROM_DATE")
                    {
                        try { Expense.from_date = DateTime.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TO_DATE")
                    {
                        try { Expense.to_date = DateTime.Parse(reader.GetValue(i).ToString()); } catch { }
                    }





                }
                listExpenses.Add(Expense);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listExpenses;
    }

    public static void addExepenses(Expenditure p)
    {
        string sql = @"INSERT INTO expense(details,type,amount,staff_received,staff_request,transaction_date)
                                VALUES(?,?,?,?,?,now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.details,
                                p.type,
                                p.amount,
                                p.staff_received,
                                p.staff_request_code);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool checkExistedexpenseTypeByDescription(string description)
    {
        bool result = false;
        
        string sql = @"select count(*) from expense_type where lower(description) = ?";
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

    public static bool addExpenseType(Expenditure p)
    {
        bool result = false;

        string sql = @"INSERT INTO expense_type(description)
                                VALUES(?)";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.description.ToUpper());
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

    public static List<Expenditure> getAllExpenseType()
    {
        string sql = @"select a.id, a.description from  expense_type a ORDER by a.description desc";
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

    public static void deleteExpenseType(int id)
    {
        string sql = @"delete from expense_type where id = ?";
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

    public static bool checkIfDepenseTypeAlreadyAfected(int id)
    {
        bool result = false;
        
        string sql = @"select count(*) from expense where type = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
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

    public static List<Expenditure> getListExpenses(Expenditure exp)
    {
        string sql = @"SELECT a.id,b.description,a.amount,transaction_date,a.details,a.staff_request as requester_code,
                           a.staff_received as staff_code ,concat(upper(c.first_name),' ',upper(c.last_name)) as staff_fullname
                        ,(select sum(a.amount)   
                            FROM expense a, expense_type b,staff c
                            WHERE a.type = b.id 
                                   and a.staff_received=c.id
                                AND concat(upper(c.first_name),' ',upper(c.last_name)) like @staff_request
                                AND a.type  like @type 
                                AND date_format(a.transaction_date ,'%Y%m%d') >= date_format(a.transaction_date ,'%Y%m%d')
                                AND date_format(a.transaction_date ,'%Y%m%d') <= date_format(a.transaction_date ,'%Y%m%d')) as total_amount 
                                 FROM expense a, expense_type b,staff c
                            WHERE a.type = b.id 
                                   and a.staff_received=c.id
                                AND concat(upper(c.first_name),' ',upper(c.last_name)) like ?
                                AND a.type  like ? 
                                AND date_format(a.transaction_date ,'%Y%m%d') >= ?
                                AND date_format(a.transaction_date ,'%Y%m%d') <= ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(exp.staff_fullname, exp.type_full, exp.fromDate, exp.toDate);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Expenditure> getListExpensesFull(Expenditure exp, string _type)
    {
        string sql = @"SELECT a.id,b.description,a.amount,transaction_date,a.details,a.staff_request as requester_code,
                           a.staff_received as staff_code ,concat(lower(c.first_name),' ',lower(c.last_name)) as staff_fullname     
                            FROM expense a, expense_type b,staff c
                            WHERE a.type = b.id 
                                   and a.staff_received=c.id
                                AND concat(lower(c.first_name),' ',lower(c.last_name)) like ?
                                AND a.type  like ? 
                                AND date_format(a.transaction_date ,'%Y%m%d') >= ?
                                AND date_format(a.transaction_date ,'%Y%m%d') <= ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(exp.staff_fullname, exp.type_full, exp.fromDate, exp.toDate);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Expenditure> getExpensesTypeByName(string description)
    {
        string sql = @"select a.* from expense_type a where a.description = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteExpense(int id)
    {
        string sql = @"delete from expense where id = ?";
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
    //transaction_date = @transaction_date    cmd.Parameters.AddWithValue("@transaction_date", p.transaction_date); 

    public static void updateExepenses(Expenditure p)
    {
        string sql = @"UPDATE expense SET
                                        details = ?,
                                        type = ?,                                       
                                        amount = ?,
                                        staff_received = ?,
                                        staff_request = ?,
                                        transaction_date = now()                                                                          
                                        where id = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(p.details,
                                p.type,
                                p.amount,
                                p.staff_code,
                                p.requester_code,
                                p.id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Expenditure> getListExpensesAdvanced(Expenditure exp)
    {
        string sql = @"select b.description, concat(st.Last_name,' ',st.First_name) as staff_request_name,
                               a.staff_request as staff_request_code,
                            (select concat(Last_name,' ',First_name) from staff where id = a.staff_received) as staff_received_name,
                            a.* from expense a, expense_type b , staff st
                            where a.type = b.id
                            and a.staff_request = st.id
                            and DATE_FORMAT(a.transaction_date,'%Y%m%d') >= ? 
                            and DATE_FORMAT(a.transaction_date,'%Y%m%d') <= ?";

        if (exp.type != -1)
        {
            sql += @" and a.type = " + exp.type + " ";
        }
        if (exp.details != null)
        {
            sql += @" and a.details = '" + exp.details + "' ";
        }
        if (exp.amount != 0)
        {
            sql += @" and a.amount = " + exp.amount + " ";
        }
        if (exp.staff_request_code != null)
        {
            sql += @" and a.staff_request = '" + exp.staff_request_code.ToUpper() + "' ";
        }

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(exp.from_date.ToString("yyyyMMdd"),exp.to_date.ToString("yyyyMMdd"));
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteExpenseById(string id)
    {
        string sql = @"delete from expense where id = ?";
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



    /* public static List<Expenditure> getListExpenseById(int expenseCode)
     {

         
         string sql = @"SELECT a.id,b.description,a.amount,transaction_date,a.details,a.staff_request as requester_code,
                        a.staff_received as staff_code                         
                              FROM expense a, expense_type b
                         WHERE a.type = b.id 
                                and a.id = @Id";

         MySqlConnection con = new MySqlConnection(constr);
         MySqlCommand cmd = new MySqlCommand();
         MySqlDataReader reader = null;

         try
         {
             con.Open();
             cmd = con.CreateCommand();
             cmd.CommandText = sql;
             cmd.Parameters.AddWithValue("@Id", expenseCode);
             reader = cmd.ExecuteReader();
         }
         catch (Exception ex)
         {
             MessBox.Show("Error : " + ex.Message);
         }
         return parse(reader);
     }*/

    /*  public static List<Expenditure> getListExpenses(string type, string staffCode, string amount, string requesterCode)
      {

          
          string sql = @"SELECT a.id,a.description,a.type, a.amount,a.staff_received as staff_code,
                              a.staff_request as requester_code,a.transaction_date, 
                              b.description as payment_type_definition
                          FROM expense a, expense_type b
                          WHERE a.type = b.id
                              and a.type  like @type
                              and a.description like @description
                              and a.amount like @amount
                              and a.staff_received like @staffCode
                              and a.staff_request like @requesterCode";

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
              cmd.Parameters.AddWithValue("@amount", amount);
              cmd.Parameters.AddWithValue("@staffCode", staffCode);
              cmd.Parameters.AddWithValue("@requesterCode", requesterCode);
              reader = cmd.ExecuteReader();
          }
          catch (Exception ex)
          {
              MessBox.Show("Error : " + ex.Message);
          }
          return parse(reader);
      }

      /*  

       public static List<Expenditure> getListExpenseByType(int _type)
       {

           
           string sql = @"SELECT a.id,a.description,a.type, a.amount,a.staff_received as staff_code,
                               a.staff_request as requester_code,a.transaction_date, 
                               b.description as payment_type_definition
                           FROM expense a, expense_type b
                           WHERE a.type = b.id
                               and a.type = @type";

           MySqlConnection con = new MySqlConnection(constr);
           MySqlCommand cmd = new MySqlCommand();
           MySqlDataReader reader = null;

           try
           {
               con.Open();
               cmd = con.CreateCommand();
               cmd.CommandText = sql;
               cmd.Parameters.AddWithValue("@type", _type);
               reader = cmd.ExecuteReader();
           }
           catch (Exception ex)
           {
               MessBox.Show("Error : " + ex.Message);
           }
           return parse(reader);
       }



       public static void deleteExpense(int id)
       {
           
           string sql = @"delete from expense where id = @Id";
           MySqlConnection con = new MySqlConnection(constr);
           MySqlCommand cmd;
           con.Open();
           try
           {
               cmd = con.CreateCommand();
               cmd.CommandText = sql;
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }*/

}