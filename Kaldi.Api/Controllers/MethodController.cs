using Microsoft.AspNetCore.Mvc;
using Kaldi.Api.Models;
using Kaldi.Api.Services;

namespace Kaldi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MethodController : ControllerBase
{
    private readonly KaldiContext _context;
    private int MaxId;

    public MethodController(KaldiContext context) => _context = context;

    [HttpPost()]
    public ActionResult<int> PostMethod([FromBody] Method method)
    {
        if (method is null)
            return BadRequest("No method data received.");

        _context.Methods.Add(method);
        _context.SaveChanges();

        return method.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<Method> GetMethodById(int id)
    {
        var method = (from m in _context.Methods
                      where m.Id == id
                      select m).Single();

        if (method is null)
            return NotFound($"Method with ID='{id} not found.");

        return method;
    }

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Method>> GetAll() => _context.Methods;

    [HttpDelete()]
    public IActionResult DeleteMethod([FromBody] int id)
    {
        var method = (from m in _context.Methods
                      where m.Id == id
                      select m)?.First();

        if (method is null)
            return NotFound($"Method with ID='{id} not found.");

        _context.Methods?.Remove(method);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public IActionResult PutMethod([FromBody] Method method)
    {
        if (method is null)
            return BadRequest("No method data received.");

        var dbMethod = (from m in _context.Methods
                        where m.Id == method.Id
                        select m)?.First();

        if (dbMethod is null)
            return NotFound($"Method with ID='{method.Id}' not found.");

        dbMethod.Name = method.Name;
        dbMethod.Description = method.Description;

        _context.SaveChanges();

        return Ok();
    }
}
