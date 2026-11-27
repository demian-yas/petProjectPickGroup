using Moq;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;
using PickGroup.OrderService.Application.OrderAg.Order.Repositories;

namespace PickGroup.OrderService.UnitTests.Application.Orders.Queries.GetOrders;

public class GetOrdersHandlerTests
{
    [Fact]
    public async Task GetOrders_WhenPageIsZero_ShouldReturnFailure()
    {
        var repositoryMock = new Mock<IOrderReadRepository>();
        var handler = new GetOrdersHandler(repositoryMock.Object);

        var result = await handler.GetOrdersAsync(0, 20);
        
        Assert.True(result.IsFailure);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task GetOrders_WhenPageIsInvalid_ShouldReturnFailure(int page)
    {
        var repositoryMock = new Mock<IOrderReadRepository>();
        var handler = new GetOrdersHandler(repositoryMock.Object);
        
        var result = await handler.GetOrdersAsync(page, 20);
        
        Assert.True(result.IsFailure);
        
        repositoryMock.Verify(
            x => x.GetOrdersAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()), Times.Never);
    }
}