using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class CustomerProvider : IUserTypeProvider<Customer>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Customer> _mapper;
    public CustomerProvider(IDatabaseService dbService, IDataMapper<Customer> mapper)
    {
        _dbService = dbService;
        _mapper = mapper;
    }

    public string SelectTypeStoredProcedureName => "sp_User_select_by_Type_Customer";

    public IEnumerable<Customer> GetUsersOfType()
    {
        var dt = _dbService.ExecuteQueryStoredProcedure(SelectTypeStoredProcedureName);
        var models = new List<Customer>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = int.Parse(dt.Rows[i]["UserId"].ToString());
            var model = _mapper.MapToModel(dt.Rows[i]);
            if (models.Any(x => x.Id == id))
            {
                var existingModel = models.Where(x => x.Id == id).First();
                (existingModel.Cars as List<Car>).Add(model.Cars.First());
            }
            else
            {
                models.Add(model);
            }
        }

        Console.WriteLine($"{GetType().Name}.{nameof(GetUsersOfType)} returned {models.Count} objects");
        return models;
    }
}