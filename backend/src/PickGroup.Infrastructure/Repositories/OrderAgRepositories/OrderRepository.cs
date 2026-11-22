using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PickGroup.Application.OrderAg.Order;
using PickGroup.Application.OrderAg.Order.Repositories;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;
using PickGroup.Infrastructure.Extensions;

namespace PickGroup.Infrastructure.Repositories.OrderAgRepositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrderAsync(Order order, CancellationToken ct = default)
    {
        await _dbContext.Orders.AddAsync(order, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<Order?> GetOrderByIdAsync(OrderId id, CancellationToken ct = default)
    {
        return await _dbContext.Orders
            .Include(i => i.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}