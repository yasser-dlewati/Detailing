namespace Detailing.Entities
{
    public class DetailingJob
    {
        public int Id { get; set; }

        public Person Detailer { get; set;}

        public Person Customer { get; set;}

        public bool DetailsExterior { get; set; }

        public bool DetailsInterior { get; set; }

        private double detailingExteriorCost = 0;

        public double DetailingExteriorCost 
        { 
            get
            {
                if (DetailsExterior && Detailer.DetailsExterior)
                {
                    return detailingExteriorCost;
                }
            }
            set
            {
                if (DetailsExterior && Detailer.DetailsExterior)
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
                if (DetailsInterior && Detailer.DetailsInterior)
                {
                    return detailingInteriorCost;
                }
            }
            set
            {
                if (DetailsInterior && Detailer.DetailsInterior)
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