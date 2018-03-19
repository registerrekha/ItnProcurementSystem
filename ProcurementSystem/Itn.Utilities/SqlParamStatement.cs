using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities
{
    public class SqlParamStatement
    {
        public string SqlCommandString { get; set; }
        public List<SqlParameter> SqlParams { get; set; }


        // for queries that have no parameters
        public static SqlParamStatement Create(string sqlCommandString)
        {
            return new SqlParamStatement { SqlCommandString = sqlCommandString, SqlParams = new List<SqlParameter>() };
        }

        public static SqlParamStatement Create(string sqlCommandString, string paramName, object paramValue)
        {
            return Create(sqlCommandString,
                new List<SqlParameter> { SqlParamWrapper.Create(paramName, paramValue) });
        }
        public static SqlParamStatement Create(string sqlCommandString, List<SqlParameter> sqlParams)
        {
            return new SqlParamStatement { SqlCommandString = sqlCommandString, SqlParams = sqlParams };
        }



        public SqlCommand ToSqlCommand(SqlConnection conn)
        {
            var cmd = new SqlCommand(SqlCommandString, conn);
            foreach (var sqlParam in SqlParams)
            {
                if (SqlCommandString.Contains(sqlParam.ParameterName))
                {
                    //cmd.Parameters.AddWithValue(sqlParam.ParameterName, sqlParam.Value);
                    cmd.Parameters.Add(sqlParam);
                }
            }
            return cmd;
        }



        public SqlCommand ToSqlSpCommand(SqlConnection conn, string spName)
        {
            var cmd = new SqlCommand(SqlCommandString, conn)
            { CommandType = CommandType.StoredProcedure, CommandText = spName };
            foreach (var sqlParam in SqlParams)
            {
                //if (SqlCommandString.Contains(sqlParam.ParameterName))
                //{
                //cmd.Parameters.AddWithValue(sqlParam.ParameterName, sqlParam.Value);
                cmd.Parameters.Add(sqlParam);
                //}
            }
            return cmd;
        }

        /// <summary>
        /// Will Add a new return "set" that gets an Identity value. Useful for getting auto-incrementing ID
        /// values from a NoQuery
        /// </summary>
        public void AddScopeIdentity()
        {
            SqlCommandString += ";SELECT Scope_Identity()";
        }

        internal void WrapInExistsClause()
        {
            SqlCommandString = String.Format("IF EXISTS ({0}) SELECT '1' AS isExists ELSE SELECT '0' AS isExists", SqlCommandString);
        }

        /// <summary>
        /// Takes a standard SELECT * FROM sql clause and wraps the star with COUNT(*)
        /// </summary>
        internal void WrapInCountClause(string countAs)
        {
            SqlCommandString = SqlCommandString.Replace(" ", " "); // remove any EXTRA spaces
            SqlCommandString = SqlCommandString.Replace("SELECT *", string.Format("SELECT COUNT(*) AS {0}", countAs));
        }
    }
}
