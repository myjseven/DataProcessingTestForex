using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class OrderControllerTests
{
    private readonly Mock<IOrderProcessingService> _mockOrderProcessingService;
    private readonly OrderController _orderController;

    public OrderControllerTests()
    {
        _mockOrderProcessingService = new Mock<IOrderProcessingService>();
        _orderController = new OrderController(_mockOrderProcessingService.Object);
    }

    [Fact]
    public async Task PostOrder_Should_Return_Ok_When_Order_Is_Valid()
    {
        // Arrange
        var validOrder = new BuySellOrder
        {
            CurrencyPair = "BTC/USD",
            BuyPrice = 45000.50m,
            SellPrice = 46000.75m,
            Timestamp = DateTime.UtcNow
        };

        _mockOrderProcessingService
            .Setup(service => service.ProcessOrderAsync(validOrder))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _orderController.PostOrder(validOrder);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be("Order processed successfully.");
    }

    [Fact]
    public async Task PostOrder_Should_Return_BadRequest_When_Order_Is_Null()
    {
        // Act
        var result = await _orderController.PostOrder(null);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().Be("Invalid order data.");
    }

    [Fact]
    public async Task PostOrder_Should_Return_BadRequest_When_CurrencyPair_Is_Empty()
    {
        // Arrange
        var invalidOrder = new BuySellOrder
        {
            CurrencyPair = "",
            BuyPrice = 45000.50m,
            SellPrice = 46000.75m,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var result = await _orderController.PostOrder(invalidOrder);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().Be("Invalid order data.");
    }
}
