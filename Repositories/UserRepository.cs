using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Mappers;
using System.Data;
using Detailing.Services;

namespace Detailing.Controllers
{
    internal class UserRepository : RepositorySerivce<User>
    {
        public UserRepository(IDatabaseService dbService, UserMapper userMapper) : base(dbService, userMapper)
        {
        }

        protected override string SelectAllStoredProcedureName => "sp_User_Select_all";

        protected override string SelectByIdStoredProcedureName => "sp_User_Select_by_Id ";

        protected override string InsertStoredProcedureName => "sp_User_insert";

        protected override string UpdateStoredProcedureName => "sp_User_update";

        protected override string DeleteByIdStoredProcedureName => "sp_User_delete_by_Id";

        public override IDbDataParameter[] GetDbParameters(User user)
        {
            var dbParamerters = new IDbDataParameter[]
                {
                new DatabaseParameter("Id", user.Id),
                new DatabaseParameter("FirstName", user.FirstName),
                new DatabaseParameter("MiddleName", user.MiddleName),
                new DatabaseParameter("LastName", user.LastName),
                new DatabaseParameter("PreferredName", user.PreferredName),
                new DatabaseParameter("DOB", user.DOB),
                new DatabaseParameter("MobileNumber", user.MobileNumber),
                new DatabaseParameter("AddressId", user.Address.Id),
                };

                return dbParamerters;
        }
    }
}