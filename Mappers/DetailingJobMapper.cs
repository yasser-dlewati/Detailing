using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers;

public class DetailingJobMapper : IDataMapper<DetailingJob>
{
    public DetailingJob MapToModel(DataRow row)
    {
        var carMapper = new CarMapper();
        var detailerMapper = new DetailerMapper();
        var businessMapper = new BusinessMapper();
        var isDetailedByBusiness = int.TryParse(row["BusinessId"].ToString(), out var businessId);
         var job = new DetailingJob
            {
                Id = int.Parse(row["JobId"].ToString()),
                DetailedCar = carMapper.MapToModel(row),
                Detailer = detailerMapper.MapToModel(row),
                Business = isDetailedByBusiness 
                ? businessMapper.MapToModel(row)
                : null,
                DetailingTime = DateTime.Parse(row["DetailingDate"].ToString()),
            };

            return job;
    }
}