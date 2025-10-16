using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
// using System.Data.OracleClient;
using System.Threading;
using MySql.Data.MySqlClient;




namespace Db_Core
{
    public class SqlStatement
    {
        private String strSQL;
        private List<int> questionMarkPos = new List<int>();
        private object[] values;
        private string checkConnectionCmd = "select 1 from dual";

        private string connectionString;

        private SqlStatement() { }

        public SqlStatement(string strSQL, SqlConnString sqlConn)
        {
            this.strSQL = strSQL;
            this.connectionString = SqlCommon.GetConnectionString(sqlConn);
            int openSquareBracketCount = 0, closeSquareBracketCount = 0;
            int firstOpenSquareBracketPos = -2, firstCloseSquareBrackPos = -1;
            for (int i = 0; i < strSQL.Length; i++)
            {
                if (strSQL[i] == '?')
                    questionMarkPos.Add(i);
                if (strSQL[i] == '[')
                    openSquareBracketCount++;
                if (strSQL[i] == ']')
                    closeSquareBracketCount++;
            }

            // check validate SQL template
            if (openSquareBracketCount != closeSquareBracketCount || firstOpenSquareBracketPos > firstCloseSquareBrackPos)
                throw new Exception("SQL template incorrect");

            values = new object[questionMarkPos.Count];
            for (int i = 0; i < values.Length; i++)
                values[i] = Missing.Value;
        }

        public static SqlStatement FromString(string strSQL, SqlConnString sqlConn)
        {
            return new SqlStatement(strSQL, sqlConn);
        }

        public static SqlStatement FromFile(string sqlFilePath, SqlConnString sqlConn)
        {
            StreamReader sr = new StreamReader(sqlFilePath);
            String str = sr.ReadToEnd();
            sr.Close();
            return FromString(str, sqlConn);
        }

        public static String GetSqlFromFile(string sqlFilePath)
        {
            StreamReader sr = new StreamReader(sqlFilePath);
            String str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        public void SetParameter(int index, object value)
        {
            values[index] = value;
        }

        public void SetParameters(params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
                SetParameter(i, args[i]);
        }

        public string GetSQL()
        {
            StringBuilder result = new StringBuilder(strSQL);
            for (int i = 0; i < questionMarkPos.Count; i++)
                if (!(values[i] is Missing))
                {
                    int pos = questionMarkPos[i];
                    int leftBracketPos = pos - 1;
                    while (leftBracketPos >= 0 && result[leftBracketPos] != '[' && result[leftBracketPos] != ']')
                        leftBracketPos--;
                    if (leftBracketPos == -1)
                        continue;
                    if (result[leftBracketPos] == ']')
                        continue;
                    int rightBracketPos = pos + 1;
                    while (rightBracketPos < result.Length && result[rightBracketPos] != '[' && result[rightBracketPos] != ']')
                        rightBracketPos++;
                    if (rightBracketPos == result.Length)
                        continue;
                    if (result[rightBracketPos] == '[')
                        continue;
                    result[leftBracketPos] = ' ';
                    result[rightBracketPos] = ' ';
                }

            bool replace = false;
            for (int i = 0; i < result.Length; i++)
            {
                char ch = result[i];
                if (ch == '[')
                    replace = true;
                if (replace)
                    result[i] = ' ';
                if (ch == ']')
                    replace = false;
            }

            int whereIndex = result.ToString().IndexOf("where", StringComparison.OrdinalIgnoreCase);
            for (int i = questionMarkPos.Count - 1; i >= 0; i--)
                if (!(values[i] is Missing))
                {
                    String temp = null;
                    if (values[i] == null)
                    {
                        for (int j = questionMarkPos[i] - 1; j >= 0; j--)
                        {
                            if (result[j] == ' ')
                                continue;
                            if (result[j] == '=')
                            {
                                if (whereIndex < j)
                                {
                                    result[j] = ' ';
                                    temp = "is null";
                                }
                                else
                                    temp = "null";
                                break;
                            }
                            else
                                temp = "null";
                        }
                    }
                    else if (values[i] is Enum)
                        temp = ((int)values[i]).ToString();
                    else if (values[i] is string)
                        temp = "'" + values[i].ToString().Replace("'", "''") + "'";
                    else if (values[i] is DateTime)
                        temp = "TO_DATE('" + ((DateTime)values[i]).ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy hh24:mi:ss')";
                    else
                        temp = values[i].ToString();
                    result.Remove(questionMarkPos[i], 1).Insert(questionMarkPos[i], temp);
                }

            return result.ToString();
        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection conn = null;
            MySqlCommand command = null;
            int retry = 0;
            while (conn == null && retry < 3)
            {
                try
                {
                    conn = new MySqlConnection(connectionString);
                    if (conn == null)
                        throw new Exception("Too many connections to database");
                    try
                    {
                        conn.Open();
                    }
                    catch(Exception ex) { throw ex; }
                    //test command
                    command = new MySqlCommand(checkConnectionCmd, conn);
                    command.ExecuteReader().Close();
                    return conn;
                }
                catch
                {
                    retry++;
                    try
                    {
                        if (conn != null)
                        {
                            if (conn.State != ConnectionState.Closed)
                                conn.Close();
                        }
                    }
                    catch { }
                    finally
                    {
                        conn = null;
                    }
                }
            }
            return conn;
        }

        public MySqlDataReader ExecuteReader()
        {
            MySqlConnection conn = null;
            MySqlCommand command = null;
            string sql = GetSQL();
            try
            {
                conn = GetConnection();
                command = new MySqlCommand(sql, conn);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                if (conn != null)
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                throw e;
            }
        }

        public object ExecuteScalar()
        {
            MySqlConnection conn = null;
            string sql = GetSQL();
            try
            {
                conn = GetConnection();
                MySqlCommand command = new MySqlCommand(sql, conn);
                object result = command.ExecuteScalar();
                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                if (conn != null)
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                throw e;
            }
        }

        public int ExecuteNonQuery()
        {
            MySqlConnection conn = null;
            string sql = GetSQL();
            try
            {
                conn = GetConnection();
                MySqlCommand command = new MySqlCommand(sql, conn);
                int result = command.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                if (conn != null)
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                throw e;
            }
        }
    }
}
