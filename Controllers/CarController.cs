using Detailing.Models;
using Detailing.Interfaces;

namespace Detailing.Controllers;

public class CarController : DetailingControllerBase<Car>
{
    public CarController(IServiceProvider provider) : base(provider)
    {
    }
}