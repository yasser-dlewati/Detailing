using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Business : IModel
    {
        public int Id { get; set; }
        public Detailer Owner { get; set; }
        public IEnumerable<Detailer>? Crew { get; set; }
        public Address Address { get; set; }
    }
}