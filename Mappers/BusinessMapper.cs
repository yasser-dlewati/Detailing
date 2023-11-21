using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers;
public class BusinessMapper : IDataMapper<Business>
{
    public Business MapToModel(DataRow row)
    {
        var addressMapper = new AddressMapper();
        var address = addressMapper.MapToModel(row);
        var business = new Business
        {
            Id = Convert.ToInt32(row["BusinessId"]),
            BusinessName = row["BusinessName"].ToString(),
            Address = address,
            Description = row["Description"].ToString(),
            Established = DateTime.Parse(row["Established"].ToString()),
            Phone = row["Phone"].ToString(),
            OwnerId = Convert.ToInt32(row["UserId"]),
            SocialMedia = row["SocialMedia"].ToString(),
            Website = row["Website"].ToString(),
            Email = row["Email"].ToString(),
        };

        return business;
    }
}
