using Detailing.Interfaces;

namespace Detailing.Managers
{
    public class BaseManager<T> : IModelManager<T>
    {
        private readonly IModelProvider<T> _provider;

        public BaseManager(IModelProvider<T> provider)
        {
            _provider = provider;
        }

        public IEnumerable<T> GetAll()
        {
            return _provider.GetAll();
        }

        public T GetById(int id)
        {
           return _provider.GetById(id);
        }

        public bool TryDelete(int id)
        {
           return _provider.TryDelete(id);
        }

        public bool TryInsert(T data, out int insertedId)
        {
            return _provider.TryInsert(data, out insertedId);
        }

        public bool TryUpdate(T data)
        {
            return _provider.TryUpdate(data);
        }
    }
}