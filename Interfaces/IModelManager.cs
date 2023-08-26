namespace Detailing.Interfaces
{
public interface IModelManager<T>
{
        IEnumerable<T> GetAll();

        T GetById(int id);

        bool TryInsert(T data, out int insertedId);

        bool TryUpdate(T data);

        bool TryDelete(int id);
}
}