using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class DetailerServiceProvider : BaseProvider<DetailerService>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<DetailerService> _mapper;
    public DetailerServiceProvider(IDatabaseService dbService, IDataMapper<DetailerService> dataMapper) : base(dbService, dataMapper)
    {
        _dbService = dbService;
        _mapper = dataMapper;
    }

    public override string SelectAllStoredProcedureName => throw new NotImplementedException();

    public override string SelectByIdStoredProcedureName => "sp_Service_select_by_Id";

    public override string InsertStoredProcedureName => "sp_Service_insert";

    public override string UpdateStoredProcedureName => "sp_Service_update";

    public override string DeleteByIdStoredProcedureName => "sp_Service_delete";

    public string SelectByDetailerIdStoredProcedureName => "sp_Service_select_by_DetailerId";

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
        var spParameter = new IDbDataParameter[]
        {
            new DatabaseParameter("UserId", detailerId)
        };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByDetailerIdStoredProcedureName, spParameter);
        if(dt!=null && dt.Rows!= null && dt.Rows.Count>0)
        {
            var services = new List<DetailerService>();
            foreach(DataRow dtRow in dt.Rows)
            {
                var service = _mapper.MapToModel(dtRow);
                services.Add(service);
            }

            return services;
        }

        return Enumerable.Empty<DetailerService>();
    }
}