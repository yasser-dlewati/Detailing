using System.Data;

namespace Detailing.Interfaces;
public interface IModelProvider<T>
{
    abstract string SelectAllStoredProcedureName { get; }

    abstract string SelectByIdStoredProcedureName { get; }

    abstract string InsertStoredProcedureName { get; }

    abstract string UpdateStoredProcedureName { get; }

    abstract string DeleteByIdStoredProcedureName { get; }

    abstract IDbDataParameter[] GetDbParameters(T data);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    bool TryInsert(T data, out int insertedId);

    Task<bool> TryUpdateAsync(T data);

    Task<bool> TryDeleteAsync(int id);
}