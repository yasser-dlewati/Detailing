using System.Data;
using System.Data.Common;

namespace Detailing.Interfaces
{
    public interface IDatabaseService
    {
        string ConnectionString { get; set; }

        DataTable ExecuteQueryStoredProcedure(string storedProcedureName, DbParameter[]? parameters = null);

        int ExecuteNonQueryStoredProcedure(string storedProcedureName, DbParameter[] parameters);
    }
}