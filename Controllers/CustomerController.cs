using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CustomerController : DetailingControllerBase<Customer>
{
    private readonly IModelManager<Car> _carManager;

    public CustomerController(IServiceProvider provider) : base(provider)
    {
        _carManager = provider.GetRequiredService<IModelManager<Car>>();
    }

    [HttpGet("{customerId:int}/car")]
    public IActionResult GetCars(int customerId)
    {
        var carManager = _carManager as CarManager;
        var cars = carManager.GetByCustomerId(customerId);
        return Ok(cars);
    }

    [HttpGet("{customerId:int}/cars/{carId:int}")]
    public IActionResult GetCarById(int customerId, int carId)
    {
        var carManager = _carManager as CarManager;
        var car = carManager.GetById(carId);
        return car != null && car.OwnerId == customerId ? Ok(car) : NotFound();
    }

    [HttpPost("{customerId:int}/cars")]
    public IActionResult CreateCar(int customerId, Car car)
    {
        car.OwnerId = customerId;
        var carManager = _carManager as CarManager;
        var isInserted = carManager.TryInsert(car, out int carId);
        car.Id = carId;
        return isInserted ? CreatedAtAction(nameof(Get), car) : BadRequest();
    }

    [HttpPut("{customerId:int}/cars/{carId:int}")]
    public IActionResult UpdateCar(int customerId, int carId, Car car)
    {
        var carManager = _carManager as CarManager;
        var isUpdated = carManager.TryUpdateCustomerCar(customerId, carId, car);
        return isUpdated ? Ok(car) : BadRequest();
    }

    [HttpDelete("{customerId:int}/cars/{carId:int}")]
    public IActionResult DeleteCar(int customerId, int carId)
    {
        var carManager = _carManager as CarManager;
        var isDeleted = carManager.TryDeleteCustomerCar(customerId, carId);
        return isDeleted ? NoContent() : BadRequest();
    }
}
