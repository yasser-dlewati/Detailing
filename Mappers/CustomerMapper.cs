using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class CustomerMapper : IDataMapper<Customer>
    {
        public Customer MapToModel(DataRow row)
        {
            var carMapper = new CarMapper();
            var addressMapper = new AddressMapper();
            var customer = new Customer
            {
                Id = int.Parse(row["UserId"].ToString()),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                PreferredName = row["PreferredName"].ToString(),
                DOB = DateTime.Parse(row["DOB"].ToString()),
                MobileNumber = row["MobileNumber"].ToString(),
                Address = addressMapper.MapToModel(row),
                CreatedAt = DateTime.Parse(row["CreationDate"].ToString()),
                Cars = new List<Car>(),
            };

            if (!string.IsNullOrEmpty(row["CarId"].ToString()))
            {
                var car = carMapper.MapToModel(row);
                (customer.Cars as List<Car>).Add(car);
            }
            
            return customer;
        }
    }
}