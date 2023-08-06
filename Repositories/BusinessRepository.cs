using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Services;
using Detailing.Mappers;

namespace Detailing.Repositories
{
    public class BusinessRepository : BaseRepositoryService<Business>
    {
        public BusinessRepository(IDatabaseService dbService, IDataMapper<Business> businessMapper) : base(dbService, businessMapper)
        {
        }

        public override string SelectAllStoredProcedureName => throw new NotImplementedException();

        public override string SelectByIdStoredProcedureName => throw new NotImplementedException();

        public override string InsertStoredProcedureName => throw new NotImplementedException();

        public override string UpdateStoredProcedureName => throw new NotImplementedException();

        public override string DeleteByIdStoredProcedureName => throw new NotImplementedException();

        public override IDbDataParameter[] GetDbParameters(Business data)
        {
            throw new NotImplementedException();
        }
    }
}