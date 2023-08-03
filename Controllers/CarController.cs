using Detailing.Models;
using Detailing.Repositories;
using Microsoft.AspNetCore.Mvc;
using Detailing.Interfaces;
using Detailing.Mappers;

namespace Detailing.Controllers;

public class CarController : DetailingControllerBase<Car>
{
    public CarController(IConfiguration config, IDatabaseService dbService, IEntityRepository<Car> repoService) : base(config, dbService, repoService)
    {
    }
}