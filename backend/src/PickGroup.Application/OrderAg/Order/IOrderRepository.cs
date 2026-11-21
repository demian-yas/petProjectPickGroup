using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Application.OrderAg.Order;

public interface IOrderRepository
{
    public Task AddOrderAsync(
        Domain.Orders.Order order, 
        CancellationToken cancellationToken = default);
    
    public Task<Domain.Orders.Order?> GetOrderByIdAsync(
        OrderId id, 
        CancellationToken cancellationToken = default);

    public Task<List<Domain.Orders.Order>> GetOrdersAsync(
        int page,
        int pageSize, 
        CancellationToken ct = default);
}