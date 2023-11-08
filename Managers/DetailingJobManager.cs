using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;

public class DetailingJobManager : BaseManager<DetailingJob>
{
    public DetailingJobManager(IModelProvider<DetailingJob> provider) : base(provider)
    {
    }
}