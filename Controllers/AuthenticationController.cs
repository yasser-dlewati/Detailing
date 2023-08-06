using Microsoft.AspNetCore.Mvc;
using Detailing.Models;
using Detailing.Interfaces;
using Detailing.Repositories;
using Detailing.Mappers;

namespace detailing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDatabaseService _dbService;
        private readonly IAuthenticationService _authService;
        private readonly UserRepository userRepository;

        public AuthenticationController(IConfiguration config, IDatabaseService dbService, IAuthenticationService authService)
        {
            _config = config;
            dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _dbService = dbService;
            _authService = authService;
            userRepository = new UserRepository(_dbService, new UserMapper());
        } 

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if(userLogin !=null && !string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
            {
                var user = userRepository.GetByLoginCredentials(userLogin);
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