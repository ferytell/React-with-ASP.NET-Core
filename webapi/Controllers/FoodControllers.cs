using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/food")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FoodController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/food
    [HttpGet]
    public IActionResult GetFoods()
    {
        var foods = _context.Foods
            .Select(f => new FoodDTO
            {
                FoodId = f.FoodId,
                Name = f.Name,
                Description = f.Description,
                Price = f.Price
            })
            .ToList();

        return Ok(foods);
    }

    // GET: api/food/{id}
    [HttpGet("{id}")]
    public IActionResult GetFood(int id)
    {
        var food = _context.Foods.Find(id);

        if (food == null)
        {
            return NotFound();
        }

        var foodDTO = new FoodDTO
        {
            FoodId = food.FoodId,
            Name = food.Name,
            Description = food.Description,
            Price = food.Price
        };

        return Ok(foodDTO);
    }


    // POST: api/food
    [HttpPost]
    public IActionResult CreateFood([FromBody] Food food)
    {
        if (food == null)
        {
            return BadRequest();
        }

        _context.Foods.Add(food);
        _context.SaveChanges();

        return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
    }

    // PUT: api/food/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFood(int id, [FromBody] Food food)
    {
        var existingFood = _context.Foods.FirstOrDefault(f => f.FoodId == id);

        if (existingFood == null)
        {
            return NotFound(); // Return a 404 Not Found response if the resource doesn't exist
        }

        try
        {
            // Perform the update on 'existingFood' properties
            existingFood.Name = food.Name;
            existingFood.Description = food.Description;
            existingFood.Price = food.Price;

            _context.SaveChanges();
            return Ok($"Edit food with ID: {id} successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE: api/food/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteFood(int id)
    {
        var food = _context.Foods.Find(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.Foods.Remove(food);
        _context.SaveChanges();

        return Ok($"Deleted food with ID: {id} successfully.");

    }
}