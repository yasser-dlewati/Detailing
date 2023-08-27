using System.Data;
using Detailing.Interfaces;
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
            (model.Cars as List<Car> ).Add(currentRecord.Cars.ElementAt(0));
        }

        return model;
    }

    public IDbDataParameter[] GetDbParameters(Customer data)
    {
        throw new NotImplementedException();
    }

    public bool TryDelete(int id)
    {
        throw new NotImplementedException();
    }

    public bool TryInsert(Customer data, out int insertedId)
    {
        throw new NotImplementedException();
    }

    public bool TryUpdate(Customer data)
    {
        throw new NotImplementedException();
    }
}