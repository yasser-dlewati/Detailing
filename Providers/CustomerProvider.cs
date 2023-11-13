using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class CustomerProvider : BaseProvider<Customer>
{

    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Customer> _mapper;
    public CustomerProvider(IDatabaseService dbService, IDataMapper<Customer> mapper) : base(dbService, mapper)
    {
        _dbService = dbService;
        _mapper = mapper;
    }

    public override string SelectAllStoredProcedureName => "sp_User_select_by_Type_Customer";

    public override string SelectByIdStoredProcedureName => "sp_User_select_by_Type_Customer_Id";

    public override string InsertStoredProcedureName => "sp_User_insert_Customer";

    public override string UpdateStoredProcedureName => "sp_User_update_Customer";

    public override string DeleteByIdStoredProcedureName => "sp_User_delete_Customer_by_Id";

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectAllStoredProcedureName);
        var models = new List<Customer>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = int.Parse(dt.Rows[i]["UserId"].ToString());
            var model = _mapper.MapToModel(dt.Rows[i]);
            if (models.Any(x => x.Id == id))
            {
                var existingModel = models.Where(x => x.Id == id).First();
                (existingModel.Cars as List<Car>).Add(model.Cars.ElementAt(0));
            }
            else
            {
                models.Add(model);
            }
        }

        return models;
    }

    public override async Task<Customer> GetByIdAsync(int id)
    {
        var spParameters = new IDbDataParameter[]
        {
            new DatabaseParameter("Id", id),
        };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByIdStoredProcedureName, spParameters);
        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
        {
            var model = _mapper.MapToModel(dt.Rows[0]);
            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var currentRecord = _mapper.MapToModel(dt.Rows[i]);
                (model.Cars as List<Car>).Add(currentRecord.Cars.ElementAt(0));
            }

            return model;
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
        new DatabaseParameter("Password", data.Password),
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
                var spParameters = new IDbDataParameter[]
                {
                        new DatabaseParameter("Id", id),
                };
                var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(DeleteByIdStoredProcedureName, spParameters);
                return rowsAffectd > 1;
            }
        }
        catch (Exception)
        {
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
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return false;

    }

    public override async Task<bool> TryUpdateAsync(Customer data)
    {
        try
        {
            var spParameters = GetDbParameters(data);
            var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(UpdateStoredProcedureName, spParameters);
            return rowsAffectd == 1;
        }
        catch (Exception ex)
        {
        }

        return false;
    }
}