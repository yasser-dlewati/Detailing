using System.Data;
using Detailing.Interfaces;
using System.Data.Common;

namespace Detailing.Services
{
    public class DatabaseService
    {
        private readonly IDatabaseService _databaseService;

        public DatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        
        public int ExecuteNonQueryStoredProcedure(string storedProcedureName, IDbDataParameter[] parameters)
        {
           return _databaseService.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);
        }
        
        public DataTable ExecuteQueryStoredProcedure(string storedProcedureName, IDbDataParameter[]? parameters = null)
        {
                return _databaseService.ExecuteQueryStoredProcedure(storedProcedureName, parameters);
        }
   }
}