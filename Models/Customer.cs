namespace Detailing.Models
{
    public class Customer : Person
    {
        public IEnumerable<Car> Cars { get; set; }

    }
}