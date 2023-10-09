using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class DetailerMapper : IDataMapper<Detailer>
    {
        public Detailer MapToModel(DataRow row)
        {
            var detailer = new Detailer
            {
                Id = int.Parse(row["UserId"].ToString()),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                PreferredName = row["PreferredName"].ToString(),
                DOB = DateTime.Parse(row["DOB"].ToString()),
                MobileNumber = row["MobileNumber"].ToString(),
                Address = new Address
                {
                    Id = int.Parse(row["AddressId"].ToString()),
                },
                CreatedAt = DateTime.Parse(row["CreationDate"].ToString()),
                DetailerId = int.Parse(row["DetailerId"].ToString()),
                DetailsExterior = bool.Parse(row["DetailsExterior"].ToString()),
                DetailsInterior = bool.Parse(row["DetailsInterior"].ToString()),
                HasBusiness = bool.Parse(row["HasBusiness"].ToString()),
                IsMobile = bool.Parse(row["IsMobile"].ToString()),
            };

            return detailer;
        }
    }
}