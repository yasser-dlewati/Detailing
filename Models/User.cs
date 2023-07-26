using Detailing.Interfaces;

namespace Detailing.Models
{
    ///Base clasee
    public class User : IModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string? PreferredName { get; set; }

        public DateTime DOB { get; set; }

        public string MobileNumber { get; set; }

        public Address Address { get; set; }
        
        public DateTime CreatedAt { get; internal set; }
    }
}