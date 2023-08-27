using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;
public class CustomerManager : IModelManager<Customer>
{
    private readonly IModelProvider<Customer> _provider;

    public CustomerManager(IModelProvider<Customer> provider)
    {
        _provider = provider;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _provider.GetAll();
    }

    public Customer GetById(int id)
    {
        return _provider.GetById(id);
    }

    public bool TryDelete(int id)
    {
        return _provider.TryDelete(id);
    }

    public bool TryInsert(Customer data, out int insertedId)
    {
        return _provider.TryInsert(data, out insertedId);
    }

    public bool TryUpdate(Customer data)
    {
        return _provider.TryUpdate(data);
    }
}