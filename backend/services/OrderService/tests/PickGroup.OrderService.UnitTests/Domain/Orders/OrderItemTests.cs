using PickGroup.OrderService.Domain.Common;
using PickGroup.OrderService.Domain.Orders;
using PickGroup.OrderService.Domain.Orders.OrdersIds;

namespace PickGroup.OrderService.UnitTests.Domain.Orders;

public class OrderItemTests
{
    [Fact]
    public void Create_WhenOrderItemIdIsNull_ShouldReturnFailure()
    {
        var resultOrderItem = OrderItem.Create(
            orderItemId: null!,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        //assert
        Assert.True(resultOrderItem.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("orderItemId"), resultOrderItem.Error);
    }

    [Theory]
    [InlineData(null, "Сахар 1 кг", "storageLocationCode")]
    [InlineData("B-02-01", null, "productName")]
    [InlineData("", "Сахар 1 кг", "storageLocationCode")]
    [InlineData("B-02-01", "", "productName")]
    [InlineData(" ", "Сахар 1 кг", "storageLocationCode")]
    [InlineData("B-02-01", " ", "productName")]
    public void Create_WhenParamIsNullOrWhiteSpace_ShouldReturnFailure(string? storageLocationCode, string? productName, string paramName)
    {
        //arrange
        var orderItemId = OrderItemId.NewOrderItemId();
        
        //act
        var resultOrderItem = OrderItem.Create(
            orderItemId: orderItemId,
            externalId: null,
            storageLocationCode: storageLocationCode!,
            productName: productName!,
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        //assert
        Assert.True(resultOrderItem.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty(paramName), resultOrderItem.Error);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WhenRequiredQuantityIsZeroOrNegative_ShouldReturnFailure(int requiredQuantity)
    {
        //arrange
        var orderItemId = OrderItemId.NewOrderItemId();
        
        //act
        var resultOrderItem = OrderItem.Create(
            orderItemId: orderItemId,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: requiredQuantity,
            unit: Unit.Kilograms);
        
        //assert
        Assert.True(resultOrderItem.IsFailure);
        Assert.Equal(Errors.General.ValueIsInvalid("requiredQuantity"), resultOrderItem.Error);
    }

    [Fact]
    public void Create_WhenArgumentsAreValid_CreateOrderItem()
    {
        //arrange
        var orderItemId = OrderItemId.NewOrderItemId();
        
        //act
        var resultOrderItem = OrderItem.Create(
            orderItemId: orderItemId,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 0.25m,
            unit: Unit.Kilograms);
        
        
        //assert
        Assert.True(resultOrderItem.IsSuccess);
        
        var orderItem = resultOrderItem.Value;
        
        Assert.Equal(orderItemId, orderItem.Id);
        Assert.Null(orderItem.ExternalId);
        Assert.Equal("B-02-01", orderItem.StorageLocationCode);
        Assert.Equal("Сахар 1 кг", orderItem.ProductName);
        Assert.False(orderItem.RequiresManufactureDate);
        Assert.Null(orderItem.Comment);
        Assert.Equal(0.25m, orderItem.RequiredQuantity);
        Assert.Equal(Unit.Kilograms, orderItem.Unit);
    }
}