using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Detailing.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CustomerController : DetailingControllerBase<Customer>
{
    private readonly IModelManager<Car> _carManager;
    private readonly IModelManager<User> _userManager;

    private readonly IConfiguration _config;

    public CustomerController(IServiceProvider provider) : base(provider)
    {
        _userManager = provider.GetRequiredService<IModelManager<User>>();
        _carManager = provider.GetRequiredService<IModelManager<Car>>();
        _config = provider.GetRequiredService<IConfiguration>();
    }

    [HttpPost]
    public new async Task<IActionResult> PostAsync([FromBody]SignupUser user)
    {
        var addressProvider  = new AddressProvider(_config);
        var coordinates = await addressProvider.GetCoordinatesAsync(user.Address);
        user.Address.Longitude = coordinates.Longitude;
        user.Address.Latitude = coordinates.Latitude;
        var isInserted = (_userManager as UserManager).TryInsert(ref user);
        var customer = user.ToCustomer();
        return isInserted ? CreatedAtAction(nameof(GetAsync), customer) : BadRequest();
    }
    
    [NonAction]
    public override async Task<IActionResult> PostAsync(Customer customer) 
    {
        return await base.PostAsync(customer);
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
