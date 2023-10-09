using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Address : IModel
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Longitude {get; set;}
        public double Latitude { get; set; }

    }
}