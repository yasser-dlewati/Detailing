using System.Data;
using Detailing.Interfaces;
using Detailing.Mappers;
using Detailing.Models;

namespace Detailing.Providers;

public class CustomerProvider : IModelProvider<Customer>
{

    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Customer> _mapper;
    public CustomerProvider(IDatabaseService dbService, IDataMapper<Customer> mapper)
    {
        _dbService = dbService;
        _mapper = mapper;
    }

    public string SelectAllStoredProcedureName => "sp_User_select_by_Type_Customer";

    public string SelectByIdStoredProcedureName => "sp_User_select_by_Type_Customer_Id";

    public string InsertStoredProcedureName => "sp_User_insert_Customer";

    public string UpdateStoredProcedureName => "sp_User_update_Customer";

    public string DeleteByIdStoredProcedureName => "sp_User_delete_Customer";

    public IEnumerable<Customer> GetAll()
    {
        var dt = _dbService.ExecuteQueryStoredProcedure(SelectAllStoredProcedureName);
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

    public Customer GetById(int id)
    {
        var spParameters = new IDbDataParameter[]
        {
            new DatabaseParameter("Id", id),
        };

        var dt = _dbService.ExecuteQueryStoredProcedure(SelectByIdStoredProcedureName, spParameters);
        var model = _mapper.MapToModel(dt.Rows[0]);
        for (var i = 1; i < dt.Rows.Count; i++)
        {

            var currentRecord = _mapper.MapToModel(dt.Rows[i]);
            (model.Cars as List<Car>).Add(currentRecord.Cars.ElementAt(0));
        }

        return model;
    }

    public IDbDataParameter[] GetDbParameters(Customer data)
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
        new DatabaseParameter("CarId", data.Cars.ElementAt(0).Id),
        new DatabaseParameter("Manufacturer", data.Cars.ElementAt(0).Manufacturer),
        new DatabaseParameter("Model", data.Cars.ElementAt(0).Model),
        new DatabaseParameter("Year", data.Cars.ElementAt(0).Year),
        new DatabaseParameter("Color", data.Cars.ElementAt(0).Color),
        new DatabaseParameter("LastDetailingDate", data.Cars.ElementAt(0).LastDetailed),
        new DatabaseParameter("OwnerId", data.Id),
        };

        return dbParamerters;
    }

    public bool TryDelete(int id)
    {
        try
        {
            if (id > 0)
            {
                var spParameters = new IDbDataParameter[]
                {
                        new DatabaseParameter("Id", id),
                };
                var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(DeleteByIdStoredProcedureName, spParameters);
                return rowsAffectd > 1;
            }
        }
        catch (Exception)
        {
        }

        return false;
    }

    public bool TryInsert(Customer data, out int insertedId)
    {
        try
        {
            var spParameters = GetDbParameters(data);
            insertedId = _dbService.ExecuteNonQueryStoredProcedure(InsertStoredProcedureName, spParameters);
            return insertedId > 0;
        }
        catch (Exception ex)
        {
        }

        insertedId = 0;
        return false;

    }

    public bool TryUpdate(Customer data)
    {
        try
        {
            var spParameters = GetDbParameters(data);
            var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(UpdateStoredProcedureName, spParameters);
            return rowsAffectd == 1;
        }
        catch (Exception ex)
        {
        }

        return false;
    }
}