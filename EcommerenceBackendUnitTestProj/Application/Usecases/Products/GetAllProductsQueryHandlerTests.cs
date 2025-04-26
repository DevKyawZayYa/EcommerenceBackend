using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using EcommerenceBackend.Application.UseCases.Products.Queries.GetAllProducts;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Images.ProductsImages;
using EcommerenceBackend.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;

public class GetAllProductsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedResult_WhenProductsExist()
    {
        // Arrange
        var fakeProducts = new List<Product>
        {
            CreateFakeProduct("Test Product 1", 99.99m, "SKU-001")
        };

        var fakeMapped = new List<ProductListDto>
        {
            new ProductListDto { Name = "Test Product 1", Price = (Money) 99.99m }
        };

        var query = new GetAllProductsQuery { Page = 1, PageSize = 10 };

        var dbContext = CreateInMemoryDbContext(fakeProducts);
        var mapperMock = new Mock<IMapper>();
        var cacheMock = new Mock<IRedisService>();

        // Setup mocks
        cacheMock.Setup(x => x.GetAsync<PagedResult<ProductListDto>>(It.IsAny<string>()))
                 .ReturnsAsync((PagedResult<ProductListDto>)null); // force cache miss

        mapperMock.Setup(m => m.Map<List<ProductListDto>>(It.IsAny<List<Product>>()))
                  .Returns(fakeMapped);

        var handler = new GetAllProductsQueryHandler(dbContext, mapperMock.Object, cacheMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
        result.Items.First().Name.Should().Be("Test Product 1");
    }

    private static OrderDbContext CreateInMemoryDbContext(List<Product> products)
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
            .Options;

        var context = new OrderDbContext(options);
        context.Products.AddRange(products);
        context.SaveChanges();
        return context;
    }

    private static Product CreateFakeProduct(string name, decimal price, string sku)
    {
        var product = (Product)System.Runtime.Serialization.FormatterServices
            .GetUninitializedObject(typeof(Product));

        typeof(Product).GetProperty("Id")?.SetValue(product, new ProductId(Guid.NewGuid()));
        typeof(Product).GetProperty("Name")?.SetValue(product, name);
        typeof(Product).GetProperty("Description")?.SetValue(product, "Sample");
        typeof(Product).GetProperty("Price")?.SetValue(product, new Money(price));
        typeof(Product).GetProperty("Sku")?.SetValue(product, new Sku(sku));
        typeof(Product).GetProperty("ProductImages")?.SetValue(product, new List<ProductImages>());

        return product;
    }


}
