using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers;

public class AddressMapper : IDataMapper<Address>
{
    public Address MapToModel(DataRow row)
    {
        var address = new Address
        {
            Id = Convert.ToInt32(row["AddressId"]),
            Line1 = row["Line1"].ToString(),
            Line2 = row["Line2"].ToString(),
            ZipCode = row["ZipCode"].ToString(),
            City = row["City"].ToString(),
            StateId = Convert.ToInt32(row["StateId"]),
            CountryId = Convert.ToInt32(row["CountryId"]),
            Longitude = Convert.ToDouble(row["Longitude"]),
            Latitude = Convert.ToDouble(row["Latitude"]),            
        };

        return address;
    }
}