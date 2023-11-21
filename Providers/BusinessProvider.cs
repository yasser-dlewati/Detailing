using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers
{
    public class BusinessProvider : BaseProvider<Business>
    {
        private readonly IDatabaseService _dbService;
        private readonly IDataMapper<Business> _businessMapper;
        public BusinessProvider(IDatabaseService dbService, IDataMapper<Business> businessMapper) : base(dbService, businessMapper)
        {
            _dbService = dbService;
            _businessMapper = businessMapper;
        }

        public override string SelectAllStoredProcedureName => "sp_Business_select_all";

        public override string SelectByIdStoredProcedureName => "sp_Business_select_by_Id";

        public override string InsertStoredProcedureName => "sp_Business_insert";

        public override string UpdateStoredProcedureName => "sp_Business_update";

        public override string DeleteByIdStoredProcedureName => "sp_Business_delete";

        public string SelectByJobIdStoredProcedureName => "sp_Business_select_by_JobId";

        public override IDbDataParameter[] GetDbParameters(Business business)
        {
            var dbParamerters = new IDbDataParameter[]
            {
                new DatabaseParameter("BusinessId", business.Id),
                new DatabaseParameter("UserId", business.OwnerId),
                new DatabaseParameter("AddressId", business.Address.Id),
                new DatabaseParameter("BusinessName", business.BusinessName),
                new DatabaseParameter("Description", business.Description),
                new DatabaseParameter("Established", business.Established),
                new DatabaseParameter("Phone", business.Phone),
                new DatabaseParameter("Website", business.Website),
                new DatabaseParameter("SocialMedia", business.SocialMedia),
                new DatabaseParameter("Email", business.Email),
                new DatabaseParameter("Line1", business.Address.Line1),
                new DatabaseParameter("Line2", business.Address.Line2),
                new DatabaseParameter("ZipCode", business.Address.ZipCode),
                new DatabaseParameter("City", business.Address.City),
                new DatabaseParameter("StateId", business.Address.State.Id),
                new DatabaseParameter("CountryId", business.Address.Country.Id),
                new DatabaseParameter("Longitude", business.Address.Longitude),
                new DatabaseParameter("Latitude", business.Address.Latitude),
            };

            return dbParamerters;
        }

        public override IDbDataParameter[] GetIdDataParameter(int id)
        {
            var parameters = new IDbDataParameter[]
            {
                new DatabaseParameter("BusinessId", id),
            };

            return parameters;
        }

        public async Task<Business> GetBusinessByJobIdAsync(int jobId)
        {
            var parameter = new IDbDataParameter[]
            {
                new DatabaseParameter("JobId", jobId),
            };

            var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByJobIdStoredProcedureName, parameter);
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                var business = _businessMapper.MapToModel(dt.Rows[0]);
                return business;
            }

            return null;
        }
    }
}