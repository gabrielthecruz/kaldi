using System.ComponentModel.DataAnnotations;

namespace Kaldi.Api.Models;

public class Coffee
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public DateTime? RoastDate { get; set; }
    public string? SensoryProfile { get; set; }
    public string? Variety { get; set; }
    public string? Region { get; set; }
    public string? ProcessingMethod { get; set; }
    public string? Producer { get; set; }
    public int? Altitude { get; set; }
    public string? Vendor { get; set; }
    public string? Roast { get; set; }
    public float? Weight { get; set; }
    public string? Description { get; set; }
}
