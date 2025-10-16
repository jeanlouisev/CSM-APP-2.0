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


public class Salary
{
    //getters and setters
    public int id { get; set; }
    public string staff_code { get; set; }
    public double amount { get; set; }
    public int status { get; set; }
    public DateTime date_register { get; set; }
    public string login_user { get; set; }
    public int login_user_id { get; set; }
    public int tax_id { get; set; }
    public string group_name { get; set; }
    public double fixed_tax { get; set; }
    public int ona { get; set; }
    public int iri { get; set; }
    public int fdu { get; set; }
    public int cas { get; set; }
    public double contract_salary { get; set; }
    public double net_salary { get; set; }
    public string salary_month { get; set; }
    public string salary_month_fr { get; set; }
    public double ona_tax_amount { get; set; }
    public double iri_tax_amount { get; set; }
    public double fdu_tax_amount { get; set; }
    public double cas_tax_amount { get; set; }
    public int academic_year { get; set; }
    public int academic_year_id { get; set; }
    public string academic_year_description { get; set; }
    public double deduction { get; set; }
    public string phone_number { get; set; }
    public string years { get; set; }
    public string fullname { get; set; }
    public string teacher_id { get; set; }



    public static class tax
    {
        public const double ona = 0.06;
        public const double iri = 0.02;
        public const double fdu = 0.01;
        public const double cas = 0.02;
    }

    public static class payroll_status
    {
        public const int active = 1;
        public const int not_active = 0;
    }


