using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.Application.OrderAg.Order.GetOrder;

public record GetOrderByIdResponse(GetOrderResponse Order, IReadOnlyCollection<GetOrderItemsResponse> Items);

public record GetOrderResponse(
    Guid Id,
    string OrderNumber,
    string? ExternalId,
    string ManagerName,
    string ManagerPhoneNumber,
    OrderStatus Status,
    int Version,
    string DeliveryAddress,
    GetDeliveryScheduleResponse DeliverySchedule,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static GetOrderResponse GetOrderEntityData(Domain.Orders.Order order)
    {
        return new GetOrderResponse(
            order.Id.Value,
            order.OrderNumber,
            order.ExternalId,
            order.ManagerName,
            order.ManagerPhoneNumber,
            order.Status,
            order.Version,
            order.DeliveryAddress,
            new GetDeliveryScheduleResponse(
                order.DeliverySchedule.StartTime, 
                order.DeliverySchedule.EndTime, 
                order.DeliverySchedule.Shift),
            order.CreatedAt,
            order.UpdatedAt);
    }
}

public record GetDeliveryScheduleResponse(DateTime StartTime, DateTime EndTime, DeliveryShift Shift);

public record GetOrderItemsResponse(
    Guid Id,
    string? ExternalId,
    string StorageLocationCode,
    string ProductName,
    bool RequiresManufactureDate,
    string? Comment,
    decimal RequiredQuantity,
    Unit Unit)
{
    public static GetOrderItemsResponse GetOrderItemsEntityData(OrderItem orderItem)
    {
        return new GetOrderItemsResponse(
            orderItem.Id.Value,
            orderItem.ExternalId,
            orderItem.StorageLocationCode,
            orderItem.ProductName,
            orderItem.RequiresManufactureDate,
            orderItem.Comment,
            orderItem.RequiredQuantity,
            orderItem.Unit);
    }
}
