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
        public DetailingJobController(IServiceProvider provider) : base(provider)
        {
            _detailerManager = provider.GetRequiredService<IModelManager<Detailer>>();
        }

        [HttpGet("/{jobId:int}/Detailer")]
        public IActionResult GetJobDetailer(int jobId)
        {
            var detailer = (_detailerManager as DetailerManager).GetJobDetailer(jobId);
            return detailer is null ? NotFound() : Ok(detailer);
        }
    }
}