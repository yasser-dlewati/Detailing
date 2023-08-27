using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class CarMapper : IDataMapper<Car>
    {
        public Car MapToModel(DataRow row)
        {
            var lastDetailed = row["LastDetailingDate"].ToString();
            var car =  new Car
            {
                Id = Convert.ToInt32(row["Id"]),
                Manufacturer = row["Manufacturer"].ToString(),
                Model = row["Model"].ToString(),
                Year = row["Year"].ToString(),
                Color = row["Color"].ToString(),
                OwnerId = Convert.ToInt32(row["OwnerId"]),
                LastDetailed = string.IsNullOrEmpty(lastDetailed) ? null : Convert.ToDateTime(lastDetailed),
            };
            
            return car;
        }
    }
}