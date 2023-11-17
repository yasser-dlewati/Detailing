using Detailing.Interfaces;

namespace Detailing.Models
{
    public class DetailingJob : IModel
    {
        public int Id { get; set; }

        public Detailer Detailer { get; set;}

        public Customer Customer { get; set;}

        public Car DetailedCar { get; set; }

        public Business Business { get; set; }

        public DateTime DetailingTime { get; set; }

        public IEnumerable<DetailerService> ConsumedServices { get; set; }

    }
}