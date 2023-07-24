namespace Detailing.Models
{
    public class Business
    {
        public Detailer Owner { get; set; }
        public IEnumerable<Detailer>? Crew { get; set; }
        public Address Address { get; set; }
    }
}