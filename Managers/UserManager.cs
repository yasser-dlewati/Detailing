using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers
{
    public class UserManager : BaseManager<User>
    {
        public UserManager(IModelProvider<User> provider) : base(provider)
        {
        }
    }
}