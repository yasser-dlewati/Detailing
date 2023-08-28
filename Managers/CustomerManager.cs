using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;
public class CustomerManager : BaseManager<Customer>
{
    public CustomerManager(IModelProvider<Customer> provider) : base(provider)
    {
    }
}