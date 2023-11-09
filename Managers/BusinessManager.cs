using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers;

public class BusinessManager : BaseManager<Business>
{
    private readonly IModelProvider<Business> _provider;
    public BusinessManager(IModelProvider<Business> provider) : base(provider)
    {
        _provider = provider;
    }

    public async Task<Business> GetBusinessByJobIdAsync(int jobId)
    {
        var business = await (_provider as BusinessProvider).GetBusinessByJobIdAsync(jobId);
        return business;
    }
}