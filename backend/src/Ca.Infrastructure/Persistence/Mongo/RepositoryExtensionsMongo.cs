using Ca.Domain.Modules.Auth;
using Ca.Infrastructure.Modules.Auth.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Mongo;

public static class RepositoryExtensionsMongo
{
    public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepositoryMongo>();
        
        return services;
    }
}