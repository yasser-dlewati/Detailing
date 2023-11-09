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
        public async Task<IActionResult> GetDetailerAsync(int jobId)
        {
            var detailer = await (_detailerManager as DetailerManager).GetJobDetailerAsync(jobId);
            return detailer is null ? NotFound() : Ok(detailer);
        }


        [HttpGet("/{jobId:int}/Car")]
        public async Task<IActionResult> GetCarAsync(int jobId)
        {
            var detailer = await (_carManager as CarManager).GetCarByJobIdAsync(jobId);
            return detailer is null ? NotFound() : Ok(detailer);
        }

        [HttpGet("/{jobId:int}/Business")]
        public async Task<IActionResult> GetBusiness(int jobId)
        {
            var business = await (_businessManager as BusinessManager).GetBusinessByJobIdAsync(jobId);
            return business is null ? NotFound() : Ok(business);
        }
    }
}