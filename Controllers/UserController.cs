using Detailing.Models;
using Detailing.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Detailing.Controllers;

[Authorize(Roles ="Admin")]
public class UserController : DetailingControllerBase<User>
{
    public UserController(IServiceProvider provider) : base(provider)
    {
    }
}

