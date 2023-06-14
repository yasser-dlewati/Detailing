namespace Detailing.Entities
{
    public class Detailer : Person
    {
        public bool HasBusiness { get; set; }

        public bool DetailsExterior { get; set; }

        public bool DetailsInterior { get; set; }

        public bool IsMobile { get; set; }
    }
}