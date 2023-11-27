using Detailing.Interfaces;

namespace Detailing.Models
{
    public class LoginUser : IPassword
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}