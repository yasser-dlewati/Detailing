using System.ComponentModel.DataAnnotations.Schema;
using Detailing.Interfaces;

namespace Detailing.Models
{
    public class Car: IModel
    {
        [Column("CarId")]
        public int Id { get; set; }

        [Column("Manufacturer")]
        public string Manufacturer { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("Year")]
        public string Year { get; set; }

        [Column("Color")]
        public string Color { get; set; }

        [Column("UserId")]
        public int OwnerId { get; set; }

        [Column("LastDetailingDate")]
        public DateTime? LastDetailed { get; set; }
    }
}