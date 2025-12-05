﻿using System.ComponentModel.DataAnnotations;

namespace Kaldi.Api.Models;

public class Grinder
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
