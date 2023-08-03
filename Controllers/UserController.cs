using Detailing.Models;
using Detailing.Interfaces;
using Detailing.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

public class UserController : DetailingControllerBase<User>
{
    public UserController(IConfiguration config, IDatabaseService dbService, IEntityRepository<User> repoService) : base(config, dbService, repoService)
    {
        //_repoService = new UserRepository(dbService, new UserMapper());
    }

   
}

