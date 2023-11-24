using System.Data;
using Detailing.Interfaces;
using Detailing.Mappers;
using Detailing.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Detailing.Providers;

public class DetailerServiceProvider : BaseProvider<DetailerService>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<DetailerService> _mapper;
    private readonly IMemoryCache _cache;

    public DetailerServiceProvider(IDatabaseService dbService, IDataMapper<DetailerService> dataMapper, IMemoryCache cache) : base(dbService, dataMapper, cache)
    {
        _dbService = dbService;
        _mapper = dataMapper;
        _cache = cache;
    }

    public override string SelectAllStoredProcedureName => "sp_Service_select_by_DetailerId";

    public override string SelectByIdStoredProcedureName => "sp_Service_select_by_Id_DetailerId";

    public override string InsertStoredProcedureName => "sp_Service_insert";

    public override string UpdateStoredProcedureName => "sp_Service_update";

    public override string DeleteByIdStoredProcedureName => "sp_Service_delete";

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var idParameter = new IDbDataParameter[]
        {
            new DatabaseParameter("ServiceId", id)
        };

        return idParameter;
    }

    public async Task<IEnumerable<DetailerService>> GetDetailerServicesAsync(int detailerId)
    {
        try
        {
            if(_cache.TryGetValue(_cacheKey, out IEnumerable<DetailerService> services))
            {
                System.Console.WriteLine($"Retreiving {GetType()} data from cache.");
                return services;
            }

            var spParameter = GetIdDataParameter(detailerId);
            var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectAllStoredProcedureName, spParameter);
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                var services = new List<DetailerService>();
                foreach (DataRow dtRow in dt.Rows)
                {
                    var service = _mapper.MapToModel(dtRow);
                    services.Add(service);
                }

                _cache.Set(_cacheKey, services);
                return services;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown at GetDetailerServicesAsync({detailerId}): {ex.Message}");
        }

        return Enumerable.Empty<DetailerService>();
    }

    public async Task<DetailerService> GetDetailerServiceAsync(int detailerId, int serviceId)
    {
        try
        {
            var spParameter = new IDbDataParameter[]
{
            new DatabaseParameter("UserId", detailerId),
            new DatabaseParameter("ServiceId", serviceId),
};

            var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByIdStoredProcedureName, spParameter);
            if (dt != null && dt.Rows != null && dt.Rows.Count == 0)
            {
                var service = _mapper.MapToModel(dt.Rows[0]);
                return service;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown at GetDetailerServiceAsync({detailerId},{serviceId}): {ex.Message}");
        }

        return null;
    }

    public async Task<bool> TryDeleteAsync(int detailerId, int serviceId)
    {
        try
        {
            var spParameter = new IDbDataParameter[]
            {
            new DatabaseParameter("UserId", detailerId),
            new DatabaseParameter("ServiceId", serviceId),
            };

            var rowsAffected = await _dbService.ExecuteNonQueryStoredProcedureAsync(SelectByIdStoredProcedureName, spParameter);
            _cache.Remove(_cacheKey);
            return rowsAffected == 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown at TryDeleteAsync({detailerId},{serviceId}): {ex.Message}");
        }

        return false;
    }
}