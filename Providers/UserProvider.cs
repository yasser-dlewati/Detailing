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

        public override string DeleteByIdStoredProcedureName => "sp_User_delete";

        public override IDbDataParameter[] GetDbParameters(User user)
        {
            var dbParamerters = new IDbDataParameter[]
                {
                new DatabaseParameter("FirstName", user.FirstName),
                new DatabaseParameter("MiddleName", user.MiddleName),
                new DatabaseParameter("LastName", user.LastName),
                new DatabaseParameter("PreferredName", user.PreferredName),
                new DatabaseParameter("DOB", user.DOB),
                new DatabaseParameter("MobileNumber", user.MobileNumber),
                new DatabaseParameter("Email", user.Email),
                new DatabaseParameter("Type", user.Role),
                new DatabaseParameter("AddressId", user.Address.Id),
                new DatabaseParameter("Line1", user.Address.Line1),
                new DatabaseParameter("Line2", user.Address.Line2),
                new DatabaseParameter("City", user.Address.City),
                new DatabaseParameter("ZipCode", user.Address.ZipCode),
                new DatabaseParameter("StateId", user.Address.State.Id),
                new DatabaseParameter("CountryId", user.Address.Country.Id),
                new DatabaseParameter("Longitude", user.Address.Longitude),
                new DatabaseParameter("Latitude", user.Address.Latitude),
                };

            return dbParamerters;
        }

        public async Task<User> GetByLoginCredentialsAsync(LoginUser LoginUser)
        {
            try
            {
                if (LoginUser is not null)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("email", LoginUser.Email),
                        new DatabaseParameter("password", LoginUser.Password),
                    };

                    var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByEmaillPasswordStoredProcedureName, spParameters);
                    var model = _dataMapper.MapToModel(dt.Rows[0]);
                    return model;
                }
            }
            catch (Exception)
            {

            }

            return null;

        }

        public override IDbDataParameter[] GetIdDataParameter(int id)
        {
            var parameters = new IDbDataParameter[]
            {
                new DatabaseParameter("UserId", id),
            };

            return parameters;
        }

        public new bool TryInsert(ref SignupUser user)
        {
            try
            {
                var dbParams = GetDbParameters(user).Append(new DatabaseParameter("Password", user.Password)).ToArray();
                var dt = _dbService.ExecuteQueryStoredProcedureAsync(InsertStoredProcedureName, dbParams).Result;
                if(dt.Rows.Count > 0)
                {
                    user.Id = int.Parse(dt.Rows[0]["UserId"].ToString());
                    user.Address.Id = int.Parse(dt.Rows[0]["AddressId"].ToString());
                }

                return dt.Rows.Count == 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception thrown at {GetType()}.{nameof(TryInsert)}.\n{ex.Message}.");
            }

            return false;
        }
    }
}