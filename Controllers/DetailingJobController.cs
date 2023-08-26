using System.Collections.Generic;
using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailingJobController : DetailingControllerBase<DetailingJob>
    {
        public DetailingJobController(IConfiguration config, IDatabaseService dbservice, IModelManager<DetailingJob> manager) : base(config, dbservice, manager)
        {
        }
    }
}