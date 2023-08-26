using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : DetailingControllerBase<Customer>
{
    public CustomerController(IConfiguration config, IDatabaseService dbservice, IModelManager<Customer> manager) : base(config, dbservice, manager)
    {
    }
}