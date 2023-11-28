using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Detailing.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

public class BusinessController : DetailingControllerBase<Business>
{
    private readonly IModelManager<Detailer> _detailerManager;
    private readonly IConfiguration _config;

    public BusinessController(IServiceProvider provider) : base(provider)
    {
        _detailerManager = provider.GetRequiredService<IModelManager<Detailer>>();
        _config = provider.GetRequiredService<IConfiguration>();
    }

    [HttpPost]
    public override async Task<IActionResult> PostAsync(Business business)
    {
        var addressProvider  = new AddressProvider(_config);
        var coordinates = await addressProvider.GetCoordinatesAsync(business.Address);
        business.Address.Longitude = coordinates.Longitude;
        business.Address.Latitude = coordinates.Latitude;
        return await base.PostAsync(business);
    }

    [HttpGet]
    [Route("{businessId}/crew")]
    public async Task<IActionResult> GetCrewAsync(int businessId)
    {
        var crew = await (_detailerManager as DetailerManager).GetCrewAsync(businessId);
        return crew.Count() == 0 ? NotFound() : Ok(crew);
    }

    [HttpPost]
    [Route("{businessId}/crew")]
    public async Task<IActionResult> AddCrewAsync(int businessId, Detailer detailer)
    {
        var isAdded = (_detailerManager as DetailerManager).AddCrew(businessId, ref detailer);
        return isAdded ? Ok(detailer) : BadRequest();
    }

    [HttpDelete]
    [Route("{businessId}/crew/{detailerId:int}")]
    public async Task<IActionResult> DeleteCrewAsync(int businessId, int detailerId)
    {
        var isAdded = await (_detailerManager as DetailerManager).DeleteCrewAsync(businessId, detailerId);
        return isAdded ? NoContent() : BadRequest();
    }
}