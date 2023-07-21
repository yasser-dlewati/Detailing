using Detailing.Entities;
using Detailing.Repositories;  
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Car> Get()
    {
        var carProvider = new CarRepository();
        var cars = carProvider.GetAll();
        return cars;
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

