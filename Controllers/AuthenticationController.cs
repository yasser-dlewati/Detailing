using Microsoft.AspNetCore.Mvc;
using Detailing.Models;
using Detailing.Interfaces;
using Detailing.Providers;
using Detailing.Mappers;
using Microsoft.Extensions.Caching.Memory;

namespace detailing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IDatabaseService _dbService;
        private readonly IAuthenticationService _authService;
        private readonly UserProvider UserProvider;
        private readonly IMemoryCache _cache; 

        public AuthenticationController(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<IConfiguration>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            _dbService = serviceProvider.GetService<IDatabaseService>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            _dbService.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _authService = serviceProvider.GetService<IAuthenticationService>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            _cache = serviceProvider.GetService<IMemoryCache>() ?? throw new ArgumentNullException(nameof(serviceProvider));

            UserProvider = new UserProvider(_dbService, new UserMapper(), _cache);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginUser LoginUser)
        {
            if (LoginUser != null && !string.IsNullOrEmpty(LoginUser.Email) && !string.IsNullOrEmpty(LoginUser.Password))
            {
                var user = await UserProvider.GetByLoginCredentialsAsync(LoginUser);
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