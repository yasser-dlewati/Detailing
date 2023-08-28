using Detailing.Interfaces;
using Detailing.Models;
using System.Data;

namespace Detailing.Providers
{
    public class UserProvider : BaseProvider<User>
    {
        private readonly IDatabaseService _dbService;
        private readonly IDataMapper<User> _dataMapper;
        public UserProvider(IDatabaseService dbService, IDataMapper<User> userMapper) : base(dbService, userMapper)
        {
            _dbService = dbService;
            _dataMapper = userMapper;
        }

        public override string SelectAllStoredProcedureName => "sp_User_Select_all";

        public override string SelectByIdStoredProcedureName => "sp_User_Select_by_Id ";

        private string SelectByEmaillPasswordStoredProcedureName => "sp_user_sel_by_email_password ";

        public override string InsertStoredProcedureName => "sp_User_insert";

        public override string UpdateStoredProcedureName => "sp_User_update";

        public override string DeleteByIdStoredProcedureName => "sp_User_delete_by_Id";

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

        public User GetByLoginCredentials(UserLogin userLogin){
           
           try
            {
                if (userLogin is not null)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("email", userLogin.Email),
                        new DatabaseParameter("password", userLogin.Password),
                    };

                    var dt = _dbService.ExecuteQueryStoredProcedure(SelectByEmaillPasswordStoredProcedureName, spParameters);
                    var model = _dataMapper.MapToModel(dt.Rows[0]);
                    return model;
                }
            }
            catch (Exception)
            {

            }

            return null;
      
        }
    }
}