    public static List<Salary> Parse(MySqlDataReader reader)
    {
        List<Salary> listSalary = new List<Salary>();
        try
        {
            while (reader.Read())
            {
                Salary salary = new Salary();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { salary.id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { salary.staff_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "AMOUNT")
                    {
                        try { salary.amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "status")
                    {
                        try { salary.status = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DATE_REGISTER")
                    {
                        try { salary.date_register = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER")
                    {
                        try { salary.login_user = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LOGIN_USER_ID")
                    {
                        try { salary.login_user_id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TAX_ID")
                    {
                        try { salary.tax_id = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "GROUP_NAME")
                    {
                        try { salary.group_name = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIXED_TAX")
                    {
                        try { salary.fixed_tax = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ONA")
                    {
                        try { salary.ona = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IRI")
                    {
                        try { salary.iri = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FDU")
                    {
                        try { salary.fdu = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CAS")
                    {
                        try { salary.cas = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CONTRACT_SALARY")
                    {
                        try { salary.contract_salary = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "NET_SALARY")
                    {
                        try { salary.net_salary = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SALARY_MONTH")
                    {
                        try { salary.salary_month = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SALARY_MONTH")
                    {
                        try
                        {
                            string _month = reader.GetString(i);
                            switch (_month)
                            {
                                case "JAN": salary.salary_month_fr = "Janvier"; break;
                                case "FEB": salary.salary_month_fr = "Février"; break;
                                case "MAR": salary.salary_month_fr = "Mars"; break;
                                case "APR": salary.salary_month_fr = "Avril"; break;
                                case "MAY": salary.salary_month_fr = "Mai"; break;
                                case "JUN": salary.salary_month_fr = "Juin"; break;
                                case "JUL": salary.salary_month_fr = "Juillet"; break;
                                case "AUG": salary.salary_month_fr = "Août"; break;
                                case "SEP": salary.salary_month_fr = "Septembre"; break;
                                case "OCT": salary.salary_month_fr = "Octobre"; break;
                                case "NOV": salary.salary_month_fr = "Novembre"; break;
                                case "DEC": salary.salary_month_fr = "Décembre"; break;
                            }
                        }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ONA_TAX_AMOUNT")
                    {
                        try { salary.ona_tax_amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IRI_TAX_AMOUNT")
                    {
                        try { salary.iri_tax_amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FDU_TAX_AMOUNT")
                    {
                        try { salary.fdu_tax_amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CAS_TAX_AMOUNT")
                    {
                        try { salary.cas_tax_amount = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR")
                    {
                        try { salary.academic_year = reader.GetInt32(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_ID")
                    {
                        try { salary.academic_year_id = reader.GetInt32(i); } catch { }
                    }                 
                    if (reader.GetName(i).ToUpper() == "DEDUCTION")
                    {
                        try { salary.deduction = reader.GetDouble(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ACADEMIC_YEAR_DESCRIPTION")
                    {
                        try { salary.academic_year_description = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE_NUMBER")
                    {
                        try { salary.phone_number = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "YEARS")
                    {
                        try { salary.years = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FULLNAME")
                    {
                        try { salary.fullname = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "TEACHER_ID")
                    {
                        try { salary.teacher_id = reader.GetValue(i).ToString(); } catch { }
                    }
                }
                listSalary.Add(salary);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listSalary;
    }

    public static void InsertStaffSalaryInfo(Salary s)
    {
        try
        {
            // delete old staff
            RemoveSalaryForStaff(s.staff_code);


            string sql = @"INSERT INTO staff_salary(staff_code,amount)
                                VALUES(?,?)";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.staff_code, s.amount);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveSalaryForStaff(string staffCode)
    {
        try
        {
            string sql = @"delete from staff_salary where staff_code = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateExistingSalaryAmountForStaff(Salary s)
    {
        string sql = @"UPDATE staff_salary set status = 0
                             WHERE upper(staff_code) = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.staff_code.ToUpper());
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void addNewTax(Salary sal)
    {
        string sql = @"INSERT INTO tax_configuration
                           (group_name, ona, iri, fdu, cas, fixed_tax, date_register, login_user_id)
                                VALUES(?, ?, ?, ?, ?, ?, now(), ?)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sal.group_name,
                                sal.ona,
                                sal.iri,
                                sal.fdu,
                                sal.cas,
                                sal.fixed_tax,
                                sal.login_user_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getStaffCurrentSalary(string staff_code)
    {
        string sql = @"select a.* , (select phone1 from staff where id = a.staff_code) as phone_number
                                        from staff_salary a
                                        where upper(a.staff_code) = ?
                                      and a.status = 1";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staff_code);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getSecondarySalaryTeacher(string staffCode, int academicYear, string monthYearPayroll)
    {
        string sql = @"select sum(
                            ((select cp.Price from classroom_cours_management ccm
                                inner join cours_price cp on cp.id = ccm.id_cours_price
                                 where Id_Class = a.Id_class and ccm.Id_Cours = a.Id_cours) *
                            round((TIME_TO_SEC(End_hours) / 3600) - (TIME_TO_SEC(Start_hours) / 3600), 2) *
                            (select count(*) from timesheets 
	                            where staff_code = a.Id_teacher
	                            and id_class > 70 
	                            and id_course = a.Id_cours 
	                            and id_academic_year = a.academic_year 
	                            and presence_status = 1 
	                            and date_format(sheet_date, '%Y%m') = ?))) as amount,
                            (select phone1 from teacher where id = a.Id_teacher) as phone_number
                             from `schedule` a 
                            where a.Id_teacher = ?
                            -- and a.vacation = 'AM'
                            and a.academic_year = ?
                            and a.Id_class  > 70";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(monthYearPayroll, staffCode, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static List<Salary> getFixedSalaryTeacher(string staffCode, int academicYear)
    {
        string sql = @"select sum(amount) as amount,
                            (select phone1 from teacher where id = @staffCode ) as phone_number
                            from classroom_salary_management
                            where classroom_id in(select a.Id_class from `schedule` a where a.Id_teacher = ? 
												and a.academic_year = ?
												and a.Id_class  < 70 -- primary class only
												group by a.Id_class)";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getListBasicAmountHisByStaffCode(string staffCode)
    {
        string sql = @"select * from staff_salary where staff_code = ?
                               order by date_register desc";

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

    public static List<Salary> getListPayrollDetais(string staffCode, int academicYear)
    {
        string sql = @"select a.id, upper(a.salary_month) as salary_month,a.contract_salary, 
                               a.ona as ona_tax_amount, a.iri as iri_tax_amount, a.fdu as fdu_tax_amount,
                                a.cas as cas_tax_amount,a.fixed_tax, a.date_register, 
                                (a.ona + a.iri + a.fdu + a.cas + a.fixed_tax)  as deduction,
                                (a.contract_salary) - (a.ona + a.iri + a.fdu + a.cas + a.fixed_tax) as net_salary,
                                (select concat(extract(year from start_date),'-',extract(year from end_date))
                                 from academic_year where id = a.academic_year) as academic_year_description
                      from staff_payroll a 
                      where staff_code = ? and academic_year = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, academicYear);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool taxGroupNameExist(string group_name)
    {
        bool result = false;

        string sql = @"select count(*) from tax_configuration where upper(group_name) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(group_name);
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

    public static bool StaffPayrollAlreadyExist(Salary sal)
    {
        bool result = false;

        string sql = @"select count(*) from staff_payroll_status 
                        where academic_year_id = ?
                            and salary_month = ?
                            and status = 1";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sal.academic_year_id, sal.salary_month);
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

    public static List<Salary> getListTax()
    {
        string sql = @"select * from tax_configuration
                          ORDER by group_name asc";

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

    public static List<Salary> getListAttachTaxById(int taxId)
    {
        string sql = @"select * from staff_tax where tax_id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(taxId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getListTaxByStaffCode(string staffCode)
    {
        string sql = @"select a.* from tax_configuration a, staff_tax b
                            where a.id = b.tax_id
                            and b.staff_Id = ?";


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

    public static void deleteTaxGroupById(int id)
    {
        string sql = @"delete from tax_configuration where id = ?";

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

    public static void AddStaffToTaxGroup(Salary sal)
    {
        try
        {
            // delete previous tax config
            RemoveStaffPreviousTax(sal.staff_code);

            //
            string sql = @"INSERT INTO staff_tax(staff_code, tax_id)
                                VALUES(?, ?)";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sal.staff_code, sal.tax_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void AddTeacherToTaxGroup(Salary sal)
    {
        try
        {
            // delete previous tax config
            RemoveTeacherPreviousTax(sal.teacher_id);

            //
            string sql = @"INSERT INTO teacher_tax(teacher_id, tax_id)
                                VALUES(?, ?)";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sal.teacher_id, sal.tax_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RemoveStaffPreviousTax(String staffCode)
    {
        try
        {
            // delete previous staff tax group info
            string sql1 = @"DELETE FROM staff_tax
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
    
    public static void RemoveTeacherPreviousTax(String teacherId)
    {
        try
        {
            // delete previous staff tax group info
            string sql1 = @"DELETE FROM teacher_tax
                               where teacher_id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
            stmt.SetParameters(teacherId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void InsertNewPayroll(List<Salary> listSalary)
    {
        try
        {
            if (listSalary != null && listSalary.Count > 0)
            {
                //
                foreach (Salary sal in listSalary)
                {
                    string sql = @"insert into staff_payroll (staff_code, salary_month, 
                                              contract_salary, ona, iri, fdu, cas, fixed_tax,
                                              academic_year_id, date_register, login_user_id)
                                       values(?, ?, ?, ?, ?, ?, ?, ?, ?,now(), ?)";

                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
                    stmt.SetParameters(sal.staff_code,
                                        sal.salary_month,
                                        sal.contract_salary,
                                        sal.ona_tax_amount,
                                        sal.iri_tax_amount,
                                        sal.fdu_tax_amount,
                                        sal.cas_tax_amount,
                                        sal.fixed_tax,
                                        sal.academic_year_id,
                                        sal.login_user_id);
                    stmt.ExecuteNonQuery();

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

    public static void deleteAnnualPayrollByStaffCode(string staff_code, int academic_year)
    {
        try
        {
            string sql = @"delete from staff_payroll 
                                         where staff_code = ? and academic_year = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staff_code, academic_year);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteMonthlyPayrollById(int id)
    {
        try
        {
            string sql = @"delete from staff_payroll where id = @id ";
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getListStaffPayroll(Salary s)
    {
        try
        {
            string sql = @"SELECT  concat(extract(YEAR from acc.start_date),' - ',extract(YEAR from acc.end_date)) as years,
		                    st.id as staff_code, concat(st.last_name,' ', st.First_name) as fullName,
		                    sp.salary_month,
		                    sp.contract_salary,
		                    sp.ona as ona_tax_amount,
		                    sp.iri as iri_tax_amount,
		                    sp.fdu as fdu_tax_amount, 
		                    sp.cas as cas_tax_amount,
		                    sp.fixed_tax,
		                    (sp.ona + sp.iri + sp.fdu + sp.cas + sp.fixed_tax) as deduction,
							(sp.contract_salary - (sp.ona + sp.iri + sp.fdu + sp.cas + sp.fixed_tax)) as net_salary
                            FROM staff_payroll sp
							    left join staff st on st.id = sp.staff_code
							    left join academic_year acc on acc.id = sp.academic_year_id
                            WHERE 1=1
                            AND st.Status = 1
							AND acc.id = ?
							AND sp.salary_month = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(s.academic_year_id, s.salary_month);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getListPayrollHistoricForStaff(string staffCode, int academicYearId)
    {
        try
        {
            string sql = @"SELECT  concat(extract(YEAR from acc.start_date),' - ',extract(YEAR from acc.end_date)) as years,
		                    st.id as staff_code, concat(st.last_name,' ', st.First_name) as fullName,
		                    sp.salary_month,
		                    sp.contract_salary,
		                    sp.ona as ona_tax_amount,
		                    sp.iri as iri_tax_amount,
		                    sp.fdu as fdu_tax_amount, 
		                    sp.cas as cas_tax_amount,
		                    sp.fixed_tax,
		                    (sp.ona + sp.iri + sp.fdu + sp.cas + sp.fixed_tax) as deduction,
							(sp.contract_salary - (sp.ona + sp.iri + sp.fdu + sp.cas + sp.fixed_tax)) as net_salary
                            FROM staff_payroll sp
							    left join staff st on st.id = sp.staff_code
							    left join academic_year acc on acc.id = sp.academic_year_id
                            WHERE 1=1
                            AND st.Status = 1
							AND sp.staff_code = ?
							AND acc.id = ?";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(staffCode, academicYearId);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Salary> getStaffSalaryInfoForPayroll()
    {
        try
        {
            string sql = @"select a.id as staff_code, 
                            concat(a.last_name,' ', a.First_name) as fullName,
                            b.amount  as contract_salary, 
                            d.ona,
                            d.iri,
                            d.fdu,
                            d.cas,
                            d.fixed_tax
                            from staff a
                            left join staff_salary b on b.staff_code = a.id
                            left join staff_tax c on c.staff_code = a.id
                            left join tax_configuration d on d.id = c.tax_id
                            where a.status = 1";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void InsertStaffPayrollStatus(Salary sal)
    {
        try
        {
            string sql = @"INSERT INTO staff_payroll_status(academic_year_id, 
                                salary_month, status, login_user_id, date_register)
                           VALUES(?,?,?,?, now())";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(sal.academic_year_id, sal.salary_month, sal.status, sal.login_user_id);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




}