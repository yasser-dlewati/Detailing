using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Detailing.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user/[controller]")]
public class DetailerController : DetailingControllerBase<Detailer>
{
    private readonly IModelManager<User> _userManager;
    private readonly IModelManager<DetailerService> _serviceManager;
    private readonly IConfiguration _config;

    public DetailerController(IServiceProvider provider) : base(provider)
    {
        _userManager = provider.GetRequiredService<IModelManager<User>>();
        _serviceManager = provider.GetRequiredService<IModelManager<DetailerService>>();
        _config = provider.GetRequiredService<IConfiguration>();
    }

    [HttpPost("")]
    public new async Task<IActionResult> PostAsync([FromBody]SignupUser user)
    {
         var addressProvider  = new AddressProvider(_config);
        var coordinates = await addressProvider.GetCoordinatesAsync(user.Address);
        user.Address.Longitude = coordinates.Longitude;
        user.Address.Latitude = coordinates.Latitude;
        var isInserted = (_userManager as UserManager).TryInsert(ref user);
        var detailer = user.ToDetailer();
        return isInserted ? CreatedAtAction(nameof(GetAsync), detailer) : BadRequest();
    }

    [NonAction]
    public override async Task<IActionResult> PostAsync(Detailer detailer) 
    {
        return await base.PostAsync(detailer);
    }

    [HttpGet("/{detailerId:int}/services")]
    public async Task<IActionResult> GetServicesAsync(int detailerId)
    {
        var service = await (_serviceManager as DetailerServiceManager).GetDetailerServicesAsync(detailerId);
        return service is null ? NotFound() : Ok(service);
    }

    [HttpGet("/{detailerId:int}/services/{serviceId:int}")]
    public async Task<IActionResult> GetServicesAsync(int detailerId, int serviceId)
    {
        var service = await (_serviceManager as DetailerServiceManager).GetDetailerServiceAsync(detailerId, serviceId);
        return service is null ? NotFound() : Ok(service);
    }

    [HttpPost("/{detailerId:int}/services")]
    public async Task<IActionResult> CreateServiceAsync(int detailerId, DetailerService service)
    {
        service.DetailerId = detailerId;
        var isCreated = (_serviceManager as DetailerServiceManager).TryInsert(ref service);
        return isCreated ? CreatedAtAction(nameof(GetAsync), service) : BadRequest();
    }

    [HttpPut("/{detailerId:int}/services")]
    public async Task<IActionResult> UpdateServiceAsync(int detailerId, DetailerService service)
    {
        service.DetailerId = detailerId;
        var isUpdated = await (_serviceManager as DetailerServiceManager).TryUpdateAsync(service);
        return isUpdated ? Ok(service) : BadRequest();
    }

    [HttpDelete("/{detailerId:int}/services/{serviceId:int}")]
    public async Task<IActionResult> DeleteServiceAsync(int detailerId, int serviceId)
    {
        var isDeleted = await (_serviceManager as DetailerServiceManager).TryDeleteAsync(detailerId, serviceId);
        return isDeleted ? NoContent() : BadRequest();
    }
}