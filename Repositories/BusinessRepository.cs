using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Services;
using Detailing.Mappers;

namespace Detailing.Repositories
{
    public class BusinessRepository : RepositorySerivce<Business>
    {
        public BusinessRepository(IDatabaseService dbService, BusinessMapper businessMapper) : base(dbService, businessMapper)
        {
        }

        protected override string SelectAllStoredProcedureName => throw new NotImplementedException();

        protected override string SelectByIdStoredProcedureName => throw new NotImplementedException();

        protected override string InsertStoredProcedureName => throw new NotImplementedException();

        protected override string UpdateStoredProcedureName => throw new NotImplementedException();

        protected override string DeleteByIdStoredProcedureName => throw new NotImplementedException();

        public override IDbDataParameter[] GetDbParameters(Business data)
        {
            throw new NotImplementedException();
        }
    }
}