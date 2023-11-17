using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class DetailerMapper : IDataMapper<Detailer>
    {
        public Detailer MapToModel(DataRow row)
        {
            var detailerServiceMapper = new DetailerServiceMapper();
            var addressMapper = new AddressMapper();
            var detailer = new Detailer
            {
                Id = int.Parse(row["UserId"].ToString()),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                PreferredName = row["PreferredName"].ToString(),
                DOB = DateTime.Parse(row["DOB"].ToString()),
                MobileNumber = row["MobileNumber"].ToString(),
                Address = addressMapper.MapToModel(row),
                CreatedAt = DateTime.Parse(row["CreationDate"].ToString()),
                Services = new List<DetailerService>()
                {
                    detailerServiceMapper.MapToModel(row),
                }
            };

            return detailer;
        }
    }
}