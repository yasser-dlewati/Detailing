namespace Detailing.Interfaces;
public interface IModelManager<T>
{
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        bool TryInsert(T data, out int insertedId);

        Task<bool> TryUpdateAsync(T data);

        Task<bool> TryDeleteAsync(int id);
}