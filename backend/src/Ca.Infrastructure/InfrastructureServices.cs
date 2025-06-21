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

        // services.AddRepositoriesPostgres();

        return services;
    }
}