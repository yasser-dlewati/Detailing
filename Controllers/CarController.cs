using Detailing.Models;
using Microsoft.AspNetCore.Authorization;

namespace Detailing.Controllers;

[Authorize(Roles ="Customer")]
public class CarController : DetailingControllerBase<Car>
{
    public CarController(IServiceProvider provider) : base(provider)
    {
    }
}