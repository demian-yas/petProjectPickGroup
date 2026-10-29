namespace PickGroup.Application.OrderAg.Order;

public interface IOrderRepository
{
    public Task AddOrderAsync(Domain.Orders.Order order, CancellationToken cancellationToken = default);
}