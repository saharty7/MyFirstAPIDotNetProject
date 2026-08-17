using Microsoft.EntityFrameworkCore;
using MyApi.models;
using MyApi.Models;

namespace MyApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car>? Cars { get; set; }
    public DbSet<Staff> Staff { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>(entity =>
        {
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });
    }
}
