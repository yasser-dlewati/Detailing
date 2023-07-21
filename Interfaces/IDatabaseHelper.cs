using System.Data;
using MySql.Data.MySqlClient;

namespace Detailing.Interfaces
{
    public interface IDatabaseHelper<T>
    {
        DataTable ExecuteQueryStoredProcedure(string storedProcedureName);

        T ExecuteScalarQueryStoredProcedure(string storedProcedureName);

        void ExecuteNonQueryStoredProcedure(string storedProcedureName, MySqlParameter[] parameters);
    }
}