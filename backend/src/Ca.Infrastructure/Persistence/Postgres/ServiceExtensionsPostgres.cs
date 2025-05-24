using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Postgres;

public static class ServiceExtensionsPostgres
{
    public static IServiceCollection AddPostgresService(this IServiceCollection services, IConfiguration config)
    {

        return services;
    }
}