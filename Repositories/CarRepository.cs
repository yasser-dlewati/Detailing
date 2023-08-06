using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Mappers;
using Detailing.Services;
using System.Data.Common;

namespace Detailing.Repositories
{
    public class CarRepository : BaseRepositoryService<Car>
    {
        public CarRepository(IDatabaseService dbService, IDataMapper<Car> carMapper) : base (dbService, carMapper)
        {
            
        }

        public override string SelectAllStoredProcedureName => "sp_car_select_all";

        public override string SelectByIdStoredProcedureName => "sp_car_select_by_Id";

        public override string InsertStoredProcedureName => "sp_car_insert";

        public override string UpdateStoredProcedureName => "sp_car_update";

        public override string DeleteByIdStoredProcedureName => "sp_car_delete_by_Id";

        public override IDbDataParameter[] GetDbParameters(Car car)
        {
             var dbParamerters = new IDbDataParameter[]
                {
                new DatabaseParameter("Id", car.Id),
                new DatabaseParameter("Manufacturer", car.Manufacturer),
                new DatabaseParameter("Model", car.Model),
                new DatabaseParameter("Year", car.Year),
                new DatabaseParameter("Color", car.Color),
                new DatabaseParameter("OwnerId", car.Owner.Id),
                new DatabaseParameter("LastDetailingDate", car.LastDetailed),
                };

                return dbParamerters;
        }

    }
}