using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Controllers
{
    
    public class BusinessController : DetailingControllerBase<Business>
    {
        public BusinessController(IConfiguration config, IDatabaseService dbService, IModelManager<Business> manager) : base(config, dbService, manager)
        {
        }
    }
}