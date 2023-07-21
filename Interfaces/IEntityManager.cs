namespace Detailing.Interfaces
{
    public interface IEntityManager<T>
    {
        bool TryInsert(T entity);

        bool TryUpdate(T entity);

        bool TryDelete(T entity);
    }
}