using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers;

public class DetailingJobMapper : IDataMapper<DetailingJob>
{
    public DetailingJob MapToModel(DataRow row)
    {
        var isDetailedByBusiness = int.TryParse(row["BusinessId"].ToString(), out var businessId);
         var job = new DetailingJob
            {
                Id = int.Parse(row["JobId"].ToString()),
                DetailedCar = new Car
                {
                    Id = int.Parse( row["CarId"].ToString()),
                },
                Detailer = new Detailer
                {
                    Id = int.Parse(row["DetailerId"].ToString()),
                },
                Business = isDetailedByBusiness 
                ? new Business
                {
                    Id = businessId,
                }
                :null,
                DetailsExterior = bool.Parse(row["IsExteriorDetailed"].ToString()),
                DetailsInterior = bool.Parse(row["IsInteriorDetailed"].ToString()),
                DetailingTime = DateTime.Parse(row["DetailingDate"].ToString()),
            };

            return job;
    }
}