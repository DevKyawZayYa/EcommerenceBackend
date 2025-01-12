using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Entities;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

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
