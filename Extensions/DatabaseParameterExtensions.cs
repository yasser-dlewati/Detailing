using System.Data;
using MySql.Data.MySqlClient;

namespace Detailing.Extensions
{
    public static class DatabaseParameterExtensions
    {
        public static MySqlParameter ToMySqlParameter(this IDbDataParameter dbParam){
            return new MySqlParameter(dbParam.ParameterName, dbParam.Value);
        }

        public static IEnumerable<MySqlParameter> ToMySqlParameters(this IDbDataParameter[] dbParams){
           var mySqlParametes = new List<MySqlParameter>();
            foreach(var dbParam in dbParams)
            {
                var mysqlParam = new MySqlParameter($"p_{dbParam.ParameterName}", dbParam.Value);
                mySqlParametes.Add(mysqlParam);
            }

            return mySqlParametes;
        }
    }
}