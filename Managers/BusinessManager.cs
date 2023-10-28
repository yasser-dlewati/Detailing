using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;

public class BusinessManager : BaseManager<Business>
{
    public BusinessManager(IModelProvider<Business> provider) : base(provider)
    {
    }
}