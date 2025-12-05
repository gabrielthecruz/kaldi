using Microsoft.AspNetCore.Mvc;
using Kaldi.Api.Models;
using Kaldi.Api.Services;

namespace Kaldi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly KaldiContext _context;

    public CoffeeController(KaldiContext context) => _context = context;

    [HttpPost()]
    public ActionResult<Guid> PostCoffee([FromBody] Coffee coffee)
    {
        if (coffee is null)
            return BadRequest("No coffee data received.");

        coffee.Id = Guid.NewGuid();
        _context.Coffees.Add(coffee);
        _context.SaveChanges();

        return coffee.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<Coffee> GetCoffeeById(Guid id)
    {
        var coffee = _context.Coffees.First();

        if (coffee is null)
            return NotFound($"Coffee with ID='{id} not found.");

        return coffee;
    }

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Coffee>> GetAll() => _context.Coffees;

    [HttpDelete()]
    public IActionResult DeleteCoffee([FromBody] Guid id)
    {
        var coffee = (from c in _context.Coffees
                      where c.Id == id
                      select c)?.First();

        if (coffee is null)
            return NotFound($"Coffee with ID='{id} not found.");

        _context.Coffees?.Remove(coffee);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public IActionResult PutCoffee([FromBody] Coffee coffee)
    {
        if (coffee is null)
            return BadRequest("No coffee data received.");

        var dbCoffee = (from c in _context.Coffees
                        where c.Id == coffee.Id
                        select c)?.First();

        if (dbCoffee is null)
            return NotFound($"Coffee with ID='{coffee.Id}' not found.");

        dbCoffee.Name = coffee.Name;
        dbCoffee.ArrivalDate = coffee.ArrivalDate;
        dbCoffee.RoastDate = coffee.RoastDate;
        dbCoffee.SensoryProfile = coffee.SensoryProfile;
        dbCoffee.Variety = coffee.Variety;
        dbCoffee.Region = coffee.Region;
        dbCoffee.ProcessingMethod = coffee.ProcessingMethod;
        dbCoffee.Producer = coffee.Producer;
        dbCoffee.Altitude = coffee.Altitude;
        dbCoffee.Vendor = coffee.Vendor;
        dbCoffee.Roast = coffee.Roast;
        dbCoffee.Weight = coffee.Weight;
        dbCoffee.Description = coffee.Description;

        _context.SaveChanges();

        return Ok();
    }
}
