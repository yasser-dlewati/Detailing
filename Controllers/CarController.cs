using Detailing.Models;
using Detailing.Repositories;
using Microsoft.AspNetCore.Mvc;
using Detailing.Interfaces;

namespace Detailing.Controllers;

[Route("[controller]")]
public class CarController : DetailingControllerBase<Car>
{
    public CarController(IConfiguration config, IDatabaseService dbService) : base(config, dbService)
    {
        _repoService = new CarRepository(dbService);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var cars = _repoService.GetAll();
        return Ok(cars);
    }

    [HttpGet("{CarId:int}")]
    public IActionResult Get(int CarId)
    {
        var car = _repoService.GetSingleById(CarId);
        return car != null ? Ok(car) : NotFound();
    }


    [HttpPost]
    public IActionResult Post([FromBody] Car newCar)
    {
        var isCarInserted = _repoService.TryInsert(newCar, out var carId);
        newCar.Id = carId;
        return isCarInserted ? CreatedAtAction(nameof(Get), newCar) : BadRequest();
    }

    [HttpPut]
    public IActionResult Put([FromBody] Car carToUpdate)
    {
        var isCarUpdated = _repoService.TryUpdate(carToUpdate);
        return isCarUpdated ? Ok(carToUpdate) : BadRequest();
    }

    [HttpDelete("{carId:int}")]
    public IActionResult Delete(int carId)
    {
        var isCarDeleted = _repoService.TryDelete(carId);
        return isCarDeleted ? NoContent() : BadRequest();
    }
}

