using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers;

public class DetailerServiceMapper : IDataMapper<DetailerService>
{
    public DetailerService MapToModel(DataRow row)
    {
        var service = new DetailerService
            {
                Id = int.Parse(row["ServiceId"].ToString()),
                DetailerId = int.Parse(row["UserId"].ToString()),
                Name = row["Name"].ToString(),
                Cost = double.Parse(row["Cost"].ToString()),
                ETA = int.Parse(row["Time"].ToString()),
                AddedOn = DateTime.Parse(row["CreationDate"].ToString()),
                IsMobile = bool.Parse(row["IsMobile"].ToString()),
            };

            return service;
    }
}
