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
    [InlineData(null, "Иван", "+79991234567", "Москва", "orderNumber")]
    [InlineData("ORD-001", null, "+79991234567", "Москва", "managerName")]
    [InlineData("ORD-001", "Иван", null, "Москва", "managerPhoneNumber")]
    [InlineData("ORD-001", "Иван", "+79991234567", null, "deliveryAddress")]
    [InlineData("", "Иван", "+79991234567", "Москва", "orderNumber")]
    [InlineData("ORD-001", "", "+79991234567", "Москва", "managerName")]
    [InlineData("ORD-001", "Иван", "", "Москва", "managerPhoneNumber")]
    [InlineData("ORD-001", "Иван", "+79991234567", "", "deliveryAddress")]
    [InlineData(" ", "Иван", "+79991234567", "Москва", "orderNumber")]
    [InlineData("ORD-001", " ", "+79991234567", "Москва", "managerName")]
    [InlineData("ORD-001", "Иван", " ", "Москва", "managerPhoneNumber")]
    [InlineData("ORD-001", "Иван", "+79991234567", " ", "deliveryAddress")]
    public void Create_WhenParamIsNullOrWhiteSpace_ShouldReturnFailure(string? orderNumber, string? managerName,
        string? managerPhoneNumber, string? deliveryAddress, string paramName)
    {
        //arrange
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var orderId = OrderId.NewOrderId();
        
        //act
        var result = Order.Create(
            orderId: orderId,
            orderNumber: orderNumber!,
            externalId: null,
            managerName: managerName!,
            managerPhoneNumber: managerPhoneNumber!,
            deliveryAddress: deliveryAddress!,
            deliverySchedule: deliverySchedule.Value);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty(paramName), result.Error);
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

    [Fact]
    public void Create_WhenArgumentsAreValid_CreatesPendingOrder()
    {
        //arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        
        //act
        var result = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        
        
        //Assert
        Assert.True(result.IsSuccess);
        var order = result.Value;
        
        Assert.Equal(orderId, order.Id);
        Assert.Equal("ORD-001", order.OrderNumber);
        Assert.Equal("Иван", order.ManagerName);
        Assert.Equal("+79991234567", order.ManagerPhoneNumber);
        Assert.Equal("Москва, ул. Ленина, 10", order.DeliveryAddress);
        Assert.Equal(deliverySchedule.Value, order.DeliverySchedule);
        Assert.Null(order.ExternalId);
        
        Assert.Equal(1, order.Version);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Empty(order.Items);
        Assert.Null(order.UpdatedAt);
    }
    
    [Fact]
    public void AddItem_WhenItemIsValid_OrderItemAdded()
    {
        // arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var orderItemId = OrderItemId.NewOrderItemId();
        var resultOrderItem = OrderItem.Create(
            orderItemId: orderItemId,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var order = resultOrder.Value;
        var orderItem = resultOrderItem.Value;
        
        //act
        
        var result = order.AddItem(orderItem);
        
        //assert
        Assert.True(result.IsSuccess);
        Assert.Equal(orderItem, Assert.Single(order.Items));
    }

    [Fact]
    public void AddItem_WhenItemIsNull_ShouldReturnFailure()
    {
        //arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var orderItemId = OrderItemId.NewOrderItemId();
        var resultOrderItem = OrderItem.Create(
            orderItemId: orderItemId,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var order = resultOrder.Value;
        var existedItem = resultOrderItem.Value;
        var setupResult = order.AddItem(existedItem);
        Assert.True(setupResult.IsSuccess);
        
        
        //act
        var result = order.AddItem(null!);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("item"), result.Error);
        Assert.Equal(existedItem, Assert.Single(order.Items));
    }

    [Fact]
    public void AddItems_WhenItemsIsValid_OrderItemsAdded()
    {
        //arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var orderItemIdOne = OrderItemId.NewOrderItemId();
        var resultOrderItemOne = OrderItem.Create(
            orderItemId: orderItemIdOne,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var orderItemIdTwo = OrderItemId.NewOrderItemId();
        var resultOrderItemTwo = OrderItem.Create(
            orderItemId: orderItemIdTwo,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var order = resultOrder.Value;
        var existedItemOne = resultOrderItemOne.Value;
        var existedItemTwo = resultOrderItemTwo.Value;
        List<OrderItem> orderItems = new List<OrderItem>() { existedItemOne, existedItemTwo };
        
        //act
        var result = order.AddItems(orderItems);
        
        //assert
        Assert.True(result.IsSuccess);
        Assert.Equal(orderItems, order.Items);
    }

    [Fact]
    public void AddItems_WhenItemsIsNull_ShouldReturnFailure()
    {
        // arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var order = resultOrder.Value;
        
        // act
        var result = order.AddItems(null!);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("items"), result.Error);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void AddItems_WhenItemsIsEmpty_ShouldReturnFailure()
    {
        //arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var order = resultOrder.Value;

        List<OrderItem> orderItems = new List<OrderItem>() { };
        Assert.Empty(orderItems);
        
        // act
        var result = order.AddItems(orderItems);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("items"), result.Error);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void AddItems_WhenAnyItemsIsNull_ShouldReturnFailure()
    {
        //arrange
        var orderId = OrderId.NewOrderId();
        var deliverySchedule = DeliverySchedule.Create("2026-11-18 14:00:00", "2026-11-18 15:00:00");
        var resultOrder = Order.Create(
            orderId: orderId,
            orderNumber: "ORD-001",
            externalId: null,
            managerName: "Иван",
            managerPhoneNumber: "+79991234567",
            deliveryAddress: "Москва, ул. Ленина, 10",
            deliverySchedule: deliverySchedule.Value);
        
        var orderItemIdOne = OrderItemId.NewOrderItemId();
        var resultOrderItemOne = OrderItem.Create(
            orderItemId: orderItemIdOne,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var orderItemIdTwo = OrderItemId.NewOrderItemId();
        var resultOrderItemTwo = OrderItem.Create(
            orderItemId: orderItemIdTwo,
            externalId: null,
            storageLocationCode: "B-02-01",
            productName: "Сахар 1 кг",
            requiresManufactureDate: false,
            comment: null,
            requiredQuantity: 10,
            unit: Unit.Kilograms);
        
        var order = resultOrder.Value;
        var existedItemOne = resultOrderItemOne.Value;
        var existedItemTwo = resultOrderItemTwo.Value;
        
        List<OrderItem> orderItems = new List<OrderItem>() { existedItemOne, null!, existedItemTwo };
        
        //act
        var result = order.AddItems(orderItems);
        
        //assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty("items"), result.Error);
        Assert.Empty(order.Items);
    }
}