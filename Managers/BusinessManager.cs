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

    public Business GetBusinessByJobId(int jobId)
    {
        var business = (_provider as BusinessProvider).GetBusinessByJobId(jobId);
        return business;
    }
}