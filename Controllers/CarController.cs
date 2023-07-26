using Detailing.Models;
using Detailing.Repositories;
using Microsoft.AspNetCore.Mvc;
using Detailing.Interfaces;
using Detailing.Mappers;

namespace Detailing.Controllers;

[Route("[controller]")]
public class CarController : DetailingControllerBase<Car>
{
    public CarController(IConfiguration config, IDatabaseService dbService) : base(config, dbService)
    {
        _repoService = new CarRepository(dbService, new CarMapper());
    }
}