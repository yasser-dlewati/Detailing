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

        public bool DetailsExterior { get; set; }

        public bool DetailsInterior { get; set; }

        private double detailingExteriorCost = 0;

        public double DetailingExteriorCost 
        { 
            get
            {
                return detailingExteriorCost;
            }
            set
            {
                if (DetailsExterior && Detailer.DetailsExterior.HasValue && Detailer.DetailsExterior.Value)
                {
                    detailingExteriorCost = value;
                }
            } 
        }

        private double detailingInteriorCost = 0;

        public double DetailingInteriorCost 
        {
             get
            {
                return detailingInteriorCost;
            }
            set
            {
                if (DetailsInterior && Detailer.DetailsInterior.HasValue && Detailer.DetailsInterior.Value)
                {
                    detailingInteriorCost = value;
                }
            } 
        }

        public double Total 
        {
            get 
            {
                return DetailingInteriorCost + DetailingExteriorCost;
            }
        }
    }
}