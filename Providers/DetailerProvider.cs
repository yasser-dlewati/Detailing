using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class DetailerProvider : BaseProvider<Detailer>
{
    private readonly IDatabaseService _dbService;
    private readonly IDataMapper<Detailer> _dataMapper;

    public DetailerProvider(IDatabaseService dbService, IDataMapper<Detailer> dataMapper) : base(dbService, dataMapper)
    {
        _dbService = dbService;
        _dataMapper = dataMapper;
    }

    public override string SelectAllStoredProcedureName => "sp_User_select_by_Type_Detailer";

    public override string SelectByIdStoredProcedureName => "sp_User_select_by_Type_Detailer_Id";

    public override string InsertStoredProcedureName => "sp_User_insert_Detailer";

    public override string UpdateStoredProcedureName => "sp_User_update_Detailer";

    public override string DeleteByIdStoredProcedureName => "sp_User_delete_Detailer";

    public string SelectBusinessCrewStoredProcedureName => "sp_User_select_by_BusinessId";

    public string SelectJobDetailerStoredProcedureName => "sp_User_select_by_JobId";

    public override IDbDataParameter[] GetDbParameters(Detailer data)
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
        new DatabaseParameter("Email", data.Email),
        new DatabaseParameter("Password", data.Password),
        new DatabaseParameter("AddressId", data.Address.Id),
        new DatabaseParameter("Line1", data.Address.Line1),
        new DatabaseParameter("Line2", data.Address.Line2),
        new DatabaseParameter("City", data.Address.City),
        new DatabaseParameter("ZipCode", data.Address.ZipCode),
        new DatabaseParameter("StateId", data.Address.State.Id),
        new DatabaseParameter("CountryId", data.Address.Country.Id),
       };

        return dbParamerters;
    }

    public async Task<IList<Detailer>> GetCrewAsync(int businessId)
    {
        var spParameters = new IDbDataParameter[]
       {
            new DatabaseParameter("BusinessId", businessId),
       };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectBusinessCrewStoredProcedureName, spParameters);
        var detailers = new List<Detailer>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var model = _dataMapper.MapToModel(dt.Rows[i]);
            detailers.Add(model);
        }

        return detailers;
    }

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
            {
                new DatabaseParameter("UserId", id),
            };

        return parameters;
    }

    public async Task<Detailer> GetJobDetailerAsync(int jobId)
    {
        var spParameters = new IDbDataParameter[]
       {
            new DatabaseParameter("JobId", jobId),
       };

        var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectJobDetailerStoredProcedureName, spParameters);
        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
        {
            var detailer = _dataMapper.MapToModel(dt.Rows[0]);
            return detailer;
        }

        return null;
    }
}
