using Detailing.Consts;

namespace Detailing.Models
{
    public class Detailer : User
    {
        public override UserType Role => UserType.Detailer;

        public IEnumerable<DetailerService> Services { get; set; }
    }
}