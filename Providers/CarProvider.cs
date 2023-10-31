using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class CarProvider : BaseProvider<Car>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Car> _carMapper;

    public CarProvider(IDatabaseService dbService, IDataMapper<Car> carMapper) : base(dbService, carMapper)
    {
        _dbService = dbService;
        _carMapper = carMapper;
    }

    public override string SelectAllStoredProcedureName => "sp_car_select_all";

    public override string SelectByIdStoredProcedureName => "sp_car_select_by_Id";

    public override string InsertStoredProcedureName => "sp_car_insert";

    public override string UpdateStoredProcedureName => "sp_car_update";

    public override string DeleteByIdStoredProcedureName => "sp_car_delete_by_Id";

    public string SelectByCustomerIdStoredProcedureName = "sp_Car_select_by_UserId";

    public string DeleteByCustomerIdStoredProcedureName = "sp_car_delete_by_Id_UserId";

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
        {
            new DatabaseParameter("CarId", id)
        };

        return parameters;
    }

    public IEnumerable<Car> GetByCustomerId(int customerId)
    {
        var spParameters = new IDbDataParameter[]
        {
            new DatabaseParameter("UserId", customerId),
        };

        var dt = _dbService.ExecuteQueryStoredProcedure(SelectByCustomerIdStoredProcedureName, spParameters);
        var cars = new List<Car>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var car = _carMapper.MapToModel(dt.Rows[i]);
            cars.Add(car);
        }

        return cars;
    }

    public bool TryDeleteCustomerCar(int customerId, int carId)
    {
        try
        {
            var spParameters = new IDbDataParameter[]
            {
                new DatabaseParameter("CarId", carId),
                new DatabaseParameter("UserId", customerId),
            };
            var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(DeleteByIdStoredProcedureName, spParameters);
            return rowsAffectd == 1;

        }
        catch (Exception)
        {
        }

        return false;
    }
}
