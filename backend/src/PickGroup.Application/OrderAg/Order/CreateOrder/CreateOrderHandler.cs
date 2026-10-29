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
        CancellationToken cancellationToken = default)
    {
        var orderId = OrderId.NewOrderId();

        var deliverySchedule = DeliverySchedule.Create(
            request.Order.DeliverySchedule.StartTime,
            request.Order.DeliverySchedule.EndTime);

        var order = Domain.Orders.Order.Create(
            orderId,
            request.Order.OrderNumber,
            request.Order.ExternalId,
            request.Order.ManagerName,
            request.Order.ManagerPhoneNumber,
            request.Order.DeliveryAddress,
            deliverySchedule.Value);

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
            
            orderItems.Add(orderItem.Value);
        }
        
        order.Value.AddItems(orderItems);
        
        return order;
    }
}