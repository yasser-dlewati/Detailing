namespace Detailing.Models
{
    public class Detailer : User
    {
        public bool? HasBusiness { get; set; }

        public bool? DetailsExterior { get; set; }

        public bool? DetailsInterior { get; set; }

        public bool? IsMobile { get; set; }
    }
}