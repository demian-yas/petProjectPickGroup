using CSharpFunctionalExtensions;
using PickGroup.Application.Common.Pagination;
using PickGroup.Application.OrderAg.Order.Queries.GetOrderById;
using PickGroup.Application.OrderAg.Order.Repositories;
using PickGroup.Domain.Common;

namespace PickGroup.Application.OrderAg.Order.Queries.GetOrders;

public class GetOrdersHandler
{
    private readonly IOrderReadRepository _orderReadRepository;
    
    public GetOrdersHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<Result<PagedResult<OrderSummaryDto>, Error>> GetOrdersAsync(
        int page, 
        int pageSize,
        CancellationToken ct = default)
    {
        if (page < 1)
            return Errors.General.ValueIsInvalid("page < 1", nameof(page));
        
        if (pageSize < 1)
            return Errors.General.ValueIsInvalid("pageSize < 1", nameof(pageSize));
        
        var orders = await _orderReadRepository.GetOrdersAsync(page, pageSize, ct);
        
        return orders;
    }
}