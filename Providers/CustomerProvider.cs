using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Mappers;
using Microsoft.Extensions.Caching.Memory;

namespace Detailing.Providers;

public class CustomerProvider : BaseProvider<Customer>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Customer> _mapper;
    private readonly IMemoryCache _cache;

    public CustomerProvider(IDatabaseService dbService, IDataMapper<Customer> mapper, IMemoryCache cache) : base(dbService, mapper, cache)
    {
        _dbService = dbService;
        _mapper = mapper;
        _cache = cache;
    }

    public override string SelectAllStoredProcedureName => "sp_User_select_by_Type_Customer";

    public override string SelectByIdStoredProcedureName => "sp_User_select_by_Type_Customer_Id";

    public override string InsertStoredProcedureName => "sp_User_insert_Customer";

    public override string UpdateStoredProcedureName => "sp_User_update_Customer";

    public override string DeleteByIdStoredProcedureName => "sp_User_delete_Customer";

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        if (_cache.TryGetValue(_cacheKey, out IEnumerable<Customer> cachedData))
        {
            Console.WriteLine($"Retreiving {_cacheKey} data from cache.");
            return cachedData;
        }

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectAllStoredProcedureName);
        var models = new List<Customer>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = int.Parse(dt.Rows[i]["UserId"].ToString());
            if (models.Any(x => x.Id == id))
            {
                var existingModel = models.First(x => x.Id == id);
                var carMapper = new CarMapper();
                var car = carMapper.MapToModel(dt.Rows[i]);
                existingModel.Cars = existingModel.Cars.Append(car);
            }
            else
            {
                var model = _mapper.MapToModel(dt.Rows[i]);
                models.Add(model);
            }
        }

        _cache.Set(_cacheKey, models);
        return models;
    }

    public override async Task<Customer> GetByIdAsync(int id)
    {
        if(_cache.TryGetValue(_cacheKey[id], out Customer customer))
        {
            Console.WriteLine($"Retreiving {_cacheKey[id]} data from cache.");
            return customer;
        }

        var spParameters = GetIdDataParameter(id);
        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByIdStoredProcedureName, spParameters);
        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
        {
            customer = _mapper.MapToModel(dt.Rows[0]);
            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var currentRecord = _mapper.MapToModel(dt.Rows[i]);
                customer.Cars = customer.Cars.Append(currentRecord.Cars.ElementAt(0));
            }

            _cache.Set(_cacheKey[id], customer);
            return customer;
        }

        return null;
    }

    public override IDbDataParameter[] GetDbParameters(Customer data)
    {
        var dbParamerters = new IDbDataParameter[]
        {
            new DatabaseParameter("UserId", data.Id),
            new DatabaseParameter("FirstName", data.FirstName),
            new DatabaseParameter("MiddleName", data.MiddleName),
            new DatabaseParameter("LastName", data.LastName),
            new DatabaseParameter("PreferredName", data.PreferredName),
            new DatabaseParameter("DOB", data.DOB),
            new DatabaseParameter("MobileNumber", data.MobileNumber),
            new DatabaseParameter("AddressId", data.Address.Id),
            new DatabaseParameter("Line1", data.Address.Line1),
            new DatabaseParameter("Line2", data.Address.Line2),
            new DatabaseParameter("City", data.Address.City),
            new DatabaseParameter("ZipCode", data.Address.ZipCode),
            new DatabaseParameter("StateId", data.Address.State.Id),
            new DatabaseParameter("CountryId", data.Address.Country.Id),
            new DatabaseParameter("Longitude", data.Address.Longitude),
            new DatabaseParameter("Latitude", data.Address.Latitude),
            new DatabaseParameter("Email", data.Email),
        };

        return dbParamerters;
    }

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
            {
                new DatabaseParameter("UserId", id),
            };

        return parameters;
    }

    public override async Task<bool> TryDeleteAsync(int id)
    {
        try
        {
            if (id > 0)
            {
                var spParameters = GetIdDataParameter(id);
                var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(DeleteByIdStoredProcedureName, spParameters);
                _cache.Remove(_cacheKey);
                return rowsAffectd > 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown while {GetType()}.{nameof(TryDeleteAsync)}. \n{ex.Message}");
        }

        return false;
    }

    public override bool TryInsert(ref Customer data)
    {
        try
        {
            var spParameters = GetDbParameters(data);
            var dt = _dbService.ExecuteQueryStoredProcedureAsync(InsertStoredProcedureName, spParameters).Result;
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                data = _mapper.MapToModel(dt.Rows[0]);
                _cache.Remove(_cacheKey);
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown while {GetType()}.{nameof(TryInsert)}. \n{ex.Message}");
        }

        return false;

    }

    public override async Task<bool> TryUpdateAsync(Customer data)
    {
        try
        {
            var spParameters = GetDbParameters(data);
            var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(UpdateStoredProcedureName, spParameters);
            _cache.Remove(_cacheKey);
            return rowsAffectd == 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception thrown while {GetType()}.{nameof(TryUpdateAsync)}. \n{ex.Message}");
        }

        return false;
    }
}