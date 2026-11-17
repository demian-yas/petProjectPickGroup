using Microsoft.Extensions.DependencyInjection;
using PickGroup.Application.OrderAg.Order;
using PickGroup.Infrastructure.Repositories.OrderAgRepositories;

namespace PickGroup.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }
}