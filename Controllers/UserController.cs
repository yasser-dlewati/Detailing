using Detailing.Models;
using Detailing.Interfaces;
using Detailing.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[Route("[controller]")]
public class UserController : DetailingControllerBase<User>
{
    public UserController(IConfiguration config, IDatabaseService dbService) : base(config, dbService)
    {
        _repoService = new UserRepository(dbService, new UserMapper());
    }

   
}

