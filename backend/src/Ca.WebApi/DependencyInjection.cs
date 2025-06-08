using Ca.Application;
using Ca.Infrastructure;
using Ca.WebApi.Extensions;

namespace Ca.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddConfigsServiceMongo(config); // config only, no runtime services
        services.AddConfigsServicePostgres(config); // same

        services.AddDataProtection(); // runtime service needed by Identity token providers
        services.AddPolicyService();  // Authorization policies

        services.AddInfrastructureServices();
        services.AddApplicationServices();

        return services;
    }
}