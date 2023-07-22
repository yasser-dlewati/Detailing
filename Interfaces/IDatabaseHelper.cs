using System.Data;
using MySql.Data.MySqlClient;

namespace Detailing.Interfaces
{
    public interface IDatabaseHelper<T>
    {
        DataTable ExecuteQueryStoredProcedure(string storedProcedureName);

        T? ExecuteScalarQueryStoredProcedure(string storedProcedureName, MySqlParameter parameter);

        int ExecuteNonQueryStoredProcedure(string storedProcedureName, MySqlParameter[] parameters);
    }
}