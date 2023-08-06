using Detailing.Interfaces;
using Detailing.Models;


namespace Detailing.Controllers
{
    
    public class BusinessController : DetailingControllerBase<Business>
    {
        public BusinessController(IConfiguration config, IDatabaseService dbService, IRepositoryService<Business> repoService) : base(config, dbService, repoService)
        {
        }
    }
}