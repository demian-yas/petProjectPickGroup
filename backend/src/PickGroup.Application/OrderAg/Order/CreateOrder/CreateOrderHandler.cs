using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.Application.OrderAg.Order.CreateOrder;

public class CreateOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<Domain.Orders.Order, Error>> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken ct = default)
    {
        var orderId = OrderId.NewOrderId();

        var deliverySchedule = DeliverySchedule.Create(
            request.Order.DeliverySchedule.StartTime,
            request.Order.DeliverySchedule.EndTime);
        
        if (deliverySchedule.IsFailure)
            return deliverySchedule.Error;

        var order = Domain.Orders.Order.Create(
            orderId,
            request.Order.OrderNumber,
            request.Order.ExternalId,
            request.Order.ManagerName,
            request.Order.ManagerPhoneNumber,
            request.Order.DeliveryAddress,
            deliverySchedule.Value);
        
        if (order.IsFailure)
            return order.Error;

        List<OrderItem> orderItems = [];
        
        foreach (var requestItem in request.Items)
        {
            var orderItemId = OrderItemId.NewOrderItemId();
            
            var orderItem = OrderItem.Create(
                orderItemId,
                requestItem.ExternalId,
                requestItem.StorageLocationCode,
                requestItem.ProductName,
                requestItem.RequiresManufactureDate,
                requestItem.Comment,
                requestItem.RequiredQuantity,
                requestItem.Unit);
            
            if (orderItem.IsFailure)
                return orderItem.Error;
            
            orderItems.Add(orderItem.Value);
        }
        
        var orderItemsResult = order.Value.AddItems(orderItems);
        
        if (orderItemsResult.IsFailure)
            return orderItemsResult.Error;
        
        await _orderRepository.AddOrderAsync(order.Value, ct);
        
        return order;
    }
}