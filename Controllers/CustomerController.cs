using Detailing.Models;
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

    [HttpGet("{customerId:int}")]
    public ActionResult<Detailer> Get(int customerId)
    {
        var customer = customers.FirstOrDefault(c => c.Id == customerId);
        if (customer == default)
        {
            return NotFound();
        }

        return Ok(customer);
    }


    [HttpPost]
    public ActionResult<Customer> Post(Customer newCustomer)
    {
        try
        {
            customers.ToList().Add(newCustomer);
            return Created(string.Empty, newCustomer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

