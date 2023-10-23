using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Business : IModel
    {
        public int Id { get; set; }
        public Detailer Owner { get; set; }
        public IEnumerable<Detailer>? Crew { get; set; }
        public Address Address { get; set; }
        public string Website { get; set; }
        public string SocialMedia { get; set; }
        public string Phone { get; set; }
        public DateTime Established { get; set; }
        public string Description { get; set; }
    }
}