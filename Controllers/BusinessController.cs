using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Repositories;
using Detailing.Mappers;


namespace Detailing.Controllers
{
    
    public class BusinessController : DetailingControllerBase<Business>
    {
        public BusinessController(IConfiguration config, IDatabaseService dbService, IEntityRepository<Business> repoService) : base(config, dbService, repoService)
        {
        }
    }
}