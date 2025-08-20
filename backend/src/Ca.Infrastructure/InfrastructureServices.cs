using Ca.Infrastructure.Persistence.Mongo.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
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