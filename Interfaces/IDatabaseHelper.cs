using System.Data;

namespace Detailing.Interfaces
{
    public interface IDatabaseHelper<T>
    {
        DataTable ExecuteQueryStoredProcedure(string storedProcedureName);

        T ExecuteScalarQueryStoredProcedure(string storedProcedureName);

        bool ExecuteNonQueryStoredProcedure(string storedProcedureName);
    }
}