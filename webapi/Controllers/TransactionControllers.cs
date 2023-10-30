using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/transactions")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/transactions
    [HttpGet]
    public IActionResult GetTransactions()
    {


        var transactions = _context.Transactions
            .Include(t => t.Customer)
            .Include(t => t.Food)
            .Select(t => new TransactionDTO
            {
                TransactionId = t.TransactionId,
                CustomerId = t.CustomerId,
                FoodId = t.FoodId,
                TransactionDate = t.TransactionDate.ToString(),
                Quantity = t.Quantity,
                CustomerName = t.Customer != null ? t.Customer.FirstName : "N/A",
                FoodName = t.Food.Name
            })
            .ToList();
        return Ok(transactions);
    }

    // GET: api/transactions/{id}
    [HttpGet("{id}")]
    public IActionResult GetTransaction(int id)
    {
        var transaction = _context.Transactions
            .Include(t => t.Customer)
            .Include(t => t.Food)
            .FirstOrDefault(t => t.TransactionId == id);

        if (transaction == null)
        {
            return NotFound(); // Return a 404 Not Found response if the transaction doesn't exist
        }

        // Calculate the amount based on the price of the associated food
        decimal amount = 0;
        if (transaction.Food != null)
        {
            amount = transaction.Food.Price * transaction.Quantity;
        }

        // Map the transaction data to a DTO that includes the calculated amount
        var transactionDTO = new TransactionDTO
        {
            TransactionId = transaction.TransactionId,
            CustomerId = transaction.CustomerId,
            FoodId = transaction.FoodId,
            TransactionDate = transaction.TransactionDate.ToString(),
            Quantity = transaction.Quantity,
            CustomerName = transaction.Customer.FirstName,
            FoodName = transaction.Food.Name,
            Amount = amount // Include the calculated amount in the DTO
        };

        return Ok(transactionDTO);
    }

    // POST: api/transactions
    [HttpPost]
    public IActionResult CreateTransaction([FromBody] Transaction transaction)
    {
        if (transaction == null)
        {
            return BadRequest();
        }

        _context.Transactions.Add(transaction);
        _context.SaveChanges();

        return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
    }

    // PUT: api/transactions/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
    {
        var existingTransaction = _context.Transactions.FirstOrDefault(t => t.TransactionId == id);

        if (existingTransaction == null)
        {
            return NotFound(); // Return a 404 Not Found response if the transaction doesn't exist
        }

        //  if (id != transaction.TransactionId)
        //  {
        //      return BadRequest("Transaction ID in the request body does not match the ID in the URL.");
        //  }

        try
        {
            // Update the properties of 'existingTransaction' based on the data in the 'transaction' parameter
            existingTransaction.CustomerId = transaction.CustomerId;
            existingTransaction.FoodId = transaction.FoodId;
            existingTransaction.TransactionDate = transaction.TransactionDate;
            existingTransaction.Quantity = transaction.Quantity;

            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE: api/transactions/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        var transaction = _context.Transactions.Find(id);
        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        _context.SaveChanges();

        return NoContent();
    }
}