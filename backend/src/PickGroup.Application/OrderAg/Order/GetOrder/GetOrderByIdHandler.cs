using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Application.OrderAg.Order.GetOrder;

public class GetOrderByIdHandler
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetOrderByIdResponse, Error>> GetOrderByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        OrderId orderId = OrderId.Create(id);
        
        var order = await _repository.GetOrderByIdAsync(orderId, ct);

        if (order is null)
            return Errors.General.NotFound(id, "order");

        var orderItemsData = order.Items
            .Select(orderItem => GetOrderItemsResponse.GetOrderItemsEntityData(orderItem))
            .ToList();
        
        var orderData = GetOrderResponse.GetOrderEntityData(order);

        return new GetOrderByIdResponse(orderData, orderItemsData);
    }
}