using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;

public class Staff
{
    //getters and setters

    //public string Staff_code { get; set; }
    public string id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string marital_status { get; set; }
    public string id_card { get; set; }
    public DateTime birth_date { get; set; }
    public string birth_place { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string image_path { get; set; }
    public string study_level { get; set; }
    public int status { get; set; }
    public int position_id { get; set; }
    public DateTime date_register { get; set; }
    public int login_user_id { get; set; }
    public int role_id { get; set; }
    public string fullName { get; set; }
    public string name { get; set; }
    





    public enum STATUS
    {
        ACTIVE = 1,
        NOT_ACTIVE = 0
    }




    public static List<Staff> Parse(MySqlDataReader reader)
    {
        List<Staff> listStaff = new List<Staff>();
        try
        {
            while (reader.Read())
            {
                Staff staff = new Staff();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { staff.id = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { staff.first_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { staff.last_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try{staff.sex = reader.GetValue(i).ToString();}
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try{staff.marital_status = reader.GetValue(i).ToString();}
                         catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { staff.id_card = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_DATE")
                    {
                        try { staff.birth_date = reader.GetDateTime(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "BIRTH_PLACE")
                    {
                        try { staff.birth_place = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { staff.address = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE")
                    {
                        try { staff.phone = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "EMAIL")
                    {
                        try { staff.email = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { staff.image_path = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NAME")
                    {
                        try { staff.name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STUDY_LEVEL")
                    {
                        try { staff.study_level = reader.GetValue(i).ToString(); }
                        catch { }
                    }                 

                    if (reader.GetName(i).ToUpper() == "STATUS")
                    {
                        try { staff.status = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "POSITION_ID")
                    {
                        try { staff.position_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { staff.date_register = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { staff.login_user_id = int.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }

                }
                listStaff.Add(staff);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
        }
        return listStaff;
    }

    //public static List<Staff> ChekIdReferemce(Staff st)
    //{
    //    List<Staff> listResult = null;

    //    string sql = @"SELECT  a.id_card,
    //                                a.first_name,
				//					a.last_name,
    //                                a.Sex,
    //                                a.Marital_status,                                  
    //                                a.Phone,
    //                                a.adress,
    //                                a.relationship
    //                            FROM reference_external_information a
    //                            WHERE 1=1 ";

    //    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //    return Parse(stmt.ExecuteReader());



        /*

        MySqlConnection con = new MySqlConnection(constr);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader reader = null;
        //
        try
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("a.id_card", st.referenceIdCard);
            cmd.Parameters.AddWithValue("a.first_name", st.referenceFirstName);
            cmd.Parameters.AddWithValue("a.last_name", st.referenceLastName);
            cmd.Parameters.AddWithValue("a.sexe", st.referenceSex);
            cmd.Parameters.AddWithValue("a.Marital_status", st.referenceMaritalStatus);
            cmd.Parameters.AddWithValue("a.Phone", st.referencePhone);
            cmd.Parameters.AddWithValue("a.relationship", st.referenceRelationship);
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                listResult = parse(reader);
            }
        }
        catch (Exception ex)
        {
            MessBox.Show("Error :" + ex.Message);
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
        */
   // }

    //public static List<Staff> getListStaffByReference(Staff staff)
    //{
    //    string sql = @"SELECT  a.*,
    //                    concat(a.Last_name,' ',a.First_name) as fullName
    //                    FROM STAFF a
    //                    WHERE  a.status = 1 and a.Id NOT IN('PS-1')
    //                    AND a.id = ?
    //                    AND concat(lower(a.first_name),' ',lower(a.last_name)) like ? 
    //                    AND a.sexe = ?";

    //    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //    stmt.SetParameters(staff.id, staff.fullName.ToUpper(), staff.sex);
    //    return Parse(stmt.ExecuteReader());
    //}

    public static List<Staff> getListStaff(Staff st)
    {
        try
        {
            string sql = @"SELECT  a.*, concat(a.first_name,' ',a.Last_name) as fullname,
                           ( SELECT (select group_concat(concat('-',' ',p.name))) from staff_position p
								inner join staff_position sp on sp.position_id = p.id and sp.staff_code in(a.id)
								group by a.id
							)	as position_name
                             FROM staff a
                            WHERE 1=1
                            [ and a.id = ? ] -- 0
                            [ and lower(concat(a.first_name,' ',a.Last_name)) like ||'%'|| ? ||'%'|| ] -- 1
                            [ and a.sexe = ? ] -- 2
                            [ and a.marital_status = ? ] -- 3
                            [ and p.id = ? ] -- 4
                            [ and a.status = ? ] -- 5
                            order by a.first_name, a.last_name asc
                            ";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (st.id != null)
            {
                stmt.SetParameter(0, st.id);
            }
            if (st.first_name != null)
            {
                stmt.SetParameter(1, st.first_name.ToLower());
            }
            if (st.sex != null)
            {
                stmt.SetParameter(2, st.sex);
            }
            if (st.marital_status != null)
            {
                stmt.SetParameter(3, st.marital_status);
            }
            if (st.position_id > 0)
            {
                stmt.SetParameter(4, st.position_id);
            }
            if (st.status != null)
            {
                stmt.SetParameter(5, st.status);
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Staff> getListAllStaff()
    {
        try
        {
            string sql = @"SELECT  a.*,
                                    concat(a.Last_name,' ',a.First_name) as fullName,
                            (select amount from staff_salary 
                                    where staff_code = a.id 
                                    and status = 1) as amount
                            FROM staff a
                            WHERE a.Status =1";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /*
    public static List<Staff> getListStaffForPayrollWithAmount(Staff st)
    {
        try
        {
            string sql = @"SELECT  a.*,
                            concat(a.last_name,' ', a.First_name) as fullName,
                            (select amount from staff_salary 
                                    where staff_code = a.id 
                                    and status = 1) as amount,
                             (select x.group_name from tax_configuration x, staff_tax y
									WHERE x.id = y.tax_id and y.staff_code = a.Id ) AS group_name,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'septembre') as september_salary,
                            (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'septembre') as september_check,
                           (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'octobre') as october_salary,
                               (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'octobre') as october_check,
                            (SELECT (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'novembre') as november_salary,
                               (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'novembre') as november_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'decembre') as december_salary,
                                                           (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'decembre') as december_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'janvier') as january_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'janvier') as january_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'fevrier') as febuary_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'fevrier') as febuary_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mars') as march_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mars') as march_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'avril') as april_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'avril') as april_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mai') as may_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'mai') as may_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juin') as june_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juin') as june_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juillet') as july_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'juillet') as july_check,
                            (SELECT case when ((b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax)) <= 0 then 0
                                      else (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) end  FROM staff_payroll b  
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'aout') as august_salary,
                             (SELECT case when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) is null then 0
																				when (b.contract_salary)-(b.ona + b.iri + b.fdu + b.cas + b.fixed_tax) <= 0 then 0
																				 else 1 end FROM staff_payroll b 
                                    WHERE b.staff_code = a.Id AND b.academic_year = @academic_year and b.salary_month = 'aout') as august_check
					             FROM staff a, staff_salary b    
                                    WHERE 1=1  
                                                AND a.Status =1     
                                                AND a.Id = b.staff_code";

            //AND b.academic_year = @academic_year

            //if (st.Staff_code != null)
            //{
            //    sql += @" AND upper(a.id) = '" + st.Staff_code.ToUpper() + "' ";
            //}
            //if (st.fullName != null)
            //{
            //    sql += @" AND  lower(concat(a.last_name,' ', a.First_name)) like '%" + st.fullName.ToLower() + "%' ";
            //}

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    */

    public static List<Staff> getListStaffByCodeAndName(string code, string firstName, string lastName)
    {
        try
        {
            string sql = @"SELECT  a.*
                                   FROM staff a
                                WHERE 1=1
                                    AND a.id = ?                                 
                                    AND a.First_name like ?
                                    AND a.Last_name like ?
                                    AND a.Status =1 ";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(code, firstName, lastName);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Staff> getListActiveStaffForTimesheet(string staffCode)
    {
        try
        {
            String sql = @"select *, concat(first_name,' ',last_name) as fullname 
                            from staff where status = 1
                            [ and id = ? ]";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (staffCode != null)
            {
                stmt.SetParameter(0, staffCode);
            }
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public static List<Staff> SearchStaffTimesheet(Staff staff)
    //{
    //    try
    //    {
    //        string sql = @"SELECT  a.*,
    //                                b.presence_status,
				//					b.validation_status,b.date_register,
				//					b.absence_reason, case when c.id = 1 then '' else c.description end as absence_reason_description
    //                                FROM staff a, timesheets b, reason_type c
    //                                WHERE a.Status = 1 AND b.staff_code = a.Id AND b.absence_reason = c.id
    //                                AND a.id like @_staffcode
    //                                AND lower(a.first_name) like @_firstName
    //                                AND lower(a.last_name) like @_lastName 
    //                                AND b.presence_status = @presence_status
    //                         ";

    //        if (staff.fromDate != null)
    //        {
    //            sql += @" AND DATE_FORMAT(b.DATE_REGISTER ,'%Y%m%d') >= '" + staff.fromDate + "' ";
    //        }
    //        if (staff.toDate != null)
    //        {
    //            sql += @" AND DATE_FORMAT(b.DATE_REGISTER ,'%Y%m%d') <= '" + staff.toDate + "' ";
    //        }


    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        stmt.SetParameters(staff.Staff_code, staff.first_name, staff.last_name,
    //            staff.register_date_timesheet.ToString("yyyyMMdd"), staff.presence_status);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static List<Staff> getListStaffById(string staffCode)
    {
        try
        {
            string sql = @"SELECT  a.*,
                            concat(a.first_name,' ',a.Last_name) as fullname,
                            ss.amount as salary_amount, u.role_id, ur.name as role_name,
						    stx.tax_id
                             FROM staff a
                                left join staff_salary ss on ss.staff_code = a.id
								left join staff_tax stx on stx.staff_code = a.id
							    left join users u on u.username = a.id
							    left join user_role ur on ur.id = u.role_id
                            WHERE a.status = 1
                            and a.id = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Staff> getListPositions()
    {
        string sql = @"select * from staff_position order by name";
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

    public static List<Staff> getListPositionsByStaffCode(string staffCode)
    {
        string sql = @"select * from staff_position 
                         where staff_code = ?";
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

    public static void updateStaff(Staff st)
    {
        try
        {
            string sql = @"UPDATE STAFF SET
                                        first_name = ?,
                                        last_name = ?,
                                        sexe = ?,
                                        marital_status = ?,
                                        id_card = ?,
                                        birth_date = ?,
                                        birth_place = ?,
                                        adress = ?,
                                        phone1 = ?,
                                        email = ?,
                                        image_path = ?,
                                        study_level = ?,
                                        ref_first_name = ?,
                                        ref_last_name = ?,
                                        ref_sex = ?,
                                        ref_phone = ?,
                                        ref_adress = ?,
                                        ref_relationship = ?
                                        where id = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(st.first_name, st.last_name, st.sex,
                                st.marital_status, st.id_card,
                                st.birth_date.ToString("yyyyMMdd"), st.birth_place, st.address,
                                st.phone, st.email, st.image_path, st.study_level,
                                st.id);

            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addPersonal(Staff st)
    {
        try
        {
            string sql = @"insert into staff values(                         
                            ?, -- Id,
                            ?, -- firstName,
                            ?, -- lastName,
                            ?, -- sex,
                            ?, -- marital_Status,
                            ?, -- cardId,
                            ?, -- birth_date,
                            ?, -- birth_place,
                            ?, -- adress,
                            ?, -- phone1,
                            ?, -- email, 
                            ?, -- imagePath,
                            ?, -- study_level,
                            ?, -- status,
                            ?, -- position_id,
                            now(),
                            ? -- login_user_id
                           )";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(st.id, st.first_name, st.last_name, st.sex, st.marital_status,
                                st.id_card, st.birth_date.ToString("yyyyMMdd"), st.birth_place, st.address, st.phone, st.email,
                                st.image_path, st.study_level, st.status, st.position_id,                             
                                st.login_user_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void attachStaffToPosition(List<Staff> listPosition)
    {
        try
        {
            // remove old position
            RemoveStaffToPosition(listPosition[0].id);


            foreach (Staff st in listPosition)
            {
                // attach staff
                string sql = @"INSERT INTO staff_position(staff_code, position_id)
                                    values(?,?)";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                stmt.SetParameters(st.id, st.position_id);

                stmt.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveStaffToPosition(string staffCode)
    {
        try
        {
            // REMOVE PREVIOUS POSITION
            string sql1 = @"DELETE from staff_position 
                                where staff_code = ?";

            SqlStatement stmt = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static void changeStatus(int status, string staffId)
    {
        try
        {
            string sql = @"UPDATE STAFF SET status = ?
                                        where id = ?";


            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(status, staffId);

            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteStaffPermanently(string staffCode)
    {
        try
        {
            string sql = @"delete from staff where id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void disableStaff(string staffCode)
    {
        try
        {
            string sql = @"update staff set status = 0 where id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool idCardExist(string idCard)
    {
        bool result = false;

        string sql = @"SELECT COUNT(*) FROM staff a  WHERE a.id_card = ?";

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

    public static bool isValidStaff(string staffCode)
    {
        bool result = false;

        string sql = @"SELECT COUNT(*) FROM staff a  
                            WHERE a.id = ? and status = 1";

        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
        stmt.SetParameters(staffCode.ToUpper());
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

    //public static List<Staff> getListStaffForTimesheets(Staff staff)
    //{
    //    try
    //    {
    //        string sql = @"SELECT  a.id, concat(upper(a.first_name),' ', upper(a.last_name)) as fullName
    //                            FROM staff a
    //                         where a.status = 1 and a.id not in ('PS-1') ";

    //        if (staff.id != null)
    //        {
    //            sql += @" AND a.id = '" + staff.id.ToUpper() + "' ";
    //        }
    //        if (staff.fullName != null)
    //        {
    //            sql += @" AND concat(upper(a.first_name),' ', upper(a.last_name)) like '%" + staff.fullName.ToUpper() + "'% ";
    //        }

    //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
    //        return Parse(stmt.ExecuteReader());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

}