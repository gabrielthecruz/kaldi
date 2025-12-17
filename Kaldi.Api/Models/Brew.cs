using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaldi.Api.Models;

public class Brew
{
    [Key]
    public Guid Id { get; set; }
    public DateTime BrewDateTime { get; set; }
    [ForeignKey(nameof(Coffee))]
    public Guid CoffeeId { get; set; }
    [ForeignKey(nameof(Recipe))]
    public int RecipeId { get; set; }
    [ForeignKey(nameof(Grinder))]
    public int GrinderId { get; set; }
    public required float WaterWeight { get; set; }
    public required float CoffeeWeight { get; set; }
    public required int GrindLevel { get; set; }
    public float? Score { get; set; }
    public string? BrewDescription { get; set; }
    public string? ResultDescription { get; set; }

    public Coffee Coffee { get; set; }
    public Recipe Recipe { get; set; }
    public Grinder Grinder { get; set; }
}
