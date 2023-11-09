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

        public AuthenticationController(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<IConfiguration>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            _dbService = serviceProvider.GetService<IDatabaseService>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            _dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _authService = serviceProvider.GetService<IAuthenticationService>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            UserProvider = new UserProvider(_dbService, new UserMapper());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLogin userLogin)
        {
            if (userLogin != null && !string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
            {
                var user = await UserProvider.GetByLoginCredentialsAsync(userLogin);
                if (user != null)
                {
                    var token = _authService.GenerateToken(user);
                    return Ok(token);
                }
            }

            return BadRequest();
        }
    }
}