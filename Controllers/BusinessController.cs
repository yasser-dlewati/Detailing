using Detailing.Interfaces;
using Detailing.Managers;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

public class BusinessController : DetailingControllerBase<Business>
{
    private readonly IModelManager<Detailer> _detailerManager;
    public BusinessController(IServiceProvider provider) : base(provider)
    {
        _detailerManager = provider.GetRequiredService<IModelManager<Detailer>>();
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