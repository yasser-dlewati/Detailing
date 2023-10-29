using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers;

public class DetailerManager : BaseManager<Detailer>
{
    private readonly IModelProvider<Detailer> _provider;
    public DetailerManager(IModelProvider<Detailer> provider) : base(provider)
    {
        _provider = provider;
    }

    public IEnumerable<Detailer> GetCrew(int businessId){
        DetailerProvider provider = _provider as DetailerProvider;
        var crew = provider.GetCrew(businessId);
        return crew;
    }
}