using Ca.Infrastructure.Persistence.Mongo;
using Ca.Infrastructure.Persistence.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongoService(config);
        services.AddPostgresService(config);
        
        services.AddMongoRepositories();
        services.AddPostgresRepositories();
        
        return services;
    }
}