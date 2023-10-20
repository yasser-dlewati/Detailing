using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/user/[controller]")]
public class DetailerController : DetailingControllerBase<Detailer>
{
    public DetailerController(IServiceProvider provider) : base(provider)
    {
    }
}