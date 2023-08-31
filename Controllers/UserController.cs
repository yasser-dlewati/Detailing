using Detailing.Models;
using Detailing.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Detailing.Controllers;

[Authorize(Roles ="Admin")]
public class UserController : DetailingControllerBase<User>
{
    public UserController(IConfiguration config, IDatabaseService dbService, IModelManager<User> manager) : base(config, dbService, manager)
    {
    }
}

