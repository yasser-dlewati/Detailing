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
}