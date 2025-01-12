using EcommerenceBackend.Application.Domain.Entities;
using EcommerenceBackend.Application.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasConversion(
                    v => v.Value, // Convert to string for storage
                    v => new FirstName(v)); // Convert back to FirstName object

            entity.Property(e => e.LastName)
                .HasConversion(
                    v => v.Value, // Convert to string for storage
                    v => new LastName(v)); // Convert back to LastName object
        });

        base.OnModelCreating(modelBuilder);
    }
}
