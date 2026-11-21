namespace PickGroup.Application.OrderAg.Order.GetOrder;

public class GetOrdersHandler
{
    private readonly IOrderRepository _orderRepository;
    
    public GetOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    //public async Task<IReadOnlyCollection<GetOrderResponse>> GetOrdersAsync(int page, int pageSize)
    //{
    //    var orders = await _orderRepository.GetOrdersAsync(pageSize, page);
//
    //    var orderItemsResponse = orders
    //        .Select(x => x.Items)
    //        .ToList();
    //}
}