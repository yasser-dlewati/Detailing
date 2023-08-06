using Detailing.Models;
using Detailing.Interfaces;

namespace Detailing.Controllers;

public class CarController : DetailingControllerBase<Car>
{
    public CarController(IConfiguration config, IDatabaseService dbService, IRepositoryService<Car> repoService) : base(config, dbService, repoService)
    {
    }
}