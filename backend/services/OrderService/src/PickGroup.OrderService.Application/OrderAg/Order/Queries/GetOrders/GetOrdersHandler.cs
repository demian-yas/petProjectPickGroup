using CSharpFunctionalExtensions;
using PickGroup.OrderService.Application.Common.Pagination;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrderById;
using PickGroup.OrderService.Application.OrderAg.Order.Repositories;
using PickGroup.OrderService.Domain.Common;

namespace PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;

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
            return Errors.General.ValueIsInvalid("page", nameof(page));
        
        if (pageSize < 1 || pageSize > 100)
            return Errors.General.ValueIsInvalid("pageSize", nameof(pageSize));
        
        if ((long)(page - 1) * pageSize > int.MaxValue)
            return Errors.General.ValueIsInvalid("page", nameof(page));
        
        var orders = await _orderReadRepository.GetOrdersAsync(page, pageSize, ct);
        
        return orders;
    }
}