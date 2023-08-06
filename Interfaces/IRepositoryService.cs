namespace Detailing.Interfaces
{
    public interface IRepositoryService<T>
    {
        abstract string SelectAllStoredProcedureName {get;}

        abstract string SelectByIdStoredProcedureName {get;}

        abstract string InsertStoredProcedureName {get;}

        abstract string UpdateStoredProcedureName {get;}

        abstract string DeleteByIdStoredProcedureName {get;}

        IEnumerable<T> GetAll();

        T GetById(int id);

        bool TryInsert(T data, out int insetedId);

        bool TryUpdate(T data);

        bool TryDelete(int id);
    }
}