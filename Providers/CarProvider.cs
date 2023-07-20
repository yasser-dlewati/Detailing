using System.Data;
using Detailing.Entities;
using Detailing.Interfaces;
using Detailing.Helpers;
using Detailing.Mappers;

namespace Detailing.Providers
{
    public class CarProvider : IEntityProvider<Car>
    {
        public List<Car> GetAll()
        {
            var dtCar = DatabaseHelper.ExecuteReadStoredProcedure("sp_CarsSelectAll");
            var cars = new List<Car>();
            var carMapper = new CarMapper();
            for(var i=0; i<dtCar.Rows.Count; i++)
            {
                var car = carMapper.MapToEntity(dtCar.Rows[i]);
                cars.Add(car);
            }
            return cars;
        }
    }
}