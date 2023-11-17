using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers;

public class DetailerServiceManager : BaseManager<DetailerService>
{
    private readonly IModelProvider<DetailerService> _provider;
    public DetailerServiceManager(IModelProvider<DetailerService> provider) : base(provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<DetailerService>> GetDetailerServicesAsync(int detailerId)
    {
        return await (_provider as DetailerServiceProvider).GetDetailerServicesAsync(detailerId);
    }

    public async Task<DetailerService> GetDetailerServiceAsync(int detailerId, int serviceId)
    {
        return await (_provider as DetailerServiceProvider).GetDetailerServiceAsync(detailerId, serviceId);
    }

    public async new Task<bool> TryDeleteAsync(int  detailerId, int serviceId)
    {
        return await (_provider as DetailerServiceProvider).TryDeleteAsync(detailerId, serviceId);
    }
}