using System.ComponentModel.DataAnnotations.Schema;
using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Address : IModel
    {
        [Column("AddressId")]
        public int Id { get; set; }
        
        [Column("Line1")]
        public string Line1 { get; set; }

        [Column("Line2")]
        public string? Line2 { get; set; }

        [Column("ZipCode")]
        public string ZipCode { get; set; }

        [Column("City")]
        public string City { get; set; }

        public State State { get; set; }

        public Country Country { get; set; }

        [Column("Longitude")]
        public double Longitude {get; set;}

        [Column("Latitude")]
        public double Latitude { get; set; }

        public string ToString()
        {
            return $"{Line1} {Line2} {ZipCode} {City} {State.Name} {Country.Name}".Replace(" ", "%20");
        }
    }
}