using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user/[controller]")]
public class DetailerController : DetailingControllerBase<Detailer>
{
    public DetailerController(IConfiguration config, IDatabaseService dbservice, IModelManager<Detailer> manager) : base(config, dbservice, manager)
    {
    }
}