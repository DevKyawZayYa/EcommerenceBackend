// EcommerenceBackend.Infrastructure/ApplicationDbContext.cs
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
