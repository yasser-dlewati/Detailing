using Detailing.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Car> Get()
    {
        return null;
    }

    [HttpGet("{CarId:int}")]
    public ActionResult<Car> Get(int CarId)
    {
        return null;
    }


    [HttpPost]
    public ActionResult<Car> Post(Car newCar)
    {
       return null;
    }
}

