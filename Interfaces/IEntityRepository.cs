namespace Detailing.Interfaces
{
    public interface IEntityRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetSingleById(int id);

        bool TryInsert(T data, out int insetedId);

        bool TryUpdate(T data);

        bool TryDelete(T data);
    }
}