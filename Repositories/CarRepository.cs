using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Mappers;

namespace Detailing.Repositories
{
    public class CarRepository : IEntityRepository<Car>
    {
        private readonly IDatabaseService _dbService;
        private readonly CarMapper carMapper = new CarMapper();

        public CarRepository(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Car> GetAll()
        {
            var dtCar = _dbService.ExecuteQueryStoredProcedure("sp_car_select_all");
            var cars = new List<Car>();
            for (var i = 0; i < dtCar.Rows.Count; i++)
            {
                var car = carMapper.MapToModel(dtCar.Rows[i]);
                cars.Add(car);
            }
            return cars;
        }

        public Car GetSingleById(int carId)
        {
            try
            {
                if (carId > 0)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("Id", carId),
                    };

                    var dtcar = _dbService.ExecuteQueryStoredProcedure("sp_car_select_by_Id", spParameters);
                    var car = carMapper.MapToModel(dtcar.Rows[0]);
                    return car;
                }
            }
            catch (Exception)
            {

            }

            return null;
        }

        public bool TryDelete(int carId)
        {
            try
            {
                if (carId > 0)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("Id", carId),
                    };
                    var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure("sp_car_delete_by_Id", spParameters);
                    return rowsAffectd == 1;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool TryInsert(Car car, out int insertedId)
        {
            try
            {
                var spParameters = new IDbDataParameter[]
                {
                new DatabaseParameter("Manufacturer", car.Manufacturer),
                new DatabaseParameter("Model", car.Model),
                new DatabaseParameter("Year", car.Year),
                new DatabaseParameter("Color", car.Color),
                new DatabaseParameter("OwnerId", car.Owner.Id),
                new DatabaseParameter("LastDetailingDate", car.LastDetailed),
                };
                insertedId = _dbService.ExecuteNonQueryStoredProcedure("sp_car_insert", spParameters);
                return insertedId > 0;
            }
            catch (Exception ex)
            {
            }

            insertedId = 0;
            return false;
        }

        public bool TryUpdate(Car car)
        {
            try
            {
                var spParameters = new IDbDataParameter[]
                {
                new DatabaseParameter("Id", car.Id),
                new DatabaseParameter("Manufacturer", car.Manufacturer),
                new DatabaseParameter("Model", car.Model),
                new DatabaseParameter("Year", car.Year),
                new DatabaseParameter("Color", car.Color),
                new DatabaseParameter("OwnerId", car.Owner.Id),
                new DatabaseParameter("LastDetailingDate", car.LastDetailed),
                };
                var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure("sp_car_update", spParameters);
                return rowsAffectd == 1;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}