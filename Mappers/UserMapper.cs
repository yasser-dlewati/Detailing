using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class UserMapper : IDataMapper<User>
    {
        public User MapToModel(DataRow row)
        {
            var user =  new User
            {
                Id = int.Parse(row["Id"].ToString()),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                PreferredName = row["PreferredName"].ToString(),
                DOB = DateTime.Parse(row["DOB"].ToString()),
                MobileNumber = row["MobileNumber"].ToString(),
                Address = new Address{
                    Id = int.Parse(row["AddressId"].ToString()),
                },
                CreatedAt = DateTime.Parse(row["CreationDate"].ToString()),
            };
            
            return user;
        }
    }
}