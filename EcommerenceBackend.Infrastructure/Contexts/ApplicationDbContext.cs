// EcommerenceBackend.Infrastructure/ApplicationDbContext.cs
using EcommerenceBackend.Application.Domain.Categories;
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Payments;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Reviews;
using EcommerenceBackend.Application.Domain.Shipment;
using EcommerenceBackend.Application.Domain.Shops;
using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Infrastructure.Configurations;
using EcommerenceBackend.Infrastructure.Configurations.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShopOwner> ShopOwners { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
