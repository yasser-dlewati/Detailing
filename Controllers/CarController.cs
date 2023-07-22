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
    public IActionResult Get()
    {
        var cars = carRepository.GetAll();
        return Ok(cars);
    }

    [HttpGet("{CarId:int}")]
    public IActionResult Get(int CarId)
    {
        var car = carRepository.GetSingleById(CarId);
        return car != null ? Ok(car) : NotFound();
    }


    [HttpPost]
    public IActionResult Post([FromBody] Car newCar)
    {
        var isCarInserted = carRepository.TryInsert(newCar, out var carId);
        newCar.Id = carId;
        return isCarInserted ? CreatedAtAction(nameof(Get), newCar) : BadRequest();
    }

    [HttpPut]
    public IActionResult Put([FromBody] Car carToUpdate)
    {
        var isCarUpdated = carRepository.TryUpdate(carToUpdate);
        return isCarUpdated ? Ok(carToUpdate) : BadRequest();
    }

     [HttpDelete("{carId:int}")]
    public IActionResult Delete(int carId)
    {
        var isCarDeleted = carRepository.TryDelete(carId);
        return isCarDeleted ? NoContent() : BadRequest();
    }
}

