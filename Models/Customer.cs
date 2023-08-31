using Detailing.Consts;

namespace Detailing.Models
{
    public class Customer : User
    {
        public IEnumerable<Car> Cars { get; set; }

        public override UserType Role => UserType.Customer;
    }
}