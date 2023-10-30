using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/customers
    [HttpGet]
    public IActionResult GetCustomers()
    {
        var customers = _context.Customers
        .Select(c => new CustomersDTO
        {
            CustomerId = c.CustomerId,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Phone = c.Phone
        })
        .ToList();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = _context.Customers
            .Include(c => c.Transactions)
            .ThenInclude(t => t.Food)
            .FirstOrDefault(c => c.CustomerId == id);

        if (customer == null)
        {
            return NotFound();
        }

        decimal totalAmount = customer.Transactions?.Sum(t => (t.Food?.Price ?? 0) * t.Quantity) ?? 0;

        var customerDTO = new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            TotalAmount = totalAmount // Calculate and include the total amount
        };

        return Ok(customerDTO);
    }


    // POST: api/customers
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] Customer customer)
    {
        if (customer == null)
        {
            return BadRequest();
        }

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
    }

    // PUT: api/customers/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
    {
        var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);

        if (existingCustomer == null)
        {
            return NotFound(); // Return a 404 Not Found response if the customer doesn't exist
        }



        try
        {
            // Update the properties of 'existingCustomer' based on the data in the 'customer' parameter
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            // Update other customer properties as needed

            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE: api/customers/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return NoContent();
    }
}