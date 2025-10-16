using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Utilities;
using Db_Core;
using System.Data;


public class Documents
{
    public int id { get; set; }
    public string staff_code { get; set; }
    public string document_path { get; set; }
    public string document_name { get; set; }
    public string description { get; set; }
    public DateTime upload_time { get; set; }

    public static List<Documents> Parse(MySqlDataReader reader)
    {
        List<Documents> listDocuments = new List<Documents>();
        try
        {
            while (reader.Read())
            {
                Documents documents = new Documents();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).ToUpper() == "ID")
                    {
                        documents.id = int.Parse(reader.GetString(i));
                    }
                    if (reader.GetName(i).ToUpper() == "STAFF_CODE")
                    {
                        documents.staff_code = reader.GetString(i);
                    }
                    if (reader.GetName(i).ToUpper() == "DOCUMENT_PATH")
                    {
                        documents.document_path = reader.GetString(i);
                    }
                    if (reader.GetName(i).ToUpper() == "DOCUMENT_NAME")
                    {
                        documents.document_name = reader.GetString(i);
                    }
                    if (reader.GetName(i).ToUpper() == "DESCRIPTION")
                    {
                        documents.description = reader.GetString(i);
                    }
                    if (reader.GetName(i).ToUpper() == "UPLOAD_TIME")
                    {
                        documents.upload_time = reader.GetDateTime(i);
                    }
                }
                listDocuments.Add(documents);
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

    public static void uploadDocument(List<Documents> listDocumentsAttach)
    {
        try
        {
            if (listDocumentsAttach != null && listDocumentsAttach.Count > 0)
            {
                // remove existing documents for current student
                string sql1 = @"DELETE FROM DOCUMENTS 
                                WHERE staff_code = ?";

                SqlStatement stmt = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
                stmt.SetParameters(listDocumentsAttach[0].staff_code.ToUpper());
                stmt.ExecuteNonQuery();


                foreach (Documents doc in listDocumentsAttach)
                {
                    string sql2 = @"INSERT INTO DOCUMENTS(staff_code,document_path,
                                    document_name, upload_time) VALUES(?,?,?,now())";

                    stmt = SqlStatement.FromString(sql2, SqlConnString.CSM_APP);
                    stmt.SetParameters(doc.staff_code.ToUpper(), doc.document_path, doc.document_name);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void uploadSudentDocuments(List<Documents> listDocumentsAttach, string studentId)
    {
        try
        {
            if (listDocumentsAttach != null && listDocumentsAttach.Count > 0)
            {
                // remove existing documents for current student
                string sql1 = @"DELETE FROM STUDENT_DOCUMENTS 
                                WHERE student_id = ?";

                SqlStatement stmt = SqlStatement.FromString(sql1, SqlConnString.CSM_APP);
                stmt.SetParameters(studentId);
                stmt.ExecuteNonQuery();


                foreach (Documents doc in listDocumentsAttach)
                {
                    string sql2 = @"INSERT INTO STUDENT_DOCUMENTS(student_id,document_path,
                                    document_type_id, upload_time) VALUES(?,?,?,now())";

                    stmt = SqlStatement.FromString(sql2, SqlConnString.CSM_APP);
                    stmt.SetParameters(doc.staff_code.ToUpper(), doc.document_path, doc.document_name);
                    stmt.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void uploadDocument(Documents doc)
    {
        try
        {
            string sql = @"INSERT INTO DOCUMENTS(staff_code,document_path,
                           document_name, upload_time) 
                         VALUES(?,?,?,?)";

            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            stmt.SetParameters(doc.staff_code.ToUpper(), doc.document_path,
                                doc.document_name, doc.upload_time);
            stmt.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Documents> getListUploadedDocuments(Documents doc)
    {
        string sql = @"select * from documents
                       WHERE 1=1 
                        [ and staff_code = ? ]";

        try
        {
            SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.CSM_APP);
            if (doc.staff_code != null)
            {
                stmt.SetParameter(0, doc.staff_code.ToUpper());
            }

            return Parse(stmt.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<Documents> getListDocumentsByStaffCode(string staffCode)
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
    
    public static List<Documents> getListDocumentCategory()
    {
        string sql = @"SELECT a.*
                            FROM document_category a
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