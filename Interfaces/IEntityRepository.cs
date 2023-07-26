using System.Data.Common;
namespace Detailing.Interfaces
{
    public interface IEntityRepository<T>
    {
        IEnumerable<T> GetAll();

        IModel GetById(int id);

        bool TryInsert(T data, out int insetedId);

        bool TryUpdate(T data);

        bool TryDelete(int id);
    }
}