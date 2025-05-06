using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products;
using System.ComponentModel.DataAnnotations.Schema;

[Table("orderitems")]
public class OrderItem
{
    public OrderItemId Id { get; private set; }
    public Guid? OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public Money Price { get; private set; }
    public Money Quantity { get; private set; }

    // Parameterless constructor for EF Core
    private OrderItem() { }

    // Constructor with parameters for business logic
    public OrderItem(OrderItemId id, Guid? orderId, ProductId productId, Money price, Money quantity)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }
    public Product Products { get; set; }
}
