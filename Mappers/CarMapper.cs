using System;
using System.Data;
using Detailing.Interfaces;
using Detailing.Entities;

namespace Detailing.Mappers
{
    public class CarMapper : IDataMapper<Car>
    {
        public Car MapToEntity(DataRow row)
        {
            var lastDetailed = row["LastDetailingDate"].ToString();
            var car =  new Car
            {
                Id = Convert.ToInt32(row["Id"]),
                Manufacturer = row["Manufacturer"].ToString(),
                Model = row["Model"].ToString(),
                Year = row["Year"].ToString(),
                Color = row["Color"].ToString(),
                LastDetailed = string.IsNullOrEmpty(lastDetailed) ? null : Convert.ToDateTime(lastDetailed),
            };
            
            return car;
        }
    }
}