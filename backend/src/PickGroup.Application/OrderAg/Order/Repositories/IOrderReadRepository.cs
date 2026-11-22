using PickGroup.Application.Common.Pagination;
using PickGroup.Application.OrderAg.Order.Queries.GetOrders;

namespace PickGroup.Application.OrderAg.Order.Repositories;

public interface IOrderReadRepository
{
    public Task<PagedResult<OrderSummaryDto>> GetOrdersAsync(
        int page, int pageSize, CancellationToken ct = default);
}