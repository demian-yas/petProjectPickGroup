using Microsoft.Extensions.DependencyInjection;
using PickGroup.OrderService.Application.OrderAg.Order;
using PickGroup.OrderService.Application.OrderAg.Order.Commands.CreateOrder;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrderById;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;

namespace PickGroup.OrderService.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();
        services.AddScoped<GetOrderByIdHandler>();
        services.AddScoped<GetOrdersHandler>();
        
        return services;
    }
}