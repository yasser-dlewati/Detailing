using Detailing.Consts;

namespace Detailing.Models
{
    public class Detailer : User
    {
        public int DetailerId {get; set;}

        public bool? HasBusiness { get; set; }

        public bool? DetailsExterior { get; set; }

        public bool? DetailsInterior { get; set; }

        public bool? IsMobile { get; set; }

        public override UserType Role => UserType.Detailer;
    }
}