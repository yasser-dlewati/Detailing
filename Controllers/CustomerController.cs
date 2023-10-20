using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user/[controller]")]
public class CustomerController : DetailingControllerBase<Customer>
{
    public CustomerController(IConfiguration config, IDatabaseService dbService, IModelManager<Customer> manager) : base(config, dbService, manager)
    {
        
    }
}