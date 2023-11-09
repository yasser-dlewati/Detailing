using System.Data;

namespace Detailing.Interfaces
{
    public interface IDatabaseService
    {
        string ConnectionString { get; set; }

        Task<DataTable> ExecuteQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[]? parameters = null);

        Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[] parameters);
    }
}