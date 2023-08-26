using Microsoft.AspNetCore.Mvc;
using Detailing.Models;
using Detailing.Interfaces;
using Detailing.Providers;
using Detailing.Mappers;

namespace detailing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IDatabaseService _dbService;
        private readonly IAuthenticationService _authService;
        private readonly UserProvider UserProvider;

        public AuthenticationController(IConfiguration config, IDatabaseService dbService, IAuthenticationService authService)
        {
            dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _dbService = dbService;
            _authService = authService;
            UserProvider = new UserProvider(_dbService, new UserMapper());
        } 

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if(userLogin !=null && !string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
            {
                var user = UserProvider.GetByLoginCredentials(userLogin);
                if(user != null)
                {
                    var token = _authService.GenerateToken(userLogin);
                    return Ok(token);
                }
            }

            return BadRequest();
        }
    }
}