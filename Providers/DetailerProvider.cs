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

    public override string InsertStoredProcedureName => throw new NotImplementedException();

    public override string UpdateStoredProcedureName => throw new NotImplementedException();

    public override string DeleteByIdStoredProcedureName => throw new NotImplementedException();

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
        new DatabaseParameter("AddressId", data.Address.Id),
        new DatabaseParameter("Email", data.Email),
        new DatabaseParameter("HasBusiness", data.HasBusiness),
        new DatabaseParameter("DetailsExterior", data.DetailsExterior),
        new DatabaseParameter("DetailsInterior", data.DetailsInterior),
        new DatabaseParameter("IsMobile", data.IsMobile),       
        };

        return dbParamerters;
    }
}
