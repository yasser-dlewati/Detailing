using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Detailing.Providers;

public class CarProvider : BaseProvider<Car>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Car> _carMapper;

    public CarProvider(IDatabaseService dbService, IDataMapper<Car> carMapper, IMemoryCache cache) : base(dbService, carMapper, cache)
    {
        _dbService = dbService;
        _carMapper = carMapper;
    }

    public override string SelectAllStoredProcedureName => "sp_Car_select_all";

    public override string SelectByIdStoredProcedureName => "sp_Car_select_by_Id";

    public override string InsertStoredProcedureName => "sp_Car_insert";

    public override string UpdateStoredProcedureName => "sp_Car_update";

    public override string DeleteByIdStoredProcedureName => "sp_Car_delete";

    public string SelectByCustomerIdStoredProcedureName = "sp_Car_select_by_UserId";

    public string DeleteByCustomerIdStoredProcedureName = "sp_Car_delete_by_Id_UserId";

    public string SelectByJobIdStoredProcedureName = "sp_Car_select_by_JobId";

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
        {
            new DatabaseParameter("CarId", id)
        };

        return parameters;
    }

    public async Task<IEnumerable<Car>> GetByCustomerIdAsync(int customerId)
    {
        var spParameters = new IDbDataParameter[]
        {
            new DatabaseParameter("UserId", customerId),
        };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByCustomerIdStoredProcedureName, spParameters);
        var cars = new List<Car>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var car = _carMapper.MapToModel(dt.Rows[i]);
            cars.Add(car);
        }

        return cars;
    }

    public async Task<bool> TryDeleteCustomerCarAsync(int customerId, int carId)
    {
        try
        {
            var spParameters = new IDbDataParameter[]
            {
                new DatabaseParameter("CarId", carId),
                new DatabaseParameter("UserId", customerId),
            };
            var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(DeleteByIdStoredProcedureName, spParameters);
            return rowsAffectd == 1;

        }
        catch (Exception)
        {
        }

        return false;
    }

    public async Task<Car> GetCarByJobIdAsync(int jobId)
    {
        var spParameters = new IDbDataParameter[]
       {
            new DatabaseParameter("JobId", jobId),
       };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByJobIdStoredProcedureName, spParameters);
        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
        {
            var car = _carMapper.MapToModel(dt.Rows[0]);
            return car;
        }

        return null;
    }
}
