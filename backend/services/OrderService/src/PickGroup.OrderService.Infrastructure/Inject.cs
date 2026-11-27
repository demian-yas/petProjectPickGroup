using Microsoft.Extensions.DependencyInjection;
using PickGroup.OrderService.Application.OrderAg.Order;
using PickGroup.OrderService.Application.OrderAg.Order.Repositories;
using PickGroup.OrderService.Infrastructure.Repositories.OrderAgRepositories;

namespace PickGroup.OrderService.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        
        return services;
    }
}