namespace PickGroup.Application.OrderAg.Order.GetOrder;

public class GetOrdersHandler
{
    private readonly IOrderRepository _orderRepository;
    
    public GetOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyCollection<GetOrdersResponse>> GetOrdersAsync(
        int page, 
        int pageSize,
        CancellationToken ct = default)
    {
        var orders = await _orderRepository.GetOrdersAsync(page, pageSize, ct);

        var orderItemsResponse = orders
            .Select(o => new GetOrdersResponse(
                GetOrderResponse.GetOrderEntityData(o),
                o.Items.Select(i => GetOrderItemsResponse.GetOrderItemsEntityData(i)).ToList()))
            .ToList();
        
        return orderItemsResponse;
    }
}