using Ca.Application;
using Ca.Infrastructure;

namespace Ca.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddInfrastructureServices(config);
        services.AddApplicationServices();
        
        return services;
    }
}