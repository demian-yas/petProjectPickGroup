using Microsoft.Extensions.DependencyInjection;
using PickGroup.Application.OrderAg.Order;
using PickGroup.Application.OrderAg.Order.Commands.CreateOrder;
using PickGroup.Application.OrderAg.Order.Queries.GetOrderById;
using PickGroup.Application.OrderAg.Order.Queries.GetOrders;

namespace PickGroup.Application;

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