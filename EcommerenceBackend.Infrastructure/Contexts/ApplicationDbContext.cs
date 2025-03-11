// EcommerenceBackend.Infrastructure/ApplicationDbContext.cs
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Payments;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Reviews;
using EcommerenceBackend.Application.Domain.Shops;
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
    public DbSet<Shop> Shops { get; set; }
    public DbSet<ShopOwner> ShopOwners { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
