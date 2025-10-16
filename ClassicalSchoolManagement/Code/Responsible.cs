using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Responsible
{
    public string staff_code { get; set; }
    public string staff_code_reference { get; set; }
    public string id_card { get; set; }
    public DateTime update_time { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string sex { get; set; }
    public string sex_code { get; set; }
    public string marital_status { get; set; }
    public string marital_status_code { get; set; }
    public string phone { get; set; }
    public string adress { get; set; }
    public string relationship { get; set; }
    public string image_path { get; set; }
    public string reference_code { get; set; }
    public string code { get; set; }


    public static List<Responsible> Parse(MySqlDataReader reader)
    {
        List<Responsible> listResponsible = new List<Responsible>();
        try
        {
            while (reader.Read())
            {
                Responsible responsible = new Responsible();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        try { responsible.staff_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE_REFERENCE")
                    {
                        try { responsible.staff_code_reference = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ID_CARD")
                    {
                        try { responsible.id_card = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "UPDATE_TIME")
                    {
                        try { responsible.update_time = reader.GetDateTime(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "FIRST_NAME")
                    {
                        try { responsible.first_name = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "LAST_NAME")
                    {
                        try { responsible.last_name = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try { responsible.sex = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "SEX")
                    {
                        try { responsible.sex_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS")
                    {
                        try { responsible.marital_status = reader.GetString(i); } catch { }
                        //try
                        //{
                        //    switch (reader.GetString(i))
                        //    {
                        //        case "C": responsible.marital_status = "Célibataire"; break;
                        //        case "M": responsible.marital_status = "Marié(e)"; break;
                        //        case "D": responsible.marital_status = "Divorcé(e)"; break;
                        //        case "V": responsible.marital_status = "Veuf(ve)"; break;
                        //    }
                        //}
                        //catch { }

                    }
                    if (reader.GetName(i).ToUpper() == "MARITAL_STATUS_CODE")
                    {
                        try { responsible.marital_status_code = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "PHONE")
                    {
                        try { responsible.phone = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "ADRESS")
                    {
                        try { responsible.adress = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "RELATIONSHIP")
                    {
                        try { responsible.relationship = reader.GetString(i); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "IMAGE_PATH")
                    {
                        try { responsible.image_path = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "REFERENCE_CODE")
                    {
                        try { responsible.reference_code = reader.GetString(i); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "CODE")
                    {
                        try { responsible.code = reader.GetString(i); }
                        catch { }
                    }
                }

                listResponsible.Add(responsible);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listResponsible;
    }

    public static List<Responsible> checkExistedIdCard(string idCard)
    {
        string sql = @"SELECT  a.*
                             FROM reference_external_information a
                             WHERE 1=1
                                AND a.id_card =?
                             ";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(idCard);
            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Responsible> getlistInternalReferenceByStaffCode(string staff_code)
    {
        string sql = @"SELECT  a.*  FROM reference_internal a
                             WHERE  a.staff_code = ?";

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

    public static List<Responsible> getlistExternalReferenceByStaffCode(string code)
    {
        List<Responsible> listResult = null;


        string sql = @"select a.code as reference_code, a.first_name, a.last_name, a.sex,
                            a.marital_status, a.phone, a.adress, a.update_time, a.image_path,
                            b.staff_code, b.relationship, a.id_card, 
                            case when a.marital_status = 'C' then 'Célibataire'
                                 when a.marital_status = 'M' then 'Marié(e)'
                                 when a.marital_status = 'D' then 'Divorcé(e)'
                                 when a.marital_status = 'V' then 'Veuf(ve)'
                            end as marital_status_code
                            from reference_external_information a, reference_external b
                            where a.code = b.reference_code
                            and b.staff_code = ?";

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

    public static void addResponsible(Responsible r)
    {
        string sql = @"INSERT INTO reference_external_information
                                VALUES( ?, -- idCard,
                                        ?, -- firstName,
                                        ?, -- lastName,
                                        ?, -- sex,
                                        ?, -- maritalStatus,
                                        ?, -- phone,
                                        ?, -- adress,
                                        ?, -- relationship,
                                        now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(r.id_card,
                                r.first_name,
                                r.last_name,
                                r.sex,
                                r.marital_status,
                                r.phone,
                                r.adress,
                                r.relationship);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //this method update the reference_external table which deals with
    //the link between staff,student or teacher...
    //connected to an external source

    public static void updateExternalRelation(Responsible r)
    {
        string sql = @"INSERT INTO reference_external
                                VALUES(?, -- staffCode
                                        ?, -- idCard
                                        now())";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(r.staff_code, r.id_card);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateInternalRelation(Responsible r)
    {
        string sql = @"INSERT INTO reference_external
                                VALUES(?, -- staffCode,
                                        ?, -- staffCodeReference,
                                        now())";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(r.staff_code, r.staff_code_reference);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void updateReference(Responsible r)
    {

        bool result = false;

        string sql = @"INSERT INTO reference_external_information 
                            VALUES (?, -- idCardNumber,
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
            stmt.SetParameters(r.id_card,
                                r.first_name,
                                r.last_name,
                                r.sex,
                                r.marital_status,
                                r.phone,
                                r.adress,
                                r.image_path);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void addExternalStaffInformation(Responsible p)
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
            stmt.SetParameters(p.code,
                                p.id_card,
                                p.first_name,
                                p.last_name,
                                p.sex,
                                p.marital_status,
                                p.phone,
                                p.adress,
                                p.image_path);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Responsible> getListExternalPrentByIdCard(string cardId)
    {
        string sql = @"SELECT a.*
                            FROM reference_external_information a
                            WHERE trim(a.id_card) = ?";
        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(cardId);
            return Parse(stmt.ExecuteReader());
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

    public static void removeExternalReference(String staffCode, string referenceCode, string relationship)
    {
        string sql = @"DELETE FROM reference_external
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

}