using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;
public class DetailerManager : BaseManager<Detailer>
{
    public DetailerManager(IModelProvider<Detailer> provider) : base(provider)
    {
    }
}