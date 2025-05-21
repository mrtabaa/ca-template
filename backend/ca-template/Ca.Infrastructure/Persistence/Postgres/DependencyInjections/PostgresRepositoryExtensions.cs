using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Postgres.DependencyInjections;

public static class PostgresRepositoryExtensions
{
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
    {
        // services.AddScoped<IAuthRepository, PostgresAuthRepository>();
        
        return services;
    }
}