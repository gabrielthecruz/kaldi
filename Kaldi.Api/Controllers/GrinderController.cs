using Microsoft.AspNetCore.Mvc;
using Kaldi.Api.Models;
using Kaldi.Api.Services;

namespace Kaldi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GrinderController : ControllerBase
{
    private readonly KaldiContext _context;
    private int MaxId;

    public GrinderController(KaldiContext context) => _context = context;

    [HttpPost()]
    public ActionResult<int> PostGrinder([FromBody] Grinder grinder)
    {
        if (grinder is null)
            return BadRequest("No grinder data received.");

        _context.Grinders.Add(grinder);
        _context.SaveChanges();

        return grinder.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<Grinder> GetGrinderById(int id)
    {
        var grinder = (from g in _context.Grinders
                      where g.Id == id
                      select g).FirstOrDefault();

        if (grinder is null)
            return NotFound($"Grinder with ID='{id}' not found.");

        return grinder;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<Grinder>> GetAll() => _context.Grinders;

    [HttpDelete()]
    public IActionResult DeleteGrinder([FromBody] int id)
    {
        var grinder = (from g in _context.Grinders
                      where g.Id == id
                      select g).FirstOrDefault();

        if (grinder is null)
            return NotFound($"Grinder with ID='{id}' not found.");

        _context.Grinders?.Remove(grinder);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public IActionResult PutGrinder([FromBody] Grinder grinder)
    {
        if (grinder is null)
            return BadRequest("No grinder data received.");

        var dbGrinder = (from g in _context.Grinders
                        where g.Id == grinder.Id
                        select g).FirstOrDefault();

        if (dbGrinder is null)
            return NotFound($"Grinder with ID='{grinder.Id}' not found.");

        dbGrinder.Name = grinder.Name;
        dbGrinder.Description = grinder.Description;

        _context.SaveChanges();

        return Ok();
    }
}
