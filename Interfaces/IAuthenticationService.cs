using Detailing.Models;

namespace Detailing.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserLogin userLogin);
    }
}