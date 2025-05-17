using Ca.Domain.Modules.Auth;
using Ca.Infrastructure.Modules.Auth.Mongo;
using Ca.Infrastructure.MongoCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongoService(config);
        
        services.AddScoped<IAuthRepository, MongoAuthRepository>();
        // services.AddScoped<IAuthRepository, PostgresPaymentRepository>();
        
        return services;
    }
}