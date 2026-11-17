using Microsoft.Extensions.DependencyInjection;
using PickGroup.Application.OrderAg.Order;
using PickGroup.Application.OrderAg.Order.CreateOrder;

namespace PickGroup.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();
        
        return services;
    }
}