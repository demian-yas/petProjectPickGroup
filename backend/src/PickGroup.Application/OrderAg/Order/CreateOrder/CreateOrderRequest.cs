using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.Application.OrderAg.Order.CreateOrder;

public record CreateOrderRequest(CreateOrderRequestData Order, IReadOnlyCollection<CreateOrderItemsRequest> Items);

public record CreateOrderRequestData(
    string OrderNumber,
    string? ExternalId,
    string ManagerName,
    string ManagerPhoneNumber,
    string DeliveryAddress,
    CreateDeliveryScheduleRequest DeliverySchedule);

public record CreateDeliveryScheduleRequest(string StartTime, string EndTime);

public record CreateOrderItemsRequest(
    string? ExternalId,
    string StorageLocationCode,
    string ProductName,
    bool RequiresManufactureDate,
    string? Comment,
    decimal RequiredQuantity,
    Unit Unit);
