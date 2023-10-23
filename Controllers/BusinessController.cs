using Detailing.Models;

namespace Detailing.Controllers
{
    
    public class BusinessController : DetailingControllerBase<Business>
    {
        public BusinessController(IServiceProvider provider) : base(provider)
        {
        }
    }
}