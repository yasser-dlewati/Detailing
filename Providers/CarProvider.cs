using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers
{
    public class CarProvider : BaseProvider<Car>
    {
        public CarProvider(IDatabaseService dbService, IDataMapper<Car> carMapper) : base (dbService, carMapper)
        {
            
        }

        public override string SelectAllStoredProcedureName => "sp_car_select_all";

        public override string SelectByIdStoredProcedureName => "sp_car_select_by_Id";

        public override string InsertStoredProcedureName => "sp_car_insert";

        public override string UpdateStoredProcedureName => "sp_car_update";

        public override string DeleteByIdStoredProcedureName => "sp_car_delete_by_Id";
    }
}