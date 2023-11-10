using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Mappers
{
    public class CustomerMapper : IDataMapper<Customer>
    {
        public Customer MapToModel(DataRow row)
        {
            var customer = new Customer
            {
                Id = int.Parse(row["Id"].ToString()),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                PreferredName = row["PreferredName"].ToString(),
                DOB = DateTime.Parse(row["DOB"].ToString()),
                MobileNumber = row["MobileNumber"].ToString(),
                Address = new Address
                {
                    Id = int.Parse(row["AddressId"].ToString()),
                },
                CreatedAt = DateTime.Parse(row["CreationDate"].ToString()),
                Cars = new List<Car>(),
            };
            if (!string.IsNullOrEmpty(row["CarId"].ToString()))
            {
                var hasDate = DateTime.TryParse(row["LastDetailingDate"].ToString(), out var lastTimeDetailed);
                var car = new Car
                {
                    Id = int.Parse(row["CarId"].ToString()),
                    Manufacturer = row["Manufacturer"].ToString(),
                    Model = row["Model"].ToString(),
                    Year = row["Year"].ToString(),
                    Color = row["Color"].ToString(),
                    OwnerId = customer.Id,
                };

                car.LastDetailed = hasDate ? lastTimeDetailed : null;
                (customer.Cars as List<Car>).Add(car);
            }
            return customer;
        }
    }
}