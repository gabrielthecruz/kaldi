using Microsoft.AspNetCore.Mvc;
using Kaldi.Api.Models;
using Kaldi.Api.Services;

namespace Kaldi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly KaldiContext _context;
    private int MaxId;

    public RecipeController(KaldiContext context) => _context = context;

    [HttpPost()]
    public ActionResult<int> PostRecipe([FromBody] Recipe recipe)
    {
        if (recipe is null)
            return BadRequest("No recipe data received.");

        _context.Recipes.Add(recipe);
        _context.SaveChanges();

        return recipe.Id;
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetRecipeById(int id)
    {
        var recipe = (from r in _context.Recipes
                      where r.Id == id
                      select r).FirstOrDefault();

        if (recipe is null)
            return NotFound($"Recipe with ID='{id}' not found.");

        return recipe;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<Recipe>> GetAll() => _context.Recipes;

    [HttpDelete()]
    public IActionResult DeleteRecipe([FromBody] int id)
    {
        var recipe = (from r in _context.Recipes
                      where r.Id == id
                      select r).FirstOrDefault();

        if (recipe is null)
            return NotFound($"Recipe with ID='{id}' not found.");

        _context.Recipes?.Remove(recipe);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public IActionResult PutRecipe([FromBody] Recipe recipe)
    {
        if (recipe is null)
            return BadRequest("No recipe data received.");

        var dbRecipe = (from r in _context.Recipes
                        where r.Id == recipe.Id
                        select r).FirstOrDefault();

        if (dbRecipe is null)
            return NotFound($"Recipe with ID='{recipe.Id}' not found.");

        dbRecipe.MethodId = recipe.MethodId;
        dbRecipe.Name = recipe.Name;
        dbRecipe.Author = recipe.Author;
        dbRecipe.Url = recipe.Url;
        dbRecipe.CoffeeWeight = recipe.CoffeeWeight;
        dbRecipe.WaterWeight = recipe.WaterWeight;
        dbRecipe.GrindLevel = recipe.GrindLevel;
        dbRecipe.Description = recipe.Description;

        _context.SaveChanges();

        return Ok();
    }
}
