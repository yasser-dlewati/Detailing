using Detailing.Models;
using Detailing.Interfaces;

namespace Detailing.Controllers;

public class UserController : DetailingControllerBase<User>
{
    public UserController(IConfiguration config, IDatabaseService dbService, IModelManager<User> manager) : base(config, dbService, manager)
    {
    }
}

