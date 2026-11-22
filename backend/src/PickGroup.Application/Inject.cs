using Microsoft.Extensions.DependencyInjection;
using PickGroup.Application.OrderAg.Order;
using PickGroup.Application.OrderAg.Order.CreateOrder;
using PickGroup.Application.OrderAg.Order.GetOrder;

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