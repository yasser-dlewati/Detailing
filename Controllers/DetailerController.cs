using Detailing.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class DetailerController : ControllerBase
{
    private static IEnumerable<Detailer> detailers = new List<Detailer>{
        new Detailer{
            Id = 101,
            FirstName = "Yas",
            LastName = "dle",
        },
        new Detailer{
            Id = 1001,
            FirstName = "Hiba",
            LastName = "sh"
        }
    };

    [HttpGet]
    public IEnumerable<Detailer> Get()
    {
        return detailers;
    }

    [HttpGet("{detailerId:int}")]
    public ActionResult<Detailer> Get(int detailerId)
    {
        var detailer = detailers.FirstOrDefault(d => d.Id == detailerId);
        if (detailer == default)
        {
            return NotFound();
        }

        return Ok(detailer);
    }

    [HttpPost]
    public ActionResult<Detailer> Post(Detailer newDetailer)
    {
        try
        {
            detailers.ToList().Add(newDetailer);
            return Created(string.Empty, newDetailer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

