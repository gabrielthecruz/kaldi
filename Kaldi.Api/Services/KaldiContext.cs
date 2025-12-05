using Microsoft.EntityFrameworkCore;
using Kaldi.Api.Models;

namespace Kaldi.Api.Services;

public class KaldiContext : DbContext
{
    public DbSet<Coffee> Coffees { get; set; }
    public DbSet<Method> Methods { get; set; }

    public KaldiContext(DbContextOptions<KaldiContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coffee>().ToTable("Coffee");
	modelBuilder.Entity<Method>().ToTable("Method");
    }
}
