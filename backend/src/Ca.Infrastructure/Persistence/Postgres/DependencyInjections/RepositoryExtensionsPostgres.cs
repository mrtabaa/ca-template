using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Postgres.DependencyInjections;

public static class RepositoryExtensionsPostgres
{
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
    {
        // services.AddScoped<IAuthRepository, AuthRepositoryPostgres>();
        
        return services;
    }
}