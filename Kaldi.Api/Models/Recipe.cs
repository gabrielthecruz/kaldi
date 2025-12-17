using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaldi.Api.Models;

public class Recipe
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Method))]
    public int MethodId { get; set; }
    public required string Name { get; set; }
    public string? Author { get; set; }
    public string? Url { get; set; }
    public required float CoffeeWeight { get; set; }
    public required float WaterWeight { get; set; }
    public required int GrindLevel { get; set; }
    public string? Description { get; set; }

    public Method Method { get; set; }
}
