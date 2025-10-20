using Db_Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using Telerik.Web.UI;
using Utilities;



public class StudentContribution
{
    public int id { get; set; }
    public int contribution_type_id { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public double paid_amount { get; set; }
    public string student_id { get; set; }
    public int classroom_id { get; set; }
    public string classroom_name { get; set; }
    public int academic_year_id { get; set; }
    public DateTime date_register { get; set; }
    public int login_user_id { get; set; }
    public int status { get; set; }




    public enum PAYMENT_STATUS
    {
        Paid = 1,
        NotPaid = 0
    }






    public static List<StudentContribution> Parse(MySqlDataReader reader)
    {
        List<StudentContribution> listContribution = new List<StudentContribution>();
        try
        {
            while (reader.Read())
            {
                StudentContribution sc = new StudentContribution();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { sc.id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTRIBUTION_TYPE_ID")
                    {
                        try { sc.contribution_type_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        try { sc.description = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PRICE")
                    {
                        try { sc.price = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PAID_AMOUNT")
                    {
                        try { sc.paid_amount = double.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDENT_ID")
                    {
                        try { sc.student_id = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_ID")
                    {
                        try { sc.classroom_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CLASSROOM_NAME")
                    {
                        try { sc.classroom_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { sc.academic_year_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { sc.date_register = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { sc.login_user_id = reader.GetInt32(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { sc.status = reader.GetInt32(i); }
                        catch { }
                    }

                }
                listContribution.Add(sc);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listContribution;
    }

    public static void addContributionType(string description, double price)
    {
        string sql = @"INSERT INTO student_contribution_type(description, price)
                                VALUES(?, ?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description, price);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void makePayments(StudentContribution sc)
    {
        string sql = @"INSERT INTO student_contribution_payments(
                        contribution_type_id,
                        paid_amount,
                        student_id,
                        classroom_id,
                        academic_year_id,
                        status,
                        date_register,
                        login_user_id)
                      VALUES(?, -- contribution_type_id,
                            ?, -- paid_amount,
                            ?, -- student_id,
                            ?, -- classroom_id,
                            ?, -- academic_year_id,
                            ?, -- status,
                            now(), -- date_register,
                            ? -- login_user_id
                              )";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sc.contribution_type_id, sc.paid_amount,
                sc.student_id, sc.classroom_id, sc.academic_year_id, sc.status, sc.login_user_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void removePayments(int id)
    {
        string sql = @"delete from student_contribution_payments where id = ?";

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

    public static void deleteContributionType(int id)
    {
        string sql = @"delete from student_contribution_type where id = ?";

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

    public static void updateContributionType(StudentContribution sc)
    {
        string sql = @"update student_contribution_type 
                        set description = ?,
                        price = ?
                        where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sc.description, sc.price, sc.id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<StudentContribution> descriptionAlreadyExist(string description)
    {
        string sql = @"select * from student_contribution_type where lower(description) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(description.ToLower());
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static StudentContribution getListContributionTypeById(int id)
    {
        string sql = @"SELECT * FROM STUDENT_CONTRIBUTION_TYPE where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            return Parse(stmt.ExecuteReader())[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<StudentContribution> getListContributionTypeByDescription(string description)
    {
        string sql = @"SELECT  a.* FROM STUDENT_CONTRIBUTION_TYPE a
                             where 1=1";

        if (description != null)
        {
            sql += @" and upper(a.description) like '%" + description.ToUpper() + "%'";
        }
        sql += @" ORDER by a.description asc";

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

    public static List<StudentContribution> getListContributionForPayment(Student st)
    {
        string sql = @"select a.*, b.description from student_contribution_payments a 
                        inner join student_contribution_type b on b.id = a.contribution_type_id
                        where a.status = 1	
                                and a.student_id  = ?
                                and a.classroom_id = ?
                                and a.academic_year_id = ?
                    ORDER by b.description asc";


        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(st.id, st.classroom_id, st.academic_year_id);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static StudentContribution getRegistrationFee()
    {
        string sql = @"SELECT * FROM STUDENT_CONTRIBUTION_TYPE where id = -2";

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

    public static List<StudentContribution> getListContributionTypes()
    {
        string sql = @"SELECT * FROM STUDENT_CONTRIBUTION_TYPE
                             order by description asc";

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

    public static double getContributionPriceById(int id)
    {
        double result = 0;

        string sql = @"SELECT price FROM student_contribution_type where id = ? ";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(id);
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

    public static bool isAlreadyPaid(StudentContribution st)
    {
        bool result = false;

        string sql = @"select count(*) from student_contribution_payments
                        where status = 1												
                        and student_id  = ?
                        and classroom_id = ? 
                        and academic_year_id = ?
                        and contribution_type_id = ?";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(st.student_id, st.classroom_id, st.academic_year_id, st.contribution_type_id);
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












}