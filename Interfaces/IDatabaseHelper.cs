using System.Data;
using MySql.Data.MySqlClient;

namespace Detailing.Interfaces
{
    public interface IDatabaseHelper<T>
    {
        DataTable ExecuteQueryStoredProcedure(string storedProcedureName, MySqlParameter[]? parameters = null);

        int ExecuteNonQueryStoredProcedure(string storedProcedureName, MySqlParameter[] parameters);
    }
}