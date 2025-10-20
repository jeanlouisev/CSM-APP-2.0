using AjaxControlToolkit;
using Db_Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Utilities;


public class StaffDocuments
{
    public int id { get; set; }
    public string staff_id { get; set; }
    public string document_name { get; set; }
    public int document_type_id { get; set; }
    public DateTime upload_time { get; set; }

    public static List<StaffDocuments> Parse(MySqlDataReader reader)
    {
        List<StaffDocuments> listDocuments = new List<StaffDocuments>();
        try
        {
            while (reader.Read())
            {
                StaffDocuments stDoc = new StaffDocuments();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        try { stDoc.document_type_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_ID")
                    {
                        try { stDoc.staff_id = reader.GetValue(i).ToString(); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DOCUMENT_NAME")
                    {
                        try { stDoc.document_name = reader.GetValue(i).ToString(); }
                        catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "DOCUMENT_TYPE_ID")
                    {
                        try { stDoc.document_type_id = int.Parse(reader.GetValue(i).ToString()); } catch { }
                    }
                    if (reader.GetName(i).ToUpper() == "UPLOAD_TIME")
                    {
                        try { stDoc.upload_time = DateTime.Parse(reader.GetValue(i).ToString()); }
                        catch { }
                    }
                }
                listDocuments.Add(stDoc);
            }
            //close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            MessBox.Show("Error : " + ex.Message);
        }
        return listDocuments;
    }

    public static void uploadStaffDocuments(List<StaffDocuments> listDocumentsAttach, string staffId)
    {
        try
        {
            if (listDocumentsAttach != null && listDocumentsAttach.Count > 0)
            {
                // remove existing documents for current student
                string sql1 = @"DELETE FROM STUDENT_DOCUMENTS 
                                WHERE student_id = ?";

                SqlStatement stmt = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
                stmt.SetParameters(staffId);
                stmt.ExecuteNonQuery();


                foreach (StaffDocuments doc in listDocumentsAttach)
                {
                    string sql2 = @"INSERT INTO STUDENT_DOCUMENTS(student_id,document_path,
                                    document_type_id, upload_time) VALUES(?,?,?,now())";

                    stmt = SqlStatement.FromString(sql2, SqlConnString.CSM_APP);
                    stmt.SetParameters(staffId, doc.document_name, doc.document_type_id);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static List<StaffDocuments> getListUploadedDocuments(StaffDocuments doc)
    {
        string sql = @"select * from student_documents
                       WHERE 1=1 
                        [ and student_id = ? ]";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (doc.staff_id != null)
            {
                stmt.SetParameter(0, doc.staff_id.ToUpper());
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<StaffDocuments> getListDocumentsByStaffCode(string staffCode)
    {
        string sql = @"SELECT a.* FROM documents a
                                WHERE a.staff_code = ?";

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

    public static void deleteDocumentById(int id)
    {
        string sql = @"delete from documents where id = ?";
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

    public static void deleteDocumentsById(string documentId)
    {
        string sql = @"delete from documents where id = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(documentId);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void deleteDocumentsByCode(string documentName, string staffCode)
    {
        string sql = @"delete from documents where document_name = ? and staff_code = ?";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(documentName, staffCode);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<StaffDocuments> getListDocumentType()
    {
        string sql = @"SELECT a.*
                            FROM student_document_type a
                            ORDER BY a.description asc";

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



}