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
        return Ok(crew);
    }
}