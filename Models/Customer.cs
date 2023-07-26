namespace Detailing.Models
{
    public class Customer : User
    {
        public IEnumerable<Car> Cars { get; set; }

    }
}