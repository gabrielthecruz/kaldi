using Microsoft.AspNetCore.Mvc;
using Kaldi.Api.Models;
using Kaldi.Api.Services;

namespace Kaldi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewController : ControllerBase
{
    private readonly KaldiContext _context;

    public BrewController(KaldiContext context) => _context = context;

    [HttpPost()]
    public ActionResult<Guid> PostBrew([FromBody] Brew brew)
    {
        if (brew is null)
            return BadRequest("No brew data received.");

        brew.Id = Guid.NewGuid();
        _context.Brews.Add(brew);
        _context.SaveChanges();

        return brew.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<Brew> GetBrewById(Guid id)
    {
        var brew = (from c in _context.Brews
                    where c.Id == id
                    select c).FirstOrDefault();

        if (brew is null)
            return NotFound($"Brew with ID='{id}' not found.");

        return brew;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<Brew>> GetAll() => _context.Brews;

    [HttpDelete()]
    public IActionResult DeleteBrew([FromBody] Guid id)
    {
        var brew = (from b in _context.Brews
                    where b.Id == id
                    select b).FirstOrDefault();

        if (brew is null)
            return NotFound($"Brew with ID='{id}' not found.");

        _context.Brews?.Remove(brew);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public IActionResult PutBrew([FromBody] Brew brew)
    {
        if (brew is null)
            return BadRequest("No brew data received.");

        var dbBrew = (from b in _context.Brews
                      where b.Id == brew.Id
                      select b).FirstOrDefault();

        if (dbBrew is null)
            return NotFound($"Brew with ID='{brew.Id}' not found.");

        dbBrew.BrewDateTime = brew.BrewDateTime;
        dbBrew.CoffeeId = brew.CoffeeId;
        dbBrew.RecipeId = brew.RecipeId;
        dbBrew.GrinderId = brew.GrinderId;
        dbBrew.WaterWeight = brew.WaterWeight;
        dbBrew.CoffeeWeight = brew.CoffeeWeight;
        dbBrew.GrindLevel = brew.GrindLevel;
        dbBrew.Score = brew.Score;
        dbBrew.BrewDescription = brew.BrewDescription;
        dbBrew.ResultDescription = brew.ResultDescription;

        _context.SaveChanges();

        return Ok();
    }
}
