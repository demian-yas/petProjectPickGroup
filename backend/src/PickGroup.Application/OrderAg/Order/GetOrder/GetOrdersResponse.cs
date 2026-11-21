namespace PickGroup.Application.OrderAg.Order.GetOrder;

public record GetOrdersResponse(
    GetOrderResponse Order, 
    IReadOnlyCollection<GetOrderItemsResponse> Items);