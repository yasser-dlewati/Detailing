using System.ComponentModel.DataAnnotations.Schema;
using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Business : IModel
    {
        [Column("BusinessId")]
        public int Id { get; set; }

        [Column("DetailerId")]
        public int OwnerId { get; set; }

        [Column("BusinessName")]
        public string BusinessName { get; set; }

        public Address Address { get; set; }

        [Column("Website")]
        public string Website { get; set; }

        [Column("SocialMedia")]
        public string SocialMedia { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }
        
        [Column("Established")]
        public DateTime Established { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}