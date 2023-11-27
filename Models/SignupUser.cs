using Detailing.Interfaces;

namespace Detailing.Models;

public class SignupUser : User, IPassword
{
    public string Password { get; set; }

    public Customer ToCustomer()
    {
        return new Customer{
            Address = Address,
            Cars = new List<Car>(),
            CreatedAt = CreatedAt,
            DOB = DOB,
            Email = Email,
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            PreferredName = PreferredName,
            Id = Id,
            MobileNumber = MobileNumber,
        };
    }

     public Detailer ToDetailer()
    {
        return new Detailer{
            Address = Address,
            Services = new List<DetailerService>(),
            CreatedAt = CreatedAt,
            DOB = DOB,
            Email = Email,
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            PreferredName = PreferredName,
            Id = Id,
            MobileNumber = MobileNumber,
        };
    }
}