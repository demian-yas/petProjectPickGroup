using Microsoft.EntityFrameworkCore;
using PickGroup.OrderService.Application.Common.Pagination;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;
using PickGroup.OrderService.Application.OrderAg.Order.Repositories;
using PickGroup.OrderService.Infrastructure.Extensions;

namespace PickGroup.OrderService.Infrastructure.Repositories.OrderAgRepositories;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly ApplicationDbContext _dbContext;
 
    public OrderReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PagedResult<OrderSummaryDto>> GetOrdersAsync(
        int page, int pageSize ,CancellationToken ct = default)
    {
        var orders = await _dbContext.Orders
            .AsNoTracking()
            .OrderBy(o => o.CreatedAt)
            .ThenBy(o => o.Id)
            .Paginate(page, pageSize)
            .Select(o => new OrderSummaryDto(
                o.Id.Value,
                o.OrderNumber,
                o.Version,
                o.Status,
                o.DeliveryAddress,
                o.DeliverySchedule,
                o.CreatedAt,
                o.UpdatedAt,
                o.Items.Count))
            .ToListAsync(ct);

        var totalCount = await _dbContext.Orders.CountAsync(ct);
        
        return new PagedResult<OrderSummaryDto>(orders, page, pageSize, totalCount);
    }
}