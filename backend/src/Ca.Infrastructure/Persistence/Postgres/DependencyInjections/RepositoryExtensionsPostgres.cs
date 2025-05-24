using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Message;
using Ca.Domain.Modules.User;
using Ca.Infrastructure.Modules.Auth.Postgres;
using Ca.Infrastructure.Modules.Message.Postgres;
using Ca.Infrastructure.Modules.User.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Postgres.DependencyInjections;

public static class RepositoryExtensionsPostgres
{
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepositoryPostgres>();
        services.AddScoped<IMessageRepository, MessageRepositoryPostgres>();
        services.AddScoped<IUserRepository, UserRepositoryPostgres>();
        
        return services;
    }
}