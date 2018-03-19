using Itn.Utilities.Enums;
using Itn.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Itn.Utilities
{
    public class SqlProvider
    {
     
        public static IEnumerable<T> GetReaderData<T>(IDataReader reader, Func<IDataReader, T> readerBuilder)
        {
            try
            {
                while (reader.Read())
                {
                    yield return readerBuilder(reader);
                }
            }
            finally
            {
                reader.Dispose();
            }
        }

        public static IEnumerable<T> GetData<T>(IDataReader reader, Func<IDataRecord, T> buildObject)
        {
            try
            {
                while (reader.Read())
                {
                    yield return buildObject(reader);
                }
            }
            finally
            {
                reader.Dispose();
            }
            //call it like this:
            //var result = GetData(YourLibraryFunction(), Employee.Create);
        }

        public static SqlConnection GetOpenSqlConn(string connString, ConnectionStringType connStringType)
        {
            string fullConnString;
            switch (connStringType)
            {
                case (ConnectionStringType.ConnectionString):
                    fullConnString = connString;
                    break;
                case (ConnectionStringType.DBKey):
                    fullConnString = GetConnString(connString);
                    break;
                default:
                    throw new InvalidEnumException("{0} is not a valid ConnectionStringType");
            }
            return GetOpenSqlConn(fullConnString);
        }

        public static SqlConnection GetOpenSqlConn(string connString)
        {
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        private static string GetConnString(string dbKey)
        {
            return AppConfig.GetConfigVal(dbKey);
        }

        /// <summary>
        /// Returns a SQLDataReader object based on a SQL Statement
        /// </summary>
        /// <param name="dbKey">AppSettings key which contains the value of the SQL Connection String</param>
        /// <param name="sqlString">SQL String which will return records to fill DataReader</param>
        /// <returns></returns>
        public static SqlDataReader GetSqlDataReader(string dbKey, string sqlString)
        {
            return GetSqlDataReader(dbKey, ConnectionStringType.DBKey, sqlString);
        }

        /// <summary>
        /// Returns a SQLDataReader object based on a SQL statement
        /// </summary>
        /// <param name="connString">Either a SQL connection string or a configuration key representing the connection string</param>
        /// <param name="connStringType">ConnString or DBKey</param>
        /// <param name="sqlString">SQL Select statement that returns records to fill DataReader.</param>
        /// <returns>SQLDataReader</returns>
        public static SqlDataReader GetSqlDataReader(string connString, ConnectionStringType connStringType, string sqlString)
        {
            using (var cm = new SqlCommand(sqlString, GetOpenSqlConn(connString, connStringType)))
            {
                return cm.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public static SqlDataReader GetSqlDataReader(string dbConnKey, string sqlString, string sqlParamName, object sqlParamValue)
        {
            return SqlProvider.GetSqlDataReader(dbConnKey, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sqlString,
                    new List<SqlParameter> { SqlParamWrapper.Create(sqlParamName, sqlParamValue) }));
        }

        public static SqlDataReader GetSqlDataReader(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            using (var cm = sqlParamStatement.ToSqlCommand(GetOpenSqlConn(connString, connStringType)))
            {
                return cm.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public static SqlDataReader GetSqlDataReaderFromSp(string connString, ConnectionStringType connStringType, string spName, SqlParamStatement sqlParamStatement)
        {
            using (var cm = sqlParamStatement.ToSqlSpCommand(GetOpenSqlConn(connString, connStringType), spName))
            {
                return cm.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public static void ExecSqlNoQueryFromSp(string connString, ConnectionStringType connStringType, string spName, SqlParamStatement sqlParamStatement)
        {
            var conn = GetOpenSqlConn(connString, connStringType);
            try
            {
                using (var cm = sqlParamStatement.ToSqlSpCommand(conn, spName))
                {
                    cm.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }


        public static void ExecSqlNoQuery(string connDbKey, string sqlCommandString)
        {
            ExecSqlNoQuery(connDbKey, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sqlCommandString));
        }
        public static void ExecSqlNoQuery(string connDbKey, string sqlString, string paramName, object paramValue)
        {
            ExecSqlNoQuery(connDbKey, ConnectionStringType.DBKey, SqlParamStatement.Create(sqlString, new List<SqlParameter> { SqlParamWrapper.Create(paramName, paramValue) }));
        }

        public static void ExecSqlNoQuery(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            var conn = GetOpenSqlConn(connString, connStringType);
            try
            {
                using (var cm = sqlParamStatement.ToSqlCommand(conn))
                {
                    cm.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static string ExecSqlNoQueryWithIdentity(string connString, string sqlCommandString)
        {
            return ExecSqlNoQueryWithIdentity(connString, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sqlCommandString));
        }
        public static void ExecSqlNoQueryUsingTran(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            var conn = GetOpenSqlConn(connString, connStringType);
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                using (var cm = sqlParamStatement.ToSqlCommand(conn))
                {
                    cm.Transaction = trans;
                    cm.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }



        public static string ExecSqlNoQueryWithIdentity(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            var conn = GetOpenSqlConn(connString, connStringType);
            try
            {
                sqlParamStatement.AddScopeIdentity();
                using (var cm = sqlParamStatement.ToSqlCommand(conn))
                {
                    //cm.Prepare();  // Must use explicit types in parameters for this to work
                    return cm.ExecuteScalar().ToString();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static string DebugSql(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            var conn = GetOpenSqlConn(connString, connStringType);
            try
            {
                sqlParamStatement.AddScopeIdentity();
                using (var cm = sqlParamStatement.ToSqlCommand(conn))
                {
                    //cm.Prepare();  // Must use explicit types in parameters for this to work
                    return SqlStatementFromParamCommandText(cm);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private static string SqlStatementFromParamCommandText(SqlCommand cmd)
        {
            var quotedParameterTypes = new[] {
             DbType.AnsiString, DbType.Date,
             DbType.DateTime, DbType.Guid, DbType.String,
             DbType.AnsiStringFixedLength, DbType.StringFixedLength
};
            string query = cmd.CommandText;

            var arrParams = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(arrParams, 0);

            foreach (var p in arrParams.OrderByDescending(p => p.ParameterName.Length))
            {
                var value = p.Value.ToString();
                if (quotedParameterTypes.Contains(p.DbType))
                    value = "'" + value + "'";
                query = query.Replace(p.ParameterName, value);
            }
            return query;
        }

        public static string GetSqlFieldValueAsString(string connString, string sqlCommandString, string fldName)
        {
            return GetSqlFieldValueAsString(connString, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sqlCommandString),
                fldName);
        }

        /// <summary>
        /// Returns a String based on the value of a single field represented by the SQL Select string.
        /// </summary>
        /// <param name="connString">Either a SQL connection string or a configuration key representing the connection string</param>
        /// <param name="connStringType">ConnString or DBKey</param>
        /// <param name="sqlParamStatement"> </param>
        /// <param name="fldName">The name of the field in the result set.</param>
        /// <returns>Field value string. If more than one record is pulled by the SQL query, then 
        /// only the field value of the first record is returned. If no record is found, returns null</returns>
        public static string GetSqlFieldValueAsString(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement, string fldName)
        {
            var dr = GetSqlDataReader(connString, connStringType, sqlParamStatement);
            try
            {
                if (!dr.HasRows) return null;
                dr.Read();
                return dr[fldName].ToString();
            }
            finally
            {
                dr.Close();
            }
        }

        public static List<string> GetSqlFieldValueAsList(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement, string fldName)
        {
            var result = new List<string>();
            var dr = GetSqlDataReader(connString, connStringType, sqlParamStatement);
            try
            {
                while (dr.Read())
                {
                    result.Add(dr[fldName].ToString());
                }
                return result;
            }
            finally
            {
                dr.Close();
            }
        }


        public static List<KeyValuePair<string, string>> GetSqlKeyValuePairList(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement, string keyFldName, string valueFldName, bool stripValueSpecialCharacters = false)
        {
            var result = new List<KeyValuePair<string, string>>();
            var dr = GetSqlDataReader(connString, connStringType, sqlParamStatement);
            try
            {
                while (dr.Read())
                {
                    var code = dr[keyFldName].ToString();
                    var descript = dr[valueFldName].ToString();
                    if (stripValueSpecialCharacters) descript = descript.RemoveSpecialCharacters();
                    result.Add(new KeyValuePair<string, string>(code, descript));
                }
                return result;
            }
            finally
            {
                dr.Close();
            }
        }

        public static Dictionary<string, string> GetSqlDictionary(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement, string keyFldName, string valueFldName, bool stripValueSpecialCharacters = false)
        {
            var result = new Dictionary<string, string>();
            var dr = GetSqlDataReader(connString, connStringType, sqlParamStatement);
            try
            {
                while (dr.Read())
                {
                    var code = dr[keyFldName].ToString();
                    var descript = dr[valueFldName].ToString();
                    if (stripValueSpecialCharacters) descript = descript.RemoveSpecialCharacters();
                    if (!result.ContainsKey(code)) result.Add(code, descript);
                }
                return result;
            }
            finally
            {
                dr.Close();
            }
        }

        public static bool SqlExists(string connDbKey, string sqlCommandString)
        {
            return SqlExists(connDbKey, ConnectionStringType.DBKey,
                SqlParamStatement.Create(sqlCommandString));
        }

        public static bool SqlExists(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            sqlParamStatement.WrapInExistsClause();
            var exists = GetSqlFieldValueAsString(connString, connStringType, sqlParamStatement, "isExists");
            return (exists == "1");
        }

        public static int SqlCount(string connString, ConnectionStringType connStringType, SqlParamStatement sqlParamStatement)
        {
            const string COUNT_AS = "REC_COUNT";
            sqlParamStatement.WrapInCountClause(COUNT_AS);
            return Convert.ToInt32(GetSqlFieldValueAsString(connString, connStringType, sqlParamStatement, COUNT_AS));
        }



        public static string GetSqlOutputValueFromSp(string connString, ConnectionStringType connStringType, string spName, SqlParamStatement sqlParamStatement)
        {
            using (var cm = sqlParamStatement.ToSqlSpCommand(GetOpenSqlConn(connString, connStringType), spName))
            {
                cm.CommandType = CommandType.StoredProcedure;
                var returnParameter = cm.Parameters.Add("@RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.Output;
                cm.ExecuteNonQuery();
                return returnParameter.Value.ToString();
            }
        }

        public static string GetSqlOutputValueFromSpUsingParam(string connString, ConnectionStringType connStringType, string spName, SqlParamStatement sqlParamStatement, string outputParamName, string paramType)
        {

            using (var cm = sqlParamStatement.ToSqlSpCommand(GetOpenSqlConn(connString, connStringType), spName))
            {
                cm.CommandType = CommandType.StoredProcedure;
                var returnParameter = cm.Parameters.Add(outputParamName, GetParamType(paramType));
                returnParameter.Direction = ParameterDirection.Output;
                cm.ExecuteNonQuery();
                return returnParameter.Value.ToString();
            }
        }

        public static SqlDbType GetParamType(string paramType)
        {
            switch (paramType)
            {
                case "Integer":
                    return SqlDbType.Int;
                case "DateTime":
                    return SqlDbType.DateTime;
                default:
                    return SqlDbType.VarChar;
            }

        }




    }
}
