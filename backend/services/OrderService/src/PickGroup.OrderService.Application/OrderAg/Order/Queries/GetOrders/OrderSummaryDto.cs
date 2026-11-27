using PickGroup.OrderService.Domain.Orders;
using PickGroup.OrderService.Domain.Orders.OrderValueObjects;

namespace PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;

public record OrderSummaryDto(
    Guid Id,
    string OrderNumber,
    int Version,
    OrderStatus Status,
    string DeliveryAddress,
    DeliverySchedule DeliverySchedule,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int ItemsCount);