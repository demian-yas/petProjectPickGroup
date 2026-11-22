using PickGroup.Application.OrderAg.Order.Queries.GetOrders;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Application.OrderAg.Order.Repositories;

public interface IOrderRepository
{
    public Task AddOrderAsync(
        Domain.Orders.Order order, 
        CancellationToken cancellationToken = default);
    
    public Task<Domain.Orders.Order?> GetOrderByIdAsync(
        OrderId id, 
        CancellationToken cancellationToken = default);
}