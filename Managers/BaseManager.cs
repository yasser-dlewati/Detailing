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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _provider.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _provider.GetByIdAsync(id);
        }

        public async Task<bool> TryDeleteAsync(int id)
        {
            return await _provider.TryDeleteAsync(id);
        }

        public bool TryInsert(ref T data)
        {
            return _provider.TryInsert(ref data);
        }

        public async Task<bool> TryUpdateAsync(T data)
        {
            return await _provider.TryUpdateAsync(data);
        }
    }
}