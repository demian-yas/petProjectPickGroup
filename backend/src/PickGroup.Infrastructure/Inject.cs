using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PickGroup.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        
        return services;
    }
}