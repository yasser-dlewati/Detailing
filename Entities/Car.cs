namespace Detailing.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Color { get; set; }

        public Person Owner { get; set; }

        public DateTime? LastDetailed { get; set; }
    }
}