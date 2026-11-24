using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.UnitTests.Domain.Orders;

public class OrderTests
{
    [Fact]
    public void Create_WhenOrderIdIsNull_ShouldReturnFailure()
    {
        // arrange
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        
        // act
        var result = Order.Create(
            orderId: null!,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        // assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("orderId"), result.Error);
    }

    [Theory]
    [InlineData(null, "Иван", "+79991234567", "Москва")]
    [InlineData("ORD-001", null, "+79991234567", "Москва")]
    [InlineData("ORD-001", "Иван", null, "Москва")]
    [InlineData("ORD-001", "Иван", "+79991234567", null)]
    [InlineData("", "Иван", "+79991234567", "Москва")]
    [InlineData("ORD-001", "", "+79991234567", "Москва")]
    [InlineData("ORD-001", "Иван", "", "Москва")]
    [InlineData("ORD-001", "Иван", "+79991234567", "")]
    public void Create_WhenParamIsNull_ShouldReturnFailure(string orderNumber, string managerName,
        string managerPhoneNumber, string deliveryAddress)
    {
        //arrange
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var orderId = OrderId.NewOrderId();
        
        //act
        var result = Order.Create(
            orderId: orderId,
            orderNumber: orderNumber,
            externalId: null,
            managerName: managerName,
            managerPhoneNumber: managerPhoneNumber,
            deliveryAddress: deliveryAddress,
            deliverySchedule: deliverySchedule.Value);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal("value.is.empty", result.Error.Code);
    }

    [Fact]
    public void Create_WhenDeliveryScheduleIsNull_ShouldReturnFailure()
    {
        // arrange
        var orderId = OrderId.NewOrderId();
        
        // act
        var result = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: null!);
        
        // Assert
        
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("deliverySchedule"), result.Error);
    }
    
    
}