using PickGroup.OrderService.Application.Common.Pagination;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;

namespace PickGroup.OrderService.Application.OrderAg.Order.Repositories;

public interface IOrderReadRepository
{
    public Task<PagedResult<OrderSummaryDto>> GetOrdersAsync(
        int page, int pageSize, CancellationToken ct = default);
}