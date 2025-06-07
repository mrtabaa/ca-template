using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Message;
using Ca.Domain.Modules.User;
using Ca.Infrastructure.Modules.Auth.Mongo;
using Ca.Infrastructure.Modules.Message.Mongo;
using Ca.Infrastructure.Modules.User.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class RepositoryExtensionsMongo
{
    public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepositoryMongo>();
        services.AddScoped<IMessageRepository, MessageRepositoryMongo>();
        services.AddScoped<IUserRepository, UserRepositoryMongo>();
        
        return services;
    }
}