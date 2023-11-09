using System.Data;
using Detailing.Interfaces;

namespace Detailing.Services
{
    public class DatabaseService
    {
        private readonly IDatabaseService _databaseService;

        public DatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[] parameters)
        {
            return await _databaseService.ExecuteNonQueryStoredProcedureAsync(storedProcedureName, parameters);
        }

        public async Task<DataTable> ExecuteQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[]? parameters = null)
        {
            return await _databaseService.ExecuteQueryStoredProcedureAsync(storedProcedureName, parameters);
        }
    }
}