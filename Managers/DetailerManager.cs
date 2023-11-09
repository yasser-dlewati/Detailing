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

    public async Task<IEnumerable<Detailer>> GetCrewAsync(int businessId)
    {
        var crew = await (_provider as DetailerProvider).GetCrewAsync(businessId);
        return crew;
    }

    public async Task<Detailer> GetJobDetailerAsync(int jobId)
    {
        var detailer = await (_provider as DetailerProvider).GetJobDetailerAsync(jobId);
        return detailer;
    }
}