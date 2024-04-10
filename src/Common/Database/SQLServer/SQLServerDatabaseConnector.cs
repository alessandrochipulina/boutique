using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Common.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common.Database.SQLServer
{
    public class SQLServerDatabaseConnector : IDatabaseConnector
    {
        private string connectionString;

        public SQLServerDatabaseConnector(){}

        public void Configure(ApiGlobalConfiguration parameters)
        {
            var host = parameters.GetObjectByAbsoluteKey("SQL_HOST");
            var port = parameters.GetObjectByAbsoluteKey("SQL_PORT");
            var password = parameters.GetObjectByAbsoluteKey("SQL_PASSWORD");
            var userid = parameters.GetObjectByAbsoluteKey("SQL_USER");
            var databaseName = parameters.GetObjectByAbsoluteKey("SQL_DATABASENAME");

            this.connectionString =
                $"Data Source={host}; " +
                $"Initial Catalog={databaseName}; " +
                $"User ID={userid}; " +
                $"Password={password}; " +
                "Connect Timeout=30; " +
                "Encrypt=False; ";
                // "Trust Server Certificate=True; " +
                // "Application Intent=ReadWrite; " +
                // "Multi Subnet Failover=False; ";
        }

        public SqlConnection GetNewConnection()
        {
            return new SqlConnection(this.connectionString);
        }          

        private void safeOpen(SqlConnection connection)
        {
            if (connection == null)
            {
                Log.Logger.Debug("connection is null. Reconfiguring");
                connection = this.GetNewConnection();
            }
            if (connection.State == ConnectionState.Closed)
            {
                Log.Logger.Debug("connection is closed. Opening");
                connection.Open();
            }
        }

        private void safeClose(SqlConnection connection)
        {
            if (connection == null)
            {
                Log.Logger.Debug("connection is null. Close will not be executed");
            }
            else
            {
                Log.Logger.Debug("current state before its close: " + connection.State);
                connection.Close();
            }
        }

        public Dictionary<string, object> ConnectionValidationQuery()
        {
            Dictionary<string, object> databaseMetrics = new Dictionary<string, object>();
            long before = (long)(DateTime.UtcNow).Millisecond;
            long after = 0;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT 1";
                decimal result = (Int32)cmd.ExecuteScalar();
                databaseMetrics.Add("stabilizedConnection", (((int)result) == 1));
                after = (long)(DateTime.UtcNow).Millisecond;
            }
            catch (SqlException ex)
            {
                string errorMessage = "Code: " + ex.Number + "\n" + "Message: " + ex.Message;
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "An exception occurred. Please contact your system administrator.");
                databaseMetrics.Add("codeSQLServerError", ex.Number);

            }
            catch (Exception ex)
            {
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "Failed to validate connection using: select 1");
            }
            finally
            {
                safeClose(connection);
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                }
            }
            databaseMetrics.Add("responseTime", (after - before) / 1000);
            return databaseMetrics;
        }

        public List<T> Query<T>(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var collection = isProc
                    ? connection.Query<T>(query, parameters, commandType: CommandType.StoredProcedure).ToList()
                    : connection.Query<T>(query, parameters).ToList();

                return collection;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public List<Dictionary<string, object>> SimpleQuery(string table)
        {
            var regexItem = new Regex("^[a-zA-Z0-9_\\[\\]]*$");

            if (!regexItem.IsMatch(table))
            {
                throw new Exception("Table name has not allowed characters:" + table);
            }

            var rows = new List<Dictionary<string, object>>();
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                SqlCommand cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT * FROM " + table;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    rows.Add(row);
                }

                return rows;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to fetch data from table:" + table, exception);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public string Query(string query, object parameters = null, bool isProc = true)//Ejecuta stored o sentencias que no devuelven un resultado
        {

            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);
                if (isProc)
                {
                    connection.Query(query, parameters, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    connection.Query(query, parameters);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
            return "OK";
        }

        public List<T> Function<T>(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var collection = isProc
                    ? connection.Query<T>(query, parameters, commandType: CommandType.Text).ToList()
                    : connection.Query<T>(query, parameters).ToList();
                return collection;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute function:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public T FirstOrDefault<T>(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var entity = isProc
                    ? connection.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.QueryFirstOrDefault<T>(query, parameters);

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query with one result:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public void FirstObjectSecondList<TFirst, TSecond>(string query, ref TFirst param1, ref List<TSecond> param2, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var collection = isProc
                    ? connection.QueryMultiple(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.QueryMultiple(query, parameters);

                param1 = collection.Read<TFirst>().First();
                param2 = collection.Read<TSecond>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query with one result:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public int Execute(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var affectedRows = isProc
                    ? connection.Execute(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.Execute(query, parameters);

                return affectedRows;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public DataTable ExecuteDT(string query, List<SqlParameter> parameters, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parm in parameters)
                {
                    cmd.Parameters.Add(parm);
                }
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute DT :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public void ExecuteVoid(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                var affectedRows = isProc
                     ? connection.Execute(query, parameters, commandType: CommandType.StoredProcedure)
                     : connection.Execute(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute void query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public int QueryRowAffected(string query, object parameters = null, bool isProc = true)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);
                int affectedRows = 0;
                if (isProc)
                {
                    affectedRows = connection.Query<int>(query, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
                else
                {
                    affectedRows = connection.Query<int>(query, parameters).SingleOrDefault();
                }

                return affectedRows;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public object ExecuteQueryOutputParemeter(string query, object parameters, bool isOracle, string outputParemeterName)
        {
            SqlConnection connection = null;
            try
            {
                connection = this.GetNewConnection();
                safeOpen(connection);

                connection.Query<int>(query, parameters, commandType: CommandType.StoredProcedure);

                //if (isOracle)
                //{
                //    return ((OracleDynamicParameters)parameters).Get(outputParemeterName);
                //}
                //else
                // {
                    return ((SQLServerDynamicParameters)parameters).Get(outputParemeterName);
                // }

            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

    }
}
