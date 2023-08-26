using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Services;
using Detailing.Mappers;

namespace Detailing.Providers
{
    public class BusinessProvider : BaseProvider<Business>
    {
        public BusinessProvider(IDatabaseService dbService, IDataMapper<Business> businessMapper) : base(dbService, businessMapper)
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