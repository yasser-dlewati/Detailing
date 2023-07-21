using Detailing.Entities;
using Detailing.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly CarRepository carRepository = new CarRepository();
    
    [HttpGet]
    public IEnumerable<Car> Get()
    {
        var cars = carRepository.GetAll();
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
        carRepository.TryInsert(newCar);
        return Ok(newCar);
    }
}

