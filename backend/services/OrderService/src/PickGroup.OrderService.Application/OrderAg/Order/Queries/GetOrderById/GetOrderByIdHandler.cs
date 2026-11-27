using CSharpFunctionalExtensions;
using PickGroup.OrderService.Application.OrderAg.Order.Repositories;
using PickGroup.OrderService.Domain.Common;
using PickGroup.OrderService.Domain.Orders.OrdersIds;

namespace PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrderById;

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