using Ca.Infrastructure.Persistence.EFCore.Postgres.Extensions;
using Ca.Infrastructure.Persistence.Mongo.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ca.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration config, IHostEnvironment env
    )
    {
        // MongoDB
        services.AddConfigsServiceMongo(config);
        services.AddServiceMongo();
        services.AddIdentityServiceMongo();
        services.AddRepositoriesMongo();

        // Postgres
        services.AddConfigsServicePostgres(config);
        services.AddServicePostgres(env);
        // services.AddIdentityServicePostgres(); // TODO add this
        // services.AddRepositoriesPostgres();

        return services;
    }
}