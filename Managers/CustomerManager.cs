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
        throw new NotImplementedException();
    }

    public bool TryInsert(Customer data, out int insertedId)
    {
        throw new NotImplementedException();
    }

    public bool TryUpdate(Customer data)
    {
        throw new NotImplementedException();
    }
}