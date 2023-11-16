using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetCarsAsync(int customerId)
    {
        var cars = await (_carManager as CarManager).GetByCustomerIdAsync(customerId);
        return Ok(cars);
    }

    [HttpGet("{customerId:int}/cars/{carId:int}")]
    public async Task<IActionResult> GetCarByIdAsync(int customerId, int carId)
    {
        var car = await (_carManager as CarManager).GetByIdAsync(carId);
        return car != null && car.OwnerId == customerId ? Ok(car) : NotFound();
    }

    [HttpPost("{customerId:int}/cars")]
    [Authorize(Policy ="CarOwner")]
    public async Task<IActionResult> CreateCarAsync(int customerId, Car car)
    {
        car.OwnerId = customerId;
        var isInserted = (_carManager as CarManager).TryInsert(ref car);
        return isInserted ? CreatedAtAction(nameof(GetAsync), car) : BadRequest();
    }

    [HttpPut("{customerId:int}/cars/{carId:int}")]
    [Authorize(Policy ="CarOwner")]
    public async Task<IActionResult> UpdateCarAsync(int customerId, int carId, Car car)
    {
        var isUpdated = await (_carManager as CarManager).TryUpdateCustomerCarAsync(customerId, carId, car);
        return isUpdated ? Ok(car) : BadRequest();
    }

    [HttpDelete("{customerId:int}/cars/{carId:int}")]
    [Authorize(Policy ="CarOwner")]
    public async Task<IActionResult> DeleteCarAsync(int customerId, int carId)
    {
        var isDeleted = await (_carManager as CarManager).TryDeleteCustomerCarAsync(customerId, carId);
        return isDeleted ? NoContent() : BadRequest();
    }
}
