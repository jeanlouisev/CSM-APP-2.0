using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using System.Data;


public class Payroll
{
    //getters and setters
    public int id { get; set; }
    public string staff_code { get; set; }
    public double amount { get; set; }
    public int status { get; set; }
    public DateTime register_date { get; set; }
    public string login_user { get; set; }

    public static List<Payroll> parse(MySqlDataReader reader)
    {
        List<Payroll> listPayroll = new List<Payroll>();
        try
        {
            while (reader.Read())
            {
                Payroll payroll = new Payroll();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { payroll.id = reader.GetInt32(i); } catch { }
                    }

                }
                listPayroll.Add(payroll);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listPayroll;
    }


    /*
    public static bool insertSalaryAmountForStaff(Payroll p)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        bool result = false;

        string sql = @"INSERT INTO payment_special_type
                            VALUES(@id,@Description,@Amount)";

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@Id", p.id);
            cmd.Parameters.AddWithValue("@description", p.description.ToUpper());
            cmd.Parameters.AddWithValue("@inscriptionFee", p.inscriptionFee);
            cmd.Parameters.AddWithValue("@entreeFee", p.entreeFee);
            cmd.Parameters.AddWithValue("@monthlyFee", p.monthlyFee);
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
    }
    */
}