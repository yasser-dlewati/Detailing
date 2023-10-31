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

    public override string DeleteByIdStoredProcedureName => "sp_User_delete_Detailer_by_Id";

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
        new DatabaseParameter("HasBusiness", data.HasBusiness),
        new DatabaseParameter("DetailsExterior", data.DetailsExterior),
        new DatabaseParameter("DetailsInterior", data.DetailsInterior),
        new DatabaseParameter("IsMobile", data.IsMobile),       
        new DatabaseParameter("AddressId", data.Address.Id),
        new DatabaseParameter("Line1", data.Address.Line1),
        new DatabaseParameter("Line2", data.Address.Line2),
        new DatabaseParameter("City", data.Address.City),
        new DatabaseParameter("ZipCode", data.Address.ZipCode),
        new DatabaseParameter("StateId", data.Address.StateId),
        new DatabaseParameter("CountryId", data.Address.CountryId),
        };

        return dbParamerters;
    }

    public IList<Detailer> GetCrew(int businessId)
    {
         var spParameters = new IDbDataParameter[]
        {
            new DatabaseParameter("BusinessId", businessId),
        };

        var dt = _dbService.ExecuteQueryStoredProcedure(SelectBusinessCrewStoredProcedureName, spParameters);
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
}
