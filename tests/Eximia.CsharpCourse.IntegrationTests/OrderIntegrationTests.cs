using Eximia.CsharpCourse.API.Models.Requests;
using Eximia.CsharpCourse.Products;
using Eximia.CsharpCourse.Products.Discounts;
using Eximia.CsharpCourse.SeedWork.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using static Eximia.CsharpCourse.API.Models.Requests.CreateOrderRequest;

namespace Eximia.CsharpCourse.IntegrationTests;

public class OrderIntegrationTests(CustomWebApplicationFactory customWebApplicationFactory) : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async void ShouldSucessfullyCreateOrder()
    {
        using var httpClient = customWebApplicationFactory.CreateClient();
        using var dbContext = customWebApplicationFactory
            .Services
            .GetRequiredService<IEFDbContextFactory<EximiaCsharpCourseDbContext>>()
            .Create();

        var product = new Product(0, Array.Empty<IDiscountStrategy>(), "Integration test product");
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();

        var itemsDto = new List<ItemDto>
            {
                new() { Amount = 10, Quantity = 200, ProductId = product.Id }
            };

        var createOrderRequest = new CreateOrderRequest
        {
            Items = itemsDto,
            PaymentMethod = new PaymentMethodDto { Method = SeedWork.EPaymentMethod.Pix }
        };

        var requestJson = JsonSerializer.Serialize(createOrderRequest);
        var body = new StringContent(requestJson, Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"api/orders") { Content = body };

        var response = await httpClient.SendAsync(httpRequest);
        var createdOrder = await dbContext
            .Orders
            .Include(od => od.Items)
            .FirstOrDefaultAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        createdOrder!.Amount.Should().Be(10);
        createdOrder.Items.Should().HaveCount(1);
    }
}