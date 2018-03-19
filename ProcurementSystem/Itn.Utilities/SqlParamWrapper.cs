using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities
{
    public static class SqlParamWrapper
    {

        public static SqlParameter Create(string parameterName, object value, bool nullable = false)
        {
            var val = (value == null && nullable) ? DBNull.Value : value;
            return new SqlParameter { ParameterName = parameterName, Value = val };
        }

        public static SqlParameter Create(string parameterName, SqlDbType sqlDbType, int dataLength, object value)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = sqlDbType, Size = dataLength, Value = value };
        }

        public static SqlParameter Create(string parameterName, object value, ParameterDirection paramDirection)
        {
            return new SqlParameter { ParameterName = parameterName, Value = value, Direction = paramDirection };
        }

        public static SqlParameter CreateReturnValueParam(string parameterName)
        {
            return new SqlParameter { ParameterName = parameterName, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.ReturnValue };
        }

        public static SqlParameter CreateOutputParam(string parameterName, SqlDbType sqlDbType, int outputSize = 255)
        {
            return new SqlParameter { ParameterName = parameterName, Direction = ParameterDirection.Output, SqlDbType = sqlDbType, Size = outputSize };
        }


    }
}
