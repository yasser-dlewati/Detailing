namespace Detailing.Interfaces
{
    public interface IModelProvider<T>
    {
        abstract string SelectAllStoredProcedureName {get;}

        abstract string SelectByIdStoredProcedureName {get;}

        abstract string InsertStoredProcedureName {get;}

        abstract string UpdateStoredProcedureName {get;}

        abstract string DeleteByIdStoredProcedureName {get;}

        IEnumerable<T> GetAll();

        T GetById(int id);

        bool TryInsert(T data, out int insertedId);

        bool TryUpdate(T data);

        bool TryDelete(int id);
    }
}