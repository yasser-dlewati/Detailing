using Detailing.Interfaces;
using Detailing.Entities;
using Detailing.Helpers;
using Detailing.Mappers;

namespace Detailing.Repositories
{
    public class CarRepository : IEntityRepository<Car>
    {
        public IEnumerable<Car> GetAll()
        {
            var dbHelper = new DatabaseHelper<Car>();
           var dtCar = dbHelper.ExecuteQueryStoredProcedure("sp_CarsSelectAll");
            var cars = new List<Car>();
            var carMapper = new CarMapper();
            for(var i=0; i<dtCar.Rows.Count; i++)
            {
                var car = carMapper.MapToEntity(dtCar.Rows[i]);
                cars.Add(car);
            }
            return cars;
        }

        public Car GetSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public bool TryDelete(Car data)
        {
            throw new NotImplementedException();
        }

        public bool TryInsert(Car data)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(Car data)
        {
            throw new NotImplementedException();
        }
    }
}