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
    public IActionResult GetCrew(int businessId)
    {
        DetailerManager detailerManager = _detailerManager as DetailerManager;
        var crew = detailerManager.GetCrew(businessId);
        return Ok(crew);
    }
}