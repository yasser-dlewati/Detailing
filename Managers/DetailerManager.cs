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

    public async Task<Detailer> GetCrewAsync(int businessId, int detailerId)
    {
        var crew = await (_provider as DetailerProvider).GetCrewAsync(businessId, detailerId);
        return crew;
    }

    public bool AddCrew(int businessId, ref Detailer detailer)
    {
       var isAdded = (_provider as DetailerProvider).AddCrew(businessId, ref detailer);
        return isAdded;
    }

    public async Task<bool> UpdateCrewAsync(int businessId, Detailer detailer)
    {
        var isUpdated = await (_provider as DetailerProvider).UpdateCrewAsync(businessId, detailer);
        return isUpdated;
    }

     public async Task<bool> DeleteCrewAsync(int businessId, int detailerId)
    {
        var isUpdated = await (_provider as DetailerProvider).DeleteCrewAsync(businessId, detailerId);
        return isUpdated;
    }
}