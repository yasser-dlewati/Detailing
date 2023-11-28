using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers
{
    public class UserManager : BaseManager<User>
    {
        private readonly IModelProvider<User> _provider;
        public UserManager(IModelProvider<User> provider) : base(provider)
        {
            _provider = provider;
        }

        public new bool TryInsert(ref SignupUser user) 
        {
            return (_provider as UserProvider).TryInsert(ref user);
        }
    }
}