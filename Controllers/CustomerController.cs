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
    private readonly IUserTypeProvider<Customer> _provider;
    public CustomerController(IConfiguration config, IDatabaseService dbService, IUserTypeProvider<Customer> provider)
    {
_config = config;
dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
_dbService = dbService;
_provider = provider;
    }

[HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_provider.GetUsersOfType());
    }
}