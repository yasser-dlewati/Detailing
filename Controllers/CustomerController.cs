using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("api/user/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IDatabaseService _dbService;
    private readonly IModelManager<Customer> _manager;

    public CustomerController(IConfiguration config, IDatabaseService dbService, IModelManager<Customer> manager)
    {
        _config = config;
        dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
        _dbService = dbService;
        _manager = manager;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var customers = _manager.GetAll();
        return customers.Count() > 0 ? Ok(customers) : NotFound();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var customer = _manager.GetById(id);
        return customer != null ? Ok(customer) : NotFound();
    }
}