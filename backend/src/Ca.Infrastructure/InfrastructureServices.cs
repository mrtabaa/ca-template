using Ca.Infrastructure.Persistence.Mongo;
using Ca.Infrastructure.Persistence.Mongo.Extensions;
using Ca.Infrastructure.Persistence.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongoDbSettingsService(config);
        services.AddMongoIdentityService();
        services.AddMongoRepositories();
        
        services.AddPostgresService(config);
        services.AddPostgresRepositories();
        
        return services;
    }
}