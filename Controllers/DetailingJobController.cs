namespace Detailing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailingJobController : ContorllerBase
    {
        private IEnumerable<DetailingJobs> jobs = mew List{
            new DetailingJob{
                Id = 1,
            },
        };

        [HttpGet("{customerId:int}")]
        public ActionResult<DetailingJob> Get(int jobId)
        {
            var job = jobs.FirstOrDefault(j => j.Id == jobId);
            if (job == default)
            {
                return NotFound();
            }

            return Ok(job);
        }
        
        [HttpPost]
        public ActionResult<DetailingJob> Post(DetailingJob newJob)
        {
            try
            {
                jobs.ToList().Add(newJob);
                return Created(string.Empty, newJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}