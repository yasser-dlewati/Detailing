using System.Data;

namespace Detailing.Interfaces
{
    public interface IDatabaseService
    {
        string ConnectionString { get; set; }

        DataTable ExecuteQueryStoredProcedure(string storedProcedureName, IDbDataParameter[]? parameters = null);

        int ExecuteNonQueryStoredProcedure(string storedProcedureName, IDbDataParameter[] parameters);
    }
}