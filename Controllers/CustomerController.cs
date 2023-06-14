using Detailing.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private static IEnumerable<Customer> customers = new List<Customer>{
        new Customer{
            FirstName = "Yas",
            LastName = "dle",
        },
        new Customer{
            FirstName = "Hiba",
            LastName = "sh"
        }
    };

    [HttpGet]
    public IEnumerable<Customer> Get()
    {
        return customers;
    }

    [HttpPost]
    public static Customer Post(Customer newCustomer)
    {
        customers.ToList().Add(newCustomer);
        return newCustomer;
    }
}

