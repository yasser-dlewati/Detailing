using Detailing.Interfaces;
using Detailing.Entities;
using Detailing.Helpers;
using Detailing.Mappers;
using MySql.Data.MySqlClient;
namespace Detailing.Repositories
{
    public class CarRepository : IEntityRepository<Car>
    {
        private readonly DatabaseHelper<Car> dbHelper = new DatabaseHelper<Car>();

        public IEnumerable<Car> GetAll()
        {
            var dtCar = dbHelper.ExecuteQueryStoredProcedure("sp_CarsSelectAll");
            var cars = new List<Car>();
            var carMapper = new CarMapper();
            for (var i = 0; i < dtCar.Rows.Count; i++)
            {
                var car = carMapper.MapToEntity(dtCar.Rows[i]);
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
                    var spParameter = new MySqlParameter("p_Id", carId);
                    var car = dbHelper.ExecuteScalarQueryStoredProcedure("sp_car_select_by_Id", spParameter);
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
                if(carId > 0)
                {
                    var spParameters = new MySqlParameter[]
                    {
                        new MySqlParameter("p_Id", carId),
                    };
                    var rowsAffectd = dbHelper.ExecuteNonQueryStoredProcedure("sp_car_select_by_Id", spParameters);
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
                var spParameters = new MySqlParameter[]
                {
                new MySqlParameter("p_Manufacturer", car.Manufacturer),
                new MySqlParameter("p_Model", car.Model),
                new MySqlParameter("p_Year", car.Year),
                new MySqlParameter("p_Color", car.Color),
                new MySqlParameter("p_OwnerId", car.Owner.Id),
                new MySqlParameter("p_LastDetailingDate", car.LastDetailed),
                };
                insertedId = dbHelper.ExecuteNonQueryStoredProcedure("sp_car_insert", spParameters);
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
                var spParameters = new MySqlParameter[]
                {
                new MySqlParameter("p_Id", car.Id),
                new MySqlParameter("p_Manufacturer", car.Manufacturer),
                new MySqlParameter("p_Model", car.Model),
                new MySqlParameter("p_Year", car.Year),
                new MySqlParameter("p_Color", car.Color),
                new MySqlParameter("p_OwnerId", car.Owner.Id),
                new MySqlParameter("p_LastDetailingDate", car.LastDetailed),
                };
                var rowsAffectd = dbHelper.ExecuteNonQueryStoredProcedure("sp_car_update", spParameters);
                return rowsAffectd == 1;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}