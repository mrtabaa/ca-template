using Ca.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ca.Infrastructure.Persistence.Mongo;

public static class ServiceExtensionsMongo
{
    public static IServiceCollection AddMongoService(this IServiceCollection services, IConfiguration config)
    {
        // 1. Bind section to config class
        services.Configure<MyMongoDbSettings>(settings =>
        {
            settings.ConnectionString = config["MyMongoDbSettings:ConnectionString"]
                                        ?? throw new InvalidOperationException("MongoDB ConnectionString is missing.");

            settings.DatabaseName = config["MyMongoDbSettings:DatabaseName"]
                                    ?? throw new InvalidOperationException("MongoDB DatabaseName is missing.");
        });
        
        // 2. Register as IMyMongoDbSettings
        services.AddSingleton<IMyMongoDbSettings>(provider =>
            provider.GetRequiredService<IOptions<MyMongoDbSettings>>().Value);

        // 3. Register MongoClient
        services.AddSingleton<IMongoClient>(provider =>
        {
            var settings = provider.GetRequiredService<IOptions<MyMongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        return services;
    }
}