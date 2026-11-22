using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.Application.OrderAg.Order.Queries.GetOrders;

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