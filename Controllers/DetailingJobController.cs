using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailingJobController : DetailingControllerBase<DetailingJob>
    {
        private readonly IModelManager<Detailer> _detailerManager;
        private readonly IModelManager<Car> _carManager;
        private readonly IModelManager<Business> _businessManager;

        public DetailingJobController(IServiceProvider provider) : base(provider)
        {
            _detailerManager = provider.GetRequiredService<IModelManager<Detailer>>();
            _carManager = provider.GetRequiredService<IModelManager<Car>>();
            _businessManager = provider.GetRequiredService<IModelManager<Business>>();
        }

        [HttpGet("/{jobId:int}/Detailer")]
        public IActionResult GetDetailer(int jobId)
        {
            var detailer = (_detailerManager as DetailerManager).GetJobDetailer(jobId);
            return detailer is null ? NotFound() : Ok(detailer);
        }

        
        [HttpGet("/{jobId:int}/Car")]
        public IActionResult GetCar(int jobId)
        {
            var detailer = (_carManager as CarManager).GetCarByJobId(jobId);
            return detailer is null ? NotFound() : Ok(detailer);
        }

        [HttpGet("/{jobId:int}/Business")]
        public IActionResult GetBusiness(int jobId)
        {
            var business = (_businessManager as BusinessManager).GetBusinessByJobId(jobId);
            return business is null ? NotFound() : Ok(business);
        }
    }
}