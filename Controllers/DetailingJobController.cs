using System.Collections.Generic;
using Detailing.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailingJobController : ControllerBase
    {
        private IEnumerable<DetailingJob> jobs = new List<DetailingJob>{
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