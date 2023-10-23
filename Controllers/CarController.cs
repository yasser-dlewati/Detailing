using Detailing.Models;

namespace Detailing.Controllers;

public class CarController : DetailingControllerBase<Car>
{
    public CarController(IServiceProvider provider) : base(provider)
    {
    }
}