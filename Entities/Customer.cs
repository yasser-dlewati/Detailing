namespace Detailing.Entities
{
    public class Customer : Person
    {
        public IEnumerable<Car> Cars { get; set; }

    }
}